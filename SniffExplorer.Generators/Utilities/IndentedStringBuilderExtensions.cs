using System;
using System.Globalization;

namespace SniffExplorer.Generators.Utilities
{
    internal static partial class IndentedStringBuilderExtensions
    {
        public static void AppendLine(this IIndentedStringBuilder builder, IFormatProvider formatProvider, string pattern, params object[] replacements)
        {
            builder.AppendFormat(formatProvider, pattern, replacements);
            builder.AppendLine();
        }

        public static void AppendLine(this IIndentedStringBuilder builder, IFormatProvider formatProvider, int indentLevel, string pattern, params object[] replacements)
        {
            builder.AppendFormat(formatProvider, pattern.Indent(indentLevel), replacements);
            builder.AppendLine();
        }

        public static void AppendLineInvariant(this IIndentedStringBuilder builder, string pattern, params object[] replacements)
        {
            builder.AppendLine(CultureInfo.InvariantCulture, pattern, replacements);
        }

        public static void AppendLineInvariant(this IIndentedStringBuilder builder, int indentLevel, string pattern, params object[] replacements)
        {
            builder.AppendLine(CultureInfo.InvariantCulture, indentLevel, pattern, replacements);
        }

        public static void AppendFormatInvariant(this IIndentedStringBuilder builder, string pattern, params object[] replacements)
        {
            builder.AppendFormat(CultureInfo.InvariantCulture, pattern, replacements);
        }

        public static IDisposable BlockInvariant(this IIndentedStringBuilder builder, string pattern, params object[] parameters)
        {
            return BlockInvariant(builder, true, pattern, parameters);
        }

        public static IDisposable BlockInvariant(this IIndentedStringBuilder builder, bool braces, string pattern, params object[] parameters)
        {
            return builder.Block(CultureInfo.InvariantCulture, braces, pattern, parameters);
        }
    }
}