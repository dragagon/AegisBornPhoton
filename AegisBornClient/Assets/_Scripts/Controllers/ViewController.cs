using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;

public abstract class ViewController : IViewController
{

    private IView _controlledView;

    protected ViewController(IView controlledView)
    {
        _controlledView = controlledView;
        PhotonEngine.Instance.Controller = this;
    }

    #region Implementation of IViewController

    public virtual void OnEvent(EventData eventData)
    {
        OnUnexpectedEvent(eventData);
    }

    public virtual void OnOperationResponse(OperationResponse operationResponse)
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
}
