using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Running;

namespace SniffExplorer.Benchmarks
{
    [MemoryDiagnoser, ShortRunJob]
    [ThreadingDiagnoser]
    public class ForkJoinBenchmark
    {
        [Params(10, 100, 1000, 10000)]
        public int ObservableCount { get; set; }
        
        [Benchmark(Description = "Rx.NET ForkJoin()")]
        public async Task<long> RxForkJoin()
        {
            var value = await Enumerable.Range(1, ObservableCount)
                .Select(v => Observable.Start(() => (long) v, TaskPoolScheduler.Default))
                .ForkJoin()
                .Select(values => values.Sum());

            Debug.Assert(value == ObservableCount * (ObservableCount + 1) / 2);
            return value;
        }

        [Benchmark(Description = "Custom ForkJoin()")]
        public async Task<long> CustomForkJoin()
        {
            var value = await new ForkJoinObservable<long>(
                Enumerable.Range(1, ObservableCount)
                    .Select(v => Observable.Start(() => (long) v, TaskPoolScheduler.Default)).ToArray()
                ).Select(values => values.Sum());

            Debug.Assert(value == ObservableCount * (ObservableCount + 1) / 2);

            return value;
        }
    }

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

            var group = new IDisposable?[count];

            var hasResults = new bool[count];
            var atomicRequiredCompletedCount = count;
            var results = new TSource[count];

            var finished = false;
            
            for (var index = 0; index < count; ++index)
            {
                var currentIndex = index;
                var source = _sources[index];

                var subscription = source.Subscribe(
                    value => {
                        if (Volatile.Read(ref finished))
                            return;

                        hasResults[currentIndex] = true;
                        results[currentIndex] = value;
                    },
                    error =>
                    {
                        Volatile.Write(ref finished, true);

                        if (Interlocked.Exchange(ref atomicRequiredCompletedCount, 0) > 0)
                        {
                            observer.OnError(error);

                            for (var i = 0; i < count; ++i)
                                Volatile.Read(ref group[i])?.Dispose();
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
                    });
                
                Volatile.Write(ref group[index], subscription);
            }

            return new CompositeDisposable(group!);
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<ForkJoinBenchmark>(); // new DebugInProcessConfig());
        }
    }
}
