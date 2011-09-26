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
                    // We just removed a server, we need to see if it is a chat or login server, and if it was, null it out.
                    if (id == ChatServer.ServerId)
                    {
                        ChatServer = null;
                    }
                    if (id == LoginServer.ServerId)
                    {
                        LoginServer = null;
                    }
                }

                Add(id, gameServerPeer);

                ResetServers();
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
                    // We just removed a server, we need to see if it is a chat or login server, and if it was, null it out.
                    if (id == ChatServer.ServerId)
                    {
                        ChatServer = null;
                    }
                    if (id == LoginServer.ServerId)
                    {
                        LoginServer = null;
                    }
                    ResetServers();
                }
            }
        }

        public void ResetServers()
        {
            // if the server is something other than a full chat server - find a full chat server if one exists.
            if(ChatServer != null && ChatServer.Type != SubServerType.Chat)
            {
                IncomingSubServerPeer peer =
                    Values.Where(subServerPeer => subServerPeer.Type == SubServerType.Chat).FirstOrDefault();
                if(peer != null)
                {
                    ChatServer = peer;
                }
            }
            // if the server is something other than a full chat server - find a full chat server if one exists.
            if (LoginServer != null && LoginServer.Type != SubServerType.Login)
            {
                IncomingSubServerPeer peer =
                    Values.Where(subServerPeer => subServerPeer.Type == SubServerType.Login).FirstOrDefault();
                if (peer != null)
                {
                    LoginServer = peer;
                }
            }
            // We just removed a server, we need to see if it is a chat or login server, and if it was, we need to find a new one.
            if (ChatServer == null || ChatServer.ServerId == null)
            {
                // Check if there is a full chat server in the list of servers
                ChatServer = Values.Where(subServerPeer => subServerPeer.Type == SubServerType.Chat).FirstOrDefault() ??
                    // If no full chat server exists, find one that supports the chat server.
                              Values.Where(subServerPeer => (subServerPeer.Type & SubServerType.Chat) == SubServerType.Chat).FirstOrDefault();
            }

            if (LoginServer == null || LoginServer.ServerId == null)
            {
                // Check if there is a full login server in the list of servers
                LoginServer = Values.Where(subServerPeer => subServerPeer.Type == SubServerType.Login).FirstOrDefault() ??
                    // If no full login server exists find one that supports the chat server.
                              Values.Where(subServerPeer => (subServerPeer.Type & SubServerType.Login) == SubServerType.Login).FirstOrDefault();
            }
            
        }

        #endregion
    }
}
