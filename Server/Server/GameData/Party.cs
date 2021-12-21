using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.GameData
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

        public static void AddMember(int idTeam, int idMember)
        {
            //member đã có team
            if (IsMemberHasParty(idMember) != -1)
            {
                //Send to Leader là idMember đã có team
                string message = Server.clients[idMember].player.username + " đã có team!"; 
                ChatSystem.MessageCallbackToClient(GAMEDATA.Partys[idTeam].Leader, message);
                return;
            }

            // team full?
            if (GAMEDATA.Partys[idTeam].Member.Count == GAMEDATA.MAX_PARTY_MEMBERS)
            {
                //Send to idMember là team đã full
                string message = "Party full!";
                ChatSystem.MessageCallbackToClient(idMember, message);
                return;
            }

            //add user có idMember vào team có  idTeam            
            MiniPlayer newPlayer = new MiniPlayer();
            newPlayer.id = idMember;
            newPlayer.username = Server.clients[idMember].player.username;
            newPlayer.hp = Server.clients[idMember].player.stat.health;
            newPlayer.hpLeft = Server.clients[idMember].player.stat.healthLeft;
            newPlayer.mp = Server.clients[idMember].player.stat.mana;
            newPlayer.mpLeft = Server.clients[idMember].player.stat.manaLeft;
            newPlayer.mapId = Server.clients[idMember].player.mapID;

            GAMEDATA.Partys[idTeam].Member.Add(newPlayer);


            //send message [TEAM] member has joined 
            ChatRec messTeam = new ChatRec();
            messTeam.chatType = ChatType.Party;
            messTeam.message.line = 1;
            messTeam.idClient = GAMEDATA.Partys[idTeam].Leader;
            messTeam.target = idTeam;

            for (int i = 0; i < GAMEDATA.Partys[idTeam].Member.Count; i++)
            {
                messTeam.message.text = Server.clients[idMember].player.username + " has joined team";                
                ServerSend.SendListPlayerToClient(GAMEDATA.Partys[idTeam].Member[i].id, GAMEDATA.Partys[idTeam]);
            }
            ChatSystem.MessageParse(messTeam);
        }

        public static void CreateParty(int idLeader, int idMember)
        {
            for(int i = 0; i < GAMEDATA.Partys.Length; i++)
            {
                //member đã có team
                if (IsMemberHasParty(idMember) != -1)
                {
                    //Send to Leader là idMember đã có team
                    string message = Server.clients[idMember].player.username + " đã có team!";
                    ChatSystem.MessageCallbackToClient(idLeader, message);
                    return;
                }

                if (GAMEDATA.Partys[i].Member == null)
                    GAMEDATA.Partys[i].Member = new List<MiniPlayer>();

                //chưa có thì create party
                if (GAMEDATA.Partys[i].Member.Count == 0)
                {
                    GAMEDATA.Partys[i].Leader = idLeader;
                    AddMember(i, idLeader);
                    AddMember(i, idMember);

                    int idTeam = i;

                    for (int z = 0; z < GAMEDATA.Partys[idTeam].Member.Count; z++)
                    {
                        //messTeam.message.text = Server.clients[GAMEDATA.Partys[idTeam].Member[z]].player.username + " has joined team";
                        
                        ServerSend.SendListPlayerToClient(GAMEDATA.Partys[idTeam].Member[z].id, GAMEDATA.Partys[idTeam]);
                    }
                    //ChatSystem.MessageParse(messTeam);

                    break;
                }
            }
        }

        public static void SmartParty(int idLeader, int idMember)
        {
            int idTeam = IsMemberHasParty(idLeader);
            int idTeamMem = IsMemberHasParty(idMember);
            // thằng leader invite chưa có team thì tạo team mới
            if (idTeam == -1 && idTeamMem == -1)
            {
                CreateParty(idLeader, idMember);
            }else // nếu thằng invite có team rồi thì add vào team
            {
                // thằng được mời có team thì báo lại
                if(idTeamMem != -1)
                {
                    string message = "Người này đã có Party!";
                    ChatSystem.MessageCallbackToClient(idLeader, message);
                    return;
                }

                // nếu thằng leader invite không phải là leader thì gửi mess là k phải lead nên k đc invite
                if(GAMEDATA.Partys[idTeam].Leader != idLeader)
                {
                    string message = "Bạn không phải Leader!";
                    ChatSystem.MessageCallbackToClient(idLeader, message);
                    return;
                }

                // nếu mà đúng là lead thì nó thoát if và xuống add member
                AddMember(idTeam, idMember);
            }

        }

        /// <summary>
        /// check xem thằng có id này có trong party nào không.
        /// nếu có return id party nếu k thì return -1
        /// </summary>
        /// <param name="idMember"></param>
        /// <returns> idTeam hoặc -1(không có team)</returns>
        public static int IsMemberHasParty(int idMember)
        {
            for (int i = 0; i < GAMEDATA.Partys.Length; i++)
            {
                if (GAMEDATA.Partys[i].Member == null)
                {
                    GAMEDATA.Partys[i].Member = new List<MiniPlayer>();
                }

                //next khi mà party trống
                if (GAMEDATA.Partys[i].Member.Count == 0)
                    continue;

                // nếu party không trống thì check mem từng team có thằng này chưa
                // nếu chưa thì return false, rồi thì true
                for (int z = 0; z< GAMEDATA.Partys[i].Member.Count; z++)
                {
                    if (GAMEDATA.Partys[i].Member[z].id == idMember)
                        return i;
                    else
                        continue;
                } 
            }

            return -1;
        }


        // leave hoặc bị kick bởi leader
        public static void LeaveParty(int idMember)
        {
            ChatRec chatRec = new ChatRec();
            chatRec.message.line = 1;
            chatRec.chatType = ChatType.Party;

            //member chưa có team
            if (IsMemberHasParty(idMember) == -1)
            {
                //Send to idMember chưa có team để rời 
                string message = "Bạn chưa có team!";
                ChatSystem.MessageCallbackToClient(idMember, message);
                return;
            }

            int idTeam = IsMemberHasParty(idMember);
            // nếu mà team mem count =0 hoặc =1 thì remove luôn cái team vì chỉ còn 1 ng
            // hoặc lỗi gì đó mà team không còn ai
            if (GAMEDATA.Partys[idTeam].Member.Count == 0 || GAMEDATA.Partys[idTeam].Member.Count == 1)
            {   
                for (int i = 0; i< GAMEDATA.Partys[idTeam].Member.Count; i++)
                {
                    ChatSystem.MessageCallbackToClient(GAMEDATA.Partys[idTeam].Member[i].id, "Party đã giải tán!");
                    ServerSend.SendListPlayerToClient(GAMEDATA.Partys[idTeam].Member[i].id, new PartyRec());
                }
                GAMEDATA.Partys[idTeam] = new PartyRec();
                return;
            }
            else
            {
                // remove thằng muốn rời
                MiniPlayer player = GAMEDATA.Partys[idTeam].Member.Find(x => x.id == idMember);
                GAMEDATA.Partys[idTeam].Member.Remove(player);

                ChatSystem.MessageCallbackToClient(idMember, "Bạn đã rời khỏi Party");
                ServerSend.SendListPlayerToClient(idMember, new PartyRec());

                if (GAMEDATA.Partys[idTeam].Member.Count == 0 || GAMEDATA.Partys[idTeam].Member.Count == 1)
                {
                    for (int i = 0; i < GAMEDATA.Partys[idTeam].Member.Count; i++)
                    {
                        ChatSystem.MessageCallbackToClient(GAMEDATA.Partys[idTeam].Member[i].id, "Party đã giải tán!");
                        ServerSend.SendListPlayerToClient(GAMEDATA.Partys[idTeam].Member[i].id, new PartyRec());
                    }
                    GAMEDATA.Partys[idTeam] = new PartyRec();
                    return;
                }

                chatRec.message.text = Server.clients[idMember].player.username + " đã rời khỏi Party";

                if (GAMEDATA.Partys[idTeam].Leader == idMember)
                {                    
                    GAMEDATA.Partys[idTeam].Leader = GAMEDATA.Partys[idTeam].Member[0].id;
                    chatRec.message.text += "\n" + 
                        Server.clients[GAMEDATA.Partys[idTeam].Leader].player.username + 
                        " trở thành đội trưởng!";
                    chatRec.message.line = 2;
                    chatRec.idClient = GAMEDATA.Partys[idTeam].Leader;
                    chatRec.target = idTeam;
                }

                for (int i = 0; i < GAMEDATA.Partys[idTeam].Member.Count; i++)
                {
                    ServerSend.SendListPlayerToClient(GAMEDATA.Partys[idTeam].Member[i].id, GAMEDATA.Partys[idTeam]);
                }
                
            }

            ChatSystem.MessageParse(chatRec);
        }

    }
}
