using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Common;
using ExitGames.Logging;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;

namespace CJRGaming.MMO.Server.SubServer.Handlers
{
    public class SubServerLoginHandler : PhotonRequestHandler
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public SubServerLoginHandler(ServerPeerBase peer)
            : base(peer)
        {
        }

        #region Overrides of PhotonRequestHandler

        public override void OnHandleRequest(OperationRequest request)
        {
            Log.Debug("SubServerLogin Handler sending response of 1");
            var para = new Dictionary<byte, object>
                           {{(byte) ParameterCode.UserId, request.Parameters[(byte) ParameterCode.UserId]}};
            _peer.SendOperationResponse(new OperationResponse(1) {Parameters = para}, new SendParameters {ChannelId = 0, Unreliable = false});
        }

        #endregion
    }
}
