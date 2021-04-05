using System;

namespace SniffExplorer.Shared.Attributes.Descriptors
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class DescriptorValueAttribute : Attribute
    {
        public Type ValueType { get; set; }

        public int Arity { get; set; }
    }
}