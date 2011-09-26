using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace CJRGaming.MMO.Server.Operations
{
    class EstablishSecureConnectionOperation : Operation
    {
    /// <summary>
    /// Initializes a new instance of the <see cref="EstablishSecureCommunicationOperation"/> class.
    /// </summary>
    /// <param name="operationRequest">
    /// The operation request.
    /// </param>
        public EstablishSecureConnectionOperation(OperationRequest operationRequest)
        : base(operationRequest)
    {
    }

    /// <summary>
    /// Gets or sets the clients public key.
    /// </summary>
    [DataMember(Code = (byte)ParameterCode.ClientKey, IsOptional = false)]
    public byte[] ClientKey { get; set; }

    /// <summary>
    /// Gets or sets the servers public key.
    /// </summary>
    [DataMember(Code = (byte)ParameterCode.ServerKey, IsOptional = false)]
    public byte[] ServerKey { get; set; }    }
}
