using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using SniffExplorer.Generators.Utilities;
using SniffExplorer.Shared.Attributes.Descriptors;

namespace SniffExplorer.Generators.UpdateFields
{
    /// <summary>
    /// This class generates the code for a structured update field.
    /// </summary>
    public class StructuredFieldGenerator : UpdateFieldGenerator
    {
        private readonly GeneratorExecutionContext _context;
        private readonly ITypeSymbol _underlyingType;

        public StructuredFieldGenerator(GeneratorExecutionContext context, ITypeSymbol underlyingTypeSymbol, ISymbol enumerationSymbol, IPropertySymbol? propertySymbol, IUpdateFieldGenerator? previousField, int? cardinality = null)
            : base(enumerationSymbol, propertySymbol, previousField, cardinality)
        {
            _underlyingType = underlyingTypeSymbol;
            _context = context;
        }

        protected override string GetElementInitializer(GeneratorExecutionContext context,
            IteratorGenerator.Iterator? iterator = null)
        {
            var methodSymbol = _underlyingType.GetMembers("FromRawData").FirstOrDefault();
            var formattedMethodCall = string.Empty;
            if (methodSymbol == null)
            {
                var spanSymbol = context.Compilation.GetTypeByMetadataName(typeof(Span<>).FullName)?.ConstructUnboundGenericType();

                // Search for a constructor taking Span<uint>
                if (_underlyingType is INamedTypeSymbol namedSymbol)
                {
                    methodSymbol = namedSymbol.Constructors.FirstOrDefault(ctor =>
                    {
                        if (ctor.Parameters.Length != 1)
                            return false;

                        if (!(ctor.Parameters[0].Type is INamedTypeSymbol namedParameterSymbol))
                            return false;

                        if (SymbolEqualityComparer.Default.Equals(namedParameterSymbol.ConstructUnboundGenericType(), spanSymbol))
                            return true;

                        return false;
                    });
                }

                if (methodSymbol != null)
                    formattedMethodCall = $"values => new {_underlyingType}(values)";
                else
                    formattedMethodCall = "null";
            }
            else
            {
                formattedMethodCall = $"{_underlyingType}.{methodSymbol.Name}";
            }

            var bitCount = ((DetermineByteCount(_underlyingType) + 3) & ~3) / Unsafe.SizeOf<uint>();

            if (iterator != null)
                return $"new StructuredUpdateField<{_underlyingType}>({EnumerationSymbol.Name}[{iterator} - 1].BitEnd, {bitCount}, {formattedMethodCall})";

            return $"new StructuredUpdateField<{_underlyingType}>({Offset}, {bitCount}, {formattedMethodCall})";
        }

        private int DetermineByteCount(ITypeSymbol valueType)
        {
            var attributeSymbol = valueType.FindAttribute<BitCountAttribute>(_context);
            if (attributeSymbol != null)
                return (int) attributeSymbol.FindArgument("count")!.Value.Value! * 4;

            switch (valueType.SpecialType)
            {
                case SpecialType.System_Boolean:
                case SpecialType.System_Byte:
                case SpecialType.System_SByte:
                    return 1;
                case SpecialType.System_Int16:
                case SpecialType.System_UInt16:
                    return 2;
                case SpecialType.System_UInt32:
                case SpecialType.System_Int32:
                case SpecialType.System_Single:
                    return 4;
                case SpecialType.System_UInt64:
                case SpecialType.System_Int64:
                case SpecialType.System_Double:
                    return 8;
                case SpecialType.None:
                {
                    // Not a special type, recursively count from the properties.
                    var byteCount = 0;
                    foreach (var memberSymbol in valueType.GetMembers())
                    {
                        if (!(memberSymbol is IPropertySymbol propertyMemberSymbol))
                            continue;

                        // Skip static properties
                        if (propertyMemberSymbol.IsStatic)
                            continue;

                        // Skip the property if the getter is compiler-generated.
                        if (!(propertyMemberSymbol.GetMethod?.HasAttribute<CompilerGeneratedAttribute>(_context) ?? false))
                            continue;

                        byteCount += DetermineByteCount(propertyMemberSymbol.Type);
                    }

                    return byteCount;
                }
                default:
                    // Diagnostic, not handled
                    throw new NotImplementedException("oof");
            }
        }
    }
}