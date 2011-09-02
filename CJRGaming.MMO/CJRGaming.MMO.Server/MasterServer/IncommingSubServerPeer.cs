using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Logging;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;

namespace CJRGaming.MMO.Server.MasterServer
{
    public class IncommingSubServerPeer : ServerPeerBase
    {
        private readonly MasterServer Server;

        #region Constants and Fields

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Properties

        public Guid? ServerId { get; protected set; }

        public string TcpAddress { get; protected set; }

        public string UdpAddress { get; protected set; }

        #endregion

        public IncommingSubServerPeer(IRpcProtocol protocol, IPhotonPeer unmanagedPeer)
            : base(protocol, unmanagedPeer)
        {
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            throw new NotImplementedException();
        }

        protected override void OnDisconnect()
        {
            Log.InfoFormat("game server connection closed (id={0})", ConnectionId);
        }

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            if (!ServerId.HasValue)
            {
                Log.Warn("received game server event but server is not registered");
                return;
            }
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            throw new NotImplementedException();
        }
    }
}
