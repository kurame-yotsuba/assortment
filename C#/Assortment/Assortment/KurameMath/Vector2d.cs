using System;

namespace Assortment.KurameMath
{
    public readonly struct Vector2d
    {
        public readonly double X;
        public readonly double Y;

        public Vector2d(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Abs() => Math.Sqrt(X * X + Y * Y);

        public Vector2d Normalize() => this / Abs();

        public double Dot(Vector2d v) => X * v.X + Y * v.Y;

        public Vector2d Rot(double theta)
        {
            var (cos, sin) = (Math.Cos(theta), Math.Sin(theta));
            double x = cos * X - sin * Y;
            double y = sin * X + cos * Y;
            return new(x, y);
        }

        #region Overload of operators

        public static Vector2d operator +(Vector2d v1, Vector2d v2)
            => new(v1.X + v2.X, v1.Y + v2.Y);

        public static Vector2d operator *(double k, Vector2d v)
            => new(k * v.X, k * v.Y);

        public static Vector2d operator *(Vector2d v, double k) => k * v;

        public static Vector2d operator -(Vector2d v) => -1 * v;

        public static Vector2d operator -(Vector2d v1, Vector2d v2) => v1 + (-v2);

        public static Vector2d operator /(Vector2d v, double k) => (1 / k) * v;

        #endregion Overload of operators
    }
}