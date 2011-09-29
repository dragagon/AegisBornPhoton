using System;
using System.Collections.Generic;
using System.IO;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;

namespace CJRGaming.MMO.Server.MasterServer
{
    using ExitGames.Logging;
    using Photon.SocketServer;
    using Photon.SocketServer.ServerToServer;

    using LogManager = ExitGames.Logging.LogManager;

    public class MasterServer : NodeResolverBase
    {
        public SubServerCollection SubServers { get; protected set; }
        public Dictionary<Guid, Unity3dPeer> ConnectedClients { get; private set; }

        #region Constants and Fields

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Overrides of ApplicationBase

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            if (IsGameServerPeer(initRequest))
            {
                if (Log.IsDebugEnabled)
                {
                    Log.DebugFormat("Received init request from sub server");
                }

                return new IncomingSubServerPeer(initRequest, this);
            }
            Log.DebugFormat("Received init request from client");
            return new Unity3dPeer(initRequest, this);
        }

        protected override void Setup()
        {
            LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["LogFileName"] = "MS" + ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(BinaryPath, "log4net.config")));

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
            SubServers = new SubServerCollection();
            ConnectedClients = new Dictionary<Guid, Unity3dPeer>();
        }

        protected virtual bool IsGameServerPeer(InitRequest initRequest)
        {
            return initRequest.LocalPort == MasterServerSettings.Default.IncomingSubServerPort;
        }

    }
}
