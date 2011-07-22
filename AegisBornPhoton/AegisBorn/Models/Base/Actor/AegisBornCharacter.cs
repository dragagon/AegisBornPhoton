using System.Collections;

namespace AegisBorn.Models.Base.Actor
{
    public abstract class AegisBornCharacter : AegisBornObject, IAegisBornCharacter
    {
        public virtual SfGuardUser UserId { get; set; }
        public int Level { get; set; }
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
                                    Level = Level,
                                    X = X,
                                    Y = Y,
                                    Z = Z
                                };

            return character.GetHashtable();
        }

        public System.Collections.Generic.List<AegisBornSkill> Skills
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public System.Collections.Generic.List<AegisBornEffect> Effects
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public AegisBornInventory Inventory
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public AegisBornCharacter Target
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public bool AllSkillsDisabled
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public System.Collections.Generic.Dictionary<SkillFamily, long> DisabledSkills
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public System.Collections.Generic.List<AegisBornCharacter> AttackerList
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public bool Mortal
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public CharacterStats Stats
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public CharacterStatus Status
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
