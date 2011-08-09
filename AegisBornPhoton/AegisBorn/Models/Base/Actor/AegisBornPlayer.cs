using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon.SocketServer.Rpc;

namespace AegisBorn.Models.Base.Actor
{
    /// <summary>
    ///  This is a real player on a real client, here we can send packets back to the user when things happen.
    /// </summary>
    public class AegisBornPlayer : AegisBornPlayable
    {
        private int[] _baseStats = new int[Enum.GetNames(typeof (Stats.Stats)).Length];

        public override int[] BaseStats
        {
            get
            {
                return _baseStats;
            }
            set
            {
                _baseStats = value;
            }
        }

        public Peer Peer { get; set; }
        public void Logout()
        {
            // kick the player out of the game.
        }

        public bool Teleporting { get; set; }
    }
}
