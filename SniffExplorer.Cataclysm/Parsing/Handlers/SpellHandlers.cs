using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

using static SniffExplorer.Parsing.Engine.SpellHistory.Entry;

namespace SniffExplorer.Cataclysm.Parsing.Handlers
{
    public static class SpellHandlers
    {
        [Flags] private enum TargetFlags : uint
        {
            Unit                = 0x00000002,
            Minipet             = 0x00010000,
            UnitEnemy           = 0x00000080,
            UnitAlly            = 0x00000100,
            GameObject          = 0x00000800,
            CorpseEnemy         = 0x00000200,
            CorpseAlly          = 0x00008000,
            Item                = 0x00000010,
            TradeItem           = 0x00001000,
            SourceLocation      = 0x00000020,
            DestinationLocation = 0x00000040,
            String              = 0x00002000,
            ExtraTargets        = 0x00080000,
        }

        [Flags] enum CastFlags : uint
        {  
            Ammo         = 0x00000020,
            DestLocation = 0x00000040,
            Power        = 0x00000800,
            Missile      = 0x00020000,
            VisualChain  = 0x00080000,
            Runes        = 0x00200000,
            Immunities   = 0x04000000,
            Prediction   = 0x40000000,
        }

        [Flags] enum CastFlagsEx : uint
        {
            NO_HISTORY = 0x1
        }

        [Attributes.Parser(PacketDirection.ServerToClient, Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(ParsingContext context, Packet packet)
        {
            var casterGUID = packet.ReadPackedGUID();
            var unitCasterGUID = packet.ReadPackedGUID();
            var castGUID = new CastGUID(packet.ReadUInt8());
            var spellID = packet.ReadUInt32();
            var castFlags = packet.ReadUInt32();
            var castFlagsEx = packet.ReadUInt32();
            var castTime = packet.ReadUInt32();

            if (spellID != 836) return; //! DEBUG
            
            // TODO: There's a lot more to read here, but this is early prototyping.
            var subscription = context.SpellHistory.Register(castGUID, casterGUID, unitCasterGUID, spellID).Subscribe(historyEntry =>
            {
                historyEntry.SpellStart = packet.Moment;
            });

            context.RegisterResource(subscription);
        }
        
        [Attributes.Parser(PacketDirection.ServerToClient, Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(ParsingContext context, Packet packet)
        {
            var casterGUID = packet.ReadPackedGUID();
            var unitCasterGUID = packet.ReadPackedGUID();
            var castGUID = new CastGUID(packet.ReadUInt8());
            var spellID = packet.ReadUInt32();
            var castFlags = (CastFlags) packet.ReadUInt32();
            var castFlagsEx = (CastFlagsEx) packet.ReadUInt32();
            var castTime = packet.ReadUInt32();

            if (spellID != 836) return; //! DEBUG
            
            var hitTargets = new IObjectGUID[packet.ReadUInt8()];
            for (var i = 0; i < hitTargets.Length; ++i)
                hitTargets[i] = packet.ReadGUID();
            
            var missTargets = new MissInfo[packet.ReadUInt8()];
            for (var i = 0; i < missTargets.Length; ++i)
            {
                var missTarget = packet.ReadGUID();
                var missType = packet.ReadUInt8();

                if (missType == 11) // Reflect
                    missTargets[i] = new MissInfo(missTarget, missType, packet.ReadUInt8());
                else
                    missTargets[i] = new MissInfo(missTarget, missType);
            }

            var targetMask = (TargetFlags) packet.ReadUInt32();

            IObjectGUID? explicitTarget = default;
            if (targetMask.HasFlag(TargetFlags.Unit | TargetFlags.CorpseAlly | TargetFlags.CorpseEnemy | TargetFlags.GameObject | TargetFlags.Minipet))
                explicitTarget = packet.ReadGUID();
            else if (targetMask.HasFlag(TargetFlags.Item | TargetFlags.TradeItem))
                explicitTarget = packet.ReadGUID();

            #region documentation
            if (targetMask.HasFlag(TargetFlags.SourceLocation))
            {
                var transportGUID = packet.ReadGUID();
                var sourceLocation = packet.ReadVector3();
            }

            if (targetMask.HasFlag(TargetFlags.DestinationLocation))
            {
                var transportGUID = packet.ReadGUID();
                var destinationLocation = packet.ReadVector3();
            }

            if (targetMask.HasFlag(TargetFlags.String))
            {
                var @string = packet.ReadCString(128);
            }

            if (targetMask.HasFlag(TargetFlags.ExtraTargets))
            {
                var extraTargetCount = packet.ReadInt32();
                for (var i = 0; i < extraTargetCount; ++i)
                {
                    var i1 = packet.ReadUInt32();
                    var i2 = packet.ReadUInt32();
                    var i3 = packet.ReadUInt32();
                    var guid = packet.ReadGUID();
                }
            }

            if (castFlags.HasFlag(CastFlags.Power))
            {
                var power = packet.ReadUInt32();
            }

            if (castFlags.HasFlag(CastFlags.Runes))
            {
                var runeStateBefore = packet.ReadUInt8();
                var runeStateAfter = packet.ReadUInt8();

                var runeStates = new byte[6];
                for (var i = 0; i < runeStates.Length; ++i)
                    runeStates[i] = packet.ReadUInt8();
            }

            if (castFlags.HasFlag(CastFlags.Missile))
            {
                var elevation = packet.ReadSingle();
                var delay = packet.ReadUInt32();
            }

            if (castFlags.HasFlag(CastFlags.Ammo))
            {
                var displayID = packet.ReadUInt32();
                var inventoryType = packet.ReadUInt32();
            }

            if (castFlags.HasFlag(CastFlags.VisualChain))
            {
                var i0 = packet.ReadUInt32();
                var i1 = packet.ReadUInt32();
            }

            if (castFlags.HasFlag(CastFlags.DestLocation))
            {
                var destLocationCastIndex = packet.ReadUInt8();
            }
            #endregion
    
            // Delayed subscription because we need to wait for SMSG_SPELL_START to be processed.
            var subscription = context.SpellHistory[castGUID].Subscribe(historyEntry =>
            {
                Debug.Assert(historyEntry.SpellID == spellID, "Spell ID mismatch!");
                Debug.Assert(historyEntry.Caster == casterGUID, "Caster GUID mismatch");
                Debug.Assert(historyEntry.UnitCaster == unitCasterGUID, "Unit caster GUID mismatch!");

                historyEntry.HitTargets = hitTargets;
                historyEntry.MissedTargets = missTargets;

                historyEntry.ExplicitTarget = explicitTarget;

                historyEntry.SpellGo = packet.Moment;
            });

            context.RegisterResource(subscription);
        }
    }
    
    // A dud implementation of a Spell cast GUID to be used for state tracking of spells.
    class CastGUID : IObjectGUID
    {
        public bool Equals(IObjectGUID? other)
        {
            if (other is not CastGUID otherCastGUID)
                return false;

            return Low == otherCastGUID.Low;
        }

        public override int GetHashCode()
        {
            return (int) (Low & 0xFFFFFFFFuL);
        }

        public uint? Entry { get; } = null;
        public uint? ServerID { get; } = null;
        public uint? RealmID { get; } = null;
        public uint? MapID { get; } = null;
        public uint Low { get; }

        public CastGUID(uint castID) => Low = castID;

        public ObjectGuidType Type { get; } = ObjectGuidType.Cast;
        public Span<ulong> Parts => throw new NotImplementedException();

        public ref ObjectGuidStream AsBitStream() => throw new NotImplementedException();
        public void FromPacket(Packet packet, bool packed) => throw new NotImplementedException();
    }
}
