using System.Security.Cryptography;
using System.Text;
using AegisBorn.Models.Base;
using AegisBorn.OperationHandlers;
using AegisBornCommon;
using NHibernate.Criterion;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using System.Collections.Generic;
using Photon.SocketServer.Rpc.Reflection;
using System;

namespace AegisBorn
{
    class AegisBornPeer : Peer, IOperationHandler
    {
        private static readonly OperationMethodInfoCache Operations = new OperationMethodInfoCache();

        private readonly OperationDispatcher _dispatcher;

        static AegisBornPeer()
        {
            Operations = new OperationMethodInfoCache();
            try
            {
                Operations.RegisterOperations(typeof(AegisBornPeer));
            }
            catch (Exception e)
            {
                ErrorHandler.OnUnexpectedException(Operations, e);
            }
        }

        public AegisBornPeer(PhotonPeer photonPeer)
            : base(photonPeer)
        {
            _dispatcher = new OperationDispatcher(Operations, this);
            SetCurrentOperationHandler(this);
        }

        public void OnDisconnect(Peer peer)
        {
            SetCurrentOperationHandler(OperationHandlerDisconnected.Instance);
            Dispose();
        }

        public void OnDisconnectByOtherPeer(Peer peer)
        {
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

        [Operation(OperationCode = (byte)OperationCode.ExchangeKeysForEncryption)]
        public OperationResponse OperationKeyExchange(Peer peer, OperationRequest request)
        {
            var operation = new EstablishSecureCommunicationOperation(request);
            if (!operation.IsValid)
            {
                return new OperationResponse(request, (int)ErrorCode.InvalidOperationParameter, operation.GetErrorMessage());
            }

            // initialize the peer to support encrytion
            operation.ServerKey = peer.PhotonPeer.InitializeEncryption(operation.ClientKey);

            // publish the servers public key to the client
            return operation.GetOperationResponse(0, "OK");
        }

        [Operation(OperationCode = (byte)OperationCode.Login)]
        public OperationResponse OperationLogin(Peer peer, OperationRequest request)
        {
            var operation = new LoginSecurely(request);
            if(!operation.IsValid)
            {
                return new OperationResponse(request, (int)ErrorCode.InvalidOperationParameter, operation.GetErrorMessage());
            }

            // Attempt to get user from db and check password.
            try
            {
                using (var session = NHibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var user = session.CreateCriteria(typeof(SfGuardUser), "sf").Add(Restrictions.Eq("sf.Username", operation.UserName)).UniqueResult<SfGuardUser>();

                        var sha1 = SHA1CryptoServiceProvider.Create();

                        var hash = BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(user.Salt + operation.Password))).Replace("-", "");

                        transaction.Commit();

                        if (String.Equals(hash.Trim(), user.Password.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            peer.PublishOperationResponse(operation.GetOperationResponse(0, "OK"));

                            // transfer operation handling to our account which maintains the user/character
                            var account = new AegisBornAccountHandler(this, user);
                            peer.SetCurrentOperationHandler(account);
                            return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Do nothing because we are about to throw them out anyway.
                peer.PublishOperationResponse(new OperationResponse(request, (int)ErrorCode.InvalidUserPass, e.ToString()));
            }

            peer.PublishOperationResponse(new OperationResponse(request, (int)ErrorCode.InvalidUserPass, "The Username or Password is incorrect"));
            peer.DisconnectByOtherPeer(this, request);
            return null;
        }
    }
}
