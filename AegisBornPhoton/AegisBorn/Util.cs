using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AegisBorn.Models.Base;

namespace AegisBorn
{
    public class Util
    {
        public static bool IsInRadius(int radius, AegisBornObject aegisBornObject1, AegisBornObject aegisBornObject2, bool useZ)
        {
            if (aegisBornObject1 == null || aegisBornObject2 == null)
            {
                return false;
            }
            if (radius == -1)
            {
                return true;
            }

            var dx = aegisBornObject1.X - aegisBornObject2.X;
            var dy = aegisBornObject1.Y - aegisBornObject2.Y;
            var dz = useZ ? aegisBornObject1.Z - aegisBornObject2.Z : 0;

            return dx * dx + dy * dy + dz * dz <= radius * radius;
        }
    }
}
