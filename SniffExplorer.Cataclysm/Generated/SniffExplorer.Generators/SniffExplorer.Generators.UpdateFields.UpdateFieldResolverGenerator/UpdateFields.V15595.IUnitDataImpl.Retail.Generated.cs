﻿// AUTOGENERATED FILE - DO NOT EDIT
// This file was generated by UpdateFieldResolverGenerator on 4/6/2021 1:30:38 AM.

using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595.Retail
{
    [SniffExplorer.Shared.Attributes.Descriptors.GeneratedDescriptorAttribute(ClientBuild = 15595, RealmType = SniffExplorer.Shared.Enums.RealmExpansionType.Retail)]
    public class IUnitDataImpl : SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUnitData
    {
        public int BitCount { get; }

        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<SniffExplorer.Parsing.Types.ObjectGUIDs.IObjectGUID> Charm { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<SniffExplorer.Parsing.Types.ObjectGUIDs.IObjectGUID> Summon { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<SniffExplorer.Parsing.Types.ObjectGUIDs.IObjectGUID> Critter { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<SniffExplorer.Parsing.Types.ObjectGUIDs.IObjectGUID> CharmedBy { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<SniffExplorer.Parsing.Types.ObjectGUIDs.IObjectGUID> SummonedBy { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<SniffExplorer.Parsing.Types.ObjectGUIDs.IObjectGUID> CreatedBy { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<SniffExplorer.Parsing.Types.ObjectGUIDs.IObjectGUID> Target { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<SniffExplorer.Parsing.Types.ObjectGUIDs.IObjectGUID> ChannelObject { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> ChannelSpell { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<byte[]> Bytes0 { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> Health { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[] Powers { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> MaxHealth { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[] MaxPower { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float>[] PowerRegenFlatModifier { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float>[] PowerRegenInterruptedFlatModifier { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> Level { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> FactionTemplate { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[] VirtualItemSlotID { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> Flags { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> Flags2 { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> Aurastate { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float>[] AttackTime { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float> BoundingRadius { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float> CombtaReach { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> DisplayID { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> NativeDisplayID { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> MountDisplayID { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float>[] Damage { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float>[] OffHandDamage { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<byte[]> Bytes1 { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> PetNumber { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> PetNameTimestamp { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> PetExperience { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> PetNextLevelXP { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> DynamicFlags { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float> ModCastSpeed { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float> ModCastHaste { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> CreatedBySpell { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> NpcFlags { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> NpcEmoteState { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[] Stats { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[] PosStats { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[] NegStats { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[] Resistances { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[] ResistanceBuffModsPositive { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[] ResistanceBuffModsNegative { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> BaseMana { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> BaseHealth { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<byte[]> Bytes2 { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> AttackPower { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> AttackPowerModPos { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> AttackPowerModNeg { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float> AttackPowerMultiplier { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> RangedAttackPower { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> RangedAttackPowerModPos { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> RangedAttackPowerModNeg { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float> RangedAttackPowerMultiplier { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float> MinRangedDamage { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float> MaxRangedDamage { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[] PowerCostModifier { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float>[] PowerCostMultiplier { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float> MaxHealthModifier { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float> HoverHeight { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> MaxItemLevel { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> Padding { get; }

        public IUnitDataImpl(ParsingContext context)
        {
            Charm = new GuidUpdateField(0, context);
            Summon = new GuidUpdateField(Charm.BitEnd, context);
            Critter = new GuidUpdateField(Summon.BitEnd, context);
            CharmedBy = new GuidUpdateField(Critter.BitEnd, context);
            SummonedBy = new GuidUpdateField(CharmedBy.BitEnd, context);
            CreatedBy = new GuidUpdateField(SummonedBy.BitEnd, context);
            Target = new GuidUpdateField(CreatedBy.BitEnd, context);
            ChannelObject = new GuidUpdateField(Target.BitEnd, context);
            ChannelSpell = new PrimitiveUpdateField<uint>(ChannelObject.BitEnd, context);
            Bytes0 = new RawUpdateField<System.Byte>(ChannelSpell.BitEnd, context);
            Health = new PrimitiveUpdateField<uint>(Bytes0.BitEnd, context);

            Powers = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[5];
            Powers[0] = new PrimitiveUpdateField<uint>(Health.BitEnd, context);
            for (var itr0 = 1; itr0 < 5; ++itr0)
                Powers[itr0] = new PrimitiveUpdateField<uint>(Powers[itr0 - 1].BitEnd, context);

            MaxHealth = new PrimitiveUpdateField<uint>(Powers[4].BitEnd, context);

            MaxPower = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[5];
            MaxPower[0] = new PrimitiveUpdateField<uint>(MaxHealth.BitEnd, context);
            for (var itr0 = 1; itr0 < 5; ++itr0)
                MaxPower[itr0] = new PrimitiveUpdateField<uint>(MaxPower[itr0 - 1].BitEnd, context);


            PowerRegenFlatModifier = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float>[5];
            PowerRegenFlatModifier[0] = new PrimitiveUpdateField<float>(MaxPower[4].BitEnd, context);
            for (var itr0 = 1; itr0 < 5; ++itr0)
                PowerRegenFlatModifier[itr0] = new PrimitiveUpdateField<float>(PowerRegenFlatModifier[itr0 - 1].BitEnd, context);


            PowerRegenInterruptedFlatModifier = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float>[5];
            PowerRegenInterruptedFlatModifier[0] = new PrimitiveUpdateField<float>(PowerRegenFlatModifier[4].BitEnd, context);
            for (var itr0 = 1; itr0 < 5; ++itr0)
                PowerRegenInterruptedFlatModifier[itr0] = new PrimitiveUpdateField<float>(PowerRegenInterruptedFlatModifier[itr0 - 1].BitEnd, context);

            Level = new PrimitiveUpdateField<uint>(PowerRegenInterruptedFlatModifier[4].BitEnd, context);
            FactionTemplate = new PrimitiveUpdateField<uint>(Level.BitEnd, context);

            VirtualItemSlotID = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[3];
            VirtualItemSlotID[0] = new PrimitiveUpdateField<uint>(FactionTemplate.BitEnd, context);
            for (var itr0 = 1; itr0 < 3; ++itr0)
                VirtualItemSlotID[itr0] = new PrimitiveUpdateField<uint>(VirtualItemSlotID[itr0 - 1].BitEnd, context);

            Flags = new PrimitiveUpdateField<uint>(VirtualItemSlotID[2].BitEnd, context);
            Flags2 = new PrimitiveUpdateField<uint>(Flags.BitEnd, context);
            Aurastate = new PrimitiveUpdateField<uint>(Flags2.BitEnd, context);

            AttackTime = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float>[3];
            AttackTime[0] = new PrimitiveUpdateField<float>(Aurastate.BitEnd, context);
            for (var itr0 = 1; itr0 < 3; ++itr0)
                AttackTime[itr0] = new PrimitiveUpdateField<float>(AttackTime[itr0 - 1].BitEnd, context);

            BoundingRadius = new PrimitiveUpdateField<float>(AttackTime[2].BitEnd, context);
            CombtaReach = new PrimitiveUpdateField<float>(BoundingRadius.BitEnd, context);
            DisplayID = new PrimitiveUpdateField<uint>(CombtaReach.BitEnd, context);
            NativeDisplayID = new PrimitiveUpdateField<uint>(DisplayID.BitEnd, context);
            MountDisplayID = new PrimitiveUpdateField<uint>(NativeDisplayID.BitEnd, context);

            Damage = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float>[2];
            Damage[0] = new PrimitiveUpdateField<float>(MountDisplayID.BitEnd, context);
            for (var itr0 = 1; itr0 < 2; ++itr0)
                Damage[itr0] = new PrimitiveUpdateField<float>(Damage[itr0 - 1].BitEnd, context);


            OffHandDamage = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float>[2];
            OffHandDamage[0] = new PrimitiveUpdateField<float>(Damage[1].BitEnd, context);
            for (var itr0 = 1; itr0 < 2; ++itr0)
                OffHandDamage[itr0] = new PrimitiveUpdateField<float>(OffHandDamage[itr0 - 1].BitEnd, context);

            Bytes1 = new RawUpdateField<System.Byte>(OffHandDamage[1].BitEnd, context);
            PetNumber = new PrimitiveUpdateField<uint>(Bytes1.BitEnd, context);
            PetNameTimestamp = new PrimitiveUpdateField<uint>(PetNumber.BitEnd, context);
            PetExperience = new PrimitiveUpdateField<uint>(PetNameTimestamp.BitEnd, context);
            PetNextLevelXP = new PrimitiveUpdateField<uint>(PetExperience.BitEnd, context);
            DynamicFlags = new PrimitiveUpdateField<uint>(PetNextLevelXP.BitEnd, context);
            ModCastSpeed = new PrimitiveUpdateField<float>(DynamicFlags.BitEnd, context);
            ModCastHaste = new PrimitiveUpdateField<float>(ModCastSpeed.BitEnd, context);
            CreatedBySpell = new PrimitiveUpdateField<uint>(ModCastHaste.BitEnd, context);
            NpcFlags = new PrimitiveUpdateField<uint>(CreatedBySpell.BitEnd, context);
            NpcEmoteState = new PrimitiveUpdateField<uint>(NpcFlags.BitEnd, context);

            Stats = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[5];
            Stats[0] = new PrimitiveUpdateField<uint>(NpcEmoteState.BitEnd, context);
            for (var itr0 = 1; itr0 < 5; ++itr0)
                Stats[itr0] = new PrimitiveUpdateField<uint>(Stats[itr0 - 1].BitEnd, context);


            PosStats = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[5];
            PosStats[0] = new PrimitiveUpdateField<uint>(Stats[4].BitEnd, context);
            for (var itr0 = 1; itr0 < 5; ++itr0)
                PosStats[itr0] = new PrimitiveUpdateField<uint>(PosStats[itr0 - 1].BitEnd, context);


            NegStats = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[5];
            NegStats[0] = new PrimitiveUpdateField<uint>(PosStats[4].BitEnd, context);
            for (var itr0 = 1; itr0 < 5; ++itr0)
                NegStats[itr0] = new PrimitiveUpdateField<uint>(NegStats[itr0 - 1].BitEnd, context);


            Resistances = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[7];
            Resistances[0] = new PrimitiveUpdateField<uint>(NegStats[4].BitEnd, context);
            for (var itr0 = 1; itr0 < 7; ++itr0)
                Resistances[itr0] = new PrimitiveUpdateField<uint>(Resistances[itr0 - 1].BitEnd, context);


            ResistanceBuffModsPositive = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[7];
            ResistanceBuffModsPositive[0] = new PrimitiveUpdateField<uint>(Resistances[6].BitEnd, context);
            for (var itr0 = 1; itr0 < 7; ++itr0)
                ResistanceBuffModsPositive[itr0] = new PrimitiveUpdateField<uint>(ResistanceBuffModsPositive[itr0 - 1].BitEnd, context);


            ResistanceBuffModsNegative = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[7];
            ResistanceBuffModsNegative[0] = new PrimitiveUpdateField<uint>(ResistanceBuffModsPositive[6].BitEnd, context);
            for (var itr0 = 1; itr0 < 7; ++itr0)
                ResistanceBuffModsNegative[itr0] = new PrimitiveUpdateField<uint>(ResistanceBuffModsNegative[itr0 - 1].BitEnd, context);

            BaseMana = new PrimitiveUpdateField<uint>(ResistanceBuffModsNegative[6].BitEnd, context);
            BaseHealth = new PrimitiveUpdateField<uint>(BaseMana.BitEnd, context);
            Bytes2 = new RawUpdateField<System.Byte>(BaseHealth.BitEnd, context);
            AttackPower = new PrimitiveUpdateField<uint>(Bytes2.BitEnd, context);
            AttackPowerModPos = new PrimitiveUpdateField<uint>(AttackPower.BitEnd, context);
            AttackPowerModNeg = new PrimitiveUpdateField<uint>(AttackPowerModPos.BitEnd, context);
            AttackPowerMultiplier = new PrimitiveUpdateField<float>(AttackPowerModNeg.BitEnd, context);
            RangedAttackPower = new PrimitiveUpdateField<uint>(AttackPowerMultiplier.BitEnd, context);
            RangedAttackPowerModPos = new PrimitiveUpdateField<uint>(RangedAttackPower.BitEnd, context);
            RangedAttackPowerModNeg = new PrimitiveUpdateField<uint>(RangedAttackPowerModPos.BitEnd, context);
            RangedAttackPowerMultiplier = new PrimitiveUpdateField<float>(RangedAttackPowerModNeg.BitEnd, context);
            MinRangedDamage = new PrimitiveUpdateField<float>(RangedAttackPowerMultiplier.BitEnd, context);
            MaxRangedDamage = new PrimitiveUpdateField<float>(MinRangedDamage.BitEnd, context);

            PowerCostModifier = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint>[7];
            PowerCostModifier[0] = new PrimitiveUpdateField<uint>(MaxRangedDamage.BitEnd, context);
            for (var itr0 = 1; itr0 < 7; ++itr0)
                PowerCostModifier[itr0] = new PrimitiveUpdateField<uint>(PowerCostModifier[itr0 - 1].BitEnd, context);


            PowerCostMultiplier = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<float>[7];
            PowerCostMultiplier[0] = new PrimitiveUpdateField<float>(PowerCostModifier[6].BitEnd, context);
            for (var itr0 = 1; itr0 < 7; ++itr0)
                PowerCostMultiplier[itr0] = new PrimitiveUpdateField<float>(PowerCostMultiplier[itr0 - 1].BitEnd, context);

            MaxHealthModifier = new PrimitiveUpdateField<float>(PowerCostMultiplier[6].BitEnd, context);
            HoverHeight = new PrimitiveUpdateField<float>(MaxHealthModifier.BitEnd, context);
            MaxItemLevel = new PrimitiveUpdateField<uint>(HoverHeight.BitEnd, context);
            Padding = new PrimitiveUpdateField<uint>(MaxItemLevel.BitEnd, context);

            BitCount = Padding.BitEnd;
        }

        public void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            Charm.ReadValue(packet, updateMask);
            Summon.ReadValue(packet, updateMask);
            Critter.ReadValue(packet, updateMask);
            CharmedBy.ReadValue(packet, updateMask);
            SummonedBy.ReadValue(packet, updateMask);
            CreatedBy.ReadValue(packet, updateMask);
            Target.ReadValue(packet, updateMask);
            ChannelObject.ReadValue(packet, updateMask);
            ChannelSpell.ReadValue(packet, updateMask);
            Bytes0.ReadValue(packet, updateMask);
            Health.ReadValue(packet, updateMask);

            for (var itr0 = 0; itr0 < 5; ++itr0)
                Powers[itr0].ReadValue(packet, updateMask);

            MaxHealth.ReadValue(packet, updateMask);

            for (var itr0 = 0; itr0 < 5; ++itr0)
                MaxPower[itr0].ReadValue(packet, updateMask);


            for (var itr0 = 0; itr0 < 5; ++itr0)
                PowerRegenFlatModifier[itr0].ReadValue(packet, updateMask);


            for (var itr0 = 0; itr0 < 5; ++itr0)
                PowerRegenInterruptedFlatModifier[itr0].ReadValue(packet, updateMask);

            Level.ReadValue(packet, updateMask);
            FactionTemplate.ReadValue(packet, updateMask);

            for (var itr0 = 0; itr0 < 3; ++itr0)
                VirtualItemSlotID[itr0].ReadValue(packet, updateMask);

            Flags.ReadValue(packet, updateMask);
            Flags2.ReadValue(packet, updateMask);
            Aurastate.ReadValue(packet, updateMask);

            for (var itr0 = 0; itr0 < 3; ++itr0)
                AttackTime[itr0].ReadValue(packet, updateMask);

            BoundingRadius.ReadValue(packet, updateMask);
            CombtaReach.ReadValue(packet, updateMask);
            DisplayID.ReadValue(packet, updateMask);
            NativeDisplayID.ReadValue(packet, updateMask);
            MountDisplayID.ReadValue(packet, updateMask);

            for (var itr0 = 0; itr0 < 2; ++itr0)
                Damage[itr0].ReadValue(packet, updateMask);


            for (var itr0 = 0; itr0 < 2; ++itr0)
                OffHandDamage[itr0].ReadValue(packet, updateMask);

            Bytes1.ReadValue(packet, updateMask);
            PetNumber.ReadValue(packet, updateMask);
            PetNameTimestamp.ReadValue(packet, updateMask);
            PetExperience.ReadValue(packet, updateMask);
            PetNextLevelXP.ReadValue(packet, updateMask);
            DynamicFlags.ReadValue(packet, updateMask);
            ModCastSpeed.ReadValue(packet, updateMask);
            ModCastHaste.ReadValue(packet, updateMask);
            CreatedBySpell.ReadValue(packet, updateMask);
            NpcFlags.ReadValue(packet, updateMask);
            NpcEmoteState.ReadValue(packet, updateMask);

            for (var itr0 = 0; itr0 < 5; ++itr0)
                Stats[itr0].ReadValue(packet, updateMask);


            for (var itr0 = 0; itr0 < 5; ++itr0)
                PosStats[itr0].ReadValue(packet, updateMask);


            for (var itr0 = 0; itr0 < 5; ++itr0)
                NegStats[itr0].ReadValue(packet, updateMask);


            for (var itr0 = 0; itr0 < 7; ++itr0)
                Resistances[itr0].ReadValue(packet, updateMask);


            for (var itr0 = 0; itr0 < 7; ++itr0)
                ResistanceBuffModsPositive[itr0].ReadValue(packet, updateMask);


            for (var itr0 = 0; itr0 < 7; ++itr0)
                ResistanceBuffModsNegative[itr0].ReadValue(packet, updateMask);

            BaseMana.ReadValue(packet, updateMask);
            BaseHealth.ReadValue(packet, updateMask);
            Bytes2.ReadValue(packet, updateMask);
            AttackPower.ReadValue(packet, updateMask);
            AttackPowerModPos.ReadValue(packet, updateMask);
            AttackPowerModNeg.ReadValue(packet, updateMask);
            AttackPowerMultiplier.ReadValue(packet, updateMask);
            RangedAttackPower.ReadValue(packet, updateMask);
            RangedAttackPowerModPos.ReadValue(packet, updateMask);
            RangedAttackPowerModNeg.ReadValue(packet, updateMask);
            RangedAttackPowerMultiplier.ReadValue(packet, updateMask);
            MinRangedDamage.ReadValue(packet, updateMask);
            MaxRangedDamage.ReadValue(packet, updateMask);

            for (var itr0 = 0; itr0 < 7; ++itr0)
                PowerCostModifier[itr0].ReadValue(packet, updateMask);


            for (var itr0 = 0; itr0 < 7; ++itr0)
                PowerCostMultiplier[itr0].ReadValue(packet, updateMask);

            MaxHealthModifier.ReadValue(packet, updateMask);
            HoverHeight.ReadValue(packet, updateMask);
            MaxItemLevel.ReadValue(packet, updateMask);
            Padding.ReadValue(packet, updateMask);
        }
    }
}