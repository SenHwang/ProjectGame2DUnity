using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.GameData
{
    [Serializable]
    public enum Role
    {
        Player = 1,
        Tester,
        Mapper,
        Moderator,
        GameMater,
        Developer,
        Admin
    }

}
