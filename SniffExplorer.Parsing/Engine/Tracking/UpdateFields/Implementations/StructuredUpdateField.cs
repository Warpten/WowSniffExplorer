using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations
{
    public class StructuredUpdateField<T> : IUpdateField<T>
        where T : class
    {
        private readonly IHistory<T> _values;

        private readonly int _bitOffset;
        private readonly int _bitCount;
        public int BitEnd { get; }

        public IEnumerable<T> Values => _values.Values;

        public delegate T TransformerDelegate(Span<uint> valueSpan);
        private readonly TransformerDelegate _transformer;

        private readonly uint[] _currentlyReceivingValue;
        private int _remainingWordCount;

        public StructuredUpdateField(int bitOffset, int bitCount, TransformerDelegate transformer)
        {
            _bitOffset = bitOffset;
            _bitCount = bitCount;
            BitEnd = _bitOffset + _bitCount;

            _values = HistoryFactory.CreateBoxed<T>();
            _transformer = transformer;

            _currentlyReceivingValue = new uint[bitCount];
            _remainingWordCount = _bitCount;
        }

        public void ReadValue(Packet packet, UpdateMask updateMask)
        {
            var valueSpan = new Span<uint>(_currentlyReceivingValue);

            for (var i = 0; i < _bitCount; ++i)
            {
                if (!updateMask[_bitOffset + i])
                    continue;

                valueSpan[i] = packet.ReadUInt32();
                --_remainingWordCount;
            }

            if (_remainingWordCount == 0)
            {
                _values.Insert(packet.Moment, _transformer(valueSpan));

                Array.Clear(_currentlyReceivingValue, 0, _currentlyReceivingValue.Length);
                _remainingWordCount = _bitCount;
            }
        }
    }
}