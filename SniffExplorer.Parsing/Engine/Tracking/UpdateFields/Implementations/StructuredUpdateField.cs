using System;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations
{
    /// <summary>
    /// Describes an updatefield whose data is stored as a type instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StructuredUpdateField<T> : BaseUpdateField<T>
        where T : class
    {
        public delegate T TransformerDelegate(Span<uint> valueSpan);
        private readonly TransformerDelegate _transformer;

        private readonly uint[] _currentlyReceivingValue;

        public StructuredUpdateField(int bitOffset, int bitCount, ParsingContext context, TransformerDelegate transformer)
            : base(bitOffset, bitCount, context, HistoryFactory.CreateBoxed<T>)
        {
            _transformer = transformer;

            _currentlyReceivingValue = new uint[bitCount];
        }

        protected override T ReadValueCore(Packet packet, UpdateMask updateMask)
        {
            var valueSpan = new Span<uint>(_currentlyReceivingValue);

            for (var i = 0; i < updateMask.Length; ++i)
                if (updateMask[i])
                    valueSpan[i] = packet.ReadUInt32();

            return _transformer(_currentlyReceivingValue);
        }
    }
}