using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Server.ObjectGame
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


    [Serializable]
    public class NPC
    {
        //tên
        public string nameNPC;

        //mô ta
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

            if(type == NPCTypes.Monster)
            {
                this.typeMonster = (TypeMonster)typeMonster;
                this.hpLeft = ((TypeMonster)typeMonster).hp;
            }
        }

        public static void InitializeNPC()
        {
            TaskQueue.listMessage.Add("InitializeNPC()");
            for (int i = 0; i < GAMEDATA.MAX_NPC; i++)
            {
                string path = $"{GAMEDATA.NPC_DATA_PATH}{i}.json";
                if (!File.Exists(Application.StartupPath + path))
                    SaveData(i, new NPC());
                else
                    ReadData(i);
            }
        }

        private static void ReadData(int i)
        {
            string path = $"{GAMEDATA.NPC_DATA_PATH}{i}.json";
            var dataRead = File.ReadAllText(Application.StartupPath + path);

            var serializer = new JavaScriptSerializer();
            NPC dataResult = serializer.Deserialize<NPC>(dataRead);
            GAMEDATA.npcs[i] = dataResult;
        }

        public static void SaveData(int i, NPC npc)
        {
            string path = $"{GAMEDATA.NPC_DATA_PATH}{i}.json";
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(npc);
            
            File.WriteAllText(Application.StartupPath + path, serializedResult);
        }


        public void UpdatePosition(Vector3 position)
        {
            this.positionUpdate = position;
        }

        public void Target(int player)
        {
            if (this.type == NPCTypes.NPC) return;
            this.targeting = player;
        }

        public void GetGamage(int dmg)
        {
            if (this.type == NPCTypes.NPC) return;

            this.hpLeft -= dmg;
            if(this.hpLeft <= 0)
            {
                this.hpLeft = this.typeMonster.hp;
                this.positionUpdate = this.position;
            }
        }


    }
}
