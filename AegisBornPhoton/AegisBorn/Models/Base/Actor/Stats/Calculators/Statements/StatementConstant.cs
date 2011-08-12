using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats.Calculators.Statements
{
    public class StatementConstant : Statement
    {
        private readonly double _value;

        public StatementConstant(double value)
        {
            _value = value;
        }
        #region Overrides of Statement

        public override double Calc(CalculatorValue calculatorValue)
        {
            return _value;
        }

        #endregion
    }
}
