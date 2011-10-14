using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Server.MasterServer;

namespace CJRGaming.MMO.Server.SubServer.Types
{
    public class ChatLoginRegionServer : SubServer
    {
        public ChatLoginRegionServer()
        {
            ServerType = SubServerType.Chat | SubServerType.Login | SubServerType.Region;
        }

        #region Overrides of SubServer

        public override void AddHandlers()
        {
            
        }

        #endregion
    }
}
