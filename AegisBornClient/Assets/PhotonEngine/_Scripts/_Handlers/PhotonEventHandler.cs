using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;

public abstract class PhotonEventHandler : IPhotonEventHandler
{
    delegate void BeforeEventReceived();
    BeforeEventReceived beforeEventReceived;

    delegate void AfterEventReceived();
    AfterEventReceived afterEventReceived;

    public void HandleEvent(EventData eventData)
    {
        if (beforeEventReceived != null)
        {
            beforeEventReceived();
        }
        OnHandleEvent(eventData);
        if (afterEventReceived != null)
        {
            afterEventReceived();
        }
    }

    public abstract void OnHandleEvent(EventData eventData);
}
