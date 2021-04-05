using System.Collections.Generic;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations
{
    public class ShortsUpdateField : BaseUpdateField<short[]>
    {
        public ShortsUpdateField(int bitOffset, ParsingContext context)
            : base(bitOffset, 1, context, () => HistoryFactory.Create(() => new short[2]))
        {
        }

        protected override short[] ReadValueCore(Packet packet, UpdateMask updateMask)
        {
            var value = packet.ReadUInt32();
            var valueSpan = MemoryMarshal.Cast<uint, short>(MemoryMarshal.CreateSpan(ref value, 1));

            return valueSpan.ToArray();
        }
    }
}