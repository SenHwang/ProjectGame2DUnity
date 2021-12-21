using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerEquipAnim : MonoBehaviour {

    [Header("Object Equipment:")]
    public GameObject[] EquipSet = new GameObject[7];
    [Space]

    [Header("Rigidbody2D:")]
    public Rigidbody2D rb;

    #region player setting
    private int spriteStop = 0;   
    int speedAnim = 3;
    int idPlayer;
    string namePlayer;
    string[] equipmentSlot;
    float IsPunch;
    float IsDead;
    float IsKick;

    bool iskicked = false;

    float counterHit = 0;
    #endregion




    #region primary sprite equipment
    private SpriteRenderer[] spriteRenderer = new SpriteRenderer[7];
    private Dictionary<string, Sprite>[] spriteSheet = new Dictionary<string, Sprite>[7];
    private Sprite[][] spriteGet = new Sprite[7][];
    private string[] spriteIndex = new string[7];//equip lưu từ equipmentSlot trước đó. dùng để check khi có thay đổi thì update
    #endregion

    string[] PATHEQUIP = new string[]
                            {
                                GAME.BODY_PATH,
                                GAME.HAIR_PATH,
                                GAME.HAT_PATH,
                                GAME.PANTS_PATH,
                                GAME.SHOE_PATH,
                                GAME.CLOTHER_PATH,
                                GAME.WEAPON_PATH
                            };
    // Use this for initialization
    /*
     * Gọi data component từ PlayerManager
     * khai báo equipmentSlot = 7 
     * Load Sprite
     */
    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //        idPlayer = this.GetComponent<PlayerManager>().id;
    //        namePlayer = this.GetComponent<PlayerManager>().username;
    //        equipmentSlot = new string[7];
    //        LoadSprite();
    //        if (GameManager.players.Count <= Client.instance.myId) return;
    //        equipmentSlot = GameManager.players[Client.instance.myId].Equipment;
    //    }
    //    else if (instance != this)
    //    {
    //        Debug.Log("Instance already exists, destroying object!");
    //        Destroy(this);
    //    }
    //}

    void Start () {

        for (int i = 0; i < 7; i++)
        {
            spriteRenderer[i] = EquipSet[i].GetComponent<SpriteRenderer>();
        }

        LoadInfo();
    }

    public void LoadInfo()
    {
        idPlayer = this.GetComponent<Player>().id;
        namePlayer = this.GetComponent<Player>().username;
        equipmentSlot = new string[7];
        LoadSprite();
        //if (GameManager.players.Count <= Client.instance.myId) return;
        equipmentSlot = GameManager.players[idPlayer].Equipment;
    }

    void LoadSprite()
    {
        for(int i = 0; i < equipmentSlot.Length; i++)
        {
            int numEq;
            int.TryParse(equipmentSlot[i], out numEq);
           
            if (numEq <=0 ) continue;
            spriteGet[i] = null;
            spriteGet[i] = Resources.LoadAll<Sprite>(PATHEQUIP[i]+"/" + equipmentSlot[i]);
        }
       
        //spriteGet[1] = Resources.LoadAll<Sprite>(GAME.HAIR_PATH);
        //spriteGet[2] = Resources.LoadAll<Sprite>(GAME.HAT_PATH);
        //spriteGet[3] = Resources.LoadAll<Sprite>(GAME.PANTS_PATH);
        //spriteGet[4] = Resources.LoadAll<Sprite>(GAME.SHOE_PATH);
        //spriteGet[5] = Resources.LoadAll<Sprite>(GAME.CLOTHER_PATH);
        //spriteGet[6] = Resources.LoadAll<Sprite>(GAME.WEAPON_PATH);        
    }

    public void LateUpdate()
    {
        try
        {
            for (int i = 0; i < 7; i++)
            {
                //if (equipmentSlot[i] == "") continue;
                
                if (spriteIndex[i] == null)
                    spriteIndex[i] = "";

                //if (equipmentSlot[i] != spriteIndex[i])
                //{
                //    LoadSpriteEach(i);
                //    if (this.GetComponent<Player>().id == Client.instance.myId)
                //    {
                //        ClientSend.PlayerEquip(this.GetComponent<Player>().id, equipmentSlot);//send equipment to server
                //    }

                //    spriteIndex[i] = equipmentSlot[i];
                //}
                if(equipmentSlot[i] == "")
                {
                    this.spriteRenderer[i].sprite = null;
                }            
                else
                {
                    if (spriteSheet[i] == null)
                    {
                        LoadSpriteEach(i);
                    }
                    this.spriteRenderer[i].sprite = spriteSheet[i][this.spriteRenderer[i].sprite.name];
                }
            }               

        }
        catch (Exception _ex)
        {
            
            LoadSprite();
            Debug.Log($"Sprite Not Found: LateUpdate()|id: {idPlayer}| Exception:"+_ex);
        }           
    }
    
    private void LoadSpriteEach(int equipSlot)
    {
        if (equipmentSlot[equipSlot] != "")
        {
            spriteSheet[equipSlot] = spriteGet[equipSlot].Where(s => s.name.IndexOf(equipmentSlot[equipSlot] + "_") >= 0).ToArray()
            .ToDictionary(x => x.name.Replace(x.name.Substring(0, x.name.IndexOf("_") + 1), "1_"), x => x);//x => x.name.Replace(x.name.Substring(0, x.name.IndexOf("_") + 1), "1_"), x => x
        }     
     

    }


    // Update is called once per frame
    public void Update () {
        if (iskicked)
        {
            //chỉnh cho cái anim punch chỉ hiện trong .3f
            if (counterHit <= 0.3f)
                counterHit += Time.deltaTime;
            else
            {
                counterHit = 0;
                iskicked = false;
            }
        }

        if (this.GetComponent<Player>().id != Client.instance.myId)
        {
            equipmentSlot = this.GetComponent<Player>().Equipment;
            spriteStop = this.GetComponent<Player>().spriteStop;
            speedAnim = this.GetComponent<Player>().speedAnim;
            IsDead = this.GetComponent<Player>().IsDead == true ? 1 : 0;
            IsKick = this.GetComponent<Player>() == true ? 3 : 0;
            IsPunch = this.GetComponent<Player>().IsPunching == true ? 1 : 0;
        }
        else
        {
            equipmentSlot = this.GetComponent<Player>().Equipment;
            spriteStop = this.GetComponent<Player>().spriteStop;
            speedAnim = this.GetComponent<Player>().speedAnim;
            IsDead = this.GetComponent<Player>().IsDead == true ? 1 : 0;

            if (!iskicked)
            {
                IsPunch = this.GetComponent<Player>().IsPunching == true ? 1 : 0;
                IsKick = (UnityEngine.Random.Range(0, 2)) * 3;
                iskicked = true;
                if (IsKick == 3)
                    this.GetComponent<Player>().IsKick = true;
            }
        }

        

        

        Animate();
    }
    

    void Animate()
    {
        for (int i = 0; i < 7; i++)
        {
            AnimatorObjectAlot(i);
        }
       
    }
    void AnimatorObjectAlot(int equipSlot)
    {
        EquipSet[equipSlot].GetComponent<Animator>().enabled = spriteRenderer[equipSlot].enabled = (equipmentSlot[equipSlot] != "") ? true :false;
        EquipAnimAlot(equipSlot);
    }
    void EquipAnimAlot(int equipSlot)
    {
        if(equipmentSlot[equipSlot] != "")
        {
            EquipSet[equipSlot].GetComponent<Animator>().SetFloat("Dir", spriteStop);
            EquipSet[equipSlot].GetComponent<Animator>().SetFloat("SpeedMove", speedAnim);
            EquipSet[equipSlot].GetComponent<Animator>().SetFloat("Dead", IsDead);
            EquipSet[equipSlot].GetComponent<Animator>().SetFloat("Punch", IsPunch); 
            EquipSet[equipSlot].GetComponent<Animator>().SetFloat("Kick", IsKick);            
        }        
    }
    
}
