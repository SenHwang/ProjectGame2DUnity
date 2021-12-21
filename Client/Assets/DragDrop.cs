using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, /*IPointerDownHandler,*/ IBeginDragHandler, IEndDragHandler, IDragHandler //, IDropHandler
{
    [SerializeField]
    public Canvas canvas;
    public static GameObject itemDragger;
    public static SkillInSlot itemCache = new SkillInSlot();

    private RectTransform rect;
    private CanvasGroup canvasGroup;
    private Vector3 startDrag;
    private Transform starParent;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        itemDragger = gameObject;
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        startDrag = transform.position;
        starParent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        itemCache = new SkillInSlot();
        //kiểm tra khi mà player kéo item ra ngoài skillbar thì sẽ remove trên thanh skillbar
        
        bool isSkill = this.gameObject.GetComponent<ItemsSlot>().isSkill;
        for (int i = 0; i < SkillSlot.slot.Length; i++)
        {
            if (SkillSlot.slot[i].skill == null) continue;
            if (SkillSlot.slot[i].skill.GetType().ToString() == "Inventory+ItemInventory" && isSkill == false)
            {
                Inventory.ItemInventory itemGet = this.gameObject.GetComponent<ItemsSlot>().thisItem;
                if (((Inventory.ItemInventory)SkillSlot.slot[i].skill).id == itemGet.id)
                {
                    itemCache = SkillSlot.slot[i];
                    return;
                }
                continue;
            }
            else if (SkillSlot.slot[i].skill.GetType().ToString() == "InventorySkill+SkillInventory" && isSkill == true)
            {
                InventorySkill.SkillInventory skillGet = this.gameObject.GetComponent<ItemsSlot>().skillItem;
                if (((InventorySkill.SkillInventory)SkillSlot.slot[i].skill).id == skillGet.id)
                {
                    itemCache = SkillSlot.slot[i];
                    return; ;
                }
                continue;
            }
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        itemDragger = null;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        //kiểm tra khi mà player kéo item ra ngoài skillbar thì sẽ remove trên thanh skillbar
        if (starParent == transform.parent && starParent.name != "InventorySlot")
        {
            bool isSkill = this.gameObject.GetComponent<ItemsSlot>().isSkill;            
            for(int i=0;i< SkillSlot.slot.Length; i++)
            {
                if (SkillSlot.slot[i].skill == null) continue;
                if (SkillSlot.slot[i].skill.GetType().ToString() == "Inventory+ItemInventory" && isSkill == false)
                {
                    Inventory.ItemInventory itemGet = this.gameObject.GetComponent<ItemsSlot>().thisItem;
                    if (((Inventory.ItemInventory)SkillSlot.slot[i].skill).id == itemGet.id)
                    {
                        SkillSlot.slot[i] = new SkillInSlot();
                        break;
                    }continue;
                }
                else if(SkillSlot.slot[i].skill.GetType().ToString() == "InventorySkill+SkillInventory" && isSkill == true)
                {
                    InventorySkill.SkillInventory skillGet = this.gameObject.GetComponent<ItemsSlot>().skillItem;
                    if (((InventorySkill.SkillInventory)SkillSlot.slot[i].skill).id == skillGet.id)
                    {
                        SkillSlot.slot[i] = new SkillInSlot();
                        break;
                    }continue;
                }
            }
            
            Destroy(this.gameObject);

        }

        transform.position = startDrag;
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log("OnPointerDown");
    //}

    //public void OnDrop(PointerEventData eventData)
    //{
    //    Debug.Log("OnDragDrop");
    //}
}
