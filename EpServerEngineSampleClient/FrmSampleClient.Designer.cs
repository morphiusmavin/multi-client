namespace EpServerEngineSampleClient
{
    partial class FrmSampleClient
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
			this.tbReceived = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbPort = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.table = new System.Data.DataTable();
			this.tbConnected = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbIPAdress = new System.Windows.Forms.ComboBox();
			this.btnFnc3 = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnFnc1 = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.lbAvailClients = new System.Windows.Forms.ListBox();
			this.tbTodaysDate = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tbTime = new System.Windows.Forms.TextBox();
			this.btnFnc2 = new System.Windows.Forms.Button();
			this.btnMinimize = new System.Windows.Forms.Button();
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.btnFnc4 = new System.Windows.Forms.Button();
			this.btnFnc5 = new System.Windows.Forms.Button();
			this.btnSendSort = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.timer3 = new System.Windows.Forms.Timer(this.components);
			this.AlertLabel = new System.Windows.Forms.Label();
			this.cbNoUpdate = new System.Windows.Forms.CheckBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.clientControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cabinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.garageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testbenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.outdoorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.utilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dS1620ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.minimizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearAlertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.assignFunctionKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clientListActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showTimeUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.getTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rebootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToShellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.shutdownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.getStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.getDirInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.listDirInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sortDirInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadTempFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeGraphParamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.graphTimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.getTemp4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.getTemp5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reduceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnConnect = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.timer4 = new System.Windows.Forms.Timer(this.components);
			this.tbNoRecs = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.lbFileNames = new System.Windows.Forms.ListBox();
			this.btnDeleteFile = new System.Windows.Forms.Button();
			this.sendMsg2OtherWinclToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbReceived
			// 
			this.tbReceived.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.tbReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbReceived.Location = new System.Drawing.Point(230, 208);
			this.tbReceived.Multiline = true;
			this.tbReceived.Name = "tbReceived";
			this.tbReceived.ReadOnly = true;
			this.tbReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbReceived.Size = new System.Drawing.Size(216, 168);
			this.tbReceived.TabIndex = 16;
			this.tbReceived.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(24, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(46, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "port:";
			// 
			// tbPort
			// 
			this.tbPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbPort.Location = new System.Drawing.Point(103, 69);
			this.tbPort.Name = "tbPort";
			this.tbPort.Size = new System.Drawing.Size(90, 29);
			this.tbPort.TabIndex = 1;
			this.tbPort.Text = "5193";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "host:";
			// 
			// tbConnected
			// 
			this.tbConnected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbConnected.Location = new System.Drawing.Point(102, 108);
			this.tbConnected.Name = "tbConnected";
			this.tbConnected.ReadOnly = true;
			this.tbConnected.Size = new System.Drawing.Size(171, 29);
			this.tbConnected.TabIndex = 13;
			this.tbConnected.TabStop = false;
			this.tbConnected.Text = "not connected";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(23, 113);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 20);
			this.label3.TabIndex = 7;
			this.label3.Text = "Status:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cbIPAdress);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tbConnected);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.tbPort);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(479, 172);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(300, 160);
			this.groupBox1.TabIndex = 34;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "TCP Status";
			// 
			// cbIPAdress
			// 
			this.cbIPAdress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbIPAdress.FormattingEnabled = true;
			this.cbIPAdress.Location = new System.Drawing.Point(102, 28);
			this.cbIPAdress.Name = "cbIPAdress";
			this.cbIPAdress.Size = new System.Drawing.Size(171, 28);
			this.cbIPAdress.TabIndex = 0;
			this.cbIPAdress.SelectedIndexChanged += new System.EventHandler(this.IPAddressChanged);
			// 
			// btnFnc3
			// 
			this.btnFnc3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFnc3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFnc3.Location = new System.Drawing.Point(139, 42);
			this.btnFnc3.Name = "btnFnc3";
			this.btnFnc3.Size = new System.Drawing.Size(52, 47);
			this.btnFnc3.TabIndex = 24;
			this.btnFnc3.Text = "F3";
			this.btnFnc3.UseVisualStyleBackColor = false;
			this.btnFnc3.Click += new System.EventHandler(this.Function3Click);
			// 
			// btnClear
			// 
			this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClear.Location = new System.Drawing.Point(23, 104);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(193, 38);
			this.btnClear.TabIndex = 6;
			this.btnClear.Text = "Clear Screen";
			this.btnClear.UseVisualStyleBackColor = false;
			this.btnClear.Click += new System.EventHandler(this.ClearScreen);
			// 
			// btnFnc1
			// 
			this.btnFnc1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFnc1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFnc1.Location = new System.Drawing.Point(23, 42);
			this.btnFnc1.Name = "btnFnc1";
			this.btnFnc1.Size = new System.Drawing.Size(52, 47);
			this.btnFnc1.TabIndex = 22;
			this.btnFnc1.Text = "F1";
			this.btnFnc1.UseVisualStyleBackColor = false;
			this.btnFnc1.Click += new System.EventHandler(this.Function1Click);
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.myTimerTick);
			// 
			// lbAvailClients
			// 
			this.lbAvailClients.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbAvailClients.FormattingEnabled = true;
			this.lbAvailClients.ItemHeight = 15;
			this.lbAvailClients.Location = new System.Drawing.Point(727, 48);
			this.lbAvailClients.Name = "lbAvailClients";
			this.lbAvailClients.Size = new System.Drawing.Size(226, 94);
			this.lbAvailClients.TabIndex = 35;
			this.lbAvailClients.SelectedIndexChanged += new System.EventHandler(this.AvailClientSelIndexChanged);
			// 
			// tbTodaysDate
			// 
			this.tbTodaysDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbTodaysDate.Location = new System.Drawing.Point(539, 48);
			this.tbTodaysDate.Name = "tbTodaysDate";
			this.tbTodaysDate.Size = new System.Drawing.Size(164, 44);
			this.tbTodaysDate.TabIndex = 15;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(482, 61);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(53, 20);
			this.label10.TabIndex = 57;
			this.label10.Text = "Date:";
			// 
			// tbTime
			// 
			this.tbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbTime.Location = new System.Drawing.Point(539, 105);
			this.tbTime.Name = "tbTime";
			this.tbTime.Size = new System.Drawing.Size(164, 44);
			this.tbTime.TabIndex = 16;
			// 
			// btnFnc2
			// 
			this.btnFnc2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFnc2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFnc2.Location = new System.Drawing.Point(81, 42);
			this.btnFnc2.Name = "btnFnc2";
			this.btnFnc2.Size = new System.Drawing.Size(52, 47);
			this.btnFnc2.TabIndex = 23;
			this.btnFnc2.Text = "F2";
			this.btnFnc2.UseVisualStyleBackColor = false;
			this.btnFnc2.Click += new System.EventHandler(this.Function2Click);
			// 
			// btnMinimize
			// 
			this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnMinimize.Location = new System.Drawing.Point(23, 157);
			this.btnMinimize.Name = "btnMinimize";
			this.btnMinimize.Size = new System.Drawing.Size(193, 38);
			this.btnMinimize.TabIndex = 7;
			this.btnMinimize.Text = "Minimize";
			this.btnMinimize.UseVisualStyleBackColor = false;
			this.btnMinimize.Click += new System.EventHandler(this.Minimize_Click);
			// 
			// timer2
			// 
			this.timer2.Enabled = true;
			this.timer2.Interval = 300000;
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// btnFnc4
			// 
			this.btnFnc4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFnc4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFnc4.Location = new System.Drawing.Point(197, 42);
			this.btnFnc4.Name = "btnFnc4";
			this.btnFnc4.Size = new System.Drawing.Size(52, 47);
			this.btnFnc4.TabIndex = 25;
			this.btnFnc4.Text = "F4";
			this.btnFnc4.UseVisualStyleBackColor = false;
			this.btnFnc4.Click += new System.EventHandler(this.btnFnc4_Click);
			// 
			// btnFnc5
			// 
			this.btnFnc5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFnc5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFnc5.Location = new System.Drawing.Point(255, 42);
			this.btnFnc5.Name = "btnFnc5";
			this.btnFnc5.Size = new System.Drawing.Size(52, 47);
			this.btnFnc5.TabIndex = 26;
			this.btnFnc5.Text = "F5";
			this.btnFnc5.UseVisualStyleBackColor = false;
			this.btnFnc5.Click += new System.EventHandler(this.btnFcn5_Click);
			// 
			// btnSendSort
			// 
			this.btnSendSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnSendSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSendSort.Location = new System.Drawing.Point(230, 157);
			this.btnSendSort.Name = "btnSendSort";
			this.btnSendSort.Size = new System.Drawing.Size(193, 38);
			this.btnSendSort.TabIndex = 15;
			this.btnSendSort.Text = "Display Sort";
			this.btnSendSort.UseVisualStyleBackColor = false;
			this.btnSendSort.Click += new System.EventHandler(this.btnSendSort_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(481, 116);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(52, 20);
			this.label9.TabIndex = 88;
			this.label9.Text = "Time:";
			// 
			// timer3
			// 
			this.timer3.Interval = 6000000;
			this.timer3.Tick += new System.EventHandler(this.timer3_tick);
			// 
			// AlertLabel
			// 
			this.AlertLabel.AutoSize = true;
			this.AlertLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.AlertLabel.Location = new System.Drawing.Point(805, 124);
			this.AlertLabel.Name = "AlertLabel";
			this.AlertLabel.Size = new System.Drawing.Size(0, 20);
			this.AlertLabel.TabIndex = 0;
			// 
			// cbNoUpdate
			// 
			this.cbNoUpdate.AutoSize = true;
			this.cbNoUpdate.Location = new System.Drawing.Point(345, 61);
			this.cbNoUpdate.Name = "cbNoUpdate";
			this.cbNoUpdate.Size = new System.Drawing.Size(78, 17);
			this.cbNoUpdate.TabIndex = 90;
			this.cbNoUpdate.Text = "No Update";
			this.cbNoUpdate.UseVisualStyleBackColor = true;
			this.cbNoUpdate.CheckedChanged += new System.EventHandler(this.CheckChangedNoUpdate);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientControlsToolStripMenuItem,
            this.utilsToolStripMenuItem,
            this.clientListActionToolStripMenuItem,
            this.testToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1444, 24);
			this.menuStrip1.TabIndex = 92;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// clientControlsToolStripMenuItem
			// 
			this.clientControlsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cabinToolStripMenuItem,
            this.garageToolStripMenuItem,
            this.testbenchToolStripMenuItem,
            this.outdoorToolStripMenuItem});
			this.clientControlsToolStripMenuItem.Name = "clientControlsToolStripMenuItem";
			this.clientControlsToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
			this.clientControlsToolStripMenuItem.Text = "Client Controls";
			// 
			// cabinToolStripMenuItem
			// 
			this.cabinToolStripMenuItem.Name = "cabinToolStripMenuItem";
			this.cabinToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			this.cabinToolStripMenuItem.Text = "Cabin";
			this.cabinToolStripMenuItem.Click += new System.EventHandler(this.cabinToolStripMenuItem_Click);
			// 
			// garageToolStripMenuItem
			// 
			this.garageToolStripMenuItem.Name = "garageToolStripMenuItem";
			this.garageToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			this.garageToolStripMenuItem.Text = "Garage";
			this.garageToolStripMenuItem.Click += new System.EventHandler(this.garageToolStripMenuItem_Click);
			// 
			// testbenchToolStripMenuItem
			// 
			this.testbenchToolStripMenuItem.Name = "testbenchToolStripMenuItem";
			this.testbenchToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			this.testbenchToolStripMenuItem.Text = "Testbench";
			this.testbenchToolStripMenuItem.Click += new System.EventHandler(this.testbenchToolStripMenuItem_Click);
			// 
			// outdoorToolStripMenuItem
			// 
			this.outdoorToolStripMenuItem.Name = "outdoorToolStripMenuItem";
			this.outdoorToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			this.outdoorToolStripMenuItem.Text = "Outdoor";
			this.outdoorToolStripMenuItem.Click += new System.EventHandler(this.outdoorToolStripMenuItem_Click);
			// 
			// utilsToolStripMenuItem
			// 
			this.utilsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dS1620ToolStripMenuItem,
            this.timersToolStripMenuItem,
            this.minimizeToolStripMenuItem,
            this.clearScreenToolStripMenuItem,
            this.clearAlertToolStripMenuItem,
            this.assignFunctionKeyToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.sendMsg2OtherWinclToolStripMenuItem});
			this.utilsToolStripMenuItem.Name = "utilsToolStripMenuItem";
			this.utilsToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
			this.utilsToolStripMenuItem.Text = "Utils";
			// 
			// dS1620ToolStripMenuItem
			// 
			this.dS1620ToolStripMenuItem.Name = "dS1620ToolStripMenuItem";
			this.dS1620ToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.dS1620ToolStripMenuItem.Text = "DS1620";
			this.dS1620ToolStripMenuItem.Click += new System.EventHandler(this.dS1620ToolStripMenuItem_Click);
			// 
			// timersToolStripMenuItem
			// 
			this.timersToolStripMenuItem.Name = "timersToolStripMenuItem";
			this.timersToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.timersToolStripMenuItem.Text = "Timers";
			this.timersToolStripMenuItem.Click += new System.EventHandler(this.timersToolStripMenuItem_Click);
			// 
			// minimizeToolStripMenuItem
			// 
			this.minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
			this.minimizeToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.minimizeToolStripMenuItem.Text = "Minimize";
			this.minimizeToolStripMenuItem.Click += new System.EventHandler(this.minimizeToolStripMenuItem_Click);
			// 
			// clearScreenToolStripMenuItem
			// 
			this.clearScreenToolStripMenuItem.Name = "clearScreenToolStripMenuItem";
			this.clearScreenToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.clearScreenToolStripMenuItem.Text = "Clear Screen";
			this.clearScreenToolStripMenuItem.Click += new System.EventHandler(this.clearScreenToolStripMenuItem_Click);
			// 
			// clearAlertToolStripMenuItem
			// 
			this.clearAlertToolStripMenuItem.Name = "clearAlertToolStripMenuItem";
			this.clearAlertToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.clearAlertToolStripMenuItem.Text = "Clear Alert";
			this.clearAlertToolStripMenuItem.Click += new System.EventHandler(this.clearAlertToolStripMenuItem_Click);
			// 
			// assignFunctionKeyToolStripMenuItem
			// 
			this.assignFunctionKeyToolStripMenuItem.Name = "assignFunctionKeyToolStripMenuItem";
			this.assignFunctionKeyToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.assignFunctionKeyToolStripMenuItem.Text = "Assign Function Key";
			this.assignFunctionKeyToolStripMenuItem.Click += new System.EventHandler(this.assignFunctionKeyToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// clientListActionToolStripMenuItem
			// 
			this.clientListActionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTimeUpToolStripMenuItem,
            this.getTimeToolStripMenuItem,
            this.setTimeToolStripMenuItem,
            this.rebootToolStripMenuItem,
            this.exitToShellToolStripMenuItem,
            this.shutdownToolStripMenuItem,
            this.getStatusToolStripMenuItem,
            this.getDirInfoToolStripMenuItem,
            this.listDirInfoToolStripMenuItem,
            this.sortDirInfoToolStripMenuItem});
			this.clientListActionToolStripMenuItem.Name = "clientListActionToolStripMenuItem";
			this.clientListActionToolStripMenuItem.Size = new System.Drawing.Size(109, 20);
			this.clientListActionToolStripMenuItem.Text = "Client List Action";
			// 
			// showTimeUpToolStripMenuItem
			// 
			this.showTimeUpToolStripMenuItem.Name = "showTimeUpToolStripMenuItem";
			this.showTimeUpToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.showTimeUpToolStripMenuItem.Text = "Show Time Up";
			this.showTimeUpToolStripMenuItem.Click += new System.EventHandler(this.showTimeUpToolStripMenuItem_Click);
			// 
			// getTimeToolStripMenuItem
			// 
			this.getTimeToolStripMenuItem.Name = "getTimeToolStripMenuItem";
			this.getTimeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.getTimeToolStripMenuItem.Text = "Get Time";
			this.getTimeToolStripMenuItem.Click += new System.EventHandler(this.getTimeToolStripMenuItem_Click);
			// 
			// setTimeToolStripMenuItem
			// 
			this.setTimeToolStripMenuItem.Name = "setTimeToolStripMenuItem";
			this.setTimeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.setTimeToolStripMenuItem.Text = "Set Time";
			this.setTimeToolStripMenuItem.Click += new System.EventHandler(this.setTimeToolStripMenuItem_Click);
			// 
			// rebootToolStripMenuItem
			// 
			this.rebootToolStripMenuItem.Name = "rebootToolStripMenuItem";
			this.rebootToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.rebootToolStripMenuItem.Text = "Reboot";
			this.rebootToolStripMenuItem.Click += new System.EventHandler(this.rebootToolStripMenuItem_Click);
			// 
			// exitToShellToolStripMenuItem
			// 
			this.exitToShellToolStripMenuItem.Name = "exitToShellToolStripMenuItem";
			this.exitToShellToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.exitToShellToolStripMenuItem.Text = "Exit to Shell";
			this.exitToShellToolStripMenuItem.Click += new System.EventHandler(this.exitToShellToolStripMenuItem_Click);
			// 
			// shutdownToolStripMenuItem
			// 
			this.shutdownToolStripMenuItem.Name = "shutdownToolStripMenuItem";
			this.shutdownToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.shutdownToolStripMenuItem.Text = "Shutdown";
			this.shutdownToolStripMenuItem.Click += new System.EventHandler(this.shutdownToolStripMenuItem_Click);
			// 
			// getStatusToolStripMenuItem
			// 
			this.getStatusToolStripMenuItem.Name = "getStatusToolStripMenuItem";
			this.getStatusToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.getStatusToolStripMenuItem.Text = "Get Status";
			this.getStatusToolStripMenuItem.Click += new System.EventHandler(this.getStatusToolStripMenuItem_Click);
			// 
			// getDirInfoToolStripMenuItem
			// 
			this.getDirInfoToolStripMenuItem.Name = "getDirInfoToolStripMenuItem";
			this.getDirInfoToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.getDirInfoToolStripMenuItem.Text = "Get Dir Info";
			this.getDirInfoToolStripMenuItem.Click += new System.EventHandler(this.getDirInfoToolStripMenuItem_Click);
			// 
			// listDirInfoToolStripMenuItem
			// 
			this.listDirInfoToolStripMenuItem.Name = "listDirInfoToolStripMenuItem";
			this.listDirInfoToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.listDirInfoToolStripMenuItem.Text = "List Dir Info";
			this.listDirInfoToolStripMenuItem.Click += new System.EventHandler(this.listDirInfoToolStripMenuItem_Click);
			// 
			// sortDirInfoToolStripMenuItem
			// 
			this.sortDirInfoToolStripMenuItem.Name = "sortDirInfoToolStripMenuItem";
			this.sortDirInfoToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.sortDirInfoToolStripMenuItem.Text = "Send Dir Info";
			this.sortDirInfoToolStripMenuItem.Click += new System.EventHandler(this.sortDirInfoToolStripMenuItem_Click);
			// 
			// testToolStripMenuItem
			// 
			this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTempFileToolStripMenuItem,
            this.loadGraphToolStripMenuItem,
            this.clearGraphToolStripMenuItem,
            this.changeGraphParamsToolStripMenuItem,
            this.graphTimerToolStripMenuItem,
            this.getTemp4ToolStripMenuItem,
            this.getTemp5ToolStripMenuItem,
            this.reduceToolStripMenuItem});
			this.testToolStripMenuItem.Name = "testToolStripMenuItem";
			this.testToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
			this.testToolStripMenuItem.Text = "Test";
			// 
			// loadTempFileToolStripMenuItem
			// 
			this.loadTempFileToolStripMenuItem.Name = "loadTempFileToolStripMenuItem";
			this.loadTempFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.loadTempFileToolStripMenuItem.Text = "Load Temp File";
			this.loadTempFileToolStripMenuItem.Click += new System.EventHandler(this.loadTempFileToolStripMenuItem_Click);
			// 
			// loadGraphToolStripMenuItem
			// 
			this.loadGraphToolStripMenuItem.Name = "loadGraphToolStripMenuItem";
			this.loadGraphToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.loadGraphToolStripMenuItem.Text = "Load Graph";
			this.loadGraphToolStripMenuItem.Click += new System.EventHandler(this.loadGraphToolStripMenuItem_Click);
			// 
			// clearGraphToolStripMenuItem
			// 
			this.clearGraphToolStripMenuItem.Name = "clearGraphToolStripMenuItem";
			this.clearGraphToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.clearGraphToolStripMenuItem.Text = "Clear Graph";
			this.clearGraphToolStripMenuItem.Click += new System.EventHandler(this.clearGraphToolStripMenuItem_Click);
			// 
			// changeGraphParamsToolStripMenuItem
			// 
			this.changeGraphParamsToolStripMenuItem.Name = "changeGraphParamsToolStripMenuItem";
			this.changeGraphParamsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.changeGraphParamsToolStripMenuItem.Text = "Change Graph Params";
			this.changeGraphParamsToolStripMenuItem.Click += new System.EventHandler(this.changeGraphParamsToolStripMenuItem_Click);
			// 
			// graphTimerToolStripMenuItem
			// 
			this.graphTimerToolStripMenuItem.Name = "graphTimerToolStripMenuItem";
			this.graphTimerToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.graphTimerToolStripMenuItem.Text = "Graph timer";
			this.graphTimerToolStripMenuItem.Click += new System.EventHandler(this.graphTimerToolStripMenuItem_Click);
			// 
			// getTemp4ToolStripMenuItem
			// 
			this.getTemp4ToolStripMenuItem.Name = "getTemp4ToolStripMenuItem";
			this.getTemp4ToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.getTemp4ToolStripMenuItem.Text = "Get Temp 4";
			this.getTemp4ToolStripMenuItem.Click += new System.EventHandler(this.getTemp4ToolStripMenuItem_Click);
			// 
			// getTemp5ToolStripMenuItem
			// 
			this.getTemp5ToolStripMenuItem.Name = "getTemp5ToolStripMenuItem";
			this.getTemp5ToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.getTemp5ToolStripMenuItem.Text = "Get Temp 5";
			this.getTemp5ToolStripMenuItem.Click += new System.EventHandler(this.getTemp5ToolStripMenuItem_Click);
			// 
			// reduceToolStripMenuItem
			// 
			this.reduceToolStripMenuItem.Name = "reduceToolStripMenuItem";
			this.reduceToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
			this.reduceToolStripMenuItem.Text = "Reduce";
			this.reduceToolStripMenuItem.Click += new System.EventHandler(this.reduceToolStripMenuItem_Click);
			// 
			// btnConnect
			// 
			this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnConnect.Location = new System.Drawing.Point(230, 105);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(193, 38);
			this.btnConnect.TabIndex = 93;
			this.btnConnect.Text = "Connect";
			this.btnConnect.UseVisualStyleBackColor = false;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// btnExit
			// 
			this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExit.Location = new System.Drawing.Point(23, 208);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(193, 38);
			this.btnExit.TabIndex = 94;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// timer4
			// 
			this.timer4.Tick += new System.EventHandler(this.Test_graph_timer);
			// 
			// tbNoRecs
			// 
			this.tbNoRecs.Location = new System.Drawing.Point(867, 356);
			this.tbNoRecs.Name = "tbNoRecs";
			this.tbNoRecs.Size = new System.Drawing.Size(43, 20);
			this.tbNoRecs.TabIndex = 95;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(806, 359);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(55, 13);
			this.label5.TabIndex = 96;
			this.label5.Text = "No. Recs.";
			// 
			// lbFileNames
			// 
			this.lbFileNames.FormattingEnabled = true;
			this.lbFileNames.Location = new System.Drawing.Point(973, 53);
			this.lbFileNames.Name = "lbFileNames";
			this.lbFileNames.Size = new System.Drawing.Size(197, 264);
			this.lbFileNames.TabIndex = 97;
			this.lbFileNames.SelectedIndexChanged += new System.EventHandler(this.lbFileNames_SelectedIndexChanged);
			this.lbFileNames.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbFileNames_GetFile);
			// 
			// btnDeleteFile
			// 
			this.btnDeleteFile.Location = new System.Drawing.Point(973, 329);
			this.btnDeleteFile.Name = "btnDeleteFile";
			this.btnDeleteFile.Size = new System.Drawing.Size(75, 23);
			this.btnDeleteFile.TabIndex = 98;
			this.btnDeleteFile.Text = "Delete";
			this.btnDeleteFile.UseVisualStyleBackColor = true;
			this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
			// 
			// sendMsg2OtherWinclToolStripMenuItem
			// 
			this.sendMsg2OtherWinclToolStripMenuItem.Name = "sendMsg2OtherWinclToolStripMenuItem";
			this.sendMsg2OtherWinclToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.sendMsg2OtherWinclToolStripMenuItem.Text = "Send Msg 2 other wincl";
			this.sendMsg2OtherWinclToolStripMenuItem.Click += new System.EventHandler(this.sendMsg2OtherWinclToolStripMenuItem_Click);
			// 
			// FrmSampleClient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(1444, 726);
			this.Controls.Add(this.btnDeleteFile);
			this.Controls.Add(this.lbFileNames);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.tbNoRecs);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.cbNoUpdate);
			this.Controls.Add(this.AlertLabel);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.btnSendSort);
			this.Controls.Add(this.btnFnc5);
			this.Controls.Add(this.btnFnc4);
			this.Controls.Add(this.btnMinimize);
			this.Controls.Add(this.btnFnc2);
			this.Controls.Add(this.tbTime);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.tbTodaysDate);
			this.Controls.Add(this.lbAvailClients);
			this.Controls.Add(this.btnFnc1);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnFnc3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tbReceived);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "FrmSampleClient";
			this.ShowIcon = false;
			this.Text = "SampleClient";
			this.Load += new System.EventHandler(this.FrmSampleClient_Load);
			((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbReceived;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label1;
        private System.Data.DataTable table;
        private System.Windows.Forms.TextBox tbConnected;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFnc3;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnFnc1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ComboBox cbIPAdress;
		private System.Windows.Forms.ListBox lbAvailClients;
		private System.Windows.Forms.TextBox tbTodaysDate;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox tbTime;
		private System.Windows.Forms.Button btnFnc2;
		private System.Windows.Forms.Button btnMinimize;
		private System.Windows.Forms.Timer timer2;
		private System.Windows.Forms.Button btnFnc4;
		private System.Windows.Forms.Button btnFnc5;
		private System.Windows.Forms.Button btnSendSort;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Timer timer3;
		private System.Windows.Forms.Label AlertLabel;
		private System.Windows.Forms.CheckBox cbNoUpdate;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem clientControlsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cabinToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem garageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem testbenchToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem outdoorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem utilsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem dS1620ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem timersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem minimizeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearScreenToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearAlertToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem assignFunctionKeyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clientListActionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToShellToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showTimeUpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem getTimeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setTimeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rebootToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem shutdownToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem getStatusToolStripMenuItem;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadTempFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadGraphToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearGraphToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeGraphParamsToolStripMenuItem;
		private System.Windows.Forms.Timer timer4;
		private System.Windows.Forms.ToolStripMenuItem graphTimerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem getTemp4ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem getDirInfoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reduceToolStripMenuItem;
		private System.Windows.Forms.TextBox tbNoRecs;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ToolStripMenuItem listDirInfoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sortDirInfoToolStripMenuItem;
		private System.Windows.Forms.ListBox lbFileNames;
		private System.Windows.Forms.Button btnDeleteFile;
		private System.Windows.Forms.ToolStripMenuItem getTemp5ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sendMsg2OtherWinclToolStripMenuItem;
	}
}

