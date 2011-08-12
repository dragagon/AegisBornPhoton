using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats.Calculators.Statements
{
    public class StatementRandom : Statement
    {
        private readonly Statement _max;
        private readonly bool _linear;
        private static readonly Random Random = new Random();
        public StatementRandom(Statement max, bool linear)
        {
            _max = max;
            _linear = linear;
        }
        #region Overrides of Statement

        public override double Calc(CalculatorValue calculatorValue)
        {
            return _max.Calc(calculatorValue) * (_linear ? Random.NextDouble() : NextGaussian());
        }

        #endregion

        private double _nextNextGaussian;
        private bool _haveNextNextGaussian;

         private double NextGaussian()
         {
             if (_haveNextNextGaussian)
             {
                 _haveNextNextGaussian = false;
                 return _nextNextGaussian;
             }
             else
             {
                 double v1, v2, s;
                 do
                 {
                     v1 = 2*Random.NextDouble() - 1; // between -1.0 and 1.0
                     v2 = 2*Random.NextDouble() - 1; // between -1.0 and 1.0
                     s = v1*v1 + v2*v2;
                 } while (s >= 1 || s == 0);
                 double multiplier = Math.Sqrt(-2*Math.Log(s)/s);
                 _nextNextGaussian = v2*multiplier;
                 _haveNextNextGaussian = true;
                 return v1*multiplier;
             }
         }
    }
}
