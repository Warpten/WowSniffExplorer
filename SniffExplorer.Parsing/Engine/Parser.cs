using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using SniffExplorer.Parsing.Attributes;
using SniffExplorer.Parsing.Helpers;
using SniffExplorer.Parsing.Helpers.Handlers;
using SniffExplorer.Parsing.Loading;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Extensions;

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
                baseDirectory = Path.Combine(baseDirectory!, "Modules");

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

                    _parsingContext = new(file.ClientBuild, type);
                    return;
                }
            }

            throw new InvalidOperationException($"No instance of {nameof(IParseHelper)} found capable of processing this sniff.");
        }

        public void Dispose()
        {
            if (_packetParsed.IsDisposed)
                _packetParsed.Dispose();

            _parsingContext.DisposeResources();
            _file.Dispose();
        }

        public IObservable<(ParsingContext Context, ParsingStatistics Statistics)> Run(ParsingOptions options)
        {
            var contextSubject = new Subject<(ParsingContext, ParsingStatistics)>();

            _parsingContext.Options = options;

            // Collect all the packets into an observable.

            #if true
            var action = Observable.Start(() =>
            {
                var count = 0;

                foreach (var (packet, index) in _file.Enumerate(_parsingContext).Indexed())
                {
                    _parsingContext.Helper.Handlers.Process(packet);
                    _packetParsed.OnNext((packet.Opcode, index));

                    ++count;
                }

                return count;
            }, TaskPoolScheduler.Default);

            action.TimeInterval().Subscribe(t =>
            {
                var statistics = new ParsingStatistics
                {
                    ExecutionTime = t.Interval,
                    ParsedPacketCount = (uint) t.Value,
                    PacketCount = (uint) _file.Count
                };

                contextSubject.OnNext((_parsingContext, statistics));
                contextSubject.OnCompleted();
            }, err =>
            {
                contextSubject.OnError(err);
            });
            
            #else
            Observable.Start(() =>
            {
                var executionSequence = new ExecutionSequence();
                // Step 1. Process SMSG_UPDATE_OBJECT sequentially.
                executionSequence.AddStep(p => p.Opcode == Opcode.SMSG_UPDATE_OBJECT, false)
                    .AddStep(p => p.Opcode == Opcode.SMSG_SPELL_START || p.Opcode == Opcode.SMSG_SPELL_GO, false)
                    .AddStep(p => p.Opcode == Opcode.SMSG_AURA_UPDATE, false);

                executionSequence.PacketParsed.Subscribe(info => {
                    _packetParsed.OnNext(info);
                });

                // Step 2. Process everything else.
                var parseObservable = executionSequence.Execute(_file.Enumerate(_parsingContext), _parsingContext).TimeInterval();

                parseObservable.Subscribe(result =>
                {
                    var statistics = new ParsingStatistics
                    {
                        ExecutionTime = result.Interval,
                        ParsedPacketCount = (uint) result.Value,
                        PacketCount = (uint) _file.Count
                    };

                    _packetParsed.OnCompleted();

                    contextSubject.OnNext((_parsingContext, statistics));
                    contextSubject.OnCompleted();
                }, error =>
                {
                    _packetParsed.OnCompleted();

                    contextSubject.OnError(error);
                });
            });
            #endif

            return contextSubject.AsObservable();
        }
    }

    class ExecutionSequence
    {
        private readonly Subject<(Opcode, int)> _packetParsed;
        public IObservable<(Opcode Opcode, int Index)> PacketParsed => _packetParsed.AsObservable();

        private readonly LinkedList<Element> _elements = new();

        public ExecutionSequence()
        {
            _packetParsed = new ();
        }

        public ExecutionSequence AddStep(Func<Packet, bool> filter, bool processInParallel)
        {
            _elements.AddLast(new Element(filter, processInParallel, this));
            return this;
        }

        class Element
        {
            private readonly Func<Packet, bool> _filter;
            private readonly bool _parallel;
            private readonly ExecutionSequence _sequence;

            private readonly List<Packet> _queuedPackets = new();

            public Element(Func<Packet, bool> filter, bool parallel, ExecutionSequence sequence)
            {
                _filter = filter;
                _parallel = parallel;

                _sequence = sequence;
            }

            public bool TryEnqueue(Packet packet)
            {
                if (!_filter(packet))
                    return false;

                _queuedPackets.Add(packet);
                return true;
            }

            public ILookup<bool, Packet> Filter(IEnumerable<Packet> grouping)
            {
                return grouping.ToLookup(g => _filter(g));
            }

            public IObservable<long> Execute(IEnumerable<Packet> handlablePackets, ParsingContext context)
            {
                if (_parallel)
                {
                    var atomicOperations = handlablePackets.Select((p, index) =>
                    {
                        var atomicOperation = Observable.Start(() => {
                            context.Helper.Handlers.Process(p);

                            _sequence._packetParsed.OnNext((p.Opcode, index));
                        }, TaskPoolScheduler.Default);
                        
                        return atomicOperation;
                    }).ToArray();

                    return new ForkJoinObservable<Unit>(atomicOperations).Select(_ => (long) _.Length);
                }
                else
                {
                    return Observable.Start(() =>
                    {
                        var count = 0L;

                        foreach (var packet in handlablePackets)
                        {
                            ++count;

                            context.Helper.Handlers.Process(packet);
                            _sequence._packetParsed.OnNext((packet.Opcode, (int) count));
                        }

                        return count;
                    });
                }
            }
        }

        public IObservable<long> Execute(IEnumerable<Packet> packets, ParsingContext context)
        {
            // Add a last step that processes everything in parallel
            AddStep(_ => true, true);
            
            static void executeNode(LinkedListNode<Element> node, IEnumerable<Packet> remainder, ParsingContext ctx, long previousCount, Subject<long> accumulator)
            {
                var partition = node.Value.Filter(remainder);
                
                node.Value.Execute(partition[true], ctx).Subscribe(results =>
                {
                    if (node.Next == null)
                    {
                        accumulator.OnNext(results + previousCount);
                        accumulator.OnCompleted();
                    }
                    else
                        executeNode(node.Next, partition[false], ctx, results + previousCount, accumulator);
                }, accumulator.OnError);
            }

            var executionSubject = new Subject<long>();
            executeNode(_elements.First!, packets, context, 0, executionSubject);
            return executionSubject;
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

            for (var index = 0; index < count; ++index)
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

                        if (Interlocked.Exchange(ref atomicRequiredCompletedCount, 0) > 0)
                        {
                            observer.OnError(error);
                            group.Dispose();
                        }
                    },
                    () => {
                        if (Volatile.Read(ref finished))
                            return;

                        if (!hasResults[currentIndex])
                        {
                            if (Interlocked.Exchange(ref atomicRequiredCompletedCount, 0) > 0)
                                observer.OnCompleted();

                            return;
                        }

                        if (Interlocked.Decrement(ref atomicRequiredCompletedCount) > 0)
                            return;

                        Volatile.Write(ref finished, true);
                        observer.OnNext(results);
                        observer.OnCompleted();
                    }));
            }
            return group;
        }
    }
}
