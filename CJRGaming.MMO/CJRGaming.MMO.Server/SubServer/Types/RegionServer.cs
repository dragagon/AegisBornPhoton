using System;
using CJRGaming.MMO.Server.MasterServer;
using Photon.SocketServer;

namespace CJRGaming.MMO.Server.SubServer.Types
{
    public class RegionServer : SubServer
    {
        public RegionServer()
        {
            ServerType = SubServerType.Region;
        }

        protected override void AddHanders()
        {
            
        }

        protected override void AddSubServerHandlers(IncomingSubServerToSubServerPeer SubServerPeer)
        {
            
        }

        protected override bool IsSubServerPeer(InitRequest initRequest)
        {
            return initRequest.LocalPort == 4531;
        }

    }
}
