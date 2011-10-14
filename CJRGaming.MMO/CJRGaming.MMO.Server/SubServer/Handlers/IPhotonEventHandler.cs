using Photon.SocketServer;

public interface IPhotonEventHandler
{
    void HandleEvent(EventData eventData);
    void OnHandleEvent(EventData eventData);
}
