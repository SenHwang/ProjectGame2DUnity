using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Types
{
    [Serializable]
    public enum ChatType
    {
        Map = 1,
        DM,
        Party,
        Clan,
        World,
        InvitePlayer,
        Callback
    }

    [Serializable]
    public struct Message
    {
        public bool IsEmoji;        
        public int line;
        public string text;
    }

    [Serializable]
    public struct ChatRec
    {
        public int idClient;
        public ChatType chatType;
        public Message message;
        public int target;
    }
}
