using System.Collections;
using System.Collections.Generic;
using AegisBornCommon;
using ExitGames.Client.Photon;

public class GameStateController : IGameStateController
{
    #region Implementation of IGameStateController

    #region properties

    private readonly GameView _gameView;

    private readonly Dictionary<OperationCode, IOperationHandler> _operationHandlers = new Dictionary<OperationCode, IOperationHandler>();

    public IGameView GameView
    {
        get { return _gameView; }
    }

    public Dictionary<OperationCode, IOperationHandler> OperationHandlers
    {
        get { return _operationHandlers; }
    }

    public virtual GameState State
    {
        get { return GameState.NilState; }
    }

    #endregion

    public GameStateController(GameView view) 
    {
        _gameView = view;
    }

    #region methods

    public virtual void OnEventReceive(PhotonClient gameLogic, EventCode eventCode, Hashtable eventData)
    {
        OnUnexpectedEventReceive(eventCode, eventData);
    }

    public virtual void OnOperationReturn(PhotonClient gameLogic, OperationCode operationCode, int returnCode, Hashtable returnValues)
    {
        OnUnexpectedPhotonReturn(returnCode, operationCode, returnValues);
    }

    public virtual void OnPeerStatusCallback(PhotonClient gameLogic, StatusCode returnCode)
    {
        OnUnexpectedPhotonReturn((int)returnCode, OperationCode.Nil, null);
    }

    public virtual void OnUpdate(PhotonClient gameLogic)
    {
    }

    public virtual void SendOperation(PhotonClient gameLogic, OperationCode operationCode, Hashtable parameter, bool sendReliable, byte channelId, bool encrypt)
    {
    }

    #endregion

    #region Error Handling

    public void OnDebugReturn(DebugLevel level, string message)
    {
        _gameView.LogDebug(this, string.Format("{0} - {1}", level, message));
    }

    public void OnUnexpectedEventReceive(EventCode eventCode, Hashtable eventData)
    {
        _gameView.LogError(this, string.Format("unexpected event {0}", eventCode));
    }

    public void OnUnexpectedOperationError(OperationCode operationCode, ErrorCode errorCode, string debugMessage, Hashtable hashtable)
    {
        _gameView.LogError(this, string.Format("unexpected operation error {0} from operation {1} in state {2}", errorCode, operationCode, State));
    }

    public void OnUnexpectedPhotonReturn(int photonReturnCode, OperationCode operationCode, Hashtable hashtable)
    {
        _gameView.LogError(this, string.Format("opcode: {1} - unexpected return {0}", photonReturnCode, operationCode));
        var error = (ErrorCode)(int)hashtable[(byte)ParameterCode.ErrorCode];
        var debug = (string)hashtable[(byte)ParameterCode.DebugMessage];
        _gameView.LogError(this, string.Format("error: {1} - message: {0}", debug, error));
    }

    #endregion

    #endregion
}
