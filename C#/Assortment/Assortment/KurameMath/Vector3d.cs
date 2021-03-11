using System;

namespace Assortment.KurameMath
{
    public readonly struct Vector3d
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Vector3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double Abs() => Math.Sqrt(X * X + Y * Y + Z * Z);

        public Vector3d Normalize() => this / Abs();

        public double Dot(Vector3d v) => X * v.X + Y * v.Y + Z * v.Z;

        #region Rotation

        public Vector3d RotYZ(double theta)
        {
            var (cos, sin) = (Math.Cos(theta), Math.Sin(theta));
            double y = cos * Y - sin * Z;
            double z = sin * Y + cos * Z;
            return new(X, y, z);
        }

        public Vector3d RotZX(double theta)
        {
            var (cos, sin) = (Math.Cos(theta), Math.Sin(theta));
            double x = cos * X + sin * Z;
            double z = -sin * X + cos * Z;
            return new(x, Y, z);
        }

        public Vector3d RotXY(double theta)
        {
            var (cos, sin) = (Math.Cos(theta), Math.Sin(theta));
            double x = cos * X - sin * Y;
            double y = sin * X + cos * Y;
            return new(x, y, Z);
        }

        #endregion Rotation

        #region Overload of operators

        public static Vector3d operator +(Vector3d v1, Vector3d v2)
            => new(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);

        public static Vector3d operator *(double k, Vector3d v)
            => new(k * v.X, k * v.Y, k * v.Z);

        public static Vector3d operator *(Vector3d v, double k) => k * v;

        public static Vector3d operator -(Vector3d v) => -1 * v;

        public static Vector3d operator -(Vector3d v1, Vector3d v2) => v1 + (-v2);

        public static Vector3d operator /(Vector3d v, double k) => (1 / k) * v;

        #endregion Overload of operators
    }
}