using System;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public class AreaTrigger : Object, IAreaTrigger
    {
        public IAreaTriggerData AreaTriggerData { get; }

        public override EntityTypeID TypeID { get; } = EntityTypeID.AreaTrigger;

        public AreaTrigger(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var areaTriggerData = context.Helper.UpdateFieldProvider.CreateAreaTriggerData(guid);

            AreaTriggerData = areaTriggerData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var areaTriggerUpdateMask = updateMask.LeftShift(ObjectData.BitCount);
            AreaTriggerData.ProcessValuesUpdate(packet, areaTriggerUpdateMask);
        }
    }
}