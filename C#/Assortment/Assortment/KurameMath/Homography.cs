using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assortment.KurameMath
{
    public class Homography
    {
        private readonly double a, b, c, d, e, f, g, h;
        public Homography(Vector3d v1, Vector3d v2, Vector3d v3, Vector3d v4)
        {
            double hTop = (v1.X - v2.X) * (v3.Y - v4.Y) - (v3.X - v4.X) * (v1.Y - v2.Y);
            double hBottom = (v1.X - v2.X) * (v2.Y - v3.Y) - (v2.X - v3.X) * (v1.Y - v2.Y);

            h = hTop / hBottom;

            double gTop = -v1.X + v2.X - v3.X + v4.X + (v2.X - v3.X) * h;
            double gBottom = v1.X - v2.X;

            g = gTop / gBottom;

            a = (g + 1) * v1.X - v4.X;
            d = (g + 1) * v1.Y - v4.Y;

            b = (h + 1) * v3.X - v4.X;
            e = (h + 1) * v3.Y - v4.Y;

            c = v4.X;
            f = v4.Y;
        }

        public Vector2d Transform(Vector3d v)
        {
            double focusPlane = (g * v.X + h * v.Y + 1);
            double x = (a * v.X + b * v.Y + c * v.Z) / focusPlane;
            double y = (d * v.X + e * v.Y + f * v.Z) / focusPlane;

            return new(x, y);
        }
    }
}
