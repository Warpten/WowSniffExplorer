using System.Collections.Generic;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking
{
    public class AuraStore
    {
        public class Entry
        {
            public uint ID { get; set; }

            public IObjectGUID? CasterGuid { get; set; }
            public uint Level { get; set; }

            public bool IsPositive { get; set; }
            public bool IsScalable { get; set; }

            public uint Charges { get; set; }

            public uint MaxDuration { get; set; }
            public uint Duration { get; set; }
            
            public int[] Values { get; set; } = new int[5];
        }

        private readonly IHistory<Entry>[] _auras = new IHistory<Entry>[0xFF];

        public IHistory<Entry> this[uint slot]
        {
            get
            {
                if (EqualityComparer<IHistory<Entry>>.Default.Equals(_auras[slot], default))
                    _auras[slot] = HistoryFactory.Create<Entry>();
                
                return _auras[slot];
            }
        }
    }
}
