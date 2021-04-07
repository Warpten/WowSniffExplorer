using System;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class UpdateFieldResolverAttribute : Attribute
    {
        public uint ClientBuild { get; set; }
        public RealmExpansionType ExpansionType { get; set; }
    }
}
