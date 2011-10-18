using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Server.MasterServer;
using Photon.SocketServer;

namespace CJRGaming.MMO.Server.SubServer.Types
{
    public class ChatServer : SubServer
    {
        public ChatServer()
        {
            ServerType = SubServerType.Chat;
        }

        #region Overrides of SubServer

        protected override void AddHanders()
        {
            
        }

        protected override void AddSubServerHandlers(IncomingSubServerToSubServerPeer SubServerPeer)
        {
            
        }

        #endregion
    }
}
