using UnityEngine;
using UnityEngine.UI;
using static Inventory;
using static InventorySkill;

public class ItemsSlot : MonoBehaviour
{
    public Image iconItem;
    public GameObject picItem;
    public GameObject count;

    private RectTransform rect;
    private CanvasGroup canvasGroup;

    public ItemInventory thisItem;
    public SkillInventory skillItem;

    private float Click = 0;
    private bool isHit = false;
    public bool isSkill = false;
    private void Start()
    {
        this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        if (thisItem.item.name == "" && skillItem.item.name == "") return;
        this.GetComponent<DragDrop>().canvas = UIManagerFunc.instance.canvasObject.GetComponent<Canvas>();
    }

    private void Update()
    {
        if (isHit)
        {
            if (Click <= 0.2f)
            {
                Click += Time.deltaTime;
            }
            else
            {
                Click = 0;
                isHit = false;
            }
        }
    }


    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (isHit == true) return;

            if (isSkill)
            {
                //call skill
                Debug.Log("Skill: " + skillItem.item.name);
            }
            else
                Debug.Log(thisItem.item.name);

            isHit = true;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (isHit == true) return;

            if (!isSkill)
                Debug.Log(thisItem.count);

            isHit = true;
        }

        if (!isSkill)
        {
            InfoItem.item = thisItem.item;
            Inventory.InfoBar.SetActive(true);
        }
        else
        {
            InfoSkill.item = skillItem.item;
            InventorySkill.InfoBar.SetActive(true);
            // tạo info skill
        }
    }

    private void OnMouseExit()
    {
        if (isSkill)
            InventorySkill.InfoBar.SetActive(false);
        else
            Inventory.InfoBar.SetActive(false);
    }

    //call item box item
    public void Setup(ItemInventory itemGet)
    {
        isSkill = false;
        thisItem = itemGet;
        Sprite icon = Resources.Load<Sprite>(GAME.ICON_ITEM_PATH + itemGet.item.icon);
        iconItem.sprite = icon;
        if (itemGet.item.stackable)
        {
            count.SetActive(true);
            count.GetComponent<Text>().text = itemGet.count.ToString();
        }
        else
            count.SetActive(false);
    }

    //call item box skill
    public void Setup(SkillInventory itemGet)
    {
        count.SetActive(false);
        isSkill = true;
        skillItem = itemGet;
        Sprite icon = Resources.Load<Sprite>(GAME.ICON_ITEM_SKILL_PATH + itemGet.item.spritePreview);

        iconItem.sprite = icon;

        picItem.transform.GetComponent<RectTransform>().position = new Vector3(16.5f, 0, 0);
        picItem.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 30);
        picItem.transform.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 0);

        picItem.transform.GetComponent<Image>().type = Image.Type.Filled;
        picItem.transform.GetComponent<Image>().fillMethod = Image.FillMethod.Horizontal;
        picItem.transform.GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Left;
        picItem.transform.GetComponent<Image>().fillAmount = 0.5f;
    }

}
