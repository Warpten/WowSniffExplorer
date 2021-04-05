using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations
{
    public class BlobUpdateField : IUpdateField<byte[]>
    {
        private readonly IHistory<byte[]> _values = HistoryFactory.CreateBoxed<byte[]>();

        private readonly int _bitOffset;
        private readonly int _bitCount;
        public int BitEnd { get; }

        private byte[] _currentValue;

        public BlobUpdateField(int bitOffset, int byteCount)
        {
            _bitOffset = bitOffset;
            _bitCount = ((byteCount + 3) & ~3) / 4;
            BitEnd = _bitOffset + _bitCount;

            _currentValue = new byte[byteCount];
        }

        public void ReadValue(Packet packet, UpdateMask updateMask)
        {
            var valueSpan = MemoryMarshal.Cast<byte, uint>(new Span<byte>(_currentValue));

            var modified = false;
            for (var i = 0; i < _bitCount; ++i)
            {
                if (!updateMask[_bitOffset + i])
                    continue;

                modified = true;
                valueSpan[i] = packet.ReadUInt32();
            }

            if (modified)
            {
                _values.Insert(packet.Moment, _currentValue);

                _currentValue = new byte[_currentValue.Length];
            }
        }

        public IEnumerable<byte[]> Values => _values.Values;
    }
}
