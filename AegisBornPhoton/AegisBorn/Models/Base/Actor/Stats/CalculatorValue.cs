using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats
{
    public class CalculatorValue
    {
        public AegisBornCharacter Player { get; set; }
        public AegisBornCharacter Target { get; set; }
        public double Value { get; set; }
    }
}
