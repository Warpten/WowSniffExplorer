using System.Collections.Generic;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations
{
    public class ShortsUpdateField : IUpdateField<short[]>
    {
        private readonly IHistory<short[]> _values = HistoryFactory.CreateBoxed<short[]>();

        private readonly int _bitOffset;
        public int BitEnd { get; }

        public ShortsUpdateField(int bitOffset)
        {
            _bitOffset = bitOffset;
            BitEnd = _bitOffset + 1;
        }

        public void ReadValue(Packet packet, UpdateMask updateMask)
        {
            if (!updateMask[_bitOffset])
                return;

            var value = packet.ReadUInt32();
            var valueSpan = MemoryMarshal.Cast<uint, short>(MemoryMarshal.CreateSpan(ref value, 1));

            _values.Insert(packet.Moment, valueSpan.ToArray());
        }

        public IEnumerable<short[]> Values => _values.Values;
    }
}