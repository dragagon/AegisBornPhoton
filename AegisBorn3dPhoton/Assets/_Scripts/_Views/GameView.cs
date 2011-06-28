using System;
using ExitGames.Client.Photon;
using UnityEngine;

public abstract class GameView : MonoBehaviour, IGameView
{
    protected PhotonClient _engine;
    protected GameStateController Controller;
    protected Disconnected _disconnected;

    // Use this for initialization
    protected virtual void Awake()
    {
        _disconnected = new Disconnected(this);

        Application.runInBackground = true;
        _engine = PhotonClient.Instance;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        try
        {
            _engine.Update();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    /// <summary>
    /// The on application quit.
    /// </summary>
    public virtual void OnApplicationQuit()
    {
        try
        {
            _engine.Disconnect();
            _engine.StateController = _disconnected;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    #region Inherited Interfaces

    #region IGameListener

    public virtual bool IsDebugLogEnabled
    {
        get { return true; }
    }

    public virtual void LogDebug(IGameStateController game, string message)
    {
        Debug.Log(message);
    }

    public virtual void LogError(IGameStateController game, string message)
    {
        Debug.Log(message);
    }

    public virtual void LogError(IGameStateController game, Exception exception)
    {
        Debug.Log(exception.ToString());
    }

    public virtual void LogInfo(IGameStateController game, string message)
    {
        Debug.Log(message);
    }

    public virtual void OnConnect(IGameStateController game)
    {
        Debug.Log("connected");
    }

    public virtual void OnDisconnect(IGameStateController game, StatusCode returnCode)
    {
        SetDisconnected();
        Debug.Log("disconnected");
    }

    public void SetDisconnected()
    {
        _engine.StateController = _disconnected;
    }
    #endregion

    #endregion
}
