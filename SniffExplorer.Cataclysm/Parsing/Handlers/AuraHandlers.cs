using System;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Extensions;
using SniffExplorer.Parsing.Reactive;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.Parsing.Handlers
{
    public sealed class AuraHandlers
    {
        [Flags]
        public enum AuraFlags
        {
            EffectIndex0 = 0x0001,
            EffectIndex1 = 0x0002,
            EffectIndex2 = 0x0004,
            NotCaster = 0x0008,
            Positive = 0x0010,
            Duration = 0x0020,
            Scalable = 0x0040,
            Negative = 0x0080,
            Unk100 = 0x0100,
            Unk400 = 0x0400,
            Unk1000 = 0x1000,
            Unk4000 = 0x4000
        }

        [Attributes.Parser(PacketDirection.ServerToClient, Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(ParsingContext context, Packet packet)
        {
            var guid = packet.ReadPackedGUID();
            
            // TODO: non-leaky? There's an IDisposable hanging here...
            var entity = context.ObjectManager[guid];
            if (entity is not IUnit unit)
                throw new InvalidOperationException($"Received aura aupdates for a {guid.Type}.");

            while (packet.CanRead())
                ReadAuraUpdateBlock(context, packet, unit);
        }

        private static void ReadAuraUpdateBlock(ParsingContext context, Packet packet, IUnit entity)
        {
            var slot = packet.ReadUInt8();
            var spellID = packet.ReadUInt32();

            var auraInfo = entity.Auras[slot];
            var auraInfoSnapshot = auraInfo[packet.Moment];

            auraInfoSnapshot.ID = spellID;

            if (spellID != 0)
            {
                var auraFlags = (AuraFlags) (context.ClientBuild.Predates(ClientBuild.V4_2_0_14333)
                    ? packet.ReadUInt8()
                    : packet.ReadUInt16());

                auraInfoSnapshot.IsPositive = auraFlags.HasFlag(AuraFlags.Positive);
                auraInfoSnapshot.IsScalable = auraFlags.HasFlag(AuraFlags.Scalable);
                auraInfoSnapshot.Level = packet.ReadUInt8();
                auraInfoSnapshot.Charges = packet.ReadUInt8();

                if (!auraFlags.HasFlag(AuraFlags.NotCaster))
                    auraInfoSnapshot.CasterGuid = packet.ReadPackedGUID();

                if (auraFlags.HasFlag(AuraFlags.Duration))
                {
                    auraInfoSnapshot.MaxDuration = packet.ReadUInt32();
                    auraInfoSnapshot.Duration = packet.ReadUInt32();
                }

                if (auraFlags.HasFlag(AuraFlags.Scalable))
                {
                    if (auraFlags.HasFlag(AuraFlags.EffectIndex0))
                        auraInfoSnapshot.Values[0] = packet.ReadInt32();

                    if (auraFlags.HasFlag(AuraFlags.EffectIndex1))
                        auraInfoSnapshot.Values[1] = packet.ReadInt32();

                    if (auraFlags.HasFlag(AuraFlags.EffectIndex2))
                        auraInfoSnapshot.Values[2] = packet.ReadInt32();
                }
            }
        }
    }
}
