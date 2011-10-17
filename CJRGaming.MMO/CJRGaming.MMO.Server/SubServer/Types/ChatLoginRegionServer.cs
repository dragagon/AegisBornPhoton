using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Server.MasterServer;
using Photon.SocketServer;

namespace CJRGaming.MMO.Server.SubServer.Types
{
    public class ChatLoginRegionServer : SubServer
    {
        public ChatLoginRegionServer()
        {
            ServerType = SubServerType.Chat | SubServerType.Login | SubServerType.Region;
        }

        #region Overrides of SubServer

        public override void AddHandlers()
        {
            
        }

        #endregion

        #region Overrides of ApplicationBase

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
