using AegisBornCommon;
using ExitGames.Client.Photon;

public class WaitingForConnect : GameStateController
{
    public WaitingForConnect(GameView view) : base(view)
    {
    }

    public override GameState State
    {
        get { return GameState.WaitingForConnect; }
    }

    public override void OnPeerStatusCallback(PhotonClient gameLogic, StatusCode returnCode)
    {
        switch (returnCode)
        {
            case StatusCode.Connect:
                {
                    GameView.OnConnect(this);
                    break;
                }

            case StatusCode.Disconnect:
            case StatusCode.DisconnectByServer:
            case StatusCode.DisconnectByServerLogic:
            case StatusCode.DisconnectByServerUserLimit:
            case StatusCode.TimeoutDisconnect:
                {
                    GameView.OnDisconnect(this, returnCode);
                    break;
                }

            default:
                {
                    OnUnexpectedPhotonReturn((int)returnCode, OperationCode.Nil, null);
                    break;
                }
        }
    }

    public override void OnUpdate(PhotonClient gameLogic)
    {
        gameLogic.Peer.Service();
    }

}
