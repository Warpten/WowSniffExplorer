using SniffExplorer.Cataclysm.UpdateFields.Types;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IItemData))]
    public enum ItemData
    {
        [DescriptorValue(ValueType = typeof(IObjectGUID))]     Owner,
        [DescriptorValue(ValueType = typeof(IObjectGUID))]     Contained,
        [DescriptorValue(ValueType = typeof(IObjectGUID))]     Creator,
        [DescriptorValue(ValueType = typeof(IObjectGUID))]     GiftCreator,
        [DescriptorValue(ValueType = typeof(uint))]            StackCount,
        [DescriptorValue(ValueType = typeof(uint))]            Duration,
        [DescriptorValue(ValueType = typeof(uint), Arity = 5)] SpellCharges,
        [DescriptorValue(ValueType = typeof(uint))]            Flags,
        [DescriptorValue(ValueType = typeof(ItemEnchantment), Arity = 15)] Enchantments,
        [DescriptorValue(ValueType = typeof(uint))]            PropertySeed,
        [DescriptorValue(ValueType = typeof(uint))]            RandomPropertiesID,
        [DescriptorValue(ValueType = typeof(uint))]            Durability,
        [DescriptorValue(ValueType = typeof(uint))]            MaxDurability,
        [DescriptorValue(ValueType = typeof(uint))]            CreatePlayedTime
    }
}