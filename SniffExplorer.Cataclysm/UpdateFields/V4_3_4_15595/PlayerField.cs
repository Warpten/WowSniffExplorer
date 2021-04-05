using SniffExplorer.Cataclysm.UpdateFields.Types;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IPlayerData))]
    public enum PlayerField
    {
        [DescriptorValue(ValueType = typeof(IObjectGUID))] DuelArbiter, // Size: 2, Type: LONG, Flags: PUBLIC
        [DescriptorValue(ValueType = typeof(uint))] FLAGS, // Size: 1, Type: INT, Flags: PUBLIC
        [DescriptorValue(ValueType = typeof(uint))] GuildRank, // Size: 1, Type: INT, Flags: PUBLIC
        [DescriptorValue(ValueType = typeof(uint))] GuildDeleteDate, // Size: 1, Type: INT, Flags: PUBLIC
        [DescriptorValue(ValueType = typeof(uint))] GuildLevel, // Size: 1, Type: INT, Flags: PUBLIC
        [DescriptorValue(ValueType = typeof(byte[]))] Bytes0, // Size: 1, Type: BYTES, Flags: PUBLIC
        [DescriptorValue(ValueType = typeof(byte[]))] Bytes1, // Size: 1, Type: BYTES, Flags: PUBLIC
        [DescriptorValue(ValueType = typeof(byte[]))] Bytes2, // Size: 1, Type: BYTES, Flags: PUBLIC
        [DescriptorValue(ValueType = typeof(uint))] DuelTeam, // Size: 1, Type: INT, Flags: PUBLIC
        [DescriptorValue(ValueType = typeof(uint))] GuildTimestamp, // Size: 1, Type: INT, Flags: PUBLIC
        [DescriptorValue(ValueType = typeof(QuestData), Arity = 50)] QuestLog,
        [DescriptorValue(ValueType = typeof(VisibleItem), Arity = 19)] VisibleItems,
        [DescriptorValue(ValueType = typeof(uint))] ChosenTitle, // Size: 1, Type: INT, Flags: PUBLIC
        [DescriptorValue(ValueType = typeof(uint))] FakeInebriation, // Size: 1, Type: INT, Flags: PUBLIC
        [DescriptorValue(ValueType = typeof(uint))] FIELD_PAD_0, // Size: 1, Type: INT, Flags: NONE
        [DescriptorValue(ValueType = typeof(IObjectGUID), Arity = 23 + 16 + 28 + 7)] InventorySlots, // Size: 46, Type: LONG, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(IObjectGUID), Arity = 12)] VendorBuyBackSlots, // Size: 24, Type: LONG, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(IObjectGUID))] Farsight, // Size: 2, Type: LONG, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(IBlobUpdateField), Arity = (2 + 2 + 2 + 2) * 4)] KnownTitles, // Size: 2 + 2 + 2 + 2, Type: LONG, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(uint))] XP, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(uint))] NextLevelXP, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(ushort[]), Arity = 64)] SkillLineIDs, // Size: 64, Type: TWO_SHORT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(ushort[]), Arity = 64)] SkillSteps, // Size: 64, Type: TWO_SHORT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(ushort[]), Arity = 64)] SkillRanks, // Size: 64, Type: TWO_SHORT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(ushort[]), Arity = 64)] SkillMaxRanks, // Size: 64, Type: TWO_SHORT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(ushort[]), Arity = 64)] SkillModifiers, // Size: 64, Type: TWO_SHORT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(ushort[]), Arity = 64)] SkillTalents, // Size: 64, Type: TWO_SHORT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(uint))] CharacterPoints, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(uint))] TrackCreatures, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(uint))] TrackResources, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(uint))] Expertise, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(uint))] OffHandExpertise, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] BlockPercentage, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] DodgePercentage, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] ParryPercentage, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] CritPercentage, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] RangedCritPercentage, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] OffHandCritPercentage, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float), Arity = 7)] SpellCritPercentages, // Size: 7, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] ShieldBlock, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] ShieldBlockCritPercentage, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] Mastery, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(IBlobUpdateField), Arity = 156 * 4)] ExploredZones, // Size: 156, Type: BYTES, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(uint))] RestStateExperience, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(ulong))] Coinage, // Size: 2, Type: LONG, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int), Arity = 7)] ModDamageDonePos, // Size: 7, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int), Arity = 7)] ModDamageDoneNeg, // Size: 7, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int), Arity = 7)] ModDamageDonePct, // Size: 7, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int))] ModHealingDonePos, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] ModHealingPct, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] ModHealingDonePct, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float), Arity = 3)] WeaponDamageMultipliers, // Size: 3, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] ModSpellPowerPCT, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] OverrideSpellPowerByApPct, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int))] ModTargetResistance, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int))] ModTargetPhysicalResistance, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(byte[]))] FIELD_BYTES, // Size: 1, Type: BYTES, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int))] SelfResurrectionSpell, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int))] PvPMedals, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int), Arity = 12)] BuyBackPrices, // Size: 12, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int), Arity = 12)] BuyBackTimestamp, // Size: 12, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(ushort[]))] Kills, // Size: 1, Type: TWO_SHORT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int))] LifetimeHonorableKills, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(byte[]))] Bytes3, // Size: 1, Type: 6, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int))] WatchedFactionIndex, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int), Arity = 26)] CombatRatings, // Size: 26, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int), Arity = 21)] ArenaTeamInfos, // Size: 21, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int))] BattlegroundRating, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int))] MaxLevel, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int), Arity = 25)] DailyQuests, // Size: 25, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float), Arity = 4)] RuneRegen, // Size: 4, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int), Arity = 3)] NoReagentCost, // Size: 3, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int), Arity = 9)] GlyphSlots, // Size: 9, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int), Arity = 9)] Glyphs, // Size: 9, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int))] GlyphsEnabled, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int))] PetSpellPower, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(IBlobUpdateField), Arity = 8 * 4)] Researching, // Size: 8, Type: TWO_SHORT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(IBlobUpdateField), Arity = 8 * 4)] ResearchSites, // Size: 8, Type: TWO_SHORT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int), Arity = 2)] ProfessionSkillLines, // Size: 2, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] UiHitModifier, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] UiSpellHitModifier, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(int))] HomeRealmTimeOffset, // Size: 1, Type: INT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] ModHaste, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] ModRangedHaste, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] ModPetHaste, // Size: 1, Type: FLOAT, Flags: PRIVATE
        [DescriptorValue(ValueType = typeof(float))] ModHasteRegen, // Size: 1, Type: FLOAT, Flags: PRIVATE
    }
}