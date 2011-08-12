using System;
using System.IO;
using System.Xml.Serialization;

namespace AegisBorn.Models.Base.Actor.Stats
{
    public class CharacterStats
    {
        private readonly AegisBornCharacter _character;
        private long _exp;
        private int _level;

        public CharacterStats(AegisBornCharacter aegisBornCharacter)
        {
            _character = aegisBornCharacter;
            BaseStats = new BaseStats();
        }

        public AegisBornCharacter Character { get { return _character; } }

        public long Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public void NewCharacter()
        {
            Level = 1;
            Exp = 0;
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
            if(calculatorValue.Value <= 0 && StatsUtil.NonNegativeStatList.Contains(stat))
            {
                calculatorValue.Value = 1;
            }

            return calculatorValue.Value;
        }

        public int GetValue(Stats stat)
        {
            return GetValue(stat, null);
        }

        public int GetValue(Stats stat, AegisBornCharacter aegisBornCharacter)
        {
            return (int)CalcStat(stat, BaseStats.GetValue(stat), aegisBornCharacter);
        }

        private BaseStats BaseStats { get; set; }

        public String BaseStatsXML
        {
            get
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(BaseStats));
                StringWriter outStream = new StringWriter();
                mySerializer.Serialize(outStream, BaseStats);
                return outStream.ToString();
            }
            set
            {
                String tempStr = value;
                XmlSerializer mySerializer = new XmlSerializer(typeof(BaseStats));
                StringReader inStream = new StringReader(tempStr);
                BaseStats = (BaseStats)mySerializer.Deserialize(inStream);
            }
        }

    }
}
