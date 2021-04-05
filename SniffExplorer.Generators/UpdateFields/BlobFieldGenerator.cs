using Microsoft.CodeAnalysis;
using SniffExplorer.Generators.Utilities;

namespace SniffExplorer.Generators.UpdateFields
{
    public class BlobFieldGenerator : UpdateFieldGenerator
    {
        private readonly int _cardinality;

        public BlobFieldGenerator(ISymbol enumerationSymbol, IPropertySymbol? propertySymbol, IUpdateFieldGenerator? previousField, int? cardinality = null)
            : base(enumerationSymbol, propertySymbol, previousField, null)
        {
            _cardinality = cardinality.GetValueOrDefault(0);
        }

        protected override string GetElementInitializer(GeneratorExecutionContext context,
            IteratorGenerator.Iterator? iterator = null)
        {
            return $"new BlobUpdateField({Offset}, {_cardinality}, context)";
        }
    }
}