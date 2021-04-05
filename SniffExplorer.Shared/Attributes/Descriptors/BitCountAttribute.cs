using System;
using System.Collections.Generic;
using System.Text;

namespace SniffExplorer.Shared.Attributes.Descriptors
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class BitCountAttribute : Attribute
    {
        private int _count;

        public BitCountAttribute(int count)
        {
            _count = count;
        }
    }
}
