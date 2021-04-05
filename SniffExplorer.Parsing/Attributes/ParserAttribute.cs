using System;
using System.Collections.Generic;
using System.Text;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Parsing.Attributes
{
    /// <summary>
    /// Denotes a method that is in charge of processing a packet.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ParserAttribute : Attribute
    {
        public abstract PacketDirection Direction { get; }
        public abstract Opcode Opcode{ get; }

        public abstract string Name { get; }
    }
}
