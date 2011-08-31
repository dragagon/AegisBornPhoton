using System;
using System.IO;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;

namespace CJRGaming.MMO.Server
{
    using ExitGames.Logging;
    using Photon.SocketServer;
    using Photon.SocketServer.ServerToServer;

    using LogManager = ExitGames.Logging.LogManager;

    public class MasterServer : NodeResolverBase
    {
        #region Constants and Fields

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Overrides of ApplicationBase

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            throw new NotImplementedException();
        }

        protected override void Setup()
        {
            LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["LogFileName"] = "MS" + this.ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));

            Initialize();
        }

        protected override void TearDown()
        {
        }

        #endregion

        #region Overrides of NodeResolverBase

        protected override void OnNodeConnected(byte nodeId, int port)
        {
            // at this point the node is connected and can be routed to
            if (Log.IsDebugEnabled)
            {
                Log.DebugFormat("Node {0} connected on port {1}", nodeId, port);
            }
        }

        protected override void OnNodeDisconnected(byte nodeId, int port)
        {
            // at this point the node is disconnected and can NOT be routed to
            if (Log.IsDebugEnabled)
            {
                Log.DebugFormat("Node {0} disconnected from port {1}", nodeId, port);
            }
        }

        #endregion

        /// <summary>
        /// Initialize our server
        /// </summary>
        public void Initialize()
        {
            
        }
    }
}
