using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Common;
using CJRGaming.MMO.Server.Operations;
using ExitGames.Logging;
using Photon.SocketServer;

namespace CJRGaming.MMO.Server.MasterServer
{
    public class Unity3dPeer : PeerBase
    {
        private MasterServer _server;
        public Guid UserId { get; protected set; }
        private IncomingSubServerPeer _currentRegionServer;
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public Unity3dPeer(InitRequest initRequest, MasterServer server)
            : base(initRequest.Protocol, initRequest.PhotonPeer)
        {
            _server = server;

            // We create a user id on the server side so players can't spoof other player ids.
            UserId = Guid.NewGuid();
            _server.ConnectedClients.Add(UserId, this);
            Log.DebugFormat("Added Peer to client list");
        }

        protected override void OnDisconnect()
        {
            // Remove from chat server and region server
            _server.ConnectedClients.Remove(UserId);
            Log.DebugFormat("Removed disconnected peer");
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            Log.DebugFormat("Received Operation request from client: " + operationRequest.OperationCode);

            // If someone gets sneaky and figures out we send an id, we rip out the old one if it was passed to us.
            operationRequest.Parameters.Remove((byte)ParameterCode.UserId);

            // Add our user id 
            operationRequest.Parameters.Add((byte)ParameterCode.UserId, UserId);
            // and pass the call through
            switch (operationRequest.OperationCode)
            {
                case (byte)OperationCode.Login:
                    _server.SubServers.LoginServer.SendOperationRequest(operationRequest, sendParameters);
                    break;
                case (byte)OperationCode.Chat:
                    _server.SubServers.ChatServer.SendOperationRequest(operationRequest, sendParameters);
                    break;
                case (byte)OperationCode.Region:
                    _currentRegionServer.SendOperationRequest(operationRequest, sendParameters);
                    break;
            }
        }
    }
}
