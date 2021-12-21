using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum SkillUse
{
    Damage = 1,
    Buff,
    DeBuff
}

[Serializable]
public enum SkillSpec
{
    Strength = 1,
    Agility,
    Intellect,
    Mp,
    Hp
}

[Serializable]
public enum PlayerUseable
{
    None = 1,// cá nhân
    Team,
    Guild,
    EveryoneInMap
}

[Serializable]
public enum SkillTypes
{
    Line = 1,
    Place,// nếu bán kính skill = 0 thì đây là skill đơn mục tiêu
    Shield,
    Teleport, // cần điểm đầu và điểm cuối, map
    JumpInto
}

[Serializable]
public struct SkillRec
{
    /// <summary>
    /// name: tên skill
    /// dùng với toàn bộ skill type
    /// </summary>
    public string name;

    /// <summary>
    /// Mô tả skill
    /// </summary>
    public string description;

    /// <summary>
    /// sound: sound Skill
    /// </summary>
    public string sound;

    /// <summary>
    /// spritePreview: sprite hiện trong túi đồ
    /// </summary>
    public string spritePreview;

    /// <summary>
    /// spriteSkill: sprite khi dùng skill
    /// dùng với toàn bộ skill type
    /// </summary>
    public string spriteSkill;

    /// <summary>
    /// spriteGetHit: sprite khi kill trúng mục tiêu
    /// dùng với toàn bộ skill type
    /// </summary>
    public string spriteGetHit;

    /// <summary>
    /// countdown: skill countdown 
    /// dùng với toàn bộ skill type
    /// </summary>
    public float countdown;

    /// <summary>
    /// mp cost cần mp dể dùng skill
    /// </summary>
    public int mpCost;

    /// <summary>
    /// lvlRequire cần để dùng skill
    /// </summary>
    public int lvlRequire;

    /// <summary>
    /// cần item nào để dùng skill
    /// </summary>
    public int itemRequire;


    /// <summary>
    /// classRequire cần để dùng skill
    /// </summary>
    public int classRequire;

    /// <summary>
    /// castTime thời gian niệm skill
    /// </summary>
    public float castTime;

    /// <summary>
    /// stunOwner stun player dùng skill
    /// </summary>
    public float stunOwner;

    /// <summary>
    /// stunTarget thời gian stun player dính skill
    /// </summary>
    public float stunTarget;

    /// <summary>
    /// castAnim animation niệm skill
    /// </summary>
    public string castAnim;

    /// <summary>
    /// positionUsing: vị trí dùng skill
    /// dùng với toàn bộ skill type
    /// </summary>
    public Vector2 positionUsing;

    /// <summary>
    /// range: phạm vi skill
    /// dùng với skill type [Face, RangeNonTarget, RangeTarget]
    /// </summary>
    public float range;

    /// <summary>
    /// expNextSkill đầy cây skill nân lên skill mới
    /// </summary>
    public int expNextSkill;
    public int newSkill;

    //tele
    /// <summary>
    /// Điểm dịch chuyển cuối
    /// </summary>
    public Vector2 positionEnd;

    /// <summary>
    /// Map id để tele
    /// </summary>
    public int idMapTarget;

    /// <summary>
    /// Thời gian skill tele tồn tại
    /// dùng với cá nhân(không cần time alive), team, Guild, everyone in map
    /// </summary>
    public PlayerUseable playerUseable;
    public float timeAliveSkill;


    //face
    /// <summary>
    /// dir: hướng skill
    /// </summary>
    public int dir;

    /// <summary>
    /// chiều ngang skill
    /// 1 là trên đường thẳng
    /// 3 là 3 ô bên cạnh
    /// vân vân
    /// </summary>
    public int len;

    /// <summary>
    /// Xuyên qua hay không
    /// </summary>
    public bool throwAble;

    /// <summary>
    /// cần mục tiêu hay không
    /// </summary>
    public bool targetAble;

    //dmg
    /// <summary>
    /// dmg thì lấy skillSpec cái nào thì dmg/buff/debuff theo cái đó
    /// với dmg thì có mấy class trên
    /// buff thì dùng theo PlayerUseable hoặc target
    /// debuff thì dùng với targetRange thôi
    /// </summary>
    public SkillUse skillUse;

    /// <summary>
    /// này là skill theo trường phái lào
    /// </summary>
    public SkillSpec skillSpec;
    public int numDmgSpec;

    public SkillTypes skillTypes;
}


public class Skill
{
    public static SkillRec skillCall;

    public static void CallSkill()
    {
        if(skillCall.skillTypes == SkillTypes.Line)
        {
            //ClientSend.RequestServerToCreateNewArrow(Client.instance.myId, PlayerControler.spriteStop, 10, 0.3f, GameManager.players[Client.instance.myId].GetComponent<Transform>().position);
        }else if(skillCall.skillTypes == SkillTypes.JumpInto)
        {

        }else if (skillCall.skillTypes == SkillTypes.Place)
        {

        }else if (skillCall.skillTypes == SkillTypes.Shield)
        {

        }else if (skillCall.skillTypes == SkillTypes.Teleport)
        {

        }

    }
}
