using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using Photon.SocketServer.Rpc.Reflection;

namespace CJRGaming.MMO.Server.SubServer.Handlers
{
    public class LoginOperationHandler
    {
        [Operation(OperationCode = (byte)0)]
        public OperationResponse OperationGetCharacters(Peer peer, OperationRequest request)
        {
            return null;
        }
    }
}
