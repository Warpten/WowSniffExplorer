using Microsoft.CodeAnalysis;
using SniffExplorer.Generators.Utilities;

namespace SniffExplorer.Generators.UpdateFields
{
    public abstract class UpdateFieldGenerator : IUpdateFieldGenerator
    {
        protected ISymbol EnumerationSymbol { get; }
        protected IPropertySymbol? PropertySymbol { get; }
        protected int? Cardinality { get; }

        protected string Offset { get; }

        public string NextOffset { get; }

        protected UpdateFieldGenerator(ISymbol enumerationSymbol, IPropertySymbol? propertySymbol, IUpdateFieldGenerator? previousField, int? cardinality = null)
        {
            EnumerationSymbol = enumerationSymbol;
            PropertySymbol = propertySymbol;
            Cardinality = cardinality;

            Offset = previousField != null
                ? $"{previousField.NextOffset}"
                : "0";

            NextOffset = Cardinality.HasValue
                ? $"{EnumerationSymbol.Name}[{Cardinality - 1}].BitEnd"
                : $"{EnumerationSymbol.Name}.BitEnd";
        }

        protected abstract string GetElementInitializer(GeneratorExecutionContext context,
            IteratorGenerator.Iterator? iterator = null);

        public void RenderDeclaration(IndentedStringBuilder stringBuilder)
        {
            if (Cardinality.HasValue)
            {
                var propertyTypeString = PropertySymbol == null
                    ? "IUpdateField[]"
                    : PropertySymbol.Type.ToString();

                stringBuilder.AppendLine($"public {propertyTypeString} {EnumerationSymbol.Name} {{ get; }}");
            }
            else
            {
                var propertyTypeString = PropertySymbol == null
                    ? "IUpdateField"
                    : PropertySymbol.Type.ToString();

                stringBuilder.AppendLine($"public {propertyTypeString} {EnumerationSymbol.Name} {{ get; }}");
            }
        }

        public void RenderInitializer(GeneratorExecutionContext context, IndentedStringBuilder stringBuilder,
            IteratorGenerator iteratorGenerator)
        {
            if (Cardinality.HasValue)
            {
                var propertyConstructionString = $"new IUpdateField[{Cardinality.Value}]";
                if (PropertySymbol != null)
                {
                    if (PropertySymbol.Type is IArrayTypeSymbol arrayTypeSymbol)
                    {
                        var baseType = arrayTypeSymbol.ElementType;
                        propertyConstructionString = $"new {baseType}[{Cardinality.Value}]";
                    }
                }

                stringBuilder.AppendLine();

                stringBuilder.AppendLine($"{EnumerationSymbol.Name} = {propertyConstructionString};");
                stringBuilder.AppendLine($"{EnumerationSymbol.Name}[0] = {GetElementInitializer(context)};");

                using var rentedIterator = iteratorGenerator.Rent();
                using (stringBuilder.BlockInvariant(false, $"for (var {rentedIterator} = 1; {rentedIterator} < {Cardinality.Value}; ++{rentedIterator})"))
                    stringBuilder.AppendLine($"{EnumerationSymbol.Name}[{rentedIterator}] = {GetElementInitializer(context, rentedIterator)};");

                stringBuilder.AppendLine();
            }
            else
                stringBuilder.AppendLine($"{EnumerationSymbol.Name} = {GetElementInitializer(context)};");
        }

        public void RenderParser(IndentedStringBuilder stringBuilder, IteratorGenerator iteratorGenerator)
        {
            if (Cardinality.HasValue)
            {
                stringBuilder.AppendLine();

                using var rentedIterator = iteratorGenerator.Rent();
                using (stringBuilder.BlockInvariant(false, $"for (var {rentedIterator} = 0; {rentedIterator} < {Cardinality.Value}; ++{rentedIterator})"))
                    stringBuilder.AppendLine($"{EnumerationSymbol.Name}[{rentedIterator}].ReadValue(packet, updateMask);");

                stringBuilder.AppendLine();
            }
            else
                stringBuilder.AppendLine($"{EnumerationSymbol.Name}.ReadValue(packet, updateMask);");
        }
    }
}