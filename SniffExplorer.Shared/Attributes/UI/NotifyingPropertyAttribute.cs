using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniffExplorer.Shared.Attributes.UI
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class NotifyingPropertyAttribute : Attribute
    {
        public string? PropertyName { get; set; }
    }
}
