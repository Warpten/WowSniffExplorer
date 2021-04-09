using System;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public class Item : Object, IItem
    {
        public IItemData ItemData { get; }

        public override EntityTypeID TypeID { get; } = EntityTypeID.Item;

        public Item(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var itemData = context.Helper.UpdateFieldProvider.CreateItemData(guid);

            ItemData = itemData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var itemUpdateMask = updateMask.LeftShift(ObjectData.BitCount);
            ItemData.ProcessValuesUpdate(packet, itemUpdateMask);
        }
    }
}