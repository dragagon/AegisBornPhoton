using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AegisBorn.Models.Base.Actor
{
    /// <summary>
    ///  This is a real player on a real client, here we can send packets back to the user when things happen.
    /// </summary>
    public class AegisBornPlayer : AegisBornPlayable
    {
        public void Logout()
        {
            // kick the player out of the game.
        }

        public bool Teleporting { get; set; }
    }
}
