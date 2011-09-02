using System;
using CJRGaming.MMO.Server.Operations;
using CJRGaming.MMO.Server.Server2Server.Operations;
using ExitGames.Logging;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;

namespace CJRGaming.MMO.Server.MasterServer
{
    public class IncomingSubServerPeer : ServerPeerBase
    {
        private readonly MasterServer _server;

        #region Constants and Fields

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        #endregion

        #region Properties

        public Guid? ServerId { get; protected set; }

        public string TcpAddress { get; protected set; }

        public string UdpAddress { get; protected set; }

        public SubServerType Type { get; protected set; }

        #endregion

        public IncomingSubServerPeer(InitRequest initRequest, MasterServer server)
            : base(initRequest.Protocol, initRequest.PhotonPeer)
        {
            _server = server;
            Log.InfoFormat("game server connection from {0}:{1} established (id={2})", RemoteIP, RemotePort, ConnectionId);
        }

        #region Overrides of PeerBase

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            throw new NotImplementedException();
        }

        protected override void OnDisconnect()
        {
            Log.InfoFormat("game server connection closed (id={0})", ConnectionId);
            if (ServerId.HasValue)
            {
                _server.SubServers.OnDisconnect(this);
                // Here we might want to check if the login or chat server is down. if the chat server is down (i.e. null) we should send a message to the players connected to this master server.

            }
        }

        #endregion

        #region Overrides of ServerPeerBase

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

        #endregion


        protected virtual OperationResponse HandleRegisterGameServerRequest(OperationRequest request)
        {
            var registerRequest = new RegisterSubServer(this.Protocol, request);
            if (registerRequest.IsValid == false)
            {
                string msg = registerRequest.GetErrorMessage();
                if (Log.IsDebugEnabled)
                {
                    Log.DebugFormat("Invalid register request: {0}", msg);
                }

                return new OperationResponse(request.OperationCode) { DebugMessage = msg, ReturnCode = (short)ErrorCode.OperationInvalid };
            }

            if (Log.IsDebugEnabled)
            {
                Log.DebugFormat(
                    "Received register request: Address={0}, UdpPort={2}, TcpPort={1}, Type={3}",
                    registerRequest.GameServerAddress,
                    registerRequest.TcpPort,
                    registerRequest.UdpPort,
                    registerRequest.ServerType);
            }

            if (registerRequest.UdpPort.HasValue)
            {
                UdpAddress = registerRequest.GameServerAddress + ":" + registerRequest.UdpPort;
            }

            if (registerRequest.TcpPort.HasValue)
            {
                TcpAddress = registerRequest.GameServerAddress + ":" + registerRequest.TcpPort;
            }

            ServerId = registerRequest.ServerId;
            Type = (SubServerType) registerRequest.ServerType;

            this._server.SubServers.OnConnect(this);

            return new OperationResponse(request.OperationCode);
        }
    }
}
