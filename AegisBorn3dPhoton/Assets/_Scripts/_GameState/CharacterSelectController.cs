using System.Collections;
using System.Collections.Generic;
using AegisBornCommon;
using AegisBornCommon.Models;
using ExitGames.Client.Photon;

public class CharacterSelectController : GameStateController
{
    private int _characterSlots;
    private List<AegisBornCharacter> _characters;

    private GetCharactersHandler _gcHandler;
    private SelectCharacterHandler _scHandler;

    public CharacterSelectController(CharacterSelect view) : base(view)
    {
        _characters = new List<AegisBornCharacter>();
        _gcHandler = new GetCharactersHandler();
        _gcHandler.afterMessageRecieved += AfterCharacterList;
        OperationHandlers.Add(OperationCode.GetCharacters, _gcHandler);

        _scHandler = new SelectCharacterHandler();
        _scHandler.afterMessageRecieved += view.AfterCharacterSelected;
        OperationHandlers.Add(OperationCode.SelectCharacter, _scHandler);
    }

    public List<AegisBornCharacter> Characters { get { return _characters; } }
    public int CharacterSlots { get { return _characterSlots; } }

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
            foreach(DictionaryEntry pair in returnValues)
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

    public void AfterCharacterList()
    {
        _characterSlots = _gcHandler.CharacterSlots;
        _characters = _gcHandler.Characters;
    }
}