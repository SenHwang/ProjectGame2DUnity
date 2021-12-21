using Assets.Script.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEngine;

/// <summary>
/// Send packet to server
/// </summary>
public class ClientSend : MonoBehaviour {

    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packet
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);

            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(UIManager.instance.usernameField.text);
            _packet.Write(encodedBytes.Length);// send length name bytes to server
            _packet.Write(encodedBytes);// send bytes name to server

            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(Vector2 _inputs, int speedAnim,int spriteStop, bool IsPunching, bool IsDead, bool IsKick)//Vector2 _inputs,
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            Vector3 send = new Vector3(_inputs.x, _inputs.y, 0);
            _packet.Write(send);

            _packet.Write(speedAnim);
            _packet.Write(spriteStop);

            _packet.Write(IsPunching);
            _packet.Write(IsDead);
            _packet.Write(IsKick);

            SendUDPData(_packet);

        }
    }

    internal static void ClientRQTeleMap(int mapID, Vector2 telePoint)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerTeleportMap))
        {
            _packet.Write(mapID);
            Vector3 telePoss = new Vector3(telePoint.x, telePoint.y, 0);
            _packet.Write(telePoss);          

            SendTCPData(_packet);
        }
    }

    public static void PlayerEquip(int clientID,string[] equipmentSlot)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerEquipment))
        {
            _packet.Write(clientID);
            _packet.Write(equipmentSlot.Length);
            foreach (string i in equipmentSlot)
            {
                _packet.Write(i);
            }
            SendUDPData(_packet);
        }
    }
    
    public static void PlayerSendMessageToServer(ChatRec mess)//(int line, string message)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMessage))
        {
            var serializedResult = JsonUtility.ToJson(mess);

            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(serializedResult);
            _packet.Write(encodedBytes.Length);
            _packet.Write(encodedBytes);
            SendTCPData(_packet);

        }
    }

    internal static void RequestServerToCreateNewArrow(int myId, int spriteStop, float length, float speed, Vector3 position)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerRQArrow))
        {
            _packet.Write(myId);
            _packet.Write(spriteStop);
            _packet.Write(length);
            _packet.Write(speed);
            _packet.Write(position);
            SendUDPData(_packet);
        }
    } 

    public static void SkillGetHit(int idOwner, string skillID, int Target, int map)
    {        
        using (Packet _packet = new Packet((int)ClientPackets.playerSkillHitted))
        {
            //với colision là thằng bị đấm
            if (Target != Client.instance.myId && idOwner == Client.instance.myId)
                TargetBar.SetTarget(Target);

            _packet.Write(idOwner);
            _packet.Write(skillID); 
            _packet.Write(Target);
            _packet.Write(map);
            SendUDPData(_packet);
        }
    }

    internal static void SendStatusUpdate(int value)
    {
        using(Packet _packet = new Packet((int)ClientPackets.playerUpdateStats))
        {
            _packet.Write(value);
            SendUDPData(_packet);
        }
    }

    internal static void SendSavingMap(Map.MapStruct mapStruct)
    {
        using (Packet _packet = new Packet((int)ClientPackets.adminSavingMap))
        {
            string mapSave = JsonUtility.ToJson(mapStruct);

            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(mapSave);

            _packet.Write(encodedBytes.Length);
            _packet.Write(encodedBytes);

            SendUDPData(_packet);
        }
    }

    internal static void SendNPCSavingToServer(int index, NPC nPC)
    {
        using (Packet _packet = new Packet((int)ClientPackets.adminSavingNPC))
        {
            _packet.Write(index);

            string npcSave = JsonUtility.ToJson(nPC);

            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(npcSave);

            _packet.Write(encodedBytes.Length);
            _packet.Write(encodedBytes); // data encode rồi gửi về server

            SendUDPData(_packet);
        }
    }

    public enum TypeItemCall
    {
        Item,
        Skill
    }
    /// <summary>
    /// TypeItemCall enum là type của Item cần call
    /// </summary>
    /// <param name="idItem"></param>
    /// <param name="count"></param>
    /// <param name="type"></param>
    internal static void AdminRequestCallItem(int idItem, int count, TypeItemCall type)
    {
        using (Packet _packet = new Packet((int)ClientPackets.adminCallItem))
        {
            _packet.Write(idItem);
            _packet.Write(count);
            _packet.Write((int)type);
            SendUDPData(_packet);
        }
    }

    internal static void PlayerCallAnimation(Vector3 poss, AnimationCalledStruct anim)
    {
        using (Packet _packet = new Packet((int)ClientPackets.spawnAnimation))
        {
            _packet.Write(poss);

            var serializedResult = JsonUtility.ToJson(anim);
            byte[] animZipSendToServer = Zip(serializedResult);
            _packet.Write(animZipSendToServer.Length);
            _packet.Write(animZipSendToServer);
            SendUDPData(_packet);
        }
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
