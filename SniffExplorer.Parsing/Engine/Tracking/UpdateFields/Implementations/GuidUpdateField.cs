using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations
{
    /// <summary>
    /// Describes an updatefield stored as a GUID.
    /// </summary>
    public class GuidUpdateField : BaseUpdateField<IObjectGUID>
    {
        private readonly ParsingContext _context;

        public GuidUpdateField(int bitOffset, ParsingContext context)
            : base(bitOffset, 
                GetSize(context),
                context, 
                () => HistoryFactory.Create(context.Helper.GuidResolver.CreateGUID))
        {
            _context = context;
        }

        protected override IObjectGUID ReadValueCore(Packet packet, UpdateMask updateMask)
        {
            var guid = _context.Helper.GuidResolver.CreateGUID();
            var wordSpan = MemoryMarshal.Cast<ulong, uint>(guid.Parts);

            for (var i = 0; i < updateMask.Length; ++i)
                if (updateMask[i])
                    wordSpan[i] = packet.ReadUInt32();

            return guid;
        }
        
        private static int GetSize(ParsingContext context)
        {
            // TODO: Extract this on GuidResolver.
            var dummyGUID = context.Helper.GuidResolver.CreateGUID();
            return MemoryMarshal.Cast<ulong, uint>(dummyGUID.Parts).Length;
        }
    }
}