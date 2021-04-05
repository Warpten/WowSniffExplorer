using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations
{
    public class PrimitiveUpdateField<T> : IUpdateField<T> where T : unmanaged
    {
        private readonly IHistory<T> _values = HistoryFactory.CreatePrimitive<T>();
        private readonly Func<Packet, T> _reader;

        private readonly int _bitOffset;
        private readonly int _bitCount = Unsafe.SizeOf<T>() / Unsafe.SizeOf<uint>();
        public int BitEnd { get; }

        public IEnumerable<T> Values => _values.Values;

        private int _receivingPartsCount;
        private T _receivingValue;

        public PrimitiveUpdateField(int offset, Func<Packet, T> reader)
        {
            // Debug.Assert(Unsafe.SizeOf<T>() <= Unsafe.SizeOf<uint>(), $"Unsafe.SizeOf<{typeof(T).Name}>()=  Unsafe.SizeOf<uint>()");

            _bitOffset = offset;
            BitEnd = _bitOffset + _bitCount;

            _reader = reader;

            _receivingPartsCount = _bitCount;
            _receivingValue = default;
        }

        public PrimitiveUpdateField(int offset) : this(offset, packet => packet.Read<T>()) { }

        public void ReadValue(Packet packet, UpdateMask updateMask)
        {
            var valueSpan = MemoryMarshal.Cast<T, uint>(MemoryMarshal.CreateSpan(ref _receivingValue, 1));

            for (var i = 0; i < _bitCount; ++i)
            {
                if (!updateMask[_bitOffset + i])
                    continue;

                valueSpan[i] = packet.ReadUInt32();
                --_receivingPartsCount;
            }

            if (_receivingPartsCount == 0)
            {
                _values.Insert(packet.Moment, _receivingValue);

                _receivingPartsCount = _bitCount;
                _receivingValue = default;
            }
        }
    }
}