using System.Collections.Generic;
using CJRGaming.MMO.Common;
using ExitGames.Client.Photon;

public class LoginController :ViewController
{
    public LoginController(IView controlledView) : base(controlledView)
    {
    }

    public void SendLogin(string username, string password)
    {
        Dictionary<byte,object> parameters = new Dictionary<byte, object>();

        parameters.Add((byte)ParameterCode.SubOperationCode, OperationSubCode.Login);
        parameters.Add((byte)ParameterCode.UserName, username);
        parameters.Add((byte)ParameterCode.Password, password);
        OperationRequest request = new OperationRequest {OperationCode = (byte)OperationCode.Login, Parameters = parameters};

        SendOperation(request, true, 0, true);
    }
}
