using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;

public abstract class PhotonOperationHandler : IPhotonOperationHandler
{
    delegate void BeforeResponseReceived();
    BeforeResponseReceived beforeResponseReceived;

    delegate void AfterResponseReceived();
    AfterResponseReceived afterResponseReceived;

    public void HandleResponse(OperationResponse response)
    {
        if (beforeResponseReceived != null)
        {
            beforeResponseReceived();
        }
        OnHandleResponse(response);
        if (afterResponseReceived != null)
        {
            afterResponseReceived();
        }
    }

    public abstract void OnHandleResponse(OperationResponse response);
}
