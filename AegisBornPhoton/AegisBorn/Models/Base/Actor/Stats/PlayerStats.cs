using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AegisBorn.Models.Base.Actor.Stats.Calculators;

namespace AegisBorn.Models.Base.Actor.Stats
{
    public class PlayerStats : PlayableStats
    {
        private readonly AegisBornPlayer _character;
        public PlayerStats(AegisBornPlayer aegisBornCharacter)
        {
            _character = aegisBornCharacter;
            // Slick way of quickly creating an array of auto-initialized Calculators, one for each Stat.
            Calculators = new Calculator[Enum.GetNames(typeof(Stats)).Length];
            for (int i = 0; i < Calculators.Length; i++)
            {
                Calculators[i] = new Calculator();
            }

            Formulas.AddCalculatorsToNewCharacter(this);
        }

        public override bool AddExp(long addToExp)
        {
            if(!base.AddExp(addToExp))
            {
                return false;
            }

            // Send an event to the player letting them know of xp update and possible level update with stats updated.
            //_character.SendEvent();
            return true;
        }

        public override long GetExpForLevel(int value)
        {
            return Experience.LevelExp[value];
        }

        public override bool AddLevel(int level)
        {
		    if (Level + level > Experience.MaxLevel - 1)
			    return false;
		
		    bool levelIncreased = base.AddLevel(level);
		
		    if (levelIncreased)
		    {
                // Broadcast to all around that the player leveled up.
                //_character.BroadcastEvent(new SocialAction(_character, SocialAction.LEVEL_UP));
                // Send ourselves a message that we leveled up.
                //_character.SendEvent(SystemMessage.getSystemMessage(SystemMessageId.YourLevelIncreased));
                // Reward the character for their level up
                // _character.Reward();
            }
		    else
		    {
                // Delevel the player, remove skills, points, etc with losing levels
		    }

            // If we are in a party recalculate the party level
            //if (_character.isInParty())
            //    _character.Party.recalculatePartyLevel(); // Recalculate the party level
		
		    // Send all our updated stats to the player
            //_character.SendEvent(new UserInfo(Character));
		
		    return levelIncreased;
        }

        public override AegisBornCharacter Character
        {
            get { return _character; }
        }
    }
}
