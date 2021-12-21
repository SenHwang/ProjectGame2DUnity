using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySkill : MonoBehaviour
{
    [Serializable]
    public struct SkillInventory
    {
        public int id;
        public SkillRec item;
        public long timeStart;
        public double timeLeft;
    }

    private static GameObject itemSlot;
    public static Transform parent;
    public static GameObject InfoBar;

    public static List<SkillInventory> skillInven;
    public static void InitInventory()
    {
        itemSlot = Resources.Load<GameObject>("Items/Prefabs/InventorySlot");
        skillInven = GameManager.players[Client.instance.myId].skillInven;

        if (skillInven == null) return;
        if (parent == null) return;
        foreach (Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (SkillInventory itemInventory in skillInven)
        {
            bool isNext = false;
            for(int i =0;i< SkillSlot.slot.Length; i++)
            {
                if (SkillSlot.slot[i].skill == null) continue;

                if (SkillSlot.slot[i].skill.GetType().ToString() == "Inventory+ItemInventory") continue;
                if (itemInventory.id == ((SkillInventory)SkillSlot.slot[i].skill).id)
                {
                    isNext = true;
                    break;
                }
            }
            if (isNext) continue;
            
            GameObject itemObject = Instantiate(itemSlot) as GameObject;
            itemObject.GetComponent<ItemsSlot>().Setup(itemInventory);
            itemObject.transform.SetParent(parent);
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
        InitInventory();
    }

}
