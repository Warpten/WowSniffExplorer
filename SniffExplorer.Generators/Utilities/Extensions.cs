using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SniffExplorer.Generators.Utilities
{
    static class Extensions
    {
        public static IEnumerable<INamedTypeSymbol> GetNamespaceTypes(this INamespaceSymbol sym)
        {
            foreach (var child in sym.GetTypeMembers())
                yield return child;

            foreach (var ns in sym.GetNamespaceMembers())
               foreach (var child2 in GetNamespaceTypes(ns))
                    yield return child2;
        }

        /// <summary>
        /// Return attributes on the current type and all its ancestors
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static IEnumerable<AttributeData> GetAllAttributes(this ISymbol? symbol)
        {
            while (symbol != null)
            {
                foreach (var attribute in symbol.GetAttributes())
                    yield return attribute;

                symbol = (symbol as INamedTypeSymbol)?.BaseType;
            }
        }

        public static TypedConstant? FindArgument(this AttributeData attributeData,
            string filter)
            => FindArgument(attributeData, name => name == filter);

        public static TypedConstant? FindArgument(this AttributeData attributeData,
            Func<string, bool> filter)
        {
            var namedArgument = attributeData.NamedArguments.FirstOrDefault(kv => filter(kv.Key));
            if (namedArgument.Key != default)
                return namedArgument.Value;

            if (attributeData.AttributeConstructor == null)
                return null;

            var ctorParameters = attributeData.AttributeConstructor.Parameters;
            for (var i = 0; i < ctorParameters.Length; ++i)
            {
                if (filter(ctorParameters[i].Name))
                    return attributeData.ConstructorArguments[i];
            }

            return null;
        }

        public static INamedTypeSymbol? GetSymbol<T>(this GeneratorExecutionContext context)
            => context.Compilation.GetTypeByMetadataName(typeof(T).FullName);

        public static AttributeData? FindAttribute<T>(this ISymbol symbol, GeneratorExecutionContext context)
            where T : Attribute
        {
            var attributeData = context.GetSymbol<T>();
            if (attributeData == null)
                return null;

            return symbol.FindAttribute(attributeData);
        }

        public static bool HasAttribute<T>(this ISymbol symbol, GeneratorExecutionContext context)
            where T : Attribute
        {
            var attributeData = context.GetSymbol<T>();
            if (attributeData == null)
                return false;

            return symbol.FindAttribute(attributeData) != null;
        }

        public static AttributeData? FindAttribute(this ISymbol property, INamedTypeSymbol? attributeClassSymbol)
            => property.GetAllAttributes().FirstOrDefault(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, attributeClassSymbol));
    }
}
