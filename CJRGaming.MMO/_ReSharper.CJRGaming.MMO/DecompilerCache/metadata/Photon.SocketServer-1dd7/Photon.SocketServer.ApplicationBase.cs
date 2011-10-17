// Type: Photon.SocketServer.ApplicationBase
// Assembly: Photon.SocketServer, Version=3.0.6.2188, Culture=neutral, PublicKeyToken=48c2fa3b6988090e
// Assembly location: C:\photon\lib\Photon.SocketServer.dll

using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using System.Net;

namespace Photon.SocketServer
{
    public abstract class ApplicationBase : IPhotonApplication, IPhotonApplicationControl, IPhotonControl
    {
        protected ApplicationBase();
        public string ApplicationName { get; }
        public string ApplicationPath { get; }
        public string BinaryPath { get; }
        public int PeerCount { get; }
        public string PhotonInstanceName { get; }
        public bool Running { get; }

        #region IPhotonApplication Members

        void IPhotonApplication.OnDisconnect(IPhotonPeer photonPeer, object userData);

        void IPhotonApplication.OnFlowControlEvent(IPhotonPeer photonPeer, object userData,
                                                   FlowControlEvent flowControlEvent);

        void IPhotonApplication.OnInit(IPhotonPeer nativePeer, byte[] data);
        void IPhotonApplication.OnOutboundConnectionEstablished(IPhotonPeer photonPeer, object userData);

        void IPhotonApplication.OnOutboundConnectionFailed(IPhotonPeer photonPeer, object userData, int errorCode,
                                                           string errorMessage);

        void IPhotonApplication.OnReceive(IPhotonPeer photonPeer, object userData, byte[] data, short invocationId,
                                          MessageReliablity reliability, byte channelId);

        #endregion

        #region IPhotonApplicationControl Members

        void IPhotonApplicationControl.OnPhotonRunning();

        IPhotonApplication IPhotonApplicationControl.OnStart(string instanceName, string applicationName,
                                                             IPhotonApplicationSink photonApplicationSink);

        void IPhotonApplicationControl.OnStop();
        void IPhotonApplicationControl.OnStopRequested();
        void IPhotonControl.OnPhotonRunning();
        void IPhotonControl.OnStop();
        void IPhotonControl.OnStopRequested();

        #endregion

        public bool ConnectToServer(IPEndPoint remoteEndpoint, string applicationName, object state);
        protected internal virtual ServerPeerBase CreateServerPeer(InitResponse initResponse, object state);
        protected abstract PeerBase CreatePeer(InitRequest initRequest);
        protected virtual void OnServerConnectionFailed(int errorCode, string errorMessage, object state);
        protected virtual void OnStopRequested();
        protected abstract void Setup();
        protected abstract void TearDown();
    }
}
