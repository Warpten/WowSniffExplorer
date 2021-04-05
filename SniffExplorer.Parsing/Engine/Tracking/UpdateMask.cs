using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SniffExplorer.Parsing.Engine.Tracking
{
    public readonly ref struct UpdateMask
    {
        private readonly Span<uint> _values;
        private readonly int _bitBase;

        public UpdateMask(Span<uint> values, int @base = 0)
        {
            _values = values;
            _bitBase = @base;
        }

        public bool this[int index]
        {
            get
            {
                // Out of bounds scan, return false
                var wordIndex = (_bitBase + index) >> 5;
                if (wordIndex < _values.Length)
                    return (_values[wordIndex] & (1 << (_bitBase + index))) != 0;

                return false;
            }
        }

        public UpdateMask LeftShift(int bitCount) => new(_values, _bitBase + bitCount);

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _values.Length * Unsafe.SizeOf<uint>() * 8; ++i)
                sb.Append(this[i] ? '1' : '0');
            return sb.ToString();
        }
    }
}
