using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SniffExplorer.Generators.Utilities;
using System.Collections.Generic;
using System.Linq;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Generators.UpdateFields
{
    [Generator(LanguageNames.CSharp)]
    public class UpdateFieldResolverGenerator : BaseGenerator
    {
        private static readonly Diagnostic _missingPropertyDiagnostic;
        private static readonly Diagnostic _unexpectedArityDiagnostic;
        private static readonly Diagnostic _expectedArityDiagnostic;
        private static readonly Diagnostic _missingAttributeDiagnostic;

        static UpdateFieldResolverGenerator()
        {
            _missingAttributeDiagnostic = new Diagnostic()
            {
                Category = "SniffExplorer.UpdateFields",
                ID = "UF001",
                Severity = DiagnosticSeverity.Error,
                Title = "Missing DescriptorValueAttribute",
                Description = "Missing [DescriptorValue(...)]",
                Message = "(DescriptorGenerator) DescriptorValueAttribute is missing from {0}. This enumeration will not generate any property."
            };

            _unexpectedArityDiagnostic = new Diagnostic
            {
                Category = "SniffExplorer.UpdateFields",
                ID = "UF002",
                Description = "Unxpected Arity",
                Message = "(DescriptorGenerator) Enumeration {0} has an arity provided, but the corresponding property in {1} is not an array.",
                Title = "Unexpected Arity",
                Severity = DiagnosticSeverity.Error
            };

            _expectedArityDiagnostic = new Diagnostic
            {
                Category = "SniffExplorer.UpdateFields",
                ID = "UF002",
                Description = "Unxpected Arity",
                Message = "(DescriptorGenerator) Property {1} is an array, but no arity was specified in enumeration {0}.",
                Title = "Unexpected Arity",
                Severity = DiagnosticSeverity.Error
            };

            _missingPropertyDiagnostic = new Diagnostic()
            {
                Category = "SniffExplorer.UpdateFields",
                ID = "UF003",
                Description = "Missing property",
                Message = "(DescriptorGenerator) No property named {0} found in {1}.",
                Title = "Missing property",
                Severity = DiagnosticSeverity.Warning
            };
        }

        public override void Initialize(GeneratorInitializationContext context)
        {
            // if (!Debugger.IsAttached)
            //     Debugger.Launch();

            // Register a syntax receiver that will be created for each generation pass
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public override void Execute(GeneratorExecutionContext context)
        {
            // retrieve the populated receiver 
            if (!(context.SyntaxContextReceiver is SyntaxReceiver receiver))
                return;

            foreach (var enumerationSymbol in receiver.Enumerations)
            {
                var generatedDescriptorData = context.Compilation.GetTypeByMetadataName(typeof(GeneratedDescriptorAttribute).FullName);

                var descriptorAttributeData = context.Compilation.GetTypeByMetadataName(typeof(DescriptorAttribute).FullName);
                var descriptorAttribute = enumerationSymbol!.FindAttribute(descriptorAttributeData);

                var targetClientBuild = (uint)descriptorAttribute!.FindArgument(nameof(DescriptorAttribute.ClientBuild))!.Value.Value!;
                var targetExpansion = descriptorAttribute!.FindArgument(nameof(DescriptorAttribute.RealmType))!.Value;

                var interfaceArgument = descriptorAttribute!.FindArgument(nameof(DescriptorAttribute.InterfaceType));
                var interfaceSymbol = interfaceArgument!.Value.Value as ISymbol;

                var typeName = $"{interfaceSymbol!.Name}Impl";

                var handler = new EnumerationHandler(enumerationSymbol, context);

                handler.OnPropertyNotFound += (symbol, name) =>
                    _missingPropertyDiagnostic.Report(context, symbol.Locations, name, interfaceSymbol.ToDisplayString());
                handler.OnMissingAttribute += (attrSymbol, target) =>
                    _missingAttributeDiagnostic.Report(context, target.Locations, target.Name);
                handler.OnInvalidArity += (symbol, expected) =>
                    (expected ? _expectedArityDiagnostic : _unexpectedArityDiagnostic).Report(context, symbol.Locations, symbol.Name, interfaceSymbol.ToDisplayString());

                handler.ResolveProperties();

                var sourceGenerator = CreateBuilder();

                sourceGenerator.AppendLine("using SniffExplorer.Parsing.Types;");
                sourceGenerator.AppendLine("using SniffExplorer.Parsing.Engine;");
                sourceGenerator.AppendLine("using SniffExplorer.Parsing.Engine.Tracking;");
                sourceGenerator.AppendLine("using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;");
                sourceGenerator.AppendLine("using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations;");
                sourceGenerator.AppendLine();

                using (sourceGenerator.BlockInvariant($"namespace {enumerationSymbol.ContainingNamespace.ToDisplayString()}.{(RealmExpansionType) targetExpansion.Value!}"))
                {
                    sourceGenerator.AppendLine($"[{generatedDescriptorData!.ToDisplayString()}(ClientBuild = {targetClientBuild}, RealmType = {targetExpansion.Type}.{(RealmExpansionType) targetExpansion.Value})]");
                    using (sourceGenerator.BlockInvariant($"public class {typeName} : {interfaceSymbol!.ToDisplayString()}"))
                    {
                        sourceGenerator.AppendLine(@"public int BitCount { get; }");
                        sourceGenerator.AppendLine();

                        foreach (var property in handler.Properties)
                            property.RenderDeclaration(sourceGenerator);

                        sourceGenerator.AppendLine();

                        using (sourceGenerator.BlockInvariant($"public {typeName}(ParsingContext context)"))
                        {
                            var iteratorGenerator = new IteratorGenerator();

                            foreach (var property in handler.Properties)
                                property.RenderInitializer(context, sourceGenerator, iteratorGenerator);

                            sourceGenerator.AppendLine();
                            sourceGenerator.AppendLineInvariant($"BitCount = {handler.Properties.Last.Value.NextOffset};");
                        }

                        sourceGenerator.AppendLine();

                        using (sourceGenerator.BlockInvariant($"public void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)"))
                        {
                            var iteratorGenerator = new IteratorGenerator();

                            foreach (var property in handler.Properties)
                                property.RenderParser(sourceGenerator, iteratorGenerator);
                        }
                    }
                }
                
                context.AddSource($"UpdateFields.V{targetClientBuild}.{typeName}.{(RealmExpansionType) targetExpansion.Value!}.Generated.cs", sourceGenerator.ToString());
            }

        }
    }

    class SyntaxReceiver : ISyntaxContextReceiver
    {
        public List<INamedTypeSymbol> Enumerations { get; } = new();

        /// <summary>
        /// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
        /// </summary>
        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            // any field with at least one attribute is a candidate for property generation
            if (context.Node is EnumDeclarationSyntax enumDeclarationSyntax && enumDeclarationSyntax.AttributeLists.Count > 0)
            {
                var enumSymbol = context.SemanticModel.GetDeclaredSymbol(enumDeclarationSyntax);
                if (!(enumSymbol is INamedTypeSymbol namedTypeSymbol))
                    return;

                if (enumSymbol.GetAttributes().Any(attr => attr.AttributeClass?.MetadataName == nameof(DescriptorAttribute))) 
                    Enumerations.Add(namedTypeSymbol);
            }
        }
    }
}
