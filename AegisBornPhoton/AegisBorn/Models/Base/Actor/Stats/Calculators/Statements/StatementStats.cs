using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats.Calculators.Statements
{
    public class StatementStats : Statement
    {
        private readonly Stats _stat;

        public StatementStats(Stats stat)
        {
            _stat = stat;
        }
        #region Overrides of Statement

        public override double Calc(CalculatorValue calculatorValue)
        {
            return calculatorValue.Player == null ? 1 : calculatorValue.Player.Stats.GetValue(_stat);
        }

        #endregion
    }
}
