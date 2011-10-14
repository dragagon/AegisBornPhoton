using ExitGames.Client.Photon;

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
    public virtual void SendOperation(OperationRequest request, bool sendReliable, byte channelId, bool encrypt)
    {
    }

    #endregion
}
