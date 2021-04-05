using Microsoft.CodeAnalysis;
using SniffExplorer.Generators.Utilities;

namespace SniffExplorer.Generators.UpdateFields
{
    public class GuidFieldGenerator : UpdateFieldGenerator
    {
        public GuidFieldGenerator(ISymbol enumerationSymbol, IPropertySymbol? propertySymbol, IUpdateFieldGenerator? previousField, int? cardinality = null)
            : base(enumerationSymbol, propertySymbol, previousField, cardinality)
        {
        }

        protected override string GetElementInitializer(GeneratorExecutionContext context,
            IteratorGenerator.Iterator? iterator = null)
        {
            if (iterator != null)
                return $"new GuidUpdateField({EnumerationSymbol.Name}[{iterator} - 1].BitEnd, context)";

            return $"new GuidUpdateField({Offset}, context)";
        }
    }
}