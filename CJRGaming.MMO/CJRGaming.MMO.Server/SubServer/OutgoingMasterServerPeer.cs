using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Common;
using CJRGaming.MMO.Server.Server2Server.Operations;
using ExitGames.Logging;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using OperationCode = CJRGaming.MMO.Server.Server2Server.Operations.OperationCode;

namespace CJRGaming.MMO.Server.SubServer
{
    public class OutgoingMasterServerPeer : ServerPeerBase
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();

        private readonly SubServer _application;

        private IDisposable _updateLoop;

        public OutgoingMasterServerPeer(IRpcProtocol protocol, IPhotonPeer nativePeer, SubServer subServer)
            : base(protocol, nativePeer)
        {
            _application = subServer;
            Log.InfoFormat("connection to master at {0}:{1} established (id={2})", RemoteIP, RemotePort, ConnectionId);
            RequestFiber.Enqueue(Register);
        }

        #region Properties

        protected bool IsRegistered { get; set; }

        #endregion


        protected virtual void Register()
        {
            // We send a message to the Master Server and tell it what our information is, including type
            SendOperationRequest((byte) OperationCode.RegisterGameServer,
                                 new RegisterSubServer()
                                     {
                                         GameServerAddress = _application.PublicIpAddress.ToString(),
                                         TcpPort = _application.GamingTcpPort,
                                         UdpPort = _application.GamingUdpPort,
                                         ServerId = SubServer.ServerId,
                                         ServerType = (int) _application.ServerType
                                     },
                                 new SendParameters());
        }

        protected void StartUpdateLoop()
        {
            if (_updateLoop != null)
            {
                Log.Error("Update Loop already started! Duplicate RegisterGameServer response?");
                _updateLoop.Dispose();
            }

            // We want the master server to know that we are still alive, so poke it every second.
            //_updateLoop = RequestFiber.ScheduleOnInterval(UpdateServerState, 1000, 1000);
        }

        protected void StopUpdateLoop()
        {
            if (_updateLoop != null)
            {
                _updateLoop.Dispose();
                _updateLoop = null;
            }
        }

        #region Overrides of PeerBase

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            Log.DebugFormat("Received operation request from master server: "+new Guid((Byte[])operationRequest.Parameters[(byte)ParameterCode.UserId]));

            throw new NotImplementedException();
        }

        protected override void OnDisconnect()
        {
            Log.InfoFormat("connection to master closed (id={0})", this.ConnectionId);

            IsRegistered = false;
            StopUpdateLoop();
            _application.ReconnectToMaster();
        }

        #endregion

        #region Overrides of ServerPeerBase

        protected override void OnEvent(IEventData eventData, SendParameters sendParameters)
        {
            throw new NotImplementedException();
        }

        protected override void OnOperationResponse(OperationResponse operationResponse, SendParameters sendParameters)
        {
            // When we successfully register, start our update loop to keep the master server aware that we are still up and running.
            switch ((OperationCode)operationResponse.OperationCode)
            {
                default:
                    {
                        if (Log.IsDebugEnabled)
                        {
                            Log.DebugFormat("Received unknown operation code {0}", operationResponse.OperationCode);
                        }

                        break;
                    }

                case OperationCode.RegisterGameServer:
                    {
                        if (operationResponse.ReturnCode != 0)
                        {
                            Log.WarnFormat("Failed to register at master: err={0}, msg={1}", operationResponse.ReturnCode, operationResponse.DebugMessage);
                            this.Disconnect();
                            return;
                        }

                        Log.Info("Successfully registered at master server");

                        IsRegistered = true;
                        StartUpdateLoop();
                        break;
                    }
            }
        }

        #endregion
    }
}
