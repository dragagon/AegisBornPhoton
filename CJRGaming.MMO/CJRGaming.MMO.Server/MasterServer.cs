using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Core;

namespace CJRGaming.MMO.Server
{
    using ExitGames.Logging;
    using ExitGames.Logging.Log4Net;

    using log4net;
    using log4net.Config;

    using Photon.SocketServer;
    using Photon.SocketServer.ServerToServer;

    using LogManager = ExitGames.Logging.LogManager;

    public class MasterServer : NodeResolverBase
    {
        #region Constants and Fields

        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Overrides of ApplicationBase

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            throw new NotImplementedException();
        }

        protected override void Setup()
        {
            throw new NotImplementedException();
        }

        protected override void TearDown()
        {
        }

        #endregion

        #region Overrides of NodeResolverBase

        protected override void OnNodeConnected(byte nodeId, int port)
        {
            // at this point the node is connected and can be routed to
            if (log.IsDebugEnabled)
            {
                log.DebugFormat("Node {0} connected on port {1}", nodeId, port);
            }
        }

        protected override void OnNodeDisconnected(byte nodeId, int port)
        {
            // at this point the node is disconnected and can NOT be routed to
            if (log.IsDebugEnabled)
            {
                log.DebugFormat("Node {0} disconnected from port {1}", nodeId, port);
            }
        }

        #endregion
    }
}
