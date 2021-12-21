using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoItem : MonoBehaviour
{
    public static GameObject infoTable;
    public static ItemRec item;
    public Image icon;
    public Text nameItem;
    public Text desc;
    public Text require;
    public Text addStat;
    public Text more;

    /// <summary>
    ///Default = 0 | quên set hay gì đó | màu bảng | 7D6F5B
    ///Common = 1, // thường | trắng | A0B0A5
    ///Uncommon, // phổ biết | xanh lá | 2E4D37
    ///Rare,// hiếm | xanh lam | 307890
    ///ExtremelyRare, // cực hiếm | tím | 733050
    ///Legendary, // Huyền thoại | vàng | A27F40
    ///Unique // độc nhất | đỏ | 8E3A3A
    /// </summary>
    string[] color = { "7D6F5B", "A0B0A5", "2E4D37" , "307890", "733050", "A27F40", "8E3A3A" };

    string[] nameRarity = { "", "Thường", "Phổ biến", "Hiếm", "Cực hiếm", "Huyền thoại", "Độc nhất" };

    private void OnEnable()
    {
        if (item.name == null || item.name == "")
        {
            this.gameObject.SetActive(false);
            return;
        }

        string rate = "";
        Color colorSet;
        if (item.rarity == 0)
            rate = "";
        else
            rate = $" <color=#{color[(int)item.rarity]}>[{nameRarity[(int)item.rarity]}]</color>";

        //set color info table
        ColorUtility.TryParseHtmlString("#"+color[(int)item.rarity], out colorSet);
        Inventory.InfoBar.GetComponent<Image>().color = colorSet;
        icon.sprite = Resources.Load<Sprite>(GAME.ICON_ITEM_PATH + item.icon);
        nameItem.text = item.name+ rate;
        desc.text = item.description;

        string req = "Yêu cầu:\n";
        if(item.lvlRequire != 0)
            req += $"Lvl {item.lvlRequire}\n";
        if (item.classRequire != 0)
            req += $"Class {item.classRequire}\n";
        if (item.GenderErquire != 0)
            req += $"Gender {item.GenderErquire}\n";

        if (req == "Yêu cầu:\n")
            req = "";

        require.text = req;

        if(item.stat == 0)
            addStat.text = "";
        else
            addStat.text = $"+{item.AddStat} {Enum.GetName(typeof(ItemStat), item.stat)}";

        more.text = "";
    }
}
