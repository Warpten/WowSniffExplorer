namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IAreaTriggerData : IUpdateFieldStorage
    {
        public IUpdateField SpellID { get; }
        public IUpdateField SpellVisualID { get; }
        public IUpdateField Duration { get; }
        public IUpdateField[] FinalPosition { get; }
    }
}