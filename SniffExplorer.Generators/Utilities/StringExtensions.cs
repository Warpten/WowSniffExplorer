using System.Text.RegularExpressions;

namespace SniffExplorer.Generators.Utilities
{
    internal static partial class StringExtensions
    {
        private static readonly Regex _newLineRegex = new Regex(@"^", RegexOptions.Compiled | RegexOptions.Multiline);

        public static string Indent(this string text, int indentCount = 1, bool useTabulations = false)
        {
            return _newLineRegex.Replace(text, new string(useTabulations ? '\t' : ' ', useTabulations ? indentCount : (indentCount * 4)));
        }

        public static string Indent(this string text, bool useTabulations = false)
        {
            return Indent(text, 1, useTabulations);
        }
    }
}