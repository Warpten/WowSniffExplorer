using System;
using System.Reflection;

namespace SniffExplorer.Shared.Attributes.Descriptors
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class DescriptorValueAttribute : Attribute
    {
        public Type ValueType { get; }

        public int Arity { get; }

        public DescriptorValueAttribute(Type valueType) : this(valueType, 0)
        {

        }

        public DescriptorValueAttribute(Type valueType, int arity)
        {
            ValueType = valueType;
            Arity = arity;
        }
    }
}