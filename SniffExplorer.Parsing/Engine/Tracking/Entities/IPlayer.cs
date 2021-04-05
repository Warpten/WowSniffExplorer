using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public interface IPlayer : IUnit
    {
        public IPlayerData PlayerData { get; }
    }
}