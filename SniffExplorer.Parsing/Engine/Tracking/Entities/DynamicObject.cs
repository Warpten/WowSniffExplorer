using System;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public class DynamicObject : Object, IDynamicObject
    {
        public IDynamicObjectData DynamicObjectData { get; }

        public override EntityTypeID TypeID { get; } = EntityTypeID.DynamicObject;

        public DynamicObject(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var dynamicObjectData = context.Helper.UpdateFieldProvider.CreateDynamicObjectData(guid);

            DynamicObjectData = dynamicObjectData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var dynamicObjectUpdateMask = updateMask.LeftShift(ObjectData.BitCount);
            DynamicObjectData.ProcessValuesUpdate(packet, dynamicObjectUpdateMask);
        }
    }
}