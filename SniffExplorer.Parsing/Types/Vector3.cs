using System.Numerics;
using Microsoft.VisualBasic.CompilerServices;

namespace SniffExplorer.Parsing.Types
{
    public readonly struct Vector3
    {
        private readonly float[] _coordinates;

        public readonly float X => _coordinates[0];
        public readonly float Y => _coordinates[1];
        public readonly float Z => _coordinates[2];

        public static Vector3 operator + (Vector3 left, Vector3 right)
            => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Vector3 operator - (Vector3 left, Vector3 right)
            => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public static Vector3 operator + (Vector3 left, float delta)
            => new(left.X + delta, left.Y + delta, left.Z + delta);

        public static Vector3 operator - (Vector3 left, float delta)
            => new(left.X - delta, left.Y - delta, left.Z - delta);

        public static Vector3 operator * (Vector3 left, float scale)
            => new(left.X * scale, left.Y * scale, left.Z * scale);

        public static Vector3 operator / (Vector3 left, float scale)
            => new(left.X / scale, left.Y / scale, left.Z / scale);

        public Vector3(float x, float y, float z)
            => _coordinates = new[] { x, y, z };

        public Vector3 XZY => new(X, Z, Y);
        public Vector3 ZXY => new(Z, X, Y);
        public Vector3 ZYX => new(Z, Y, X);
        public Vector3 YXZ => new(Y, X, Z);
        public Vector3 YZX => new(Y, Z, X);
    }

    public class Vector4
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float O { get; set; }
    }
}
