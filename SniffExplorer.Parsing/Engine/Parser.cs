using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SniffExplorer.Parsing.Attributes;
using SniffExplorer.Parsing.Helpers;
using SniffExplorer.Parsing.Loading;
using SniffExplorer.Parsing.Versions;

namespace SniffExplorer.Parsing.Engine
{
    /// <summary>
    /// The <see cref="Parser" /> is an object in charge of consuming a <see cref="SniffFile" />. It will detect the
    /// game build in the sniff and use it to resolve the methods to be called for parsing.
    /// </summary>
    public class Parser : IDisposable
    {
        /// <summary>
        /// Enumerates all modules, returning ones that could be able to process the sniff.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Parser Of(SniffFile file)
        {
            var modules = new List<Assembly>();

            // Look for all assemblies in the current directory.
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                var baseDirectory = Path.GetDirectoryName(entryAssembly.Location);

                foreach (var assemblyPath in Directory.EnumerateFiles(baseDirectory!, "*.dll", SearchOption.AllDirectories))
                {
                    try
                    {
                        var module = Assembly.LoadFile(assemblyPath);

                        foreach (var attribute in module.GetCustomAttributes<ExpansionAttribute>())
                            if (attribute.Expansion == file.ClientBuild.Expansion &&
                                attribute.RealmExpansionType == file.ClientBuild.ExpansionType)
                                modules.Add(module);
                    }
                    catch (Exception)
                    {
                        // TODO: ignore for now, but we should only try to load expansion modules
                    }
                }
            }

            return new Parser(modules, file);
        }

        private readonly SniffFile _file;
        private readonly ParsingContext _parsingContext;
        
        private readonly Subject<(Opcode, int)> _packetParsed;
        public IObservable<(Opcode Opcode, int Index)> PacketParsed => _packetParsed;
        
        private Parser(IEnumerable<Assembly> modules, SniffFile file)
        {
            _file = file;

            _packetParsed = new Subject<(Opcode, int)>();

            // Prepare parser data for each module
            foreach (var module in modules)
            {
                foreach (var type in module.GetTypes())
                {
                    if (!typeof(IParseHelper).IsAssignableFrom(type))
                        continue;

                    _parsingContext = new ParsingContext(file.ClientBuild, type);
                    return;
                }
            }

            throw new InvalidOperationException($"No instance of {nameof(IParseHelper)} found capable of processing this sniff.");
        }

        public void Dispose()
        {
            if (_packetParsed.IsDisposed)
                _packetParsed.Dispose();

            _file.Dispose();
        }

        public IObservable<(ParsingContext Context, ParsingStatistics Statistics)> Run(ParsingOptions options)
        {
            var contextSubject = new Subject<(ParsingContext, ParsingStatistics)>();

            _parsingContext.Options = options;

            // Collect all the packets into an observable.

            Observable.Start(() =>
            {
                var executionObservable = Observable.Defer(() =>
                {
                    // Enumerates all the packets in the file, in the order in which they appear.
                    var fileObservable = _file.Enumerate(_parsingContext)
                        .Select((packet, index) =>
                        {
                            // Launch an atomic parse operation.
                            var atomicOperation = Observable.Start(() =>
                            {
                                _parsingContext.Helper.Handlers.Process(packet);
                                return (packet.Opcode, Index: index);
                                
                            }, TaskPoolScheduler.Default);

                            // And subscribe to it for UI feedback.
                            atomicOperation.Subscribe(results =>
                            {
                                _packetParsed.OnNext((results.Opcode, results.Index));
                            });

                            return atomicOperation;
                        })
                        .ToArray();

                    // Collect all the observables and join them.
                    return new ForkJoinObservable<(Opcode Opcode, int Index)>(fileObservable);
                }).TimeInterval();

                // Subscribe on the overall execution to get parse statistics.
                executionObservable.Subscribe(results =>
                {
                    var statistics = new ParsingStatistics
                    {
                        ExecutionTime = results.Interval,
                        ParsedPacketCount = (uint) results.Value.Length,
                        PacketCount = (uint) _file.Count
                    };

                    _packetParsed.OnCompleted();

                    contextSubject.OnNext((_parsingContext, statistics));
                    contextSubject.OnCompleted();
                }, error =>
                {
                    _packetParsed.OnCompleted();

                    contextSubject.OnError(error);
                    contextSubject.OnCompleted();
                });
            });

            return contextSubject.AsObservable();
        }
    }

    /// <summary>
    /// Reimplementation of ForkJoin without locks.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    sealed class ForkJoinObservable<TSource> : ObservableBase<TSource[]>
    {
        private readonly IObservable<TSource>[] _sources;

        public ForkJoinObservable(IObservable<TSource>[] sources)
        {
            _sources = sources;
        }

        protected override IDisposable SubscribeCore(IObserver<TSource[]> observer)
        {
            var count = _sources.Length;
            if (count == 0)
            {
                observer.OnCompleted();
                return Disposable.Empty;
            }

            var group = new CompositeDisposable(count);

            var finished = false;
            var hasResults = new bool[count];
            var atomicRequiredCompletedCount = count;
            var results = new TSource[count];

            Parallel.For(0, count, (index, state) =>
            {
                var currentIndex = index;
                var source = _sources[index];

                group.Add(source.Subscribe(
                    value => {
                        if (Volatile.Read(ref finished))
                            return;

                        hasResults[currentIndex] = true;
                        results[currentIndex] = value;
                    },
                    error => {
                        Volatile.Write(ref finished, true);
                        observer.OnError(error);
                        group.Dispose();
                    },
                    () => {
                        if (Volatile.Read(ref finished))
                            return;

                        if (!hasResults[currentIndex])
                        {
                            observer.OnCompleted();
                            return;
                        }

                        if (Interlocked.Decrement(ref atomicRequiredCompletedCount) > 0)
                            return;

                        Volatile.Write(ref finished, true);
                        observer.OnNext(results);
                        observer.OnCompleted();
                    }));
            });
            return group;
        }
    }
}
