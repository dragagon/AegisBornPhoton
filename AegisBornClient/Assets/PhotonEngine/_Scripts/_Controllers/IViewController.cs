using ExitGames.Client.Photon;

public interface IViewController
{
    #region Implementation of IPhotonPeerListener

    void DebugReturn(DebugLevel level, string message);
    void OnOperationResponse(OperationResponse operationResponse);
    void OnEvent(EventData eventData);

    void OnUnexpectedEvent(EventData eventData);
    void OnUnexpectedOperationResponse(OperationResponse operationResponse);
    void OnUnexpectedStatusCode(StatusCode statusCode);

    void OnDisconnected(string message);

    void ApplicationQuit();

    bool IsConnected { get; }

    void Connect();

    void SendOperation(OperationRequest request, bool sendReliable, byte channelId, bool encrypt);

    #endregion

}
