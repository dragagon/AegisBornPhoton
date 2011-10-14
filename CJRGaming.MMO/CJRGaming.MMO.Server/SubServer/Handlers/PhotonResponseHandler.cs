
using Photon.SocketServer;

public abstract class PhotonResponseHandler : IPhotonResponseHandler
{
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
