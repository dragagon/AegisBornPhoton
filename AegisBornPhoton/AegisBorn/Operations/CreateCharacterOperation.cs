using AegisBornCommon;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

namespace AegisBorn.Operations
{
    public class CreateCharacterOperation : Operation
    {
        public CreateCharacterOperation(OperationRequest request) : base(request)
        {
        }

        [RequestParameter(Code = (short)ParameterCode.CharacterName, IsOptional = false)]
        public string CharacterName { get; set; }

        [RequestParameter(Code = (short)ParameterCode.CharacterSex, IsOptional = false)]
        public string CharacterSex { get; set; }

        [RequestParameter(Code = (short)ParameterCode.CharacterClass, IsOptional = false)]
        public string CharacterClass { get; set; }
    }
}
