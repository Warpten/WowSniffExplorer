using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public interface IGameObject : IObject
    {
        public IGameObjectData GameObjectData { get; }
    }
}