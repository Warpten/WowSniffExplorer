using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public interface IContainer : IItem
    {
        public IContainerData ContainerData { get; }
    }
}