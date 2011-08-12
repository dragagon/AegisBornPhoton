using System;

namespace AegisBorn.Models.Base.Actor.Stats.Calculators.Functions
{
    public abstract class StatFunction
    {
        public Stats Stat { get; protected set; }

        public int Order { get; protected set; }

        public Object FunctionOwner { get; protected set; }

        protected StatFunction(Stats stat, int order, Object owner)
        {
            Stat = stat;
            Order = order;
            FunctionOwner = owner;
        }

        public abstract void Calc(CalculatorValue calculatorValue);
    }
}
