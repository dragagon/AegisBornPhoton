using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Server.Operations;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using Photon.SocketServer.Rpc.Reflection;

namespace CJRGaming.MMO.Server.MasterServer.Handlers
{
    public class LoginPassthroughHandler
    {
        [Operation(OperationCode = (byte)CommonOperationCodes.Encryption)]
        public OperationResponse HandleEncryption(PeerBase peer, OperationRequest request, SendParameters sendParameters)
        {
            var operation = new EstablishSecureConnectionOperation(request);
            if (!operation.IsValid)
            {
                return new OperationResponse(request, (int)ErrorCode.InvalidOperationParameter, operation.GetErrorMessage());
            }

            // initialize the peer to support encrytion
            operation.ServerKey = peer.InitializeEncryption(operation.ClientKey);

            // publish the servers public key to the client
            return new OperationResponse {OperationCode = request.OperationCode};
        }

        [Operation(OperationCode = (byte)CommonOperationCodes.Login)]
        public OperationResponse HandleLogin(Peer peer, OperationRequest request)
        {
            return null;
        }

    }
}
