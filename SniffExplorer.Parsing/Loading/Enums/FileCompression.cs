using System;
using System.Collections.Generic;
using System.Text;
using SniffExplorer.Parsing.Loading.Attributes;

namespace SniffExplorer.Parsing.Loading.Enums
{
    public enum FileCompression
    {
        None = 0,
        [FileCompression(".gz")]
        GZip = 1
    }
}
