using Photon.SocketServer;

public interface IPhotonResponseHandler
{
    void HandleResponse(OperationResponse response);
    void OnHandleResponse(OperationResponse response);
}
