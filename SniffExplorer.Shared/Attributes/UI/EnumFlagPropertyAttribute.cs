using System;

namespace SniffExplorer.Shared.Attributes.UI
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class EnumFlagPropertyAttribute : Attribute
    {
    }
}
