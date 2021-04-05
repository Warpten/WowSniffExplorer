using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public interface IObject : IEntity
    {
        public IObjectData ObjectData { get; }
    }
}