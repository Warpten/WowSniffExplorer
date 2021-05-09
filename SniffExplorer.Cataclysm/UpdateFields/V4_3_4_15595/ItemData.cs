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
        [DescriptorValue(typeof(IObjectGUID))]
        Owner,

        [DescriptorValue(typeof(IObjectGUID))]
        Contained,
        
        [DescriptorValue(typeof(IObjectGUID))]
        Creator,
        
        [DescriptorValue(typeof(IObjectGUID))]
        GiftCreator,
        
        [DescriptorValue(typeof(uint))]
        StackCount,
        
        [DescriptorValue(typeof(uint))]
        Duration,
        
        [DescriptorValue(typeof(uint), 5)]
        SpellCharges,
        
        [DescriptorValue(typeof(uint))]
        Flags,
        
        [DescriptorValue(typeof(ItemEnchantment), 15)]
        Enchantments,
        
        [DescriptorValue(typeof(uint))]
        PropertySeed,
        
        [DescriptorValue(typeof(uint))]
        RandomPropertiesID,
        
        [DescriptorValue(typeof(uint))]
        Durability,
        
        [DescriptorValue(typeof(uint))]
        MaxDurability,
        
        [DescriptorValue(typeof(uint))]
        CreatePlayedTime
    }
}