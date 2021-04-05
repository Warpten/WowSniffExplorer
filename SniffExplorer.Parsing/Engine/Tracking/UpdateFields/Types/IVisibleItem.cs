using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types
{
    public interface IVisibleItem
    {
        public uint ID { get; }
        public ushort PermanentEnchantment { get; }
        public ushort TemporaryEnchantment { get; }
    }
}
