using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IUnitData : IUpdateFieldStorage
    {
        public IUpdateField<IObjectGUID> Charm { get; }
        public IUpdateField<IObjectGUID> Summon { get; }
        public IUpdateField<IObjectGUID> Critter { get; }
        public IUpdateField<IObjectGUID> CharmedBy { get; }
        public IUpdateField<IObjectGUID> SummonedBy { get; }
        public IUpdateField<IObjectGUID> CreatedBy { get; }
        public IUpdateField<IObjectGUID> Target { get; }
        public IUpdateField<IObjectGUID> ChannelObject { get; }
        public IUpdateField<uint> ChannelSpell { get; }
        public IUpdateField<byte[]> Bytes0 { get; }
        public IUpdateField<uint> Health { get; }
        public IUpdateField<uint>[] Powers { get; }
        public IUpdateField<uint> MaxHealth { get; }
        public IUpdateField<uint>[] MaxPower { get; }
        public IUpdateField<float>[] PowerRegenFlatModifier { get; }
        public IUpdateField<float>[] PowerRegenInterruptedFlatModifier { get; }
        public IUpdateField<uint> Level { get; }
        public IUpdateField<uint> FactionTemplate { get; }
        public IUpdateField<uint>[] VirtualItemSlotID { get; }
        public IUpdateField<uint> Flags { get; }
        public IUpdateField<uint> Flags2 { get; }
        public IUpdateField<uint> Aurastate { get; }
        public IUpdateField<float>[] AttackTime { get; }
        public IUpdateField<float> BoundingRadius { get; }
        public IUpdateField<float> CombtaReach { get; }
        public IUpdateField<uint> DisplayID { get; }
        public IUpdateField<uint> NativeDisplayID { get; }
        public IUpdateField<uint> MountDisplayID { get; }
        public IUpdateField<float>[] Damage { get; }
        public IUpdateField<float>[] OffHandDamage { get; }
        public IUpdateField<byte[]> Bytes1 { get; }
        public IUpdateField<uint> PetNumber { get; }
        public IUpdateField<uint> PetNameTimestamp { get; }
        public IUpdateField<uint> PetExperience { get; }
        public IUpdateField<uint> PetNextLevelXP { get; }
        public IUpdateField<uint> DynamicFlags { get; }
        public IUpdateField<float> ModCastSpeed { get; }
        public IUpdateField<float> ModCastHaste { get; }
        public IUpdateField<uint> CreatedBySpell { get; }
        public IUpdateField<uint> NpcFlags { get; }
        public IUpdateField<uint> NpcEmoteState { get; }
        public IUpdateField<uint>[] Stats { get; }
        public IUpdateField<uint>[] PosStats { get; }
        public IUpdateField<uint>[] NegStats { get; }
        public IUpdateField<uint>[] Resistances { get; }
        public IUpdateField<uint>[] ResistanceBuffModsPositive { get; }
        public IUpdateField<uint>[] ResistanceBuffModsNegative { get; }
        public IUpdateField<uint> BaseMana { get; }
        public IUpdateField<uint> BaseHealth { get; }
        public IUpdateField<byte[]> Bytes2 { get; }
        public IUpdateField<uint> AttackPower { get; }
        public IUpdateField<uint> AttackPowerModPos { get; }
        public IUpdateField<uint> AttackPowerModNeg { get; }
        public IUpdateField<float> AttackPowerMultiplier { get; }
        public IUpdateField<uint> RangedAttackPower { get; }
        public IUpdateField<uint> RangedAttackPowerModPos { get; }
        public IUpdateField<uint> RangedAttackPowerModNeg { get; }
        public IUpdateField<float> RangedAttackPowerMultiplier { get; }
        public IUpdateField<float> MinRangedDamage { get; }
        public IUpdateField<float> MaxRangedDamage { get; }
        public IUpdateField<uint>[] PowerCostModifier { get; }
        public IUpdateField<float>[] PowerCostMultiplier { get; }
        public IUpdateField<float> MaxHealthModifier { get; }
        public IUpdateField<float> HoverHeight { get; }
        public IUpdateField<uint> MaxItemLevel { get; }
        public IUpdateField<uint> Padding { get; }
    }
}