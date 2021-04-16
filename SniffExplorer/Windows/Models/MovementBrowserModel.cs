using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Engine.Tracking.Types;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.UI;

namespace SniffExplorer.Windows.Models
{
    public partial class MovementBrowserModel
    {
        public IUnit Unit { get; set; }

        public IObjectGUID Guid => Unit.Guid;

        public IEnumerable<SplineInfo> Splines => Unit.Splines.Values.Where(splineInfo => splineInfo.Points.Length > 0);
    }
}
