using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IUnitData))]
    public enum UnitField
    {
        [DescriptorValue(typeof(IObjectGUID))]
        Charm,
        
        [DescriptorValue(typeof(IObjectGUID))]
        Summon,
        
        [DescriptorValue(typeof(IObjectGUID))]
        Critter,
        
        [DescriptorValue(typeof(IObjectGUID))]
        CharmedBy,

        [DescriptorValue(typeof(IObjectGUID))]
        SummonedBy,
        
        [DescriptorValue(typeof(IObjectGUID))]
        CreatedBy,
        
        [DescriptorValue(typeof(IObjectGUID))]
        Target,
        
        [DescriptorValue(typeof(IObjectGUID))]
        ChannelObject,
        
        [DescriptorValue(typeof(uint))]
        ChannelSpell,
        
        [DescriptorValue(typeof(byte[]))]
        Bytes0,
        
        [DescriptorValue(typeof(uint))]
        Health,
        
        [DescriptorValue(typeof(uint), 5)]
        Powers,
        
        [DescriptorValue(typeof(uint))]
        MaxHealth,
        
        [DescriptorValue(typeof(uint), 5)]
        MaxPower,
        
        [DescriptorValue(typeof(float), 5)]
        PowerRegenFlatModifier,
        
        [DescriptorValue(typeof(float), 5)]
        PowerRegenInterruptedFlatModifier,
        
        [DescriptorValue(typeof(uint))]
        Level,
        
        [DescriptorValue(typeof(uint))]
        FactionTemplate,
        
        [DescriptorValue(typeof(uint), 3)]
        VirtualItemSlotID,
        
        [DescriptorValue(typeof(uint))]
        Flags,
        
        [DescriptorValue(typeof(uint))]
        Flags2,
        
        [DescriptorValue(typeof(uint))]
        Aurastate,
        
        [DescriptorValue(typeof(float), 3)]
        AttackTime,
        
        [DescriptorValue(typeof(float))]
        BoundingRadius,
        
        [DescriptorValue(typeof(float))]
        CombtaReach,
        
        [DescriptorValue(typeof(uint))]
        DisplayID,
        
        [DescriptorValue(typeof(uint))]
        NativeDisplayID,
        
        [DescriptorValue(typeof(uint))]
        MountDisplayID,
        
        [DescriptorValue(typeof(float), 2)]
        Damage,
        
        [DescriptorValue(typeof(float), 2)]
        OffHandDamage,
        
        [DescriptorValue(typeof(byte[]))]
        Bytes1,
        
        [DescriptorValue(typeof(uint))]
        PetNumber,
        
        [DescriptorValue(typeof(uint))]
        PetNameTimestamp,
        
        [DescriptorValue(typeof(uint))]
        PetExperience,
        
        [DescriptorValue(typeof(uint))]
        PetNextLevelXP,
        
        [DescriptorValue(typeof(uint))]
        DynamicFlags,
        
        [DescriptorValue(typeof(float))]
        ModCastSpeed,
        
        [DescriptorValue(typeof(float))]
        ModCastHaste,
        
        [DescriptorValue(typeof(uint))]
        CreatedBySpell,
        
        [DescriptorValue(typeof(uint))]
        NpcFlags,
        
        [DescriptorValue(typeof(uint))]
        NpcEmoteState,
        
        [DescriptorValue(typeof(uint), 5)]
        Stats,
        
        [DescriptorValue(typeof(uint), 5)]
        PosStats,
        
        [DescriptorValue(typeof(uint), 5)]
        NegStats,
        
        [DescriptorValue(typeof(uint), 7)]
        Resistances,
        
        [DescriptorValue(typeof(uint), 7)]
        ResistanceBuffModsPositive,
        
        [DescriptorValue(typeof(uint), 7)]
        ResistanceBuffModsNegative,
        
        [DescriptorValue(typeof(uint))]
        BaseMana,
        
        [DescriptorValue(typeof(uint))]
        BaseHealth,
        
        [DescriptorValue(typeof(byte[]))]
        Bytes2,
        
        [DescriptorValue(typeof(uint))]
        AttackPower,
        
        [DescriptorValue(typeof(uint))]
        AttackPowerModPos,
        
        [DescriptorValue(typeof(uint))]
        AttackPowerModNeg,
        
        [DescriptorValue(typeof(float))]
        AttackPowerMultiplier,
        
        [DescriptorValue(typeof(uint))]
        RangedAttackPower,
        
        [DescriptorValue(typeof(uint))]
        RangedAttackPowerModPos,
        
        [DescriptorValue(typeof(uint))]
        RangedAttackPowerModNeg,
        
        [DescriptorValue(typeof(float))]
        RangedAttackPowerMultiplier,
        
        [DescriptorValue(typeof(float))]
        MinRangedDamage,
        
        [DescriptorValue(typeof(float))]
        MaxRangedDamage,
        
        [DescriptorValue(typeof(uint), 7)]
        PowerCostModifier,
        
        [DescriptorValue(typeof(float), 7)]
        PowerCostMultiplier,
        
        [DescriptorValue(typeof(float))]
        MaxHealthModifier,
        
        [DescriptorValue(typeof(float))]
        HoverHeight,
        
        [DescriptorValue(typeof(uint))]
        MaxItemLevel,
        
        [DescriptorValue(typeof(uint))]
        _,
    }
}