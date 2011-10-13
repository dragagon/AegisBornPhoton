using ExitGames.Client.Photon;

public interface IPhotonOperationHandler
{
    void HandleResponse(OperationResponse response);
    void OnHandleResponse(OperationResponse response);
}
