using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AegisBorn.Models.Base.Actor.Stats
{
    public class BaseStats
    {
        private int[] _values = new int[Enum.GetNames(typeof(Stats)).Length];

        public int[] Values { get { return _values; } set { _values = value; } }
    }
}
