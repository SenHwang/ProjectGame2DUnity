using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Web.Script.Serialization;
using System.IO;
using Server.Data;
using Server.FormPlayerOnline;
using Server.GameData;
using Server.FormChild;
using Server.ObjectGame;

namespace Server
{
    public partial class ServerGame : Form
    {
        private static bool isRunning = false;

        private const int MaxPlayer = 50;
        private const int Port = 26950;
        private int playerConnect = 0;
        public ServerGame()
        {
            InitializeComponent();
            isRunning = true;
            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();
            Server.Start(MaxPlayer, Port);
            GAMEDATA.APPLICATION_PATH = Application.StartupPath;
            Map.InitializeMap();
            NPC.InitializeNPC();
            Skills.InitializeSkill();
            Items.InitializeItems();
            //this.Refresh();
        }

        //Load map
        /// <summary>
        /// InitializeMap
        /// Load dữ liệu map từ file json
        /// </summary>
        private static void MainThread()
        {
            Console.WriteLine($"Main thread running at {Constants.TICK_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;

            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();
                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);
                    if (_nextLoop > DateTime.Now)
                    {
                        var numSleep = _nextLoop - DateTime.Now;
                        if (_nextLoop < DateTime.Now) continue;                                           
                        Thread.Sleep(numSleep);
                    }
                }
            }
        }


        /*
         * Exit ứng dụng 
         */
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
                if (MessageBox.Show("Bạn Muốn Thoát?", "Exit Server Game", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    //TODO: Thoát ứng dụng nhưng k thoát console
                    Application.Exit();
                    Environment.Exit(0);
                    //e.Cancel = true;

                }
                else
                {
                    return;
                }
            //}
        }


        public void MainChatBox(string message)
        {
            if (message.Length == 0) return;
            lbxMainChat.Items.Add(message);
            
            lbxMainChat.SelectedIndex = lbxMainChat.Items.Count - 1;
            lbxMainChat.SelectedIndex = -1;
        }

        private void SendMessge(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSendMessage_Click(sender, e);
            }               
        }
         
        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (tbxChat.Text == null || tbxChat.Text == "") return;


            MainChatBox("Server: " + tbxChat.Text);
            ChatRec mess = new ChatRec();
            mess.chatType = ChatType.World;
            mess.idClient = -1;
            mess.message.line = 1;
            mess.message.text = tbxChat.Text;

            ChatSystem.MessageParse(mess);
            tbxChat.Text = null;
        }
       

        private void ServerGame_Load(object sender, EventArgs e)
        {            
            lblMaxPlayer.Text = "Max Player: " + MaxPlayer;
            lblPort.Text = "Port: " + Port;
            lblPlayer.Text = "Player: " + playerConnect;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!isRunning) return;
            if (TaskQueue.listMessage.Count == 0) return;
            MainChatBox(TaskQueue.listMessage.First());
            TaskQueue.listMessage.RemoveAt(0);
            playerConnect = 0;
            for (int i = 0; i < MaxPlayer; i++)
            {
                if (Server.clients[i].tcp.socket == null) continue;     
                
                playerConnect +=1;                
            }

            lblPlayer.Text = "Player: " + playerConnect;
        }

        private void btnEditMap_Click(object sender, EventArgs e)
        {
        }

        private void TabControl_MouseClick(object sender, MouseEventArgs e)
        {
            listPlayerOnline.Items.Clear();
            if (TaskQueue.players.Count != 0)
            {                
                for (int i = 0; i < TaskQueue.players.Count; i++)
                {
                    Client cConnect = (Client)TaskQueue.players[i];
                    Player p = cConnect.player;
                    ListViewItem item;
                    string[] userAddList = new string[5];
                    userAddList[0] = p.id.ToString();
                    userAddList[1] = p.username.ToString();
                    userAddList[2] = cConnect.tcp.socket.Client.RemoteEndPoint.ToString();
                    userAddList[3] = "";
                    userAddList[4] = "";
                    item = new ListViewItem(userAddList);
                    listPlayerOnline.Items.Add(item);
                }
                    
            }
            
        }


        string ID;
        string name;
        private void listPlayerOnline_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem focusedItem = listPlayerOnline.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    menuStripPlayer.Show(Cursor.Position);
                    ID = focusedItem.SubItems[0].Text;
                    name = focusedItem.SubItems[1].Text;

                }
            }
        }

        private void showInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {//
            Player playerGet = Server.clients.SingleOrDefault(x => x.Key == int.Parse(ID)).Value.player;
            if (playerGet == null) return;
            TaskQueue.playerFind = playerGet;
            bool isOpen = false;

            foreach(Form i in Application.OpenForms)
            {
                if(i.Text == "PlayerInfo")
                {
                    isOpen = true;
                    i.Focus();
                    break;
                }
            }

            if (!isOpen)
            {
                PlayerInfo info = new PlayerInfo();
                info.Show();
                info.Focus();
            }
        }

        private void btnEditNPC_Click(object sender, EventArgs e)
        {
            bool isOpen = false;

            foreach (Form i in Application.OpenForms)
            {
                if (i.Text == "NPCManager")
                {
                    isOpen = true;
                    i.Focus();
                    break;
                }
            }

            if (!isOpen)
            {
                NPCManager npc = new NPCManager();
                npc.Show();
                npc.Focus();
            }
        }

        private void btnEditSkill_Click(object sender, EventArgs e)
        {
            bool isOpen = false;

            foreach (Form i in Application.OpenForms)
            {
                if (i.Text == "SkillManager")
                {
                    isOpen = true;
                    i.Focus();
                    break;
                }
            }

            if (!isOpen)
            {
                SkillManager skills = new SkillManager();
                skills.Show();
                skills.Focus();
            }
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            bool isOpen = false;

            foreach (Form i in Application.OpenForms)
            {
                if (i.Text == "ItemManager")
                {
                    isOpen = true;
                    i.Focus();
                    break;
                }
            }

            if (!isOpen)
            {
                ItemManager items = new ItemManager();
                items.Show();
                items.Focus();
            }
        }
    }



}
