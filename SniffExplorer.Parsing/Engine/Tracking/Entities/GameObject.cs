using System;
using System.Reactive.Linq;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public class GameObject : Object, IGameObject
    {
        public IGameObjectData GameObjectData { get; }

        public override EntityTypeID TypeID { get; } = EntityTypeID.GameObject;

        public GameObject(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var gameObjectData = context.Helper.UpdateFieldProvider.CreateGameObjectData(guid);

            GameObjectData = gameObjectData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var gameObjectUpdateMask = updateMask.LeftShift(ObjectData.BitCount);
            GameObjectData.ProcessValuesUpdate(packet, gameObjectUpdateMask);
        }
    }
}