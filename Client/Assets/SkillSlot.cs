using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public struct SkillInSlot
{
    public System.Object skill;
    public long timeStart;
    public double timeLeft;
}

public class SkillSlot : MonoBehaviour, IDropHandler
{
    public static SkillInSlot[] slot = new SkillInSlot[10];

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (!item)
        {
            DragDrop.itemDragger.transform.SetParent(transform);
        }
        int slotIndex = int.Parse(this.gameObject.name) - 1;

        if (DragDrop.itemCache.skill != null)
        {
            slot[slotIndex] = DragDrop.itemCache;
            return;
        }

        if (DragDrop.itemDragger.GetComponent<ItemsSlot>().isSkill)
        {
            SkillInSlot skillGet = new SkillInSlot();
            skillGet.skill = DragDrop.itemDragger.GetComponent<ItemsSlot>().skillItem;
            slot[slotIndex] = skillGet;
        }
        else
        {            
            SkillInSlot skillGet = new SkillInSlot();
            skillGet.skill = DragDrop.itemDragger.GetComponent<ItemsSlot>().thisItem;
            slot[slotIndex] = skillGet;
        }
        
        //if(eventData.pointerDrag != null)
        //{
        //    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        //}
    }


    public static void OnUseSkill(int index)
    {
        if(slot[index].skill == null)
        {
            Debug.Log("Item is not available!");
            return;
        }
        bool isSkill = false;
        if (slot[index].skill.GetType().ToString() == "Inventory+ItemInventory")
        {
            if (((Inventory.ItemInventory) slot[index].skill).item.name == null)
            {
                Debug.Log("cannot use this Item!");
                return;
            }
        }
        else
        {
            if (((InventorySkill.SkillInventory)slot[index].skill).item.name == null)
            {
                Debug.Log("Cannot use this Skill!");
                return;
            }
            isSkill = true;
        }

        if (isSkill != true)
            return;

        //if skill hết cd thì cho dùng
        //else khi cd vẫn còn thì k cho dùng skill
        if (slot[index].timeStart == 0)
        {
            slot[index].timeStart = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
            Debug.Log("Dùng skill: " + ((InventorySkill.SkillInventory)slot[index].skill).item.name);
            
            //send packet về server hỏi req call skill
            //TODO: Sendpaket req skill
            //đồng ý thì callSkill
            SkillCall.CallSkill((InventorySkill.SkillInventory)slot[index].skill);
        }
    }

   

    private void Update()
    {
        if (item == null) return;

        int index = int.Parse(this.name) - 1;
        if (slot[index].skill == null) return;
        if (slot[index].skill.GetType().ToString() == "Inventory+ItemInventory") return;

        if (slot[index].timeStart == 0) return;
        Transform itemSlot = this.transform.Find("InventorySlot(Clone)");
        //time mới mỗi update sẽ lớn hơn time cũ nên lấy time mới- time cũ khi bắt đầu bấm skill
        double.TryParse(((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - slot[index].timeStart).ToString(), out double timeParse);
        slot[index].timeLeft = TimeSpan.FromMilliseconds(timeParse).TotalSeconds;

        if (((float)slot[index].timeLeft) >= ((InventorySkill.SkillInventory)slot[index].skill).item.countdown)
        {
            slot[index].timeLeft = 0;
            slot[index].timeStart = 0;

            
            if (itemSlot == null) return;
            itemSlot.GetComponent<ItemsSlot>().count.SetActive(false);

            //đổi màu skill từ disable thành enable
            itemSlot.GetComponent<ItemsSlot>().picItem.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(16.5f, 0, 0);
            itemSlot.GetComponent<ItemsSlot>().picItem.GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Left;
        }
        else
        {
            if (itemSlot == null) return;
            itemSlot.GetComponent<ItemsSlot>().count.SetActive(true);
            int testCeiling = (int)Math.Ceiling((((InventorySkill.SkillInventory)slot[index].skill).item.countdown - ((float)slot[index].timeLeft)));
            itemSlot.GetComponent<ItemsSlot>().count.GetComponent<Text>().text = testCeiling + "";

            //đổi màu skill từ enable thành disable
            itemSlot.GetComponent<ItemsSlot>().picItem.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-16.5f, 0, 0);
            itemSlot.GetComponent<ItemsSlot>().picItem.GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Right;
        }
    }
}
