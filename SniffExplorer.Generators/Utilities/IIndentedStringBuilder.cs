using System;

namespace SniffExplorer.Generators.Utilities
{
    internal partial interface IIndentedStringBuilder
    {
        /// <summary>
        /// Gets the current indentation level
        /// </summary>
        int CurrentLevel { get; }

        /// <summary>
        /// Appends text using the current indentation level
        /// </summary>
        /// <param name="text"></param>
        void Append(string text);

        /// <summary>
        /// Appends formatted text using the current indentation level
        /// </summary>
        void AppendFormat(IFormatProvider formatProvider, string pattern, params object[] replacements);

        /// <summary>
        /// Appends a line using the current indentation level 
        /// </summary>
        void AppendLine();

        /// <summary>
        /// Writes the provided text and adds line using the current indentation level 
        /// </summary>
        void AppendLine(string text);

        /// <summary>
        /// Creates an indentation block
        /// </summary>
        /// <param name="count">The indentation level of the new block.</param>
        /// <param name="braces">Wether or not braces should be injected.</param>
        /// <returns>A disposable that will close the block</returns>
        IDisposable Block(int count = 1, bool braces = true);

        /// <summary>
        /// Creates an indentation block, e.g. using a C# curly braces.
        /// </summary>
        /// <returns>A disposable that will close the block</returns>
        IDisposable Block(IFormatProvider formatProvider, bool braces, string pattern, params object[] parameters);

        /// <summary>
        /// Adds an indentation 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        IDisposable Indent(int count = 1);

        /// <summary>
        /// Provides a string representing the complete builder.
        /// </summary>
        string ToString();
    }
}