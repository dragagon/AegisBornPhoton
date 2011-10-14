using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CJRGaming.MMO.Server.MasterServer;

namespace CJRGaming.MMO.Server.SubServer.Types
{
    public class LoginServer : SubServer
    {
        public LoginServer()
        {
            ServerType = SubServerType.Login;
        }

        #region Overrides of SubServer

        public override void AddHandlers()
        {

        }

        #endregion
    }
}
