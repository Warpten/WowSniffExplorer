using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public interface IAreaTrigger : IObject
    {
        public IAreaTriggerData AreaTriggerData { get; }
    }
}