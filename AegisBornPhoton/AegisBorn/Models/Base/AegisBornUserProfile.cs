using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base
{
    public class AegisBornUserProfile
    {
        public virtual int Id { get; set; }
        public virtual SfGuardUser UserId { get; set; }
        public virtual int CharacterSlots { get; set; }
    }
}
