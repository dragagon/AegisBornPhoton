using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using ExitGames.Logging;

namespace AegisBorn.Models.Base.Actor.Stats
{
    public class BaseStats
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        private int[] _values = new int[StatsUtil.BaseStatList.Count];

        /// <summary>
        /// Used by XML Serialization, use GetValue instead
        /// </summary>
        public int[] Values { get { return _values; } set { _values = value; } }

        public int GetValue(Stats stat)
        {
            return StatsUtil.BaseStatList.Contains(stat) ? _values[StatsUtil.BaseStatList.IndexOf(stat)] : 0;
        }

        public void IncreaseBaseStat(Stats stat, int value)
        {
            if(StatsUtil.BaseStatList.Contains(stat))
            {
                _values[StatsUtil.BaseStatList.IndexOf(stat)] += value;
            }
        }
    }

    public class StatsUtil
    {
        public static readonly List<Stats> BaseStatList = new List<Stats> { Stats.STR, Stats.AGI, Stats.VIT, Stats.INT, Stats.DEX, Stats.LUK, };
        public static readonly List<Stats> NonNegativeStatList = new List<Stats> { Stats.STR, Stats.AGI, Stats.VIT, Stats.INT, Stats.DEX, Stats.LUK, Stats.Max_HP, Stats.Max_MP };
    }

    public enum Stats
    {
        STR,
        AGI,
        VIT,
        INT,
        DEX,
        LUK,
        Max_HP,
        Max_MP,
        HP_Regen_Rate,
        MP_Regen_Rate,
    }
}
