using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public interface ICorpse : IObject
    {
        public ICorpseData CorpseData { get; }
    }
}