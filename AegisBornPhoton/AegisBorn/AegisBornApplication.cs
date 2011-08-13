using System;
using System.IO;
using AegisBorn.Models.Base;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Photon.SocketServer;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;

namespace AegisBorn
{
    public class AegisBornApplication : Application
    {

        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILogger log = LogManager.GetCurrentClassLogger();

        protected override IPeer CreatePeer(PhotonPeer photonPeer, InitRequest initRequest)
        {
            return new AegisBornPeer(photonPeer);
        }

        protected override void Setup()
        {
            LogManager.SetLoggerFactory(ExitGames.Logging.Log4Net.Log4NetLoggerFactory.Instance);
            var configFileInfo = new FileInfo("log4net.config");
            XmlConfigurator.ConfigureAndWatch(configFileInfo);

            ErrorHandler.UnexpectedException += ErrorHandler_OnUnexpectedException;
            PhotonPeer.SendBufferFull += PhotonPeer_OnSendBufferFull;
            AppDomain.CurrentDomain.UnhandledException += AppDomain_OnUnhandledException;
        }

        protected override void TearDown()
        {
            
        }

        /// <summary>
        /// Logs unhandled exceptions.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The event args.
        /// </param>
        private static void AppDomain_OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Error(e.ExceptionObject);
        }

        /// <summary>
        /// Logs unexpected errors.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        private static void ErrorHandler_OnUnexpectedException(object source, Exception exception)
        {
            log.Error(exception);
        }

        /// <summary>
        /// Disconnects a client after send buffer full error.
        /// </summary>
        /// <param name="peer">
        /// The client peer.
        /// </param>
        private static void PhotonPeer_OnSendBufferFull(PhotonPeer peer)
        {
            log.WarnFormat("Send data failed: reason=SendBufferFull ConnectionId={0} - disconnected client", peer.NativePeer.GetConnectionID());
            peer.Disconnect();
        }

    }
}
