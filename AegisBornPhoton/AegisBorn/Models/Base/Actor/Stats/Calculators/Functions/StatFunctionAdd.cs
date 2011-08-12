using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AegisBorn.Models.Base.Actor.Stats.Calculators.Statements;

namespace AegisBorn.Models.Base.Actor.Stats.Calculators.Functions
{
    public class StatFunctionAdd : StatFunction
    {
        private Statement _statement;

        public StatFunctionAdd(Stats stat, int order, Object owner, Statement statement)
            : base(stat, order, owner)
        {
            _statement = statement;
        }

        #region Overrides of StatFunction

        public override void Calc(CalculatorValue calculatorValue)
        {
            calculatorValue.Value += _statement.Calc(calculatorValue);
        }

        #endregion
    }
}
