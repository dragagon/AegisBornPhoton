using System;
using System.Collections.Generic;
using AegisBorn.Events;
using AegisBorn.Models.Base;
using AegisBorn.Models.Base.Actor;
using AegisBornCommon;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using Photon.SocketServer.Rpc.Reflection;

namespace AegisBorn.OperationHandlers
{
    class AegisBornPlayerHandler : IOperationHandler
    {
        private static readonly OperationMethodInfoCache Operations = new OperationMethodInfoCache();

        private readonly OperationDispatcher _dispatcher;

        private readonly Peer _peer;

        private readonly SfGuardUser _user;

        private readonly AegisBornPlayer _selectedCharacter;

        public string PlayerName
        {
            get { return _selectedCharacter.Name; }
        }

        static AegisBornPlayerHandler()
        {
            Operations = new OperationMethodInfoCache();
            try
            {
                Operations.RegisterOperations(typeof(AegisBornPlayerHandler));
            }
            catch (Exception e)
            {
                ErrorHandler.OnUnexpectedException(Operations, e);
            }
        }

        public AegisBornPlayerHandler(Peer photonPeer, SfGuardUser user, AegisBornPlayer selectedCharacter)
        {
            _user = user;
            _selectedCharacter = selectedCharacter;
            _peer = photonPeer;
            _selectedCharacter.Peer = _peer;
            _dispatcher = new OperationDispatcher(Operations, this);
        }

        #region Implementation of IOperationHandler

        public void OnDisconnect(Peer peer)
        {
            peer.SetCurrentOperationHandler(OperationHandlerDisconnected.Instance);
            peer.Dispose();
        }

        public void OnDisconnectByOtherPeer(Peer peer)
        {
            ExitGame();
            // disconnect peer after the exit world event is sent
            peer.ResponseQueue.EnqueueAction(() => peer.RequestQueue.EnqueueAction(() => peer.PhotonPeer.Disconnect()));
        }

        public OperationResponse OnOperationRequest(Peer peer, OperationRequest operationRequest)
        {
            OperationResponse result;
            if (_dispatcher.DispatchOperationRequest(peer, operationRequest, out result))
            {
                return result;
            }
            return new OperationResponse(operationRequest, 0, "Ok", new Dictionary<short, object> { { 100, "We get signal." } });
        }

        #endregion

        [Operation(OperationCode = (byte)OperationCode.ExitGame)]
        public OperationResponse OperationExitWorld(Peer peer, OperationRequest request)
        {
            ExitGame();

            // don't send a response, since it is sent as an event
            request.OnCompleted();
            return null;
        }

        /// <summary>
        /// ExitGame is called when the user is removed from the server or requests to leave the game. Allows for nice cleanup.
        /// User must be logged in.
        /// </summary>
        public void ExitGame()
        {
            var gameExited = new GameExited();
            // set previous handler
            _peer.SetCurrentOperationHandler(new AegisBornAccountHandler(_peer, _user));

            // use item channel to ensure that this event arrives in correct order with move/subscribe events
            _peer.PublishEvent(gameExited.GetEventData((byte)EventCode.GameExited, Reliability.Reliable, 0));


        }
    }
}
