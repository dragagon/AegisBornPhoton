using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Common;
using ExitGames.Logging;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;

namespace CJRGaming.MMO.Server.SubServer
{
    class IncomingSubServerToSubServerPeer : ServerPeerBase
    {
        private readonly SubServer _server;

        #region Constants and Fields

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public Dictionary<Server2Server.Operations.OperationCode, IPhotonRequestHandler> SubServerRequestHandlers = new Dictionary<Server2Server.Operations.OperationCode, IPhotonRequestHandler>();
        public Dictionary<Server2Server.Operations.EventCode, IPhotonEventHandler> SubServerEventHandlers = new Dictionary<Server2Server.Operations.EventCode, IPhotonEventHandler>();
        public Dictionary<Server2Server.Operations.OperationCode, IPhotonResponseHandler> SubServerResponseHandlers = new Dictionary<Server2Server.Operations.OperationCode, IPhotonResponseHandler>();

        #endregion

        public IncomingSubServerToSubServerPeer(InitRequest initRequest, SubServer server)
            : base(initRequest.Protocol, initRequest.PhotonPeer)
        {
            _server = server;
            Log.InfoFormat("game server connection from {0}:{1} established (id={2})", RemoteIP, RemotePort, ConnectionId);
        }

        #region Overrides of PeerBase

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            IPhotonRequestHandler handler;

            if (SubServerRequestHandlers.TryGetValue((Server2Server.Operations.OperationCode)operationRequest.OperationCode, out handler))
            {
                handler.HandleRequest(operationRequest);
            }
            else
            {
            }
        }

        protected override void OnDisconnect()
        {
        }

        #endregion

        #region Overrides of ServerPeerBase

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            IPhotonEventHandler handler;

            if (SubServerEventHandlers.TryGetValue((Server2Server.Operations.EventCode)eventData.Code, out handler))
            {
                handler.HandleEvent(eventData as EventData);
            }
            else
            {
            }
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            IPhotonResponseHandler handler;

            if (SubServerResponseHandlers.TryGetValue((Server2Server.Operations.OperationCode)operationResponse.OperationCode, out handler))
            {
                handler.HandleResponse(operationResponse);
            }
            else
            {
            }
        }

        #endregion
    }
}
