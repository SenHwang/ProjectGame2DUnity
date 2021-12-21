using Server.ObjectGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.FormChild
{
    public partial class ItemManager : Form
    {
        private List<Image> spritesIcons;
        private List<Image> spritesAnimation;

        private int indexItem = 0;
        private void btnExit_Click(object sender, EventArgs e)
        {
            foreach (Form i in Application.OpenForms)
            {
                if (i.Text == "ItemManager")
                {
                    i.Close();
                    break;
                }
            }
        }
        public ItemManager()
        {
            InitializeComponent();
            InitializeListSkills();
            LoadItem(0);
        }

        private void LoadItem(int index)
        {
            ItemRec itemGet = GAMEDATA.items[index];
            tbxName.Text = itemGet.name;
            tbxDesc.Text = itemGet.description;
            tbxSound.Text = itemGet.sound;
            int animGet;

            int.TryParse(itemGet.icon, out animGet);
            cbxIcon.SelectedIndex = animGet -1 ;

            int.TryParse(itemGet.Animation, out animGet);
            cbxAnim.SelectedIndex = animGet;

            cbxRarity.SelectedIndex = (int)itemGet.rarity - 1;

            cbxEquip.SelectedIndex = (int)itemGet.EquipSlot - 1;

            cbxStackable.Checked = itemGet.stackable;
            tbxPrice.Text = itemGet.price.ToString();
            tbxClassReq.Text = itemGet.classRequire.ToString();

            tbxMaximumInBag.Text = itemGet.maximumInBag == null ? "-1" : itemGet.maximumInBag.ToString();
            tbxLvReq.Text = itemGet.lvlRequire.ToString();
            tbxGenderReq.Text = itemGet.GenderErquire.ToString();
            tbxSkillReq.Text = itemGet.skillRequire.ToString();

            cbxEquipSkillBar.Checked = itemGet.EquipSkillBar;
            cbxIsReusable.Checked = itemGet.IsReusable;

            tbxAddHp.Text = itemGet.AddHp.ToString();
            tbxAddMp.Text = itemGet.AddMp.ToString();
            tbxAddExp.Text = itemGet.AddExp.ToString();

            tbxCastSkill.Text = itemGet.CastSkill.ToString();

            cbxStat.SelectedIndex = (int)itemGet.stat - 1;
            tbxStatAdd.Text = itemGet.AddStat.ToString();
        }

        private void InitializeListSkills()
        {
            //init list item
            listItems.Items.Clear();
            for (int i = 0; i < GAMEDATA.items.Length; i++)
            {
                ItemRec itemGet = GAMEDATA.items[i];

                string showing = i + ": " + (itemGet.name == null || itemGet.name == "" ? "Null" : itemGet.name);
                listItems.Items.Add(showing);
            }


            //init list icon
            cbxIcon.Items.Clear();
            spritesIcons = new List<Image>();
            string[] filePaths = Directory.GetFiles(GAMEDATA.ITEM_ICON_SOURCE_PATH, "*.png");
            for (int i = 0; i < filePaths.Length; i++)
            {
                Image image = Image.FromFile(GAMEDATA.ITEM_ICON_SOURCE_PATH + (i + 1) + ".png");
                spritesIcons.Add(image);

                string name = ((GAMEDATA.ITEM_ICON_SOURCE_PATH + (i + 1) + ".png").Replace(GAMEDATA.ITEM_ICON_SOURCE_PATH, "")).ToLower().Replace(".png", "");
                cbxIcon.Items.Add(name);
            }

            

            //init list Item Rarity
            cbxRarity.Items.Clear();
            var typeCount = Enum.GetNames(typeof(Rarity)).Length;
            for (int i = 0; i < typeCount; i++)
            {
                cbxRarity.Items.Add(Enum.GetName(typeof(Rarity), i + 1));
            }
            


            //init list animation
            cbxAnim.Items.Clear();
            cbxAnim.Items.Add("None");

            spritesAnimation = new List<Image>();            
            string[] filePathsAnim = Directory.GetFiles(GAMEDATA.ANIMATION_SOURCE_PATH, "*.png");
            for (int i = 0; i < filePathsAnim.Length; i++)
            {
                Image image = Image.FromFile(GAMEDATA.ANIMATION_SOURCE_PATH + (i + 1) + ".png");
                spritesAnimation.Add(image);

                string name = ((GAMEDATA.ANIMATION_SOURCE_PATH + (i + 1) + ".png").Replace(GAMEDATA.ANIMATION_SOURCE_PATH, "")).ToLower().Replace(".png", "");
                cbxAnim.Items.Add(name);
            }

            //init list Stat
            cbxStat.Items.Clear();
            var statCount = Enum.GetNames(typeof(ItemStat)).Length;
            for (int i = 0; i < statCount; i++)
            {
                cbxStat.Items.Add(Enum.GetName(typeof(ItemStat), i + 1));
            }


            cbxStat.SelectedIndex = 0;
            cbxIcon.SelectedIndex = 0;
            cbxAnim.SelectedIndex = 0;
            cbxRarity.SelectedIndex = 0;
        }

        private void OnSpriteChange(object sender, EventArgs e)
        {
            if (cbxIcon.SelectedIndex == -1)
                cbxIcon.SelectedIndex = 0;
            if (cbxIcon.SelectedIndex != -1)
                picIcon.Image = spritesIcons[cbxIcon.SelectedIndex];
            if (cbxAnim.SelectedIndex != -1)
                picAnimation.Image = cbxAnim.SelectedIndex == 0 ? null : spritesAnimation[cbxAnim.SelectedIndex - 1];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GAMEDATA.items[indexItem].name = tbxName.Text;
            GAMEDATA.items[indexItem].description = tbxDesc.Text;
            GAMEDATA.items[indexItem].sound = tbxSound.Text;
            GAMEDATA.items[indexItem].icon = cbxIcon.Text;
            GAMEDATA.items[indexItem].Animation = cbxAnim.Text;

            GAMEDATA.items[indexItem].rarity = (Rarity)Enum.ToObject(typeof(Rarity), cbxRarity.SelectedIndex + 1);
            GAMEDATA.items[indexItem].stackable = cbxStackable.Checked;

            GAMEDATA.items[indexItem].EquipSlot = (Equipment)Enum.ToObject(typeof(Equipment), cbxSlot.SelectedIndex + 1);

            int intTryParse;
            byte byteTryParse;
            int.TryParse(tbxPrice.Text, out intTryParse);
            GAMEDATA.items[indexItem].price = intTryParse;

            byte.TryParse(tbxClassReq.Text, out byteTryParse);
            GAMEDATA.items[indexItem].classRequire = byteTryParse;

            int.TryParse(tbxMaximumInBag.Text, out intTryParse);
            if (intTryParse == -1)
                GAMEDATA.items[indexItem].maximumInBag = null;
            else
                GAMEDATA.items[indexItem].maximumInBag = intTryParse;

            int.TryParse(tbxLvReq.Text, out intTryParse);
            GAMEDATA.items[indexItem].lvlRequire = intTryParse;

            byte.TryParse(tbxGenderReq.Text, out byteTryParse);
            GAMEDATA.items[indexItem].GenderErquire = byteTryParse;

            int.TryParse(tbxSkillReq.Text, out intTryParse);
            GAMEDATA.items[indexItem].skillRequire = intTryParse;

            GAMEDATA.items[indexItem].EquipSkillBar = cbxEquipSkillBar.Checked;

            GAMEDATA.items[indexItem].IsReusable = cbxIsReusable.Checked;

            int.TryParse(tbxAddHp.Text, out intTryParse);
            GAMEDATA.items[indexItem].AddHp = intTryParse;

            int.TryParse(tbxAddMp.Text, out intTryParse);
            GAMEDATA.items[indexItem].AddMp = intTryParse;

            int.TryParse(tbxAddExp.Text, out intTryParse);
            GAMEDATA.items[indexItem].AddExp = intTryParse;

            int.TryParse(tbxCastSkill.Text, out intTryParse);
            GAMEDATA.items[indexItem].CastSkill = intTryParse;

            int.TryParse(tbxCastSkill.Text, out intTryParse);
            GAMEDATA.items[indexItem].CastSkill = intTryParse;

            GAMEDATA.items[indexItem].stat = (ItemStat)Enum.ToObject(typeof(ItemStat), cbxStat.SelectedIndex + 1);
            
            int.TryParse(tbxStatAdd.Text, out intTryParse);
            GAMEDATA.items[indexItem].AddStat = intTryParse;

            //save into database
            Items.SaveData(indexItem, GAMEDATA.items[indexItem]);

            //load lại list item
            listItems.Items.Clear();
            for (int i = 0; i < GAMEDATA.items.Length; i++)
            {
                ItemRec itemGet = GAMEDATA.items[i];

                string showing = i + ": " + (itemGet.name == null || itemGet.name == "" ? "Null" : itemGet.name);
                listItems.Items.Add(showing);
            }

            for (int i = 0; i < Server.clients.Count; i++)
            {
                if (Server.clients[i].player == null) continue;

                List<ItemInventory> getList = Server.clients[i].player.items;
                if (getList == null) continue;

                if (getList.Exists(x => x.id == indexItem))
                {
                    for (int y = 0; y < getList.Count; y++)
                    {
                        if (getList[y].id != indexItem) continue;

                        ItemInventory editItem = new ItemInventory();
                        editItem.id = indexItem;
                        editItem.item = GAMEDATA.items[indexItem];
                        editItem.count = getList[y].count;
                        getList[y] = editItem;
                    }

                    Server.clients[i].player.items = getList;
                    ServerSend.SendInventoryPlayer(i, getList);
                }                
            }

        }

        private void listItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int focusedItem = listItems.FocusedItem.Index;
            indexItem = focusedItem;
            LoadItem(focusedItem);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ItemRec clear = new ItemRec();
            clear.name = null;
            GAMEDATA.items[indexItem] = clear;
            InitializeListSkills();

            for (int i = 0; i < Server.clients.Count; i++)
            {
                if (Server.clients[i].player == null) continue;

                
                Server.clients[i].player.InitInventory();
                ServerSend.SendInventoryPlayer(i, Server.clients[i].player.items);                
            }

            string[] filePathsAcc = Directory.GetFiles(GAMEDATA.APPLICATION_PATH + GAMEDATA.ACCOUNT_PATH, $"*{GAMEDATA.ACCOUNT_TYPE}");

            for (int i = 0; i < filePathsAcc.Length; i++)
            {
                if (File.Exists(filePathsAcc[i]))
                {
                    using (Stream stream = File.Open(filePathsAcc[i], FileMode.OpenOrCreate))
                    {
                        var binaryFormatter = new BinaryFormatter();
                        Player playerRead = (Player)binaryFormatter.Deserialize(stream);

                        List<ItemInventory> getList = new List<ItemInventory>();
                        if (playerRead.items == null) continue;

                        if (playerRead.items.Exists(x => x.id == indexItem))
                        {
                            for (int y = 0; y < playerRead.items.Count; y++)
                            {
                                if (playerRead.items[y].id != indexItem) 
                                    getList.Add(playerRead.items[y]);
                            }

                            playerRead.items = getList;

                            binaryFormatter.Serialize(stream, playerRead);
                            stream.Close();
                            continue;
                            
                        }
                    }
                    
                }
            }

            LoadItem(indexItem);
            btnSave_Click(sender, e);
        }
    }
}
