using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CJRGaming.MMO.Server.MasterServer
{
    [Flags]
    public enum SubServerType
    {
        Login = 1,
        Chat = 2,
        Region = 4,
    }
}
