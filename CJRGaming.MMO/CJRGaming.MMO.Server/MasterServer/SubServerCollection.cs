using System;
using System.Collections.Generic;
using System.Linq;

namespace CJRGaming.MMO.Server.MasterServer
{
    public class SubServerCollection : Dictionary<Guid, IncomingSubServerPeer>
    {
        #region Constants and Fields

        public IncomingSubServerPeer LoginServer { get; protected set; }

        public IncomingSubServerPeer ChatServer { get; protected set; }

        #endregion

        #region Public Methods

        public void OnConnect(IncomingSubServerPeer gameServerPeer)
        {
            if (!gameServerPeer.ServerId.HasValue)
            {
                throw new InvalidOperationException("server id cannot be null");
            }

            Guid id = gameServerPeer.ServerId.Value;

            lock (this)
            {
                IncomingSubServerPeer peer;
                if (TryGetValue(id, out peer))
                {
                    peer.Disconnect();
                    Remove(id);
                }

                Add(id, gameServerPeer);

                // 1) If we support the Chat type and the chat server is empty OR
                // 2) if we are a full chat server and the current chat server does more than one thing
                // Make this the chat server.
                if (((gameServerPeer.Type & SubServerType.Chat) == SubServerType.Chat && ChatServer == null || ChatServer.ServerId == null) ||

                    (gameServerPeer.Type == SubServerType.Chat && (ChatServer.Type & SubServerType.Region) == SubServerType.Region ||
                    (ChatServer.Type & SubServerType.Login) == SubServerType.Login))
                {
                    ChatServer = gameServerPeer;
                }
                // 1) If we support the Login type and the login server is empty OR
                // 2) if we are a full login server and the current login server does more than one thing
                // Make this the login server.
                if(((gameServerPeer.Type & SubServerType.Login) == SubServerType.Login && LoginServer == null || LoginServer.ServerId == null) ||
                        
                    (gameServerPeer.Type == SubServerType.Login && (LoginServer.Type & SubServerType.Region) == SubServerType.Region ||
                    (LoginServer.Type & SubServerType.Chat) == SubServerType.Chat))
                {
                    LoginServer = gameServerPeer;
                }
            }
        }

        public void OnDisconnect(IncomingSubServerPeer gameServerPeer)
        {
            if (!gameServerPeer.ServerId.HasValue)
            {
                throw new InvalidOperationException("server id cannot be null");
            }

            Guid id = gameServerPeer.ServerId.Value;

            lock (this)
            {
                IncomingSubServerPeer peer;
                if (!TryGetValue(id, out peer)) return;
                if (peer == gameServerPeer)
                {
                    Remove(id);
                    // We just removed a server, we need to see if it is a chat or login server, and if it was, we need to find a new one.
                    if(id == ChatServer.ServerId)
                    {
                        ChatServer = Values.Where(subServerPeer => subServerPeer.Type == SubServerType.Chat).FirstOrDefault() ??
                                      Values.Where(subServerPeer => (subServerPeer.Type & SubServerType.Chat) == SubServerType.Chat).FirstOrDefault();
                    }
                    
                    if (id == LoginServer.ServerId)
                    {
                        LoginServer = Values.Where(subServerPeer => subServerPeer.Type == SubServerType.Login).FirstOrDefault() ??
                                      Values.Where(subServerPeer => (subServerPeer.Type & SubServerType.Login) == SubServerType.Login).FirstOrDefault();
                    }
                }
            }
        }

        #endregion
    }
}
