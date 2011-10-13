using ExitGames.Client.Photon;

public interface IPhotonEventHandler
{
    void HandleEvent(EventData eventData);
    void OnHandleEvent(EventData eventData);
}
