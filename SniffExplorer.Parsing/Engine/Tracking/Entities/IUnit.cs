using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.Enums;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public interface IUnit : IObject
    {
        public IUnitData UnitData { get; }

        public AuraStore Auras { get; }

        public ClassMask Class { get; }
        public RaceMask Race { get; }
    }
}