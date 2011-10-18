
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;

public abstract class PhotonRequestHandler : IPhotonRequestHandler
{
    protected readonly ServerPeerBase _peer;

    protected PhotonRequestHandler(ServerPeerBase peer)
    {
        _peer = peer;
    }

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
