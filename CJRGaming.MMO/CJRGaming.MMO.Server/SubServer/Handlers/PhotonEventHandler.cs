
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;

public abstract class PhotonEventHandler : IPhotonEventHandler
{
    protected readonly ServerPeerBase _peer;

    protected PhotonEventHandler(ServerPeerBase peer)
    {
        _peer = peer;
    }

    delegate void BeforeEventRecieved();
    BeforeEventRecieved beforeEventRecieved;

    delegate void AfterEventRecieved();
    AfterEventRecieved afterEventRecieved;

    public void HandleEvent(EventData eventData)
    {
        if (beforeEventRecieved != null)
        {
            beforeEventRecieved();
        }
        OnHandleEvent(eventData);
        if (afterEventRecieved != null)
        {
            afterEventRecieved();
        }
    }

    public abstract void OnHandleEvent(EventData eventData);
}
