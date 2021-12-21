using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NPC
{

    [Serializable]
    public enum NPCTypes
    {
        NPC = 1,
        Monster
    }

    [Serializable]
    public enum StoryType
    {
        Message = 1,
        Shop
    }

    [Serializable]
    public struct SkillMonster
    {
        public string name;
        public int IndexOnList;
        public float countdown;
    }

    [Serializable]
    public struct ItemMonsterDrop
    {
        public string name;
        public int IndexOnList;
        public int length;
        public float rate;
    }

    [Serializable]
    public struct TypeMonster
    {
        public int expGot;
        public int damage;
        public int level;
        public int hp;
        public List<ItemMonsterDrop> Items;
        public List<SkillMonster> Skills;
    }

    [Serializable]
    public struct Story
    {
        //xây dựng 1 stoty có nhiều lời mỗi lời là 1 text trong list 
        //bấm chuột qua mỗi text
        public string title;
        public StoryType type;
        public List<string> text;//text này của npc story
        public List<ItemRec> item;// item này là item bán trong npc
    }


    //tên
    public string nameNPC;

    //mô tả
    public string description;

    //position
    public Vector3 position;

    //position
    public Vector3 positionUpdate;

    public int targeting = -1;

    public float speed;
    public bool movable;
    public string sprite; // ảnh
    public int hpLeft;
    public List<Story> story; //cái này là npc nói chuyện

    public NPCTypes type = NPCTypes.NPC;

    public TypeMonster typeMonster = new TypeMonster();

    public NPC()
    {

    }

    public NPC(string name, Vector3 position, NPCTypes type, float speed, string sprite, List<Story> story, TypeMonster? typeMonster)
    {
        this.nameNPC = name;
        this.position = position;
        this.type = type;
        this.speed = speed;
        this.sprite = sprite;

        this.story = story;
        this.positionUpdate = this.position;

        if (type == NPCTypes.Monster)
        {
            this.typeMonster = (TypeMonster)typeMonster;
            this.hpLeft = ((TypeMonster)typeMonster).hp;
        }
    }


    public void UpdatePosition(Vector3 position)
    {
        this.positionUpdate = position;
    }


    public void GetGamage(int dmg)
    {
        if (this.type == NPCTypes.NPC) return;

        //send pakage quái này về server;
    }

    public void OnGetHandHit(int dmg)
    {
        //nếu mà đánh tay vào con quái thì set dmg con quái
        if (this.type == NPCTypes.Monster)
        {
            GetGamage(dmg);
            return;
        }else if (this.type == NPCTypes.NPC)
        {            
            StoryGameFunc.SetStory(this.story, this.nameNPC, this.description);            
        }



        // còn không thì show story
        //if (this.story.Count == 0) return;

        //TODO Show story
    }



}
