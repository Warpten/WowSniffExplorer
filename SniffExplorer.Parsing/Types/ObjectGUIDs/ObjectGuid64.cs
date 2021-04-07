using System;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Versions;

namespace SniffExplorer.Parsing.Types.ObjectGUIDs
{
    public sealed class ObjectGuid64 : IObjectGUID
    {
        private ObjectGuidStream _stream;

        public uint? Entry
        {
            get
            {
                switch (Type)
                {
                    case ObjectGuidType.Creature:
                    case ObjectGuidType.GameObject:
                    case ObjectGuidType.Pet:
                    case ObjectGuidType.Vehicle:
                    case ObjectGuidType.AreaTrigger:
                        if (_parsingContext.ClientBuild >= ClientBuild.V4_0_1_13164)
                            return (uint)((Parts[0] & 0x000FFFFF00000000) >> 32);
                        return (uint)((Parts[0] & 0x000FFFFFFF000000) >> 24);
                    default:
                        return null;
                }
            }
        }

        public uint? ServerID { get; } = null;
        public uint? RealmID  { get; } = null;
        public uint? MapID    { get; } = null;

        public uint Low 
        {
            get
            {
                switch (Type)
                {
                    case ObjectGuidType.Player:
                    case ObjectGuidType.DynamicObject:
                    case ObjectGuidType.RaidGroup:
                    case ObjectGuidType.Item:
                        return (uint)(Parts[0] & 0x000FFFFFFFFFFFFFuL);
                    case ObjectGuidType.GameObject:
                    case ObjectGuidType.Transport:
                    case ObjectGuidType.Vehicle:
                    case ObjectGuidType.Creature:
                    case ObjectGuidType.Pet:
                        if (_parsingContext.ClientBuild > ClientBuild.V4_0_1_13164)
                            return (uint)(Parts[0] & 0x00000000FFFFFFFFul);
                        return (uint) (Parts[0] & 0x0000000000FFFFFFul);
                }

                return (uint) (Parts[0] & 0x00000000FFFFFFFFul);
            }
        }

        public ObjectGuidType Type => _parsingContext.Helper.GuidResolver.ResolveObjectGuidType(this, _parsingContext);

        private ulong _value;
        public Span<ulong> Parts => MemoryMarshal.CreateSpan(ref _value, 1);

        private readonly ParsingContext _parsingContext;

        internal ObjectGuid64(ulong value, ParsingContext parseContext)
        {
            _value = value;

            _parsingContext = parseContext;

            _stream = new ObjectGuidStream(this, 8, index =>
            {
                var byteSpan = MemoryMarshal.Cast<ulong, byte>(Parts);
                return ref byteSpan[index];
            });
        }

        public override string ToString()
            => $"0x{_value:X16}";

        public ref ObjectGuidStream AsBitStream() => ref _stream;

        public void FromPacket(Packet packet, bool packed)
            => _value = packed ? packet.ReadPackedUInt64() : packet.ReadUInt64();


        private bool Equals(ObjectGuid64 other)
        {
            return other._value == _value;
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is ObjectGuid128 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public bool Equals(IObjectGUID? other)
        {
            if (other is ObjectGuid64 otherGUID)
                return Equals(otherGUID);

            return false;
        }
    }
}