using System;

namespace SniffExplorer.Generators.Utilities
{
    public class IteratorGenerator
    {
        private int _generation = 0;

        public class Iterator : IDisposable
        {
            private readonly IteratorGenerator _owner;
            private readonly string _generation;

            public Iterator(string generation, IteratorGenerator owner)
            {
                _owner = owner;
                _generation = generation;
            }

            public void Dispose()
            {
                --_owner._generation;
            }

            public override string ToString()
            {
                return _generation;
            }
        }

        public Iterator Rent()
        {
            var generation = $"itr{_generation}";
            ++_generation;

            return new Iterator(generation, this);
        }
    }
}