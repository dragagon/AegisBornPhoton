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
    public class IncomingSubServerToSubServerPeer : ServerPeerBase
    {
        private readonly SubServer _server;

        #region Constants and Fields

        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        public Dictionary<byte, IPhotonRequestHandler> RequestHandlers = new Dictionary<byte, IPhotonRequestHandler>();
        public Dictionary<byte, IPhotonEventHandler> EventHandlers = new Dictionary<byte, IPhotonEventHandler>();
        public Dictionary<byte, IPhotonResponseHandler> ResponseHandlers = new Dictionary<byte, IPhotonResponseHandler>();

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

            if (RequestHandlers.TryGetValue(operationRequest.OperationCode, out handler))
            {
                handler.HandleRequest(operationRequest);
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

            if (EventHandlers.TryGetValue(eventData.Code, out handler))
            {
                handler.HandleEvent(eventData as EventData);
            }
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            IPhotonResponseHandler handler;

            if (ResponseHandlers.TryGetValue(operationResponse.OperationCode, out handler))
            {
                handler.HandleResponse(operationResponse);
            }
        }

        #endregion
    }
}
