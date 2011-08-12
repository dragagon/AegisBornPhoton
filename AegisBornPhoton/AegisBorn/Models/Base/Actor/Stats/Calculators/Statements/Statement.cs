using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats.Calculators.Statements
{
    public abstract class Statement
    {
        public abstract double Calc(CalculatorValue calculatorValue);
    }
}
