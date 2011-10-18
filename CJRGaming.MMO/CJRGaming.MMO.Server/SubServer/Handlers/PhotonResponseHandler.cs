
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;

public abstract class PhotonResponseHandler : IPhotonResponseHandler
{
    protected readonly ServerPeerBase _peer;

    protected PhotonResponseHandler(ServerPeerBase peer)
    {
        _peer = peer;
    }

    delegate void BeforeResponseRecieved();
    BeforeResponseRecieved beforeResponseRecieved;

    delegate void AfterResponseRecieved();
    AfterResponseRecieved afterResponseRecieved;

    public void HandleResponse(OperationResponse response)
    {
        if (beforeResponseRecieved != null)
        {
            beforeResponseRecieved();
        }
        OnHandleResponse(response);
        if (afterResponseRecieved != null)
        {
            afterResponseRecieved();
        }
    }

    public abstract void OnHandleResponse(OperationResponse response);
}
