namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IPlayerData : IUpdateFieldStorage
    {
        public IUpdateField DuelArbiter { get; }
        public IUpdateField FLAGS { get; }
        public IUpdateField GuildRank { get; }
        public IUpdateField GuildDeleteDate { get; }
        public IUpdateField GuildLevel { get; }
        public IUpdateField Bytes0 { get; }
        public IUpdateField Bytes1 { get; }
        public IUpdateField Bytes2 { get; }
        public IUpdateField DuelTeam { get; }
        public IUpdateField GuildTimestamp { get; }
        public IUpdateField[] QuestLog { get; }
        public IUpdateField[] VisibleItems { get; }
        public IUpdateField ChosenTitle { get; }
        public IUpdateField FakeInebriation { get; }
        public IUpdateField _ { get; }
    }

    public interface IActivePlayerData : IUpdateFieldStorage
    {
        public IUpdateField[] InventorySlots { get; }
        public IUpdateField[] VendorBuyBackSlots { get; }
        public IUpdateField Farsight { get; }
        public IUpdateField KnownTitles { get; }
        public IUpdateField XP { get; }
        public IUpdateField NextLevelXP { get; }
        public IUpdateField[] SkillLineIDs { get; }
        public IUpdateField[] SkillSteps { get; }
        public IUpdateField[] SkillRanks { get; }
        public IUpdateField[] SkillMaxRanks { get; }
        public IUpdateField[] SkillModifiers { get; }
        public IUpdateField[] SkillTalents { get; }
        public IUpdateField CharacterPoints { get; }
        public IUpdateField TrackCreatures { get; }
        public IUpdateField TrackResources { get; }
        public IUpdateField Expertise { get; }
        public IUpdateField OffHandExpertise { get; }
        public IUpdateField BlockPercentage { get; }
        public IUpdateField DodgePercentage { get; }
        public IUpdateField ParryPercentage { get; }
        public IUpdateField CritPercentage { get; }
        public IUpdateField RangedCritPercentage { get; }
        public IUpdateField OffHandCritPercentage { get; }
        public IUpdateField[] SpellCritPercentages { get; }
        public IUpdateField ShieldBlock { get; }
        public IUpdateField ShieldBlockCritPercentage { get; }
        public IUpdateField Mastery { get; }
        public IUpdateField ExploredZones { get; }
        public IUpdateField RestStateExperience { get; }
        public IUpdateField Coinage { get; }
        public IUpdateField[] ModDamageDonePos { get; }
        public IUpdateField[] ModDamageDoneNeg { get; }
        public IUpdateField[] ModDamageDonePct { get; }
        public IUpdateField ModHealingDonePos { get; }
        public IUpdateField ModHealingPct { get; }
        public IUpdateField ModHealingDonePct { get; }
        public IUpdateField[] WeaponDamageMultipliers { get; }
        public IUpdateField ModSpellPowerPCT { get; }
        public IUpdateField OverrideSpellPowerByApPct { get; }
        public IUpdateField ModTargetResistance { get; }
        public IUpdateField ModTargetPhysicalResistance { get; }
        public IUpdateField FIELD_BYTES { get; }
        public IUpdateField SelfResurrectionSpell { get; }
        public IUpdateField PvPMedals { get; }
        public IUpdateField[] BuyBackPrices { get; }
        public IUpdateField[] BuyBackTimestamp { get; }
        public IUpdateField Kills { get; }
        public IUpdateField LifetimeHonorableKills { get; }
        public IUpdateField Bytes3 { get; }
        public IUpdateField WatchedFactionIndex { get; }
        public IUpdateField[] CombatRatings { get; }
        public IUpdateField[] ArenaTeamInfos { get; }
        public IUpdateField BattlegroundRating { get; }
        public IUpdateField MaxLevel { get; }
        public IUpdateField[] DailyQuests { get; }
        public IUpdateField[] RuneRegen { get; }
        public IUpdateField[] NoReagentCost { get; }
        public IUpdateField[] GlyphSlots { get; }
        public IUpdateField[] Glyphs { get; }
        public IUpdateField GlyphsEnabled { get; }
        public IUpdateField PetSpellPower { get; }
        public IUpdateField Researching { get; }
        public IUpdateField ResearchSites { get; }
        public IUpdateField[] ProfessionSkillLines { get; }
        public IUpdateField UiHitModifier { get; }
        public IUpdateField UiSpellHitModifier { get; }
        public IUpdateField HomeRealmTimeOffset { get; }
        public IUpdateField ModHaste { get; }
        public IUpdateField ModRangedHaste { get; }
        public IUpdateField ModPetHaste { get; }
        public IUpdateField ModHasteRegen { get; }
    }
}