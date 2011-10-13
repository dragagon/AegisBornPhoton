using System;
using UnityEngine;

public abstract class View : MonoBehaviour, IView
{
    #region Implementation of IView

    private IViewController _controller;

    public IViewController Controller
    {
        get { return _controller; }
        protected set { _controller = value; }
    }

    public virtual void LogDebug(string message)
    {
        Debug.Log(message);
    }

    public virtual void LogError(string message)
    {
        Debug.Log(message);
    }

    public virtual void LogError(Exception exception)
    {
        Debug.Log(exception.ToString());
    }

    public virtual void LogInfo(string message)
    {
        Debug.Log(message);
    }

    public void Disconnected(string message)
    {
        // Show dialog stating reason for disconnection
        if(!String.IsNullOrEmpty(message))
        {
            
        }
        Application.LoadLevel(0);
    }
    #endregion

    public virtual void OnApplicationQuit()
    {
        Controller.ApplicationQuit();
    }
}
