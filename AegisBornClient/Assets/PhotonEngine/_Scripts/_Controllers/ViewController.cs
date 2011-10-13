using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;

public class ViewController : IViewController
{

    private IView _controlledView;

    public ViewController(IView controlledView)
    {
        _controlledView = controlledView;
        PhotonEngine.Instance.Controller = this;
    }

    #region Implementation of IViewController

    public void OnEvent(EventData eventData)
    {
        OnUnexpectedEvent(eventData);
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        OnUnexpectedOperationResponse(operationResponse);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        _controlledView.LogDebug(string.Format("{0} - {1}", level, message));
    }

    public void OnUnexpectedEvent(EventData eventData)
    {
        _controlledView.LogError(string.Format("unexpected event {0}", eventData.Code));
    }

    public void OnUnexpectedOperationResponse(OperationResponse operationResponse)
    {
        _controlledView.LogError(string.Format("unexpected operation error {0} from operation {1}.", operationResponse.ReturnCode, operationResponse.OperationCode));
    }

    public void OnUnexpectedStatusCode(StatusCode statusCode)
    {
        _controlledView.LogError(string.Format("unexpected Status {0}", statusCode));
    }

    public void OnDisconnected(string message)
    {
        _controlledView.Disconnected(message);
    }
    #endregion

    public void ApplicationQuit()
    {
        PhotonEngine.Instance.Disconnect();
    }

    public bool IsConnected
    {
        get { return PhotonEngine.Instance.State is Connected; }
    }

    public void Connect()
    {
        if(!IsConnected)
        {
            PhotonEngine.Instance.Initialize();
        }
    }
}
