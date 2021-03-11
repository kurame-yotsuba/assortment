using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assortment.KurameMath
{
    public readonly struct Matrix2x2
    {
        public readonly Vector2d V1;
        public readonly Vector2d V2;

        public Matrix2x2(Vector2d v1, Vector2d v2) => (this.V1, this.V2) = (v1, v2);
        public Matrix2x2(double a11, double a12, double a21, double a22)
            : this(new(a11, a21), new(a12, a22)) { }

        public Matrix2x2 Transpose() => new(V1.X, V1.Y, V2.X, V2.Y);

        public double Det() => V1.X * V2.Y - V1.Y * V2.X;
        public double Trace() => V1.X + V2.Y;

        public Matrix2x2 Inverse() => (1 / Det()) * new Matrix2x2(V2.Y, -V2.X, -V1.Y, V1.X);

        #region Overload of operators

        public static Matrix2x2 operator +(Matrix2x2 m1, Matrix2x2 m2)
            => new(m1.V1 + m2.V1, m1.V2 + m2.V2);

        public static Matrix2x2 operator *(double k, Matrix2x2 m)
            => new(k * m.V1, k * m.V2);

        public static Matrix2x2 operator *(Matrix2x2 m1, Matrix2x2 m2)
        {
            var m1T = m1.Transpose();
            return new(
                m1T.V1.Dot(m2.V1), m1T.V1.Dot(m2.V2),
                m1T.V2.Dot(m2.V1), m1T.V2.Dot(m2.V2));
        }

        public static Matrix2x2 operator -(Matrix2x2 m) => -1 * m;
        public static Matrix2x2 operator -(Matrix2x2 m1, Matrix2x2 m2) => m1 + (-m2);

        #endregion
    }
}
