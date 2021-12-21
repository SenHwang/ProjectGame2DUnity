
namespace Server.FormChild
{
    partial class ItemManager
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
            this.listItems = new System.Windows.Forms.ListView();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tbxDesc = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxIcon = new System.Windows.Forms.ComboBox();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxAnim = new System.Windows.Forms.ComboBox();
            this.picAnimation = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxSound = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tbxGenderReq = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbxMaximumInBag = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbxClassReq = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbxLvReq = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbxPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbxStackable = new System.Windows.Forms.CheckBox();
            this.cbxEquipSkillBar = new System.Windows.Forms.CheckBox();
            this.tbxSkillReq = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxRarity = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxIsReusable = new System.Windows.Forms.CheckBox();
            this.tbxCastSkill = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbxAddExp = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbxAddMp = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbxAddHp = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.grbStatAdd = new System.Windows.Forms.GroupBox();
            this.tbxStatAdd = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbxStat = new System.Windows.Forms.ComboBox();
            this.picEquip = new System.Windows.Forms.PictureBox();
            this.cbxEquip = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cbxSlot = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnimation)).BeginInit();
            this.grbStatAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEquip)).BeginInit();
            this.SuspendLayout();
            // 
            // listItems
            // 
            this.listItems.FullRowSelect = true;
            this.listItems.HideSelection = false;
            this.listItems.Location = new System.Drawing.Point(12, 12);
            this.listItems.Name = "listItems";
            this.listItems.Size = new System.Drawing.Size(163, 288);
            this.listItems.TabIndex = 2;
            this.listItems.UseCompatibleStateImageBehavior = false;
            this.listItems.View = System.Windows.Forms.View.Tile;
            this.listItems.SelectedIndexChanged += new System.EventHandler(this.listItems_SelectedIndexChanged);
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(234, 12);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(121, 20);
            this.tbxName.TabIndex = 8;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(193, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 7;
            this.lblName.Text = "Name";
            // 
            // tbxDesc
            // 
            this.tbxDesc.Location = new System.Drawing.Point(234, 38);
            this.tbxDesc.Name = "tbxDesc";
            this.tbxDesc.Size = new System.Drawing.Size(216, 58);
            this.tbxDesc.TabIndex = 15;
            this.tbxDesc.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Mô tả Item";
            // 
            // cbxIcon
            // 
            this.cbxIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxIcon.FormattingEnabled = true;
            this.cbxIcon.Location = new System.Drawing.Point(399, 12);
            this.cbxIcon.Name = "cbxIcon";
            this.cbxIcon.Size = new System.Drawing.Size(50, 21);
            this.cbxIcon.TabIndex = 19;
            this.cbxIcon.SelectedIndexChanged += new System.EventHandler(this.OnSpriteChange);
            // 
            // picIcon
            // 
            this.picIcon.Location = new System.Drawing.Point(456, 7);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(72, 39);
            this.picIcon.TabIndex = 18;
            this.picIcon.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Icon";
            // 
            // cbxAnim
            // 
            this.cbxAnim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAnim.FormattingEnabled = true;
            this.cbxAnim.Location = new System.Drawing.Point(551, 25);
            this.cbxAnim.Name = "cbxAnim";
            this.cbxAnim.Size = new System.Drawing.Size(50, 21);
            this.cbxAnim.TabIndex = 22;
            this.cbxAnim.SelectedIndexChanged += new System.EventHandler(this.OnSpriteChange);
            // 
            // picAnimation
            // 
            this.picAnimation.Location = new System.Drawing.Point(607, 7);
            this.picAnimation.Name = "picAnimation";
            this.picAnimation.Size = new System.Drawing.Size(89, 77);
            this.picAnimation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAnimation.TabIndex = 21;
            this.picAnimation.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(560, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Anim";
            // 
            // tbxSound
            // 
            this.tbxSound.Location = new System.Drawing.Point(496, 52);
            this.tbxSound.Name = "tbxSound";
            this.tbxSound.Size = new System.Drawing.Size(44, 20);
            this.tbxSound.TabIndex = 55;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(455, 55);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(38, 13);
            this.label27.TabIndex = 54;
            this.label27.Text = "Sound";
            // 
            // tbxGenderReq
            // 
            this.tbxGenderReq.Location = new System.Drawing.Point(346, 130);
            this.tbxGenderReq.Name = "tbxGenderReq";
            this.tbxGenderReq.Size = new System.Drawing.Size(44, 20);
            this.tbxGenderReq.TabIndex = 67;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(281, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 66;
            this.label10.Text = "Gender Req";
            // 
            // tbxMaximumInBag
            // 
            this.tbxMaximumInBag.Location = new System.Drawing.Point(484, 104);
            this.tbxMaximumInBag.Name = "tbxMaximumInBag";
            this.tbxMaximumInBag.Size = new System.Drawing.Size(44, 20);
            this.tbxMaximumInBag.TabIndex = 65;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(396, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 64;
            this.label9.Text = "Maximum in bag";
            // 
            // tbxClassReq
            // 
            this.tbxClassReq.Location = new System.Drawing.Point(346, 104);
            this.tbxClassReq.Name = "tbxClassReq";
            this.tbxClassReq.Size = new System.Drawing.Size(44, 20);
            this.tbxClassReq.TabIndex = 63;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(288, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 62;
            this.label8.Text = "Class Req";
            // 
            // tbxLvReq
            // 
            this.tbxLvReq.Location = new System.Drawing.Point(233, 130);
            this.tbxLvReq.Name = "tbxLvReq";
            this.tbxLvReq.Size = new System.Drawing.Size(44, 20);
            this.tbxLvReq.TabIndex = 61;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(183, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "LV Req";
            // 
            // tbxPrice
            // 
            this.tbxPrice.Location = new System.Drawing.Point(233, 104);
            this.tbxPrice.Name = "tbxPrice";
            this.tbxPrice.Size = new System.Drawing.Size(44, 20);
            this.tbxPrice.TabIndex = 57;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(183, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Price";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(291, 274);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 68;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbxStackable
            // 
            this.cbxStackable.AutoSize = true;
            this.cbxStackable.Location = new System.Drawing.Point(456, 78);
            this.cbxStackable.Name = "cbxStackable";
            this.cbxStackable.Size = new System.Drawing.Size(74, 17);
            this.cbxStackable.TabIndex = 70;
            this.cbxStackable.Text = "Stackable";
            this.cbxStackable.UseVisualStyleBackColor = true;
            // 
            // cbxEquipSkillBar
            // 
            this.cbxEquipSkillBar.AutoSize = true;
            this.cbxEquipSkillBar.Location = new System.Drawing.Point(487, 132);
            this.cbxEquipSkillBar.Name = "cbxEquipSkillBar";
            this.cbxEquipSkillBar.Size = new System.Drawing.Size(94, 17);
            this.cbxEquipSkillBar.TabIndex = 71;
            this.cbxEquipSkillBar.Text = "Equip Skill Bar";
            this.cbxEquipSkillBar.UseVisualStyleBackColor = true;
            // 
            // tbxSkillReq
            // 
            this.tbxSkillReq.Location = new System.Drawing.Point(437, 130);
            this.tbxSkillReq.Name = "tbxSkillReq";
            this.tbxSkillReq.Size = new System.Drawing.Size(44, 20);
            this.tbxSkillReq.TabIndex = 73;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(393, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "Skill req";
            // 
            // cbxRarity
            // 
            this.cbxRarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRarity.FormattingEnabled = true;
            this.cbxRarity.Location = new System.Drawing.Point(595, 103);
            this.cbxRarity.Name = "cbxRarity";
            this.cbxRarity.Size = new System.Drawing.Size(100, 21);
            this.cbxRarity.TabIndex = 74;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(538, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 75;
            this.label6.Text = "Phân loại";
            // 
            // cbxIsReusable
            // 
            this.cbxIsReusable.AutoSize = true;
            this.cbxIsReusable.Location = new System.Drawing.Point(579, 132);
            this.cbxIsReusable.Name = "cbxIsReusable";
            this.cbxIsReusable.Size = new System.Drawing.Size(121, 17);
            this.cbxIsReusable.TabIndex = 76;
            this.cbxIsReusable.Text = "Không mất khi dùng";
            this.cbxIsReusable.UseVisualStyleBackColor = true;
            // 
            // tbxCastSkill
            // 
            this.tbxCastSkill.Location = new System.Drawing.Point(619, 155);
            this.tbxCastSkill.Name = "tbxCastSkill";
            this.tbxCastSkill.Size = new System.Drawing.Size(44, 20);
            this.tbxCastSkill.TabIndex = 78;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(563, 158);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 13);
            this.label11.TabIndex = 77;
            this.label11.Text = "Cast Skill";
            // 
            // tbxAddExp
            // 
            this.tbxAddExp.Location = new System.Drawing.Point(435, 158);
            this.tbxAddExp.Name = "tbxAddExp";
            this.tbxAddExp.Size = new System.Drawing.Size(44, 20);
            this.tbxAddExp.TabIndex = 84;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(382, 161);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 13);
            this.label12.TabIndex = 83;
            this.label12.Text = "Add Exp";
            // 
            // tbxAddMp
            // 
            this.tbxAddMp.Location = new System.Drawing.Point(332, 158);
            this.tbxAddMp.Name = "tbxAddMp";
            this.tbxAddMp.Size = new System.Drawing.Size(44, 20);
            this.tbxAddMp.TabIndex = 82;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(282, 161);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 13);
            this.label13.TabIndex = 81;
            this.label13.Text = "Add Mp";
            // 
            // tbxAddHp
            // 
            this.tbxAddHp.Location = new System.Drawing.Point(233, 158);
            this.tbxAddHp.Name = "tbxAddHp";
            this.tbxAddHp.Size = new System.Drawing.Size(44, 20);
            this.tbxAddHp.TabIndex = 80;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(183, 161);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 79;
            this.label14.Text = "Add Hp";
            // 
            // grbStatAdd
            // 
            this.grbStatAdd.Controls.Add(this.tbxStatAdd);
            this.grbStatAdd.Controls.Add(this.label15);
            this.grbStatAdd.Controls.Add(this.cbxStat);
            this.grbStatAdd.Location = new System.Drawing.Point(186, 185);
            this.grbStatAdd.Name = "grbStatAdd";
            this.grbStatAdd.Size = new System.Drawing.Size(273, 68);
            this.grbStatAdd.TabIndex = 85;
            this.grbStatAdd.TabStop = false;
            this.grbStatAdd.Text = "Stat Add";
            // 
            // tbxStatAdd
            // 
            this.tbxStatAdd.Location = new System.Drawing.Point(153, 24);
            this.tbxStatAdd.Name = "tbxStatAdd";
            this.tbxStatAdd.Size = new System.Drawing.Size(101, 20);
            this.tbxStatAdd.TabIndex = 86;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 27);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 13);
            this.label15.TabIndex = 86;
            this.label15.Text = "Stat Add";
            // 
            // cbxStat
            // 
            this.cbxStat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStat.FormattingEnabled = true;
            this.cbxStat.Location = new System.Drawing.Point(75, 24);
            this.cbxStat.Name = "cbxStat";
            this.cbxStat.Size = new System.Drawing.Size(72, 21);
            this.cbxStat.TabIndex = 75;
            // 
            // picEquip
            // 
            this.picEquip.Location = new System.Drawing.Point(607, 192);
            this.picEquip.Name = "picEquip";
            this.picEquip.Size = new System.Drawing.Size(89, 77);
            this.picEquip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEquip.TabIndex = 86;
            this.picEquip.TabStop = false;
            // 
            // cbxEquip
            // 
            this.cbxEquip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEquip.FormattingEnabled = true;
            this.cbxEquip.Location = new System.Drawing.Point(551, 232);
            this.cbxEquip.Name = "cbxEquip";
            this.cbxEquip.Size = new System.Drawing.Size(50, 21);
            this.cbxEquip.TabIndex = 88;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(513, 216);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 13);
            this.label16.TabIndex = 87;
            this.label16.Text = "Sprite Equipment";
            // 
            // cbxSlot
            // 
            this.cbxSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSlot.FormattingEnabled = true;
            this.cbxSlot.Location = new System.Drawing.Point(541, 192);
            this.cbxSlot.Name = "cbxSlot";
            this.cbxSlot.Size = new System.Drawing.Size(60, 21);
            this.cbxSlot.TabIndex = 89;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(483, 195);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 13);
            this.label17.TabIndex = 90;
            this.label17.Text = "EquipSlot";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(372, 274);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 69;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(202, 274);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 91;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ItemManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 309);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cbxSlot);
            this.Controls.Add(this.cbxEquip);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.picEquip);
            this.Controls.Add(this.grbStatAdd);
            this.Controls.Add(this.tbxAddExp);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbxAddMp);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbxAddHp);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tbxCastSkill);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbxIsReusable);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbxRarity);
            this.Controls.Add(this.tbxSkillReq);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxEquipSkillBar);
            this.Controls.Add(this.cbxStackable);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbxGenderReq);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbxMaximumInBag);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbxClassReq);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbxLvReq);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbxPrice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbxSound);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.cbxAnim);
            this.Controls.Add(this.picAnimation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxIcon);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxDesc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.listItems);
            this.Name = "ItemManager";
            this.Text = "ItemManager";
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAnimation)).EndInit();
            this.grbStatAdd.ResumeLayout(false);
            this.grbStatAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEquip)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listItems;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.RichTextBox tbxDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxIcon;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxAnim;
        private System.Windows.Forms.PictureBox picAnimation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxSound;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox tbxGenderReq;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbxMaximumInBag;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbxClassReq;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbxLvReq;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbxPrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox cbxStackable;
        private System.Windows.Forms.CheckBox cbxEquipSkillBar;
        private System.Windows.Forms.TextBox tbxSkillReq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxRarity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbxIsReusable;
        private System.Windows.Forms.TextBox tbxCastSkill;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbxAddExp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbxAddMp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbxAddHp;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox grbStatAdd;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbxStat;
        private System.Windows.Forms.TextBox tbxStatAdd;
        private System.Windows.Forms.PictureBox picEquip;
        private System.Windows.Forms.ComboBox cbxEquip;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbxSlot;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
    }
}