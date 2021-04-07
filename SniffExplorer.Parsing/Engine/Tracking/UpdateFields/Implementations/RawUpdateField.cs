using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations
{
    // TODO: This is possibly redundant with BlobUpdateField (with size = 4 bytes)
    // TODO: It however hels for updatefields such as ushort[2] ?
    // TODO: But we have ShortsUpdateField (which isn't used in generation ...)
    // TODO: URGENT!
    public class RawUpdateField<T> : BaseUpdateField<T[]> where T : unmanaged
    {
        public RawUpdateField(int bitOffset, ParsingContext context)
            : base(bitOffset, 1, context, () => HistoryFactory.Create(() => new T[Unsafe.SizeOf<uint>() / Unsafe.SizeOf<T>()]))
        {
        }

        protected override T[] ReadValueCore(Packet packet, UpdateMask updateMask)
        {
            var value = packet.ReadUInt32();
            var valueSpan = MemoryMarshal.Cast<uint, T>(MemoryMarshal.CreateSpan(ref value, 1));
            return valueSpan.ToArray();
        }
    }
}
