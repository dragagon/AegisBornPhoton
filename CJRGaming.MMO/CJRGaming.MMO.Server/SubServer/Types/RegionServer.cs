using System;
using CJRGaming.MMO.Server.MasterServer;
using Photon.SocketServer;

namespace CJRGaming.MMO.Server.SubServer.Types
{
    public class RegionServer : SubServer
    {
        private static int IncomingPort = 4521;
        public RegionServer()
        {
            ServerType = SubServerType.Region;
            _acceptsSubServerConnections = true;
        }

        #region Overrides of SubServer

        public override void AddHandlers()
        {
        }

        #endregion

        protected override bool IsSubServerPeer(InitRequest initRequest)
        {
            return initRequest.LocalPort == IncomingPort;
        }

    }
}
