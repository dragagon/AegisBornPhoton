using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AegisBorn.Models.Base.Actor.Stats.Calculators.Functions;
using AegisBorn.Models.Base.Actor.Stats.Calculators.Statements;
using ExitGames.Logging;

namespace AegisBorn.Models.Base.Actor.Stats.Calculators
{
    public class Formulas
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public static void AddCalculatorsToNewCharacter(CharacterStats stats)
        {
            Log.Debug("Adding calculators");
            stats.AddStatFunction(MaxHPFunction.Instance);
        }

        public class MaxHPFunction : StatFunction
        {

            private static MaxHPFunction _instance;

            public static StatFunction Instance
            {
                get { return _instance ?? (_instance = new MaxHPFunction()); }
            }

            // Slot 0x10 is slot 16, this leaves 15 slots for functions that can happen before it.
            private MaxHPFunction()
                : base(Stats.Max_HP, 0x10, null)
            {
            }

            #region Overrides of StatFunction

            public override void Calc(CalculatorValue calculatorValue)
            {
                double levelMult = 5 * (calculatorValue.Player == null ? 1 : calculatorValue.Player.Stats.Level);
                double vitDiv = (calculatorValue.Player == null ? 1 : calculatorValue.Player.Stats.GetValue(Stats.VIT)) / 10.0;
                calculatorValue.Value += 40 + levelMult + vitDiv;
            }

            #endregion
        }
    }
}
