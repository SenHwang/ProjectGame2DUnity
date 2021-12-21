using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Types
{
    [Serializable]
    public struct MiniPlayer
    {
        public int id;
        public string username;
        public int hp;
        public int hpLeft;
        public int mp;
        public int mpLeft;
        public int mapId;
    }
    [Serializable]
    public struct PartyRec
    {
        public int Leader;
        public List<MiniPlayer> Member;
    }


    public class Party
    {
        public static bool isMemberTeam(int id, int idHit)
        {
            if (GameManager.players[idHit].party.Leader == 0 && GameManager.players[idHit].party.Member.Count == 0)
                if (GameManager.players[id].party.Leader == 0 && GameManager.players[id].party.Member.Count == 0)
                    return false;

            foreach(MiniPlayer member in GameManager.players[idHit].party.Member)
            {
                if (member.id == id) 
                    return true;
            }

            foreach (MiniPlayer member in GameManager.players[id].party.Member)
            {
                if (member.id == idHit)
                    return true;
            }

            return false;
        }
    }
}
