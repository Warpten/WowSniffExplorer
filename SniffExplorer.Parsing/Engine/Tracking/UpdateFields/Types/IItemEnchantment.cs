namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types
{
    public interface IItemEnchantment
    {
        public uint ID { get; }
        public uint Duration { get; }
        public ushort Inactive { get; }
        public ushort Charges { get; }
    }
}