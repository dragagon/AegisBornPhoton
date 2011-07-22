using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor
{
    public interface IAegisBornCharacter
    {
        List<AegisBornSkill> Skills { get; set; }
        List<AegisBornEffect> Effects { get; set; }
        AegisBornInventory Inventory { get; set; }
        AegisBornCharacter Target { get; set; }
        bool AllSkillsDisabled { get; set; }
        Dictionary<SkillFamily, long> DisabledSkills { get; set; }
        List<AegisBornCharacter> AttackerList { get; set; }
        bool Mortal { get; set; }
        CharacterStats Stats { get; set; }
        CharacterStatus Status { get; set; }

    }
}
