using System.Collections.Generic;
using CJRGaming.MMO.Common;

public interface IGameState
{
    void OnUpdate();
    void SendOperation(OperationCode operationCode, Dictionary<byte, object> parameters, bool sendReliable, byte channelId, bool encrypt);
}
