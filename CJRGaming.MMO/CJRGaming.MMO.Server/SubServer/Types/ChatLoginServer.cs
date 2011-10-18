using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Server.MasterServer;
using Photon.SocketServer;

namespace CJRGaming.MMO.Server.SubServer.Types
{
    public class ChatLoginServer : SubServer
    {
        public  ChatLoginServer()
        {
            ServerType = SubServerType.Chat | SubServerType.Login;
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
