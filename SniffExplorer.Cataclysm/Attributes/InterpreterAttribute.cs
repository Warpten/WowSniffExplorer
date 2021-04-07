using System;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class ParserAttribute : SniffExplorer.Parsing.Attributes.ParserAttribute
    {
        public override PacketDirection Direction { get; }
        public override Opcode Opcode { get; }

        public override string Name { get; }
    
        public ParserAttribute(PacketDirection direction, Opcode opcode)
        {
            Direction = direction;
            Opcode = opcode;

            Name = opcode.ToString();
        }
    }
}
