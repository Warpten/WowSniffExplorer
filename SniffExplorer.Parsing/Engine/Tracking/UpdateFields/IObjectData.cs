using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IObjectData : IUpdateFieldStorage
    {
        public IUpdateField GUID { get; }
        public IUpdateField Data { get; }
        public IUpdateField<ObjectType> Type { get; }
        public IUpdateField<uint> Entry { get; }
        public IUpdateField Scale { get; }
        public IUpdateField Padding { get; }
    }
}
