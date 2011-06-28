using AegisBornCommon;
using ExitGames.Client.Photon;
using System.Collections;

public class PhotonClient : IPhotonPeerListener
{

    private static PhotonClient _instance;

    private PhotonPeer _peer;

    private IGameStateController _stateStrategy;

    private PhotonClient()
    {
    }

    public static PhotonClient Instance
    {
        get { return _instance ?? (_instance = new PhotonClient()); }
    }

    public PhotonPeer Peer
    {
        get { return _peer; }
    }

    public GameState State
    {
        get
        {
            return _stateStrategy.State;
        }

    }

    public IGameStateController StateController
    {
        get { return _stateStrategy; }
        set { _stateStrategy = value; }
    }

    #region Inherited Interfaces

    #region IPhotonPeerListener

    public void DebugReturn(DebugLevel level, string message)
    {
        _stateStrategy.OnDebugReturn(level, message);
    }

    public void EventAction(byte eventCode, Hashtable photonEvent)
    {
        _stateStrategy.OnEventReceive(this, (EventCode)eventCode, photonEvent);
    }

    public void OperationResult(byte operationCode, int returnCode, Hashtable returnValues, short invocId)
    {
        _stateStrategy.OnOperationReturn(this, (OperationCode)operationCode, returnCode, returnValues);
    }

    public void PeerStatusCallback(StatusCode returnCode)
    {
        _stateStrategy.OnPeerStatusCallback(this, returnCode);
    }

    #endregion

    #endregion

    public void Initialize(PhotonPeer peer, string serverAddress, string applicationName)
    {
        _peer = peer;
        peer.Connect(serverAddress, applicationName);
    }

    public void Disconnect()
    {
        if (Peer != null)
        {
            Peer.Disconnect();
        }
    }

    public void Update()
    {
        _stateStrategy.OnUpdate(this);
    }

    public void SendOp(OperationCode operationCode, Hashtable parameter, bool sendReliable, byte channelId, bool encrypt)
    {
        _stateStrategy.SendOperation(this, operationCode, parameter, sendReliable, channelId, encrypt);
    }
}
