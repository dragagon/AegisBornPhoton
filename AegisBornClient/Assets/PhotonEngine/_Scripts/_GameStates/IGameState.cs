using ExitGames.Client.Photon;

public interface IGameState
{
    void OnUpdate();
    void SendOperation(OperationRequest request, bool sendReliable, byte channelId, bool encrypt);
}
