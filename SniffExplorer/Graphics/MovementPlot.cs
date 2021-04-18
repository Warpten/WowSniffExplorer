using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using HelixToolkit.Wpf;

namespace SniffExplorer.Graphics
{
    public class MovementPlot : HelixViewport3D
    {
        private readonly double[] _boundingBox = new double[6];
        
        private double MinimumX => _boundingBox[0];
        private double MaximumX => _boundingBox[1];
        private double MinimumY => _boundingBox[2];
        private double MaximumY => _boundingBox[3];
        private double MinimumZ => _boundingBox[4];
        private double MaximumZ => _boundingBox[5];

        private class Sequence
        {
            public readonly LinesVisual3D Path;
            public readonly PointsVisual3D Points;

            public Sequence(Color color)
            {
                Path = new LinesVisual3D
                {
                    Color = color,
                    Thickness = 3.0,
                };

                Points = new PointsVisual3D()
                {
                    Color = color,
                    Size = 6.0,
                };
            }
        }

        private readonly List<Sequence> _sequences = new();


        public MovementPlot() : base()
        {
            Clear();
        }

        public void AddPoint(Point3D point)
        {
            var currentSequence = _sequences.Last();

            // If this is the third point being added (or more), we add the last point again, so that we get proper segments.
            if (currentSequence.Points.Points.Count > 1)
                currentSequence.Path.Points.Add(currentSequence.Points.Points.Last());

            currentSequence.Path.Points.Add(point);
            currentSequence.Points.Points.Add(point);
            
            _boundingBox[0] = Math.Min(_boundingBox[0], point.X - 5.0);
            _boundingBox[1] = Math.Max(_boundingBox[1], point.X + 5.0);
            _boundingBox[2] = Math.Min(_boundingBox[2], point.Y - 5.0);
            _boundingBox[3] = Math.Max(_boundingBox[3], point.Y + 5.0);
            _boundingBox[4] = Math.Min(_boundingBox[4], point.Z - 5.0);
            _boundingBox[5] = Math.Max(_boundingBox[5], point.Z + 5.0);
        }

        public void Clear()
        {
            _sequences.Clear();
            
            _boundingBox[0] = double.MaxValue;
            _boundingBox[1] = double.MinValue;
            _boundingBox[2] = double.MaxValue;
            _boundingBox[3] = double.MinValue;
            _boundingBox[4] = double.MaxValue;
            _boundingBox[5] = double.MinValue;
        }

        public void BeginSequence(Color color)
        {
            // If there was a previous sequence, link to it
            var previousSequence = _sequences.LastOrDefault();
            var newSequence = new Sequence(color);

            _sequences.Add(newSequence);

            if (previousSequence != null && previousSequence.Points.Points.Count != 0)
            {
                var lastPoint = previousSequence.Points.Points.Last();
                newSequence.Points.Points.Add(lastPoint);
                newSequence.Path.Points.Add(lastPoint);
            }
        }

        public void AddPoints(IEnumerable<Point3D> points)
        {
            foreach (var point in points)
                AddPoint(point);
        }

        public void Render()
        {
            Children.Clear();
            Children.Add(new DefaultLights());
            
            var xLength = MaximumX - MinimumX;
            var yLength = MaximumY - MinimumY;
            var zLength = MaximumZ - MinimumZ;

            var boundingBoxSize = Math.Max(Math.Max(Math.Abs(xLength), Math.Abs(yLength)), Math.Abs(zLength));
            var lineThickness = boundingBoxSize / 1000;

            var boundingBox = new Rect3D {
                X = MinimumX,
                Y = MinimumY,
                Z = MinimumZ,
                SizeX = xLength,
                SizeY = yLength,
                SizeZ = zLength
            };

            // Create Axis.
            void createAxis(string label, Point3D start, Point3D end, Point3D labelPosition)
            {
                Children.Add(new ArrowVisual3D {
                    Diameter = lineThickness * 5.0,
                    Fill = new SolidColorBrush(Colors.Black),
                    Point1 = start,
                    Point2 = end,
                    ThetaDiv = 16
                });

                Children.Add(new BillboardTextVisual3D {
                    Text = label,
                    FontWeight = FontWeights.Bold,
                    FontSize = 18,
                    Foreground = new SolidColorBrush(Colors.Black),
                    Position = labelPosition
                });
            }

            createAxis("X", new Point3D(MinimumX, MinimumY, MinimumZ),
                new Point3D(MinimumX + xLength + 15, MinimumY, MinimumZ),
                new Point3D(MinimumX + xLength + 15 + 5, MinimumY, MinimumZ));
            createAxis("Y", new Point3D(MinimumX, MinimumY, MinimumZ),
                new Point3D(MinimumX, MinimumY + yLength + 15, MinimumZ),
                new Point3D(MinimumX, MinimumY + yLength + 15 + 5, MinimumZ));
            createAxis("Z", new Point3D(MinimumX, MinimumY, MinimumZ),
                new Point3D(MinimumX, MinimumY, MinimumZ + zLength + 15),
                new Point3D(MinimumX, MinimumY, MinimumZ + zLength + 15 + 5));

            foreach (var sequence in _sequences)
            {
                if (sequence.Points.Points.Count == 0)
                    continue;
                
                Children.Add(sequence.Points);
                // Don't try to plot the path if it only has one point.
                if (sequence.Path.Points.Count > 1)
                    Children.Add(sequence.Path);
            }
            
            Children.Add(new BoundingBoxWireFrameVisual3D {
                Thickness = 1.0,
                Color = Colors.Black,
                BoundingBox = boundingBox
            });

            Children.Add(new GridLinesVisual3D {
                Center = new Point3D(boundingBox.X + boundingBox.SizeX / 2.0f,
                    boundingBox.Y + boundingBox.SizeY / 2.0f,
                    MinimumZ),
                Length = boundingBox.SizeX,
                Width = boundingBox.SizeY,
                Thickness = 0.15,
                Fill = new SolidColorBrush(Colors.Gray),
                MinorDistance = 5.0,
                MajorDistance = boundingBoxSize
            });
            
            Camera.LookAt(new Point3D(MinimumX, MinimumY, MinimumZ), 0.0d);

            // Expand the bounding box for zooming a bit farther back
            boundingBox.SizeX *= 1.15f;
            boundingBox.SizeY *= 1.15f;
            boundingBox.SizeZ *= 1.15f;
            ZoomExtents(boundingBox, 0.0D);
        }
    }
}
