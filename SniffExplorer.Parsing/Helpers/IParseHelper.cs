using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Helpers.GUIDs;
using SniffExplorer.Parsing.Helpers.Handlers;
using SniffExplorer.Parsing.Helpers.Opcodes;
using SniffExplorer.Parsing.Helpers.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Parsing.Helpers
{
    /// <summary>
    /// Basice interface implemented at the root of each module supplying
    /// 1. Handlers to parse packets.
    /// 2. Various helpers to parse complex types.
    /// </summary>
    public interface IParseHelper
    {
        /// <summary>
        /// A reference to the owning context.
        /// </summary>
        public ParsingContext Context { get; }

        /// <summary>
        /// The client build of the sniff being parsed.
        /// </summary>
        public ClientBuild ClientBuild { get; }

        /// <summary>
        /// Provides utility methods to process a <see cref="Packet"/>.
        /// </summary>
        public HandlerHelper Handlers { get; }

        /// <summary>
        /// Provides utility methods to identify an opcode from its <see cref="PacketDirection"/>,
        /// the <see cref="Versions.ClientBuild"/>, and its value as seen on the wire.
        /// </summary>
        public IOpcodeResolver OpcodeResolver { get; }

        /// <summary>
        /// Provides utility methods to generate appropriate GUID types.
        /// </summary>
        public GuidResolver GuidResolver { get; }

        /// <summary>
        /// Provides utility methods to generate appropriate descriptor storage.
        /// </summary>
        public UpdateFieldProvider UpdateFieldProvider { get; }

        /// <summary>
        /// Returns an instance of an entity from a GUID.
        /// </summary>
        /// <param name="objectGUID"></param>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public IEntity CreateEntity(IObjectGUID objectGUID, EntityTypeID typeID);

        public EntityTypeID ResolveTypeID(byte value);
    }
}