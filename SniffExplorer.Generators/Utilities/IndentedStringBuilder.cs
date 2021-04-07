using System;
using System.Text;

namespace SniffExplorer.Generators.Utilities
{
    /// <summary>
    /// A C# code indented builder.
    /// </summary>
    public partial class IndentedStringBuilder : IIndentedStringBuilder
    {
        private readonly StringBuilder _stringBuilder;

        public int CurrentLevel { get; private set; }

        public IndentedStringBuilder()
            : this(new StringBuilder())
        {
        }

        public IndentedStringBuilder(StringBuilder stringBuilder)
        {
            _stringBuilder = stringBuilder;
        }

        public virtual IDisposable Indent(int count = 1)
        {
            CurrentLevel += count;
            return new DisposableAction(() => CurrentLevel -= count);
        }

        public virtual IDisposable Block(int count = 1, bool braces = true)
        {
            var current = CurrentLevel;

            CurrentLevel += count;
            if (braces)
            {
                Append("{".Indent(current));
                AppendLine();
            }

            return new DisposableAction(() =>
            {
                CurrentLevel -= count;
                if (!braces)
                    return;

                Append("}".Indent(current));
                AppendLine();
            });
        }

        public virtual IDisposable Block(IFormatProvider formatProvider, bool braces, string pattern, params object[] parameters)
        {
            AppendFormat(formatProvider, pattern, parameters);
            AppendLine();

            return Block(1, braces);
        }

        public virtual void Append(string text)
        {
            _stringBuilder.Append(text);
        }

        public virtual void AppendFormat(IFormatProvider formatProvider, string pattern, params object[] replacements)
        {
            _stringBuilder.AppendFormat(formatProvider, pattern.Indent(CurrentLevel), replacements);
        }

        public virtual void AppendLine()
        {
            _stringBuilder.AppendLine();
        }

        public virtual void AppendLine(string text)
        {
            _stringBuilder.AppendLine(text.Indent(CurrentLevel));
        }

        public override string ToString()
        {
            return _stringBuilder.ToString();
        }
    }
}
