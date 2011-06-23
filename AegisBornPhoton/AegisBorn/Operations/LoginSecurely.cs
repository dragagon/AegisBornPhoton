using AegisBornCommon;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;

public class LoginSecurely : Operation
{
    public LoginSecurely(OperationRequest operationRequest)
        : base(operationRequest)
    {
    }

    [RequestParameter(Code = (short)ParameterCode.UserName, IsOptional = false)]
    public string UserName { get; set; }

    [RequestParameter(Code = (short)ParameterCode.Password, IsOptional = false)]
    public string Password { get; set; }

}
