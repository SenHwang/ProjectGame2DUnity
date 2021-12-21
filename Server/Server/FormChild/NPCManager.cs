using Server.ObjectGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.FormChild
{
    public partial class NPCManager : Form
    {

        List<Image> sprites = new List<Image>();
        private int indexNpc = 0;
        private int indexStory = 0;
        private bool isAdd = false;

        public NPCManager()
        {
            InitializeComponent();
            InitListNpc();
            LoadNpc(indexNpc);
        }

        private void LoadNpc(int index)
        {
            NPC npcGet = GAMEDATA.npcs[index];
            tbxName.Text = npcGet.nameNPC;
            tbxSpeed.Text = npcGet.speed.ToString();
            cbxMovable.Checked = npcGet.movable;

            cbxTypes.SelectedIndex = ((int)npcGet.type) - 1;
            
            cbxSprite.SelectedIndex =
            npcGet.sprite == null || npcGet.sprite.ToString() == "" ?
            0 : int.Parse(npcGet.sprite.ToString());

            tbxHp.Text = npcGet.typeMonster.hp.ToString();
            tbxDamage.Text = npcGet.typeMonster.damage.ToString();
            tbxExp.Text = npcGet.typeMonster.expGot.ToString();
            tbxLvl.Text = npcGet.typeMonster.level.ToString();

            grbAddStory.Location = new Point(549, 122);

            listStory.Items.Clear();
            for (int i = 0; i < GAMEDATA.npcs[index].story.Count; i++)
            {
                Story storyGet = GAMEDATA.npcs[index].story[i];
                if (npcGet == null) npcGet = GAMEDATA.npcs[i] = new NPC();

                string showing =storyGet.title == null ? "Null" : storyGet.title;
                listStory.Items.Add(showing);
            }

        }

        private void InitListNpc()
        {
            listNPC.Items.Clear();
            for (int i = 0; i < GAMEDATA.npcs.Length; i++)
            {
                NPC npcGet = GAMEDATA.npcs[i];
                if (npcGet == null) npcGet = GAMEDATA.npcs[i] = new NPC();

                string showing = i + ": " + (npcGet.nameNPC == null ? "Null" : npcGet.nameNPC + $" [{Enum.GetName(typeof(NPCTypes), npcGet.type)}]");
                listNPC.Items.Add(showing);
            }

            cbxTypes.Items.Clear();
            var typeCount = Enum.GetNames(typeof(NPCTypes)).Length;
            for(int i = 0; i < typeCount; i++)
            {
                cbxTypes.Items.Add(Enum.GetName(typeof(NPCTypes), i+1));
            }
            cbxTypes.SelectedIndex = 0;



            cbxSprite.Items.Clear();
            sprites = new List<Image>();
            string[] filePaths = Directory.GetFiles(GAMEDATA.NPC_SOURCE_PATH, "*.png");
            
            cbxSprite.Items.Add("None");
            for (int i = 0; i < filePaths.Length; i++)
            {
                Image image = Image.FromFile(GAMEDATA.NPC_SOURCE_PATH + (i+1)+".png");
                sprites.Add(image);

                string name = ((GAMEDATA.NPC_SOURCE_PATH + (i + 1) + ".png").Replace(GAMEDATA.NPC_SOURCE_PATH, "")).ToLower().Replace(".png", "");
                cbxSprite.Items.Add(name);
            }
            cbxSprite.SelectedIndex = 0;


            cbxTypeStory.Items.Clear();
            var typeStory = Enum.GetNames(typeof(StoryType)).Length;
            for (int i = 0; i < typeStory; i++)
            {
                cbxTypeStory.Items.Add(Enum.GetName(typeof(StoryType), i + 1));
            }
            cbxTypeStory.SelectedIndex = 0;
        }

        private void TypeChange(object sender, EventArgs e)
        {
            if(Enum.GetName(typeof(NPCTypes), cbxTypes.SelectedIndex + 1) == "NPC")
            {
                grbNpc.Visible = true;
                grbMonster.Visible = false;
            }else if(Enum.GetName(typeof(NPCTypes), cbxTypes.SelectedIndex + 1) == "Monster")
            {
                grbNpc.Visible = false;
                grbMonster.Visible = true;
            }
        }

        private void TypeStoryChange(object sender, EventArgs e)
        {
            if (Enum.GetName(typeof(NPCTypes), cbxTypes.SelectedIndex + 1) == "Message")
            {
                //do sơm think
            }
            else if (Enum.GetName(typeof(NPCTypes), cbxTypes.SelectedIndex + 1) == "Shop")
            {
                //do sơm think
            }
        }

        private void SpriteChange(object sender, EventArgs e)
        {
            if(cbxSprite.SelectedIndex == 0)
            {
                rawSprite.Image = null;
                return;
            }
            rawSprite.Image = sprites[cbxSprite.SelectedIndex - 1];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GAMEDATA.npcs[indexNpc].nameNPC = tbxName.Text;
            GAMEDATA.npcs[indexNpc].description = tbxDesc.Text;
            float speedGet;
            float.TryParse(tbxSpeed.Text, out speedGet);
            GAMEDATA.npcs[indexNpc].speed = speedGet;

            GAMEDATA.npcs[indexNpc].type = cbxTypes.SelectedIndex == 0 ? NPCTypes.NPC : NPCTypes.Monster;
            GAMEDATA.npcs[indexNpc].movable = cbxMovable.Checked;
            string spriteName = "";
            if(cbxSprite.SelectedIndex != 0)
            {
                spriteName = cbxSprite.Items[cbxSprite.SelectedIndex].ToString();
            }
            GAMEDATA.npcs[indexNpc].sprite = spriteName;
            
            if(GAMEDATA.npcs[indexNpc].type == NPCTypes.NPC)
            {
                //GAMEDATA.npcs[indexNpc].story = new List<Story>();
            }else if(GAMEDATA.npcs[indexNpc].type == NPCTypes.Monster)
            {
                int cache;
                int.TryParse(tbxHp.Text, out cache);
                GAMEDATA.npcs[indexNpc].typeMonster.hp = cache;

                int.TryParse(tbxDamage.Text, out cache);
                GAMEDATA.npcs[indexNpc].typeMonster.damage = cache;

                int.TryParse(tbxExp.Text, out cache);
                GAMEDATA.npcs[indexNpc].typeMonster.expGot = cache;

                int.TryParse(tbxLvl.Text, out cache);
                GAMEDATA.npcs[indexNpc].typeMonster.level = cache;

                GAMEDATA.npcs[indexNpc].typeMonster.Items = new List<ItemMonsterDrop>();
                GAMEDATA.npcs[indexNpc].typeMonster.Skills = new List<SkillMonster>();
            }

            NPC.SaveData(indexNpc, GAMEDATA.npcs[indexNpc]);

            for (int i = 0; i < Server.MaxPlayers; i++)
            {
                ServerSend.SendNPCListToClient(i, GAMEDATA.npcs);
            }
            
            InitListNpc();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            foreach (Form i in Application.OpenForms)
            {
                if (i.Text == "NPCManager")
                {
                    i.Close();
                    break;
                }
            }
        }

        private void listNPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            int focusedItem = listNPC.FocusedItem.Index;
            indexNpc = focusedItem;
            LoadNpc(focusedItem);
        }

        private void btnStoryAdd_Click(object sender, EventArgs e)
        {
            isAdd = true;
            grbAddStory.Location = new Point(156, 146);
            indexStory = GAMEDATA.npcs[indexNpc].story.Count;
            CleanStoryAdd();
        }

        private void btnASSave_Click(object sender, EventArgs e)
        {
            string story = tbxStory.Text;
            string[] listStory = Regex.Split(story, "&;");
            for (int i = 0; i < listStory.Length; i++)
                if (listStory[i].IndexOf("\n") == 0 && listStory[i].Length > 1)
                    listStory[i] = listStory[i].Substring(1);

            if (cbxTypeStory.SelectedIndex == 0)
            {
                List<string> storySet = new List<string>();
                foreach (string line in listStory)
                {
                    if (line.Length != 0 && line.Replace("\n", "").Length != 0)
                        storySet.Add(line);
                }

                if (storySet.Count == 0) return;
                Story storyNPC = new Story();
                storyNPC.title = tbxTitleStory.Text.Length == 0 ? "New Story" : tbxTitleStory.Text;
                storyNPC.type = StoryType.Message;
                storyNPC.text = storySet;

                if (isAdd == true)
                    GAMEDATA.npcs[indexNpc].story.Add(storyNPC);
                else
                    GAMEDATA.npcs[indexNpc].story[indexStory] = storyNPC;
            }
            else if(cbxTypeStory.SelectedIndex == 1)
            {                
                List<ItemRec> itemGet = new List<ItemRec>();
                foreach (string line in listStory)
                {
                    string item = line.Replace("\n", "");
                    if (item == "") continue;
                    int.TryParse(item, out int itemId);
                    itemGet.Add(GAMEDATA.items[itemId]);
                }

                Story storyNPC = new Story();
                storyNPC.title = tbxTitleStory.Text.Length == 0 ? "New Shop" : tbxTitleStory.Text;
                storyNPC.type = StoryType.Shop;
                storyNPC.item = itemGet;

                if (isAdd == true)
                    GAMEDATA.npcs[indexNpc].story.Add(storyNPC);
                else
                    GAMEDATA.npcs[indexNpc].story[indexStory] = storyNPC;
            }

            isAdd = false;
            LoadNpc(indexNpc);
            btnASExit_Click(sender, e);            
        }

        private void btnASExit_Click(object sender, EventArgs e)
        {
            grbAddStory.Location = new Point(549, 122);
        }

        private void CleanStoryAdd()
        {
            tbxTitleStory.Text = "New Story";
            cbxTypeStory.SelectedIndex = 0;
            tbxStory.Text = "";
        }

        private void LoadStoryAdd(int index)
        {
            Story story = GAMEDATA.npcs[indexNpc].story[index];
            tbxTitleStory.Text = story.title;
            cbxTypeStory.SelectedIndex = (int)story.type-1;

            string message = "";
            if (story.type == StoryType.Message)
            {
                foreach (string line in story.text)
                    message += line + "&;\n";
            }else if(story.type == StoryType.Shop)
            {
                foreach (ItemRec item in story.item)
                {
                    int i = GAMEDATA.items.ToList().FindIndex(x=> x.name == item.name);
                    message += i + "&;\n";
                } 
            }
            tbxStory.Text = message;
        }

        private void listStory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int focusedItem = listStory.FocusedItem.Index;
            indexStory = focusedItem;
            grbAddStory.Location = new Point(156, 146);
            LoadStoryAdd(focusedItem);
        }
        private void btnRmStory_Click(object sender, EventArgs e)
        {
            GAMEDATA.npcs[indexNpc].story.RemoveAt(indexStory);
            btnASExit_Click(sender, e);
        }
    }
}
