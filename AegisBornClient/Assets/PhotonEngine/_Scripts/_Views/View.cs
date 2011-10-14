using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour, IView
{
    #region Implementation of IView

    public virtual void Awake()
    {
        Controller = new ViewController(this);
    }

    public abstract IViewController Controller { get; protected set; }

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
