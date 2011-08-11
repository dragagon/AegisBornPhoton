using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats
{
    public class CharacterStats
    {
        private readonly AegisBornCharacter _character;
        private long _exp;
        private byte _level;

        public CharacterStats(AegisBornCharacter aegisBornCharacter)
        {
            _character = aegisBornCharacter;
        }

        public AegisBornCharacter Character { get { return _character; } }

        public long Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }

        public byte Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public double CalcStat(Stats stat, double initialValue, AegisBornCharacter target)
        {
            if(_character == null)
            {
                return initialValue;
            }

            Calculator c = _character.Calculators[(int)stat];

            if(c.Size == 0)
            {
                return initialValue;
            }

            var calculatorValue = new CalculatorValue {Player = _character, Target = target, Value = initialValue};

            c.Calc(calculatorValue);

            // Ensure certain stats do not drop below 1 no matter what debuffs are applied
            // Find a better way to do this than to switch on a list of Stats.
            if(calculatorValue.Value <= 0)
            {
                switch (stat)
                {
                    case Stats.Max_HP:
                    case Stats.Max_MP:
                    case Stats.STR:
                    case Stats.AGI:
                    case Stats.VIT:
                    case Stats.INT:
                    case Stats.DEX:
                    case Stats.LUK:
                        calculatorValue.Value = 1;
                        break;
                }
            }

            return calculatorValue.Value;
        }

        public int GetValue(Stats stat, int defaultValue, AegisBornCharacter aegisBornCharacter)
        {
            if(_character == null)
            {
                return defaultValue;
            }
            return (int)CalcStat(stat, _character.BaseStats.Values[(int)stat], aegisBornCharacter);
        }
    }
}
