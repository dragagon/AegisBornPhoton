using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AegisBorn.Models.Base.Actor;
using AegisBorn.Models.Base.Actor.Transform;

namespace AegisBorn.Models.Base
{
    public interface IAegisBornObject
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
