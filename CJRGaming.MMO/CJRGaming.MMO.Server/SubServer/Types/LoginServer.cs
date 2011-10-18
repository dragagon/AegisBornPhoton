using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Common;
using CJRGaming.MMO.Server.MasterServer;
using CJRGaming.MMO.Server.SubServer.Handlers;
using Photon.SocketServer;

namespace CJRGaming.MMO.Server.SubServer.Types
{
    public class LoginServer : SubServer
    {
        public LoginServer()
        {
            ServerType = SubServerType.Login;
        }

        #region Overrides of SubServer

        // Add handlers for the MasterPeer here.
        protected override void AddHanders()
        {
            MasterPeer.RequestHandlers.Add((byte)OperationSubCode.Login, new SubServerLoginHandler(MasterPeer));
        }

        // Add handlers for communication between sub servers - not used yet.
        protected override void AddSubServerHandlers(IncomingSubServerToSubServerPeer SubServerPeer)
        {
            
        }

        #endregion
    }
}
