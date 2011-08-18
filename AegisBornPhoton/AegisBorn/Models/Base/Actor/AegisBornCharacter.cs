using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using AegisBorn.Models.Base.Actor.Stats;
using AegisBorn.Models.Base.Actor.Stats.Calculators;
using AegisBorn.Models.Persistence;

namespace AegisBorn.Models.Base.Actor
{
    public abstract class AegisBornCharacter : AegisBornObject, IAegisBornCharacter
    {
        public SfGuardUser UserId { get; set; }
        public string Class { get; set; }
        public string Sex { get; set; }

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

        public abstract CharacterStats Stats{ get; }
    }
}
