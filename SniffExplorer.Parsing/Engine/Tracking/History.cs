using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SniffExplorer.Parsing.Engine.Tracking
{
    public interface IHistory<T>
    {
        public IEnumerable<T> Values { get; }

        public T this[DateTime moment] { get; }

        public void Insert(DateTime moment, T value);

        public bool HasValue(DateTime moment);
    }

    /// <summary>
    /// A simple object in charge of maitaining multiple instances of the same type, as seen in different points in time.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class History<T> : IHistory<T>
    {
        private readonly ConcurrentDictionary<DateTime, T> _values = new();
        private readonly Func<T> _factory;

        public Func<T> Factory => _factory;

        internal History(Func<T> factory)
            => _factory = factory;

        public T this[DateTime moment] => _values.TryGetValue(moment, out var value) ? value : (_values[moment] = _factory());

        public void Insert(DateTime moment, T value)
            => _values[moment] = value;

        public bool HasValue(DateTime moment)
            => _values.ContainsKey(moment);

        public IEnumerable<T> Values => _values.OrderBy(kv => kv.Key).Select(kv => kv.Value);
    }

    public class BoxedHistory<T> : IHistory<T>
    {
        private readonly ConcurrentDictionary<DateTime, StrongBox<T>> _values = new();

        internal BoxedHistory() { }

        public T this[DateTime moment] => _values[moment].Value ?? throw new InvalidOperationException();

        public void Insert(DateTime moment, T value)
            => _values[moment] = new StrongBox<T>(value);

        public bool HasValue(DateTime moment)
            => _values.ContainsKey(moment);

        public IEnumerable<T> Values => _values.Values.Select(v => v.Value!);
    }

    public static class HistoryFactory
    {
        public static IHistory<U> Create<U>() where U : class, new() => new History<U>(() => new U());
        public static IHistory<U> Create<U>(Func<U> factory) where U : class => new History<U>(factory);
        public static IHistory<U> CreatePrimitive<U>() where U : unmanaged => new History<U>(() => default);
        public static IHistory<U> CreateBoxed<U>() => new BoxedHistory<U>();
    }
}