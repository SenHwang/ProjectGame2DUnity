using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Map : MonoBehaviour
{
    #region Type của map
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

    public static Map instance;

    public static MapStruct[] maps = new MapStruct[GAME.MAX_MAP];
    public static List<GameObject> nPCInMaps = new List<GameObject>();

    //skills        
    public static Dictionary<int, List<ArrowSkill>> skillsInMap = new Dictionary<int, List<ArrowSkill>>();
    public GameObject arrowLoad;    
    //end skills

    public Transform MainMap;
    public GameObject NPC;

    public static int currentMap;
    
    private GameObject[] gameObjects = new GameObject[GAME.MAX_MAP];
    private GameObject map;
   

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GAME.animation == null)
            AnimationManager.LoadAnimList();
        if (GAME.objectAnimation == null)
            GAME.objectAnimation = Resources.Load<GameObject>("Skill/SkillAnim");


        arrowLoad = Resources.Load<GameObject>("Skill/SkillArrow");
        gameObjects = Resources.LoadAll<GameObject>(GAME.MAP_PATH);

        ////change audio map
        //AudioManager.ChangeAudioMapByText(maps[GAME.MAP_START].sound);

        for (int i = 0; i < gameObjects.Length; i++)
        {
            skillsInMap[i] = new List<ArrowSkill>();

            Debug.Log("Object map loaded: " + gameObjects[i].name);
            if (i == GAME.MAP_START)
            {
                currentMap = GAME.MAP_START;
                map = Instantiate(gameObjects[i]) as GameObject;
                map.transform.SetParent(MainMap);

                if (maps.Length != 0)
                {
                    for (int z = 0; z < maps[i].npcInMaps.Count; z++)
                    {
                        GameObject npc;
                        Vector3 possSet = new Vector3(maps[i].npcInMaps[z].pos.x, maps[i].npcInMaps[z].pos.y, maps[i].npcInMaps[z].pos.y);

                        npc = Instantiate(NPC, possSet, new Quaternion()) as GameObject;
                        npc.transform.GetComponent<MonsterEquipAnim>().spriteStop = maps[i].npcInMaps[z].dir;
                        npc.transform.GetComponent<MonsterEquipAnim>().speedAnim = 0;
                        npc.transform.GetComponent<MonsterEquipAnim>().sprite = GAME.npcs[maps[i].npcInMaps[z].npcID].sprite == "" ? -1 : int.Parse(GAME.npcs[maps[i].npcInMaps[z].npcID].sprite);
                        npc.transform.GetComponent<MonsterEquipAnim>().NPCID = maps[i].npcInMaps[z].npcID;
                        npc.transform.SetParent(MainMap);
                        nPCInMaps.Add(npc);
                    }
                }
                
            }

        }
    }


    //Update is called once per frame
    //[Obsolete]
    public void UpdateMap()
    {
        if (currentMap == GAME.MAP_START)
        {
            try
            {
                foreach (Player p in GameManager.players.Values)
                {
                    if (p.id == Client.instance.myId)
                        continue;

                    if (p.mapID != GameManager.players[Client.instance.myId].mapID)
                    {
                        if(GameManager.players[p.id] != null)
                            Destroy(p.gameObject);
                        continue;
                    }else if(p.mapID == GameManager.players[Client.instance.myId].mapID)
                    {
                        if(GameManager.players[p.id] == null)
                        {
                            GameObject _player;
                            _player = Instantiate(GameManager.instance.playerPrefab, GameManager.players[p.id].position, GameManager.players[p.id].rotation);
                            _player.GetComponent<Player>().id = p.id;
                            _player.GetComponent<Player>().username = p.username;
                            _player.GetComponent<Player>().mapID = p.mapID;
                            _player.GetComponent<Player>().stat = p.stat;
                            GameManager.players.Remove(p.id);
                            GameManager.players.Add(p.id, _player.GetComponent<Player>());
                            GameManager.players[p.id].gameObject.transform.position = GameManager.players[p.id].position;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }

            return;
        }

        GameObject m = MainMap.Find(map.name).gameObject;
        if (m != null)
        {
            Destroy(m);
            map = Instantiate(gameObjects[GAME.MAP_START]) as GameObject;
            map.transform.SetParent(MainMap);

            //clear Npc in map
            for(int i = 0; i < nPCInMaps.Count; i++)
            {
                Destroy(nPCInMaps[i]);
            }

            //change audio map
            AudioManager.ChangeAudioMapByText(maps[GAME.MAP_START].sound);

            if(maps.Length != 0)
            {
                nPCInMaps = new List<GameObject>();
                for (int z = 0; z < maps[GAME.MAP_START].npcInMaps.Count; z++)
                {
                    GameObject npc;
                    npc = Instantiate(NPC, maps[GAME.MAP_START].npcInMaps[z].pos, new Quaternion());
                    npc.transform.GetComponent<MonsterEquipAnim>().spriteStop = maps[GAME.MAP_START].npcInMaps[z].dir;
                    npc.transform.GetComponent<MonsterEquipAnim>().speedAnim = 0;
                    npc.transform.GetComponent<MonsterEquipAnim>().sprite = GAME.npcs[maps[GAME.MAP_START].npcInMaps[z].npcID].sprite == "" ? -1 : int.Parse(GAME.npcs[maps[GAME.MAP_START].npcInMaps[z].npcID].sprite);
                    npc.transform.GetComponent<MonsterEquipAnim>().NPCID = maps[GAME.MAP_START].npcInMaps[z].npcID;
                    npc.transform.SetParent(MainMap);
                    nPCInMaps.Add(npc);
                }
            }                

            if (Map.skillsInMap[currentMap].Count != 0)
            {
                foreach (ArrowSkill a in Map.skillsInMap[currentMap].ToArray())
                {
                    Destroy(a.gameObject);
                    Map.skillsInMap[currentMap].Remove(a);
                }                
            }

            try
            {
                foreach (Player p in GameManager.players.Values)
                {
                    if (p.id == Client.instance.myId)
                        continue;

                    if (p.mapID != GAME.MAP_START)
                    {
                        Destroy(p.gameObject);
                    }
                }
            } catch (Exception ex){
                Debug.Log(ex);
            }
            
            currentMap = GAME.MAP_START;
        }
    }

    public static void LoadMap(string[] input)
    {
        maps = new MapStruct[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            MapStruct myObject = JsonUtility.FromJson<MapStruct>(input[i]);
            if (myObject.teleport.Count == 0)
                myObject.teleport = new List<Teleport>();
            if (myObject.block.Count == 0)
                myObject.block = new List<MapBlock>();
            if (myObject.npcInMaps.Count == 0)
                myObject.npcInMaps = new List<NPCInMap>();

            maps[i] = myObject;
        }
    }

    public void SpawnMap()
    {
        gameObjects = Resources.LoadAll<GameObject>(GAME.MAP_PATH);
        
        GameObject map = Instantiate(gameObjects[GAME.MAP_START]) as GameObject;
        map.transform.SetParent(MainMap);
    }


    //tạm thời read map data rồi lưu vào maps list nên k cần lưu data tại client
    //public void SaveData(int i)
    //{
    //    maps[i].name = gameObjects[i].name;
    //    maps[i].mapIndex = i;
    //    maps[i].teleport = new List<int[][]>();
    //    File.WriteAllText(Application.dataPath + "/Data/Assets/Resources/MapData/" + $"{i}.json", JsonUtility.ToJson(maps[i]));
    //}

    //public void ReadData(int i)
    //{
    //    string data = File.ReadAllText(Application.dataPath + "/Data/Assets/Resources/MapData/" + $"{i}.json");

    //    MapStruct myObject = JsonUtility.FromJson<MapStruct>(data);
    //    maps[i] = myObject;
    //    Debug.Log(JsonUtility.ToJson(maps[i]));
    //}
}
