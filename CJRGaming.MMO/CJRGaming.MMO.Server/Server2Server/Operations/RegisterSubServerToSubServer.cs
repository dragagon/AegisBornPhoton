using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace CJRGaming.MMO.Server.Server2Server.Operations
{
    public class RegisterSubServerToSubServer : Operation
    {

        public RegisterSubServerToSubServer(IRpcProtocol rpcProtocol, OperationRequest operationRequest)
            : base(rpcProtocol, operationRequest)
        {
        }

        public RegisterSubServerToSubServer()
        {
        }

        #region Properties
        #endregion
    }
}
