using System;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public class Corpse : Object, ICorpse
    {
        public ICorpseData CorpseData { get; }

        public override EntityTypeID TypeID { get; } = EntityTypeID.Item;

        public Corpse(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var corpseData = context.Helper.UpdateFieldProvider.CreateCorpseData(guid);

            CorpseData = corpseData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var corpseUpdateMask = updateMask.LeftShift(ObjectData.BitCount);
            CorpseData.ProcessValuesUpdate(packet, corpseUpdateMask);
        }
    }
}
