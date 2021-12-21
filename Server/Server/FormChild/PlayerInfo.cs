using Server.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server.FormPlayerOnline
{
    public partial class PlayerInfo : Form
    {
        public PlayerInfo()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            Player pGet = (Player)TaskQueue.playerFind;
            tbxIDConnect.Text = pGet.id.ToString();
            tbxName.Text = pGet.username;
            tbxLevel.Text = pGet.stat.level.ToString();
            tbxMapID.Text = pGet.mapID.ToString();
            tbxMapName.Text = "Chưa Set";

            tbxBody.Text = pGet.Equipment[0];
            tbxHair.Text = pGet.Equipment[1];
            tbxHat.Text = pGet.Equipment[2];
            tbxPants.Text = pGet.Equipment[3];
            tbxShoe.Text = pGet.Equipment[4];
            tbxClother.Text = pGet.Equipment[5];
            tbxWeapon.Text = pGet.Equipment[6];

            tbxHp.Text = pGet.stat.health.ToString();
            tbxMp.Text = pGet.stat.mana.ToString();

            tbxStrength.Text = pGet.stat.strength.ToString();
            tbxAgility.Text = pGet.stat.agility.ToString();
            tbxIntellect.Text = pGet.stat.intellect.ToString();
            tbxMPStat.Text = pGet.stat.mp.ToString();
            tbxHPStat.Text = pGet.stat.hp.ToString();
            tbxPoint.Text = pGet.stat.pointFree.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Player pGet = (Player)TaskQueue.playerFind;
            pGet.Equipment[0] = tbxBody.Text;
            pGet.Equipment[1] = tbxHair.Text;
            pGet.Equipment[2] = tbxHat.Text;
            pGet.Equipment[3] = tbxPants.Text;
            pGet.Equipment[4] = tbxShoe.Text;
            pGet.Equipment[5] = tbxClother.Text;
            pGet.Equipment[6] = tbxWeapon.Text;

            if (pGet.mapID != int.Parse(tbxMapID.Text))
            {
                pGet.mapID = int.Parse(tbxMapID.Text);
                ServerSend.RefreshMap(pGet.id, pGet.mapID, Server.clients[pGet.id].player.position);
            }
            pGet.stat.level = int.Parse(tbxLevel.Text);
            pGet.stat.health = int.Parse(tbxHp.Text);
            pGet.stat.mana = int.Parse(tbxMp.Text);

            pGet.stat.strength = int.Parse(tbxStrength.Text);
            pGet.stat.agility = int.Parse(tbxAgility.Text);
            pGet.stat.intellect = int.Parse(tbxIntellect.Text);
            pGet.stat.mp = int.Parse(tbxMPStat.Text);
            pGet.stat.hp = int.Parse(tbxHPStat.Text);
            pGet.stat.pointFree = int.Parse(tbxPoint.Text);

            ServerSend.PlayerEquipment(pGet);
            ServerSend.UpdateStatPlayer(pGet);
            Account.SavePlayer(pGet);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (Form i in Application.OpenForms)
            {
                if (i.Text == "PlayerInfo")
                {
                    i.Close();
                    break;
                }
            }
        }

        
    }
}
