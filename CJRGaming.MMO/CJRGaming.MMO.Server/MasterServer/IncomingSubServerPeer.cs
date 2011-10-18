using System;
using CJRGaming.MMO.Common;
using CJRGaming.MMO.Server.Server2Server.Operations;
using ExitGames.Logging;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using ErrorCode = CJRGaming.MMO.Server.Operations.ErrorCode;
using OperationCode = CJRGaming.MMO.Server.Server2Server.Operations.OperationCode;

namespace CJRGaming.MMO.Server.MasterServer
{
    public class IncomingSubServerPeer : ServerPeerBase
    {
        #region Constants and Fields

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        private readonly MasterServer _server;

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
            if (Log.IsDebugEnabled)
            {
                Log.DebugFormat("OnOperationRequest: pid={0}, op={1}", ConnectionId, operationRequest.OperationCode);
            }

            OperationResponse response;

            switch ((OperationCode)operationRequest.OperationCode)
            {
                default:
                    response = new OperationResponse(operationRequest.OperationCode) { ReturnCode = -1, DebugMessage = "Unknown operation code" };
                    break;

                case OperationCode.RegisterGameServer:
                    {
                        response = ServerId.HasValue
                                       ? new OperationResponse(operationRequest.OperationCode) { ReturnCode = -1, DebugMessage = "already registered" }
                                       : HandleRegisterGameServerRequest(operationRequest);
                        break;
                    }
            }

            SendOperationResponse(response, sendParameters);
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

        // Forward Events back tot he client that initially sent it.
        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            if (!ServerId.HasValue)
            {
                Log.Warn("received game server event but server is not registered");
                return;
            }
            // Check for a userid
            if(eventData.Parameters.ContainsKey((byte)ParameterCode.UserId))
            {
                Unity3dPeer peer;
                // make sure a peer exists with that user id
                _server.ConnectedClients.TryGetValue(new Guid((Byte[])eventData.Parameters[(byte) ParameterCode.UserId]), out peer);
                if(peer != null)
                {
                    // Remove the user id
                    eventData.Parameters.Remove((byte) ParameterCode.UserId);
                    // Send the event to the client
                    peer.SendEvent(eventData, sendParameters);
                }
            }
        }

        // Forward responses back to the client that initially sent it.
        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            // Check for a userid
            if (operationResponse.Parameters.ContainsKey((byte)ParameterCode.UserId))
            {
                Unity3dPeer peer;
                // make sure a peer exists with that user id
                _server.ConnectedClients.TryGetValue(new Guid((Byte[])operationResponse.Parameters[(byte)ParameterCode.UserId]), out peer);
                if (peer != null)
                {
                    Log.DebugFormat("Found user {0}, returning response {1}", new Guid((Byte[])operationResponse.Parameters[(byte)ParameterCode.UserId]), operationResponse.OperationCode);
                    // Remove the user id
                    operationResponse.Parameters.Remove((byte)ParameterCode.UserId);
                    // Send the event to the client
                    peer.SendOperationResponse(operationResponse, sendParameters);
                }
            }
        }

        #endregion


        protected virtual OperationResponse HandleRegisterGameServerRequest(OperationRequest request)
        {
            var registerRequest = new RegisterSubServer(Protocol, request);
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

            _server.SubServers.OnConnect(this);

            return new OperationResponse(request.OperationCode);
        }
    }
}
