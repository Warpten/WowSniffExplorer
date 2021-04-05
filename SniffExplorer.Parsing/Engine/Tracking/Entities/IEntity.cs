using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.Enums;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public interface IEntity
    {
        public IObjectGUID Guid { get; }
        public IHistory<MovementInfo> MovementInfo { get; }

        /// <summary>
        /// Object type as seen in JamCliObjCreate.
        /// </summary>
        public EntityTypeID TypeID { get; }

        public bool IsSelf { get; set; }

        public void ProcessValuesUpdate(Packet packet, UpdateMask updateMask);
    }

    public interface IObject : IEntity
    {
        public IObjectData ObjectData { get; }
    }

    public interface IUnit : IObject
    {
        public IUnitData UnitData { get; }

        public AuraStore Auras { get; }

        public ClassMask Class { get; }
        public RaceMask Race { get; }
    }

    public interface IPlayer : IUnit
    {
        public IPlayerData PlayerData { get; }
    }

    public interface IItem : IObject
    {
        public IItemData ItemData { get; }
    }

    public interface ICorpse : IObject
    {
        public ICorpseData CorpseData { get; }
    }

    public interface IContainer : IItem
    {
        public IContainerData ContainerData { get; }
    }

    public interface IDynamicObject : IObject
    {
        public IDynamicObjectData DynamicObjectData { get; }
    }

    public interface IGameObject : IObject
    {
        public IGameObjectData GameObjectData { get; }
    }

    public interface IAreaTrigger : IObject
    {
        public IAreaTriggerData AreaTriggerData { get; }
    }
}
