using System;
using System.Collections.Generic;
using System.IO;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Extensions;
using SniffExplorer.Parsing.Loading.Enums;
using SniffExplorer.Parsing.Loading.Implementations;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Versions;

namespace SniffExplorer.Parsing.Loading
{
    public sealed class SniffFile : IDisposable
    {
        public readonly string FileName;

        private readonly ISniffFileLoader _fileLoader;
        public ClientBuild ClientBuild => _fileLoader.ClientBuild;

        public int Count => _fileLoader.Count;

        private readonly Stream _dataStream;

        public SniffFile(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            FileName = Path.GetFileName(filePath);

            var compression = extension.ToFileCompressionEnum();
            if (compression != FileCompression.None)
                FileName = FileName.Remove(FileName.Length - extension.Length);

            extension = Path.GetExtension(FileName);

            _dataStream = File.OpenRead(filePath);

            switch (extension)
            {
                case ".pkt":
                    _fileLoader = new PktSniffLoader(_dataStream);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filePath));
            }
        }

        public void Dispose()
        {
            _dataStream.Dispose();
        }

        public IEnumerable<Packet> Enumerate(ParsingContext context)
            => _fileLoader.Parse(_dataStream, context);
    }
}
