using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types
{
    public interface IQuestData
    {
        public uint ID { get; }
        public int State { get; }
        public ushort[] Counts { get; } // 4
        public int Time { get; }
    }
}
