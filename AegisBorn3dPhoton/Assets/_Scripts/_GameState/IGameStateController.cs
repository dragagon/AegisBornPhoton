using System.Collections;
using System.Collections.Generic;
using AegisBornCommon;
using ExitGames.Client.Photon;

public interface IGameStateController
{
    IGameView GameView { get; }

    Dictionary<OperationCode, IOperationHandler> OperationHandlers { get; }

    GameState State { get; }

    void OnEventReceive(PhotonClient gameLogic, EventCode eventCode, Hashtable eventData);

    void OnOperationReturn(PhotonClient gameLogic, OperationCode operationCode, int returnCode, Hashtable returnValues);

    void OnPeerStatusCallback(PhotonClient gameLogic, StatusCode returnCode);

    void OnUpdate(PhotonClient gameLogic);

    void SendOperation(PhotonClient gameLogic, OperationCode operationCode, Hashtable parameter, bool sendReliable, byte channelId, bool encrypt);

    void OnUnexpectedEventReceive(EventCode eventCode, Hashtable eventData);

    void OnUnexpectedOperationError(OperationCode operationCode, ErrorCode errorCode, string debugMessage,
                                           Hashtable hashtable);

    void OnUnexpectedPhotonReturn(int photonReturnCode, OperationCode operationCode, Hashtable hashtable);

    void OnDebugReturn(DebugLevel debugLevel, string message);
}
