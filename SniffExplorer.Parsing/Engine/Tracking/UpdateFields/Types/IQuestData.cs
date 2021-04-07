namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types
{
    public interface IQuestData
    {
        public uint ID { get; }
        public int State { get; }
        public ushort[] Counts { get; } // 4
        public int Time { get; }
    }
}
