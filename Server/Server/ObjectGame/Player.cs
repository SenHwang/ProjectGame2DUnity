using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Server.Data;
using Server.GameData;
using Server.ObjectGame;

namespace Server
{
    [Serializable]
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;
    }

    [Serializable]
    public class Vector2
    {
        public float x;
        public float y;

        public Vector2()
        {

        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [Serializable]
    public struct Quaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;
    }

    [Serializable]
    class Player
    {   
        public int id;
        public string username;
       
        public Vector3 position;
        public Quaternion rotation;
        public int spriteStop = 0;
        public int speedAnim = 0;

        public bool IsPunching;
        public bool IsDead;
        public bool IsKick;

        public int mapID;
        
        //inventory n equipment
        public string[] Equipment;//7 slot equip

        public List<SkillInventory> skillInven;
        public List<ItemInventory> items;

        public Stats stat = new Stats();

        public PartyRec party = new PartyRec();

        //role
        public Role role;

        //speed movement
        public float speedWalk = 10f;
        

        public Player(int _id, string _username, Vector3 _spawnPosition)
        {
            id = _id; 
            username = _username;
            position = _spawnPosition;
            this.stat.healthLeft = this.stat.health = 100;
            this.stat.manaLeft = this.stat.mana = 50;
            mapID = 0;
            Quaternion q;
            q.x = 0; q.y = 0;q.z = 0;q.w = 1;
            rotation = q;
            spriteStop = 0;
            speedAnim = 0;
            Equipment = new string[7];
        }

        public void Update()
        {
            //UpdateSkills();
            this.speedWalk -= 1f;
            if (this.speedWalk <= 0)
            {
                this.speedWalk = 10f;
            }

            ServerSend.UpdateStatPlayer(this);
        }

        public void SetInput(Vector3 _position, int _speedAnim, int _spriteStop, bool IsPunching, bool IsDead, bool IsKick)//
        {
            this.position = _position;
            this.speedAnim = _speedAnim;
            this.spriteStop = _spriteStop;

            this.IsPunching = IsPunching;
            this.IsDead = IsDead;
            this.IsKick = IsKick;

            ServerSend.PlayerPosition(this);
        }

        public void SetEquipment(string[] _input)
        {
            Equipment = _input;
            ServerSend.PlayerEquipment(this);
        }

        /// <summary>
        /// call dmg qua hàm với int dmg
        /// </summary>
        /// <param name="damage"></param>
        public void PlayerTakeDamage(int damage)
        {
            this.stat.healthLeft -= damage;
            if (Party.IsMemberHasParty(this.id) != -1)
            {
                for(int i = 0;i< GAMEDATA.Partys[Party.IsMemberHasParty(this.id)].Member.Count; i++)
                {
                    if (GAMEDATA.Partys[Party.IsMemberHasParty(this.id)].Member[i].id != this.id) continue;
                    MiniPlayer player = GAMEDATA.Partys[Party.IsMemberHasParty(this.id)].Member[i];
                    player.hpLeft = this.stat.healthLeft;
                    GAMEDATA.Partys[Party.IsMemberHasParty(this.id)].Member[i] = player;
                    break;
                }

                for (int i = 0; i < GAMEDATA.Partys[Party.IsMemberHasParty(this.id)].Member.Count; i++)
                {
                    ServerSend.SendListPlayerToClient(GAMEDATA.Partys[Party.IsMemberHasParty(this.id)].Member[i].id, GAMEDATA.Partys[Party.IsMemberHasParty(this.id)]);
                }
            }
            ServerSend.SpawnBlood(this.mapID,this.position);
            ServerSend.SpawnText(this.mapID, this.position, "-"+damage.ToString());
            if (this.stat.healthLeft <= 0)
                OnPlayerDead();

            ServerSend.UpdateStatPlayer(this);
            ServerSend.UpdateStatPlayerInMap(this.mapID);
        }

        public void OnPlayerDead()
        {
            this.stat.healthLeft = this.stat.health;
            this.stat.manaLeft = this.stat.mana;
            mapID = GAMEDATA.MAP_SPAWN_ON_DEAD;
            //chết về vị trí 0,0,0 tại map OnDead của server
            position = new Vector3();
            ServerSend.RefreshMap(id, mapID, position);
            ServerSend.PlayerPosition(this);
        }

        public void InitInventory()
        {
            List<ItemInventory> initListItem = new List<ItemInventory>();
            for(int i =0;i< items.Count; i++)
            {
                if (GAMEDATA.items[items[i].id].name == null || GAMEDATA.items[items[i].id].name == "") continue;

                ItemInventory itemSet = new ItemInventory();
                itemSet.id = items[i].id;
                itemSet.count = items[i].count;
                itemSet.item = GAMEDATA.items[items[i].id];
                initListItem.Add(itemSet);
            }
            items = initListItem;
        }

        public void InitInventorySkill()
        {
            List<SkillInventory> initListItem = new List<SkillInventory>();
            if(skillInven == null)
            {
                skillInven = initListItem;
                return;
            } 
            for (int i = 0; i < skillInven.Count; i++)
            {
                if (GAMEDATA.skills[skillInven[i].id].name == null || GAMEDATA.skills[skillInven[i].id].name == "") continue;

                SkillInventory itemSet = new SkillInventory();
                itemSet.id = skillInven[i].id;
                itemSet.item = GAMEDATA.skills[skillInven[i].id];
                initListItem.Add(itemSet);
            }
            skillInven = initListItem;
        }
        public void AddItemInventory(int idItem, int count)
        {
            if (GAMEDATA.items[idItem].name == null || GAMEDATA.items[idItem].name == "")
            {
                ChatSystem.MessageCallbackToClient(id, $"Item id {idItem} không tồn tại!");
                return;
            }

            if (items == null)
            {
                items = new List<ItemInventory>();
                ItemInventory money = new ItemInventory();
                money.count = 0;
                money.item = GAMEDATA.items[0];
                items.Add(money);
                ServerSend.SendInventoryPlayer(id, items);
            }

            ItemInventory inventory;
            inventory.id = idItem;
            inventory.item = GAMEDATA.items[idItem];
            inventory.count = count;

            //nếu item k có stackable(cộng dồn) thì add luôn
            if (inventory.item.stackable != true)
            {
                if (items.Count == GAMEDATA.MAX_INVENTORY + 1)
                {
                    ChatSystem.MessageCallbackToClient(id, $"Đầy túi đồ!");
                    return;
                }

                if(items.Count + count <= GAMEDATA.MAX_INVENTORY + 1)
                {
                    for(int i=0;i< count; i++)
                    {
                        inventory.count = 1;
                        items.Add(inventory);
                    }
                    ChatSystem.MessageCallbackToClient(id, $"Đã nhận {count} {inventory.item.name}!");
                }else if(items.Count + count > GAMEDATA.MAX_INVENTORY + 1)
                {
                    int stackLeft = (GAMEDATA.MAX_INVENTORY + 1) - items.Count;
                    for (int i = 0; i < stackLeft; i++)
                    {
                        inventory.count = 1;
                        items.Add(inventory);
                    }
                    ChatSystem.MessageCallbackToClient(id, $"Đã nhận {stackLeft} {inventory.item.name}!");
                    ChatSystem.MessageCallbackToClient(id, $"Túi đồ đầy!");
                }

                ServerSend.SendInventoryPlayer(id, items);
                return;
            }

            //nếu item có stackable thì xem trong túi có item không
            // nếu có thì count++, không thì add
            if (items.Exists(x => x.item.name == inventory.item.name))
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (items.ToArray()[i].item.name != inventory.item.name) continue;
                    ItemInventory addCount = items[i];
                    addCount.count += inventory.count;
                    items[i] = addCount;

                    if(inventory.count != 0)
                        ChatSystem.MessageCallbackToClient(id, $"Đã nhận {inventory.count} { inventory.item.name}!");

                    ServerSend.SendInventoryPlayer(id, items);
                    return;
                }
            }
            else
            {
                if (items.Count == GAMEDATA.MAX_INVENTORY + 1)
                {
                    ChatSystem.MessageCallbackToClient(id, $"Đầy túi đồ!");
                    return;
                }
                items.Add(inventory);
                ChatSystem.MessageCallbackToClient(id, $"Đã nhận {inventory.item.name}!");

                ServerSend.SendInventoryPlayer(id, items);
            }

        }

        public void AddSkillInventory(int idSkill)
        {
            if (GAMEDATA.skills[idSkill].name == null || GAMEDATA.skills[idSkill].name == "")
            {
                ChatSystem.MessageCallbackToClient(id, $"Skill id {idSkill} không tồn tại!");
                return;
            }
            if (skillInven == null)
                skillInven = new List<SkillInventory>();


            SkillInventory inventory;
            inventory.item = GAMEDATA.skills[idSkill];
            inventory.id = idSkill;
            inventory.timeStart = 0;
            inventory.timeLeft = 0;

            if (skillInven.Count == GAMEDATA.MAX_INVENTORY)
            {
                ChatSystem.MessageCallbackToClient(id, $"Skill đã học quá nhiều!");
                return;
            }

            //nếu có item này thì báo là skill đã đc học
            if (skillInven.Exists(x => x.id == inventory.id))
            {
                ChatSystem.MessageCallbackToClient(id, $"{inventory.item.name} đã được học!");
                return;
            }
            else
            {
                ChatSystem.MessageCallbackToClient(id, $"{inventory.item.name} học thành công!");
                skillInven.Add(inventory);
                //send pakage to client;
                ServerSend.SendInventorySkillPlayer(id, skillInven);
            }

        }
    }


    [Serializable]
    class Stats
    {
        #region STAT ĐỘNG
        public int level = 1;
        public int exp = 100; //TODO: này sau có file exp list thì add vào này
        public int expOwn;
        public int health = 100;
        public int mana = 50;
        public int healthLeft = 100;
        public int manaLeft = 50;

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

        public void GainExp(int expGot)
        {
            this.expOwn += expGot;
            if(this.expOwn >= exp)
            {
                LevelUp(1);
                this.expOwn = (this.expOwn - this.exp) > 0 ? this.expOwn - this.exp : 0;
            }
        }

        public void LevelUp(int levelAdd)
        {
            this.level+= levelAdd;
            this.pointFree += 5*levelAdd;
        }

        public void UpdateStat(int srtength,int agility, int intellect, int mp, int hp)
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
}
