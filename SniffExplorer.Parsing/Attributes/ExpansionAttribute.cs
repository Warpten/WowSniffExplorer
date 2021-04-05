using System;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Parsing.Attributes
{
    /// <summary>
    /// This attribute needs to be applied to expandion handling modules for them to be
    /// used in packet parsing.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class ExpansionAttribute : Attribute
    {
        public Expansion Expansion { get; set; }
        public RealmExpansionType RealmExpansionType { get; set; }
    }
}
