using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.Types
{
    public class SplineInfo
    {
        public IObjectGUID? TransportGUID { get; set; }
        public uint? SeatID { get; set; }

        public bool IsVoluntaryExit { get; set; }
        public uint ID { get; set; }

        public SplineMode Mode { get; set; }

        public IObjectGUID? TargetGUID { get; set; }
        public Vector3 FacingSpot { get; set; }

        public Vector3[] Points { get; set; }

        public uint MoveTime { get; set; }
    }
}
