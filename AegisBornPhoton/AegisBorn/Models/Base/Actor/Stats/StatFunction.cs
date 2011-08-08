using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats
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
