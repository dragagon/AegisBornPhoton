using System;

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
