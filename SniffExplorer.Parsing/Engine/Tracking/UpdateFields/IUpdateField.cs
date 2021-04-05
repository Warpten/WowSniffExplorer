using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IUpdateField
    {
        /// <summary>
        /// The index of the next bit in an <see cref="BitArray">Update mask</see> that does not correspond to this <see cref="IUpdateField"/>.
        /// </summary>
        public int BitEnd { get; }

        public void ReadValue(Packet packet, UpdateMask updateMask);
    }

    public interface IUpdateField<T> : IUpdateField
    {
        public IEnumerable<T> Values { get; }

        public IObservable<(DateTime Moment, T Value)> ValueUpdate { get; }
    }

    /// <summary>
    /// Basic implementation of an update field (or descriptor).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseUpdateField<T> : IUpdateField<T>
    {
        public int BitEnd { get; }

        /// <summary>
        /// An observable that emits a value whenever a new value for this descriptor is received.
        /// </summary>
        public IObservable<(DateTime Moment, T Value)> ValueUpdate { get; }

        protected readonly int _bitOffset;
        protected readonly int _bitCount;

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

        protected BaseUpdateField(int bitOffset, int bitCount, ParsingContext context, Func<IHistory<T>> historyFactory)
        {
            _bitOffset = bitOffset;
            _bitCount = bitCount;

            _valueSubject = new();

            BitEnd = bitOffset + bitCount;
            ValueUpdate = _valueSubject;
            
            if (!context.Options.DiscardUpdateFields)
            {
                _values = historyFactory();

                // Listen for new values on a thread pool
                // and publish these values to our cache.
                _valueSubject.Subscribe(tuple => _values!.Insert(tuple.Moment, tuple.Value));
            }
        }

        public void ReadValue(Packet packet, UpdateMask updateMask)
        {
            var slicedMask = updateMask.Slice(_bitOffset, _bitCount);
            if (!slicedMask.Any())
                return;

            var value = ReadValueCore(packet, slicedMask);
            _valueSubject.OnNext(value: (packet.Moment, value!));
        }
        
        protected abstract T ReadValueCore(Packet packet, UpdateMask updateMask);
    }

}
