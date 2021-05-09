using System;
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

        private class SpellCastData
        {
            public readonly IObjectGUID Caster;
            public readonly IObjectGUID UnitCaster;
            public readonly uint CastID;
            public readonly uint SpellID;
            public readonly uint CastTime;
            public readonly CastFlags CastFlags;
            public readonly CastFlagsEx CastFlagsEx;
            public readonly TargetFlags TargetFlags;

            public readonly IObjectGUID? ExplicitTarget;
            
            public readonly IObjectGUID[]? HitTargets;
            public readonly MissInfo[]? MissedTargets;

            public IObjectGUID CastGUID => new CastGUID(CastID);

            public readonly struct Location
            {
                public readonly IObjectGUID Transport;
                public readonly Vector3 Position;

                public Location(Packet packet)
                {
                    Transport = packet.ReadPackedGUID();
                    Position = packet.ReadVector3();
                }
            }

            public readonly Location? Destination;
            public readonly Location? Source;

            public readonly uint? Power;

            public readonly struct RuneState
            {
                public readonly uint Before;
                public readonly uint After;
                public readonly byte[] States;

                public RuneState(Packet packet)
                {
                    Before = packet.ReadUInt8();
                    After = packet.ReadUInt8();

                    States = new byte[6];
                    for (var i = 0; i < States.Length; ++i)
                        States[i] = packet.ReadUInt8();
                }
            }

            public readonly struct MissileState
            {
                public readonly float Elevation;
                public readonly uint Delay;

                public MissileState(Packet packet)
                {
                    Elevation = packet.ReadSingle();
                    Delay = packet.ReadUInt32();
                }
            }

            public readonly RuneState? Runes;
            public readonly MissileState? MissileInfo;

            public SpellCastData(Packet packet)
            {
                Caster = packet.ReadPackedGUID();
                UnitCaster = packet.ReadPackedGUID();
                CastID = packet.ReadUInt8();
                SpellID = packet.ReadUInt32();
                CastFlags = (CastFlags) packet.ReadUInt32();
                CastFlagsEx = (CastFlagsEx) packet.ReadUInt32();
                CastTime = packet.ReadUInt32();

                if (packet.Opcode == Opcode.SMSG_SPELL_GO)
                {
                    HitTargets = new IObjectGUID[packet.ReadUInt8()];
                    for (var i = 0; i < HitTargets.Length; ++i)
                        HitTargets[i] = packet.ReadGUID();

                    MissedTargets = new MissInfo[packet.ReadUInt8()];
                    for (var i = 0; i < MissedTargets.Length; ++i)
                    {
                        var missTarget = packet.ReadGUID();
                        var missType = packet.ReadUInt8();

                        if (missType == 11) // Reflect
                            MissedTargets[i] = new(missTarget, missType, packet.ReadUInt8());
                        else
                            MissedTargets[i] = new(missTarget, missType);
                    }
                }

                TargetFlags = (TargetFlags) packet.ReadUInt32();
                if (TargetFlags.HasFlag(TargetFlags.Unit | TargetFlags.CorpseAlly | TargetFlags.CorpseEnemy | TargetFlags.GameObject | TargetFlags.Minipet))
                    ExplicitTarget = packet.ReadPackedGUID();
                else if (TargetFlags.HasFlag(TargetFlags.Item | TargetFlags.TradeItem))
                    ExplicitTarget = packet.ReadPackedGUID();
                
                if (TargetFlags.HasFlag(TargetFlags.SourceLocation))
                    Source = new(packet);
                
                if (TargetFlags.HasFlag(TargetFlags.DestinationLocation))
                    Destination = new(packet);
                
                if (TargetFlags.HasFlag(TargetFlags.String))
                {
                    var @string = packet.ReadCString(128);
                }

                if (TargetFlags.HasFlag(TargetFlags.ExtraTargets))
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

                if (CastFlags.HasFlag(CastFlags.Power))
                    Power = packet.ReadUInt32();

                if (CastFlags.HasFlag(CastFlags.Runes))
                    Runes = new(packet);

                if (CastFlags.HasFlag(CastFlags.Missile))
                    MissileInfo = new(packet);

                if (CastFlags.HasFlag(CastFlags.Ammo))
                {
                    var displayID = packet.ReadUInt32();
                    var inventoryType = packet.ReadUInt32();
                }

                if (CastFlags.HasFlag(CastFlags.VisualChain))
                {
                    var i0 = packet.ReadUInt32();
                    var i1 = packet.ReadUInt32();
                }

                if (TargetFlags.HasFlag(TargetFlags.DestinationLocation)) // Wrong mask, cba fixing rn
                {
                    var destLocationCastIndex = packet.ReadUInt8();
                }

                if (CastFlags.HasFlag(CastFlags.Immunities))
                {
                    var mechanicImmunity = packet.ReadUInt32();
                    var immunity = packet.ReadUInt32();
                }

                // More data (prediction)
            }
        }

        [Attributes.Parser(PacketDirection.ServerToClient, Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(ParsingContext context, Packet packet)
        {
            var castData = new SpellCastData(packet);
            
            var subscription = context.SpellHistory.Register(castData.CastGUID, castData.Caster, castData.UnitCaster, castData.SpellID).Subscribe(historyEntry =>
            {
                historyEntry.SpellStart = packet.Moment;
            });

            context.RegisterResource(subscription);
        }
        
        [Attributes.Parser(PacketDirection.ServerToClient, Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(ParsingContext context, Packet packet)
        {
            var spellCastData = new SpellCastData(packet);
    
            // Delayed subscription because we need to wait for SMSG_SPELL_START to be processed.
            var subscription = context.SpellHistory[spellCastData.CastGUID].Subscribe(historyEntry =>
            {
                // Debug.Assert(historyEntry.SpellID == spellCastData.SpellID, "Spell ID mismatch!");
                // Debug.Assert(historyEntry.Caster == spellCastData.Caster, "Caster GUID mismatch");
                // Debug.Assert(historyEntry.UnitCaster == spellCastData.UnitCaster, "Unit caster GUID mismatch!");

                historyEntry.HitTargets = spellCastData.HitTargets;
                historyEntry.MissedTargets = spellCastData.MissedTargets;

                historyEntry.ExplicitTarget = spellCastData.ExplicitTarget;

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
