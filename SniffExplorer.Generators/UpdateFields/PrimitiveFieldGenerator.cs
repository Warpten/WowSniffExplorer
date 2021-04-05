using Microsoft.CodeAnalysis;
using SniffExplorer.Generators.Utilities;

namespace SniffExplorer.Generators.UpdateFields
{
    /// <summary>
    /// This class generates the code for a primitive update field (int, float, etc).
    /// </summary>
    public class PrimitiveFieldGenerator : UpdateFieldGenerator
    {
        private readonly ITypeSymbol _underlyingType;

        public PrimitiveFieldGenerator(ITypeSymbol underlyingTypeSymbol, ISymbol enumerationSymbol, IPropertySymbol? propertySymbol, IUpdateFieldGenerator? previousField, int? cardinality = null)
            : base(enumerationSymbol, propertySymbol, previousField, cardinality)
        {
            _underlyingType = underlyingTypeSymbol;
        }

        protected override string GetElementInitializer(GeneratorExecutionContext context,
            IteratorGenerator.Iterator? iterator = null)
        {
            if (iterator != null)
                return $"new PrimitiveUpdateField<{_underlyingType}>({EnumerationSymbol.Name}[{iterator} - 1].BitEnd, context)";

            return $"new PrimitiveUpdateField<{_underlyingType}>({Offset}, context)";
        }
    }
}