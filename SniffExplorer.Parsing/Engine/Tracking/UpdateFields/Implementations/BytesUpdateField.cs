using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations
{
    public class RawUpdateField<T> : IUpdateField<T[]> where T : unmanaged
    {
        private readonly IHistory<T[]> _values = HistoryFactory.CreateBoxed<T[]>();

        private readonly int _bitOffset;
        public int BitEnd { get; }

        public RawUpdateField(int bitOffset)
        {
            _bitOffset = bitOffset;
            BitEnd = _bitOffset + 1;
        }

        public void ReadValue(Packet packet, UpdateMask updateMask)
        {
            if (!updateMask[_bitOffset])
                return;

            var value = packet.ReadUInt32();
            var valueSpan = MemoryMarshal.Cast<uint, T>(MemoryMarshal.CreateSpan(ref value, 1));

            _values.Insert(packet.Moment, valueSpan.ToArray());
        }

        public IEnumerable<T[]> Values => _values.Values;
    }
}
