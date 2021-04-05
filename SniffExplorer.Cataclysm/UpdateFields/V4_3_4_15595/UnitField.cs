﻿using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IUnitData))]
    public enum UnitField
    {
        [DescriptorValue(ValueType = typeof(IObjectGUID))]      Charm,
        [DescriptorValue(ValueType = typeof(IObjectGUID))]      Summon,
        [DescriptorValue(ValueType = typeof(IObjectGUID))]      Critter,
        [DescriptorValue(ValueType = typeof(IObjectGUID))]      CharmedBy,
        [DescriptorValue(ValueType = typeof(IObjectGUID))]      SummonedBy,
        [DescriptorValue(ValueType = typeof(IObjectGUID))]      CreatedBy,
        [DescriptorValue(ValueType = typeof(IObjectGUID))]      Target,
        [DescriptorValue(ValueType = typeof(IObjectGUID))]      ChannelObject,
        [DescriptorValue(ValueType = typeof(uint))]             ChannelSpell,
        [DescriptorValue(ValueType = typeof(byte[]))]           Bytes0,
        [DescriptorValue(ValueType = typeof(uint))]             Health,
        [DescriptorValue(ValueType = typeof(uint), Arity = 5)]  Powers,
        [DescriptorValue(ValueType = typeof(uint))]             MaxHealth,
        [DescriptorValue(ValueType = typeof(uint), Arity = 5)]  MaxPower,
        [DescriptorValue(ValueType = typeof(float), Arity = 5)] PowerRegenFlatModifier,
        [DescriptorValue(ValueType = typeof(float), Arity = 5)] PowerRegenInterruptedFlatModifier,
        [DescriptorValue(ValueType = typeof(uint))]             Level,
        [DescriptorValue(ValueType = typeof(uint))]             FactionTemplate,
        [DescriptorValue(ValueType = typeof(uint), Arity = 3)]  VirtualItemSlotID,
        [DescriptorValue(ValueType = typeof(uint))]             Flags,
        [DescriptorValue(ValueType = typeof(uint))]             Flags2,
        [DescriptorValue(ValueType = typeof(uint))]             Aurastate,
        [DescriptorValue(ValueType = typeof(float), Arity = 3)] AttackTime,
        [DescriptorValue(ValueType = typeof(float))]            BoundingRadius,
        [DescriptorValue(ValueType = typeof(float))]            CombtaReach,
        [DescriptorValue(ValueType = typeof(uint))]             DisplayID,
        [DescriptorValue(ValueType = typeof(uint))]             NativeDisplayID,
        [DescriptorValue(ValueType = typeof(uint))]             MountDisplayID,
        [DescriptorValue(ValueType = typeof(float), Arity = 2)] Damage,
        [DescriptorValue(ValueType = typeof(float), Arity = 2)] OffHandDamage,
        [DescriptorValue(ValueType = typeof(byte[]))]           Bytes1,
        [DescriptorValue(ValueType = typeof(uint))]             PetNumber,
        [DescriptorValue(ValueType = typeof(uint))]             PetNameTimestamp,
        [DescriptorValue(ValueType = typeof(uint))]             PetExperience,
        [DescriptorValue(ValueType = typeof(uint))]             PetNextLevelXP,
        [DescriptorValue(ValueType = typeof(uint))]             DynamicFlags,
        [DescriptorValue(ValueType = typeof(float))]            ModCastSpeed,
        [DescriptorValue(ValueType = typeof(float))]            ModCastHaste,
        [DescriptorValue(ValueType = typeof(uint))]             CreatedBySpell,
        [DescriptorValue(ValueType = typeof(uint))]             NpcFlags,
        [DescriptorValue(ValueType = typeof(uint))]             NpcEmoteState,
        [DescriptorValue(ValueType = typeof(uint), Arity = 5)]  Stats,
        [DescriptorValue(ValueType = typeof(uint), Arity = 5)]  PosStats,
        [DescriptorValue(ValueType = typeof(uint), Arity = 5)]  NegStats,
        [DescriptorValue(ValueType = typeof(uint), Arity = 7)]  Resistances,
        [DescriptorValue(ValueType = typeof(uint), Arity = 7)]  ResistanceBuffModsPositive,
        [DescriptorValue(ValueType = typeof(uint), Arity = 7)]  ResistanceBuffModsNegative,
        [DescriptorValue(ValueType = typeof(uint))]             BaseMana,
        [DescriptorValue(ValueType = typeof(uint))]             BaseHealth,
        [DescriptorValue(ValueType = typeof(byte[]))]           Bytes2,
        [DescriptorValue(ValueType = typeof(uint))]             AttackPower,
        [DescriptorValue(ValueType = typeof(uint))]             AttackPowerModPos,
        [DescriptorValue(ValueType = typeof(uint))]             AttackPowerModNeg,
        [DescriptorValue(ValueType = typeof(float))]            AttackPowerMultiplier,
        [DescriptorValue(ValueType = typeof(uint))]             RangedAttackPower,
        [DescriptorValue(ValueType = typeof(uint))]             RangedAttackPowerModPos,
        [DescriptorValue(ValueType = typeof(uint))]             RangedAttackPowerModNeg,
        [DescriptorValue(ValueType = typeof(float))]            RangedAttackPowerMultiplier,
        [DescriptorValue(ValueType = typeof(float))]            MinRangedDamage,
        [DescriptorValue(ValueType = typeof(float))]            MaxRangedDamage,
        [DescriptorValue(ValueType = typeof(uint), Arity = 7)]  PowerCostModifier,
        [DescriptorValue(ValueType = typeof(float), Arity = 7)] PowerCostMultiplier,
        [DescriptorValue(ValueType = typeof(float))]            MaxHealthModifier,
        [DescriptorValue(ValueType = typeof(float))]            HoverHeight,
        [DescriptorValue(ValueType = typeof(uint))]             MaxItemLevel,
        [DescriptorValue(ValueType = typeof(uint))]             Padding,
    }
}