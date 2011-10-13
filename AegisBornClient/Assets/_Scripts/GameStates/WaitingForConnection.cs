using System.Collections.Generic;
using CJRGaming.MMO.Common;

class WaitingForConnection : GameState
{
    public WaitingForConnection(PhotonEngine engine) : base(engine)
    {
    }

    public override void OnUpdate()
    {
        _engine.Peer.Service();
    }

    public override void SendOperation(OperationCode operationCode, Dictionary<byte, object> parameters, bool sendReliable, byte channelId, bool encrypt)
    {
        _engine.Peer.OpCustom((byte) operationCode, parameters, sendReliable, channelId, encrypt);
    }
}
