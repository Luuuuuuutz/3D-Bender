namespace _3D_Bender
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ContextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.genGCodeButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metricToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.benderConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialConnectButton = new System.Windows.Forms.Button();
            this.jogPosXButton = new System.Windows.Forms.Button();
            this.jogNegXButton = new System.Windows.Forms.Button();
            this.jogPosYButton = new System.Windows.Forms.Button();
            this.jogNegYButton = new System.Windows.Forms.Button();
            this.eStopButton = new System.Windows.Forms.Button();
            this.jogNegBButton = new System.Windows.Forms.Button();
            this.jogPosBButton = new System.Windows.Forms.Button();
            this.jogPosAButton = new System.Windows.Forms.Button();
            this.jogNegAButton = new System.Windows.Forms.Button();
            this.jogPosZButton = new System.Windows.Forms.Button();
            this.jogNegZButton = new System.Windows.Forms.Button();
            this.homeXButton = new System.Windows.Forms.Button();
            this.homeYButton = new System.Windows.Forms.Button();
            this.homeZButton = new System.Windows.Forms.Button();
            this.homeAButton = new System.Windows.Forms.Button();
            this.homeBButton = new System.Windows.Forms.Button();
            this.homeAllButton = new System.Windows.Forms.Button();
            this.actualPosXTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.actualPosYTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.actualPosZTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.actualPosATextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.actualPosBTextBox = new System.Windows.Forms.TextBox();
            this.moveToPosXButton = new System.Windows.Forms.Button();
            this.moveToPosYButton = new System.Windows.Forms.Button();
            this.moveToPosZButton = new System.Windows.Forms.Button();
            this.moveToPosAButton = new System.Windows.Forms.Button();
            this.moveToPosBButton = new System.Windows.Forms.Button();
            this.setZeroXButton = new System.Windows.Forms.Button();
            this.setZeroYButton = new System.Windows.Forms.Button();
            this.setZeroZButton = new System.Windows.Forms.Button();
            this.setZeroAButton = new System.Windows.Forms.Button();
            this.setZeroBButton = new System.Windows.Forms.Button();
            this.positionControlGroupBox = new System.Windows.Forms.GroupBox();
            this.commandPosBTextBox = new System.Windows.Forms.TextBox();
            this.commandPosATextBox = new System.Windows.Forms.TextBox();
            this.commandPosZTextBox = new System.Windows.Forms.TextBox();
            this.commandPosYTextBox = new System.Windows.Forms.TextBox();
            this.commandPosXTextBox = new System.Windows.Forms.TextBox();
            this.jogControlGroupBox = new System.Windows.Forms.GroupBox();
            this.buttonTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.COMPortComboBox = new System.Windows.Forms.ComboBox();
            this.refreshCOMPortsButton = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.homeAxesGroupBox = new System.Windows.Forms.GroupBox();
            this.totalMaterialTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.positionControlGroupBox.SuspendLayout();
            this.jogControlGroupBox.SuspendLayout();
            this.homeAxesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.ContextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(375, 385);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellEndEdit);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridView1_MouseDown);
            // 
            // ContextMenuStrip1
            // 
            this.ContextMenuStrip1.Name = "contextMenuStrip1";
            this.ContextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // genGCodeButton
            // 
            this.genGCodeButton.Location = new System.Drawing.Point(420, 400);
            this.genGCodeButton.Name = "genGCodeButton";
            this.genGCodeButton.Size = new System.Drawing.Size(75, 38);
            this.genGCodeButton.TabIndex = 3;
            this.genGCodeButton.Text = "Generate G-Code";
            this.genGCodeButton.UseVisualStyleBackColor = true;
            this.genGCodeButton.Click += new System.EventHandler(this.Button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(420, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 363);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "3D Model";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1117, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.importToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.ImportToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unitsToolStripMenuItem,
            this.benderConfigToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Visible = false;
            // 
            // unitsToolStripMenuItem
            // 
            this.unitsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.metricToolStripMenuItem});
            this.unitsToolStripMenuItem.Name = "unitsToolStripMenuItem";
            this.unitsToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.unitsToolStripMenuItem.Text = "Units";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.CheckOnClick = true;
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.EnglishToolStripMenuItem_Click);
            // 
            // metricToolStripMenuItem
            // 
            this.metricToolStripMenuItem.Checked = true;
            this.metricToolStripMenuItem.CheckOnClick = true;
            this.metricToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.metricToolStripMenuItem.Enabled = false;
            this.metricToolStripMenuItem.Name = "metricToolStripMenuItem";
            this.metricToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.metricToolStripMenuItem.Text = "Metric";
            this.metricToolStripMenuItem.Click += new System.EventHandler(this.MetricToolStripMenuItem_Click);
            // 
            // benderConfigToolStripMenuItem
            // 
            this.benderConfigToolStripMenuItem.Name = "benderConfigToolStripMenuItem";
            this.benderConfigToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.benderConfigToolStripMenuItem.Text = "Bender Config";
            this.benderConfigToolStripMenuItem.Click += new System.EventHandler(this.benderConfigToolStripMenuItem_Click);
            // 
            // serialConnectButton
            // 
            this.serialConnectButton.Location = new System.Drawing.Point(1024, 415);
            this.serialConnectButton.Name = "serialConnectButton";
            this.serialConnectButton.Size = new System.Drawing.Size(75, 23);
            this.serialConnectButton.TabIndex = 6;
            this.serialConnectButton.Text = "Connect";
            this.serialConnectButton.UseVisualStyleBackColor = true;
            this.serialConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // jogPosXButton
            // 
            this.jogPosXButton.Location = new System.Drawing.Point(246, 59);
            this.jogPosXButton.Name = "jogPosXButton";
            this.jogPosXButton.Size = new System.Drawing.Size(55, 23);
            this.jogPosXButton.TabIndex = 7;
            this.jogPosXButton.Text = "X+";
            this.jogPosXButton.UseVisualStyleBackColor = true;
            this.jogPosXButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogPosXButton_MouseDown);
            this.jogPosXButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogPosXButton_MouseUp);
            // 
            // jogNegXButton
            // 
            this.jogNegXButton.Location = new System.Drawing.Point(185, 59);
            this.jogNegXButton.Name = "jogNegXButton";
            this.jogNegXButton.Size = new System.Drawing.Size(55, 23);
            this.jogNegXButton.TabIndex = 8;
            this.jogNegXButton.Text = "X-";
            this.jogNegXButton.UseVisualStyleBackColor = true;
            this.jogNegXButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogNegXButton_MouseDown);
            this.jogNegXButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogNegXButton_MouseUp);
            // 
            // jogPosYButton
            // 
            this.jogPosYButton.Location = new System.Drawing.Point(47, 30);
            this.jogPosYButton.Name = "jogPosYButton";
            this.jogPosYButton.Size = new System.Drawing.Size(55, 23);
            this.jogPosYButton.TabIndex = 9;
            this.jogPosYButton.Text = "Y+";
            this.jogPosYButton.UseVisualStyleBackColor = true;
            this.jogPosYButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogPosYButton_MouseDown);
            this.jogPosYButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogPosYButton_MouseUp);
            // 
            // jogNegYButton
            // 
            this.jogNegYButton.Location = new System.Drawing.Point(47, 88);
            this.jogNegYButton.Name = "jogNegYButton";
            this.jogNegYButton.Size = new System.Drawing.Size(55, 23);
            this.jogNegYButton.TabIndex = 10;
            this.jogNegYButton.Text = "Y-";
            this.jogNegYButton.UseVisualStyleBackColor = true;
            this.jogNegYButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogNegYButton_MouseDown);
            this.jogNegYButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogNegYButton_MouseUp);
            // 
            // eStopButton
            // 
            this.eStopButton.BackColor = System.Drawing.Color.Red;
            this.eStopButton.ForeColor = System.Drawing.Color.White;
            this.eStopButton.Location = new System.Drawing.Point(501, 395);
            this.eStopButton.Name = "eStopButton";
            this.eStopButton.Size = new System.Drawing.Size(89, 49);
            this.eStopButton.TabIndex = 11;
            this.eStopButton.Text = "Emergency Stop";
            this.eStopButton.UseVisualStyleBackColor = false;
            this.eStopButton.Click += new System.EventHandler(this.eStopButton_Click);
            // 
            // jogNegBButton
            // 
            this.jogNegBButton.Location = new System.Drawing.Point(14, 59);
            this.jogNegBButton.Name = "jogNegBButton";
            this.jogNegBButton.Size = new System.Drawing.Size(55, 23);
            this.jogNegBButton.TabIndex = 12;
            this.jogNegBButton.Text = "B-";
            this.jogNegBButton.UseVisualStyleBackColor = true;
            this.jogNegBButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogNegBButton_MouseDown);
            this.jogNegBButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogNegBButton_MouseUp);
            // 
            // jogPosBButton
            // 
            this.jogPosBButton.Location = new System.Drawing.Point(75, 59);
            this.jogPosBButton.Name = "jogPosBButton";
            this.jogPosBButton.Size = new System.Drawing.Size(55, 23);
            this.jogPosBButton.TabIndex = 13;
            this.jogPosBButton.Text = "B+";
            this.jogPosBButton.UseVisualStyleBackColor = true;
            this.jogPosBButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogPosBButton_MouseDown);
            this.jogPosBButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.jogPosBButton_MouseUp);
            // 
            // jogPosAButton
            // 
            this.jogPosAButton.Location = new System.Drawing.Point(218, 88);
            this.jogPosAButton.Name = "jogPosAButton";
            this.jogPosAButton.Size = new System.Drawing.Size(55, 23);
            this.jogPosAButton.TabIndex = 14;
            this.jogPosAButton.Text = "A+";
            this.jogPosAButton.UseVisualStyleBackColor = true;
            this.jogPosAButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogPosAButton_MouseDown);
            this.jogPosAButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogPosAButton_MouseUp);
            // 
            // jogNegAButton
            // 
            this.jogNegAButton.Location = new System.Drawing.Point(218, 30);
            this.jogNegAButton.Name = "jogNegAButton";
            this.jogNegAButton.Size = new System.Drawing.Size(55, 23);
            this.jogNegAButton.TabIndex = 15;
            this.jogNegAButton.Text = "A-";
            this.jogNegAButton.UseVisualStyleBackColor = true;
            this.jogNegAButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogNegAButton_MouseDown);
            this.jogNegAButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogNegAButton_MouseUp);
            // 
            // jogPosZButton
            // 
            this.jogPosZButton.Location = new System.Drawing.Point(130, 30);
            this.jogPosZButton.Name = "jogPosZButton";
            this.jogPosZButton.Size = new System.Drawing.Size(55, 23);
            this.jogPosZButton.TabIndex = 16;
            this.jogPosZButton.Text = "Z+";
            this.jogPosZButton.UseVisualStyleBackColor = true;
            this.jogPosZButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogPosZButton_MouseDown);
            this.jogPosZButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogPosZButton_MouseUp);
            // 
            // jogNegZButton
            // 
            this.jogNegZButton.Location = new System.Drawing.Point(130, 88);
            this.jogNegZButton.Name = "jogNegZButton";
            this.jogNegZButton.Size = new System.Drawing.Size(55, 23);
            this.jogNegZButton.TabIndex = 17;
            this.jogNegZButton.Text = "Z-";
            this.jogNegZButton.UseVisualStyleBackColor = true;
            this.jogNegZButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JogNegZButton_MouseDown);
            this.jogNegZButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.JogNegZButton_MouseUp);
            // 
            // homeXButton
            // 
            this.homeXButton.Location = new System.Drawing.Point(9, 19);
            this.homeXButton.Name = "homeXButton";
            this.homeXButton.Size = new System.Drawing.Size(65, 23);
            this.homeXButton.TabIndex = 18;
            this.homeXButton.Text = "Home X";
            this.homeXButton.UseVisualStyleBackColor = true;
            // 
            // homeYButton
            // 
            this.homeYButton.Location = new System.Drawing.Point(80, 19);
            this.homeYButton.Name = "homeYButton";
            this.homeYButton.Size = new System.Drawing.Size(65, 23);
            this.homeYButton.TabIndex = 19;
            this.homeYButton.Text = "Home Y";
            this.homeYButton.UseVisualStyleBackColor = true;
            // 
            // homeZButton
            // 
            this.homeZButton.Location = new System.Drawing.Point(151, 19);
            this.homeZButton.Name = "homeZButton";
            this.homeZButton.Size = new System.Drawing.Size(65, 23);
            this.homeZButton.TabIndex = 20;
            this.homeZButton.Text = "Home Z";
            this.homeZButton.UseVisualStyleBackColor = true;
            // 
            // homeAButton
            // 
            this.homeAButton.Location = new System.Drawing.Point(9, 48);
            this.homeAButton.Name = "homeAButton";
            this.homeAButton.Size = new System.Drawing.Size(65, 23);
            this.homeAButton.TabIndex = 21;
            this.homeAButton.Text = "Home A";
            this.homeAButton.UseVisualStyleBackColor = true;
            // 
            // homeBButton
            // 
            this.homeBButton.Location = new System.Drawing.Point(80, 48);
            this.homeBButton.Name = "homeBButton";
            this.homeBButton.Size = new System.Drawing.Size(65, 23);
            this.homeBButton.TabIndex = 22;
            this.homeBButton.Text = "Home B";
            this.homeBButton.UseVisualStyleBackColor = true;
            // 
            // homeAllButton
            // 
            this.homeAllButton.Location = new System.Drawing.Point(151, 48);
            this.homeAllButton.Name = "homeAllButton";
            this.homeAllButton.Size = new System.Drawing.Size(65, 23);
            this.homeAllButton.TabIndex = 23;
            this.homeAllButton.Text = "Home All";
            this.homeAllButton.UseVisualStyleBackColor = true;
            // 
            // actualPosXTextBox
            // 
            this.actualPosXTextBox.Location = new System.Drawing.Point(26, 17);
            this.actualPosXTextBox.MaxLength = 8;
            this.actualPosXTextBox.Name = "actualPosXTextBox";
            this.actualPosXTextBox.Size = new System.Drawing.Size(54, 20);
            this.actualPosXTextBox.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Y";
            // 
            // actualPosYTextBox
            // 
            this.actualPosYTextBox.Location = new System.Drawing.Point(26, 43);
            this.actualPosYTextBox.MaxLength = 8;
            this.actualPosYTextBox.Name = "actualPosYTextBox";
            this.actualPosYTextBox.Size = new System.Drawing.Size(54, 20);
            this.actualPosYTextBox.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Z";
            // 
            // actualPosZTextBox
            // 
            this.actualPosZTextBox.Location = new System.Drawing.Point(26, 69);
            this.actualPosZTextBox.MaxLength = 8;
            this.actualPosZTextBox.Name = "actualPosZTextBox";
            this.actualPosZTextBox.Size = new System.Drawing.Size(54, 20);
            this.actualPosZTextBox.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "A";
            // 
            // actualPosATextBox
            // 
            this.actualPosATextBox.Location = new System.Drawing.Point(26, 95);
            this.actualPosATextBox.MaxLength = 8;
            this.actualPosATextBox.Name = "actualPosATextBox";
            this.actualPosATextBox.Size = new System.Drawing.Size(54, 20);
            this.actualPosATextBox.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "B";
            // 
            // actualPosBTextBox
            // 
            this.actualPosBTextBox.Location = new System.Drawing.Point(26, 121);
            this.actualPosBTextBox.MaxLength = 8;
            this.actualPosBTextBox.Name = "actualPosBTextBox";
            this.actualPosBTextBox.Size = new System.Drawing.Size(54, 20);
            this.actualPosBTextBox.TabIndex = 32;
            // 
            // moveToPosXButton
            // 
            this.moveToPosXButton.Location = new System.Drawing.Point(157, 15);
            this.moveToPosXButton.Name = "moveToPosXButton";
            this.moveToPosXButton.Size = new System.Drawing.Size(55, 23);
            this.moveToPosXButton.TabIndex = 34;
            this.moveToPosXButton.Text = "Move to";
            this.moveToPosXButton.UseVisualStyleBackColor = true;
            this.moveToPosXButton.Click += new System.EventHandler(this.MoveToPosXButton_Click);
            // 
            // moveToPosYButton
            // 
            this.moveToPosYButton.Location = new System.Drawing.Point(157, 41);
            this.moveToPosYButton.Name = "moveToPosYButton";
            this.moveToPosYButton.Size = new System.Drawing.Size(55, 23);
            this.moveToPosYButton.TabIndex = 35;
            this.moveToPosYButton.Text = "Move to";
            this.moveToPosYButton.UseVisualStyleBackColor = true;
            this.moveToPosYButton.Click += new System.EventHandler(this.MoveToPosYButton_Click);
            // 
            // moveToPosZButton
            // 
            this.moveToPosZButton.Location = new System.Drawing.Point(157, 67);
            this.moveToPosZButton.Name = "moveToPosZButton";
            this.moveToPosZButton.Size = new System.Drawing.Size(55, 23);
            this.moveToPosZButton.TabIndex = 36;
            this.moveToPosZButton.Text = "Move to";
            this.moveToPosZButton.UseVisualStyleBackColor = true;
            this.moveToPosZButton.Click += new System.EventHandler(this.MoveToPosZButton_Click);
            // 
            // moveToPosAButton
            // 
            this.moveToPosAButton.Location = new System.Drawing.Point(157, 93);
            this.moveToPosAButton.Name = "moveToPosAButton";
            this.moveToPosAButton.Size = new System.Drawing.Size(55, 23);
            this.moveToPosAButton.TabIndex = 37;
            this.moveToPosAButton.Text = "Move to";
            this.moveToPosAButton.UseVisualStyleBackColor = true;
            this.moveToPosAButton.Click += new System.EventHandler(this.MoveToPosAButton_Click);
            // 
            // moveToPosBButton
            // 
            this.moveToPosBButton.Location = new System.Drawing.Point(157, 119);
            this.moveToPosBButton.Name = "moveToPosBButton";
            this.moveToPosBButton.Size = new System.Drawing.Size(55, 23);
            this.moveToPosBButton.TabIndex = 38;
            this.moveToPosBButton.Text = "Move to";
            this.moveToPosBButton.UseVisualStyleBackColor = true;
            this.moveToPosBButton.Click += new System.EventHandler(this.MoveToPosBButton_Click);
            // 
            // setZeroXButton
            // 
            this.setZeroXButton.Location = new System.Drawing.Point(218, 15);
            this.setZeroXButton.Name = "setZeroXButton";
            this.setZeroXButton.Size = new System.Drawing.Size(55, 23);
            this.setZeroXButton.TabIndex = 39;
            this.setZeroXButton.Text = "Set zero";
            this.setZeroXButton.UseVisualStyleBackColor = true;
            this.setZeroXButton.Click += new System.EventHandler(this.SetZeroXButton_Click);
            // 
            // setZeroYButton
            // 
            this.setZeroYButton.Location = new System.Drawing.Point(218, 41);
            this.setZeroYButton.Name = "setZeroYButton";
            this.setZeroYButton.Size = new System.Drawing.Size(55, 23);
            this.setZeroYButton.TabIndex = 40;
            this.setZeroYButton.Text = "Set zero";
            this.setZeroYButton.UseVisualStyleBackColor = true;
            this.setZeroYButton.Click += new System.EventHandler(this.SetZeroYButton_Click);
            // 
            // setZeroZButton
            // 
            this.setZeroZButton.Location = new System.Drawing.Point(218, 67);
            this.setZeroZButton.Name = "setZeroZButton";
            this.setZeroZButton.Size = new System.Drawing.Size(55, 23);
            this.setZeroZButton.TabIndex = 41;
            this.setZeroZButton.Text = "Set zero";
            this.setZeroZButton.UseVisualStyleBackColor = true;
            this.setZeroZButton.Click += new System.EventHandler(this.SetZeroZButton_Click);
            // 
            // setZeroAButton
            // 
            this.setZeroAButton.Location = new System.Drawing.Point(218, 93);
            this.setZeroAButton.Name = "setZeroAButton";
            this.setZeroAButton.Size = new System.Drawing.Size(55, 23);
            this.setZeroAButton.TabIndex = 42;
            this.setZeroAButton.Text = "Set zero";
            this.setZeroAButton.UseVisualStyleBackColor = true;
            this.setZeroAButton.Click += new System.EventHandler(this.SetZeroAButton_Click);
            // 
            // setZeroBButton
            // 
            this.setZeroBButton.Location = new System.Drawing.Point(218, 119);
            this.setZeroBButton.Name = "setZeroBButton";
            this.setZeroBButton.Size = new System.Drawing.Size(55, 23);
            this.setZeroBButton.TabIndex = 43;
            this.setZeroBButton.Text = "Set zero";
            this.setZeroBButton.UseVisualStyleBackColor = true;
            this.setZeroBButton.Click += new System.EventHandler(this.SetZeroBButton_Click);
            // 
            // positionControlGroupBox
            // 
            this.positionControlGroupBox.Controls.Add(this.commandPosBTextBox);
            this.positionControlGroupBox.Controls.Add(this.commandPosATextBox);
            this.positionControlGroupBox.Controls.Add(this.commandPosZTextBox);
            this.positionControlGroupBox.Controls.Add(this.commandPosYTextBox);
            this.positionControlGroupBox.Controls.Add(this.commandPosXTextBox);
            this.positionControlGroupBox.Controls.Add(this.setZeroBButton);
            this.positionControlGroupBox.Controls.Add(this.setZeroAButton);
            this.positionControlGroupBox.Controls.Add(this.setZeroZButton);
            this.positionControlGroupBox.Controls.Add(this.setZeroYButton);
            this.positionControlGroupBox.Controls.Add(this.setZeroXButton);
            this.positionControlGroupBox.Controls.Add(this.moveToPosBButton);
            this.positionControlGroupBox.Controls.Add(this.moveToPosAButton);
            this.positionControlGroupBox.Controls.Add(this.moveToPosZButton);
            this.positionControlGroupBox.Controls.Add(this.moveToPosYButton);
            this.positionControlGroupBox.Controls.Add(this.moveToPosXButton);
            this.positionControlGroupBox.Controls.Add(this.label5);
            this.positionControlGroupBox.Controls.Add(this.actualPosBTextBox);
            this.positionControlGroupBox.Controls.Add(this.label4);
            this.positionControlGroupBox.Controls.Add(this.actualPosATextBox);
            this.positionControlGroupBox.Controls.Add(this.label3);
            this.positionControlGroupBox.Controls.Add(this.actualPosZTextBox);
            this.positionControlGroupBox.Controls.Add(this.label2);
            this.positionControlGroupBox.Controls.Add(this.actualPosYTextBox);
            this.positionControlGroupBox.Controls.Add(this.label1);
            this.positionControlGroupBox.Controls.Add(this.actualPosXTextBox);
            this.positionControlGroupBox.Enabled = false;
            this.positionControlGroupBox.Location = new System.Drawing.Point(764, 38);
            this.positionControlGroupBox.Name = "positionControlGroupBox";
            this.positionControlGroupBox.Size = new System.Drawing.Size(286, 150);
            this.positionControlGroupBox.TabIndex = 44;
            this.positionControlGroupBox.TabStop = false;
            this.positionControlGroupBox.Text = "Position Control";
            // 
            // commandPosBTextBox
            // 
            this.commandPosBTextBox.Location = new System.Drawing.Point(86, 121);
            this.commandPosBTextBox.MaxLength = 8;
            this.commandPosBTextBox.Name = "commandPosBTextBox";
            this.commandPosBTextBox.ReadOnly = true;
            this.commandPosBTextBox.Size = new System.Drawing.Size(54, 20);
            this.commandPosBTextBox.TabIndex = 48;
            // 
            // commandPosATextBox
            // 
            this.commandPosATextBox.Location = new System.Drawing.Point(86, 95);
            this.commandPosATextBox.MaxLength = 8;
            this.commandPosATextBox.Name = "commandPosATextBox";
            this.commandPosATextBox.ReadOnly = true;
            this.commandPosATextBox.Size = new System.Drawing.Size(54, 20);
            this.commandPosATextBox.TabIndex = 47;
            // 
            // commandPosZTextBox
            // 
            this.commandPosZTextBox.Location = new System.Drawing.Point(86, 69);
            this.commandPosZTextBox.MaxLength = 8;
            this.commandPosZTextBox.Name = "commandPosZTextBox";
            this.commandPosZTextBox.ReadOnly = true;
            this.commandPosZTextBox.Size = new System.Drawing.Size(54, 20);
            this.commandPosZTextBox.TabIndex = 46;
            // 
            // commandPosYTextBox
            // 
            this.commandPosYTextBox.Location = new System.Drawing.Point(86, 43);
            this.commandPosYTextBox.MaxLength = 8;
            this.commandPosYTextBox.Name = "commandPosYTextBox";
            this.commandPosYTextBox.ReadOnly = true;
            this.commandPosYTextBox.Size = new System.Drawing.Size(54, 20);
            this.commandPosYTextBox.TabIndex = 45;
            // 
            // commandPosXTextBox
            // 
            this.commandPosXTextBox.Location = new System.Drawing.Point(86, 17);
            this.commandPosXTextBox.MaxLength = 8;
            this.commandPosXTextBox.Name = "commandPosXTextBox";
            this.commandPosXTextBox.ReadOnly = true;
            this.commandPosXTextBox.Size = new System.Drawing.Size(54, 20);
            this.commandPosXTextBox.TabIndex = 44;
            // 
            // jogControlGroupBox
            // 
            this.jogControlGroupBox.Controls.Add(this.jogNegZButton);
            this.jogControlGroupBox.Controls.Add(this.jogPosZButton);
            this.jogControlGroupBox.Controls.Add(this.jogNegAButton);
            this.jogControlGroupBox.Controls.Add(this.jogPosAButton);
            this.jogControlGroupBox.Controls.Add(this.jogPosBButton);
            this.jogControlGroupBox.Controls.Add(this.jogNegYButton);
            this.jogControlGroupBox.Controls.Add(this.jogPosYButton);
            this.jogControlGroupBox.Controls.Add(this.jogNegXButton);
            this.jogControlGroupBox.Controls.Add(this.jogNegBButton);
            this.jogControlGroupBox.Controls.Add(this.jogPosXButton);
            this.jogControlGroupBox.Enabled = false;
            this.jogControlGroupBox.Location = new System.Drawing.Point(764, 194);
            this.jogControlGroupBox.Name = "jogControlGroupBox";
            this.jogControlGroupBox.Size = new System.Drawing.Size(314, 138);
            this.jogControlGroupBox.TabIndex = 45;
            this.jogControlGroupBox.TabStop = false;
            this.jogControlGroupBox.Text = "Jog Control";
            // 
            // buttonTimer
            // 
            this.buttonTimer.Tick += new System.EventHandler(this.ButtonTimer_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(640, 415);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 46;
            this.button1.Text = "Test Button";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // COMPortComboBox
            // 
            this.COMPortComboBox.FormattingEnabled = true;
            this.COMPortComboBox.Location = new System.Drawing.Point(1024, 359);
            this.COMPortComboBox.Name = "COMPortComboBox";
            this.COMPortComboBox.Size = new System.Drawing.Size(79, 21);
            this.COMPortComboBox.TabIndex = 47;
            // 
            // refreshCOMPortsButton
            // 
            this.refreshCOMPortsButton.Location = new System.Drawing.Point(1024, 386);
            this.refreshCOMPortsButton.Name = "refreshCOMPortsButton";
            this.refreshCOMPortsButton.Size = new System.Drawing.Size(75, 23);
            this.refreshCOMPortsButton.TabIndex = 48;
            this.refreshCOMPortsButton.Text = "Refresh";
            this.refreshCOMPortsButton.UseVisualStyleBackColor = true;
            this.refreshCOMPortsButton.Click += new System.EventHandler(this.RefreshCOMPortsButton_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM4";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort1_DataReceived);
            // 
            // homeAxesGroupBox
            // 
            this.homeAxesGroupBox.Controls.Add(this.homeAllButton);
            this.homeAxesGroupBox.Controls.Add(this.homeBButton);
            this.homeAxesGroupBox.Controls.Add(this.homeAButton);
            this.homeAxesGroupBox.Controls.Add(this.homeZButton);
            this.homeAxesGroupBox.Controls.Add(this.homeYButton);
            this.homeAxesGroupBox.Controls.Add(this.homeXButton);
            this.homeAxesGroupBox.Enabled = false;
            this.homeAxesGroupBox.Location = new System.Drawing.Point(764, 338);
            this.homeAxesGroupBox.Name = "homeAxesGroupBox";
            this.homeAxesGroupBox.Size = new System.Drawing.Size(218, 78);
            this.homeAxesGroupBox.TabIndex = 49;
            this.homeAxesGroupBox.TabStop = false;
            this.homeAxesGroupBox.Text = "Home Axes";
            // 
            // totalMaterialTextBox
            // 
            this.totalMaterialTextBox.Location = new System.Drawing.Point(302, 422);
            this.totalMaterialTextBox.MaxLength = 8;
            this.totalMaterialTextBox.Name = "totalMaterialTextBox";
            this.totalMaterialTextBox.ReadOnly = true;
            this.totalMaterialTextBox.Size = new System.Drawing.Size(85, 20);
            this.totalMaterialTextBox.TabIndex = 50;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 425);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 51;
            this.label6.Text = "Total Material:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.totalMaterialTextBox);
            this.Controls.Add(this.homeAxesGroupBox);
            this.Controls.Add(this.refreshCOMPortsButton);
            this.Controls.Add(this.COMPortComboBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.jogControlGroupBox);
            this.Controls.Add(this.positionControlGroupBox);
            this.Controls.Add(this.eStopButton);
            this.Controls.Add(this.serialConnectButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.genGCodeButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "3D Bender Interface";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.positionControlGroupBox.ResumeLayout(false);
            this.positionControlGroupBox.PerformLayout();
            this.jogControlGroupBox.ResumeLayout(false);
            this.homeAxesGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button genGCodeButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.Button serialConnectButton;
        private System.Windows.Forms.Button jogPosXButton;
        private System.Windows.Forms.Button jogNegXButton;
        private System.Windows.Forms.Button jogPosYButton;
        private System.Windows.Forms.Button jogNegYButton;
        private System.Windows.Forms.Button eStopButton;
        private System.Windows.Forms.Button jogNegBButton;
        private System.Windows.Forms.Button jogPosBButton;
        private System.Windows.Forms.Button jogPosAButton;
        private System.Windows.Forms.Button jogNegAButton;
        private System.Windows.Forms.Button jogPosZButton;
        private System.Windows.Forms.Button jogNegZButton;
        private System.Windows.Forms.Button homeXButton;
        private System.Windows.Forms.Button homeYButton;
        private System.Windows.Forms.Button homeZButton;
        private System.Windows.Forms.Button homeAButton;
        private System.Windows.Forms.Button homeBButton;
        private System.Windows.Forms.Button homeAllButton;
        private System.Windows.Forms.TextBox actualPosXTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox actualPosYTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox actualPosZTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox actualPosATextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox actualPosBTextBox;
        private System.Windows.Forms.Button moveToPosXButton;
        private System.Windows.Forms.Button moveToPosYButton;
        private System.Windows.Forms.Button moveToPosZButton;
        private System.Windows.Forms.Button moveToPosAButton;
        private System.Windows.Forms.Button moveToPosBButton;
        private System.Windows.Forms.Button setZeroXButton;
        private System.Windows.Forms.Button setZeroYButton;
        private System.Windows.Forms.Button setZeroZButton;
        private System.Windows.Forms.Button setZeroAButton;
        private System.Windows.Forms.Button setZeroBButton;
        private System.Windows.Forms.GroupBox positionControlGroupBox;
        private System.Windows.Forms.GroupBox jogControlGroupBox;
        private System.Windows.Forms.Timer buttonTimer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox COMPortComboBox;
        private System.Windows.Forms.Button refreshCOMPortsButton;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox commandPosBTextBox;
        private System.Windows.Forms.TextBox commandPosATextBox;
        private System.Windows.Forms.TextBox commandPosZTextBox;
        private System.Windows.Forms.TextBox commandPosYTextBox;
        private System.Windows.Forms.TextBox commandPosXTextBox;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem unitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metricToolStripMenuItem;
        private System.Windows.Forms.GroupBox homeAxesGroupBox;
        private System.Windows.Forms.TextBox totalMaterialTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem benderConfigToolStripMenuItem;
    }
}

