using System;

namespace SniffExplorer.Shared.Attributes.UI
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class NotifyingPropertyAttribute : Attribute
    {
        public string? PropertyName { get; set; }

        public string? BeforeCallback { get; set; }
        public string? AfterCallback { get; set; }
    }
}
