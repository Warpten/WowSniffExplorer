using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SniffExplorer.Cataclysm.Attributes;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.Parsing.Handlers
{
    public static class MiscHandlers
    {
        [Parser(PacketDirection.ClientToServer, Opcode.SMSG_FORCE_ANIM)]
        public static void HandleForceAnimation(ParsingContext context, Packet packet) // It's still unknown until confirmed.
        {
            var target = packet.ReadGUID();
            var argumets = packet.ReadCString(0x200);
        }
    }
}
