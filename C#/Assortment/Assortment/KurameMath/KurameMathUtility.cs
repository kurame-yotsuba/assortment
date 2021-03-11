using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assortment.KurameMath
{
    public static class KurameMathUtility
    {
        public static double ToRad(this double degree) => degree * Math.PI / 180;

        public static double ToDeg(this double radian) => radian * 180 / Math.PI;
    }
}
