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
    /// <summary>
    /// General purpose class in charge of tracking the current evaluation context of a sniff.
    /// </summary>
    public class ParsingContext
    {
        /// <summary>
        /// The build in which the sniff was captured.
        /// </summary>
        public ClientBuild ClientBuild { get; }

        public ObjectManager ObjectManager { get; }
        public IParseHelper Helper { get; }

        public NameCache NameCache { get; }

        private readonly ConcurrentDictionary<SniffEventType, ConcurrentDictionary<DateTime, ConcurrentBag<ISniffEvent>>> _sniffEvents = new();

        internal ParsingContext(ClientBuild clientBuild, Type parseHelperType)
        {
            ClientBuild = clientBuild;
            Helper = (IParseHelper) Activator.CreateInstance(parseHelperType, this)!;

            ObjectManager = new ObjectManager(Helper);

            NameCache = new NameCache();
        }

        internal Packet CreatePacket(byte[] dataStream, DateTime moment, Opcode opcode, uint connectionIndex, PacketDirection direction)
            => new(dataStream, moment, opcode, direction, this);

        public void RegisterEvent(DateTime moment, ISniffEvent sniffEvent)
        {
            var bucket = _sniffEvents.GetOrAdd(sniffEvent.Type, _ => new());
            var subBucket = bucket.GetOrAdd(moment, _ => new());
            subBucket.Add(sniffEvent);
        }
    }
}
