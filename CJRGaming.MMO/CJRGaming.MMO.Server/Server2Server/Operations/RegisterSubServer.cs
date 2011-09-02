using System;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace CJRGaming.MMO.Server.Server2Server.Operations
{
    public class RegisterSubServer : Operation
    {
        public RegisterSubServer(IRpcProtocol rpcProtocol, OperationRequest operationRequest)
            : base(rpcProtocol, operationRequest)
        {
        }

        public RegisterSubServer()
        {
        }


        #region Properties

        /// <summary>
        ///   Gets or sets the public game server ip address.
        /// </summary>
        [DataMember(Code = 4, IsOptional = false)]
        public string GameServerAddress { get; set; }

        /// <summary>
        ///   Gets or sets a unique server id.
        ///   This id is used to sync reconnects.
        /// </summary>
        [DataMember(Code = 3, IsOptional = false)]
        public Guid? ServerId { get; set; }

        /// <summary>
        ///   Gets or sets the TCP port of the game server instance.
        /// </summary>
        /// <value>The TCP port.</value>
        [DataMember(Code = 2, IsOptional = true)]
        public int? TcpPort { get; set; }

        /// <summary>
        ///   Gets or sets the UDP port of the game server instance.
        /// </summary>
        /// <value>The UDP port.</value>
        [DataMember(Code = 1, IsOptional = true)]
        public int? UdpPort { get; set; }

        [DataMember(Code = 5, IsOptional = false)]
        public int ServerType { get; set; }
        #endregion
    }
}
