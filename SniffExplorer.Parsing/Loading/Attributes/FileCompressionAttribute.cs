using System;

namespace SniffExplorer.Parsing.Loading.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FileCompressionAttribute : Attribute
    {
        public string Extension { get; }

        public FileCompressionAttribute(string extension)
        {
            Extension = extension;
        }
    }
}
