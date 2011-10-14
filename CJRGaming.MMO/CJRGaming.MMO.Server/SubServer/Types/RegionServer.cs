using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Server.MasterServer;

namespace CJRGaming.MMO.Server.SubServer.Types
{
    public class RegionServer : SubServer
    {
        public RegionServer()
        {
            ServerType = SubServerType.Region;
        }

        #region Overrides of SubServer

        public override void AddHandlers()
        {
            
        }

        #endregion
    }
}
