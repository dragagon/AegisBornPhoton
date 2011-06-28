using System.Collections;
using AegisBornCommon;
using ExitGames.Client.Photon;

public class CharacterSelectController : GameStateController
{
    public CharacterSelectController(GameView view) : base(view)
    {
    }

    public override GameState State
    {
        get
        {
            return GameState.CharacterSelect;
        }
    }

    public override void OnOperationReturn(PhotonClient gameLogic, OperationCode operationCode, int returnCode, Hashtable returnValues)
    {
        IOperationHandler handler;

        if (OperationHandlers.TryGetValue(operationCode, out handler))
        {
            handler.HandleMessage(gameLogic, operationCode, returnCode, returnValues);
        }
        else
        {
            OnUnexpectedPhotonReturn(returnCode, operationCode, returnValues);
        }
    }

    public override void OnPeerStatusCallback(PhotonClient gameLogic, StatusCode returnCode)
    {
        switch (returnCode)
        {
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

    public override void SendOperation(PhotonClient gameLogic, OperationCode operationCode, Hashtable parameter, bool sendReliable, byte channelId, bool encrypt)
    {
        gameLogic.Peer.OpCustom((byte)operationCode, parameter, sendReliable, channelId, encrypt);
    }

}