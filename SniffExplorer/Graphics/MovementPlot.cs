using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace SniffExplorer.Graphics
{
    public class MovementPlot : HelixViewport3D
    {
        private double[] _boundingBox = new double[6];
        
        private double MinimumX => _boundingBox[0];
        private double MaximumX => _boundingBox[1];
        private double MinimumY => _boundingBox[2];
        private double MaximumY => _boundingBox[3];
        private double MinimumZ => _boundingBox[4];
        private double MaximumZ => _boundingBox[5];

        private readonly LinesVisual3D _path;
        private readonly PointsVisual3D _points;

        public MovementPlot() : base()
        {
            _path = new LinesVisual3D
            {
                Color = Colors.Red,
                Thickness = 3.0,
            };

            _points = new PointsVisual3D()
            {
                Color = Colors.Red,
                Size = 5.0,
            };

            Clear();
        }

        public void AddPoint(Point3D point)
        {
            // Add the point twice if adding it would make the count go even.
            if (_path.Points.Count > 0)
                _path.Points.Add(point);
            _path.Points.Add(point);

            _points.Points.Add(point);
            
            _boundingBox[0] = Math.Min(_boundingBox[0], point.X - 5.0);
            _boundingBox[1] = Math.Max(_boundingBox[1], point.X + 5.0);
            _boundingBox[2] = Math.Min(_boundingBox[2], point.Y - 5.0);
            _boundingBox[3] = Math.Max(_boundingBox[3], point.Y + 5.0);
            _boundingBox[4] = Math.Min(_boundingBox[4], point.Z - 5.0);
            _boundingBox[5] = Math.Max(_boundingBox[5], point.Z + 5.0);
        }

        public void Clear()
        {
            _path.Points.Clear();
            _points.Points.Clear();
            
            _boundingBox[0] = double.MaxValue;
            _boundingBox[1] = double.MinValue;
            _boundingBox[2] = double.MaxValue;
            _boundingBox[3] = double.MinValue;
            _boundingBox[4] = double.MaxValue;
            _boundingBox[5] = double.MinValue;
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

            if (_points.Points.Count > 0)
            {
                var xLength = MaximumX - MinimumX;
                var yLength = MaximumY - MinimumY;
                var zLength = MaximumZ - MinimumZ;
                var boundingBoxSize = Math.Max(Math.Max(Math.Abs(xLength), Math.Abs(yLength)), Math.Abs(zLength));
                var lineThickness = boundingBoxSize / 1000;

                var boundingBox = new Rect3D()
                {
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
                    Children.Add(new ArrowVisual3D
                    {
                        Diameter = lineThickness * 5.0,
                        Fill = new SolidColorBrush(Colors.Black),
                        Point1 = start,
                        Point2 = end,
                        ThetaDiv = 16
                    });

                    Children.Add(new BillboardTextVisual3D()
                    {
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

                if (true)
                {
                    Children.Add(new BoundingBoxWireFrameVisual3D()
                    {
                        Thickness = 1.0,
                        Color = Colors.Black,
                        BoundingBox = boundingBox
                    });

                    Children.Add(new GridLinesVisual3D()
                    {
                        Center = new Point3D(boundingBox.X + boundingBox.SizeX / 2.0f,
                            boundingBox.Y + boundingBox.SizeY / 2.0f,
                            MinimumZ),
                        Length = boundingBox.SizeX,
                        Width = boundingBox.SizeY,
                        Thickness = 0.15,
                        Fill = new SolidColorBrush(Colors.Gray),
                        MinorDistance = 5.0,
                        MajorDistance =  boundingBoxSize
                    });
                }

                Children.Add(_points);
                Children.Add(_path);

                Camera.LookAt(new Point3D(MinimumX, MinimumY, MinimumZ), 0.0d);

                // Expand the bounding box for zooming a bit farther back
                boundingBox.SizeX *= 1.15f;
                boundingBox.SizeY *= 1.15f;
                boundingBox.SizeZ *= 1.15f;
                ZoomExtents(boundingBox, 0.0D);
            }
        }
    }
}
