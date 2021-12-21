using Assets.Script.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;
using static Inventory;
using static InventorySkill;

/// <summary>
/// Receive packet from server
/// </summary>
public class ClientHandle : MonoBehaviour {

    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log("Message from server: "+_msg);
        if (_msg.IndexOf("[Error]") >= 0)
        {
            UIManager.instance.startMenu.SetActive(true);
            UIManager.instance.usernameField.interactable = true;
            Client.instance.Disconnect();
            return;
        }
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }
    public static void SpawnPlayer(Packet _packet)
    {
        //id
        int _id = _packet.ReadInt();

        //read length tên user
        //sau đó đọc bytes array bằng length gán vào bytes 
        //cuối cùng decode utf8 thành string rồi gán vào _username
        int length = _packet.ReadInt();
        Byte[] _msg = _packet.ReadBytes(length);

        UTF8Encoding utf8 = new UTF8Encoding();
        String decodedString = utf8.GetString(_msg);

        string _username = decodedString;

        //đọc và gán pos và rot
        Vector2 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();
        int mapID = _packet.ReadInt();

        byte[] statHandle = _packet.ReadBytes(_packet.ReadInt());
        string playerStat = Unzip(statHandle);
        Stats stat = JsonUtility.FromJson<Stats>(playerStat);
        
        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation, mapID , stat);
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
    public static void PlayerPosition(Packet _packet)
    {

        int length = _packet.ReadInt();
        string _msg = Unzip(_packet.ReadBytes(length));
        PlayerPossFastUse packet = JsonUtility.FromJson<PlayerPossFastUse>(_msg);


        //int _id = _packet.ReadInt();
        //Vector3 _position = _packet.ReadVector3();
        //int _spriteStop = _packet.ReadInt();
        //int _speedAnim = _packet.ReadInt();
        //int _mapID = _packet.ReadInt();

        //bool _isPunching = _packet.ReadBool();
        //bool _isDead = _packet.ReadBool();
        //bool _isKick = _packet.ReadBool();

        try
        {
            if (GameManager.players[packet.id] == null) return;
            GameManager.instance.PlayerSet(packet.id, packet.poss, packet.spriteStop, packet.speedAnim, packet.mapID, packet.IsPunching, packet.IsDead, packet.IsKick);
            GameManager.players[packet.id].gameObject.transform.localPosition = new Vector3(packet.poss.x, packet.poss.y, packet.poss.y);
            GameManager.players[packet.id].position = new Vector3(packet.poss.x, packet.poss.y, packet.poss.y);
        }
        catch(Exception _ex)
        {
            Debug.Log($"Error set player id {packet.id} poss via {_ex}");
        }
    }

    public static void PlayerLoadMap(Packet _packet)
    {
        string[] _inputs = new string[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            int length = _packet.ReadInt();
            Byte[] _msg = _packet.ReadBytes(length);

            UTF8Encoding utf8 = new UTF8Encoding();
            String decodedString = utf8.GetString(_msg);

            _inputs[i] = decodedString;
        }
        Map.LoadMap(_inputs);
        GAME.MAX_MAP = _inputs.Length;
    }

    public static void ReceivedMessagePlayerFromServer(Packet _packet)
    {
        int length = _packet.ReadInt();
        string _msg = Unzip(_packet.ReadBytes(length));

        //UTF8Encoding utf8 = new UTF8Encoding();
        //String decodedString = utf8.GetString(_msg);

        //string message = _packet.ReadString();
        ChatRec mess = JsonUtility.FromJson<ChatRec>(_msg);
        if (mess.chatType == ChatType.InvitePlayer) return;
        ChatInput.instance.UpdateChatBar(mess);

    }

    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();

        try
        {

            if (GameManager.players[_id] != null)
                Destroy(GameManager.players[_id].gameObject);

            GameManager.players.Remove(_id);
        }
        catch { }
    }   

    public static void PlayerEquipment(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string[] _inputs = new string[_packet.ReadInt()];
        for (int i = 0; i < _inputs.Length; i++)
        {
            _inputs[i] = _packet.ReadString();
        }

        //if (GameManager.players.Count <= _id) return;

        try
        {
            if (Client.instance.myId == -1)
                return;
            GameManager.players[_id].Equipment = _inputs;
        }catch//(Exception _ex)
        {
            //Debug.Log($"Error setting data to playerid {_id} via: {_ex}");
        }
        
    }

    public static void RefreshMap(Packet _packet)
    {
        int _map = _packet.ReadInt();
        //int _id = _packet.ReadInt();
        Vector3 vector = _packet.ReadVector3();
        try
        {
            if (GameManager.players[Client.instance.myId] == null) return;
            GameManager.players[Client.instance.myId].transform.position = vector;
            GameManager.players[Client.instance.myId].position = vector;
            GameManager.players[Client.instance.myId].mapID = _map;
            GAME.MAP_START = _map;
            Map.instance.UpdateMap();
            ClientSend.PlayerMovement(vector, GameManager.players[Client.instance.myId].speedAnim, GameManager.players[Client.instance.myId].spriteStop, GameManager.players[Client.instance.myId].IsPunching, GameManager.players[Client.instance.myId].IsDead, GameManager.players[Client.instance.myId].IsKick);
        }
        catch(Exception _ex) {
            Debug.Log($"Error Setdata via {_ex}");
        }
        
    }

    internal static void PlayerCreateArrow(Packet _packet)
    {
        Vector3 position = _packet.ReadVector3();
        float _length = _packet.ReadFloat();
        int _idOwner = _packet.ReadInt();
        string _idSkill = _packet.ReadString();
        float speed = _packet.ReadFloat();
        int _spriteStop = _packet.ReadInt();
        int _map = _packet.ReadInt();

        if (_map != GAME.MAP_START) return;

        Map.instance.arrowLoad.GetComponent<ArrowSkill>().idOwner = _idOwner;
        Map.instance.arrowLoad.GetComponent<ArrowSkill>().length = _length;
        Map.instance.arrowLoad.GetComponent<ArrowSkill>().face = _spriteStop;
        Map.instance.arrowLoad.GetComponent<ArrowSkill>().speed = speed;
        Map.instance.arrowLoad.GetComponent<ArrowSkill>().locationShoot = position;
        Map.instance.arrowLoad.GetComponent<ArrowSkill>().idSkill = _idSkill;
        GameObject arrow = Instantiate(Map.instance.arrowLoad, position, new Quaternion());

        //addAudio skill fireball
        AudioManager.SpawnAudioSRCByText("Fire2");

        Map.skillsInMap[_map].Add(arrow.GetComponent<ArrowSkill>());

    } //done 4 & 5

    internal static void PlayerUpdateDataArrow(Packet _packet)
    {
        int _idOwner = _packet.ReadInt();
        string _idSkill = _packet.ReadString();
        Vector3 _movement = _packet.ReadVector3();
        bool _isAlive = _packet.ReadBool();
        int _map = _packet.ReadInt();

        if (_map != GAME.MAP_START) return;

        ArrowSkill skill = Map.skillsInMap[_map].SingleOrDefault(x => x.idSkill == _idSkill);
        if (skill == null) return;
        if (_isAlive == false)
        {
            Destroy(skill.gameObject);
            Map.skillsInMap[_map].Remove(skill);
        }
        else
        {
            skill.transform.position = _movement;
        }

    }

    internal static void PlayerLoadAnimation(Packet _packet)
    {
        Vector3 poss = _packet.ReadVector3();

        int len = _packet.ReadInt();
        byte[] dataByte = _packet.ReadBytes(len);

        string stringJson = Unzip(dataByte);

        AnimationCalledStruct dataResult = JsonUtility.FromJson<AnimationCalledStruct>(stringJson);
        GameObject animation = AnimationManager.CallAnimation(poss,dataResult);

        if (dataResult.idPlayer == -1) return;
        GameObject playerGet = GameManager.players[dataResult.idPlayer].gameObject;
        animation.gameObject.transform.SetParent(playerGet.transform);

    }

    internal static void UpdateStatPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();

        string playerStat = _packet.ReadString();
        Stats stat = JsonUtility.FromJson<Stats>(playerStat);

        float bHP = (float)stat.healthLeft / stat.health;
        float bMP = (float)stat.manaLeft / stat.mana;
        if (bHP <= 0)
        {
            bMP = bHP = 1;           
        }

        BarHealth.instance.SetHP(bHP);
        BarHealth.instance.SetMP(bMP);
        try
        {
            GameManager.players[_id].stat = stat;
        }
        catch
        {

        }
        
    }

    internal static void PlayerLoadInfoPlayer(Packet _packet)
    {
        //id
        int _id = _packet.ReadInt();

        //read length tên user
        //sau đó đọc bytes array bằng length gán vào bytes 
        //cuối cùng decode utf8 thành string rồi gán vào _username
        int length = _packet.ReadInt();
        Byte[] _msg = _packet.ReadBytes(length);

        UTF8Encoding utf8 = new UTF8Encoding();
        String decodedString = utf8.GetString(_msg);

        string _username = decodedString;

        //đọc và gán pos và rot
        Vector2 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();
        int mapID = _packet.ReadInt();

        byte[] statHandle = _packet.ReadBytes(_packet.ReadInt());
        string playerStat = Unzip(statHandle);
        Stats stat = JsonUtility.FromJson<Stats>(playerStat);



        //Player playerAdd = new Player(_id, _username, mapID);
        //playerAdd.position = _position;
        //playerAdd.rotation = _rotation;
        //playerAdd.stat = stat;
        if (_id == Client.instance.myId) return;
        if (GameManager.players[_id] != null) return;
        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation, mapID, stat);

        //try
        //{
        //    Player p = GameManager.players[_id];
        //    if (p == null)
        //        GameManager.players[_id] = playerAdd;
        //    else
        //    {
        //        p.mapID = mapID;
        //    }                

        //    Map.instance.UpdateMap();
        //}
        //catch { }
        
    }

    internal static void BloodUI(Packet _packet)
    {
        int map = _packet.ReadInt();
        Vector3 position = _packet.ReadVector3();
        if (GAME.MAP_START != map) return;
        MapUpdateUI.CreateBlood(position);
    }

    internal static void TextUI(Packet _packet)
    {
        int map = _packet.ReadInt();
        Vector3 position = _packet.ReadVector3();
        string text = _packet.ReadString();
        if (GAME.MAP_START != map) return;
        MapUpdateUI.CreateText(position,text);
    }

    internal static void PlayerHandleNPC(Packet _packet)
    {
        int len = _packet.ReadInt();
        GAME.MAX_NPC = len;
        GAME.npcs = new NPC[GAME.MAX_NPC];
        for (int i = 0; i < len; i++)
        {
            int length = _packet.ReadInt();
            Byte[] _msg = _packet.ReadBytes(length);

            UTF8Encoding utf8 = new UTF8Encoding();
            String decodedString = utf8.GetString(_msg);

            NPC npc = JsonUtility.FromJson<NPC>(decodedString);
            GAME.npcs[i] = npc;
        }
        NPCManager.isUpdateList = true;
    }

    internal static void PlayerReceivedListParty(Packet _packet)
    {
        //int length = _packet.ReadInt();
        //Byte[] _msg = _packet.ReadBytes(length);

        //string JsonListParty = UTF8Decoded(_msg);

        byte[] byteParty = _packet.ReadBytes(_packet.ReadInt());
        string JsonListParty = Unzip(byteParty);
        PartyRec _party = JsonUtility.FromJson<PartyRec>(JsonListParty);

        GameManager.players[Client.instance.myId].party = _party;
    }

    internal static void PlayerLoadInventorySkill(Packet _packet)
    {
        int count = _packet.ReadInt();
        List<SkillInventory> items = new List<SkillInventory>();
        for (int i = 0; i < count; i++)
        {
            int lengthItemEach = _packet.ReadInt();
            Byte[] _itemByte = _packet.ReadBytes(lengthItemEach);

            string JsonItem = UTF8Decoded(_itemByte);
            SkillInventory itemGet = JsonUtility.FromJson<SkillInventory>(JsonItem);
            items.Add(itemGet);
        }
        try
        {
            GameManager.players[Client.instance.myId].skillInven = items;
            InventorySkill.InitInventory();
        }
        catch
        {

        }
        
        
    }

    internal static void PlayerLoadInventory(Packet _packet)
    {
        int count = _packet.ReadInt();
        
        List<ItemInventory> items = new List<ItemInventory>();
        
        for (int i = 0; i < count; i++)
        {
            int lengthItemEach = _packet.ReadInt();
            byte[] _itemByte = _packet.ReadBytes(lengthItemEach);

            string JsonItem = Unzip(_itemByte);
            ItemInventory itemGet = JsonUtility.FromJson<ItemInventory>(JsonItem);
            items.Add(itemGet);
        }

        try
        {
            GameManager.players[Client.instance.myId].items = items;
            Inventory.InitInventory();
        }
        catch { }
        
    }

    internal static void HandleStatPlayerInMap(Packet _packet)
    {
        int countPlayer = _packet.ReadInt();
        for (int i = 0; i < countPlayer; i++)
        {
            int id = _packet.ReadInt();

            string playerStat = _packet.ReadString();
            Stats stat = JsonUtility.FromJson<Stats>(playerStat);
            GameManager.players[id].stat = stat;
        }
    }



    /// <summary>
    /// cần một byte array đọc của packet để decode. 
    /// Đầu ra là một chuỗi string đã decoded
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
