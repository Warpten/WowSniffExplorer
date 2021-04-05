using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SniffExplorer.Parsing.Loading.Attributes;
using SniffExplorer.Parsing.Loading.Enums;

namespace SniffExplorer.Parsing.Extensions
{
    public static class StringExtensions
    {
        public static FileCompression ToFileCompressionEnum(this string str)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var item in (FileCompression[]) Enum.GetValues(typeof(FileCompression)))
            {
                var member = item.GetType().GetField(item.ToString());
                if (member == null)
                    continue;

                var attributes = member.GetCustomAttributes<FileCompressionAttribute>().ToList();
                if (attributes.Count > 0 && attributes.Any(attr => attr.Extension.Equals(str.ToLower())))
                    return item!;
            }

            return FileCompression.None;
        }
    }
}
