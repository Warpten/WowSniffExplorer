using System;
using System.Collections.Generic;

namespace SniffExplorer.Parsing.Engine.Tracking
{
    public interface IHistory<T>
    {
        public IEnumerable<T> Values { get; }
        public IEnumerable<(DateTime Moment, T Value)> DataPoints { get; }

        public T this[DateTime moment] { get; }

        public void Insert(DateTime moment, T value);

        public bool HasValue(DateTime moment);
    }
}