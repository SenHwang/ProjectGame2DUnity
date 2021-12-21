namespace Server
{
    public partial class ServerGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbxMainChat = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.lblMaxPlayer = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbxChat = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listPlayerOnline = new System.Windows.Forms.ListView();
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ipConnect = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.role = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.level = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnEditSkill = new System.Windows.Forms.Button();
            this.btnEditItem = new System.Windows.Forms.Button();
            this.btnEditNPC = new System.Windows.Forms.Button();
            this.btnEditMap = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStripPlayer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teleToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.menuStripPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Controls.Add(this.tabPage3);
            this.TabControl.Location = new System.Drawing.Point(12, 12);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(647, 261);
            this.TabControl.TabIndex = 4;
            this.TabControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TabControl_MouseClick);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.lbxMainChat);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.btnSend);
            this.tabPage1.Controls.Add(this.tbxChat);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(639, 235);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            // 
            // lbxMainChat
            // 
            this.lbxMainChat.FormattingEnabled = true;
            this.lbxMainChat.Location = new System.Drawing.Point(142, 9);
            this.lbxMainChat.Name = "lbxMainChat";
            this.lbxMainChat.Size = new System.Drawing.Size(486, 186);
            this.lbxMainChat.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblPlayer);
            this.groupBox2.Controls.Add(this.lblMaxPlayer);
            this.groupBox2.Controls.Add(this.lblPort);
            this.groupBox2.Location = new System.Drawing.Point(8, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(128, 215);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Server";
            // 
            // lblPlayer
            // 
            this.lblPlayer.AutoSize = true;
            this.lblPlayer.Location = new System.Drawing.Point(6, 75);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(39, 13);
            this.lblPlayer.TabIndex = 0;
            this.lblPlayer.Text = "Player:";
            // 
            // lblMaxPlayer
            // 
            this.lblMaxPlayer.AutoSize = true;
            this.lblMaxPlayer.Location = new System.Drawing.Point(6, 47);
            this.lblMaxPlayer.Name = "lblMaxPlayer";
            this.lblMaxPlayer.Size = new System.Drawing.Size(62, 13);
            this.lblMaxPlayer.TabIndex = 0;
            this.lblMaxPlayer.Text = "Max Player:";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(6, 23);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 13);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "Port:";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(556, 203);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // tbxChat
            // 
            this.tbxChat.Location = new System.Drawing.Point(142, 204);
            this.tbxChat.Name = "tbxChat";
            this.tbxChat.Size = new System.Drawing.Size(408, 20);
            this.tbxChat.TabIndex = 5;
            this.tbxChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SendMessge);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.listPlayerOnline);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(639, 235);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Player Online";
            // 
            // listPlayerOnline
            // 
            this.listPlayerOnline.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.userName,
            this.ipConnect,
            this.role,
            this.level});
            this.listPlayerOnline.FullRowSelect = true;
            this.listPlayerOnline.HideSelection = false;
            this.listPlayerOnline.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listPlayerOnline.Location = new System.Drawing.Point(0, 0);
            this.listPlayerOnline.Name = "listPlayerOnline";
            this.listPlayerOnline.Size = new System.Drawing.Size(639, 235);
            this.listPlayerOnline.TabIndex = 0;
            this.listPlayerOnline.UseCompatibleStateImageBehavior = false;
            this.listPlayerOnline.View = System.Windows.Forms.View.Details;
            this.listPlayerOnline.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listPlayerOnline_MouseClick);
            // 
            // id
            // 
            this.id.Text = "ID";
            this.id.Width = 40;
            // 
            // userName
            // 
            this.userName.Text = "UserName";
            this.userName.Width = 148;
            // 
            // ipConnect
            // 
            this.ipConnect.Text = "Connect From";
            this.ipConnect.Width = 168;
            // 
            // role
            // 
            this.role.Text = "Role";
            this.role.Width = 79;
            // 
            // level
            // 
            this.level.Text = "Level";
            this.level.Width = 56;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage3.Controls.Add(this.btnEditSkill);
            this.tabPage3.Controls.Add(this.btnEditItem);
            this.tabPage3.Controls.Add(this.btnEditNPC);
            this.tabPage3.Controls.Add(this.btnEditMap);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(639, 235);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Edit Game";
            // 
            // btnEditSkill
            // 
            this.btnEditSkill.Location = new System.Drawing.Point(0, 87);
            this.btnEditSkill.Name = "btnEditSkill";
            this.btnEditSkill.Size = new System.Drawing.Size(75, 23);
            this.btnEditSkill.TabIndex = 0;
            this.btnEditSkill.Text = "Edit Skill";
            this.btnEditSkill.UseVisualStyleBackColor = true;
            this.btnEditSkill.Click += new System.EventHandler(this.btnEditSkill_Click);
            // 
            // btnEditItem
            // 
            this.btnEditItem.Location = new System.Drawing.Point(0, 58);
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Size = new System.Drawing.Size(75, 23);
            this.btnEditItem.TabIndex = 0;
            this.btnEditItem.Text = "Edit Item";
            this.btnEditItem.UseVisualStyleBackColor = true;
            this.btnEditItem.Click += new System.EventHandler(this.btnEditItem_Click);
            // 
            // btnEditNPC
            // 
            this.btnEditNPC.Location = new System.Drawing.Point(0, 29);
            this.btnEditNPC.Name = "btnEditNPC";
            this.btnEditNPC.Size = new System.Drawing.Size(75, 23);
            this.btnEditNPC.TabIndex = 0;
            this.btnEditNPC.Text = "Edit NPC";
            this.btnEditNPC.UseVisualStyleBackColor = true;
            this.btnEditNPC.Click += new System.EventHandler(this.btnEditNPC_Click);
            // 
            // btnEditMap
            // 
            this.btnEditMap.Location = new System.Drawing.Point(0, 0);
            this.btnEditMap.Name = "btnEditMap";
            this.btnEditMap.Size = new System.Drawing.Size(75, 23);
            this.btnEditMap.TabIndex = 0;
            this.btnEditMap.Text = "Edit Map";
            this.btnEditMap.UseVisualStyleBackColor = true;
            this.btnEditMap.Click += new System.EventHandler(this.btnEditMap_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStripPlayer
            // 
            this.menuStripPlayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInfoToolStripMenuItem,
            this.teleToToolStripMenuItem,
            this.disconnectToolStripMenuItem,
            this.banToolStripMenuItem});
            this.menuStripPlayer.Name = "menuStripPlayer";
            this.menuStripPlayer.Size = new System.Drawing.Size(132, 92);
            // 
            // showInfoToolStripMenuItem
            // 
            this.showInfoToolStripMenuItem.Name = "showInfoToolStripMenuItem";
            this.showInfoToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.showInfoToolStripMenuItem.Text = "Show Info";
            this.showInfoToolStripMenuItem.Click += new System.EventHandler(this.showInfoToolStripMenuItem_Click);
            // 
            // teleToToolStripMenuItem
            // 
            this.teleToToolStripMenuItem.Name = "teleToToolStripMenuItem";
            this.teleToToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.teleToToolStripMenuItem.Text = "Teleport To";
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.disconnectToolStripMenuItem.Text = "Kick";
            // 
            // banToolStripMenuItem
            // 
            this.banToolStripMenuItem.Name = "banToolStripMenuItem";
            this.banToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.banToolStripMenuItem.Text = "Ban";
            // 
            // ServerGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 273);
            this.Controls.Add(this.TabControl);
            this.MaximizeBox = false;
            this.Name = "ServerGame";
            this.Text = "ServerGame";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.ServerGame_Load);
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.menuStripPlayer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.Label lblMaxPlayer;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbxChat;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox lbxMainChat;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnEditMap;
        private System.Windows.Forms.Button btnEditNPC;
        private System.Windows.Forms.Button btnEditItem;
        private System.Windows.Forms.Button btnEditSkill;
        private System.Windows.Forms.ListView listPlayerOnline;
        private System.Windows.Forms.ColumnHeader id;
        private System.Windows.Forms.ColumnHeader userName;
        private System.Windows.Forms.ColumnHeader ipConnect;
        private System.Windows.Forms.ColumnHeader role;
        private System.Windows.Forms.ColumnHeader level;
        private System.Windows.Forms.ContextMenuStrip menuStripPlayer;
        private System.Windows.Forms.ToolStripMenuItem showInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem teleToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem banToolStripMenuItem;
    }
}