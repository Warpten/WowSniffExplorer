using System;

namespace SniffExplorer.Parsing.Engine
{
    public class ParsingStatistics
    {
        public TimeSpan ExecutionTime { get; internal set; }
        public uint PacketCount { get; internal set; }
        public uint ParsedPacketCount { get; internal set; }
    }
}