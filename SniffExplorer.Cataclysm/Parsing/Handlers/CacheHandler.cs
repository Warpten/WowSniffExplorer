using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.Parsing.Handlers
{
    public class CacheHandler
    {
        [Attributes.Parser(PacketDirection.ServerToClient, Opcode.SMSG_NAME_QUERY_RESPONSE)]
        public static void HandleNameQueryResponse(ParsingContext context, Packet packet)
        {
            var guid = packet.ReadPackedGUID();
            var resultCode = packet.ReadUInt8();
            switch (resultCode)
            {
                case 3: // Temporary add
                    break;
                case 2: // Retry item?
                    break;
                case 1:// Non-existing
                    break;
                case 0:
                {
                    var name = packet.ReadCString(0x30);
                    var realmName = packet.ReadCString(0x100);
                    
                    // Not needed, left for clarity (or if someday we run in headless mode)
                    // var race = packet.ReadUInt8();
                    // var gender = packet.ReadUInt8();
                    // var @class = packet.ReadUInt8();
                    // var hasDeclinedNames = packet.ReadUInt8();
                    // for (var i = 0; i < 4; ++i)
                    // {
                    //     var declinedName = packet.ReadCString(0x40);
                    // }

                    if (!string.IsNullOrEmpty(realmName))
                        name = $"{name}-{realmName}";

                    context.NameCache.Register(guid, name);

                    break;
                }
            }
        }

        [Attributes.Parser(PacketDirection.ServerToClient, Opcode.SMSG_CREATURE_QUERY_RESPONSE)]
        public static void HandleCreatureNameQueryResponse(ParsingContext context, Packet packet)
        {
            var entry = packet.ReadUInt32();
            if ((entry & 0x80000000u) != 0)
                return;

            entry &= ~0x80000000u;
            
            context.NameCache.Register(ObjectGuidType.Creature, entry, packet.ReadCString());
        }
    }
}
