using System;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public class Container : Item, IContainer
    {
        public IContainerData ContainerData { get; }

        public override EntityTypeID TypeID { get; } = EntityTypeID.Container;

        public Container(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var containerData = context.Helper.UpdateFieldProvider.CreateContainerData(guid);

            ContainerData = containerData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var containerUpdateMask = updateMask.LeftShift(ObjectData.BitCount + ItemData.BitCount);
            ContainerData.ProcessValuesUpdate(packet, containerUpdateMask);
        }
    }
}