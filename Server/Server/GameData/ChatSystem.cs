using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.GameData
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

    public class ChatSystem
    {
        public static void MessageParse(ChatRec message)
        {
            if (message.chatType == ChatType.Map)
            {
                message.message.text = "[Map]"+message.message.text;
                for (int i =0; i< Server.clients.Count; i++)
                {
                    if (Server.clients[i].player == null) return;
                    if (Server.clients[i].player.mapID == Server.clients[message.idClient].player.mapID)
                    {
                        //send message to mấy thằng này id bằng sendpakage _target
                        ServerSend.ServerSendMessageToClient(i, message);
                    }
                }
            }
            else if (message.chatType == ChatType.DM)
            {
                message.message.text = "[DM]" + message.message.text;
                if (Server.clients[message.target] == null)
                {
                    //TODO: báo lại thằng ID message.target không onl
                    MessageCallbackToClient(message.idClient, "Người dùng không trực tuyến!");
                    return;
                }
                ServerSend.ServerSendMessageToClient(message.target, message);

            }
            else if (message.chatType == ChatType.Party)
            {
                message.message.text = "[Party]" + message.message.text;
                // check xem thằng này có trong team k k có thì return
                if (Party.IsMemberHasParty(message.idClient) == -1)
                {
                    //báo lại không có team để send message này
                    MessageCallbackToClient(message.idClient, "Bạn chưa tham gia Party!");
                    return; 
                }
                   

                int idTeam = message.target;
                for(int i = 0;i< GAMEDATA.Partys[idTeam].Member.Count; i++)
                {
                    ServerSend.ServerSendMessageToClient(GAMEDATA.Partys[idTeam].Member[i].id, message);
                }

            }
            else if (message.chatType == ChatType.Clan)
            {
                message.message.text = "[Clan]" + message.message.text;
                //TODO: tạo class clan đã rồi làm gì làm
                //TODO: send message to id team message.target
            }
            else if (message.chatType == ChatType.World)
            {
                message.message.text = "[World]" + message.message.text;

                // send message to all player 
                for (int i = 0; i < Server.clients.Count; i++)
                {
                    if (Server.clients[i].player == null) return;
                    ServerSend.ServerSendMessageToClient(i, message);
                }
            }else if(message.chatType == ChatType.InvitePlayer)
            {
                // nếu là lệnh leave thì kick theo id ra
                if(message.message.text == "/leave" && message.message.line == -1)
                {
                    Party.LeaveParty(message.idClient);
                    return;
                }

                int idLeader = message.idClient;// message.message.text                

                Client user = Server.clients.Values.SingleOrDefault(x => x.player != null && x.player.username == message.message.text);

                if (user == null)
                {
                    MessageCallbackToClient(idLeader, "Người dùng không online!");
                    return;
                }

                int idMember = user.player.id;

                Party.SmartParty(idLeader, idMember);              
            }
            else if (message.chatType == ChatType.Callback)
            {
                //send message back to message.idClient với tin nhắn message
                //thường là báo lỗi
                ServerSend.ServerSendMessageToClient(message.idClient, message);
            }
        }

        public static void MessageCallbackToClient(int _clientId,string message)
        {
            ChatRec mess = new ChatRec();
            mess.chatType = ChatType.Callback;
            mess.idClient = _clientId;
            mess.message.line = 1;
            mess.message.text = "Server: "+ message;

            ChatSystem.MessageParse(mess);
        }
    }
}
