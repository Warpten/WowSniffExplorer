using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    /// <summary>
    /// Basic implementation of an update field (or descriptor).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseUpdateField<T> : IUpdateField<T>
    {
        public int BitEnd { get; }

        private readonly int _bitOffset;
        private readonly int _bitCount;

        private readonly ReplaySubject<(DateTime Moment, T Value)> _valueSubject;
        private readonly IHistory<T>? _values;
        
        public IEnumerable<T> Values
        {
            get
            {
                if (_values != null)
                    return _values.Values;

                // TODO: Pull out the last value or get a default one
                // Enumerable.Repeat(_valueSubject.Value.Value, 1);
                throw new InvalidOperationException("Unable to access history when disabled");
            }
        }

        public IDisposable Subscribe(IObserver<(DateTime, T)> observer)
        {
            return _valueSubject.Subscribe(observer);
        }

        protected BaseUpdateField(int bitOffset, int bitCount, ParsingContext context, Func<IHistory<T>> historyFactory)
        {
            _bitOffset = bitOffset;
            _bitCount = bitCount;

            _valueSubject = new(1);

            BitEnd = bitOffset + bitCount;
            
            if (!context.Options.DiscardUpdateFields)
            {
                _values = historyFactory();
            
                // Listen for new values and store them.
                _valueSubject.Subscribe(tuple => _values!.Insert(tuple.Moment, tuple.Value));
            }
        }

        public void ReadValue(Packet packet, UpdateMask updateMask)
        {
            var slicedMask = updateMask.Slice(_bitOffset, _bitCount);
            if (!slicedMask.Any())
                return;

            var value = ReadValueCore(packet, slicedMask);
            _valueSubject.OnNext(value: (packet.Moment, value));
        }
        
        protected abstract T ReadValueCore(Packet packet, UpdateMask updateMask);
    }
}