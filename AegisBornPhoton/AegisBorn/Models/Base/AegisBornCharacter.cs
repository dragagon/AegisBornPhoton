using System.Collections;

namespace AegisBorn.Models.Base
{
    public class AegisBornCharacter
    {
        public virtual int Id { get; set; }
        public virtual SfGuardUser UserId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Sex { get; set; }
        public virtual string Class { get; set; }
        public virtual int Level { get; set; }
        public virtual int PositionX { get; set; }
        public virtual int PositionY { get; set; }

        public virtual AegisBornCommon.Models.AegisBornCharacter GetFullPlayer()
        {

            var character = new AegisBornCommon.Models.AegisBornCharacter
                                {
                                    Id = Id,
                                    Name = Name,
                                    Sex = Sex,
                                    Class = Class,
                                    Level = Level,
                                    PositionX = PositionX,
                                    PositionY = PositionY
                                };

            return character;
        }
    }
}
