using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SniffExplorer.Generators.UpdateFields;
using SniffExplorer.Generators.Utilities;
using SniffExplorer.Shared.Attributes.Descriptors;

namespace SniffExplorer.Generators.UpdateFields
{
    class EnumerationHandler
    {
        public INamedTypeSymbol Symbol => _symbol;

        private readonly INamedTypeSymbol _symbol;
        private readonly GeneratorExecutionContext _context;

        private readonly AttributeData? _descriptorAttributeData;

        public event Action<INamedTypeSymbol /* interfaceSymbol */, string /* propertyName */>? OnPropertyNotFound;
        public event Action<Type /* attrSymbol */, ISymbol /* target */>? OnMissingAttribute;
        public event Action<ISymbol /* enumMember */, bool /* expected */>? OnInvalidArity;

        private readonly LinkedList<IUpdateFieldGenerator> _generatedFields;

        public LinkedList<IUpdateFieldGenerator> Properties => _generatedFields;

        public EnumerationHandler(INamedTypeSymbol symbol, GeneratorExecutionContext context)
        {
            _symbol = symbol;
            _context = context;

            var attributeSymbol = context.GetSymbol<DescriptorAttribute>();
            _descriptorAttributeData = symbol.FindAttribute(attributeSymbol);

            _generatedFields = new LinkedList<IUpdateFieldGenerator>();
        }

        private static IPropertySymbol? _ResolveProperty(INamedTypeSymbol interfaceSymbol, ISymbol? enumerationValue)
        {
            if (enumerationValue == null)
                return null;

            var interfaceMember = interfaceSymbol.GetMembers(enumerationValue.Name).FirstOrDefault();
            if (interfaceMember == null || interfaceMember.Kind != SymbolKind.Property)
                return null;

            return (IPropertySymbol) interfaceMember;
        }

        private AttributeData? GetAttributeData<T>(ISymbol symbol)
        {
            var descriptorValueSymbol = _context.Compilation.GetTypeByMetadataName(typeof(T).FullName);
            return symbol.FindAttribute(descriptorValueSymbol);
        }

        public void ResolveProperties()
        {
            if (_descriptorAttributeData == null)
                return;

            // Find the actual interface type for the descriptor block.
            var interfaceType = _descriptorAttributeData.FindArgument(nameof(DescriptorAttribute.InterfaceType))!.Value;
            var interfaceSymbol = (INamedTypeSymbol) interfaceType.Value!;
            if (interfaceSymbol == null)
                return;

            // For each member of the decorated enumeration ...
            var enumMembers = _symbol.GetMembers();
            for (var i = 0; i < enumMembers.Length; ++i)
            {
                if (enumMembers[i].Kind != SymbolKind.Field)
                    continue;

                // Find the corresponding interface property, by name.
                var interfaceProperty = _ResolveProperty(interfaceSymbol, enumMembers[i]);
                var previousGeneratedField = _generatedFields.Count switch
                {
                    0 => null,
                    _ => _generatedFields.Last,
                };

                if (interfaceProperty == null)
                {
                    // Property not found; probably a mistyped enum member: emit a diagnostic.
                    OnPropertyNotFound?.Invoke(interfaceSymbol, enumMembers[i].Name);
                }

                // Find the attribute decorating the enumeration member.
                // If gives us detailed informations about the type of the property.
                var descriptorValueAttributeData = GetAttributeData<DescriptorValueAttribute>(enumMembers[i]);
                if (descriptorValueAttributeData == null)
                {
                    // Missing attribute: emit a diagnostic.
                    OnMissingAttribute?.Invoke(typeof(DescriptorValueAttribute), enumMembers[i]);
                    continue;
                }

                // Given the attribute, we will then extract
                // 1. The actual type of the updatefield.
                // 2. The arity of the property, if it is an array of updatefields.

                var updateFieldType =
                    descriptorValueAttributeData.FindArgument(nameof(DescriptorValueAttribute.ValueType))!.Value
                        .Value as ITypeSymbol;
                var propertyArity = descriptorValueAttributeData.FindArgument(nameof(DescriptorValueAttribute.Arity));

                // Can this happen?
                if (updateFieldType == null)
                    return;

                // If an arity was given, make sure the property is expected to be an array.
                // If an arity was not given, make sure the property is not expected to be an array.
                if (interfaceProperty != null && (interfaceProperty.Type.TypeKind == TypeKind.Array) != propertyArity.HasValue)
                {
                    // This one is special (sic) and expects arity
                    // Possibly move this to after having added fields? Build will fail anyways.
                    if (updateFieldType.Name != "IBlobUpdateField")
                    {
                        OnInvalidArity?.Invoke(enumMembers[i], interfaceProperty.Type.TypeKind == TypeKind.Array);
                        continue;
                    }
                }

                // Actual code generation part.
                switch (updateFieldType.SpecialType)
                {
                    case SpecialType.System_Byte:
                    case SpecialType.System_SByte:
                    case SpecialType.System_Char:
                    case SpecialType.System_Int16:
                    case SpecialType.System_UInt16:
                    case SpecialType.System_UInt32:
                    case SpecialType.System_Int32:
                    case SpecialType.System_UInt64:
                    case SpecialType.System_Int64:
                    case SpecialType.System_Single:
                    case SpecialType.System_Double:
                        _generatedFields.AddLast(
                            new PrimitiveFieldGenerator(updateFieldType, enumMembers[i], interfaceProperty,
                                previousGeneratedField?.Value ?? null, (int?) propertyArity?.Value));

                        break;
                    case SpecialType.None:
                    {
                        if (updateFieldType is IArrayTypeSymbol arrayTypeSymbol)
                        {
                            switch (arrayTypeSymbol.ElementType.SpecialType)
                            {
                                case SpecialType.System_Byte:
                                    _generatedFields.AddLast(
                                        new RawFieldGenerator<byte>(enumMembers[i], interfaceProperty,
                                            previousGeneratedField?.Value ?? null, (int?)propertyArity?.Value));
                                    break;
                                case SpecialType.System_SByte:
                                    _generatedFields.AddLast(
                                        new RawFieldGenerator<sbyte>(enumMembers[i], interfaceProperty,
                                            previousGeneratedField?.Value ?? null, (int?)propertyArity?.Value));
                                    break;
                                case SpecialType.System_Int16:
                                    _generatedFields.AddLast(
                                        new RawFieldGenerator<short>(enumMembers[i], interfaceProperty,
                                            previousGeneratedField?.Value ?? null, (int?)propertyArity?.Value));
                                    break;
                                case SpecialType.System_UInt16:
                                    _generatedFields.AddLast(
                                        new RawFieldGenerator<ushort>(enumMembers[i], interfaceProperty,
                                            previousGeneratedField?.Value ?? null, (int?)propertyArity?.Value));
                                    break;
                            }
                        }
                        else
                        {
                            switch (updateFieldType.Name)
                            {
                                case "GuidUpdateField":
                                case "IObjectGUID":
                                    _generatedFields.AddLast(
                                        new GuidFieldGenerator(enumMembers[i], interfaceProperty,
                                            previousGeneratedField?.Value ?? null,
                                            (int?) propertyArity?.Value));
                                    break;
                                case "IBlobUpdateField":
                                    _generatedFields.AddLast(
                                        new BlobFieldGenerator(enumMembers[i], interfaceProperty,
                                            previousGeneratedField?.Value ?? null,
                                            (int?)propertyArity?.Value));
                                    break;
                                default:
                                    _generatedFields.AddLast(
                                        new StructuredFieldGenerator(_context, updateFieldType, enumMembers[i], interfaceProperty,
                                            previousGeneratedField?.Value ?? null, (int?) propertyArity?.Value));
                                    break;
                            }
                        }

                        break;
                    }
                }
            }
        }
    }
}