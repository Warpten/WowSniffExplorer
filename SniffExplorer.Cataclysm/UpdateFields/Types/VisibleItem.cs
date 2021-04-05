using System;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types;
using SniffExplorer.Shared.Attributes.Descriptors;

namespace SniffExplorer.Cataclysm.UpdateFields.Types
{
    [BitCount(2)]
    public class VisibleItem : IVisibleItem
    {
        public uint ID { get; }
        public ushort PermanentEnchantment { get; }
        public ushort TemporaryEnchantment { get; }

        public VisibleItem(Span<uint> values)
        {
            ID = values[0];

            var enchantmentSlice = MemoryMarshal.Cast<uint, ushort>(values.Slice(1));
            PermanentEnchantment = enchantmentSlice[0];
            TemporaryEnchantment = enchantmentSlice[1];
        }
    }
}
