using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;
using SniffExplorer.Shared.Extensions;

namespace SniffExplorer.Cataclysm.Parsing.Handlers
{
    struct CharacterInfo
    {
        public IObjectGUID GUID { get; set; }
        public IObjectGUID GuildGUID { get; set; }
        public string Name { get; set; }
    }

    public class CharacterHandler
    {
        [Attributes.Parser(PacketDirection.ServerToClient, Opcode.SMSG_ENUM_CHARACTERS_RESULT)]
        public static void HandleCharacterEnum(ParsingContext context, Packet packet)
        {
            var restrictedFactionChangesCount = packet.ReadBits(23);
            var success = packet.ReadBit();
            var characterCount = packet.ReadBits(17);

            var characters = new CharacterInfo[characterCount];
            var characterNames = new uint[characterCount];

            for (var i = 0; i < characterCount; ++i)
            {
                var characterInfo = new CharacterInfo();

                characterInfo.GUID = context.Helper.GuidResolver.CreateGUID();
                characterInfo.GuildGUID = context.Helper.GuidResolver.CreateGUID();
                characterInfo.GUID.AsBitStream().Initialize(packet, 3);
                characterInfo.GuildGUID.AsBitStream().Initialize(packet, 1, 7, 2);

                characterNames[i] = packet.ReadBits(7);

                characterInfo.GUID.AsBitStream().Initialize(packet, 4, 7);
                characterInfo.GuildGUID.AsBitStream().Initialize(packet, 3);
                characterInfo.GUID.AsBitStream().Initialize(packet, 5);
                characterInfo.GuildGUID.AsBitStream().Initialize(packet, 6);
                characterInfo.GUID.AsBitStream().Initialize(packet, 1);
                characterInfo.GuildGUID.AsBitStream().Initialize(packet, 5, 4);

                packet.ReadBit(); // First login

                characterInfo.GUID.AsBitStream().Initialize(packet, 0, 2, 6);
                characterInfo.GuildGUID.AsBitStream().Initialize(packet, 0);

                characters[i] = characterInfo;
            }

            packet.ResetBitReader();

            for (var i = 0; i < characterCount; ++i)
            {
                ref var characterInfo = ref characters[i];

                packet.Skip<byte>();

                for (var j = 0; j < 23; ++j)
                {
                    var inventoryType = packet.ReadUInt8();
                    var displayID = packet.ReadUInt32();
                    var enchantmentDisplayID = packet.ReadUInt32();
                }

                packet.Skip<uint>(); // Pet Family
                characterInfo.GuildGUID.AsBitStream().Parse(packet, 2);
                packet.Skip<byte>(2); // List Order + Hair Style
                characterInfo.GuildGUID.AsBitStream().Parse(packet, 3);
                packet.Skip<uint>(2); // Pet Display ID + Character Flag
                packet.Skip<byte>(); // Hair Color
                characterInfo.GUID.AsBitStream().Parse(packet, 4);
                packet.Skip<uint>(); // Map ID
                characterInfo.GuildGUID.AsBitStream().Parse(packet, 5);
                packet.Skip<float>(); // Position.Z
                characterInfo.GuildGUID.AsBitStream().Parse(packet, 6);
                packet.Skip<uint>(); // Pet Level
                characterInfo.GUID.AsBitStream().Parse(packet, 3);
                packet.Skip<float>(); // Position Y
                packet.Skip<uint>(); // Customization Flags
                packet.Skip<byte>(); // Facial Hair
                characterInfo.GUID.AsBitStream().Parse(packet, 7);
                packet.Skip<byte>(); // Gender
                characterInfo.Name = packet.ReadWoWString(characterNames[i]);
                packet.Skip<byte>(); // Face
                characterInfo.GUID.AsBitStream().Parse(packet, 0, 2);
                characterInfo.GuildGUID.AsBitStream().Parse(packet, 1, 7);
                packet.Skip<float>(); // Position X
                packet.Skip<byte>(3); // Skin + Race + Level
                characterInfo.GUID.AsBitStream().Parse(packet, 6);
                characterInfo.GuildGUID.AsBitStream().Parse(packet, 4, 0);
                characterInfo.GUID.AsBitStream().Parse(packet, 5, 1);
                packet.Skip<uint>(); // Zone ID

                context.NameCache.Register(characterInfo.GUID, characterInfo.Name);
            }
        }
    }
}
