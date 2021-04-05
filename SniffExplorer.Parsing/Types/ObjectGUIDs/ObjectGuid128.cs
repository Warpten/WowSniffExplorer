using System;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Engine;

namespace SniffExplorer.Parsing.Types.ObjectGUIDs
{
    public sealed class ObjectGuid128 : IObjectGUID
    {
        private ObjectGuidStream _stream;

        public uint? Entry    { get; }
        public uint? ServerID { get; }
        public uint? RealmID  { get; }
        public uint? MapID    { get; }
        public uint Low      { get; }

        public ObjectGuidType Type { get; }

        private readonly ulong[] _parts;
        public Span<ulong> Parts => _parts;

        internal ObjectGuid128(ulong lowPart, ulong highPart, ParsingContext parseContext)
        {
            _parts = new[] {lowPart, highPart};

            Type = parseContext.Helper.GuidResolver.ResolveObjectGuidType(this, parseContext);

            Entry = (uint) ((highPart >> 6) & 0x7FFFFF);
            ServerID = (uint)((lowPart >> 40) & 0xFFFFFF);
            RealmID = (uint)((highPart >> 42) & 0x1FFF);
            MapID = (uint)((highPart >> 29) & 0x1FFF);
            Low = (uint)(lowPart & 0xFFFFFFFFFF);

            _stream = new ObjectGuidStream(this, 16, index =>
            {
                var byteSpan = MemoryMarshal.Cast<ulong, byte>(Parts);
                return ref byteSpan[index];
            });
        }

        public override string ToString()
            => $"0x{_parts[0]:X16}{Parts[1]:X16}";

        public ref ObjectGuidStream AsBitStream() => ref _stream;

        public void FromPacket(Packet packet, bool packed)
        {
            _parts[0] = packed ? packet.ReadPackedUInt64() : packet.ReadUInt64();
            _parts[1] = packed ? packet.ReadPackedUInt64() : packet.ReadUInt64();
        }

        private bool Equals(ObjectGuid128 other)
        {
            return other._parts[0] == _parts[0] && other._parts[1] == _parts[1];
        }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj) || obj is ObjectGuid128 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _parts.GetHashCode();
        }

        public bool Equals(IObjectGUID? other)
        {
            if (other is ObjectGuid128 otherGUID)
                return Equals(other);

            return false;
        }
    }
}