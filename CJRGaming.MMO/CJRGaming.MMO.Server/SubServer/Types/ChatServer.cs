using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Server.MasterServer;

namespace CJRGaming.MMO.Server.SubServer.Types
{
    public class ChatServer : SubServer
    {
        public ChatServer()
        {
            ServerType = SubServerType.Chat;
        }
    }
}
