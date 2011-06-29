using System.Collections;
using AegisBornCommon;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace AegisBorn.Operations
{
    public class GetCharactersOperation : Operation
    {
        public GetCharactersOperation(OperationRequest request) : base(request)
        {
        }

        [ResponseParameter(Code = (short)ParameterCode.CharacterSlots, IsOptional = false)]
        public int CharacterSlots { get; set; }

        [ResponseParameter(Code = (short)ParameterCode.Characters, IsOptional = false)]
        public Hashtable Characters { get; set; }
    }
}
