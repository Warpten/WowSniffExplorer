using SniffExplorer.Shared.Enums;

using System;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace SniffExplorer.Shared.Attributes.Descriptors
{
    /// <summary>
    /// This attribute, when applied on an enumeration, declares it as a source
    /// to generate an IUpdateFieldResolver.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = true)]
    public class DescriptorAttribute : Attribute
    {
        public uint ClientBuild { get; set; }
        public RealmExpansionType RealmType { get; set; }
        public Type InterfaceType { get; set; }
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
