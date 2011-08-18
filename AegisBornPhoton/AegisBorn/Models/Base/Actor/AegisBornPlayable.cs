using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor
{
    /// <summary>
    /// Any set of classes that is "playable", these are players and pets.
    /// </summary>
    public abstract class AegisBornPlayable : AegisBornCharacter
    {
        private AegisBornCharacter _lockedTarget;
    }
}
