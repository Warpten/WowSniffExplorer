using NUnit.Framework;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Loading;

namespace SniffExplorer.Tests
{
    public class ParsingTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            using var sniffFile = new SniffFile("./sniff.pkt");
            var context = Parser.Of(sniffFile).Run();
        }
    }
}