using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static NPC;

public class StoryGameFunc : MonoBehaviour
{
    public static StoryGameFunc instance;
    /// <summary>
    /// sence story
    /// </summary>
    public GameObject storyObject;

   

    /// <summary>
    /// sence shop
    /// </summary>
    public GameObject shopObject;

    /// <summary>
    /// này là object title story khi mà player bấm vào npc
    /// khi npc có story thì sẽ display cái này ra
    /// default y = 80 nếu thêm story title thì y+=30 cho mỗi story title
    /// bắt đầu với 270,80
    /// </summary>
    public GameObject titleStory;

    /// <summary>
    /// này là nội dung mà khi player click vào story
    /// sẽ hiển thị message của story
    /// </summary>
    public GameObject displayMessage;

    public GameObject displayNameNPC;

    private static List<Story> storyGet;
    private static string nameNPC;
    private static string desc;

    public static List<GameObject> listTitleStory;

   

    //này là index cái story nào
    public int indexStory = -1;
    //này là index message trong story đó
    public int indexStep = -1;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Debug.Log("next mess");
            if (indexStory == -1)
                return;
            else
                ShowStory(storyGet, nameNPC, desc);
        }
    }

    //private void OnEnable()
    //{
    //    //khi mà màn hình story enable thì disable movement player
    //    //disable toàn bộ sence của UI chỉ enable mỗi Story
    //    UIManagerFunc.instance.EnableObject(this.gameObject);
    //}

    public static void SetStory(List<Story> story, string npcName, string desc)
    {
        StoryGameFunc.storyGet = story;
        StoryGameFunc.nameNPC = npcName;
        StoryGameFunc.desc = desc;
        StoryGameFunc.instance.displayNameNPC.transform.Find("Text").gameObject.GetComponent<Text>().text = npcName;
        StoryGameFunc.instance.ShowStory(story, npcName, desc);
    }

    internal static void NextPageStory()
    {
        StoryGameFunc.instance.ShowStory(StoryGameFunc.storyGet, StoryGameFunc.nameNPC, StoryGameFunc.desc);
    }

    public void ShowStory(List<Story> story, string npcName, string desc)
    {    
        if(StoryGameFunc.listTitleStory != null)
        {
            for (int i = 0; i < StoryGameFunc.listTitleStory.Count; i++)
            {
                Destroy(StoryGameFunc.listTitleStory[i].gameObject);                
            }
        }  
        
        if(story.Count != 0)
        {
            StoryGameFunc.listTitleStory = new List<GameObject>();
            for (int i = 0; i < story.Count; i++)
            {
                GameObject titleSpawn = Instantiate(titleStory) as GameObject;
                titleSpawn.transform.SetParent(storyObject.gameObject.transform);
                titleSpawn.SetActive(true);
                titleSpawn.GetComponent<StoryOnClick>().idStory = i;
                titleSpawn.GetComponent<StoryOnClick>().type = story[i].type;
                titleSpawn.transform.Find("Text").gameObject.GetComponent<Text>().text = story[i].title;
                StoryGameFunc.listTitleStory.Add(titleSpawn);
            }
        }

        //nếu mà indexStory chưa được set thì đây là bắt đầu story
        if (indexStory == -1)
        {
            if (desc == "")
                this.displayMessage.SetActive(false);
            else
            {
                this.displayMessage.SetActive(true);
                this.displayMessage.GetComponent<Text>().text = desc;
            }
        }
        else
        {
            if (indexStep == -1)
                indexStep = 0;

            if(story[indexStory].text.Count > indexStep)
            {
                this.displayMessage.SetActive(true);
                this.displayMessage.GetComponent<Text>().text = story[indexStory].text[indexStep];
                indexStep++;
            }
            else
            {
                UIManagerFunc.instance.EnableObject(UIManagerFunc.instance.canvasObject);
                indexStep = -1;
                indexStory = -1;
            }            
           
        }

    }
}
