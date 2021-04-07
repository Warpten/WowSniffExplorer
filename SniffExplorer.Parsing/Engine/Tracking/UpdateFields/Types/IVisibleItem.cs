namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types
{
    public interface IVisibleItem
    {
        public uint ID { get; }
        public ushort PermanentEnchantment { get; }
        public ushort TemporaryEnchantment { get; }
    }
}
