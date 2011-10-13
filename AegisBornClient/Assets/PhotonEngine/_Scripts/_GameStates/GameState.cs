using System.Collections.Generic;
using CJRGaming.MMO.Common;

public abstract class GameState : IGameState
{

    protected PhotonEngine _engine;

    protected GameState(PhotonEngine engine)
    {
        _engine = engine;
    }

    #region Implementation of IGameState

    // Do nothing
    public virtual void OnUpdate()
    {
    }

    // Do nothing
    public virtual void SendOperation(OperationCode operationCode, Dictionary<byte, object> parameters, bool sendReliable, byte channelId, bool encrypt)
    {
    }

    #endregion
}
