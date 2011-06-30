using UnityEngine;
using ExitGames.Client.Photon;

public class Login : GameView
{
    private string _username = "";
    private string _password = "";

    private WaitingForConnect _waitingForConnect;
    private Connected _connected;

    protected override void Awake()
    {
        base.Awake();

        _waitingForConnect = new WaitingForConnect(this);
        _connected = new Connected(this);

        SetDisconnected();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 116, 100, 100), "Userame: ");
        _username = GUI.TextField(new Rect(100, 116, 200, 20), _username, 25);
        GUI.Label(new Rect(10, 141, 100, 100), "Password: ");
        _password = GUI.PasswordField(new Rect(100, 141, 200, 20), _password, '*', 25);

        GUI.Label(new Rect(10, 225, 400, 100), _engine.State.ToString());

        if (GUI.Button(new Rect(100, 165, 100, 25), "Login") || (Event.current.type == EventType.keyDown && Event.current.character == '\n'))
        {
            var peer = new PhotonPeer(_engine, false);

            _engine.Initialize(peer, "localhost:5055", "AegisBorn");
            _engine.StateController = _waitingForConnect;
        }
        if (GUI.Button(new Rect(100, 195, 100, 25), "Logout"))
        {
            _engine.Disconnect();
        }
    }

    public void AfterKeysExchanged()
    {
        LoginOperations.Login(_engine, _username, _password);
    }

    public override void OnConnect(IGameStateController game)
    {
        base.OnConnect(game);
        _engine.StateController = _connected;
        _engine.Peer.OpExchangeKeysForEncryption();
    }

    public void AfterLogin()
    {
        Application.LoadLevel("CharacterSelect");
    }
}
