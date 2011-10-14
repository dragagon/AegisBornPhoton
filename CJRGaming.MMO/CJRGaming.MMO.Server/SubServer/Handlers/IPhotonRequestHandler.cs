using Photon.SocketServer;

public interface IPhotonRequestHandler
{
    void HandleRequest(OperationRequest request);
    void OnHandleRequest(OperationRequest request);
}
