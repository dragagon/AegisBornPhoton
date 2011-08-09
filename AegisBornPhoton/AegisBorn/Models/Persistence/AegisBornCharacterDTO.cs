using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AegisBorn.Models.Base;

namespace AegisBorn.Models.Persistence
{
    public class AegisBornCharacterDTO
    {
        public virtual int Id { get; set; }
        public virtual String Name { get; set; }
        public virtual int Level { get; set; }
        public virtual String Class { get; set; }
        public virtual String Sex { get; set; }
        public virtual float X { get; set; }
        public virtual float Y { get; set; }
        public virtual float Z { get; set; }
        public virtual int STR { get; set; }
        public virtual int AGI { get; set; }
        public virtual int VIT { get; set; }
        public virtual int INT { get; set; }
        public virtual int DEX { get; set; }
        public virtual int LUK { get; set; }
        public virtual SfGuardUser UserId { get; set; }
    }
}
