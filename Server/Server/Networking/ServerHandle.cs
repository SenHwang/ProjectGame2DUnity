using Server.Data;
using Server.GameData;
using Server.ObjectGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Server
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clienIdCheck = _packet.ReadInt();

            int length = _packet.ReadInt();
            Byte[] _msg = _packet.ReadBytes(length);

            UTF8Encoding utf8 = new UTF8Encoding();
            String decodedString = utf8.GetString(_msg);

            string _username = decodedString;
            List<Client> c;
            c = Server.clients.Values.Where(x => x.player != null ).ToList();
            if (c.Count != 0)
            {
                Client get = c.SingleOrDefault(x => x.player.username == _username);
                if (get != null)
                {
                    ServerSend.Welcome(_fromClient, "[Error] User đang trong trạng thái login!");
                    return;
                }
            }

            TaskQueue.listMessage.Add($"Server: {_username} connection successfully and this is now player {_fromClient}.");          
            
            if (_fromClient != _clienIdCheck)
                TaskQueue.listMessage.Add($"Server: Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clienIdCheck})");

            Server.clients[_fromClient].SendIntoGame(_username);   
        }

        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
             Vector3 _position = _packet.ReadVector3();

            int _speedAnim = _packet.ReadInt();
            int _spriteStop = _packet.ReadInt();

            bool _isPunching = _packet.ReadBool();
            bool _isDead = _packet.ReadBool();
            bool _isKick = _packet.ReadBool();

            if (Server.clients[_fromClient].player == null) return;
            Server.clients[_fromClient].player.SetInput(_position, _speedAnim, _spriteStop, _isPunching, _isDead, _isKick);     
        }

        public static void PlayerEquipment(int _fromClient, Packet _packet)
        {
            string msg = "";
            int _clienIdCheck = _packet.ReadInt();
            
            string[] _inputs = new string[_packet.ReadInt()];
            for (int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i] = _packet.ReadString();
                msg += _inputs[i] + "|";
            }

            if (_clienIdCheck == _fromClient)
            {
                Server.clients[_clienIdCheck].player.SetEquipment(_inputs);
            }                
        }
            
        public static void MessageFromClientReceived(int _fromClient, Packet _packet)
        {
            int length = _packet.ReadInt();
            Byte[] _msg = _packet.ReadBytes(length);

            UTF8Encoding utf8 = new UTF8Encoding();
            string decodedString = utf8.GetString(_msg);

            var serializer = new JavaScriptSerializer();
            ChatRec dataResult = serializer.Deserialize<ChatRec>(decodedString);

            ChatSystem.MessageParse(dataResult);
        }

        internal static void RQArrowFromClient(int _fromClient, Packet _packet)
        {
            int _id = _packet.ReadInt();
            int _spriteStop = _packet.ReadInt();
            float _length = _packet.ReadFloat();
            float speed = _packet.ReadFloat();
            Vector3 position = _packet.ReadVector3();
            int map = Server.clients[_id].player.mapID;

            Map.CreateNewArrow(_id, map, _spriteStop, _length, speed, position);
            //Server.clients[_id].player.CreateNewArrow(_spriteStop,_length, speed, position);
        }

        internal static void PlayerSkillHitted(int _fromClient, Packet _packet)
        {
            //khi _idSkill == "" thì tack dmg và trả về luôn
            
            //nếu find  !=  null thì 
            //    check thằng bắn ra skill này
            //        nếu target != -1 thì call dmg

            int _idOwner = _packet.ReadInt();
            string _idSkill = _packet.ReadString();
            int _target = _packet.ReadInt();
            int _map = _packet.ReadInt();
            if(_idSkill == "")
            {
                Server.clients[_target].player.PlayerTakeDamage(10);
                return;
            }

            ArrowSkill find = Map.skillsInMap[_map].FirstOrDefault(x => x.idSkill == _idSkill);
            if (find != null)
            {                
                if (_target != find.idOwner)
                {
                    if (_target != -1)
                    {
                        Server.clients[_target].player.PlayerTakeDamage(10);
                    } 

                    ServerSend.SendBackDataArrowToClient(find.idOwner, find.idSkill, find.poss, false, _map);
                    Map.skillsInMap[_map].Remove(find);
                }
            }
        }

        internal static void PlayerTeleMap(int _fromClient, Packet _packet)
        {
            int mapID = _packet.ReadInt();
            Vector3 telePoss = _packet.ReadVector3();
            ServerSend.RefreshMap(_fromClient, mapID, telePoss);
        }

        internal static void PlayerRQUpdateStats(int _fromClient, Packet _packet)
        {
            int value = _packet.ReadInt();

            if (value < 1 || value > 5) return;
            if (Server.clients[_fromClient].player.stat.pointFree == 0) return;

            if (value == 1)
            {
                Server.clients[_fromClient].player.stat.strength++; Server.clients[_fromClient].player.stat.pointFree--;
            }
            if (value == 2)
            {
                Server.clients[_fromClient].player.stat.agility++; Server.clients[_fromClient].player.stat.pointFree--;
            }
            if (value == 3)
            {
                Server.clients[_fromClient].player.stat.intellect++; Server.clients[_fromClient].player.stat.pointFree--;
            }
            if (value == 4)
            {
                Server.clients[_fromClient].player.stat.mp++; Server.clients[_fromClient].player.stat.pointFree--;
            }
            if (value == 5)
            {
                Server.clients[_fromClient].player.stat.hp++; Server.clients[_fromClient].player.stat.pointFree--;
            }

            ServerSend.UpdateStatPlayer(Server.clients[_fromClient].player);
        }

        internal static void ClientCreateAnimation(int _fromClient, Packet _packet)
        {
            Vector3 poss = _packet.ReadVector3();

            int len = _packet.ReadInt();
            byte[] dataByte = _packet.ReadBytes(len);

            string stringJson = Unzip(dataByte);
            var serializer = new JavaScriptSerializer();
            AnimationCalledStruct dataResult = serializer.Deserialize<AnimationCalledStruct>(stringJson);

            ServerSend.ServerSendAnimationToAllClientInMap(_fromClient, poss, dataResult);
        }

        #region admin request packet
        public enum TypeItemCall
        {
            Item,
            Skill
        }
        internal static void AdminRequestItem(int _fromClient, Packet _packet)
        {
            int idItem = _packet.ReadInt();
            int count = _packet.ReadInt();
            int type = _packet.ReadInt();
            if((int)TypeItemCall.Item == type)
                Server.clients[_fromClient].player.AddItemInventory(idItem, count);
            else if((int)TypeItemCall.Skill == type)
                Server.clients[_fromClient].player.AddSkillInventory(idItem);
        }

        internal static void AdminSendSaveNPC(int _fromClient, Packet _packet)
        {
            int index = _packet.ReadInt();

            int length = _packet.ReadInt();
            Byte[] _string = _packet.ReadBytes(length);

            UTF8Encoding utf8 = new UTF8Encoding();
            string decodedString = utf8.GetString(_string);

            var serializer = new JavaScriptSerializer();
            NPC npc = serializer.Deserialize<NPC>(decodedString);
            GAMEDATA.npcs[index] = npc;
        }

        internal static void AdminSendSaveMap(int _fromClient, Packet _packet)
        {
            //TODO if(_fromClient != admin) return;

            int length = _packet.ReadInt(); // đọc length packet rồi read byte theo length
            Byte[] _byte = _packet.ReadBytes(length);

            string decodedString = UTF8Decoded(_byte); // decode UTF8

            var serializer = new JavaScriptSerializer();
            MapStruct mapSave = serializer.Deserialize<MapStruct>(decodedString);

            Map.SavingMapFromAdmin(mapSave);
        }
        internal static void ClientRQSpawnAnim(int _fromClient, Packet _packet)
        {
            int id = _fromClient;

            Vector3 idAnim = _packet.ReadVector3(); // đọc length packet rồi read byte theo length
            
            int len = _packet.ReadInt();
            Byte[] _byte = _packet.ReadBytes(len);
            string JsonString = Unzip(_byte);

            var serializer = new JavaScriptSerializer();
            AnimationCalledStruct animCall = serializer.Deserialize<AnimationCalledStruct>(JsonString);

            //TODO: process anim and send to all client in map
        }

        #endregion


        /// <summary>
        /// cần một byte array đọc của packet để decode. 
        /// Đầu ra là một chuỗi string đã được decoded
        /// </summary>
        /// <param name="_byte"></param>
        /// <returns></returns>
        public static string UTF8Decoded(Byte[] _byte)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            return utf8.GetString(_byte);            
        }

        /// <summary>
        /// đầu vào là một chuỗi string để encoded thành một byte array
        /// </summary>
        /// <param name="_string"></param>
        /// <returns></returns>
        public static Byte[] UTF8Encoded(string _string)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            return utf8.GetBytes(_string);
        }

        #region Zip n Unzip String
        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        /// <summary>
        /// return byte[] with Zip("string")
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        /// <summary>
        /// return string with Unzip(byte[])
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }
        #endregion

    }
}
