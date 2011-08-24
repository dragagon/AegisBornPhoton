using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats
{
    public abstract class PlayableStats : CharacterStats
    {
        public virtual bool AddExp(long addToExp)
        {
            if ((Exp + addToExp) < 0 || (addToExp > 0 && Exp == (GetExpForLevel(GetMaxLevel()) - 1)))
			    return true;
		
		    if (Exp + addToExp >= GetExpForLevel(GetMaxLevel()))
			    addToExp = GetExpForLevel(GetMaxLevel()) - 1 - Exp;
		
		    Exp += addToExp;
		
		    int minimumLevel = 1;
            //if (Character is AegisBornPet)
            //{
            //    // get minimum level for pet
            //    minimumLevel = PetMinLevelLookup;
            //}

            int level = minimumLevel; // minimum level

            for (int tmp = level; tmp <= GetMaxLevel(); tmp++)
		    {
			    if (Exp >= GetExpForLevel(tmp))
				    continue;
			    level = --tmp;
			    break;
		    }
		    if (level != Level && level >= minimumLevel)
			    AddLevel((level - Level));
		
		    return true;
        }

        public virtual long GetExpForLevel(int level) { return level; }

        public int GetMaxLevel()
        {
            return Experience.MaxLevel;
        }

        public virtual bool AddLevel(int value)
        {
		    if (Level + value > GetMaxLevel() - 1)
		    {
			    if (Level < GetMaxLevel() - 1)
				    value = (GetMaxLevel() - 1 - Level);
			    else
				    return false;
		    }
		
		    bool levelIncreased = (Level + value > Level);
            Level += value;

            // If exp is higher than the xp for the next level or the current xp is lower than the xp necessary for that level
            // Set the xp to that level. This forces the level set even if they do not meet the xp requirements or if they go so far
            // over that they reach the next level - GM forced level down.
		    if (Exp >= GetExpForLevel(Level + 1) || GetExpForLevel(Level) > Exp)
		    {
		        Exp = GetExpForLevel(Level);
		    }
		
		    if (!levelIncreased) return false;

            // Reset the player's health and mana to full.
            //Character.Status.setCurrentHp(Character.Stats.GetValue(Stats.Max_HP));
            //Character.Status.setCurrentMp(Character.Stats.GetValue(Stats.Max_MP));
		
		    return true;
        }
    }
}
