using System.Collections;
using AegisBornCommon;
using ExitGames.Client.Photon;

class CharacterCreateController : GameStateController
{
    private CharacterCreateHandler _ccHandler;
    public CharacterCreateController(CharacterCreate view) : base(view)
    {
        _ccHandler = new CharacterCreateHandler();
        _ccHandler.afterMessageRecieved += view.AfterCreateCharacter;
        OperationHandlers.Add(OperationCode.CreateCharacter, _ccHandler);
    }

    public override GameState State
    {
        get
        {
            return GameState.CharacterCreate;
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
            foreach (DictionaryEntry pair in returnValues)
            {
                OnDebugReturn(DebugLevel.ERROR, string.Format("{0} - {1}", pair.Key, pair.Value));
            }
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
