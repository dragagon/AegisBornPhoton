using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base
{
    public class Experience
    {
        public readonly static List<long> LevelExp = new List<long>
                {
                    -1,
                    0,
                    70,
                    400,
                    1200,
                    3000,
                    6300,
                };

        public static readonly int MaxLevel = 6;
    }
}
