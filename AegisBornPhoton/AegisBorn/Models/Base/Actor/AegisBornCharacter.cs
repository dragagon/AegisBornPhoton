using System;
using System.Collections;
using System.Collections.Generic;
using AegisBorn.Models.Base.Actor.Stats;

namespace AegisBorn.Models.Base.Actor
{
    public abstract class AegisBornCharacter : AegisBornObject, IAegisBornCharacter
    {
        public virtual SfGuardUser UserId { get; set; }
        public virtual int Level { get; set; }
        public virtual string Class { get; set; }
        public virtual string Sex { get; set; }

        public Calculator[] Calculators { get; set; }

        public Hashtable GetHashtable()
        {

            var character = new AegisBornCommon.Models.AegisBornCharacter
                                {
                                    Id = Id,
                                    Name = Name,
                                    Sex = Sex,
                                    Class = Class,
                                    Level = Level,
                                    X = X,
                                    Y = Y,
                                    Z = Z
                                };

            return character.GetHashtable();
        }


        public AegisBornCharacter Target { get; set; }

        public List<AegisBornCharacter> AttackerList { get; set; }

        public CharacterStats Stats{ get; set; }

        public virtual int[] BaseStats { get; set; }
    }
}
