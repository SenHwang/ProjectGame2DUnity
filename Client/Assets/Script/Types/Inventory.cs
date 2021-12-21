using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory: MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    public static Inventory instance;

    public static GameObject money;

    private static GameObject itemSlot;
    public static Transform parent;
    public static GameObject InfoBar;

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


    [Serializable]
    public struct ItemInventory
    {
        public int id;
        public ItemRec item;
        public int count;
    }

    public static List<ItemInventory> items;
   
    public static void InitInventory()
    {    
        itemSlot = Resources.Load<GameObject>("Items/Prefabs/InventorySlot");
        //nếu mà items == null là cái túi chưa đc khởi tạo
        //khởi tạo túi với item money = 0 tiền
        items = GameManager.players[Client.instance.myId].items;

        if (items == null || items.Count == 0) return;
        if (parent == null) return;
        foreach (Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //init cái data tạm này
        //foreach (ItemRec item in GAME.items)
        //{
        //    if (items.Count == GAME.MAX_INVENTORY)
        //    {
        //        Debug.Log("Full inventory!");
        //        return;
        //    }

        //    if (item.name == null || item.name == "") continue;
        //    ItemInventory inventory = new ItemInventory();
        //    inventory.item = item;
        //    inventory.count = 1;           
        //    AddItem(inventory);
        //}

        foreach (ItemInventory itemInventory in items)
        {
            //nếu là cái money thì update moneybar
            if (itemInventory.item.name == "Money")
            {
                money.transform.Find("CountMoney").gameObject.GetComponent<Text>().text = string.Format("{000:#,000}", itemInventory.count) == "000"?"0": string.Format("{000:#,000}", itemInventory.count);
                money.transform.Find("MoneyItem").gameObject.GetComponent<ItemsSlot>().Setup(itemInventory);
                continue;
            }

            bool isNext = false;
            for (int i = 0; i < SkillSlot.slot.Length; i++)
            {
                if (SkillSlot.slot[i].skill == null) continue;

                if (SkillSlot.slot[i].skill.GetType().ToString() == "Inventory+ItemInventory")
                {
                    if (itemInventory.id == ((ItemInventory)SkillSlot.slot[i].skill).id)
                    {
                        isNext = true;
                        break;
                    }
                }
                continue;
            }
            if (isNext) continue;

            GameObject itemObject = Instantiate(itemSlot) as GameObject;
            itemObject.GetComponent<ItemsSlot>().Setup(itemInventory);            
            itemObject.transform.SetParent(parent);
            itemObject.GetComponent<DragDrop>().canvas = Inventory.instance.canvas;
        }

    }

    private void OnEnable()
    {
        InitInventory();
    }

    private void Start()
    {
        parent = this.gameObject.transform.Find("InventorySlot").transform;
        InfoBar = this.gameObject.transform.Find("Info").gameObject;
        money = this.gameObject.transform.Find("Money").gameObject;
        InitInventory();
    }

}
