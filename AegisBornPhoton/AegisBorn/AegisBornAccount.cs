using System;
using System.Collections;
using System.Collections.Generic;
using AegisBorn.Events;
using AegisBorn.Models.Base;
using AegisBorn.Operations;
using AegisBornCommon;
using NHibernate.Criterion;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using Photon.SocketServer.Rpc.Reflection;

namespace AegisBorn
{
    public class AegisBornAccount : IOperationHandler
    {

        private static readonly OperationMethodInfoCache Operations = new OperationMethodInfoCache();

        private readonly OperationDispatcher _dispatcher;

        private readonly Peer _peer;

        private readonly SfGuardUser _user;

        private int _characterSlots;

        private bool _canAddCharacters;

        static AegisBornAccount()
        {
            Operations = new OperationMethodInfoCache();
            try
            {
                Operations.RegisterOperations(typeof(AegisBornAccount));
            }
            catch (Exception e)
            {
                ErrorHandler.OnUnexpectedException(Operations, e);
            }
        }

        public AegisBornAccount(Peer photonPeer, SfGuardUser user)
        {
            _user = user;
            _peer = photonPeer;
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

        [Operation(OperationCode = (byte)OperationCode.GetCharacters)]
        public OperationResponse OperationGetCharacters(Peer peer, OperationRequest request)
        {
            var operation = new GetCharactersOperation(request);
            if (!operation.IsValid)
            {
                return new OperationResponse(request, (int)ErrorCode.InvalidOperationParameter, operation.GetErrorMessage());
            }

            try
            {
                using (var session = NHibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var user = session.CreateCriteria(typeof(AegisBornUserProfile), "abup").Add(Restrictions.Eq("abup.UserId", _user)).UniqueResult<AegisBornUserProfile>();

                        operation.CharacterSlots = _characterSlots = user.CharacterSlots;

                        var characters =
                            session.CreateCriteria(typeof (AegisBornCharacter), "abc").Add(Restrictions.Eq(
                                "abc.UserId", _user)).List<AegisBornCharacter>();

                        _canAddCharacters = _characterSlots > characters.Count;

                        operation.Characters = new Hashtable();
                        foreach (var aegisBornCharacter in characters)
                        {
                            //load characters into hash table
                            operation.Characters.Add(aegisBornCharacter.Id, aegisBornCharacter.GetFullPlayer().GetHashtable());
                        }
                        transaction.Commit();

                        return operation.GetOperationResponse(0, "OK");
                    }
                }
            }
            catch (Exception e)
            {
                // Do nothing because we are about to throw them out anyway.
                peer.PublishOperationResponse(new OperationResponse(request, (int)ErrorCode.InvalidUserPass, e.ToString()));
            }

            return operation.GetOperationResponse((int)ErrorCode.Fatal, "Fatal error with database");
        }

        [Operation(OperationCode = (byte)OperationCode.CreateCharacter)]
        public OperationResponse OperationCreateCharacter(Peer peer, OperationRequest request)
        {
            var operation = new CreateCharacterOperation(request);
            if (!operation.IsValid)
            {
                return new OperationResponse(request, (int)ErrorCode.InvalidOperationParameter, operation.GetErrorMessage());
            }

            if(!_canAddCharacters)
            {
                return new OperationResponse(request, (int) ErrorCode.InvalidCharacter, "No free Character Slots.");
            }

            try
            {
                using (var session = NHibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var character = session.CreateCriteria(typeof(AegisBornCharacter), "abc").Add(Restrictions.Eq("abc.Name", operation.CharacterName)).UniqueResult<AegisBornCharacter>();

                        if(character != null)
                        {
                            return new OperationResponse(request, (int)ErrorCode.InvalidCharacter, "Character name taken.");
                        }

                        var newChar = new AegisBornCharacter
                                          {
                                              Name = operation.CharacterName,
                                              Sex = operation.CharacterSex,
                                              Class = operation.CharacterClass,
                                              Level = 1,
                                              UserId = _user
                                          };
                        session.Save(newChar);

                        transaction.Commit();
                        
                        return operation.GetOperationResponse(0, "OK");
                    }
                }
            }
            catch (Exception e)
            {
                // Do nothing because we are about to throw them out anyway.
                peer.PublishOperationResponse(new OperationResponse(request, (int)ErrorCode.InvalidCharacter, e.ToString()));
            }

            return operation.GetOperationResponse((int)ErrorCode.Fatal, "Fatal error with database");
        }

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
            // set initial handler
            _peer.SetCurrentOperationHandler(OperationHandlerDisconnected.Instance);

            // use item channel to ensure that this event arrives in correct order with move/subscribe events
            _peer.PublishEvent(gameExited.GetEventData((byte)EventCode.GameExited, Reliability.Reliable, 0));
        }
    }
}
