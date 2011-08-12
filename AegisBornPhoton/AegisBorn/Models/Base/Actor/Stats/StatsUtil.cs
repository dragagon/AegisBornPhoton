using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats
{
    public class StatsUtil
    {
        /// <summary>
        /// Manually set list of stats that when calculated can never return 0 they always return 1
        /// </summary>
        public static readonly List<Stats> NonNegativeStatsList = new List<Stats> { Stats.STR, Stats.AGI, Stats.VIT, Stats.INT, Stats.DEX, Stats.LUK, Stats.HP_Max, Stats.MP_Max, };
        /// <summary>
        /// Manually set list of stats that are the basic stats. Assumed these can be incremented.
        /// </summary>
        public static readonly List<Stats> BaseStatsList = new List<Stats> { Stats.STR, Stats.AGI, Stats.VIT, Stats.INT, Stats.DEX, Stats.LUK, };
    }
    
    public enum Stats
    {
        STR, AGI, VIT, INT, DEX, LUK,           // Base Stats
        HP_Max, HP_Regen, MP_Max, MP_Regen,     // Health and Mana
        HP_Current, MP_Current,                  // Current values
    }
}
