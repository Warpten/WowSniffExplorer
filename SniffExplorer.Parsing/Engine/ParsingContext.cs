using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using SniffExplorer.Parsing.Engine.Events;
using SniffExplorer.Parsing.Engine.Tracking;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Helpers;
using SniffExplorer.Parsing.IO;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Parsing.Engine
{
    public readonly struct ParsingOptions
    {
        /// <summary>
        /// When this option is set to <see cref="bool">true</see>, descriptors (also know as update fields)
        /// will not keep an history of their value. Only the last value seen in sniffs will be kept.
        ///
        /// You should use this in situations where you need to parse a sniff only for movements, or spells.
        /// </summary>
        public bool DiscardUpdateFields { get; init; }
    }

    /// <summary>
    /// General purpose class in charge of tracking the current evaluation context of a sniff.
    /// </summary>
    public class ParsingContext
    {
        public ParsingOptions Options { get; }

        /// <summary>
        /// The build in which the sniff was captured.
        /// </summary>
        public ClientBuild ClientBuild { get; }

        public ObjectManager ObjectManager { get; }
        public IParseHelper Helper { get; }

        public NameCache NameCache { get; }
        
        internal ParsingContext(ClientBuild clientBuild, Type parseHelperType)
        {
            ClientBuild = clientBuild;
            Helper = (IParseHelper) Activator.CreateInstance(parseHelperType, this)!;

            ObjectManager = new ObjectManager(Helper);

            NameCache = new NameCache();
        }

        internal Packet CreatePacket(byte[] dataStream, DateTime moment, Opcode opcode, uint connectionIndex, PacketDirection direction)
            => new(dataStream, moment, opcode, direction, this);
    }
}
