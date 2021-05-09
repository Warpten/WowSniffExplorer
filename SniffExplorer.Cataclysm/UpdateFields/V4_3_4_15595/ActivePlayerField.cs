using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IActivePlayerData))]
    public enum ActivePlayerField
    {
        [DescriptorValue(typeof(IObjectGUID), 23 + 16 + 28 + 7)]
        InventorySlots,

        [DescriptorValue(typeof(IObjectGUID), 12)]
        VendorBuyBackSlots,

        [DescriptorValue(typeof(IObjectGUID))] 
        Farsight,

        [DescriptorValue(typeof(IBlobUpdateField), (2 + 2 + 2 + 2) * 4)]
        KnownTitles,

        [DescriptorValue(typeof(uint))]
        XP,

        [DescriptorValue(typeof(uint))]
        NextLevelXP,

        [DescriptorValue(typeof(ushort[]), 64)]
        SkillLineIDs,

        [DescriptorValue(typeof(ushort[]), 64)]
        SkillSteps,

        [DescriptorValue(typeof(ushort[]), 64)]
        SkillRanks,

        [DescriptorValue(typeof(ushort[]), 64)]
        SkillMaxRanks,

        [DescriptorValue(typeof(ushort[]), 64)]
        SkillModifiers,

        [DescriptorValue(typeof(ushort[]), 64)]
        SkillTalents,

        [DescriptorValue(typeof(uint))]
        CharacterPoints,

        [DescriptorValue(typeof(uint))]
        TrackCreatures,

        [DescriptorValue(typeof(uint))]
        TrackResources,

        [DescriptorValue(typeof(uint))]
        Expertise,

        [DescriptorValue(typeof(uint))]
        OffHandExpertise,

        [DescriptorValue(typeof(float))]
        BlockPercentage,

        [DescriptorValue(typeof(float))]
        DodgePercentage,

        [DescriptorValue(typeof(float))]
        ParryPercentage,

        [DescriptorValue(typeof(float))]
        CritPercentage,

        [DescriptorValue(typeof(float))]
        RangedCritPercentage,

        [DescriptorValue(typeof(float))]
        OffHandCritPercentage,

        [DescriptorValue(typeof(float), 7)]
        SpellCritPercentages,

        [DescriptorValue(typeof(float))]
        ShieldBlock,

        [DescriptorValue(typeof(float))]
        ShieldBlockCritPercentage,

        [DescriptorValue(typeof(float))]
        Mastery,

        [DescriptorValue(typeof(IBlobUpdateField), 156 * 4)]
        ExploredZones,

        [DescriptorValue(typeof(uint))]
        RestStateExperience,

        [DescriptorValue(typeof(ulong))]
        Coinage,

        [DescriptorValue(typeof(int), 7)]
        ModDamageDonePos,

        [DescriptorValue(typeof(int), 7)]
        ModDamageDoneNeg,

        [DescriptorValue(typeof(int), 7)]
        ModDamageDonePct,

        [DescriptorValue(typeof(int))]
        ModHealingDonePos,

        [DescriptorValue(typeof(float))]
        ModHealingPct,

        [DescriptorValue(typeof(float))]
        ModHealingDonePct,

        [DescriptorValue(typeof(float), 3)]
        WeaponDamageMultipliers,

        [DescriptorValue(typeof(float))]
        ModSpellPowerPCT,

        [DescriptorValue(typeof(float))]
        OverrideSpellPowerByApPct,

        [DescriptorValue(typeof(int))]
        ModTargetResistance,

        [DescriptorValue(typeof(int))]
        ModTargetPhysicalResistance,

        [DescriptorValue(typeof(byte[]))]
        FIELD_BYTES,

        [DescriptorValue(typeof(int))]
        SelfResurrectionSpell,

        [DescriptorValue(typeof(int))]
        PvPMedals,

        [DescriptorValue(typeof(int), 12)]
        BuyBackPrices,

        [DescriptorValue(typeof(int), 12)]
        BuyBackTimestamp,

        [DescriptorValue(typeof(ushort[]))]
        Kills,

        [DescriptorValue(typeof(int))]
        LifetimeHonorableKills,

        [DescriptorValue(typeof(byte[]))]
        Bytes3,

        [DescriptorValue(typeof(int))]
        WatchedFactionIndex,

        [DescriptorValue(typeof(int), 26)]
        CombatRatings,

        [DescriptorValue(typeof(int), 21)]
        ArenaTeamInfos,

        [DescriptorValue(typeof(int))]
        BattlegroundRating,

        [DescriptorValue(typeof(int))]
        MaxLevel,

        [DescriptorValue(typeof(int), 25)]
        DailyQuests,

        [DescriptorValue(typeof(float), 4)]
        RuneRegen,

        [DescriptorValue(typeof(int), 3)]
        NoReagentCost,

        [DescriptorValue(typeof(int), 9)]
        GlyphSlots,

        [DescriptorValue(typeof(int), 9)]
        Glyphs,

        [DescriptorValue(typeof(int))]
        GlyphsEnabled,

        [DescriptorValue(typeof(int))]
        PetSpellPower,

        [DescriptorValue(typeof(IBlobUpdateField), 8 * 4)]
        Researching,

        [DescriptorValue(typeof(IBlobUpdateField), 8 * 4)]
        ResearchSites,

        [DescriptorValue(typeof(int), 2)]
        ProfessionSkillLines,

        [DescriptorValue(typeof(float))]
        UiHitModifier,

        [DescriptorValue(typeof(float))]
        UiSpellHitModifier,

        [DescriptorValue(typeof(int))]
        HomeRealmTimeOffset,

        [DescriptorValue(typeof(float))]
        ModHaste,

        [DescriptorValue(typeof(float))]
        ModRangedHaste,

        [DescriptorValue(typeof(float))]
        ModPetHaste,

        [DescriptorValue(typeof(float))]
        ModHasteRegen,
    }
}