namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface ICorpseData : IUpdateFieldStorage
    {
        public IUpdateField Owner { get; }
        public IUpdateField Party { get; }
        public IUpdateField DisplayId { get; }
        public IUpdateField[] Items { get; }
        public IUpdateField Bytes1 { get; }
        public IUpdateField Bytes2 { get; }
        public IUpdateField Flags { get; }
        public IUpdateField DynamicFlags { get; }
    }
}