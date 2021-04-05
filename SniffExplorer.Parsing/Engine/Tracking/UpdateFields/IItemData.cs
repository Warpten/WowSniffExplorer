namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IItemData : IUpdateFieldStorage
    {
        public IUpdateField Owner { get; }
        public IUpdateField Contained { get; }
        public IUpdateField Creator { get; }
        public IUpdateField GiftCreator { get; }
        public IUpdateField StackCount { get; }
        public IUpdateField Duration { get; }
        public IUpdateField[] SpellCharges { get; }
        public IUpdateField Flags { get; }
        public IUpdateField[] Enchantments { get; }
        public IUpdateField PropertySeed { get; }
        public IUpdateField RandomPropertiesID { get; }
        public IUpdateField Durability { get; }
        public IUpdateField MaxDurability { get; }
        public IUpdateField CreatePlayedTime { get; }
    }
}