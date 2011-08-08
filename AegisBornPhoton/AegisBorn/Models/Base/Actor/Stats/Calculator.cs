using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats
{
    public class Calculator
    {
        private static SortedList<int, StatFunction> _emptyFunctions = new SortedList<int, StatFunction>(0);

        private SortedList<int, StatFunction> _functions;

        public Calculator()
        {
            _functions = _emptyFunctions;
        }

        public Calculator(Calculator c)
        {
            _functions = c._functions;
        }

        public int Size
        {
            get { return _functions.Count(); }
        }

        public void Add(StatFunction statFunction)
        {
            _functions.Add(statFunction.Order, statFunction);
        }

        private void Remove(StatFunction statFunction)
        {
            _functions.RemoveAt(_functions.IndexOfValue(statFunction));
        }

        public void Calc(CalculatorValue calculatorValue)
        {
            foreach (var statFunction in _functions)
            {
                statFunction.Value.Calc(calculatorValue);
            }
        }

        public List<Stats> RemoveOwner(Object owner)
        {
            var modifiedStats = new List<Stats>();

            foreach (var statFunction in _functions.Where(statFunction => statFunction.Value.FunctionOwner == owner))
            {
                modifiedStats.Add(statFunction.Value.Stat);
                Remove(statFunction.Value);
            }
            return modifiedStats;
        }
    }
}
