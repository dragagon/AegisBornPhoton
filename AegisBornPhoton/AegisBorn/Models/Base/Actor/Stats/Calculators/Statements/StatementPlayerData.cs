using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats.Calculators.Statements
{
    public class StatementPlayerData : Statement
    {
        private readonly PlayerData _stat;

        public StatementPlayerData(PlayerData stat)
        {
            _stat = stat;
        }

        #region Overrides of Statement

        public override double Calc(CalculatorValue calculatorValue)
        {
            switch(_stat)
            {
                case PlayerData.PlayerLevel:
                    return calculatorValue.Player == null ? 1 : calculatorValue.Player.Stats.Level;
                case PlayerData.TargetLevel:
                    return calculatorValue.Target == null ? 1 : calculatorValue.Target.Stats.Level;
            }
            return 0;
        }

        #endregion
    }

    public enum PlayerData
    {
        PlayerLevel,
        TargetLevel,
    }
}
