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

        public void AddExp(long addToExp)
        {
            
        }
    }
}
