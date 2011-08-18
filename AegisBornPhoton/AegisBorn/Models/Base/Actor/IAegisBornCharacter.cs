using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AegisBorn.Models.Base.Actor.Stats;

namespace AegisBorn.Models.Base.Actor
{
    public interface IAegisBornCharacter
    {
        AegisBornCharacter Target { get; set; }
        List<AegisBornCharacter> AttackerList { get; set; }
        CharacterStats Stats { get; }
        //CharacterStatus Status { get; set; }
        //List<AegisBornSkill> Skills { get; set; }
        //List<AegisBornEffect> Effects { get; set; }
        //AegisBornInventory Inventory { get; set; }
        //Dictionary<SkillFamily, long> DisabledSkills { get; set; }
        //bool AllSkillsDisabled { get; set; }
    }
}
