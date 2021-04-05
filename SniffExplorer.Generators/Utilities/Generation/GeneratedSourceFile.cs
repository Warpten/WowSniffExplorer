using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security;
using System.Text;
using Microsoft.CodeAnalysis;

namespace SniffExplorer.Generators.Utilities.Generation
{
    public interface IGeneratedBlock
    {
        public string Name { get; }
        public IGeneratedBlock? ContainingBlock { get; }

        public GeneratedClass BeginClass(string name);

        public IEnumerable<INamespaceSymbol?> CollectNamespaces();

        public void Render(IndentedStringBuilder builder);
    }

    public class GeneratedNamespace : IGeneratedBlock
    {
        private readonly string _name;

        private readonly List<GeneratedClass> _classes = new();

        public string Name => _name;
        public IGeneratedBlock? ContainingBlock { get; } = null;

        public GeneratedNamespace(string name)
            => _name = name;

        public GeneratedClass BeginClass(string name)
            => new(this, name);

        public IEnumerable<INamespaceSymbol?> CollectNamespaces()
            => _classes.SelectMany(c => c.CollectNamespaces());

        public void Render(IndentedStringBuilder builder)
        {
            var collectedNamespaces = CollectNamespaces().Distinct(SymbolEqualityComparer.Default);
            foreach (var collectedNamespace in collectedNamespaces)
                builder.AppendLine($"using {collectedNamespace};");

            builder.AppendLine();

            using (builder.BlockInvariant($"namespace {_name}"))
            {
                foreach (var @class in _classes)
                {
                    @class.Render(builder);
                    builder.AppendLine();
                }
            }

            builder.AppendLine();
        }
    }

    public class GeneratedMethod : IGeneratedBlock
    {
        private readonly string _name;
        private readonly GeneratedClass _enclosingType;

        private readonly ITypeSymbol? _returnType;
        private readonly List<GeneratedParameter> _parameters = new();

        public string Name => _name;
        public IGeneratedBlock? ContainingBlock => _enclosingType;

        public GeneratedMethod(string name, ITypeSymbol returnType, GeneratedClass enclosingType)
        {
            _name = name;
            _returnType = returnType;
            _enclosingType = enclosingType;
        }

        public GeneratedClass BeginClass(string name)
            => throw new InvalidOperationException();

        public IEnumerable<INamespaceSymbol?> CollectNamespaces()
        {
            if (_returnType != null)
                yield return _returnType.ContainingNamespace;

            foreach (var parameter in _parameters)
                yield return parameter.Type.ContainingNamespace;
        }

        public void Render(IndentedStringBuilder builder)
        {
            throw new NotImplementedException();
        }
    }

    public enum VisiblityType
    {
        Public,
        Private,
        Protected
    }

    public class GeneratedField
    {
        public ITypeSymbol Type { get; }
        public bool IsReadOnly { get; }
        public bool IsRef { get; }
        public string Name { get; }
        public VisiblityType Visibility { get; }

        public GeneratedField(ITypeSymbol type, bool isReadOnly, bool isRef, string name, VisiblityType visibility = VisiblityType.Public)
        {
            Type = type;
            IsReadOnly = isReadOnly;
            IsRef = isRef;
            Name = name;
            Visibility = visibility;
        }
    }

    public class GeneratedParameter
    {
        public ITypeSymbol Type { get; }
        public string Name { get; }

        public GeneratedParameter(ITypeSymbol type, string name)
        {
            Type = type;
            Name = name;
        }
    }

    public class GeneratedProperty
    {
        public VisiblityType Visibility { get; }
        public ITypeSymbol Type { get; }
        public string Name { get; }

        public GeneratedGetter? Getter { get; set; }
        public GeneratedSetter? Setter { get; set; }

        public GeneratedProperty(VisiblityType visibility, ITypeSymbol type, string name)
        {
            Type = type;
            Name = name;
            Visibility = visibility;
        }
    }

    public class GeneratedGetter
    {

    }

    public class GeneratedSetter
    {

    }

    public class GeneratedClass : IGeneratedBlock
    {
        private readonly List<GeneratedMethod> _methods = new();
        private readonly List<GeneratedField> _fields = new();
        private readonly List<GeneratedProperty> _properties = new();

        public string Name { get; }
        public IGeneratedBlock? ContainingBlock { get; }

        public GeneratedClass(IGeneratedBlock enclosingGeneratedBlock, string name)
        {
            ContainingBlock = enclosingGeneratedBlock;
            Name = name;
        }

        public GeneratedClass BeginClass(string name)
            => new(this, name);

        public IEnumerable<INamespaceSymbol?> CollectNamespaces()
        {
            return _methods.SelectMany(m => m.CollectNamespaces());
        }

        public void Render(IndentedStringBuilder builder)
        {
            IEnumerable<string> getModifiers(GeneratedField field)
            {
                if (field.IsReadOnly)
                    yield return "readonly";

                if (field.IsRef)
                    yield return "ref";
            }

            foreach (var property in _properties)
            {

            }

            foreach (var field in _fields)
                builder.AppendLine($"{field.Visibility.ToString().ToLower()} {string.Join(" ", getModifiers(field))} {field.Type.Name} {field.Name};");

            if (_fields.Count != 0)
                builder.AppendLine();

            builder.AppendLine();
        }
    }
}
