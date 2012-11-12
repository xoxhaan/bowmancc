using System;

namespace TheBeastMasterTree
{
    partial class BeastGUI
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

        public static string label31Msg
        {
            get
            {
                return "[Aspect of the Cheetah]\r\n- Lets us run faster without getting dazed, increases mobility.\r\n[Aspect of" +
                    "the Pack]\r\n- Increases Packs range, useful as a group speed buff between combats.\r\n[Revive Pet]\r\n- Useful for reviving pet when you are under attack.";
            }
        }

        public static string label30Msg
        {
            get
            {
                return "[Marked for Death]\r\n- Applies Hunters Mark automatically, saving time thus increasing DPS.\r\n[Animal Bond]\r\n- Inc" +
    "reases healing taken, thus survivability.\r\n[Misdirection]\r\n- Let's us redirect threat to our Pet more often, increases survivability.\r\n[Dise" +
    "ngage]\r\n- Fun, although mostly useless talent.\r\n[Deterrence]\r\n- Useful for damage reduction, increases survivability.\r\n[Mending]\r\n- Incre" +
    "ases Pets survivability, good for Soloing.\r\n[Path Finding]\r\n- Increases running speed, thus Mobility.\r\n[Distracting Shot]\r\n- Distracts your" +
    " target to attack your Pet, increases survivability.";
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.StExplosive = new System.Windows.Forms.CheckBox();
            this.FreezingTD = new System.Windows.Forms.CheckBox();
            this.FreezingTL = new System.Windows.Forms.CheckBox();
            this.SnakeTD = new System.Windows.Forms.CheckBox();
            this.SnakeTL = new System.Windows.Forms.CheckBox();
            this.IceTD = new System.Windows.Forms.CheckBox();
            this.IceTL = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.BossHealth = new System.Windows.Forms.NumericUpDown();
            this.DetHealth = new System.Windows.Forms.NumericUpDown();
            this.Distract = new System.Windows.Forms.CheckBox();
            this.ScatterShot = new System.Windows.Forms.Label();
            this.ScatBox = new System.Windows.Forms.ComboBox();
            this.IntiBox = new System.Windows.Forms.ComboBox();
            this.Intimidate = new System.Windows.Forms.Label();
            this.SerBox = new System.Windows.Forms.ComboBox();
            this.Serpent = new System.Windows.Forms.Label();
            this.SSStacks = new System.Windows.Forms.Label();
            this.FFS = new System.Windows.Forms.NumericUpDown();
            this.FocusFire = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.FDBox = new System.Windows.Forms.ComboBox();
            this.AspectSwitching = new System.Windows.Forms.CheckBox();
            this.Arcane = new System.Windows.Forms.CheckBox();
            this.MDFocus = new System.Windows.Forms.CheckBox();
            this.KillCom = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.AoEDire = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Mobs = new System.Windows.Forms.NumericUpDown();
            this.Launcher = new System.Windows.Forms.CheckBox();
            this.AoELynx = new System.Windows.Forms.CheckBox();
            this.MultiShot = new System.Windows.Forms.CheckBox();
            this.HMark = new System.Windows.Forms.CheckBox();
            this.Concussive = new System.Windows.Forms.CheckBox();
            this.Deterrence = new System.Windows.Forms.CheckBox();
            this.KillShot = new System.Windows.Forms.CheckBox();
            this.BWrath = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.FocusShots = new System.Windows.Forms.NumericUpDown();
            this.SaveButton2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Belt = new System.Windows.Forms.CheckBox();
            this.LifeBlood = new System.Windows.Forms.CheckBox();
            this.Rapid = new System.Windows.Forms.CheckBox();
            this.Gloves = new System.Windows.Forms.CheckBox();
            this.CallWild = new System.Windows.Forms.CheckBox();
            this.Racial = new System.Windows.Forms.CheckBox();
            this.Trinket2 = new System.Windows.Forms.CheckBox();
            this.Trinket1 = new System.Windows.Forms.CheckBox();
            this.Readiness = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Rabid = new System.Windows.Forms.CheckBox();
            this.PetAtk = new System.Windows.Forms.CheckBox();
            this.Frost = new System.Windows.Forms.CheckBox();
            this.Burrow = new System.Windows.Forms.CheckBox();
            this.SpiritMend = new System.Windows.Forms.CheckBox();
            this.Misdirect = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.MendHealth = new System.Windows.Forms.NumericUpDown();
            this.PET = new System.Windows.Forms.NumericUpDown();
            this.CallPet = new System.Windows.Forms.CheckBox();
            this.MendPet = new System.Windows.Forms.CheckBox();
            this.RevivePet = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Use2 = new System.Windows.Forms.Button();
            this.Use1 = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.TL5_None = new System.Windows.Forms.RadioButton();
            this.TL5_Barrage = new System.Windows.Forms.RadioButton();
            this.TL5_Glaive = new System.Windows.Forms.RadioButton();
            this.TL5_Power = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.TL2_None = new System.Windows.Forms.RadioButton();
            this.TL2_SpiritBond = new System.Windows.Forms.RadioButton();
            this.TL2_Exhilaration = new System.Windows.Forms.RadioButton();
            this.TL2_IronHawk = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TL1_None = new System.Windows.Forms.RadioButton();
            this.TL1_Binding = new System.Windows.Forms.RadioButton();
            this.TL1_Silence = new System.Windows.Forms.RadioButton();
            this.label16 = new System.Windows.Forms.Label();
            this.TL1_Wyvern = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TL3_None = new System.Windows.Forms.RadioButton();
            this.TL3_Thrill = new System.Windows.Forms.RadioButton();
            this.TL3_Fervor = new System.Windows.Forms.RadioButton();
            this.TL3_Dire = new System.Windows.Forms.RadioButton();
            this.label17 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TL4_None = new System.Windows.Forms.RadioButton();
            this.TL4_Lynx = new System.Windows.Forms.RadioButton();
            this.TL4_Crows = new System.Windows.Forms.RadioButton();
            this.TL4_Blink = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label37 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.SaveButton3 = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.VirmensBypass = new System.Windows.Forms.CheckBox();
            this.VirmenHealth = new System.Windows.Forms.NumericUpDown();
            this.Virmen = new System.Windows.Forms.CheckBox();
            this.ItemsHealth = new System.Windows.Forms.NumericUpDown();
            this.label32 = new System.Windows.Forms.Label();
            this.AlchRejuv = new System.Windows.Forms.CheckBox();
            this.HealingPot = new System.Windows.Forms.CheckBox();
            this.LifeSpirit = new System.Windows.Forms.CheckBox();
            this.HealthStone = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DSNOR = new System.Windows.Forms.RadioButton();
            this.DSHC = new System.Windows.Forms.RadioButton();
            this.Party = new System.Windows.Forms.RadioButton();
            this.DSLFR = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label38 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BossHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FFS)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Mobs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FocusShots)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MendHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PET)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VirmenHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsHealth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HealthStone)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 11);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(501, 497);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.linkLabel2);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(493, 471);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Beast Master";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox8);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.BossHealth);
            this.groupBox6.Controls.Add(this.DetHealth);
            this.groupBox6.Controls.Add(this.Distract);
            this.groupBox6.Controls.Add(this.ScatterShot);
            this.groupBox6.Controls.Add(this.ScatBox);
            this.groupBox6.Controls.Add(this.IntiBox);
            this.groupBox6.Controls.Add(this.Intimidate);
            this.groupBox6.Controls.Add(this.SerBox);
            this.groupBox6.Controls.Add(this.Serpent);
            this.groupBox6.Controls.Add(this.SSStacks);
            this.groupBox6.Controls.Add(this.FFS);
            this.groupBox6.Controls.Add(this.FocusFire);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.FDBox);
            this.groupBox6.Controls.Add(this.AspectSwitching);
            this.groupBox6.Controls.Add(this.Arcane);
            this.groupBox6.Controls.Add(this.MDFocus);
            this.groupBox6.Controls.Add(this.KillCom);
            this.groupBox6.Controls.Add(this.groupBox4);
            this.groupBox6.Controls.Add(this.HMark);
            this.groupBox6.Controls.Add(this.Concussive);
            this.groupBox6.Controls.Add(this.Deterrence);
            this.groupBox6.Controls.Add(this.KillShot);
            this.groupBox6.Controls.Add(this.BWrath);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.FocusShots);
            this.groupBox6.Controls.Add(this.SaveButton2);
            this.groupBox6.Location = new System.Drawing.Point(153, 34);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(334, 431);
            this.groupBox6.TabIndex = 15;
            this.groupBox6.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.StExplosive);
            this.groupBox8.Controls.Add(this.FreezingTD);
            this.groupBox8.Controls.Add(this.FreezingTL);
            this.groupBox8.Controls.Add(this.SnakeTD);
            this.groupBox8.Controls.Add(this.SnakeTL);
            this.groupBox8.Controls.Add(this.IceTD);
            this.groupBox8.Controls.Add(this.IceTL);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(10, 269);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(176, 156);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Trap options";
            // 
            // StExplosive
            // 
            this.StExplosive.AutoSize = true;
            this.StExplosive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StExplosive.Location = new System.Drawing.Point(6, 20);
            this.StExplosive.Name = "StExplosive";
            this.StExplosive.Size = new System.Drawing.Size(162, 17);
            this.StExplosive.TabIndex = 39;
            this.StExplosive.Text = "Single Target Explosive Trap";
            this.StExplosive.UseVisualStyleBackColor = true;
            this.StExplosive.CheckedChanged += new System.EventHandler(this.StExplosive_CheckedChanged);
            // 
            // FreezingTD
            // 
            this.FreezingTD.AutoSize = true;
            this.FreezingTD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FreezingTD.Location = new System.Drawing.Point(6, 132);
            this.FreezingTD.Name = "FreezingTD";
            this.FreezingTD.Size = new System.Drawing.Size(117, 17);
            this.FreezingTD.TabIndex = 6;
            this.FreezingTD.Text = "Freezing Trap Drop";
            this.FreezingTD.UseVisualStyleBackColor = true;
            this.FreezingTD.CheckedChanged += new System.EventHandler(this.FreezingTD_CheckedChanged);
            // 
            // FreezingTL
            // 
            this.FreezingTL.AutoSize = true;
            this.FreezingTL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FreezingTL.Location = new System.Drawing.Point(6, 113);
            this.FreezingTL.Name = "FreezingTL";
            this.FreezingTL.Size = new System.Drawing.Size(139, 17);
            this.FreezingTL.TabIndex = 5;
            this.FreezingTL.Text = "Freezing Trap Launcher";
            this.FreezingTL.UseVisualStyleBackColor = true;
            this.FreezingTL.CheckedChanged += new System.EventHandler(this.FreezingTL_CheckedChanged);
            // 
            // SnakeTD
            // 
            this.SnakeTD.AutoSize = true;
            this.SnakeTD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SnakeTD.Location = new System.Drawing.Point(6, 94);
            this.SnakeTD.Name = "SnakeTD";
            this.SnakeTD.Size = new System.Drawing.Size(108, 17);
            this.SnakeTD.TabIndex = 4;
            this.SnakeTD.Text = "Snake Trap Drop";
            this.SnakeTD.UseVisualStyleBackColor = true;
            this.SnakeTD.CheckedChanged += new System.EventHandler(this.SnakeTD_CheckedChanged);
            // 
            // SnakeTL
            // 
            this.SnakeTL.AutoSize = true;
            this.SnakeTL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SnakeTL.Location = new System.Drawing.Point(6, 76);
            this.SnakeTL.Name = "SnakeTL";
            this.SnakeTL.Size = new System.Drawing.Size(130, 17);
            this.SnakeTL.TabIndex = 3;
            this.SnakeTL.Text = "Snake Trap Launcher";
            this.SnakeTL.UseVisualStyleBackColor = true;
            this.SnakeTL.CheckedChanged += new System.EventHandler(this.SnakeTL_CheckedChanged);
            // 
            // IceTD
            // 
            this.IceTD.AutoSize = true;
            this.IceTD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IceTD.Location = new System.Drawing.Point(6, 57);
            this.IceTD.Name = "IceTD";
            this.IceTD.Size = new System.Drawing.Size(92, 17);
            this.IceTD.TabIndex = 2;
            this.IceTD.Text = "Ice Trap Drop";
            this.IceTD.UseVisualStyleBackColor = true;
            this.IceTD.CheckedChanged += new System.EventHandler(this.IceTD_CheckedChanged);
            // 
            // IceTL
            // 
            this.IceTL.AutoSize = true;
            this.IceTL.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IceTL.Location = new System.Drawing.Point(6, 38);
            this.IceTL.Name = "IceTL";
            this.IceTL.Size = new System.Drawing.Size(114, 17);
            this.IceTL.TabIndex = 1;
            this.IceTL.Text = "Ice Trap Launcher";
            this.IceTL.UseVisualStyleBackColor = true;
            this.IceTL.CheckedChanged += new System.EventHandler(this.IceTL_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(150, 39);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 13);
            this.label12.TabIndex = 41;
            this.label12.Text = "* 100000 (100k) ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 40;
            this.label11.Text = "Boss Health > ";
            // 
            // BossHealth
            // 
            this.BossHealth.Location = new System.Drawing.Point(94, 35);
            this.BossHealth.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.BossHealth.Name = "BossHealth";
            this.BossHealth.Size = new System.Drawing.Size(52, 20);
            this.BossHealth.TabIndex = 39;
            this.BossHealth.ValueChanged += new System.EventHandler(this.BossHealth_ValueChanged);
            // 
            // DetHealth
            // 
            this.DetHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetHealth.Location = new System.Drawing.Point(249, 60);
            this.DetHealth.Name = "DetHealth";
            this.DetHealth.Size = new System.Drawing.Size(52, 20);
            this.DetHealth.TabIndex = 30;
            this.DetHealth.ValueChanged += new System.EventHandler(this.DetHealth_ValueChanged);
            // 
            // Distract
            // 
            this.Distract.AutoSize = true;
            this.Distract.Location = new System.Drawing.Point(17, 165);
            this.Distract.Name = "Distract";
            this.Distract.Size = new System.Drawing.Size(101, 17);
            this.Distract.TabIndex = 37;
            this.Distract.Text = "Distracting Shot";
            this.Distract.UseVisualStyleBackColor = true;
            this.Distract.CheckedChanged += new System.EventHandler(this.Distract_CheckedChanged);
            // 
            // ScatterShot
            // 
            this.ScatterShot.AutoSize = true;
            this.ScatterShot.Location = new System.Drawing.Point(141, 227);
            this.ScatterShot.Name = "ScatterShot";
            this.ScatterShot.Size = new System.Drawing.Size(69, 13);
            this.ScatterShot.TabIndex = 36;
            this.ScatterShot.Text = "Scatter Shot:";
            // 
            // ScatBox
            // 
            this.ScatBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScatBox.DropDownWidth = 95;
            this.ScatBox.FormattingEnabled = true;
            this.ScatBox.Location = new System.Drawing.Point(144, 243);
            this.ScatBox.Name = "ScatBox";
            this.ScatBox.Size = new System.Drawing.Size(98, 21);
            this.ScatBox.TabIndex = 35;
            this.ScatBox.SelectedIndexChanged += new System.EventHandler(this.ScatBox_SelectedIndexChanged);
            // 
            // IntiBox
            // 
            this.IntiBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IntiBox.DropDownWidth = 95;
            this.IntiBox.FormattingEnabled = true;
            this.IntiBox.Location = new System.Drawing.Point(17, 242);
            this.IntiBox.Name = "IntiBox";
            this.IntiBox.Size = new System.Drawing.Size(113, 21);
            this.IntiBox.TabIndex = 34;
            this.IntiBox.SelectedIndexChanged += new System.EventHandler(this.IntiBox_SelectedIndexChanged);
            // 
            // Intimidate
            // 
            this.Intimidate.AutoSize = true;
            this.Intimidate.Location = new System.Drawing.Point(13, 226);
            this.Intimidate.Name = "Intimidate";
            this.Intimidate.Size = new System.Drawing.Size(55, 13);
            this.Intimidate.TabIndex = 33;
            this.Intimidate.Text = "Intimidate:";
            // 
            // SerBox
            // 
            this.SerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerBox.DropDownWidth = 95;
            this.SerBox.FormattingEnabled = true;
            this.SerBox.Location = new System.Drawing.Point(16, 201);
            this.SerBox.Name = "SerBox";
            this.SerBox.Size = new System.Drawing.Size(113, 21);
            this.SerBox.TabIndex = 32;
            this.SerBox.SelectedIndexChanged += new System.EventHandler(this.SerBox_SelectedIndexChanged);
            // 
            // Serpent
            // 
            this.Serpent.AutoSize = true;
            this.Serpent.Location = new System.Drawing.Point(13, 185);
            this.Serpent.Name = "Serpent";
            this.Serpent.Size = new System.Drawing.Size(74, 13);
            this.Serpent.TabIndex = 31;
            this.Serpent.Text = "Serpent Sting:";
            // 
            // SSStacks
            // 
            this.SSStacks.AutoSize = true;
            this.SSStacks.Location = new System.Drawing.Point(211, 147);
            this.SSStacks.Name = "SSStacks";
            this.SSStacks.Size = new System.Drawing.Size(43, 13);
            this.SSStacks.TabIndex = 28;
            this.SSStacks.Text = "Stacks:";
            // 
            // FFS
            // 
            this.FFS.Location = new System.Drawing.Point(256, 144);
            this.FFS.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.FFS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FFS.Name = "FFS";
            this.FFS.Size = new System.Drawing.Size(33, 20);
            this.FFS.TabIndex = 27;
            this.FFS.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FFS.ValueChanged += new System.EventHandler(this.FFS_ValueChanged);
            // 
            // FocusFire
            // 
            this.FocusFire.AutoSize = true;
            this.FocusFire.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FocusFire.Location = new System.Drawing.Point(142, 146);
            this.FocusFire.Name = "FocusFire";
            this.FocusFire.Size = new System.Drawing.Size(75, 17);
            this.FocusFire.TabIndex = 27;
            this.FocusFire.Text = "Focus Fire";
            this.FocusFire.UseVisualStyleBackColor = true;
            this.FocusFire.CheckedChanged += new System.EventHandler(this.FocusFire_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(141, 185);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Feign Death:";
            // 
            // FDBox
            // 
            this.FDBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FDBox.DropDownWidth = 95;
            this.FDBox.FormattingEnabled = true;
            this.FDBox.Location = new System.Drawing.Point(144, 201);
            this.FDBox.Name = "FDBox";
            this.FDBox.Size = new System.Drawing.Size(95, 21);
            this.FDBox.TabIndex = 25;
            this.FDBox.SelectedIndexChanged += new System.EventHandler(this.FDBox_SelectedIndexChanged);
            // 
            // AspectSwitching
            // 
            this.AspectSwitching.AutoSize = true;
            this.AspectSwitching.Location = new System.Drawing.Point(17, 60);
            this.AspectSwitching.Name = "AspectSwitching";
            this.AspectSwitching.Size = new System.Drawing.Size(106, 17);
            this.AspectSwitching.TabIndex = 24;
            this.AspectSwitching.Text = "Aspect switching";
            this.AspectSwitching.UseVisualStyleBackColor = true;
            this.AspectSwitching.CheckedChanged += new System.EventHandler(this.AspectSwitching_CheckedChanged);
            // 
            // Arcane
            // 
            this.Arcane.AutoSize = true;
            this.Arcane.Location = new System.Drawing.Point(142, 104);
            this.Arcane.Name = "Arcane";
            this.Arcane.Size = new System.Drawing.Size(85, 17);
            this.Arcane.TabIndex = 24;
            this.Arcane.Text = "Arcane Shot";
            this.Arcane.UseVisualStyleBackColor = true;
            this.Arcane.CheckedChanged += new System.EventHandler(this.Arcane_CheckedChanged);
            // 
            // MDFocus
            // 
            this.MDFocus.AutoSize = true;
            this.MDFocus.Location = new System.Drawing.Point(17, 123);
            this.MDFocus.Name = "MDFocus";
            this.MDFocus.Size = new System.Drawing.Size(115, 17);
            this.MDFocus.TabIndex = 24;
            this.MDFocus.Text = "Misdirect on Focus";
            this.MDFocus.UseVisualStyleBackColor = true;
            this.MDFocus.CheckedChanged += new System.EventHandler(this.MDFocus_CheckedChanged);
            // 
            // KillCom
            // 
            this.KillCom.AutoSize = true;
            this.KillCom.Location = new System.Drawing.Point(17, 144);
            this.KillCom.Name = "KillCom";
            this.KillCom.Size = new System.Drawing.Size(89, 17);
            this.KillCom.TabIndex = 24;
            this.KillCom.Text = "Kill Command";
            this.KillCom.UseVisualStyleBackColor = true;
            this.KillCom.CheckedChanged += new System.EventHandler(this.KillCom_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.AoEDire);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.Mobs);
            this.groupBox4.Controls.Add(this.Launcher);
            this.groupBox4.Controls.Add(this.AoELynx);
            this.groupBox4.Controls.Add(this.MultiShot);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(192, 269);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(135, 126);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "AoE options";
            // 
            // AoEDire
            // 
            this.AoEDire.AutoSize = true;
            this.AoEDire.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AoEDire.Location = new System.Drawing.Point(7, 84);
            this.AoEDire.Name = "AoEDire";
            this.AoEDire.Size = new System.Drawing.Size(75, 17);
            this.AoEDire.TabIndex = 4;
            this.AoEDire.Text = "Dire Beast";
            this.AoEDire.UseVisualStyleBackColor = true;
            this.AoEDire.CheckedChanged += new System.EventHandler(this.AoEDire_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(43, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "mobs";
            // 
            // Mobs
            // 
            this.Mobs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mobs.Location = new System.Drawing.Point(6, 18);
            this.Mobs.Name = "Mobs";
            this.Mobs.Size = new System.Drawing.Size(32, 20);
            this.Mobs.TabIndex = 2;
            this.Mobs.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.Mobs.ValueChanged += new System.EventHandler(this.Mobs_ValueChanged);
            // 
            // Launcher
            // 
            this.Launcher.AutoSize = true;
            this.Launcher.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Launcher.Location = new System.Drawing.Point(7, 64);
            this.Launcher.Name = "Launcher";
            this.Launcher.Size = new System.Drawing.Size(96, 17);
            this.Launcher.TabIndex = 1;
            this.Launcher.Text = "Explosive Trap";
            this.Launcher.UseVisualStyleBackColor = true;
            this.Launcher.CheckedChanged += new System.EventHandler(this.Launcher_CheckedChanged);
            // 
            // AoELynx
            // 
            this.AoELynx.AutoSize = true;
            this.AoELynx.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AoELynx.Location = new System.Drawing.Point(7, 104);
            this.AoELynx.Name = "AoELynx";
            this.AoELynx.Size = new System.Drawing.Size(76, 17);
            this.AoELynx.TabIndex = 1;
            this.AoELynx.Text = "Lynx Rush";
            this.AoELynx.UseVisualStyleBackColor = true;
            this.AoELynx.CheckedChanged += new System.EventHandler(this.AoELynx_CheckedChanged);
            // 
            // MultiShot
            // 
            this.MultiShot.AutoSize = true;
            this.MultiShot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MultiShot.Location = new System.Drawing.Point(7, 43);
            this.MultiShot.Name = "MultiShot";
            this.MultiShot.Size = new System.Drawing.Size(73, 17);
            this.MultiShot.TabIndex = 0;
            this.MultiShot.Text = "Multi-Shot";
            this.MultiShot.UseVisualStyleBackColor = true;
            this.MultiShot.CheckedChanged += new System.EventHandler(this.MultiShot_CheckedChanged);
            // 
            // HMark
            // 
            this.HMark.AutoSize = true;
            this.HMark.Location = new System.Drawing.Point(17, 102);
            this.HMark.Name = "HMark";
            this.HMark.Size = new System.Drawing.Size(92, 17);
            this.HMark.TabIndex = 24;
            this.HMark.Text = "Hunter\'s Mark";
            this.HMark.UseVisualStyleBackColor = true;
            this.HMark.CheckedChanged += new System.EventHandler(this.HMark_CheckedChanged);
            // 
            // Concussive
            // 
            this.Concussive.AutoSize = true;
            this.Concussive.Location = new System.Drawing.Point(17, 81);
            this.Concussive.Name = "Concussive";
            this.Concussive.Size = new System.Drawing.Size(106, 17);
            this.Concussive.TabIndex = 24;
            this.Concussive.Text = "Concussive Shot";
            this.Concussive.UseVisualStyleBackColor = true;
            this.Concussive.CheckedChanged += new System.EventHandler(this.Concussive_CheckedChanged);
            // 
            // Deterrence
            // 
            this.Deterrence.AutoSize = true;
            this.Deterrence.Location = new System.Drawing.Point(142, 61);
            this.Deterrence.Name = "Deterrence";
            this.Deterrence.Size = new System.Drawing.Size(105, 17);
            this.Deterrence.TabIndex = 24;
            this.Deterrence.Text = "Deterrence at %:";
            this.Deterrence.UseVisualStyleBackColor = true;
            this.Deterrence.CheckedChanged += new System.EventHandler(this.Deterrence_CheckedChanged);
            // 
            // KillShot
            // 
            this.KillShot.AutoSize = true;
            this.KillShot.Location = new System.Drawing.Point(142, 125);
            this.KillShot.Name = "KillShot";
            this.KillShot.Size = new System.Drawing.Size(64, 17);
            this.KillShot.TabIndex = 24;
            this.KillShot.Text = "Kill Shot";
            this.KillShot.UseVisualStyleBackColor = true;
            this.KillShot.CheckedChanged += new System.EventHandler(this.KillShot_CheckedChanged);
            // 
            // BWrath
            // 
            this.BWrath.AutoSize = true;
            this.BWrath.Location = new System.Drawing.Point(142, 82);
            this.BWrath.Name = "BWrath";
            this.BWrath.Size = new System.Drawing.Size(89, 17);
            this.BWrath.TabIndex = 24;
            this.BWrath.Text = "Bestial Wrath";
            this.BWrath.UseVisualStyleBackColor = true;
            this.BWrath.CheckedChanged += new System.EventHandler(this.BWrath_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(72, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(185, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "No Steady/Cobra shot over this focus";
            // 
            // FocusShots
            // 
            this.FocusShots.Location = new System.Drawing.Point(16, 13);
            this.FocusShots.Name = "FocusShots";
            this.FocusShots.Size = new System.Drawing.Size(52, 20);
            this.FocusShots.TabIndex = 22;
            this.FocusShots.ValueChanged += new System.EventHandler(this.FocusShots_ValueChanged);
            // 
            // SaveButton2
            // 
            this.SaveButton2.Location = new System.Drawing.Point(252, 401);
            this.SaveButton2.Name = "SaveButton2";
            this.SaveButton2.Size = new System.Drawing.Size(75, 23);
            this.SaveButton2.TabIndex = 12;
            this.SaveButton2.Text = "Save";
            this.SaveButton2.UseVisualStyleBackColor = true;
            this.SaveButton2.Click += new System.EventHandler(this.SaveButton2_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Candara", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Firebrick;
            this.label5.Location = new System.Drawing.Point(185, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(255, 39);
            this.label5.TabIndex = 14;
            this.label5.Text = "The Beast Master";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(243, 119);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(110, 13);
            this.linkLabel2.TabIndex = 13;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "+Survival Talent Build";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Belt);
            this.groupBox5.Controls.Add(this.LifeBlood);
            this.groupBox5.Controls.Add(this.Rapid);
            this.groupBox5.Controls.Add(this.Gloves);
            this.groupBox5.Controls.Add(this.CallWild);
            this.groupBox5.Controls.Add(this.Racial);
            this.groupBox5.Controls.Add(this.Trinket2);
            this.groupBox5.Controls.Add(this.Trinket1);
            this.groupBox5.Controls.Add(this.Readiness);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(11, 244);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(136, 221);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Cooldowns";
            // 
            // Belt
            // 
            this.Belt.AutoSize = true;
            this.Belt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Belt.Location = new System.Drawing.Point(7, 147);
            this.Belt.Name = "Belt";
            this.Belt.Size = new System.Drawing.Size(87, 17);
            this.Belt.TabIndex = 26;
            this.Belt.Text = "Belt Enchant";
            this.Belt.UseVisualStyleBackColor = true;
            this.Belt.CheckedChanged += new System.EventHandler(this.Belt_CheckedChanged);
            // 
            // LifeBlood
            // 
            this.LifeBlood.AutoSize = true;
            this.LifeBlood.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.LifeBlood.Location = new System.Drawing.Point(7, 82);
            this.LifeBlood.Name = "LifeBlood";
            this.LifeBlood.Size = new System.Drawing.Size(69, 17);
            this.LifeBlood.TabIndex = 5;
            this.LifeBlood.Text = "Lifeblood";
            this.LifeBlood.UseVisualStyleBackColor = true;
            this.LifeBlood.CheckedChanged += new System.EventHandler(this.LifeBlood_CheckedChanged);
            // 
            // Rapid
            // 
            this.Rapid.AutoSize = true;
            this.Rapid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Rapid.Location = new System.Drawing.Point(7, 61);
            this.Rapid.Name = "Rapid";
            this.Rapid.Size = new System.Drawing.Size(74, 17);
            this.Rapid.TabIndex = 4;
            this.Rapid.Text = "Rapid Fire";
            this.Rapid.UseVisualStyleBackColor = true;
            this.Rapid.CheckedChanged += new System.EventHandler(this.Rapid_CheckedChanged);
            // 
            // Gloves
            // 
            this.Gloves.AutoSize = true;
            this.Gloves.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Gloves.Location = new System.Drawing.Point(7, 125);
            this.Gloves.Name = "Gloves";
            this.Gloves.Size = new System.Drawing.Size(102, 17);
            this.Gloves.TabIndex = 3;
            this.Gloves.Text = "Gloves Enchant";
            this.Gloves.UseVisualStyleBackColor = true;
            this.Gloves.CheckedChanged += new System.EventHandler(this.Gloves_CheckedChanged);
            // 
            // CallWild
            // 
            this.CallWild.AutoSize = true;
            this.CallWild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CallWild.Location = new System.Drawing.Point(7, 189);
            this.CallWild.Name = "CallWild";
            this.CallWild.Size = new System.Drawing.Size(74, 17);
            this.CallWild.TabIndex = 25;
            this.CallWild.Text = "Stampede";
            this.CallWild.UseVisualStyleBackColor = true;
            this.CallWild.CheckedChanged += new System.EventHandler(this.CallWild_CheckedChanged);
            // 
            // Racial
            // 
            this.Racial.AutoSize = true;
            this.Racial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Racial.Location = new System.Drawing.Point(7, 103);
            this.Racial.Name = "Racial";
            this.Racial.Size = new System.Drawing.Size(78, 17);
            this.Racial.TabIndex = 2;
            this.Racial.Text = "Racial Skill";
            this.Racial.UseVisualStyleBackColor = true;
            this.Racial.CheckedChanged += new System.EventHandler(this.Racial_CheckedChanged);
            // 
            // Trinket2
            // 
            this.Trinket2.AutoSize = true;
            this.Trinket2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Trinket2.Location = new System.Drawing.Point(7, 40);
            this.Trinket2.Name = "Trinket2";
            this.Trinket2.Size = new System.Drawing.Size(68, 17);
            this.Trinket2.TabIndex = 1;
            this.Trinket2.Text = "Trinket 2";
            this.Trinket2.UseVisualStyleBackColor = true;
            this.Trinket2.CheckedChanged += new System.EventHandler(this.Trinket2_CheckedChanged);
            // 
            // Trinket1
            // 
            this.Trinket1.AutoSize = true;
            this.Trinket1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Trinket1.Location = new System.Drawing.Point(7, 19);
            this.Trinket1.Name = "Trinket1";
            this.Trinket1.Size = new System.Drawing.Size(68, 17);
            this.Trinket1.TabIndex = 0;
            this.Trinket1.Text = "Trinket 1";
            this.Trinket1.UseVisualStyleBackColor = true;
            this.Trinket1.CheckedChanged += new System.EventHandler(this.Trinket1_CheckedChanged);
            // 
            // Readiness
            // 
            this.Readiness.AutoSize = true;
            this.Readiness.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Readiness.Location = new System.Drawing.Point(7, 168);
            this.Readiness.Name = "Readiness";
            this.Readiness.Size = new System.Drawing.Size(76, 17);
            this.Readiness.TabIndex = 24;
            this.Readiness.Text = "Readiness";
            this.Readiness.UseVisualStyleBackColor = true;
            this.Readiness.CheckedChanged += new System.EventHandler(this.Readiness_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Rabid);
            this.groupBox3.Controls.Add(this.PetAtk);
            this.groupBox3.Controls.Add(this.Frost);
            this.groupBox3.Controls.Add(this.Burrow);
            this.groupBox3.Controls.Add(this.SpiritMend);
            this.groupBox3.Controls.Add(this.Misdirect);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.MendHealth);
            this.groupBox3.Controls.Add(this.PET);
            this.groupBox3.Controls.Add(this.CallPet);
            this.groupBox3.Controls.Add(this.MendPet);
            this.groupBox3.Controls.Add(this.RevivePet);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(11, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(136, 234);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pet options";
            // 
            // Rabid
            // 
            this.Rabid.AutoSize = true;
            this.Rabid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rabid.Location = new System.Drawing.Point(6, 202);
            this.Rabid.Name = "Rabid";
            this.Rabid.Size = new System.Drawing.Size(54, 17);
            this.Rabid.TabIndex = 31;
            this.Rabid.Text = "Rabid";
            this.Rabid.UseVisualStyleBackColor = true;
            this.Rabid.CheckedChanged += new System.EventHandler(this.Rabid_CheckedChanged);
            // 
            // PetAtk
            // 
            this.PetAtk.AutoSize = true;
            this.PetAtk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PetAtk.Location = new System.Drawing.Point(6, 181);
            this.PetAtk.Name = "PetAtk";
            this.PetAtk.Size = new System.Drawing.Size(128, 17);
            this.PetAtk.TabIndex = 30;
            this.PetAtk.Text = "Attack Current Target";
            this.PetAtk.UseVisualStyleBackColor = true;
            this.PetAtk.CheckedChanged += new System.EventHandler(this.PetAtk_CheckedChanged);
            // 
            // Frost
            // 
            this.Frost.AutoSize = true;
            this.Frost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Frost.Location = new System.Drawing.Point(6, 140);
            this.Frost.Name = "Frost";
            this.Frost.Size = new System.Drawing.Size(108, 17);
            this.Frost.TabIndex = 29;
            this.Frost.Text = "Froststorm Breath";
            this.Frost.UseVisualStyleBackColor = true;
            this.Frost.CheckedChanged += new System.EventHandler(this.Frost_CheckedChanged);
            // 
            // Burrow
            // 
            this.Burrow.AutoSize = true;
            this.Burrow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Burrow.Location = new System.Drawing.Point(6, 161);
            this.Burrow.Name = "Burrow";
            this.Burrow.Size = new System.Drawing.Size(93, 17);
            this.Burrow.TabIndex = 28;
            this.Burrow.Text = "Burrow Attack";
            this.Burrow.UseVisualStyleBackColor = true;
            this.Burrow.CheckedChanged += new System.EventHandler(this.Burrow_CheckedChanged);
            // 
            // SpiritMend
            // 
            this.SpiritMend.AutoSize = true;
            this.SpiritMend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpiritMend.Location = new System.Drawing.Point(6, 119);
            this.SpiritMend.Name = "SpiritMend";
            this.SpiritMend.Size = new System.Drawing.Size(79, 17);
            this.SpiritMend.TabIndex = 27;
            this.SpiritMend.Text = "Spirit Mend";
            this.SpiritMend.UseVisualStyleBackColor = true;
            this.SpiritMend.CheckedChanged += new System.EventHandler(this.SpiritMend_CheckedChanged);
            // 
            // Misdirect
            // 
            this.Misdirect.AutoSize = true;
            this.Misdirect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Misdirect.Location = new System.Drawing.Point(6, 99);
            this.Misdirect.Name = "Misdirect";
            this.Misdirect.Size = new System.Drawing.Size(102, 17);
            this.Misdirect.TabIndex = 26;
            this.Misdirect.Text = "Misdirect on Pet";
            this.Misdirect.CheckedChanged += new System.EventHandler(this.Misdirect_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Pet Health %";
            // 
            // MendHealth
            // 
            this.MendHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MendHealth.Location = new System.Drawing.Point(76, 78);
            this.MendHealth.Name = "MendHealth";
            this.MendHealth.Size = new System.Drawing.Size(52, 20);
            this.MendHealth.TabIndex = 23;
            this.MendHealth.ValueChanged += new System.EventHandler(this.MendHealth_ValueChanged);
            // 
            // PET
            // 
            this.PET.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PET.Location = new System.Drawing.Point(75, 19);
            this.PET.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.PET.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PET.Name = "PET";
            this.PET.Size = new System.Drawing.Size(33, 20);
            this.PET.TabIndex = 5;
            this.PET.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PET.ValueChanged += new System.EventHandler(this.PET_ValueChanged);
            // 
            // CallPet
            // 
            this.CallPet.AutoSize = true;
            this.CallPet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.CallPet.Location = new System.Drawing.Point(6, 20);
            this.CallPet.Name = "CallPet";
            this.CallPet.Size = new System.Drawing.Size(62, 17);
            this.CallPet.TabIndex = 4;
            this.CallPet.Text = "Call Pet";
            this.CallPet.UseVisualStyleBackColor = true;
            this.CallPet.CheckedChanged += new System.EventHandler(this.CallPet_CheckedChanged);
            // 
            // MendPet
            // 
            this.MendPet.AutoSize = true;
            this.MendPet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MendPet.Location = new System.Drawing.Point(6, 60);
            this.MendPet.Name = "MendPet";
            this.MendPet.Size = new System.Drawing.Size(72, 17);
            this.MendPet.TabIndex = 2;
            this.MendPet.Text = "Mend Pet";
            this.MendPet.UseVisualStyleBackColor = true;
            this.MendPet.CheckedChanged += new System.EventHandler(this.MendPet_CheckedChanged);
            // 
            // RevivePet
            // 
            this.RevivePet.AutoSize = true;
            this.RevivePet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RevivePet.Location = new System.Drawing.Point(6, 40);
            this.RevivePet.Name = "RevivePet";
            this.RevivePet.Size = new System.Drawing.Size(79, 17);
            this.RevivePet.TabIndex = 1;
            this.RevivePet.Text = "Revive Pet";
            this.RevivePet.UseVisualStyleBackColor = true;
            this.RevivePet.CheckedChanged += new System.EventHandler(this.RevivePet_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(641, 359);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.Use2);
            this.tabPage1.Controls.Add(this.Use1);
            this.tabPage1.Controls.Add(this.label22);
            this.tabPage1.Controls.Add(this.label31);
            this.tabPage1.Controls.Add(this.label30);
            this.tabPage1.Controls.Add(this.label29);
            this.tabPage1.Controls.Add(this.label28);
            this.tabPage1.Controls.Add(this.label27);
            this.tabPage1.Controls.Add(this.label26);
            this.tabPage1.Controls.Add(this.label25);
            this.tabPage1.Controls.Add(this.label24);
            this.tabPage1.Controls.Add(this.label23);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.SaveButton);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(493, 471);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Talents";
            // 
            // Use2
            // 
            this.Use2.Location = new System.Drawing.Point(284, 403);
            this.Use2.Name = "Use2";
            this.Use2.Size = new System.Drawing.Size(60, 23);
            this.Use2.TabIndex = 51;
            this.Use2.Text = "Use";
            this.Use2.UseVisualStyleBackColor = true;
            this.Use2.Click += new System.EventHandler(this.Use2_Click);
            // 
            // Use1
            // 
            this.Use1.Location = new System.Drawing.Point(206, 403);
            this.Use1.Name = "Use1";
            this.Use1.Size = new System.Drawing.Size(60, 23);
            this.Use1.TabIndex = 50;
            this.Use1.Text = "Use";
            this.Use1.UseVisualStyleBackColor = true;
            this.Use1.Click += new System.EventHandler(this.Use1_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label22.Location = new System.Drawing.Point(367, 348);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(111, 30);
            this.label22.TabIndex = 49;
            this.label22.Text = "If none mentioned, \r\nuse preferred.";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.LemonChiffon;
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label31.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(97, 370);
            this.label31.Name = "label31";
            this.label31.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label31.Size = new System.Drawing.Size(59, 27);
            this.label31.TabIndex = 40;
            this.label31.Text = "Minor";
            this.toolTip1.SetToolTip(this.label31, label31Msg);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Khaki;
            this.label30.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label30.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(31, 370);
            this.label30.Name = "label30";
            this.label30.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label30.Size = new System.Drawing.Size(59, 27);
            this.label30.TabIndex = 39;
            this.label30.Text = "Major";
            this.toolTip1.SetToolTip(this.label30, label30Msg);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.SteelBlue;
            this.label29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label29.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(358, 288);
            this.label29.Name = "label29";
            this.label29.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label29.Size = new System.Drawing.Size(59, 27);
            this.label29.TabIndex = 38;
            this.label29.Text = "LVL90";
            this.toolTip1.SetToolTip(this.label29, "[Glaive Toss]\r\n- Best DPS, great for AoE, doesn\'t waste time casting.\r\n[Power Sho" +
        "t]\r\n- Long cast makes this undesireable.\r\n[Barrage]\r\n- Channels, Good for Burst," +
        " can be used while moving (Fox)");
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label28.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(291, 288);
            this.label28.Name = "label28";
            this.label28.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label28.Size = new System.Drawing.Size(59, 27);
            this.label28.TabIndex = 37;
            this.label28.Text = "LVL75";
            this.toolTip1.SetToolTip(this.label28, "[A Murder of Crows]\r\n- Best on long duration fights (Raid bosses).\r\n[Blink Strike" +
        "]\r\n- Great for Pet control, useful in soloing.\r\n[Lynx Rush]\r\n- Great for fast bu" +
        "rst of DPS, useful in AoE.");
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.SkyBlue;
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label27.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(224, 288);
            this.label27.Name = "label27";
            this.label27.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label27.Size = new System.Drawing.Size(59, 27);
            this.label27.TabIndex = 36;
            this.label27.Text = "LVL60";
            this.toolTip1.SetToolTip(this.label27, "[Fervor]\r\n- High and fast Focus Regen.\r\n[Dire Beast]\r\n- Direct additional DPS, lo" +
        "w focus Regen.\r\n[Thrill of the Hunt]\r\n- Useful in AoE Situations.");
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.PowderBlue;
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label26.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(158, 288);
            this.label26.Name = "label26";
            this.label26.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label26.Size = new System.Drawing.Size(59, 27);
            this.label26.TabIndex = 35;
            this.label26.Text = "LVL45";
            this.toolTip1.SetToolTip(this.label26, "[Exhilaration]\r\n- A fast 30% Heal (good for PvP).\r\n[Aspect of the Iron Hawk]\r\n- B" +
        "est in high damage situations (Raid Bosses).\r\n[Spirit Bond]\r\n- Heals you and pet" +
        " Passively (Soloing)");
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.PaleTurquoise;
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label25.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(92, 288);
            this.label25.Name = "label25";
            this.label25.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label25.Size = new System.Drawing.Size(59, 27);
            this.label25.TabIndex = 34;
            this.label25.Text = "LVL30";
            this.toolTip1.SetToolTip(this.label25, "[Silencing Shot]\r\n- Useful is a Silence is required.\r\n[Wyvern Sting]\r\n- Useuful i" +
        "f an extra Crowd Control is required.\r\n[Binding Shot]\r\n- Useful for controlling " +
        "Adds.");
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.LightCyan;
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label24.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(26, 288);
            this.label24.Name = "label24";
            this.label24.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label24.Size = new System.Drawing.Size(59, 27);
            this.label24.TabIndex = 33;
            this.label24.Text = "LVL15";
            this.toolTip1.SetToolTip(this.label24, "[Posthaste]\r\n- Useful when you need to move somewhere fast.\r\n[Narrow Escape]\r\n- U" +
        "seful for controlling Adds.\r\n[Crouching Tiger, Hidden Chimera]\r\n- Useful if you " +
        "need to deterrence often.");
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label23.Location = new System.Drawing.Point(23, 342);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(150, 23);
            this.label23.TabIndex = 31;
            this.label23.Text = "Glyphs Explained:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label20.Location = new System.Drawing.Point(194, 342);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(162, 23);
            this.label20.TabIndex = 29;
            this.label20.Text = "Recommendations:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.PaleGreen;
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(279, 369);
            this.label19.Name = "label19";
            this.label19.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label19.Size = new System.Drawing.Size(68, 27);
            this.label19.TabIndex = 28;
            this.label19.Text = "Soloing";
            this.toolTip1.SetToolTip(this.label19, "[Talents]\r\n- [Spirit Bond]\r\n- [Fervor]\r\n- [Blink Strike]\r\n- [Glaive Toss]\r\n [Glyp" +
        "hs]\r\n- [Marked for Death]\r\n- [Misdirection]");
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label14.Location = new System.Drawing.Point(174, 266);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(118, 15);
            this.label14.TabIndex = 27;
            this.label14.Text = "Mouse over for info.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(20, 261);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 23);
            this.label7.TabIndex = 26;
            this.label7.Text = "Talents Explained:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.LightCoral;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(201, 369);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.label10.Size = new System.Drawing.Size(70, 27);
            this.label10.TabIndex = 24;
            this.label10.Text = "Raiding";
            this.toolTip1.SetToolTip(this.label10, "[Talents]\r\n -[Binding Shot]\r\n- [Iron Hawk]\r\n- [Fervor]\r\n- [A Murder of Crows]\r\n- " +
        "[Glaive Toss]\r\n [Glyphs]\r\n- [Marked for Death]\r\n- [Animal Bond]");
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(413, 442);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 7;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel6);
            this.groupBox2.Controls.Add(this.panel5);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.panel4);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(24, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(439, 233);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Talents";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.TL5_None);
            this.panel6.Controls.Add(this.TL5_Barrage);
            this.panel6.Controls.Add(this.TL5_Glaive);
            this.panel6.Controls.Add(this.TL5_Power);
            this.panel6.Controls.Add(this.label3);
            this.panel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel6.Location = new System.Drawing.Point(8, 191);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(422, 37);
            this.panel6.TabIndex = 38;
            // 
            // TL5_None
            // 
            this.TL5_None.AutoSize = true;
            this.TL5_None.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL5_None.Checked = true;
            this.TL5_None.Location = new System.Drawing.Point(3, 3);
            this.TL5_None.Name = "TL5_None";
            this.TL5_None.Size = new System.Drawing.Size(37, 30);
            this.TL5_None.TabIndex = 34;
            this.TL5_None.TabStop = true;
            this.TL5_None.Text = "None";
            this.TL5_None.UseVisualStyleBackColor = true;
            this.TL5_None.CheckedChanged += new System.EventHandler(this.TL5_None_CheckedChanged);
            // 
            // TL5_Barrage
            // 
            this.TL5_Barrage.AutoSize = true;
            this.TL5_Barrage.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL5_Barrage.Location = new System.Drawing.Point(350, 3);
            this.TL5_Barrage.Name = "TL5_Barrage";
            this.TL5_Barrage.Size = new System.Drawing.Size(48, 30);
            this.TL5_Barrage.TabIndex = 36;
            this.TL5_Barrage.TabStop = true;
            this.TL5_Barrage.Text = "Barrage";
            this.TL5_Barrage.UseVisualStyleBackColor = true;
            this.TL5_Barrage.CheckedChanged += new System.EventHandler(this.TL5_Barrage_CheckedChanged);
            // 
            // TL5_Glaive
            // 
            this.TL5_Glaive.AutoSize = true;
            this.TL5_Glaive.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL5_Glaive.Location = new System.Drawing.Point(96, 3);
            this.TL5_Glaive.Name = "TL5_Glaive";
            this.TL5_Glaive.Size = new System.Drawing.Size(67, 30);
            this.TL5_Glaive.TabIndex = 35;
            this.TL5_Glaive.TabStop = true;
            this.TL5_Glaive.Text = "Glaive Toss";
            this.TL5_Glaive.UseVisualStyleBackColor = true;
            this.TL5_Glaive.CheckedChanged += new System.EventHandler(this.TL5_Glaive_CheckedChanged);
            // 
            // TL5_Power
            // 
            this.TL5_Power.AutoSize = true;
            this.TL5_Power.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL5_Power.Location = new System.Drawing.Point(227, 3);
            this.TL5_Power.Name = "TL5_Power";
            this.TL5_Power.Size = new System.Drawing.Size(61, 30);
            this.TL5_Power.TabIndex = 34;
            this.TL5_Power.TabStop = true;
            this.TL5_Power.Text = "Powershot";
            this.TL5_Power.UseVisualStyleBackColor = true;
            this.TL5_Power.CheckedChanged += new System.EventHandler(this.TL5_Power_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, -16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(391, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "________________________________________________________________";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.TL2_None);
            this.panel5.Controls.Add(this.TL2_SpiritBond);
            this.panel5.Controls.Add(this.TL2_Exhilaration);
            this.panel5.Controls.Add(this.TL2_IronHawk);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(8, 61);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(422, 37);
            this.panel5.TabIndex = 37;
            // 
            // TL2_None
            // 
            this.TL2_None.AutoSize = true;
            this.TL2_None.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL2_None.Checked = true;
            this.TL2_None.Location = new System.Drawing.Point(3, 3);
            this.TL2_None.Name = "TL2_None";
            this.TL2_None.Size = new System.Drawing.Size(37, 30);
            this.TL2_None.TabIndex = 34;
            this.TL2_None.TabStop = true;
            this.TL2_None.Text = "None";
            this.TL2_None.UseVisualStyleBackColor = true;
            this.TL2_None.CheckedChanged += new System.EventHandler(this.TL2_None_CheckedChanged);
            // 
            // TL2_SpiritBond
            // 
            this.TL2_SpiritBond.AutoSize = true;
            this.TL2_SpiritBond.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL2_SpiritBond.Location = new System.Drawing.Point(344, 3);
            this.TL2_SpiritBond.Name = "TL2_SpiritBond";
            this.TL2_SpiritBond.Size = new System.Drawing.Size(62, 30);
            this.TL2_SpiritBond.TabIndex = 36;
            this.TL2_SpiritBond.TabStop = true;
            this.TL2_SpiritBond.Text = "Spirit Bond";
            this.TL2_SpiritBond.UseVisualStyleBackColor = true;
            this.TL2_SpiritBond.CheckedChanged += new System.EventHandler(this.TL2_SpiritBond_CheckedChanged);
            // 
            // TL2_Exhilaration
            // 
            this.TL2_Exhilaration.AutoSize = true;
            this.TL2_Exhilaration.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL2_Exhilaration.Location = new System.Drawing.Point(97, 3);
            this.TL2_Exhilaration.Name = "TL2_Exhilaration";
            this.TL2_Exhilaration.Size = new System.Drawing.Size(65, 30);
            this.TL2_Exhilaration.TabIndex = 35;
            this.TL2_Exhilaration.TabStop = true;
            this.TL2_Exhilaration.Text = "Exhilaration";
            this.TL2_Exhilaration.UseVisualStyleBackColor = true;
            this.TL2_Exhilaration.CheckedChanged += new System.EventHandler(this.TL2_Exhilaration_CheckedChanged);
            // 
            // TL2_IronHawk
            // 
            this.TL2_IronHawk.AutoSize = true;
            this.TL2_IronHawk.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL2_IronHawk.Location = new System.Drawing.Point(192, 3);
            this.TL2_IronHawk.Name = "TL2_IronHawk";
            this.TL2_IronHawk.Size = new System.Drawing.Size(132, 30);
            this.TL2_IronHawk.TabIndex = 34;
            this.TL2_IronHawk.TabStop = true;
            this.TL2_IronHawk.Text = "Aspect Of The Iron Hawk";
            this.TL2_IronHawk.UseVisualStyleBackColor = true;
            this.TL2_IronHawk.CheckedChanged += new System.EventHandler(this.TL2_IronHawk_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, -16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(391, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "________________________________________________________________";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TL1_None);
            this.panel2.Controls.Add(this.TL1_Binding);
            this.panel2.Controls.Add(this.TL1_Silence);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.TL1_Wyvern);
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(8, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(422, 36);
            this.panel2.TabIndex = 21;
            // 
            // TL1_None
            // 
            this.TL1_None.AutoSize = true;
            this.TL1_None.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL1_None.Checked = true;
            this.TL1_None.Location = new System.Drawing.Point(3, 2);
            this.TL1_None.Name = "TL1_None";
            this.TL1_None.Size = new System.Drawing.Size(37, 30);
            this.TL1_None.TabIndex = 27;
            this.TL1_None.TabStop = true;
            this.TL1_None.Text = "None";
            this.TL1_None.UseVisualStyleBackColor = true;
            this.TL1_None.CheckedChanged += new System.EventHandler(this.TL1_None_CheckedChanged);
            // 
            // TL1_Binding
            // 
            this.TL1_Binding.AutoSize = true;
            this.TL1_Binding.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL1_Binding.Location = new System.Drawing.Point(340, 3);
            this.TL1_Binding.Name = "TL1_Binding";
            this.TL1_Binding.Size = new System.Drawing.Size(71, 30);
            this.TL1_Binding.TabIndex = 20;
            this.TL1_Binding.TabStop = true;
            this.TL1_Binding.Text = "Binding Shot";
            this.TL1_Binding.UseVisualStyleBackColor = true;
            this.TL1_Binding.CheckedChanged += new System.EventHandler(this.TL1_Binding_CheckedChanged);
            // 
            // TL1_Silence
            // 
            this.TL1_Silence.AutoSize = true;
            this.TL1_Silence.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL1_Silence.Location = new System.Drawing.Point(91, 3);
            this.TL1_Silence.Name = "TL1_Silence";
            this.TL1_Silence.Size = new System.Drawing.Size(79, 30);
            this.TL1_Silence.TabIndex = 18;
            this.TL1_Silence.TabStop = true;
            this.TL1_Silence.Text = "Silencing Shot";
            this.TL1_Silence.UseVisualStyleBackColor = true;
            this.TL1_Silence.CheckedChanged += new System.EventHandler(this.TL1_Silence_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(-3, -16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(391, 13);
            this.label16.TabIndex = 14;
            this.label16.Text = "________________________________________________________________";
            // 
            // TL1_Wyvern
            // 
            this.TL1_Wyvern.AutoSize = true;
            this.TL1_Wyvern.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL1_Wyvern.Location = new System.Drawing.Point(221, 3);
            this.TL1_Wyvern.Name = "TL1_Wyvern";
            this.TL1_Wyvern.Size = new System.Drawing.Size(75, 30);
            this.TL1_Wyvern.TabIndex = 17;
            this.TL1_Wyvern.TabStop = true;
            this.TL1_Wyvern.Text = "Wyvern Sting";
            this.TL1_Wyvern.UseVisualStyleBackColor = true;
            this.TL1_Wyvern.CheckedChanged += new System.EventHandler(this.TL1_Wyvern_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.TL3_None);
            this.panel3.Controls.Add(this.TL3_Thrill);
            this.panel3.Controls.Add(this.TL3_Fervor);
            this.panel3.Controls.Add(this.TL3_Dire);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(8, 104);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(422, 38);
            this.panel3.TabIndex = 28;
            // 
            // TL3_None
            // 
            this.TL3_None.AutoSize = true;
            this.TL3_None.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL3_None.Checked = true;
            this.TL3_None.Location = new System.Drawing.Point(3, 3);
            this.TL3_None.Name = "TL3_None";
            this.TL3_None.Size = new System.Drawing.Size(37, 30);
            this.TL3_None.TabIndex = 28;
            this.TL3_None.TabStop = true;
            this.TL3_None.Text = "None";
            this.TL3_None.UseVisualStyleBackColor = true;
            this.TL3_None.CheckedChanged += new System.EventHandler(this.TL3_None_CheckedChanged);
            // 
            // TL3_Thrill
            // 
            this.TL3_Thrill.AutoSize = true;
            this.TL3_Thrill.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL3_Thrill.Location = new System.Drawing.Point(330, 3);
            this.TL3_Thrill.Name = "TL3_Thrill";
            this.TL3_Thrill.Size = new System.Drawing.Size(89, 30);
            this.TL3_Thrill.TabIndex = 33;
            this.TL3_Thrill.TabStop = true;
            this.TL3_Thrill.Text = "Thrill of the Hunt";
            this.TL3_Thrill.UseVisualStyleBackColor = true;
            this.TL3_Thrill.CheckedChanged += new System.EventHandler(this.TL3_Thrill_CheckedChanged);
            // 
            // TL3_Fervor
            // 
            this.TL3_Fervor.AutoSize = true;
            this.TL3_Fervor.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL3_Fervor.Location = new System.Drawing.Point(110, 3);
            this.TL3_Fervor.Name = "TL3_Fervor";
            this.TL3_Fervor.Size = new System.Drawing.Size(41, 30);
            this.TL3_Fervor.TabIndex = 32;
            this.TL3_Fervor.TabStop = true;
            this.TL3_Fervor.Text = "Fervor";
            this.TL3_Fervor.UseVisualStyleBackColor = true;
            this.TL3_Fervor.CheckedChanged += new System.EventHandler(this.TL3_Fervor_CheckedChanged);
            // 
            // TL3_Dire
            // 
            this.TL3_Dire.AutoSize = true;
            this.TL3_Dire.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL3_Dire.Location = new System.Drawing.Point(229, 3);
            this.TL3_Dire.Name = "TL3_Dire";
            this.TL3_Dire.Size = new System.Drawing.Size(60, 30);
            this.TL3_Dire.TabIndex = 31;
            this.TL3_Dire.TabStop = true;
            this.TL3_Dire.Text = "Dire Beast";
            this.TL3_Dire.UseVisualStyleBackColor = true;
            this.TL3_Dire.CheckedChanged += new System.EventHandler(this.TL3_Dire_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(-3, -16);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(391, 13);
            this.label17.TabIndex = 14;
            this.label17.Text = "________________________________________________________________";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.TL4_None);
            this.panel4.Controls.Add(this.TL4_Lynx);
            this.panel4.Controls.Add(this.TL4_Crows);
            this.panel4.Controls.Add(this.TL4_Blink);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(8, 148);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(422, 37);
            this.panel4.TabIndex = 34;
            // 
            // TL4_None
            // 
            this.TL4_None.AutoSize = true;
            this.TL4_None.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL4_None.Checked = true;
            this.TL4_None.Location = new System.Drawing.Point(3, 3);
            this.TL4_None.Name = "TL4_None";
            this.TL4_None.Size = new System.Drawing.Size(37, 30);
            this.TL4_None.TabIndex = 34;
            this.TL4_None.TabStop = true;
            this.TL4_None.Text = "None";
            this.TL4_None.UseVisualStyleBackColor = true;
            this.TL4_None.CheckedChanged += new System.EventHandler(this.TL4_None_CheckedChanged);
            // 
            // TL4_Lynx
            // 
            this.TL4_Lynx.AutoSize = true;
            this.TL4_Lynx.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL4_Lynx.Location = new System.Drawing.Point(344, 3);
            this.TL4_Lynx.Name = "TL4_Lynx";
            this.TL4_Lynx.Size = new System.Drawing.Size(61, 30);
            this.TL4_Lynx.TabIndex = 36;
            this.TL4_Lynx.TabStop = true;
            this.TL4_Lynx.Text = "Lynx Rush";
            this.TL4_Lynx.UseVisualStyleBackColor = true;
            this.TL4_Lynx.CheckedChanged += new System.EventHandler(this.TL4_Lynx_CheckedChanged);
            // 
            // TL4_Crows
            // 
            this.TL4_Crows.AutoSize = true;
            this.TL4_Crows.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL4_Crows.Location = new System.Drawing.Point(82, 3);
            this.TL4_Crows.Name = "TL4_Crows";
            this.TL4_Crows.Size = new System.Drawing.Size(98, 30);
            this.TL4_Crows.TabIndex = 35;
            this.TL4_Crows.TabStop = true;
            this.TL4_Crows.Text = "A Murder of Crows";
            this.TL4_Crows.UseVisualStyleBackColor = true;
            this.TL4_Crows.CheckedChanged += new System.EventHandler(this.TL4_Crows_CheckedChanged);
            // 
            // TL4_Blink
            // 
            this.TL4_Blink.AutoSize = true;
            this.TL4_Blink.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.TL4_Blink.Location = new System.Drawing.Point(227, 3);
            this.TL4_Blink.Name = "TL4_Blink";
            this.TL4_Blink.Size = new System.Drawing.Size(64, 30);
            this.TL4_Blink.TabIndex = 34;
            this.TL4_Blink.TabStop = true;
            this.TL4_Blink.Text = "Blink Strike";
            this.TL4_Blink.UseVisualStyleBackColor = true;
            this.TL4_Blink.CheckedChanged += new System.EventHandler(this.TL4_Blink_CheckedChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(-3, -16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(391, 13);
            this.label18.TabIndex = 14;
            this.label18.Text = "________________________________________________________________";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.label38);
            this.tabPage3.Controls.Add(this.label37);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label36);
            this.tabPage3.Controls.Add(this.label35);
            this.tabPage3.Controls.Add(this.label34);
            this.tabPage3.Controls.Add(this.label33);
            this.tabPage3.Controls.Add(this.SaveButton3);
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.linkLabel5);
            this.tabPage3.Controls.Add(this.linkLabel1);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(493, 471);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Misc";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label37.Location = new System.Drawing.Point(198, 156);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(286, 30);
            this.label37.TabIndex = 57;
            this.label37.Text = "<- Will skip checking if we have Bloodlust/Heroism\r\n(Only checks if target is Bos" +
    "s and for Health)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label6.Location = new System.Drawing.Point(150, 281);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(293, 15);
            this.label6.TabIndex = 56;
            this.label6.Text = "^ Will use extrabutton actions in Dragon Soul Raid ^";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label36.Location = new System.Drawing.Point(19, 319);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(114, 23);
            this.label36.TabIndex = 55;
            this.label36.Text = "Other Notes:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label35.Location = new System.Drawing.Point(200, 27);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(263, 15);
            this.label35.TabIndex = 54;
            this.label35.Text = "<- Set to 0 if you don\'t want to use Health Stone";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label34.Location = new System.Drawing.Point(200, 95);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(209, 30);
            this.label34.TabIndex = 53;
            this.label34.Text = "<- Your health must be below to use \r\nLife Spirit / Alch. Rejuv. / Healing Pot.";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label33.Location = new System.Drawing.Point(200, 131);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(285, 15);
            this.label33.TabIndex = 52;
            this.label33.Text = "<- Target Health must be below to use Virmen\'s Bite";
            // 
            // SaveButton3
            // 
            this.SaveButton3.Location = new System.Drawing.Point(413, 441);
            this.SaveButton3.Name = "SaveButton3";
            this.SaveButton3.Size = new System.Drawing.Size(75, 23);
            this.SaveButton3.TabIndex = 51;
            this.SaveButton3.Text = "Save";
            this.SaveButton3.UseVisualStyleBackColor = true;
            this.SaveButton3.Click += new System.EventHandler(this.SaveButton3_Click_1);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label21.Location = new System.Drawing.Point(30, 347);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(358, 45);
            this.label21.TabIndex = 50;
            this.label21.Text = "Setting your Custom Lag Tolerance to around 250-400 \r\nwill help the bot to queue " +
    "up spells.\r\n(Go to Interface > Combat, to change your Custom Lag Tolerance)";
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linkLabel5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel5.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel5.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLabel5.Location = new System.Drawing.Point(289, 440);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(118, 21);
            this.linkLabel5.TabIndex = 49;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "Submit feedback";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.SystemColors.Highlight;
            this.linkLabel1.Location = new System.Drawing.Point(172, 432);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(119, 33);
            this.linkLabel1.TabIndex = 47;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "FallDown";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(5, 437);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 26);
            this.label4.TabIndex = 46;
            this.label4.Text = "A Custom Class by:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.VirmensBypass);
            this.groupBox7.Controls.Add(this.VirmenHealth);
            this.groupBox7.Controls.Add(this.Virmen);
            this.groupBox7.Controls.Add(this.ItemsHealth);
            this.groupBox7.Controls.Add(this.label32);
            this.groupBox7.Controls.Add(this.AlchRejuv);
            this.groupBox7.Controls.Add(this.HealingPot);
            this.groupBox7.Controls.Add(this.LifeSpirit);
            this.groupBox7.Controls.Add(this.HealthStone);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(13, 11);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(181, 175);
            this.groupBox7.TabIndex = 45;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Items";
            // 
            // VirmensBypass
            // 
            this.VirmensBypass.AutoSize = true;
            this.VirmensBypass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VirmensBypass.Location = new System.Drawing.Point(21, 137);
            this.VirmensBypass.Name = "VirmensBypass";
            this.VirmensBypass.Size = new System.Drawing.Size(146, 30);
            this.VirmensBypass.TabIndex = 50;
            this.VirmensBypass.Text = "Bypass Virmen\'s Bite \r\nBloodlust/Heroism Check";
            this.VirmensBypass.UseVisualStyleBackColor = true;
            this.VirmensBypass.CheckedChanged += new System.EventHandler(this.VirmensBypass_CheckedChanged);
            // 
            // VirmenHealth
            // 
            this.VirmenHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VirmenHealth.Location = new System.Drawing.Point(118, 114);
            this.VirmenHealth.Name = "VirmenHealth";
            this.VirmenHealth.Size = new System.Drawing.Size(52, 20);
            this.VirmenHealth.TabIndex = 49;
            this.VirmenHealth.ValueChanged += new System.EventHandler(this.VirmenHealth_ValueChanged);
            // 
            // Virmen
            // 
            this.Virmen.AutoSize = true;
            this.Virmen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Virmen.Location = new System.Drawing.Point(8, 115);
            this.Virmen.Name = "Virmen";
            this.Virmen.Size = new System.Drawing.Size(112, 17);
            this.Virmen.TabIndex = 48;
            this.Virmen.Text = "Virmen\'s Bite at %:";
            this.Virmen.UseVisualStyleBackColor = true;
            this.Virmen.CheckedChanged += new System.EventHandler(this.Virmen_CheckedChanged);
            // 
            // ItemsHealth
            // 
            this.ItemsHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemsHealth.Location = new System.Drawing.Point(87, 88);
            this.ItemsHealth.Name = "ItemsHealth";
            this.ItemsHealth.Size = new System.Drawing.Size(52, 20);
            this.ItemsHealth.TabIndex = 46;
            this.ItemsHealth.ValueChanged += new System.EventHandler(this.ItemsHealth_ValueChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(7, 91);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(76, 13);
            this.label32.TabIndex = 47;
            this.label32.Text = "Use items at %";
            // 
            // AlchRejuv
            // 
            this.AlchRejuv.AutoSize = true;
            this.AlchRejuv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AlchRejuv.Location = new System.Drawing.Point(8, 50);
            this.AlchRejuv.Name = "AlchRejuv";
            this.AlchRejuv.Size = new System.Drawing.Size(144, 17);
            this.AlchRejuv.TabIndex = 45;
            this.AlchRejuv.Text = "Alchemist\'s Rejuvenation";
            this.AlchRejuv.UseVisualStyleBackColor = true;
            this.AlchRejuv.CheckedChanged += new System.EventHandler(this.AlchRejuv_CheckedChanged);
            // 
            // HealingPot
            // 
            this.HealingPot.AutoSize = true;
            this.HealingPot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HealingPot.Location = new System.Drawing.Point(8, 69);
            this.HealingPot.Name = "HealingPot";
            this.HealingPot.Size = new System.Drawing.Size(130, 17);
            this.HealingPot.TabIndex = 44;
            this.HealingPot.Text = "Master Healing Potion";
            this.HealingPot.UseVisualStyleBackColor = true;
            this.HealingPot.CheckedChanged += new System.EventHandler(this.HealingPot_CheckedChanged);
            // 
            // LifeSpirit
            // 
            this.LifeSpirit.AutoSize = true;
            this.LifeSpirit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LifeSpirit.Location = new System.Drawing.Point(8, 32);
            this.LifeSpirit.Name = "LifeSpirit";
            this.LifeSpirit.Size = new System.Drawing.Size(69, 17);
            this.LifeSpirit.TabIndex = 5;
            this.LifeSpirit.Text = "Life Spirit";
            this.LifeSpirit.UseVisualStyleBackColor = true;
            this.LifeSpirit.CheckedChanged += new System.EventHandler(this.LifeSpirit_CheckedChanged);
            // 
            // HealthStone
            // 
            this.HealthStone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HealthStone.Location = new System.Drawing.Point(124, 14);
            this.HealthStone.Name = "HealthStone";
            this.HealthStone.Size = new System.Drawing.Size(52, 20);
            this.HealthStone.TabIndex = 42;
            this.HealthStone.ValueChanged += new System.EventHandler(this.HealthStone_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(5, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(114, 13);
            this.label15.TabIndex = 43;
            this.label15.Text = "Use Health Stone at %";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(30, 212);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 66);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Raids and Dungeons";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DSNOR);
            this.panel1.Controls.Add(this.DSHC);
            this.panel1.Controls.Add(this.Party);
            this.panel1.Controls.Add(this.DSLFR);
            this.panel1.Location = new System.Drawing.Point(7, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 41);
            this.panel1.TabIndex = 15;
            // 
            // DSNOR
            // 
            this.DSNOR.AutoSize = true;
            this.DSNOR.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.DSNOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DSNOR.Location = new System.Drawing.Point(211, 3);
            this.DSNOR.Name = "DSNOR";
            this.DSNOR.Size = new System.Drawing.Size(97, 30);
            this.DSNOR.TabIndex = 20;
            this.DSNOR.TabStop = true;
            this.DSNOR.Text = "Dragon Soul NOR";
            this.DSNOR.UseVisualStyleBackColor = true;
            // 
            // DSHC
            // 
            this.DSHC.AutoSize = true;
            this.DSHC.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.DSHC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DSHC.Location = new System.Drawing.Point(331, 3);
            this.DSHC.Name = "DSHC";
            this.DSHC.Size = new System.Drawing.Size(88, 30);
            this.DSHC.TabIndex = 19;
            this.DSHC.TabStop = true;
            this.DSHC.Text = "Dragon Soul HC";
            this.DSHC.UseVisualStyleBackColor = true;
            // 
            // Party
            // 
            this.Party.AutoSize = true;
            this.Party.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Party.Checked = true;
            this.Party.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Party.Location = new System.Drawing.Point(3, 3);
            this.Party.Name = "Party";
            this.Party.Size = new System.Drawing.Size(61, 30);
            this.Party.TabIndex = 18;
            this.Party.TabStop = true;
            this.Party.Text = "Party/Solo";
            this.Party.UseVisualStyleBackColor = true;
            // 
            // DSLFR
            // 
            this.DSLFR.AutoSize = true;
            this.DSLFR.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.DSLFR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DSLFR.Location = new System.Drawing.Point(93, 3);
            this.DSLFR.Name = "DSLFR";
            this.DSLFR.Size = new System.Drawing.Size(93, 30);
            this.DSLFR.TabIndex = 17;
            this.DSLFR.TabStop = true;
            this.DSLFR.Text = "Dragon Soul LFR";
            this.DSLFR.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 30000;
            this.toolTip1.InitialDelay = 50;
            this.toolTip1.ReshowDelay = 0;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label38.Location = new System.Drawing.Point(31, 408);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(380, 15);
            this.label38.TabIndex = 58;
            this.label38.Text = "Agility > Expertise (7.5%) = Ranged Hit (7.5%) > Crit > Haste > Mastery";
            // 
            // BeastGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(524, 520);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "BeastGUI";
            this.Text = "The Beast Master";
            this.Load += new System.EventHandler(this.BeastForm1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BossHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DetHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FFS)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Mobs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FocusShots)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MendHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PET)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VirmenHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsHealth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HealthStone)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton TL1_Binding;
        private System.Windows.Forms.RadioButton TL1_Silence;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RadioButton TL1_Wyvern;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton TL4_None;
        private System.Windows.Forms.RadioButton TL4_Lynx;
        private System.Windows.Forms.RadioButton TL4_Crows;
        private System.Windows.Forms.RadioButton TL4_Blink;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton TL1_None;
        private System.Windows.Forms.RadioButton TL3_None;
        private System.Windows.Forms.RadioButton TL3_Thrill;
        private System.Windows.Forms.RadioButton TL3_Fervor;
        private System.Windows.Forms.RadioButton TL3_Dire;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton TL2_None;
        private System.Windows.Forms.RadioButton TL2_SpiritBond;
        private System.Windows.Forms.RadioButton TL2_Exhilaration;
        private System.Windows.Forms.RadioButton TL2_IronHawk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton TL5_None;
        private System.Windows.Forms.RadioButton TL5_Barrage;
        private System.Windows.Forms.RadioButton TL5_Glaive;
        private System.Windows.Forms.RadioButton TL5_Power;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown BossHealth;
        private System.Windows.Forms.NumericUpDown DetHealth;
        private System.Windows.Forms.CheckBox Distract;
        private System.Windows.Forms.Label ScatterShot;
        private System.Windows.Forms.ComboBox ScatBox;
        private System.Windows.Forms.ComboBox IntiBox;
        private System.Windows.Forms.Label Intimidate;
        private System.Windows.Forms.ComboBox SerBox;
        private System.Windows.Forms.Label Serpent;
        private System.Windows.Forms.Label SSStacks;
        private System.Windows.Forms.NumericUpDown FFS;
        private System.Windows.Forms.CheckBox FocusFire;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox FDBox;
        private System.Windows.Forms.CheckBox AspectSwitching;
        private System.Windows.Forms.CheckBox Arcane;
        private System.Windows.Forms.CheckBox MDFocus;
        private System.Windows.Forms.CheckBox KillCom;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox AoEDire;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown Mobs;
        private System.Windows.Forms.CheckBox Launcher;
        private System.Windows.Forms.CheckBox AoELynx;
        private System.Windows.Forms.CheckBox MultiShot;
        private System.Windows.Forms.CheckBox HMark;
        private System.Windows.Forms.CheckBox Concussive;
        private System.Windows.Forms.CheckBox Deterrence;
        private System.Windows.Forms.CheckBox KillShot;
        private System.Windows.Forms.CheckBox BWrath;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown FocusShots;
        private System.Windows.Forms.Button SaveButton2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox Belt;
        private System.Windows.Forms.CheckBox LifeBlood;
        private System.Windows.Forms.CheckBox Rapid;
        private System.Windows.Forms.CheckBox Gloves;
        private System.Windows.Forms.CheckBox CallWild;
        private System.Windows.Forms.CheckBox Racial;
        private System.Windows.Forms.CheckBox Trinket2;
        private System.Windows.Forms.CheckBox Trinket1;
        private System.Windows.Forms.CheckBox Readiness;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox Rabid;
        private System.Windows.Forms.CheckBox PetAtk;
        private System.Windows.Forms.CheckBox Frost;
        private System.Windows.Forms.CheckBox Burrow;
        private System.Windows.Forms.CheckBox SpiritMend;
        private System.Windows.Forms.CheckBox Misdirect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown MendHealth;
        private System.Windows.Forms.NumericUpDown PET;
        private System.Windows.Forms.CheckBox CallPet;
        private System.Windows.Forms.CheckBox MendPet;
        private System.Windows.Forms.CheckBox RevivePet;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown VirmenHealth;
        private System.Windows.Forms.CheckBox Virmen;
        private System.Windows.Forms.NumericUpDown ItemsHealth;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.CheckBox AlchRejuv;
        private System.Windows.Forms.CheckBox HealingPot;
        private System.Windows.Forms.CheckBox LifeSpirit;
        private System.Windows.Forms.NumericUpDown HealthStone;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton DSNOR;
        private System.Windows.Forms.RadioButton DSHC;
        private System.Windows.Forms.RadioButton Party;
        private System.Windows.Forms.RadioButton DSLFR;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button SaveButton3;
        private System.Windows.Forms.Button Use2;
        private System.Windows.Forms.Button Use1;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox FreezingTD;
        private System.Windows.Forms.CheckBox FreezingTL;
        private System.Windows.Forms.CheckBox SnakeTD;
        private System.Windows.Forms.CheckBox SnakeTL;
        private System.Windows.Forms.CheckBox IceTD;
        private System.Windows.Forms.CheckBox IceTL;
        private System.Windows.Forms.CheckBox StExplosive;
        private System.Windows.Forms.CheckBox VirmensBypass;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
    }
}

