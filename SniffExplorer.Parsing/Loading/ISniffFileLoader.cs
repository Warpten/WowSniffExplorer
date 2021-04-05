using System.Collections.Generic;
using System.IO;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Versions;

namespace SniffExplorer.Parsing.Loading
{
    public interface ISniffFileLoader
    {
        public ClientBuild ClientBuild { get; }

        public int Count { get; }
        
        public IEnumerable<Packet> Parse(Stream dataStream, ParsingContext context);
    }
}