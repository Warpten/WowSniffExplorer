using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public interface IItem : IObject
    {
        public IItemData ItemData { get; }
    }
}