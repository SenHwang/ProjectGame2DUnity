
namespace Server.FormChild
{
    partial class NPCManager
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
            this.listNPC = new System.Windows.Forms.ListView();
            this.lblName = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.tbxSpeed = new System.Windows.Forms.TextBox();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxSprite = new System.Windows.Forms.ComboBox();
            this.cbxTypes = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.rawSprite = new System.Windows.Forms.PictureBox();
            this.cbxMovable = new System.Windows.Forms.CheckBox();
            this.listStory = new System.Windows.Forms.ListView();
            this.grbNpc = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStoryAdd = new System.Windows.Forms.Button();
            this.grbMonster = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnItems = new System.Windows.Forms.Button();
            this.lvwItem = new System.Windows.Forms.ListView();
            this.tbxLvl = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxExp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbxDamage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxHp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSkillAdd = new System.Windows.Forms.Button();
            this.lvwSkill = new System.Windows.Forms.ListView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grbAddStory = new System.Windows.Forms.GroupBox();
            this.btnRmStory = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxTypeStory = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxTitleStory = new System.Windows.Forms.TextBox();
            this.tbxStory = new System.Windows.Forms.RichTextBox();
            this.btnASExit = new System.Windows.Forms.Button();
            this.btnASSave = new System.Windows.Forms.Button();
            this.tbxDesc = new System.Windows.Forms.RichTextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.rawSprite)).BeginInit();
            this.grbNpc.SuspendLayout();
            this.grbMonster.SuspendLayout();
            this.grbAddStory.SuspendLayout();
            this.SuspendLayout();
            // 
            // listNPC
            // 
            this.listNPC.FullRowSelect = true;
            this.listNPC.HideSelection = false;
            this.listNPC.Location = new System.Drawing.Point(12, 12);
            this.listNPC.Name = "listNPC";
            this.listNPC.Size = new System.Drawing.Size(127, 313);
            this.listNPC.TabIndex = 0;
            this.listNPC.UseCompatibleStateImageBehavior = false;
            this.listNPC.View = System.Windows.Forms.View.Tile;
            this.listNPC.SelectedIndexChanged += new System.EventHandler(this.listNPC_SelectedIndexChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(145, 26);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(186, 23);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(100, 20);
            this.tbxName.TabIndex = 2;
            // 
            // tbxSpeed
            // 
            this.tbxSpeed.Location = new System.Drawing.Point(186, 49);
            this.tbxSpeed.Name = "tbxSpeed";
            this.tbxSpeed.Size = new System.Drawing.Size(100, 20);
            this.tbxSpeed.TabIndex = 4;
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(145, 52);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(38, 13);
            this.lblSpeed.TabIndex = 3;
            this.lblSpeed.Text = "Speed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(303, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Sprite";
            // 
            // cbxSprite
            // 
            this.cbxSprite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSprite.FormattingEnabled = true;
            this.cbxSprite.Location = new System.Drawing.Point(343, 44);
            this.cbxSprite.Name = "cbxSprite";
            this.cbxSprite.Size = new System.Drawing.Size(52, 21);
            this.cbxSprite.TabIndex = 6;
            this.cbxSprite.SelectedIndexChanged += new System.EventHandler(this.SpriteChange);
            // 
            // cbxTypes
            // 
            this.cbxTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTypes.FormattingEnabled = true;
            this.cbxTypes.Location = new System.Drawing.Point(403, 22);
            this.cbxTypes.Name = "cbxTypes";
            this.cbxTypes.Size = new System.Drawing.Size(102, 21);
            this.cbxTypes.TabIndex = 7;
            this.cbxTypes.SelectedIndexChanged += new System.EventHandler(this.TypeChange);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(363, 26);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(36, 13);
            this.lblType.TabIndex = 8;
            this.lblType.Text = "Types";
            // 
            // rawSprite
            // 
            this.rawSprite.Location = new System.Drawing.Point(403, 48);
            this.rawSprite.Name = "rawSprite";
            this.rawSprite.Size = new System.Drawing.Size(102, 94);
            this.rawSprite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.rawSprite.TabIndex = 9;
            this.rawSprite.TabStop = false;
            // 
            // cbxMovable
            // 
            this.cbxMovable.AutoSize = true;
            this.cbxMovable.Location = new System.Drawing.Point(295, 24);
            this.cbxMovable.Name = "cbxMovable";
            this.cbxMovable.Size = new System.Drawing.Size(67, 17);
            this.cbxMovable.TabIndex = 10;
            this.cbxMovable.Text = "Movable";
            this.cbxMovable.UseVisualStyleBackColor = true;
            // 
            // listStory
            // 
            this.listStory.FullRowSelect = true;
            this.listStory.HideSelection = false;
            this.listStory.Location = new System.Drawing.Point(6, 70);
            this.listStory.Name = "listStory";
            this.listStory.Size = new System.Drawing.Size(335, 82);
            this.listStory.TabIndex = 11;
            this.listStory.TileSize = new System.Drawing.Size(335, 20);
            this.listStory.UseCompatibleStateImageBehavior = false;
            this.listStory.View = System.Windows.Forms.View.Tile;
            this.listStory.SelectedIndexChanged += new System.EventHandler(this.listStory_SelectedIndexChanged);
            // 
            // grbNpc
            // 
            this.grbNpc.Controls.Add(this.listStory);
            this.grbNpc.Controls.Add(this.label2);
            this.grbNpc.Controls.Add(this.btnStoryAdd);
            this.grbNpc.Location = new System.Drawing.Point(156, 146);
            this.grbNpc.Name = "grbNpc";
            this.grbNpc.Size = new System.Drawing.Size(347, 164);
            this.grbNpc.TabIndex = 12;
            this.grbNpc.TabStop = false;
            this.grbNpc.Text = "Npc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Story";
            // 
            // btnStoryAdd
            // 
            this.btnStoryAdd.Location = new System.Drawing.Point(268, 41);
            this.btnStoryAdd.Name = "btnStoryAdd";
            this.btnStoryAdd.Size = new System.Drawing.Size(75, 23);
            this.btnStoryAdd.TabIndex = 12;
            this.btnStoryAdd.Text = "Add";
            this.btnStoryAdd.UseVisualStyleBackColor = true;
            this.btnStoryAdd.Click += new System.EventHandler(this.btnStoryAdd_Click);
            // 
            // grbMonster
            // 
            this.grbMonster.Controls.Add(this.label8);
            this.grbMonster.Controls.Add(this.label3);
            this.grbMonster.Controls.Add(this.btnItems);
            this.grbMonster.Controls.Add(this.lvwItem);
            this.grbMonster.Controls.Add(this.tbxLvl);
            this.grbMonster.Controls.Add(this.label7);
            this.grbMonster.Controls.Add(this.tbxExp);
            this.grbMonster.Controls.Add(this.label6);
            this.grbMonster.Controls.Add(this.tbxDamage);
            this.grbMonster.Controls.Add(this.label5);
            this.grbMonster.Controls.Add(this.tbxHp);
            this.grbMonster.Controls.Add(this.label4);
            this.grbMonster.Controls.Add(this.btnSkillAdd);
            this.grbMonster.Controls.Add(this.lvwSkill);
            this.grbMonster.Location = new System.Drawing.Point(156, 145);
            this.grbMonster.Name = "grbMonster";
            this.grbMonster.Size = new System.Drawing.Size(347, 158);
            this.grbMonster.TabIndex = 14;
            this.grbMonster.TabStop = false;
            this.grbMonster.Text = "Monster";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(245, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Skills";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(134, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Items";
            // 
            // btnItems
            // 
            this.btnItems.Location = new System.Drawing.Point(172, 39);
            this.btnItems.Name = "btnItems";
            this.btnItems.Size = new System.Drawing.Size(58, 23);
            this.btnItems.TabIndex = 23;
            this.btnItems.Text = "Add";
            this.btnItems.UseVisualStyleBackColor = true;
            // 
            // lvwItem
            // 
            this.lvwItem.HideSelection = false;
            this.lvwItem.Location = new System.Drawing.Point(125, 68);
            this.lvwItem.Name = "lvwItem";
            this.lvwItem.Size = new System.Drawing.Size(105, 82);
            this.lvwItem.TabIndex = 22;
            this.lvwItem.UseCompatibleStateImageBehavior = false;
            // 
            // tbxLvl
            // 
            this.tbxLvl.Location = new System.Drawing.Point(236, 13);
            this.tbxLvl.Name = "tbxLvl";
            this.tbxLvl.Size = new System.Drawing.Size(61, 20);
            this.tbxLvl.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(208, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Lvl";
            // 
            // tbxExp
            // 
            this.tbxExp.Location = new System.Drawing.Point(139, 13);
            this.tbxExp.Name = "tbxExp";
            this.tbxExp.Size = new System.Drawing.Size(61, 20);
            this.tbxExp.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(111, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Exp";
            // 
            // tbxDamage
            // 
            this.tbxDamage.Location = new System.Drawing.Point(34, 41);
            this.tbxDamage.Name = "tbxDamage";
            this.tbxDamage.Size = new System.Drawing.Size(61, 20);
            this.tbxDamage.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Dmg";
            // 
            // tbxHp
            // 
            this.tbxHp.Location = new System.Drawing.Point(34, 13);
            this.tbxHp.Name = "tbxHp";
            this.tbxHp.Size = new System.Drawing.Size(61, 20);
            this.tbxHp.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "HP";
            // 
            // btnSkillAdd
            // 
            this.btnSkillAdd.Location = new System.Drawing.Point(283, 39);
            this.btnSkillAdd.Name = "btnSkillAdd";
            this.btnSkillAdd.Size = new System.Drawing.Size(58, 23);
            this.btnSkillAdd.TabIndex = 12;
            this.btnSkillAdd.Text = "Add";
            this.btnSkillAdd.UseVisualStyleBackColor = true;
            // 
            // lvwSkill
            // 
            this.lvwSkill.HideSelection = false;
            this.lvwSkill.Location = new System.Drawing.Point(236, 68);
            this.lvwSkill.Name = "lvwSkill";
            this.lvwSkill.Size = new System.Drawing.Size(105, 82);
            this.lvwSkill.TabIndex = 11;
            this.lvwSkill.UseCompatibleStateImageBehavior = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(369, 331);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(58, 23);
            this.btnExit.TabIndex = 26;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(441, 331);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(58, 23);
            this.btnSave.TabIndex = 27;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grbAddStory
            // 
            this.grbAddStory.Controls.Add(this.btnRmStory);
            this.grbAddStory.Controls.Add(this.label10);
            this.grbAddStory.Controls.Add(this.cbxTypeStory);
            this.grbAddStory.Controls.Add(this.label9);
            this.grbAddStory.Controls.Add(this.tbxTitleStory);
            this.grbAddStory.Controls.Add(this.tbxStory);
            this.grbAddStory.Controls.Add(this.btnASExit);
            this.grbAddStory.Controls.Add(this.btnASSave);
            this.grbAddStory.Location = new System.Drawing.Point(549, 122);
            this.grbAddStory.Name = "grbAddStory";
            this.grbAddStory.Size = new System.Drawing.Size(347, 164);
            this.grbAddStory.TabIndex = 28;
            this.grbAddStory.TabStop = false;
            this.grbAddStory.Text = "Story";
            // 
            // btnRmStory
            // 
            this.btnRmStory.Location = new System.Drawing.Point(157, 135);
            this.btnRmStory.Name = "btnRmStory";
            this.btnRmStory.Size = new System.Drawing.Size(58, 23);
            this.btnRmStory.TabIndex = 35;
            this.btnRmStory.Text = "Remove";
            this.btnRmStory.UseVisualStyleBackColor = true;
            this.btnRmStory.Click += new System.EventHandler(this.btnRmStory_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(179, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Types";
            // 
            // cbxTypeStory
            // 
            this.cbxTypeStory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTypeStory.FormattingEnabled = true;
            this.cbxTypeStory.Location = new System.Drawing.Point(219, 19);
            this.cbxTypeStory.Name = "cbxTypeStory";
            this.cbxTypeStory.Size = new System.Drawing.Size(102, 21);
            this.cbxTypeStory.TabIndex = 33;
            this.cbxTypeStory.SelectedIndexChanged += new System.EventHandler(this.TypeStoryChange);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Title";
            // 
            // tbxTitleStory
            // 
            this.tbxTitleStory.Location = new System.Drawing.Point(47, 19);
            this.tbxTitleStory.Name = "tbxTitleStory";
            this.tbxTitleStory.Size = new System.Drawing.Size(100, 20);
            this.tbxTitleStory.TabIndex = 31;
            // 
            // tbxStory
            // 
            this.tbxStory.Location = new System.Drawing.Point(17, 62);
            this.tbxStory.Name = "tbxStory";
            this.tbxStory.Size = new System.Drawing.Size(312, 55);
            this.tbxStory.TabIndex = 30;
            this.tbxStory.Text = "";
            // 
            // btnASExit
            // 
            this.btnASExit.Location = new System.Drawing.Point(219, 135);
            this.btnASExit.Name = "btnASExit";
            this.btnASExit.Size = new System.Drawing.Size(58, 23);
            this.btnASExit.TabIndex = 29;
            this.btnASExit.Text = "Exit";
            this.btnASExit.UseVisualStyleBackColor = true;
            this.btnASExit.Click += new System.EventHandler(this.btnASExit_Click);
            // 
            // btnASSave
            // 
            this.btnASSave.Location = new System.Drawing.Point(283, 135);
            this.btnASSave.Name = "btnASSave";
            this.btnASSave.Size = new System.Drawing.Size(58, 23);
            this.btnASSave.TabIndex = 28;
            this.btnASSave.Text = "Save";
            this.btnASSave.UseVisualStyleBackColor = true;
            this.btnASSave.Click += new System.EventHandler(this.btnASSave_Click);
            // 
            // tbxDesc
            // 
            this.tbxDesc.Location = new System.Drawing.Point(186, 81);
            this.tbxDesc.Name = "tbxDesc";
            this.tbxDesc.Size = new System.Drawing.Size(209, 58);
            this.tbxDesc.TabIndex = 30;
            this.tbxDesc.Text = "";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(153, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(26, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "Des";
            // 
            // NPCManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 361);
            this.Controls.Add(this.tbxDesc);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.grbAddStory);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cbxMovable);
            this.Controls.Add(this.rawSprite);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cbxTypes);
            this.Controls.Add(this.cbxSprite);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxSpeed);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.listNPC);
            this.Controls.Add(this.grbNpc);
            this.Controls.Add(this.grbMonster);
            this.Name = "NPCManager";
            this.Text = "NPCManager";
            ((System.ComponentModel.ISupportInitialize)(this.rawSprite)).EndInit();
            this.grbNpc.ResumeLayout(false);
            this.grbNpc.PerformLayout();
            this.grbMonster.ResumeLayout(false);
            this.grbMonster.PerformLayout();
            this.grbAddStory.ResumeLayout(false);
            this.grbAddStory.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listNPC;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.TextBox tbxSpeed;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxSprite;
        private System.Windows.Forms.ComboBox cbxTypes;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.PictureBox rawSprite;
        private System.Windows.Forms.CheckBox cbxMovable;
        private System.Windows.Forms.ListView listStory;
        private System.Windows.Forms.GroupBox grbNpc;
        private System.Windows.Forms.Button btnStoryAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grbMonster;
        private System.Windows.Forms.Button btnSkillAdd;
        private System.Windows.Forms.ListView lvwSkill;
        private System.Windows.Forms.TextBox tbxHp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxDamage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxExp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbxLvl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnItems;
        private System.Windows.Forms.ListView lvwItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grbAddStory;
        private System.Windows.Forms.RichTextBox tbxStory;
        private System.Windows.Forms.Button btnASExit;
        private System.Windows.Forms.Button btnASSave;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxTitleStory;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxTypeStory;
        private System.Windows.Forms.Button btnRmStory;
        private System.Windows.Forms.RichTextBox tbxDesc;
        private System.Windows.Forms.Label label11;
    }
}