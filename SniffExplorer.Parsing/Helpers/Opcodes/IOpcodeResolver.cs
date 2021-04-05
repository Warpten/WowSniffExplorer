using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Parsing.Helpers.Opcodes
{
    /// <summary>
    /// This interface provides methods in charge of resolving an <see cref="Opcode"/> from a <see cref="ClientBuild"/> and an <see cref="uint">opcode value</see>.
    /// </summary>
    public interface IOpcodeResolver
    {
        public void Initialize();

        /// <summary>
        /// Identifies an opcode from its value, the client build, and the direction in which it was sniffed.
        /// </summary>
        /// <param name="build">The build in which the packet was sniffed.</param>
        /// <param name="direction">The direction in which the packet is going.</param>
        /// <param name="value">The value of the opcode, as seen on the wire.</param>
        /// <returns>The opcode identified.</returns>
        public Opcode Resolve(ClientBuild build, PacketDirection direction, uint value);
    }
}
