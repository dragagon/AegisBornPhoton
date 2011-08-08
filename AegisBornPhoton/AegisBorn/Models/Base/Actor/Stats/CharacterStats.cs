using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats
{
    public class CharacterStats
    {
        private AegisBornCharacter _character;
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

        public int STR
        {
            get
            {
                if (_character == null)
                    return 1;
                return (int) CalcStat(Stats.STR, _character.BaseStats.STR, null);
            }
        }
    }
}
