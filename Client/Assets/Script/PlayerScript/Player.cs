using Assets.Script.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;
using static InventorySkill;

[Serializable]
public class Player : MonoBehaviour {

    public int id;
    public string username;

    public Vector3 position;
    public Quaternion rotation;

    /// <summary>
    /// sprite stop
    /// 0: down
    /// 4: Left
    /// 8: Right
    /// 12: Up
    /// </summary>
    public int spriteStop = 0;

    /// <summary>
    /// speedAnim
    /// 0: stop
    /// 3: run
    /// </summary>
    public int speedAnim = 0;

    public bool IsPunching;
    public bool IsDead;
    public bool IsKick;

    //public float maxSpeed;
    public int mapID;

    public string[] Equipment = new string[7];

    public List<SkillInventory> skillInven;
    public List<ItemInventory> items;

    public Stats stat = new Stats();
    public PartyRec party = new PartyRec();
    
    //role
    public Role role;

    //speed movement
    public float speedWalk = 10f;

    public Player()
    {

    }

    public Player(int _id, string username, int mapID):this(){
        this.id = _id;
        this.username = username;
        this.mapID = mapID;
    }
}

[Serializable]
public class Stats
{
    #region STAT ĐỘNG
    public int level = 1;
    public int exp = 100; //TODO: này sau có file exp list thì add vào này
    public int expOwn;

    public int health;
    public int mana;
    public int healthLeft;
    public int manaLeft;

    //Sức mạnh
    public int strength = 5;
    //Nhanh nhẹn
    public int agility = 5;
    //Tinh thần
    public int intellect = 5;
    //Mana
    public int mp = 5;
    //HP
    public int hp = 5;
    // điểm tăng skill còn
    public int pointFree = 0;
    #endregion

    #region STAT TĨNH
    //Luck
    public float luck = 0;
    //tỉ lệ bạo kích
    public float tlbk = 10;
    //sát thương bạo kích
    public float stbk = 10;
    //kháng phép
    public float kPhep = 10;
    //kháng vật lí
    public float kVL = 10;
    //tốc hồi phục
    public float healRegen = 10;
    #endregion


    public Stats()
    {

    }

    public Stats(int level, int exp, int expOwn, int srtength, int agility, int intellect, int mp, int hp, int pointFree, float luck, float tlbk, float stbk, float kPhep, float kVL, float healRegen)
    {
        this.level = level;
        this.exp = exp;
        this.expOwn = expOwn;
        this.strength = srtength;
        this.agility = agility;
        this.intellect = intellect;
        this.mp = mp;
        this.hp = hp;
        this.pointFree = pointFree;
        this.luck = luck;
        this.tlbk = tlbk;
        this.stbk = stbk;
        this.kPhep = kPhep;
        this.kVL = kVL;
        this.healRegen = healRegen;
    }
  



    public void GainExp(int expGot)
    {
        this.expOwn += expGot;
        if (this.expOwn >= exp)
        {
            LevelUp(1);
            this.expOwn = (this.expOwn - this.exp) > 0 ? this.expOwn - this.exp : 0;
        }
    }

    public void LevelUp(int levelAdd)
    {
        this.level += levelAdd;
        this.pointFree += 5 * levelAdd;
    }

    public void UpdateStat(int srtength, int agility, int intellect, int mp, int hp)
    {
        this.strength = srtength;
        this.agility = agility;
        this.intellect = intellect;
        this.mp = mp;
        this.hp = hp;
    }

    public void ResetStat()
    {
        this.strength = 5;
        this.agility = 5;
        this.intellect = 5;
        this.mp = 5;
        this.hp = 5;
        this.pointFree = (this.level - 1) * 5;
    }

}
