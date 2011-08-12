using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using AegisBorn.Models.Base.Actor.Stats;

namespace AegisBorn.Models.Base.Actor
{
    public abstract class AegisBornCharacter : AegisBornObject, IAegisBornCharacter
    {
        public SfGuardUser UserId { get; set; }
        public string Class { get; set; }
        public string Sex { get; set; }

        public Calculator[] Calculators { get; set; }

        protected AegisBornCharacter()
        {
            Stats = new CharacterStats(this);
        }

        public Hashtable GetHashtable()
        {

            var character = new AegisBornCommon.Models.AegisBornCharacter
                                {
                                    Id = Id,
                                    Name = Name,
                                    Sex = Sex,
                                    Class = Class,
                                    Level = Stats.Level,
                                    X = X,
                                    Y = Y,
                                    Z = Z
                                };

            return character.GetHashtable();
        }

        public AegisBornCharacter Target { get; set; }

        public List<AegisBornCharacter> AttackerList { get; set; }

        public CharacterStats Stats{ get; set; }
    }
}
