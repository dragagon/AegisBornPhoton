using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AegisBorn.Models.Base;

namespace AegisBorn
{
    public class Util
    {
        public static bool IsInRadius(int radius, AegisBornObject aegisBornObject1, AegisBornObject aegisBornObject2, bool use3D)
        {
            if (aegisBornObject1 == null || aegisBornObject2 == null)
            {
                return false;
            }
            if (radius == -1)
            {
                return true;
            }

            // in 3d software packages, Y is up, not Z, so when dealing with planar functions, Y is the odd-ball out.
            var dx = aegisBornObject1.X - aegisBornObject2.X;
            var dz = aegisBornObject1.Z - aegisBornObject2.Z;
            var dy = use3D ? aegisBornObject1.Y - aegisBornObject2.Y : 0;

            return dx * dx + dy * dy + dz * dz <= radius * radius;
        }
    }
}
