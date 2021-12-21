using Server.ObjectGame;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Server.Data
{
    #region Map mới build lại
    /// <summary>
    /// Sửa đống map bên dưới với client rồi mới dùng đồng này
    /// TODO: phải dây dựng chức năng click block bên client rồi dùng
    /// </summary>
    [Serializable]
    public enum BlockType
    {
        Box = 1,
        Circle
    }

    [Serializable]
    public struct MapBlock
    {
        public BlockType blockType;
        public Vector2 location;
    }

    [Serializable]
    public enum TeleType
    {
        OneWay = 1,
        TwoWay
    }

    [Serializable]
    public struct Teleport
    {
        public TeleType teleType;
        public Vector2 location;
    }

    [Serializable]
    public enum MapTypes
    {
        // mấy cái này chỉnh theo bên thời tiết
        // bên trong nhà thì k draw thời tiết và ngược lại
        OutSite = 1,// bên ngoài trời
        InSite //bên trong nhà hoặc hang
    }

    [Serializable]
    public struct NPCInMap
    {
        public bool isDead;
        public int npcID;
        public int dir;//này là hướng quái quay mặt
        public Vector2 pos;
    }

    [Serializable]
    public struct LengthMap
    {
        public float up;
        public float down;
        public float left;
        public float right;
    }
    #endregion

    [Serializable]
    public struct MapStruct
    {
        public string name;
        public int mapIndex;
        public MapTypes mapTypes;
        public List<MapBlock> block;// block trên map
        public List<Teleport> teleport;// cổng tp
        public string sound;// âm thanh bg của map

        public List<NPCInMap> npcInMaps;// NPC trên map       

        public LengthMap lengthMap;
        public string prefabName;
    }


    public class Map
    {
        //skills        
        public static Dictionary<int, List<ArrowSkill>> skillsInMap = new Dictionary<int, List<ArrowSkill>>();
        //end skills
        public static void InitializeMap()
        {
            TaskQueue.listMessage.Add("InitializeMap()");
            for (int i = 0; i < GAMEDATA.MAP_MAX; i++)
            {
                string path = $"/Data/Map/{i}.json";
                skillsInMap[i] = new List<ArrowSkill>();
                if (!File.Exists(Application.StartupPath + path))
                    SaveData(i);
                else
                    ReadData(i);
            }
        }
        private static void SaveData(int i)
        {
            string path = $"/Data/Map/{i}.json";
            GAMEDATA.maps[i].name = "";            
            GAMEDATA.maps[i].mapIndex = i;
            GAMEDATA.maps[i].mapTypes = MapTypes.OutSite;
            GAMEDATA.maps[i].block = new List<MapBlock>();
            GAMEDATA.maps[i].teleport = new List<Teleport>();
            GAMEDATA.maps[i].sound = "";
            GAMEDATA.maps[i].npcInMaps = new List<NPCInMap>();
            GAMEDATA.maps[i].lengthMap = new LengthMap();
            GAMEDATA.maps[i].prefabName = "";

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(GAMEDATA.maps[i]);
            GAMEDATA.mapsString[i] = serializedResult;
            File.WriteAllText(Application.StartupPath + path, serializedResult);
        }
        private static void ReadData(int i)
        {
            string path = $"/Data/Map/{i}.json";
            var dataRead = File.ReadAllText(Application.StartupPath + path);
            GAMEDATA.mapsString[i] = dataRead;
            var serializer = new JavaScriptSerializer();
            MapStruct dataResult = serializer.Deserialize<MapStruct>(dataRead);
            GAMEDATA.maps[i] = dataResult;
        }

        public static void SavingMapFromAdmin(MapStruct map)
        {
            string path = $"/Data/Map/{map.mapIndex}.json";

            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(map);
            GAMEDATA.mapsString[map.mapIndex] = serializedResult;

            File.WriteAllText(Application.StartupPath + path, serializedResult);

            for (int i = 0; i < Server.MaxPlayers; i++)
            {
                if(Server.clients[i].player != null)
                {
                    ServerSend.SendMapToClient(Server.clients[i].player.id, GAMEDATA.mapsString);
                }
            }
        }
        //end load map


        //create skill type arrow
        public static void CreateNewArrow(int idOwner, int map ,int spriteStop, float length, float speed, Vector3 position)
        {
            String timeStamp = (DateTime.Now).ToString("yyyyMMddHHmmssffff");
            ArrowSkill arrowSkill = new ArrowSkill(position, length, idOwner, timeStamp, speed, spriteStop);
            skillsInMap[map].Add(arrowSkill);

            ServerSend.AcceptRQArrow(position, length, idOwner, timeStamp, speed, spriteStop, map);
        }

        public static void UpdateSkills()
        {
            if (skillsInMap.Count == 0)
                return;

            SkillsMovement();
        }

        internal static void SkillsMovement()
        {
            for (int i = 0; i < skillsInMap.Count; i++)
            {
                if (skillsInMap[i].Count == 0) continue;

                List<ArrowSkill> lista = skillsInMap[i].ToList();
                foreach(ArrowSkill a in lista)
                {
                    ArrowSkill.ArrowUpdatePosition(a, i);
                }
                
            }
        }

        public static void UpdateNPC(int mapID)
        {
            // thường thì chỉ update NPC type monster
            // update position hay targeting, máu me các kiểu của monster rồi gửi về client
            //TODO: hàm send package về cho client cái data map thôi
        }

        /// <summary>
        /// idNPC là id npc trong list npc trên map
        /// </summary>
        /// <param name="idNPC"></param>
        /// <param name="idTarget"></param>
        /// <param name="isHitted"></param>
        /// <param name="mapID"></param>
        public static void UpdateNPC(int idNPC, int idTarget,bool isHitted, int mapID)
        {
            if (!GAMEDATA.npcs[GAMEDATA.maps[mapID].npcInMaps[idNPC].npcID].movable) return;
            //bool[] dir = WayCanMove(GAMEDATA.maps[mapID].monsterInMaps[idNPC].npc, mapID);
            //if(type npc != monster) return;
            //update idtarget của npc rồi gọi hàm updatenpc map thôi là ok

            // nếu isHitted = true thì là player đấm con quái
            //server tính dmg rồi update con quái thôi

            UpdateNPC(mapID);
        }

        public static void UpdateNPCMovement(int idNPC, int mapID, Vector3 vector3)
        {

            //if(type npc != monster) return;
            //update idtarget của npc rồi gọi hàm updatenpc map thôi là ok

            // nếu isHitted = true thì là player đấm con quái
            //server tính dmg rồi update con quái thôi

            UpdateNPC(mapID);
        }



        public static bool[] WayCanMove(NPC npc, int mapId)
        {
            bool[] way = new bool[4];
            way[0] = DirUp(npc, mapId);
            way[1] = DirDown(npc, mapId);
            way[2] = DirLeft(npc, mapId);
            way[3] = DirRight(npc, mapId);
            return way;
        }

        public static bool DirUp(NPC npc,int mapId)
        { //up y+1
            if (!npc.movable)
                return false;

            MapBlock mapBlock = GAMEDATA.maps[mapId].block.SingleOrDefault(x => x.location == new Vector2(npc.positionUpdate.x, npc.positionUpdate.y + 1));

            if (mapBlock.blockType != BlockType.Box && mapBlock.blockType != BlockType.Circle)
                return true;
            return false;
        }
        public static bool DirDown(NPC npc, int mapId)
        { //down y+1
            if (!npc.movable)
                return false;

            MapBlock mapBlock = GAMEDATA.maps[mapId].block.SingleOrDefault(x => x.location == new Vector2(npc.positionUpdate.x, npc.positionUpdate.y - 1));

            if (mapBlock.blockType != BlockType.Box && mapBlock.blockType != BlockType.Circle)
                return true;
            return false;
        }
        public static bool DirLeft(NPC npc, int mapId)
        { //left x-1
            if (!npc.movable)
                return false;

            MapBlock mapBlock = GAMEDATA.maps[mapId].block.SingleOrDefault(x => x.location == new Vector2(npc.positionUpdate.x -1, npc.positionUpdate.y));

            if (mapBlock.blockType != BlockType.Box && mapBlock.blockType != BlockType.Circle)
                return true;
            return false;
        }
        public static bool DirRight(NPC npc, int mapId)
        { //right x+1
            if (!npc.movable)
                return false;

            MapBlock mapBlock = GAMEDATA.maps[mapId].block.SingleOrDefault(x => x.location == new Vector2(npc.positionUpdate.x + 1, npc.positionUpdate.y));

            if (mapBlock.blockType != BlockType.Box && mapBlock.blockType != BlockType.Circle)
                return true;
            return false;
        }
    }
}
