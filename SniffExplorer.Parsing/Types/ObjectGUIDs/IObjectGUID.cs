using System;

namespace SniffExplorer.Parsing.Types.ObjectGUIDs
{
    public interface IObjectGUID : IEquatable<IObjectGUID>
    {
        public uint? Entry { get; }
        public uint? ServerID { get; }
        public uint? RealmID { get; }
        public uint? MapID { get; }
        public uint Low { get; }

        public ObjectGuidType Type { get; }

        public Span<ulong> Parts { get; }

        /// <summary>
        /// Obtains an object representing the current GUID as a bit stream, as seen during Cataclysm.
        /// </summary>
        /// <returns></returns>
        public ref ObjectGuidStream AsBitStream();

        public void FromPacket(Packet packet, bool packed);
    }
}
