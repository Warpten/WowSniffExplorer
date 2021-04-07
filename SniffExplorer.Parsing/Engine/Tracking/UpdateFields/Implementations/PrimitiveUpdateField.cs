using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations
{
    /// <summary>
    /// Describes an updatefield whose value is stored as a primitive type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PrimitiveUpdateField<T> : BaseUpdateField<T> where T : unmanaged
    {
        public PrimitiveUpdateField(int offset, ParsingContext context)
            : base(offset, ((Unsafe.SizeOf<T>() + 3) & ~3) / 4, context, HistoryFactory.CreatePrimitive<T>)
        {
        }
        
        protected override T ReadValueCore(Packet packet, UpdateMask updateMask)
        {
            T value = default;
            var valueSpan = MemoryMarshal.Cast<T, uint>(MemoryMarshal.CreateSpan(ref value, 1));

            for (var i = 0; i < updateMask.Length; ++i)
                if (updateMask[i])
                    valueSpan[i] = packet.ReadUInt32();

            return value;
        }
    }
}