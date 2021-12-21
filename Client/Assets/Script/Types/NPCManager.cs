using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    public GameObject ScrollViewListNPC;
    public GameObject TextInList;

    public GameObject ScrollViewListNPCStory;
    
    #region input default
    private InputField tbxNameNpc;
    private Dropdown dropdownTypes;
    private InputField tbxSpeed;
    private Dropdown dropdownSprites;
    private Toggle movAble;
    private RawImage previewSprite;
    #endregion

    #region input NPC

    #endregion

    #region input Monster
    private InputField tbxHp;
    private InputField tbxDmg;
    private InputField tbxExp;
    private InputField tbxLvl;
    private Button itemAdd;
    private Button skillAdd;
    #endregion

    private Texture[] spritesGet;
    private List<GameObject> listNPC = new List<GameObject>();
    public static bool isUpdateList = false;
    //testing

    int countListNpcStory = 5;
    //end testing

    int index = 0;
    int indexTypes = 0;
    int indexSprites = 0;
    string nameSpriteSelect = "";

    // Start is called before the first frame update
    void Start()
    {
        LoadSprite();
        InitFieldForm();
        InsertListViewNPC();
        InsertListViewNPCStory();
        InitData();

        SetupPanel(index);
    }

    private void FixedUpdate()
    {
        if (!isUpdateList) return;
        InsertListViewNPC();
    }

    private void LoadSprite()
    {
        spritesGet = Resources.LoadAll<Texture>(GAME.NPC_PATH);
    }

    private void InitFieldForm()
    {
        // default setup
        tbxNameNpc = transform.Find("tbxName").GetComponent<InputField>();
        dropdownTypes = transform.Find("Dropdown").GetComponent<Dropdown>();
        dropdownSprites = transform.Find("DropdownSprite").GetComponent<Dropdown>();
        tbxSpeed = transform.Find("tbxSpeed").GetComponent<InputField>();
        movAble = transform.Find("Movable").GetComponent<Toggle>();
        previewSprite = transform.Find("spritePic").GetComponent<RawImage>();

        //monster setup
        tbxHp = transform.Find("Monster").Find("tbxHP").GetComponent<InputField>();
        tbxDmg = transform.Find("Monster").Find("tbxDamage").GetComponent<InputField>();
        tbxExp = transform.Find("Monster").Find("tbxExp").GetComponent<InputField>();
        tbxLvl = transform.Find("Monster").Find("tbxLvl").GetComponent<InputField>();
        itemAdd = transform.Find("Monster").Find("btnAddItem").GetComponent<Button>();
        skillAdd = transform.Find("Monster").Find("btnAddSkill").GetComponent<Button>();

        itemAdd.onClick.AddListener(delegate { OnItemAddClick(); });
        skillAdd.onClick.AddListener(delegate { OnSkillAddClick(); });
    }


    private void OnItemAddClick()
    {
        Debug.Log("Item add on index: " + index);
    }
    private void OnSkillAddClick()
    {
        Debug.Log("Skill add on index: " + index);
    }





    void InitData()
    {

        //init list NPC type dropdown
        List<string> items = new List<string>();

        var count = Enum.GetNames(typeof(NPC.NPCTypes)).Length;

        for(int i = 0; i < count; i++)
        {
            items.Add(Enum.GetName(typeof(NPC.NPCTypes), i+1));
        }

        dropdownTypes.options.Clear();
        foreach (var item in items)
        {
            dropdownTypes.options.Add(new Dropdown.OptionData() { text = item });
        }

        dropdownTypes.captionText.text = dropdownTypes.options[indexTypes].text;

        DropdownItemSelected(dropdownTypes);

        dropdownTypes.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdownTypes); });

        // list sprite
        items = new List<string>();
        var countSprite = spritesGet.Length;
        items.Add("None");
        for (int i = 0; i < countSprite; i++)
        {
            items.Add(spritesGet[i].name);
        }
        dropdownSprites.options.Clear();

        foreach (var item in items)
        {
            dropdownSprites.options.Add(new Dropdown.OptionData() { text = item });
        }

        DropdownSpriteSelected(dropdownSprites);

        dropdownSprites.onValueChanged.AddListener(delegate { DropdownSpriteSelected(dropdownSprites); });
    }

    private void DropdownSpriteSelected(Dropdown dropdownSprites)
    {
        indexSprites = dropdownSprites.value;
        Debug.Log("Selected Sprite: " + dropdownSprites.options[indexSprites].text);
        if(dropdownSprites.options[indexSprites].text == "None")
        {
            previewSprite.texture = null;
            nameSpriteSelect = "";
            return;
        }
        previewSprite.texture = spritesGet[indexSprites-1];
        nameSpriteSelect = dropdownSprites.options[indexSprites].text;
    }

    private void DropdownItemSelected(Dropdown dropdown)
    {
        indexTypes = dropdown.value;
        Debug.Log("Selected: " + dropdown.options[indexTypes].text);
        if(indexTypes == 0) //show Story NPC
        {
            transform.Find("NPC").gameObject.SetActive(true);
            transform.Find("Monster").gameObject.SetActive(false);
        }else if(indexTypes == 1) //show Monster setting
        {
            transform.Find("NPC").gameObject.SetActive(false);
            transform.Find("Monster").gameObject.SetActive(true);
        }
    }

    void SetupPanel(int index)
    {
        tbxNameNpc.text = GAME.npcs[index].nameNPC;
        dropdownTypes.value = ((int)GAME.npcs[index].type) - 1;
        DropdownItemSelected(dropdownTypes);

        tbxSpeed.text = GAME.npcs[index].speed.ToString();

        dropdownSprites.value = 
            GAME.npcs[index].sprite == null || GAME.npcs[index].sprite.ToString() == "" ?
            0 : int.Parse(GAME.npcs[index].sprite.ToString());
        DropdownSpriteSelected(dropdownSprites);

        movAble.isOn = GAME.npcs[index].movable;

        //if(GAME.nPCs[index].type == NPC.NPCTypes.NPC)
        //{
        //    //list story
        //}else if (GAME.nPCs[index].type == NPC.NPCTypes.Monster)
        //{
        tbxHp.text = GAME.npcs[index].typeMonster.hp.ToString();
        tbxDmg.text = GAME.npcs[index].typeMonster.damage.ToString();
        tbxExp.text = GAME.npcs[index].typeMonster.expGot.ToString();
        tbxLvl.text = GAME.npcs[index].typeMonster.level.ToString();

            //list list skill 
            //list list items
        //}

    }


    void InsertListViewNPC()
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
        ScrollViewListNPC.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 30 * GAME.npcs.Length);
        for (int i = 0; i < GAME.npcs.Length; i++)
        {
            if (GAME.npcs[i] == null) GAME.npcs[i] = new NPC();
            // Create new instances of our prefab until we've created as many as we specified
            obj = (GameObject)Instantiate(TextInList, transform);
            obj.transform.SetParent(ScrollViewListNPC.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.transform);
            string name = GAME.npcs[i].nameNPC == null ? ": Tên này" :": "+ GAME.npcs[i].nameNPC + $" [{Enum.GetName(typeof(NPC.NPCTypes), GAME.npcs[i].type)}]";
            obj.transform.Find("Text").GetComponent<Text>().text = i+ name;
            obj.name = "Npc: " + i;
            obj.transform.GetComponent<RectTransform>().localPosition = new Vector3(obj.transform.GetComponent<RectTransform>().localPosition.x, 0 - (i + 1) * 30 + 15);
            obj.SetActive(true);
            listNPC.Add(obj);
        }

        isUpdateList = false;
    }

    void InsertListViewNPCStory()
    {  
        GameObject obj; // Create GameObject instance  
        ScrollViewListNPCStory.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector3(0, 18 * countListNpcStory);
        ScrollViewListNPCStory.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.GetComponent<RectTransform>().localPosition = new Vector3();
        for (int i = 0; i < countListNpcStory; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            obj = (GameObject)Instantiate(TextInList, transform);
            obj.transform.SetParent(ScrollViewListNPCStory.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.transform);
            obj.transform.Find("Text").GetComponent<Text>().text = i + ": Story này";
            obj.name = "Story: "+i;
            obj.transform.GetComponent<RectTransform>().localPosition = new Vector3(150, 0 - (i + 1) * 18 + 9);
            obj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 18);
            obj.SetActive(true);
            
        }

        
    }

    public void OnListViewNPCClicked(GameObject gameObject)
    {
        //get đc item click
        Debug.Log(gameObject.name);

        if(gameObject.name.IndexOf("Story: ") > -1)
        {
            //click in Story
            string selected = gameObject.name.Replace("Story: ", "");
        }
        else if (gameObject.name.IndexOf("Npc: ") > -1)
        {
            //click in NPC
            string selected = gameObject.name.Replace("Npc: ", "");
            index = int.Parse(selected);
            SetupPanel(index);
        }
        //TODO show data trong List với index = name;
    }

    public void OnSavingClicked()
    {
        GAME.npcs[index].nameNPC = tbxNameNpc.text;
        float speedGet;
        float.TryParse(tbxSpeed.text, out speedGet);
        GAME.npcs[index].speed = speedGet;
        GAME.npcs[index].type = indexTypes == 0 ? NPC.NPCTypes.NPC : NPC.NPCTypes.Monster;
        GAME.npcs[index].movable = movAble.isOn;
        GAME.npcs[index].sprite = nameSpriteSelect;

        if (indexTypes == 0)
        {
            GAME.npcs[index].story = new List<NPC.Story>();
            //chưa làm
        }
        else if (indexTypes == 1)
        {
            int cache;
            int.TryParse(tbxHp.text, out cache);
            GAME.npcs[index].typeMonster.hp = cache;

            int.TryParse(tbxDmg.text, out cache);
            GAME.npcs[index].typeMonster.damage = cache;

            int.TryParse(tbxExp.text, out cache);
            GAME.npcs[index].typeMonster.expGot = cache;

            int.TryParse(tbxLvl.text, out cache);
            GAME.npcs[index].typeMonster.level = cache;

            GAME.npcs[index].typeMonster.Items = new List<NPC.ItemMonsterDrop>();
            GAME.npcs[index].typeMonster.Skills = new List<NPC.SkillMonster>();
        }
        ClientSend.SendNPCSavingToServer(index, GAME.npcs[index]);
        InsertListViewNPC();
    }
}
