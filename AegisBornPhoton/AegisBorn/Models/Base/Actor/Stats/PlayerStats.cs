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

        public override bool AddExp(long addToExp)
        {
            AegisBornPlayer player = Character as AegisBornPlayer;
            if(!base.AddExp(addToExp))
            {
                return false;
            }

            // Send an event to the player letting them know of xp update and possible level update with stats updated.
            //player.Peer.PublishEvent();
            return true;
        }

        public override long GetExpForLevel(int value)
        {
            return Experience.LevelExp[value];
        }
    }
}
