using System;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Shared.Attributes.Descriptors
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class GeneratedDescriptorAttribute : Attribute
    {
        public uint ClientBuild { get; set; }
        public RealmExpansionType RealmType { get; set; }
    }
}