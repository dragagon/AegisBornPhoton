using AegisBornCommon;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace AegisBorn.Operations
{
    public class SelectCharacterOperation : Operation
    {
        public SelectCharacterOperation(OperationRequest request) : base(request)
        {
        }

        [RequestParameter(Code = (short)ParameterCode.ObjectId, IsOptional = false)]
        public int CharacterId { get; set; }
    }
}
