using System;
using System.Linq;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public class Player : Unit, IPlayer
    {
        public IPlayerData PlayerData { get; }

        public override EntityTypeID TypeID => EntityTypeID.Player;

        public uint Level => UnitData.Level.Values.Last();

        public Player(IObjectGUID guid, ParsingContext context) : base(guid, context)
        {
            var playerData = context.Helper.UpdateFieldProvider.CreatePlayerData(guid);

            PlayerData = playerData ?? throw new InvalidOperationException();
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var playerUpdateMask = updateMask.LeftShift(ObjectData.BitCount + UnitData.BitCount);
            PlayerData.ProcessValuesUpdate(packet, playerUpdateMask);
        }
    }
}