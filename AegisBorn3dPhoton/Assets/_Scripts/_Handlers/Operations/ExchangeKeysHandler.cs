using System.Collections;
using AegisBornCommon;

public class ExchangeKeysHandler : IOperationHandler
{
    public override void OnHandleMessage(PhotonClient gameLogic, OperationCode operationCode, int returnCode, Hashtable returnValues)
    {
        gameLogic.Peer.DeriveSharedKey((byte[])returnValues[(byte)ParameterCode.ServerKey]);
    }
}
