﻿using System;
using System.Linq;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.Enums;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public abstract class Unit : Object, IUnit
    {
        public IUnitData UnitData { get; }

        public override EntityTypeID TypeID => EntityTypeID.Creature;

        public AuraStore Auras { get; } = new AuraStore();

        public ClassMask Class => (ClassMask)(1 << (UnitData.Bytes0.Values.First()[1] - 1));
        public RaceMask Race => (RaceMask)(1 << (UnitData.Bytes0.Values.First()[0] - 1));

        protected Unit(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var unitData = context.Helper.UpdateFieldProvider.CreateUnitData(guid);

            UnitData = unitData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var unitUpdateMask = updateMask.LeftShift(ObjectData.BitCount);
            UnitData.ProcessValuesUpdate(packet, unitUpdateMask);
        }
    }

    public class Creature : Unit
    {
        public uint Entry => Guid.Entry!.Value;

        public Creature(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
        }
    }

    public class Item : Object, IItem
    {
        public IItemData ItemData { get; }

        public override EntityTypeID TypeID => EntityTypeID.Item;

        public Item(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var itemData = context.Helper.UpdateFieldProvider.CreateItemData(guid);

            ItemData = itemData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var itemUpdateMask = updateMask.LeftShift(ObjectData.BitCount);
            ItemData.ProcessValuesUpdate(packet, itemUpdateMask);
        }
    }

    public class Corpse : Object, ICorpse
    {
        public ICorpseData CorpseData { get; }

        public override EntityTypeID TypeID => EntityTypeID.Item;

        public Corpse(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var corpseData = context.Helper.UpdateFieldProvider.CreateCorpseData(guid);

            CorpseData = corpseData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var corpseUpdateMask = updateMask.LeftShift(ObjectData.BitCount);
            CorpseData.ProcessValuesUpdate(packet, corpseUpdateMask);
        }
    }

    public class Container : Item, IContainer
    {
        public IContainerData ContainerData { get; }

        public override EntityTypeID TypeID => EntityTypeID.Container;

        public Container(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var containerData = context.Helper.UpdateFieldProvider.CreateContainerData(guid);

            ContainerData = containerData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var containerUpdateMask = updateMask.LeftShift(ObjectData.BitCount + ItemData.BitCount);
            ContainerData.ProcessValuesUpdate(packet, containerUpdateMask);
        }
    }

    public class DynamicObject : Object, IDynamicObject
    {
        public IDynamicObjectData DynamicObjectData { get; }

        public override EntityTypeID TypeID => EntityTypeID.DynamicObject;

        public DynamicObject(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var dynamicObjectData = context.Helper.UpdateFieldProvider.CreateDynamicObjectData(guid);

            DynamicObjectData = dynamicObjectData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var dynamicObjectUpdateMask = updateMask.LeftShift(ObjectData.BitCount);
            DynamicObjectData.ProcessValuesUpdate(packet, dynamicObjectUpdateMask);
        }
    }

    public class GameObject : Object, IGameObject
    {
        public IGameObjectData GameObjectData { get; }

        public override EntityTypeID TypeID => EntityTypeID.GameObject;

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

    public class AreaTrigger : Object, IAreaTrigger
    {
        public IAreaTriggerData AreaTriggerData { get; }

        public override EntityTypeID TypeID => EntityTypeID.AreaTrigger;

        public AreaTrigger(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var areaTriggerData = context.Helper.UpdateFieldProvider.CreateAreaTriggerData(guid);

            AreaTriggerData = areaTriggerData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var areaTriggerUpdateMask = updateMask.LeftShift(ObjectData.BitCount);
            AreaTriggerData.ProcessValuesUpdate(packet, areaTriggerUpdateMask);
        }
    }
}