using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Engine.Tracking.Types;
using SniffExplorer.Windows.Models;

namespace SniffExplorer.Windows
{
    /// <summary>
    /// Interaction logic for MovementBrowser.xaml
    /// </summary>
    public partial class MovementBrowser : Window
    {
        private MovementBrowserModel Model => (MovementBrowserModel) DataContext;

        public MovementBrowser()
        {
            InitializeComponent();
        }

        public MovementBrowser(Unit unit)
        {
            DataContext = new MovementBrowserModel();
            Model.Unit = unit;

            InitializeComponent();
        }

        private void HandleSplineSelection(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ListView listView)
                return;

            if (listView.SelectedItem is not SplineInfo splineInfo)
                return;

            _movementPlot.Clear();
            _movementPlot.AddPoints(splineInfo.Points.Select(p => new Point3D(p.X, p.Y, p.Z)));
            _movementPlot.Render();
        }

        private void HandlePlotPositionHistory(object sender, RoutedEventArgs e)
        {
            _movementPlot.Clear();
            foreach (var splineInfo in Model.Unit.Splines.Values)
            {
                _movementPlot.AddPoints(splineInfo.Points.Select(p => new Point3D(p.X, p.Y, p.Z)));
            }

            _movementPlot.Render();
        }
    }
}
