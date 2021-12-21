using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static Map;

public class EditMapFunc : MonoBehaviour
{
    private Toggle block;
    private Toggle drawLine;
    private Toggle clickVector;
    private Toggle npcSet;//NPCSet

    //mapEdit
    public static InputField up;
    public static InputField down;
    public static InputField right;
    public static InputField left;

    public static InputField nameMap;
    public static InputField sound;
    public static InputField prefab;

    //NPC setup
    public static InputField dir;
    public GameObject ScrollViewListNPC;
    public GameObject TextInList;
    private List<GameObject> listNPC = new List<GameObject>();
    public static int indexNPC;
    private bool isUpdateList;
    public Text SelectedIndex;
    private int indexNPCSelected;

    //main Panel
    private GameObject settingPanel;
    

    private void Awake()
    {
        TestClickMap.EditMap = this.gameObject;
    }

    private void OnDisable()
    {
        block.isOn = false;
        drawLine.isOn = false;

        TestClickMap.blockEnable = block.isOn;
        TestClickMap.lineEnable = drawLine.isOn;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject gamecontrol = transform.Find("Controls").gameObject;
        settingPanel = transform.Find("SettingPanel").gameObject;

        block = gamecontrol.transform.Find("Blockable").GetComponent<Toggle>();
        drawLine = gamecontrol.transform.Find("Line").GetComponent<Toggle>();
        clickVector = gamecontrol.transform.Find("Click").GetComponent<Toggle>();
        npcSet = gamecontrol.transform.Find("NPCSet").GetComponent<Toggle>();


        up = settingPanel.transform.Find("u").transform.GetComponent<InputField>();
        down = settingPanel.transform.Find("d").transform.GetComponent<InputField>();
        right = settingPanel.transform.Find("r").transform.GetComponent<InputField>();
        left = settingPanel.transform.Find("l").transform.GetComponent<InputField>();

        nameMap = settingPanel.transform.Find("Name").transform.GetComponent<InputField>();
        sound = settingPanel.transform.Find("Sound").transform.GetComponent<InputField>();
        prefab = settingPanel.transform.Find("Prefab").transform.GetComponent<InputField>();


        nameMap.text = Map.maps[GAME.MAP_START].name;
        sound.text = Map.maps[GAME.MAP_START].sound;
        prefab.text = Map.maps[GAME.MAP_START].prefabName;

        up.text = Map.maps[GAME.MAP_START].lengthMap.up.ToString();
        down.text = Map.maps[GAME.MAP_START].lengthMap.down.ToString();
        left.text = Map.maps[GAME.MAP_START].lengthMap.left.ToString();
        right.text = Map.maps[GAME.MAP_START].lengthMap.right.ToString();


        TestClickMap.blockEnable = block.isOn = false;
        TestClickMap.lineEnable = drawLine.isOn = false;
        TestClickMap.clickVectorEnable = clickVector.isOn = false;
        TestClickMap.npcSetEnable = npcSet.isOn = false;

        //Add listener for when the state of the Toggle changes, to take action
        block.onValueChanged.AddListener(delegate {
            ToggleValueChanged(block);
        });

        drawLine.onValueChanged.AddListener(delegate {
            ToggleValueChanged(drawLine);
        });

        clickVector.onValueChanged.AddListener(delegate {
            ToggleValueChanged(clickVector);
        });

        npcSet.onValueChanged.AddListener(delegate {
            ToggleValueChanged(npcSet);
        });

        InitListNPC();
    }

    void ToggleValueChanged(Toggle toggle)
    {
        TestClickMap.blockEnable = block.isOn;
        TestClickMap.lineEnable = drawLine.isOn;
        TestClickMap.clickVectorEnable = clickVector.isOn;
        TestClickMap.npcSetEnable = npcSet.isOn;
    }


    public void OnSavingSetting()
    {
        //TODO later: thêm mấy cái sound event npc các kiểu
        //TODO: Save data
        if (up.text == "" || up.text == null) return;
        if (down.text == "" || down.text == null) return;
        if (left.text == "" || left.text == null) return;
        if (right.text == "" || right.text == null) return;

        up.text = up.text.Replace('.', ',');
        down.text = down.text.Replace('.', ',');
        left.text = left.text.Replace('.', ',');
        right.text = right.text.Replace('.', ',');

        int y = (int)(float.Parse(up.text) + 0.5f);
        int y1 = (int)(float.Parse(down.text) - 0.5f);
        int x = (int)(float.Parse(right.text) + 0.5f);
        int x1 = (int)(float.Parse(left.text) - 0.5f);
            
        TestClickMap.y = y;
        TestClickMap.y1 = y1;
        TestClickMap.x = x;
        TestClickMap.x1 = x1;

        LengthMap lengthMap = new LengthMap();
        lengthMap.up = TestClickMap.y;
        lengthMap.down = TestClickMap.y1;
        lengthMap.left = TestClickMap.x1;
        lengthMap.right = TestClickMap.x;
        TestClickMap.lengthMap = lengthMap;

        //close pannel setting
        settingPanel.SetActive(false);
    }

    public void onSaveMap()
    {
        LengthMap lengthMap = new LengthMap();
        lengthMap.up = TestClickMap.y;
        lengthMap.down = TestClickMap.y1;
        lengthMap.left = TestClickMap.x1;
        lengthMap.right = TestClickMap.x;

        int i = GAME.MAP_START;
        Map.maps[i].name = nameMap.text;
        Map.maps[i].mapIndex = i;
        Map.maps[i].sound = sound.text;
        Map.maps[i].prefabName = prefab.text;
        Map.maps[i].block = TestClickMap.blockList;
        Map.maps[i].lengthMap = lengthMap;

        // Map.maps[i].mapTypes //chưa có này
        // Map.maps[i].monsterInMaps //chưa có này
        // Map.maps[i].teleport //chưa có này
        // Map.maps[i].npcInMaps //chưa có này
        Map.maps[i].npcInMaps = Map.maps[GAME.MAP_START].npcInMaps;
        
        ///khi set map bấm chọn npc muốn set rồi qua map tích vào NPCset 
        ///sau khi tích vào thì bấm lên map từng lượt
        ///default(0) là face down
        ///1 là face left
        ///2 là face up
        ///3 là face right
        ///bấm phát nữa thì xóa npc
        ///add lên listnpc
        ///bấm save thì save list là ok

        //save file map json local
        File.WriteAllText(Application.dataPath + "/Data/Assets/Resources/Map_Data/" + $"{i}.json", JsonUtility.ToJson(Map.maps[i]));

        //send map to server
        ClientSend.SendSavingMap(Map.maps[i]);
    }

    public void OnSelectNPC(GameObject gameObject)
    {
        ///select NPC để spawn trên map
        if (gameObject.name.IndexOf("Npc: ") > -1)
        {
            //click in NPC
            string selected = gameObject.name.Replace("Npc: ", "");
            indexNPC = int.Parse(selected);
            Debug.Log(indexNPC);
        }
    }

    void InitListNPC()
    {
        if (listNPC.Count != 0)
        {
            foreach (GameObject gameObject in listNPC)
            {
                Destroy(gameObject);
            }
            listNPC = new List<GameObject>();
        }

        GameObject obj; // Create GameObject instance  
        
        int itemCount = 0;
        for (int i = 0; i < GAME.npcs.Length; i++)
        {
            if (GAME.npcs[i].nameNPC == null || GAME.npcs[i].nameNPC == "") continue;
            itemCount++;
            ScrollViewListNPC.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 30 * itemCount);

            // Create new instances of our prefab until we've created as many as we specified
            obj = (GameObject)Instantiate(TextInList, transform);
            obj.transform.SetParent(ScrollViewListNPC.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.transform);
            string name = GAME.npcs[i].nameNPC == null ? ": Tên này" : ": " + GAME.npcs[i].nameNPC + $" [{Enum.GetName(typeof(NPC.NPCTypes), GAME.npcs[i].type)}]";
            obj.transform.Find("Text").GetComponent<Text>().text = i + name;
            obj.name = "Npc: " + i;
            obj.transform.GetComponent<RectTransform>().localPosition = new Vector3(82, 0 - (i + 1) * 30 + 15);
            obj.SetActive(true);
            listNPC.Add(obj);
        }

        isUpdateList = false;
    }

    public void OnSelectNPCSpawn()
    {
        indexNPCSelected = indexNPC;
        SelectedIndex.text = GAME.npcs[indexNPCSelected].nameNPC;
    }
}
