using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Equipment
{
    Body = 1,
    Hair,
    Hat,
    Pants,
    Shoe,
    Clother,
    Weapon
}

public enum ItemStat
{
    Strength = 1,
    Agility,
    Intellect,
    Mp,
    Hp
}

[Serializable]
public enum Rarity
{
    Common = 1, // thường | trắng
    Uncommon, // phổ biết | xanh lá
    Rare,// hiếm | xanh lam
    ExtremelyRare, // cực hiếm | tím
    Legendary, // Huyền thoại | vàng
    Unique // độc nhất | đỏ
}

[Serializable]
public struct ItemRec
{
    /// <summary>
    /// name: tên item
    /// </summary>
    public string name;

    /// <summary>
    /// Mô tả item
    /// </summary>
    public string description;

    /// <summary>
    /// sound: sound item khi dùng
    /// </summary>
    public string sound;

    /// <summary>
    /// icon: icon hiện trong túi đồ của item
    /// </summary>
    public string icon;

    /// <summary>
    /// Số lượng lớn nhất mà một người có thể nhận
    /// </summary>
    public int? maximumInBag;

    /// <summary>
    /// Iteam có cộng dồn/chồng lên nhau được hay không
    /// </summary>
    public bool stackable;

    /// <summary>
    /// Giá vật phẩm.
    /// khi bán thì nhận được 70% so với giá gốc
    /// </summary>
    public long price;

    /// <summary>
    /// Nếu là các trang bị thì equip trên slot nào của equipment
    /// </summary>
    public Equipment EquipSlot;

    /// <summary>
    /// vật phẩm có thể equip trên skill bar được không
    /// </summary>
    public bool EquipSkillBar;

    /// <summary>
    /// animation khi dùng item
    /// </summary>
    public string Animation;

    //require
    /// <summary>
    /// cấp cần để dùng item
    /// </summary>
    public int lvlRequire;

    /// <summary>
    /// class cần để dùng item
    /// </summary>
    public byte classRequire;

    /// <summary>
    /// giới tính yêu cầu để dùng vật phẩm
    /// </summary>
    public byte GenderErquire;

    /// <summary>
    /// skill cần để dùng item
    /// </summary>
    public int skillRequire;

    /// <summary>
    /// cấp độ hiếm của item
    /// </summary>
    public Rarity rarity;


    /// <summary>
    /// AddStat thêm stat khi dùng item
    /// với AddStat là số còn stat là stat thêm
    /// </summary>
    public ItemStat stat;
    public int AddStat;

    public int AddHp;
    public int AddMp;
    public int AddExp;

    /// <summary>
    /// gọi nhận được skill khi dùng kỹ năng với id skill 
    /// </summary>
    public int CastSkill;

    /// <summary>
    /// có thể tái sử dụng
    /// </summary>
    public bool IsReusable;

}

public class Items
{
    
}
