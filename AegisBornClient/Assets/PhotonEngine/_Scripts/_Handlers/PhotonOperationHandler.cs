using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;

public abstract class PhotonOperationHandler : IPhotonOperationHandler
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
