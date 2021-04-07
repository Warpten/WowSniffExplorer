using Microsoft.CodeAnalysis;
using SniffExplorer.Generators.Utilities;

namespace SniffExplorer.Generators.UpdateFields
{
    /// <summary>
    /// Interface for types in charge of generating updatefield members.
    /// </summary>
    public interface IUpdateFieldGenerator
    {
        public string NextOffset { get; }

        public void RenderInitializer(GeneratorExecutionContext context, IndentedStringBuilder stringBuilder, IteratorGenerator iteratorGenerator);
        public void RenderParser(IndentedStringBuilder stringBuilder, IteratorGenerator iteratorGenerator);
        public void RenderDeclaration(IndentedStringBuilder stringBuilder);
    }
}

