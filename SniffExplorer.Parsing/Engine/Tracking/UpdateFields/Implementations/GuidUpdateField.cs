using System.Collections.Generic;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations
{
    public class GuidUpdateField : IUpdateField<IObjectGUID>
    {
        private readonly IHistory<IObjectGUID> _values;
        private readonly ParsingContext _context;

        private readonly int _bitOffset;
        private readonly int _bitCount;
        public int BitEnd { get; }

        public IEnumerable<IObjectGUID> Values => _values.Values;

        private int _remainingWordCount;
        private IObjectGUID _currentlyReceivingValue;

        public GuidUpdateField(int offset, ParsingContext context)
        {
            _bitOffset = offset;

            _values = HistoryFactory.Create(context.Helper.GuidResolver.CreateGUID);
            _context = context;

            _currentlyReceivingValue = context.Helper.GuidResolver.CreateGUID();
            _bitCount = MemoryMarshal.Cast<ulong, uint>(_currentlyReceivingValue.Parts).Length;
            _remainingWordCount = _bitCount;

            BitEnd = _bitOffset + _bitCount;
        }

        public void ReadValue(Packet packet, UpdateMask updateMask)
        {
            var wordSpan = MemoryMarshal.Cast<ulong, uint>(_currentlyReceivingValue.Parts);

            for (var i = 0; i < _bitCount; ++i)
            {
                if (!updateMask[_bitOffset + i])
                    continue;

                wordSpan[i] = packet.ReadUInt32();
                --_remainingWordCount;
            }

            if (_remainingWordCount == 0)
            {
                _values.Insert(packet.Moment, _currentlyReceivingValue);

                _currentlyReceivingValue = _context.Helper.GuidResolver.CreateGUID();
                _remainingWordCount = _bitCount;
            }
        }
    }
}