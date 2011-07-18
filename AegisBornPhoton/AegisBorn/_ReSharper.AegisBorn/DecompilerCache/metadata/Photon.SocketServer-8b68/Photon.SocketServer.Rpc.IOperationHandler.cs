// Type: Photon.SocketServer.Rpc.IOperationHandler
// Assembly: Photon.SocketServer, Version=2.4.7.0, Culture=neutral, PublicKeyToken=48c2fa3b6988090e
// Assembly location: C:\Photon\lib\Photon.SocketServer.dll

using Photon.SocketServer;

namespace Photon.SocketServer.Rpc
{
    public interface IOperationHandler
    {
        void OnDisconnect(Peer peer);
        void OnDisconnectByOtherPeer(Peer peer);
        OperationResponse OnOperationRequest(Peer peer, OperationRequest operationRequest);
    }
}
