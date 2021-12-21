using Server.GameData;
using Server.ObjectGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Server
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }
    
        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 0; i < Server.MaxPlayers; i++)
            {
                if (Server.clients[i].udp.endPoint == null)
                    continue;
                Server.clients[i].tcp.SendData(_packet);
            }            
        }

        private static void SendTCPDataToAll(int _exeptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 0; i < Server.MaxPlayers; i++)
            {
                if (Server.clients[i].udp.endPoint == null)
                    continue;
                if (i != _exeptClient)
                    Server.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();            
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 0; i < Server.MaxPlayers; i++)
            {
                if (Server.clients[i].udp.endPoint == null) 
                    continue;
                Server.clients[i].udp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(int _exeptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 0; i < Server.MaxPlayers; i++)
            {
                if (Server.clients[i].udp.endPoint == null)
                    continue;
                if (i != _exeptClient)
                    Server.clients[i].udp.SendData(_packet);
            }
        }


        // từ đây trở đi là function viết gửi theo type custom
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SpawnPlayer(int _toClient, Player _player)
        {            
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);

                Byte[] encodedName = UTF8Encoded(_player.username);

                _packet.Write(encodedName.Length);
                _packet.Write(encodedName); //đây là tên đã được bytes qua utf8 khi qua client sẽ cần decode utf8

                _packet.Write(_player.position);
                _packet.Write(_player.rotation);
                _packet.Write(_player.mapID);

                var serializer = new JavaScriptSerializer();
                var serializedResult = serializer.Serialize(_player.stat);
                byte[] statSend = Zip(serializedResult);
                _packet.Write(statSend.Length);
                _packet.Write(statSend);

                SendTCPData(_toClient, _packet);     
                if(_player.id == _toClient)
                {
                    ServerSend.SendMapToClient(_player.id, GAMEDATA.mapsString);
                    ServerSend.SendNPCListToClient(_player.id, GAMEDATA.npcs);
                    //ServerSend.RefreshMap(_toClient, _player.mapID, _player.position);
                    ServerSend.RefreshMap(_toClient, _player.mapID, _player.position);
                }
            }
            if (Server.clients[_toClient].player.items == null)
            {
                Server.clients[_toClient].player.AddItemInventory(0, 0);
            }

            Server.clients[_toClient].player.InitInventory();
            Server.clients[_toClient].player.InitInventorySkill();
            ServerSend.SendInventoryPlayer(_toClient, Server.clients[_toClient].player.items);
            ServerSend.SendInventorySkillPlayer(_toClient, Server.clients[_toClient].player.skillInven);            
            ServerSend.PlayerEquipment(_player);           

        }

        #region Spawn UI blood and Text
        internal static void SpawnBlood(int map, Vector3 position)
        {
            using (Packet _packet = new Packet((int)ServerPackets.bloodUI))
            {
                _packet.Write(map);
                _packet.Write(position);
                SendUDPDataToAll(_packet);
            }
        }

        internal static void SpawnText(int map, Vector3 position, string text)
        {
            using (Packet _packet = new Packet((int)ServerPackets.textUI))
            {
                _packet.Write(map);
                _packet.Write(position);
                _packet.Write(text);
                SendUDPDataToAll(_packet);
            }
        }
        #endregion

        internal static void SendListPlayerToClient(int _toClient, PartyRec member)
        {
            using (Packet _packet = new Packet((int)ServerPackets.SendListParty))
            {
                var serializer = new JavaScriptSerializer();
                var serializedResult = serializer.Serialize(member);

                byte[] memberZip = Zip(serializedResult);
                _packet.Write(memberZip.Length);
                _packet.Write(memberZip);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void RefreshMap(int _toClient, int _map, Vector3 vector3)
        {
            using (Packet _packet = new Packet((int)ServerPackets.refreshMap))
            {
                int mapCurrent = Server.clients[_toClient].player.mapID;
                
                Server.clients[_toClient].player.mapID = _map;
                Server.clients[_toClient].player.position = vector3;
                _packet.Write(_map);
                _packet.Write(vector3);
                SendTCPData(_toClient,_packet);


                //làm mới player trong map cũ
                foreach (Client p in Server.clients.Values)
                {
                    if (p.udp.endPoint == null)
                        continue;

                    if (p.player.id == _toClient) continue;

                    //gửi packet thằng tele map về những player map cũ và map mới
                    if (p.player.mapID == mapCurrent || p.player.mapID == _map)
                    {
                        //send player trong map để player spawn                       
                        SendPlayerToClient(p.player.id, Server.clients[_toClient].player);
                        SendPlayerToClient(_toClient, Server.clients[p.player.id].player);
                    }                        
                }

                ////phần sau sẽ làm mới player trong map mới
                //foreach (Client p in Server.clients.Values)
                //{
                //    if (p.udp.endPoint == null)
                //        continue;

                //    if (p.player.id == _toClient) continue;
                //    if (p.player.mapID == _map)
                //    {
                //        //send player trong map để player spawn                        
                //        SendPlayerToClient(_toClient, Server.clients[p.player.id].player);
                //    }
                //}

                //phần sau sẽ làm mới player trong map
                foreach (Client p in Server.clients.Values)
                {
                    if (p.udp.endPoint == null)
                        continue;

                    if (p.player.mapID == _map)
                    {
                        PlayerEquipment(p.player);
                        PlayerPosition(p.player);
                    }
                }
            }
        }

        public static void SendPlayerToClient(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendPlayerInfoToClient))
            {
                _packet.Write(_player.id);

                Byte[] encodedName = UTF8Encoded(_player.username);

                _packet.Write(encodedName.Length);
                _packet.Write(encodedName); //đây là tên đã được bytes qua utf8 khi qua client sẽ cần decode utf8

                _packet.Write(_player.position);
                _packet.Write(_player.rotation);
                _packet.Write(_player.mapID);

                var serializer = new JavaScriptSerializer();
                var serializedResult = serializer.Serialize(_player.stat);
                byte[] statSend = Zip(serializedResult);
                _packet.Write(statSend.Length);
                _packet.Write(statSend);

                SendTCPData(_toClient, _packet);
            }
        }


        public static void SendMapToClient(int _toClient, string[] _map)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerLoadMap))
            {
                _packet.Write(_map.Length);
                foreach (string i in _map)
                {
                    Byte[] encodedBytes = UTF8Encoded(i);

                    _packet.Write(encodedBytes.Length);
                    _packet.Write(encodedBytes);
                }
                SendTCPData(_toClient, _packet);
            }
        }

        public static void SendInventoryPlayer(int _toClient, List<ItemInventory> items)
        {
            //gửi list item dưới dạng zip json cho player với di truyền vào
            using (Packet _packet = new Packet((int)ServerPackets.playerLoadInventory))
            {
                if (items == null) return;
                _packet.Write(items.Count);

                foreach (ItemInventory i in items)
                {
                    var serializer = new JavaScriptSerializer();
                    var serializedResult = serializer.Serialize(i);
                    byte[] listItemInventory = Zip(serializedResult);
                    _packet.Write(listItemInventory.Length);
                    _packet.Write(listItemInventory);
                }
                SendUDPData(_toClient, _packet);
            }
        }

        [Serializable]
        public struct PlayerPossFastUse
        {
            public int id;
            public Vector3 poss;
            public int spriteStop;
            public int speedAnim;
            public int mapID;

            public bool IsPunching;
            public bool IsDead;
            public bool IsKick;

            public PlayerPossFastUse(int id, Vector3 position, int spriteStop, int speedAnim, int mapID, bool isPunching, bool isDead, bool isKick) : this()
            {
                this.id = id;
                this.poss = position;
                this.spriteStop = spriteStop;
                this.speedAnim = speedAnim;
                this.mapID = mapID;
                IsPunching = isPunching;
                IsDead = isDead;
                IsKick = isKick;
            }
        }
        public static void PlayerPosition(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
            {

                PlayerPossFastUse packetPoss = new PlayerPossFastUse(_player.id, _player.position, _player.spriteStop, _player.speedAnim, _player.mapID, _player.IsPunching, _player.IsDead, _player.IsKick);
                var serializer = new JavaScriptSerializer();
                var serializedResult = serializer.Serialize(packetPoss);

                byte[] packetZip = Zip(serializedResult);
                _packet.Write(packetZip.Length);
                _packet.Write(packetZip);

                SendUDPDataToAll(_player.id,_packet);
            }
        }

        public static void PlayerEquipment(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerEquipment))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.Equipment.Length);
                foreach (string i in _player.Equipment)
                {
                    _packet.Write(i);
                }
                SendTCPDataToAll(_packet);
            }
        }

        public static void ServerSendMessageToClient(int _idPlayer, ChatRec mess)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerMessage))
            {
                var serializer = new JavaScriptSerializer();
                var serializedResult = serializer.Serialize(mess);

                //Byte[] encodedBytes = UTF8Encoded(serializedResult);
                byte[] messageZip = Zip(serializedResult);
                _packet.Write(messageZip.Length);
                _packet.Write(messageZip);

                SendTCPData(_idPlayer,  _packet);
            }
        }

        /// <summary>
        /// nếu id == -1 thì gửi tới toàn bộ client đang connect
        /// </summary>
        /// <param name="id"></param>
        /// <param name="skillInven"></param>
        internal static void SendInventorySkillPlayer(int id, List<SkillInventory> skillInven)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerLoadInventorySkill))
            {
                if (skillInven == null) return;
                _packet.Write(skillInven.Count);
                foreach (SkillInventory i in skillInven)
                {
                    var serializer = new JavaScriptSerializer();
                    var serializedResult = serializer.Serialize(i);

                    Byte[] encodedBytes = UTF8Encoded(serializedResult);

                    _packet.Write(encodedBytes.Length);
                    _packet.Write(encodedBytes);
                }
                if (id != -1)
                    SendUDPData(id, _packet);
                else
                    SendUDPDataToAll(_packet);
            }
        }

        public static void PlayerDisconnected(int _playerId)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerDisconnected))
            {
                _packet.Write(_playerId);
                SendTCPDataToAll(_playerId, _packet);
            }
        }

        public static void UpdateStatPlayer(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.updateStatPlayer))
            {
                _packet.Write(_player.id);

                var serializer = new JavaScriptSerializer();
                var serializedResult = serializer.Serialize(_player.stat);
                _packet.Write(serializedResult);//send stat

                SendTCPData(_player.id,_packet);
            }
        }

        internal static void SendBackDataArrowToClient(int idOwner, string idSkill, Vector3 movement ,bool isAlive, int map)
        {
            //Console.WriteLine($"id {idOwner} has moved arrow {idSkill} at poss {movement.ToString()}");
            using (Packet _packet = new Packet((int)ServerPackets.arrowDataSendBackToClient))
            {
                _packet.Write(idOwner);
                _packet.Write(idSkill);
                _packet.Write(movement);
                _packet.Write(isAlive);
                _packet.Write(map);
                SendUDPDataToAll(_packet);               
            }
        }  //done

        internal static void AcceptRQArrow(Vector3 position, float length, int idOwner, string idSkill, float speed, int spriteStop, int map)
        {
            // Console.WriteLine($"id {idOwner} has arrow {idSkill} at poss {position.ToString()}");
            using (Packet _packet = new Packet((int)ServerPackets.playerHasAcceptCreateArrow))
            {
                _packet.Write(position);
                _packet.Write(length);
                _packet.Write(idOwner);
                _packet.Write(idSkill);
                _packet.Write(speed);
                _packet.Write(spriteStop);
                _packet.Write(map);
                SendUDPDataToAll(_packet);
            }
        } //done 3

        public static void UpdateStatPlayerInMap(int _map)
        {

            // đối với player trong map thì update lan62 dau962 khi player spawn 
            // lọc list player trong map và spawn về client vừa connected trong map.
            using (Packet _packet = new Packet((int)ServerPackets.updateStatPlayerInMap))
            {
                Dictionary<int,Client> listGet = Server.clients.Where(x => x.Value.player != null).ToDictionary(x=>x.Key, x=>x.Value);
                listGet = listGet.Where(x=> x.Value.player.mapID == _map).ToDictionary(x => x.Key, x => x.Value);

                if (listGet.Count == 0) return;

                _packet.Write(listGet.Count);   
                
                foreach(Client client in listGet.Values)
                {
                    _packet.Write(client.id);

                    var serializer = new JavaScriptSerializer();
                    var serializedResult = serializer.Serialize(client.player.stat);
                    _packet.Write(serializedResult);//send stat
                }

                SendTCPDataToAll(_packet);
            }
        }

        internal static void SendNPCListToClient(int _toClient, NPC[] npcs)
        {
            using (Packet _packet = new Packet((int)ServerPackets.ServerSendListNPC))
            {
                _packet.Write(npcs.Length);

                for (int i = 0; i < npcs.Length; i++)
                {
                    var serializer = new JavaScriptSerializer();
                    var serializedResult = serializer.Serialize(npcs[i]);

                    UTF8Encoding utf8 = new UTF8Encoding();
                    Byte[] encodedBytes = utf8.GetBytes(serializedResult);

                    _packet.Write(encodedBytes.Length);
                    _packet.Write(encodedBytes);
                }

                SendTCPData(_toClient, _packet);
            }
        }

        public static void UpdateItems(ItemRec _items)
        {
            using (Packet _packet = new Packet((int)ServerPackets.ServerUpdateItems))
            {
                var serializer = new JavaScriptSerializer();
                var serializedResult = serializer.Serialize(_items);

                UTF8Encoding utf8 = new UTF8Encoding();
                Byte[] encodedBytes = utf8.GetBytes(serializedResult);

                // qua client handle leng và encode
                //sau đó decode.
                _packet.Write(encodedBytes.Length); 
                _packet.Write(encodedBytes);

                SendTCPDataToAll(_packet);
            }
        }        

        internal static void ServerSendAnimationToAllClientInMap(int _exeptClient, Vector3 poss, AnimationCalledStruct anim)
        {
            using (Packet _packet = new Packet((int)ServerPackets.ServerSendAnimation))
            {
                _packet.Write(poss);

                var serializer = new JavaScriptSerializer();
                var serializedResult = serializer.Serialize(anim);
                byte[] animZipSendToServer = Zip(serializedResult);
                _packet.Write(animZipSendToServer.Length);
                _packet.Write(animZipSendToServer);

                //phần sau sẽ làm mới player trong map
                foreach (Client p in Server.clients.Values)
                {
                    if (p.udp.endPoint == null)
                        continue;

                    if (p.player.id == _exeptClient) continue;

                    if (p.player.mapID == Server.clients[_exeptClient].player.mapID)
                    {
                        SendUDPData(p.id, _packet);
                    }
                }
               
            }
        }


        #region UTF8 func
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
        #endregion

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
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }
        #endregion
    }
}
