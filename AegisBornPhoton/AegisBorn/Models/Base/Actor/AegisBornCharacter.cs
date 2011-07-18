using System.Collections;

namespace AegisBorn.Models.Base.Actor
{
    public class AegisBornCharacter : AegisBornObject
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
    }
}
