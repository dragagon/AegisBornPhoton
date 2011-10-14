
using Photon.SocketServer;

public abstract class PhotonEventHandler : IPhotonEventHandler
{
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
