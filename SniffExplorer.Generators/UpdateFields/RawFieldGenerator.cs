using Microsoft.CodeAnalysis;
using SniffExplorer.Generators.Utilities;

namespace SniffExplorer.Generators.UpdateFields
{
    /// <summary>
    /// This class generates the code for a bytes update field.
    /// </summary>
    public class RawFieldGenerator<T> : UpdateFieldGenerator where T : unmanaged
    {
        public RawFieldGenerator(ISymbol enumerationSymbol, IPropertySymbol? propertySymbol, IUpdateFieldGenerator? previousField, int? cardinality = null)
            : base(enumerationSymbol, propertySymbol, previousField, cardinality)
        {
        }

        protected override string GetElementInitializer(GeneratorExecutionContext context,
            IteratorGenerator.Iterator? iterator = null)
        {
            if (iterator != null)
                return $"new RawUpdateField<{typeof(T).FullName}>({EnumerationSymbol.Name}[{iterator} - 1].BitEnd, context)";

            return $"new RawUpdateField<{typeof(T).FullName}>({Offset}, context)";
        }
    }
}