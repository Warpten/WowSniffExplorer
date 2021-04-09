using System;
using System.Reactive.Linq;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.Enums;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public abstract class Unit : Object, IUnit
    {
        public IUnitData UnitData { get; }

        public AuraStore Auras { get; } = new();

        public RaceMask Race { get; private set; }
        public ClassMask Class { get; private set; }

        public override EntityTypeID TypeID { get; } = EntityTypeID.Creature;

        protected Unit(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var unitData = context.Helper.UpdateFieldProvider.CreateUnitData(guid);

            UnitData = unitData ?? throw new InvalidOperationException();

            UnitData.Bytes0.ValueUpdate.Take(1).Subscribe(tuple => {
                Class = (ClassMask)(1 << (tuple.Value[1] - 1));
                Race = (RaceMask) (1 << (tuple.Value[0] - 1));
            });
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var unitUpdateMask = updateMask.LeftShift(ObjectData.BitCount);
            UnitData.ProcessValuesUpdate(packet, unitUpdateMask);
        }
    }
}