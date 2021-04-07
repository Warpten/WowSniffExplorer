using System;
using System.Linq;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations
{
    /// <summary>
    /// Describes an updatefield stored as a byte array.
    /// </summary>
    public class BlobUpdateField : BaseUpdateField<byte[]>
    {
        // Accumulates value changes.
        private readonly byte[] _currentValue;
        
        /// <summary>
        /// Creates an instance of <see cref="BlobUpdateField"/>
        /// </summary>
        /// <param name="bitOffset">The offset of the first bit describing this updatefield in the <see cref="UpdateMask"/>.</param>
        /// <param name="byteCount">The amount of bytes this blob spans.</param>
        /// <param name="context">The parsing context.</param>
        public BlobUpdateField(int bitOffset, int byteCount, ParsingContext context)
            : base(bitOffset,
                ((byteCount + 3) & ~3) / 4, // Compute the amount of bits (align up to u32 boundary)
                context, 
                () => HistoryFactory.Create(() => new byte[byteCount]))
        {
            _currentValue = new byte[byteCount];
        }

        protected override byte[] ReadValueCore(Packet packet, UpdateMask updateMask)
        {
            var valueSpan = MemoryMarshal.Cast<byte, uint>(new Span<byte>(_currentValue));

            for (var i = 0; i < updateMask.Length; ++i)
            {
                if (updateMask[i])
                    valueSpan[i] = packet.ReadUInt32();
            }

            // Return a copy of the array.
            return _currentValue.ToArray();
        }
    }
}
