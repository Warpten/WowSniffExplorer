using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types;
using SniffExplorer.Shared.Attributes.Descriptors;

namespace SniffExplorer.Cataclysm.UpdateFields.Types
{
    [BitCount(3)]
    public class ItemEnchantment : IItemEnchantment
    {
        public uint ID { get; }
        public uint Duration { get; }
        public ushort Inactive { get; }
        public ushort Charges { get; }

        public ItemEnchantment(Span<uint> values)
        {
            ID = values[0];
            Duration = values[1];

            var wordSpan = MemoryMarshal.Cast<uint, ushort>(values.Slice(2));
            Inactive = wordSpan[0];
            Charges = wordSpan[1];
        }
    }
}
