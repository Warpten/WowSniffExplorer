using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SniffExplorer.Parsing.IO;

namespace SniffExplorer.Parsing.Extensions
{
    public static class StreamExtensions
    {
        public static Stream EnsureSeekable(this Stream stream)
        {
            if (stream.CanSeek)
                return stream;

            var ms = new MemoryStream();
            stream.CopyTo(ms);
            stream.Dispose();
            return ms;
        }
    }
}
