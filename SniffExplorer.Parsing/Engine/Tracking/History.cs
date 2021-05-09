using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SniffExplorer.Parsing.Engine.Tracking
{
    /// <summary>
    /// A simple object in charge of maitaining multiple instances of the same type, as seen in different points in time.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="D"></typeparam>
    public class History<T, D> : IHistory<T> where D : class, IDictionary<DateTime, T>, new()
    {
        private readonly D _values = new();
        private readonly Func<T> _factory;

        internal History(Func<T> factory)
        {
            _factory = factory;
        }

        public T this[DateTime moment] => _values.TryGetValue(moment, out var value) ? value : (_values[moment] = _factory());

        public void Insert(DateTime moment, T value)
            => _values[moment] = value;

        public bool HasValue(DateTime moment)
            => _values.ContainsKey(moment);

        public IEnumerable<T> Values => _values.Values;
        public IEnumerable<(DateTime, T)> DataPoints => _values.Select(kp => (kp.Key, kp.Value));
    }

    public class BoxedHistory<T, D> : IHistory<T> where D : class, IDictionary<DateTime, StrongBox<T>>, new()
    {
        private readonly D _values = new();

        internal BoxedHistory() { }

        public T this[DateTime moment] => _values[moment].Value ?? throw new InvalidOperationException();

        public void Insert(DateTime moment, T value)
            => _values[moment] = new(value);

        public bool HasValue(DateTime moment)
            => _values.ContainsKey(moment);

        public IEnumerable<T> Values => _values.Values.Select(v => v.Value!);
        public IEnumerable<(DateTime, T)> DataPoints => _values.Select(kv => (kv.Key, kv.Value.Value!));
    }

    public static class HistoryFactory
    {
        public static IHistory<U> Create<U>() where U : class, new() => new History<U, Dictionary<DateTime, U>>(() => new U());
        public static IHistory<U> Create<U>(Func<U> factory) where U : class => new History<U, Dictionary<DateTime, U>>(factory);
        public static IHistory<U> CreatePrimitive<U>() where U : unmanaged => new History<U, Dictionary<DateTime, U>>(() => default);
        public static IHistory<U> CreateBoxed<U>() => new BoxedHistory<U, Dictionary<DateTime, StrongBox<U>>>();

        public static IHistory<U> CreateConcurrent<U>() where U : class, new() => new History<U, ConcurrentDictionary<DateTime, U>>(() => new U());
        public static IHistory<U> CreateConcurrent<U>(Func<U> factory) where U : class => new History<U, ConcurrentDictionary<DateTime, U>>(factory);
        public static IHistory<U> CreatePrimitiveConcurrent<U>() where U : unmanaged => new History<U, ConcurrentDictionary<DateTime, U>>(() => default);
        public static IHistory<U> CreateBoxedConcurrent<U>() => new BoxedHistory<U, ConcurrentDictionary<DateTime, StrongBox<U>>>();
    }
}