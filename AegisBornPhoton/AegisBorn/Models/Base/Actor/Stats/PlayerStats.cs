using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor.Stats
{
    public class PlayerStats : PlayableStats
    {
        public PlayerStats(AegisBornPlayer aegisBornCharacter) : base(aegisBornCharacter)
        {
        }

        public void AddExp(long addToExp)
        {
            AegisBornPlayer player = Character as AegisBornPlayer;
            base.AddExp(addToExp);
        }
    }
}
