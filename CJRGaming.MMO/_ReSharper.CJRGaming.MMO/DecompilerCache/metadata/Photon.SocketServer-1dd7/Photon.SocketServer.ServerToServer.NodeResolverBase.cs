// Type: Photon.SocketServer.ServerToServer.NodeResolverBase
// Assembly: Photon.SocketServer, Version=3.0.6.2188, Culture=neutral, PublicKeyToken=48c2fa3b6988090e
// Assembly location: C:\photon\lib\Photon.SocketServer.dll

using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using System.Net;

namespace Photon.SocketServer.ServerToServer
{
    public abstract class NodeResolverBase : ApplicationBase, IPhotonResolver, IPhotonResolverControl, IPhotonControl
    {
        protected NodeResolverBase();
        public bool IsResolver { get; }

        #region IPhotonResolver Members

        void IPhotonResolver.NodeConnected(byte endNodeId, short port);
        void IPhotonResolver.NodeDisconnected(byte endNodeId, short port);

        #endregion

        #region IPhotonResolverControl Members

        void IPhotonResolverControl.OnPhotonRunning();
        IPhotonResolver IPhotonResolverControl.OnStart(IPhotonResolverSink sink);
        void IPhotonResolverControl.OnStop();
        void IPhotonResolverControl.OnStopRequested();

        #endregion

        protected void AddNode(byte nodeId, IPAddress address);
        protected void ChangeNode(byte nodeId, IPAddress address);
        protected abstract void OnNodeConnected(byte nodeId, int port);
        protected abstract void OnNodeDisconnected(byte nodeId, int port);
        protected void RemoveNode(byte nodeId);
    }
}
