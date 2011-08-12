using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AegisBorn.Models.Base.Actor.Stats.Calculators.Functions;

namespace AegisBorn.Models.Base.Actor.Stats.Calculators.Statements
{
    public class StatementFunctions : Statement
    {

        public List<StatFunction> StatFunctions { get; set; }

        public StatementFunctions()
        {
            StatFunctions = new List<StatFunction>();
        }

        #region Overrides of Statement

        public override double Calc(CalculatorValue calculatorValue)
        {
            // store the current value
            double preValue = calculatorValue.Value;

            // reuse the current Calculator Value
            calculatorValue.Value = 0;
            foreach(StatFunction statFunction in StatFunctions)
            {
                statFunction.Calc(calculatorValue);
            }

            // Store the answer to return
            double retValue = calculatorValue.Value;
            calculatorValue.Value = preValue;
            return retValue;
        }

        #endregion
    }
}
