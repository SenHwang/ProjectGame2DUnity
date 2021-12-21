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
    public partial class SkillManager : Form
    {
        private List<Image> spritesIcons = new List<Image>();

        private List<Image> spritesAnim = new List<Image>();

        private int SpecGet = -1;
        private int indexSkill = 0;

        public SkillManager()
        {
            InitializeComponent();
            InittializeListSkills();
            LoadSkill(indexSkill);
        }

        private void InittializeListSkills()
        {

            //init list skill
            listSkills.Items.Clear();
            for (int i = 0; i < GAMEDATA.skills.Length; i++)
            {
                SkillRec skillGet = GAMEDATA.skills[i];

                string showing = i + ": " + (skillGet.name == null || skillGet.name == "" ? "Null" : skillGet.name);
                listSkills.Items.Add(showing);
            }


            //init list Skill type
            cbxTypes.Items.Clear();
            var typeCount = Enum.GetNames(typeof(SkillTypes)).Length;
            for (int i = 0; i < typeCount; i++)
            {
                cbxTypes.Items.Add(Enum.GetName(typeof(SkillTypes), i + 1));
            }
            cbxTypes.SelectedIndex = 0;

            //init list icon
            cbxIcon.Items.Clear();
            spritesIcons = new List<Image>();
            string[] filePaths = Directory.GetFiles(GAMEDATA.SKILL_ICON_SOURCE_PATH, "*.png");
            for (int i = 0; i < filePaths.Length; i++)
            {
                Image image = Image.FromFile(GAMEDATA.SKILL_ICON_SOURCE_PATH + (i+1)+ ".png");
                spritesIcons.Add(image);

                string name = ((GAMEDATA.SKILL_ICON_SOURCE_PATH + (i + 1) + ".png").Replace(GAMEDATA.SKILL_ICON_SOURCE_PATH, "")).ToLower().Replace(".png", "");
                cbxIcon.Items.Add(name);
            }           
            

            //init AnimSkill

            cbxAnimS.Items.Clear();
            cbxAnimC.Items.Clear();
            cbxAnimLine.Items.Clear();
            cbxAnimJump.Items.Clear();
            cbxAnimPlace.Items.Clear();
            spritesAnim = new List<Image>();

            cbxAnimS.Items.Add("None");
            cbxAnimC.Items.Add("None");
            cbxAnimLine.Items.Add("None");
            cbxAnimJump.Items.Add("None");
            cbxAnimPlace.Items.Add("None");

            string[] animGet = Directory.GetFiles(GAMEDATA.ANIMATION_SOURCE_PATH, "*.png");
            for (int i = 0; i < animGet.Length; i++)
            {
                Image image = Image.FromFile(GAMEDATA.ANIMATION_SOURCE_PATH + (i + 1) + ".png");
                spritesAnim.Add(image);

                string name = ((GAMEDATA.ANIMATION_SOURCE_PATH + (i + 1) + ".png").Replace(GAMEDATA.ANIMATION_SOURCE_PATH, "")).ToLower().Replace(".png", "");
                cbxAnimS.Items.Add(name);
                cbxAnimC.Items.Add(name);
                cbxAnimLine.Items.Add(name);
                cbxAnimJump.Items.Add(name);
                cbxAnimPlace.Items.Add(name);
            }

            cbxAnimPlace.SelectedIndex =
            cbxAnimJump.SelectedIndex =
            cbxAnimLine.SelectedIndex =
            cbxAnimC.SelectedIndex = 
            cbxAnimS.SelectedIndex = 
            cbxIcon.SelectedIndex = 0;



            //init list Skill type
            cbxDmgType.Items.Clear();
            cbxSpecJump.Items.Clear();
            cbxSpecPlace.Items.Clear();
            var specCount = Enum.GetNames(typeof(SkillSpec)).Length;
            for (int i = 0; i < specCount; i++)
            {
                cbxSpecJump.Items.Add(Enum.GetName(typeof(SkillSpec), i + 1));
                cbxDmgType.Items.Add(Enum.GetName(typeof(SkillSpec), i + 1));
                cbxSpecPlace.Items.Add(Enum.GetName(typeof(SkillSpec), i + 1));
            }
            cbxSpecPlace.SelectedIndex =
            cbxDmgType.SelectedIndex =
            cbxSpecJump.SelectedIndex = 0;


            cbxSkillUsePlace.Items.Clear();
            cbxSkillUseJump.Items.Clear();
            var skillUseCount = Enum.GetNames(typeof(SkillUse)).Length;
            for (int i = 0; i < skillUseCount; i++)
            {
                cbxSkillUsePlace.Items.Add(Enum.GetName(typeof(SkillUse), i + 1));
                cbxSkillUseJump.Items.Add(Enum.GetName(typeof(SkillUse), i + 1));
            }
            cbxSkillUsePlace.SelectedIndex =
            cbxSkillUseJump.SelectedIndex = 0;


            cbxPlayerUseable.Items.Clear();
            var playerUseableCount = Enum.GetNames(typeof(PlayerUseable)).Length;
            for (int i = 0; i < playerUseableCount; i++)
            {
                cbxPlayerUseable.Items.Add(Enum.GetName(typeof(PlayerUseable), i + 1));
            }
            cbxPlayerUseable.SelectedIndex = 0;
        }

        private void IconChange(object sender, EventArgs e)
        {
            picIcon.Image = null;
            if (cbxIcon.SelectedIndex != -1)
                picIcon.Image = spritesIcons[cbxIcon.SelectedIndex];
            if (cbxAnimS.SelectedIndex != -1)
                picSkill.Image = cbxAnimS.SelectedIndex == 0 ? null : spritesAnim[cbxAnimS.SelectedIndex - 1];
            if (cbxAnimC.SelectedIndex != -1)
                picCast.Image = cbxAnimC.SelectedIndex == 0 ? null : spritesAnim[cbxAnimC.SelectedIndex - 1];

            //grbox
            if (cbxAnimLine.SelectedIndex != -1)
                picAnimLine.Image = cbxAnimLine.SelectedIndex == 0 ? null : spritesAnim[cbxAnimLine.SelectedIndex -1];
            if (cbxAnimJump.SelectedIndex != -1)
                picAnimJump.Image = cbxAnimJump.SelectedIndex == 0 ? null : spritesAnim[cbxAnimJump.SelectedIndex - 1];
            if (cbxAnimPlace.SelectedIndex != -1)
                picAnimPlace.Image = cbxAnimPlace.SelectedIndex == 0 ? null : spritesAnim[cbxAnimPlace.SelectedIndex - 1];

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            foreach (Form i in Application.OpenForms)
            {
                if (i.Text == "SkillManager")
                {
                    i.Close();
                    break;
                }
            }
        }

        private void OnSkillTypeChange(object sender, EventArgs e)
        {
            // không select thì đưa grbox về 763, 15            
            grbLine.Location =           
            grbPlace.Location = 
            grbShield.Location = 
            grbTeleport.Location = 
            grbJumpInto.Location = new Point(763, 15);

            switch (cbxTypes.SelectedIndex)
            {
                case 0:
                    grbLine.Location = new Point(184, 238);
                    break;
                case 1:
                    grbPlace.Location = new Point(184, 238);
                    break;
                case 2:
                    grbShield.Location = new Point(184, 238);
                    break;
                case 3:
                    grbTeleport.Location = new Point(184, 238);
                    break;
                case 4:
                    grbJumpInto.Location = new Point(184, 238);
                    break;
            }
        }

        private void ChangeSpec(object sender, EventArgs e)
        {
            if (cbxSpecPlace.SelectedIndex != -1)
                SpecGet = cbxSpecPlace.SelectedIndex;
            if (cbxDmgType.SelectedIndex != -1)
                SpecGet = cbxDmgType.SelectedIndex;
            if (cbxSpecJump.SelectedIndex != -1)
                SpecGet = cbxSpecJump.SelectedIndex;
        }

     

        private void listSkills_SelectedIndexChanged(object sender, EventArgs e)
        {
            int focusedItem = listSkills.FocusedItem.Index;
            indexSkill = focusedItem;
            LoadSkill(focusedItem);
        }

        private void LoadSkill(int index)
        {
            SkillRec skillGet = GAMEDATA.skills[index];
            tbxName.Text = skillGet.name;
            tbxDesc.Text = skillGet.description;
            tbxSound.Text = skillGet.sound;
            int animGet;

            int.TryParse(skillGet.spritePreview, out animGet);
            cbxIcon.SelectedIndex = animGet-1;

            int.TryParse(skillGet.spriteSkill, out animGet);
            cbxAnimS.SelectedIndex = animGet;

            int.TryParse(skillGet.castAnim, out animGet);
            cbxAnimC.SelectedIndex = animGet;

            tbxCD.Text = skillGet.countdown.ToString();
            tbxLvReq.Text = skillGet.lvlRequire.ToString();
            tbxMpCost.Text = skillGet.mpCost.ToString();
            tbxClassReq.Text = skillGet.classRequire.ToString();
            tbxCastTime.Text = skillGet.castTime.ToString();
            tbxStunOwn.Text = skillGet.stunOwner.ToString();
            tbxExp.Text = skillGet.expNextSkill.ToString();
            tbxNextSkill.Text = skillGet.newSkill.ToString();

            cbxTypes.SelectedIndex = (int)skillGet.skillTypes -1;


            tbxRangeJump.Text = tbxRangePlace.Text = tbxRangeLine.Text = skillGet.range.ToString();
            tbxLenJump.Text = tbxLenLine.Text = tbxLenPlace.Text = skillGet.len.ToString();
            
            int.TryParse(skillGet.spriteGetHit, out animGet);
            cbxAnimLine.SelectedIndex =
            cbxAnimPlace.SelectedIndex =
            cbxAnimJump.SelectedIndex = animGet;

            tbxStunTLine.Text =
            tbxStunTPlace.Text =
            tbxStunTJump.Text = skillGet.stunTarget.ToString();

            chbThrow.Checked = skillGet.throwAble;

            cbxTargetPlace.Checked = cbxTargetJump.Checked = skillGet.targetAble;

            cbxSkillUseJump.SelectedIndex =
            cbxSkillUsePlace.SelectedIndex = (int)skillGet.skillUse - 1;

            cbxSpecJump.SelectedIndex =
            cbxSpecPlace.SelectedIndex =
            cbxDmgType.SelectedIndex = (int)skillGet.skillSpec - 1;

            tbxDmgJump.Text = tbxDmgLine.Text = tbxDmgPlace.Text = skillGet.numDmgSpec.ToString();

            //tele
            tbxX.Text = skillGet.positionEnd == null ? "0" : skillGet.positionEnd.x.ToString();
            tbxY.Text = skillGet.positionEnd == null ? "0" : skillGet.positionEnd.y.ToString();
            tbxMapID.Text = skillGet.idMapTarget.ToString();
            tbxTimeAlive.Text = skillGet.timeAliveSkill.ToString();
            cbxPlayerUseable.SelectedIndex = (int)skillGet.playerUseable - 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GAMEDATA.skills[indexSkill].name = tbxName.Text;
            GAMEDATA.skills[indexSkill].description = tbxDesc.Text;
            GAMEDATA.skills[indexSkill].sound = tbxSound.Text;
            GAMEDATA.skills[indexSkill].spritePreview = cbxIcon.Text;
            GAMEDATA.skills[indexSkill].spriteSkill = cbxAnimS.Text;
            GAMEDATA.skills[indexSkill].castAnim = cbxAnimC.Text;

            float floatTryParse;
            int intTryParse;

            float.TryParse(tbxCD.Text, out floatTryParse);
            GAMEDATA.skills[indexSkill].countdown = floatTryParse;           

            int.TryParse(tbxLvReq.Text, out intTryParse);
            GAMEDATA.skills[indexSkill].lvlRequire = intTryParse;

            int.TryParse(tbxLvReq.Text, out intTryParse);
            GAMEDATA.skills[indexSkill].lvlRequire = intTryParse;

            int.TryParse(tbxMpCost.Text, out intTryParse);
            GAMEDATA.skills[indexSkill].mpCost = intTryParse;

            int.TryParse(tbxClassReq.Text, out intTryParse);
            GAMEDATA.skills[indexSkill].classRequire = intTryParse;

            float.TryParse(tbxCastTime.Text, out floatTryParse);
            GAMEDATA.skills[indexSkill].castTime = floatTryParse;

            float.TryParse(tbxStunOwn.Text, out floatTryParse);
            GAMEDATA.skills[indexSkill].stunOwner = floatTryParse;

            int.TryParse(tbxExp.Text, out intTryParse);
            GAMEDATA.skills[indexSkill].expNextSkill = intTryParse;

            int.TryParse(tbxNextSkill.Text, out intTryParse);
            GAMEDATA.skills[indexSkill].newSkill = intTryParse;

            GAMEDATA.skills[indexSkill].skillTypes = (SkillTypes)Enum.ToObject(typeof(SkillTypes), cbxTypes.SelectedIndex + 1);

            switch (GAMEDATA.skills[indexSkill].skillTypes)
            {
                case SkillTypes.Line:
                    float.TryParse(tbxRangeLine.Text, out floatTryParse);
                    GAMEDATA.skills[indexSkill].range = floatTryParse;

                    int.TryParse(tbxLenLine.Text, out intTryParse);
                    GAMEDATA.skills[indexSkill].len = intTryParse;

                    GAMEDATA.skills[indexSkill].spriteGetHit = cbxAnimLine.Text;

                    float.TryParse(tbxStunTLine.Text, out floatTryParse);
                    GAMEDATA.skills[indexSkill].stunTarget = floatTryParse;

                    GAMEDATA.skills[indexSkill].throwAble = chbThrow.Checked;

                    GAMEDATA.skills[indexSkill].skillSpec = (SkillSpec)Enum.ToObject(typeof(SkillSpec), cbxDmgType.SelectedIndex + 1);

                    int.TryParse(tbxDmgLine.Text, out intTryParse);
                    GAMEDATA.skills[indexSkill].numDmgSpec = intTryParse;
                    break;

                case SkillTypes.Place:
                    float.TryParse(tbxRangePlace.Text, out floatTryParse);
                    GAMEDATA.skills[indexSkill].range = floatTryParse;

                    int.TryParse(tbxLenPlace.Text, out intTryParse);
                    GAMEDATA.skills[indexSkill].len = intTryParse;

                    GAMEDATA.skills[indexSkill].spriteGetHit = cbxAnimPlace.Text;

                    float.TryParse(tbxStunTPlace.Text, out floatTryParse);
                    GAMEDATA.skills[indexSkill].stunTarget = floatTryParse;

                    GAMEDATA.skills[indexSkill].targetAble = cbxTargetPlace.Checked;

                    GAMEDATA.skills[indexSkill].skillUse = (SkillUse)Enum.ToObject(typeof(SkillUse), cbxSkillUsePlace.SelectedIndex + 1);
                    GAMEDATA.skills[indexSkill].skillSpec = (SkillSpec)Enum.ToObject(typeof(SkillSpec), cbxSpecPlace.SelectedIndex + 1);
                    
                    int.TryParse(tbxDmgPlace.Text, out intTryParse);
                    GAMEDATA.skills[indexSkill].numDmgSpec = intTryParse;
                    break;

                case SkillTypes.Shield:
                    break;

                case SkillTypes.Teleport:
                    float x, y;
                    float.TryParse(tbxX.Text, out x);
                    float.TryParse(tbxY.Text, out y);
                    GAMEDATA.skills[indexSkill].positionEnd = new Vector2(x,y);

                    int.TryParse(tbxMapID.Text, out intTryParse);
                    GAMEDATA.skills[indexSkill].idMapTarget = intTryParse;

                    float.TryParse(tbxTimeAlive.Text, out floatTryParse);
                    GAMEDATA.skills[indexSkill].timeAliveSkill = floatTryParse;
                    GAMEDATA.skills[indexSkill].playerUseable = (PlayerUseable)Enum.ToObject(typeof(PlayerUseable), cbxPlayerUseable.SelectedIndex + 1);
                    break;

                case SkillTypes.JumpInto:
                    float.TryParse(tbxRangeJump.Text, out floatTryParse);
                    GAMEDATA.skills[indexSkill].range = floatTryParse;

                    int.TryParse(tbxLenJump.Text, out intTryParse);
                    GAMEDATA.skills[indexSkill].len = intTryParse;

                    GAMEDATA.skills[indexSkill].spriteGetHit = cbxAnimJump.Text;

                    float.TryParse(tbxStunTJump.Text, out floatTryParse);
                    GAMEDATA.skills[indexSkill].stunTarget = floatTryParse;

                    GAMEDATA.skills[indexSkill].targetAble = cbxTargetJump.Checked;

                    GAMEDATA.skills[indexSkill].skillUse = (SkillUse)Enum.ToObject(typeof(SkillUse), cbxSkillUseJump.SelectedIndex + 1);
                    GAMEDATA.skills[indexSkill].skillSpec = (SkillSpec)Enum.ToObject(typeof(SkillSpec), cbxSpecJump.SelectedIndex + 1);

                    int.TryParse(tbxDmgJump.Text, out intTryParse);
                    GAMEDATA.skills[indexSkill].numDmgSpec = intTryParse;
                    break;
            }

            Skills.SaveData(indexSkill, GAMEDATA.skills[indexSkill]);

            //init list skill
            listSkills.Items.Clear();
            for (int i = 0; i < GAMEDATA.skills.Length; i++)
            {
                SkillRec skillGet = GAMEDATA.skills[i];

                string showing = i + ": " + (skillGet.name == null || skillGet.name == "" ? "Null" : skillGet.name);
                listSkills.Items.Add(showing);
            }

            for(int i=0;i< Server.clients.Count; i++)
            {
                if (Server.clients[i].player == null) continue;

                List<SkillInventory> getList = Server.clients[i].player.skillInven;
                if (getList == null) continue;

                if (getList.Exists(x=>x.id == indexSkill))
                {
                    for (int y = 0; y < getList.Count; y++)
                    {
                        if (getList[y].id != indexSkill) continue;

                        SkillInventory editSkill = new SkillInventory();
                        editSkill.id = indexSkill;
                        editSkill.item = GAMEDATA.skills[indexSkill];

                        getList[y] = editSkill;
                    }
                    Server.clients[i].player.skillInven = getList;
                    ServerSend.SendInventorySkillPlayer(i, getList);
                }
                
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            SkillRec clear = new SkillRec();
            clear.name = null;
            GAMEDATA.skills[indexSkill] = clear;

            InittializeListSkills();
            for (int i = 0; i < Server.clients.Count; i++)
            {
                if (Server.clients[i].player == null) continue;

                Server.clients[i].player.InitInventorySkill();
                ServerSend.SendInventorySkillPlayer(i, Server.clients[i].player.skillInven);
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

                        List<SkillInventory> getList = new List<SkillInventory>();
                        if (playerRead.skillInven == null) continue;

                        if (playerRead.skillInven.Exists(x => x.id == indexSkill))
                        {
                            for (int y = 0; y < playerRead.skillInven.Count; y++)
                            {
                                if (playerRead.skillInven[y].id != indexSkill)
                                    getList.Add(playerRead.skillInven[y]);
                            }

                            playerRead.skillInven = getList;

                            binaryFormatter.Serialize(stream, playerRead);
                            stream.Close();
                            continue;
                        }
                    }

                }
            }

            LoadSkill(indexSkill);
            btnSave_Click(sender, e);
        }
    }
}
