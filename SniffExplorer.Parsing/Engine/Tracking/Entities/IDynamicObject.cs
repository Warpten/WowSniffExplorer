using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public interface IDynamicObject : IObject
    {
        public IDynamicObjectData DynamicObjectData { get; }
    }
}