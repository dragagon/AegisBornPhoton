using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats
{
    public class PlayableStats : CharacterStats
    {
        public PlayableStats(AegisBornPlayable aegisBornCharacter) : base(aegisBornCharacter)
        {
        }

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

        public bool AddLevel(int value)
        {
            throw new NotImplementedException();
        }
    }
}
