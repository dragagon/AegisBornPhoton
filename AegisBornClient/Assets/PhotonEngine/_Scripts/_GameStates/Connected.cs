using ExitGames.Client.Photon;

public class Connected : GameState
{
    public Connected(PhotonEngine engine) : base(engine)
    {
    }

    public override void OnUpdate()
    {
        _engine.Peer.Service();
    }

    public override void SendOperation(OperationRequest request, bool sendReliable, byte channelId, bool encrypt)
    {
        _engine.Peer.OpCustom(request, sendReliable, channelId, encrypt);
    }

}
