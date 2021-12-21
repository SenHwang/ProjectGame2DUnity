using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSkill : MonoBehaviour
{
    public static GameObject infoTable;
    public static SkillRec item;
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
        //if (item.rarity == 0)
        //    rate = "";
        //else
        //    rate = $" <color=#{color[(int)item.rarity]}>[{nameRarity[(int)item.rarity]}]</color>";
        //.TryParseHtmlString("#" + color[(int)item.rarity], out colorSet);



        rate = $" <color=#{color[0]}>[{nameRarity[0]}]</color>";
        //set color info table
        ColorUtility.TryParseHtmlString("#"+color[0], out colorSet);

        InventorySkill.InfoBar.GetComponent<Image>().color = colorSet;
        icon.sprite = Resources.Load<Sprite>(GAME.ICON_ITEM_SKILL_PATH + item.spritePreview);
        nameItem.text = item.name+ rate;
        desc.text = item.description;

        
        string req = "CD Time: "+item.countdown + $"s\n Mp: {item.mpCost}\nCast: {item.castTime}s\nYêu cầu:\n";
        if (item.lvlRequire != 0)
            req += $"Lvl {item.lvlRequire}\n";
        if (item.classRequire != 0)
            req += $"Class {item.classRequire}\n";

        if(item.targetAble)
            req += $"Target: True\n";
        else
            req += $"Target: none\n";

        require.text = req;

        switch (item.skillTypes)
        {
            case SkillTypes.Line:
                more.text = "";
                if (item.range != 0)
                    more.text += $"Range: {item.range}T\n";

                if(item.len != 0)
                    more.text += $"Length: { item.len}T";

                addStat.text = "";
               
                if (item.numDmgSpec != 0)
                {
                    addStat.text += $"Dmg: {item.numDmgSpec}*{item.skillSpec}%";
                }
                
                break;
            case SkillTypes.Place:
                more.text = "";
                if (item.range != 0)
                    more.text += $"Range: {item.range}T\n";

                if (item.len != 0)
                    more.text += $"Len: {item.len}T";

                addStat.text = "";
                if (item.skillUse == SkillUse.Damage)
                    addStat.text += "Dmg:\n";
                if (item.skillUse == SkillUse.Buff)
                    addStat.text += "Buff:\n";
                if (item.skillUse == SkillUse.DeBuff)
                    addStat.text += "DeBuff:\n";                

                if (item.numDmgSpec != 0)
                {
                    addStat.text += $"{item.numDmgSpec}*{item.skillSpec}%";
                }

                break;
            case SkillTypes.Shield:
                more.text = "";
                addStat.text = "";
                break;
            case SkillTypes.Teleport:
                more.text = $"TP to mapID:{item.idMapTarget}";
                addStat.text = "";
                break;
            case SkillTypes.JumpInto:
                more.text = "";
                if (item.range != 0)
                    more.text += $"Range: {item.range}T\n";

                if (item.len != 0)
                    more.text += $"Len: {item.len}T";

                addStat.text = "";
                if (item.skillUse == SkillUse.Damage)
                    addStat.text += "Dmg:\n";
                if (item.skillUse == SkillUse.Buff)
                    addStat.text += "Buff:\n";
                if (item.skillUse == SkillUse.DeBuff)
                    addStat.text += "DeBuff:\n";

                if (item.numDmgSpec != 0)
                {
                    addStat.text += $"{item.numDmgSpec}*{item.skillSpec}%";
                }
                break;
            default:
                more.text = "";
                addStat.text = "";
                break;

        }
      

        //if(item.stat == 0)
        //    addStat.text = "";
        //else
        //    addStat.text = $"+{item.AddStat} {Enum.GetName(typeof(ItemStat), item.stat)}";


    }
}
