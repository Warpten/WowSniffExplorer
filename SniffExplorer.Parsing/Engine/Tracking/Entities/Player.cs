using System;
using System.Reactive.Linq;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Entities
{
    public class Player : Unit, IPlayer
    {
        public IPlayerData PlayerData { get; }
        public IActivePlayerData? ActivePlayerData { get; }

        public override EntityTypeID TypeID => EntityTypeID.Player;

        public uint Level { get; set; }

        public Player(IObjectGUID guid, ParsingContext context, bool isSelf) : base(guid, context)
        {
            var playerData = context.Helper.UpdateFieldProvider.CreatePlayerData(guid);

            PlayerData = playerData ?? throw new InvalidOperationException();

            if (isSelf)
            {
                var activePlayerData = context.Helper.UpdateFieldProvider.CreateActivePlayerData(guid);
                ActivePlayerData = activePlayerData ?? throw new InvalidOperationException();
            }

            UnitData.Level.Take(1).Subscribe(tuple => {
                Level = tuple.Value;
            });
        }

        public override void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            base.ProcessValuesUpdate(packet, updateMask);

            var playerUpdateMask = updateMask.LeftShift(ObjectData.BitCount + UnitData.BitCount);
            PlayerData.ProcessValuesUpdate(packet, playerUpdateMask);

            if (ActivePlayerData != null)
            {
                var activePlayerDataMask = playerUpdateMask.LeftShift(PlayerData.BitCount);
                ActivePlayerData!.ProcessValuesUpdate(packet, activePlayerDataMask);
            }
        }
    }
}