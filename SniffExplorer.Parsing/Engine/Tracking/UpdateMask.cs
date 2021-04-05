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
        private readonly int _limit;

        public int Length => _limit - _bitBase;

        public UpdateMask(Span<uint> values, int @base = 0, int limit = 0)
        {
            _values = values;

            // First accessible bit
            _bitBase = @base;

            // First non-accessible bit
            _limit = limit == 0 ? _bitBase + (values.Length << 5) : _bitBase + limit;
        }

        public bool this[int index]
        {
            get
            {
                // Out of bounds scan, return false
                var wordIndex = (_bitBase + index) >> 5;
                if (_bitBase + index < _limit && wordIndex < _values.Length)
                    return (_values[wordIndex] & (1 << (_bitBase + index))) != 0;

                return false;
            }
        }

        public UpdateMask LeftShift(int bitCount) => new(_values, _bitBase + bitCount);

        public UpdateMask Slice(int bitOffset, int bitCount) => new(_values, _bitBase + bitOffset, bitCount);

        public bool Any()
        {
            for (var i = _bitBase; i < _limit; ++i)
            {
                var wordIndex = i >> 5;
                if (wordIndex < _values.Length && (_values[wordIndex] & (1 << i)) != 0)
                    return true;
            }

            return false;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _limit - _bitBase; ++i)
                sb.Append(this[i] ? '1' : '0');
            return sb.ToString();
        }
    }
}
