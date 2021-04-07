using System;
using System.Runtime.InteropServices;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types;
using SniffExplorer.Shared.Attributes.Descriptors;

namespace SniffExplorer.Cataclysm.UpdateFields.Types
{
    [BitCount(5)]
    public class QuestData : IQuestData
    {
        public uint ID { get; }
        public int State { get; }
        public ushort[] Counts { get; }
        public int Time { get; }

        public QuestData(Span<uint> values)
        {
            ID = values[0];
            State = (int) values[1];

            var counters = MemoryMarshal.Cast<uint, ushort>(values.Slice(2, 2));

            Counts = new ushort[4];
            counters.CopyTo(new Span<ushort>(Counts));

            Time = (int) values[4];
        }
    }
}
