using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
