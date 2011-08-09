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
                return (int) CalcStat(Stats.STR, _character.BaseStats[(int)Stats.STR], null);
            }
        }

        public int AGI
        {
            get
            {
                if (_character == null)
                    return 1;
                return (int)CalcStat(Stats.AGI, _character.BaseStats[(int)Stats.AGI], null);
            }
        }

        public int VIT
        {
            get
            {
                if (_character == null)
                    return 1;
                return (int)CalcStat(Stats.VIT, _character.BaseStats[(int)Stats.VIT], null);
            }
        }

        public int INT
        {
            get
            {
                if (_character == null)
                    return 1;
                return (int)CalcStat(Stats.INT, _character.BaseStats[(int)Stats.INT], null);
            }
        }

        public int DEX
        {
            get
            {
                if (_character == null)
                    return 1;
                return (int)CalcStat(Stats.DEX, _character.BaseStats[(int)Stats.DEX], null);
            }
        }

        public int LUK
        {
            get
            {
                if (_character == null)
                    return 1;
                return (int)CalcStat(Stats.LUK, _character.BaseStats[(int)Stats.LUK], null);
            }
        }
    }
}
