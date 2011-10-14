
using Photon.SocketServer;

public abstract class PhotonRequestHandler : IPhotonRequestHandler
{
    delegate void BeforeRequestRecieved();
    BeforeRequestRecieved beforeRequestRecieved;

    delegate void AfterRequestRecieved();
    AfterRequestRecieved afterRequestRecieved;

    public void HandleRequest(OperationRequest request)
    {
        if (beforeRequestRecieved != null)
        {
            beforeRequestRecieved();
        }
        OnHandleRequest(request);
        if (afterRequestRecieved != null)
        {
            afterRequestRecieved();
        }
    }

    public abstract void OnHandleRequest(OperationRequest request);
}
