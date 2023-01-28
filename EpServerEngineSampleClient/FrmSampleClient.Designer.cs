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
			this.btnConnect = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.tbPort = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.table = new System.Data.DataTable();
			this.tbConnected = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnAssignFunc = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cbWhichWinClient = new System.Windows.Forms.ComboBox();
			this.cbIPAdress = new System.Windows.Forms.ComboBox();
			this.btnMngServer = new System.Windows.Forms.Button();
			this.btnRescan = new System.Windows.Forms.Button();
			this.btnFnc3 = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnFnc1 = new System.Windows.Forms.Button();
			this.btnGarageForm = new System.Windows.Forms.Button();
			this.btnSettings = new System.Windows.Forms.Button();
			this.btnCabin = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.lbAvailClients = new System.Windows.Forms.ListBox();
			this.btnRebootClient = new System.Windows.Forms.Button();
			this.btnShutdownClient = new System.Windows.Forms.Button();
			this.btnSendStatus = new System.Windows.Forms.Button();
			this.btnSendMsg = new System.Windows.Forms.Button();
			this.bSetClientTime = new System.Windows.Forms.Button();
			this.btnReportTimeUp = new System.Windows.Forms.Button();
			this.btnExit2Shell = new System.Windows.Forms.Button();
			this.tbSendMsg = new System.Windows.Forms.TextBox();
			this.btnTestBench = new System.Windows.Forms.Button();
			this.tbAlarmHours = new System.Windows.Forms.TextBox();
			this.cbAlarm = new System.Windows.Forms.CheckBox();
			this.tbTodaysDate = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tbTime = new System.Windows.Forms.TextBox();
			this.btnFnc2 = new System.Windows.Forms.Button();
			this.btnMinimize = new System.Windows.Forms.Button();
			this.btnSunriseSunset = new System.Windows.Forms.Button();
			this.SunriseLabel = new System.Windows.Forms.Label();
			this.SunsetLabel = new System.Windows.Forms.Label();
			this.MoonriseLabel = new System.Windows.Forms.Label();
			this.MoonsetLabel = new System.Windows.Forms.Label();
			this.btnTimerSchedules = new System.Windows.Forms.Button();
			this.btnOutdoor = new System.Windows.Forms.Button();
			this.btnSaveJournalEntry = new System.Windows.Forms.Button();
			this.tbAlarmMinutes = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.tbAlarmSeconds = new System.Windows.Forms.TextBox();
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.tbAlarmTick = new System.Windows.Forms.TextBox();
			this.btnGetTime = new System.Windows.Forms.Button();
			this.tbQuoteOfTheDay = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnFnc4 = new System.Windows.Forms.Button();
			this.btnFnc5 = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.NextSrssLabel = new System.Windows.Forms.Label();
			this.btnNextSunrise = new System.Windows.Forms.Button();
			this.btnSendSort = new System.Windows.Forms.Button();
			this.timer3 = new System.Windows.Forms.Timer(this.components);
			this.tbJournalEntry = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.btnUnused = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbReceived
			// 
			this.tbReceived.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.tbReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbReceived.Location = new System.Drawing.Point(21, 378);
			this.tbReceived.Multiline = true;
			this.tbReceived.Name = "tbReceived";
			this.tbReceived.ReadOnly = true;
			this.tbReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbReceived.Size = new System.Drawing.Size(235, 243);
			this.tbReceived.TabIndex = 16;
			this.tbReceived.TabStop = false;
			// 
			// btnConnect
			// 
			this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnConnect.Location = new System.Drawing.Point(272, 69);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(233, 61);
			this.btnConnect.TabIndex = 4;
			this.btnConnect.Text = "Connect Server";
			this.btnConnect.UseVisualStyleBackColor = false;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
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
			// btnAssignFunc
			// 
			this.btnAssignFunc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnAssignFunc.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAssignFunc.Location = new System.Drawing.Point(358, 12);
			this.btnAssignFunc.Name = "btnAssignFunc";
			this.btnAssignFunc.Size = new System.Drawing.Size(200, 46);
			this.btnAssignFunc.TabIndex = 27;
			this.btnAssignFunc.Text = "Assign Func";
			this.btnAssignFunc.UseVisualStyleBackColor = false;
			this.btnAssignFunc.Click += new System.EventHandler(this.BtnAssignFunction);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.cbWhichWinClient);
			this.groupBox1.Controls.Add(this.cbIPAdress);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tbConnected);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.tbPort);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(529, 67);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(300, 204);
			this.groupBox1.TabIndex = 34;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "TCP Status";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(9, 157);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(93, 20);
			this.label4.TabIndex = 33;
			this.label4.Text = "This Client";
			// 
			// cbWhichWinClient
			// 
			this.cbWhichWinClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbWhichWinClient.FormattingEnabled = true;
			this.cbWhichWinClient.Items.AddRange(new object[] {
            "Second_Windows7",
            "Win7-x64"});
			this.cbWhichWinClient.Location = new System.Drawing.Point(102, 151);
			this.cbWhichWinClient.Name = "cbWhichWinClient";
			this.cbWhichWinClient.Size = new System.Drawing.Size(172, 28);
			this.cbWhichWinClient.TabIndex = 32;
			this.cbWhichWinClient.SelectedIndexChanged += new System.EventHandler(this.cbWhichWinClient_SelectedIndexChanged);
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
			// btnMngServer
			// 
			this.btnMngServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnMngServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnMngServer.Location = new System.Drawing.Point(21, 69);
			this.btnMngServer.Name = "btnMngServer";
			this.btnMngServer.Size = new System.Drawing.Size(235, 61);
			this.btnMngServer.TabIndex = 0;
			this.btnMngServer.Text = "Manage Server";
			this.btnMngServer.UseVisualStyleBackColor = false;
			this.btnMngServer.Click += new System.EventHandler(this.btnMngServer_Click);
			// 
			// btnRescan
			// 
			this.btnRescan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnRescan.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRescan.Location = new System.Drawing.Point(21, 147);
			this.btnRescan.Name = "btnRescan";
			this.btnRescan.Size = new System.Drawing.Size(235, 61);
			this.btnRescan.TabIndex = 1;
			this.btnRescan.Text = "Unused";
			this.btnRescan.UseVisualStyleBackColor = false;
			this.btnRescan.Click += new System.EventHandler(this.btnRescan_Click);
			// 
			// btnFnc3
			// 
			this.btnFnc3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFnc3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFnc3.Location = new System.Drawing.Point(155, 12);
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
			this.btnClear.Location = new System.Drawing.Point(272, 225);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(235, 61);
			this.btnClear.TabIndex = 6;
			this.btnClear.Text = "Clear Screen";
			this.btnClear.UseVisualStyleBackColor = false;
			this.btnClear.Click += new System.EventHandler(this.ClearScreen);
			// 
			// btnFnc1
			// 
			this.btnFnc1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFnc1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFnc1.Location = new System.Drawing.Point(23, 12);
			this.btnFnc1.Name = "btnFnc1";
			this.btnFnc1.Size = new System.Drawing.Size(52, 47);
			this.btnFnc1.TabIndex = 22;
			this.btnFnc1.Text = "F1";
			this.btnFnc1.UseVisualStyleBackColor = false;
			this.btnFnc1.Click += new System.EventHandler(this.Function1Click);
			// 
			// btnGarageForm
			// 
			this.btnGarageForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnGarageForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGarageForm.Location = new System.Drawing.Point(1056, 491);
			this.btnGarageForm.Name = "btnGarageForm";
			this.btnGarageForm.Size = new System.Drawing.Size(274, 38);
			this.btnGarageForm.TabIndex = 17;
			this.btnGarageForm.Text = "Garage Lights";
			this.btnGarageForm.UseVisualStyleBackColor = false;
			this.btnGarageForm.Click += new System.EventHandler(this.GarageFormClick);
			// 
			// btnSettings
			// 
			this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSettings.Location = new System.Drawing.Point(272, 147);
			this.btnSettings.Name = "btnSettings";
			this.btnSettings.Size = new System.Drawing.Size(235, 61);
			this.btnSettings.TabIndex = 5;
			this.btnSettings.Text = "Settings";
			this.btnSettings.UseVisualStyleBackColor = false;
			this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
			// 
			// btnCabin
			// 
			this.btnCabin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnCabin.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCabin.Location = new System.Drawing.Point(1056, 445);
			this.btnCabin.Name = "btnCabin";
			this.btnCabin.Size = new System.Drawing.Size(274, 38);
			this.btnCabin.TabIndex = 16;
			this.btnCabin.Text = "Cabin";
			this.btnCabin.UseVisualStyleBackColor = false;
			this.btnCabin.Click += new System.EventHandler(this.Cabin_Click);
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
			this.lbAvailClients.Location = new System.Drawing.Point(859, 84);
			this.lbAvailClients.Name = "lbAvailClients";
			this.lbAvailClients.Size = new System.Drawing.Size(173, 94);
			this.lbAvailClients.TabIndex = 35;
			this.lbAvailClients.SelectedIndexChanged += new System.EventHandler(this.AvailClientSelIndexChanged);
			// 
			// btnRebootClient
			// 
			this.btnRebootClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnRebootClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnRebootClient.Location = new System.Drawing.Point(859, 190);
			this.btnRebootClient.Name = "btnRebootClient";
			this.btnRebootClient.Size = new System.Drawing.Size(173, 37);
			this.btnRebootClient.TabIndex = 8;
			this.btnRebootClient.Text = "Reboot";
			this.btnRebootClient.UseVisualStyleBackColor = false;
			this.btnRebootClient.Click += new System.EventHandler(this.btnRebootClient_Click);
			// 
			// btnShutdownClient
			// 
			this.btnShutdownClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnShutdownClient.Location = new System.Drawing.Point(859, 290);
			this.btnShutdownClient.Name = "btnShutdownClient";
			this.btnShutdownClient.Size = new System.Drawing.Size(173, 37);
			this.btnShutdownClient.TabIndex = 10;
			this.btnShutdownClient.Text = "Shutdown";
			this.btnShutdownClient.UseVisualStyleBackColor = true;
			this.btnShutdownClient.Click += new System.EventHandler(this.btnShutdownClient_Click);
			// 
			// btnSendStatus
			// 
			this.btnSendStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnSendStatus.Location = new System.Drawing.Point(859, 340);
			this.btnSendStatus.Name = "btnSendStatus";
			this.btnSendStatus.Size = new System.Drawing.Size(173, 37);
			this.btnSendStatus.TabIndex = 11;
			this.btnSendStatus.Text = "Get Status";
			this.btnSendStatus.UseVisualStyleBackColor = true;
			this.btnSendStatus.Click += new System.EventHandler(this.btnSendStatus_Click);
			// 
			// btnSendMsg
			// 
			this.btnSendMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnSendMsg.Location = new System.Drawing.Point(533, 475);
			this.btnSendMsg.Name = "btnSendMsg";
			this.btnSendMsg.Size = new System.Drawing.Size(198, 37);
			this.btnSendMsg.TabIndex = 20;
			this.btnSendMsg.Text = "Send Message";
			this.btnSendMsg.UseVisualStyleBackColor = true;
			this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
			// 
			// bSetClientTime
			// 
			this.bSetClientTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.bSetClientTime.Location = new System.Drawing.Point(859, 390);
			this.bSetClientTime.Name = "bSetClientTime";
			this.bSetClientTime.Size = new System.Drawing.Size(173, 37);
			this.bSetClientTime.TabIndex = 12;
			this.bSetClientTime.Text = "Set Time";
			this.bSetClientTime.UseVisualStyleBackColor = true;
			this.bSetClientTime.Click += new System.EventHandler(this.bSetClientTime_Click);
			// 
			// btnReportTimeUp
			// 
			this.btnReportTimeUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnReportTimeUp.Location = new System.Drawing.Point(859, 490);
			this.btnReportTimeUp.Name = "btnReportTimeUp";
			this.btnReportTimeUp.Size = new System.Drawing.Size(173, 37);
			this.btnReportTimeUp.TabIndex = 14;
			this.btnReportTimeUp.Text = "Time Up";
			this.btnReportTimeUp.UseVisualStyleBackColor = true;
			this.btnReportTimeUp.Click += new System.EventHandler(this.btnReportTimeUp_Click);
			// 
			// btnExit2Shell
			// 
			this.btnExit2Shell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnExit2Shell.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnExit2Shell.Location = new System.Drawing.Point(859, 240);
			this.btnExit2Shell.Name = "btnExit2Shell";
			this.btnExit2Shell.Size = new System.Drawing.Size(173, 37);
			this.btnExit2Shell.TabIndex = 9;
			this.btnExit2Shell.Text = "Exit to Shell";
			this.btnExit2Shell.UseVisualStyleBackColor = false;
			this.btnExit2Shell.Click += new System.EventHandler(this.Exit2Shell_Click);
			// 
			// tbSendMsg
			// 
			this.tbSendMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbSendMsg.Location = new System.Drawing.Point(533, 518);
			this.tbSendMsg.Name = "tbSendMsg";
			this.tbSendMsg.Size = new System.Drawing.Size(285, 26);
			this.tbSendMsg.TabIndex = 20;
			this.tbSendMsg.TextChanged += new System.EventHandler(this.tbSendMsg_TextChanged);
			// 
			// btnTestBench
			// 
			this.btnTestBench.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnTestBench.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTestBench.Location = new System.Drawing.Point(1056, 586);
			this.btnTestBench.Name = "btnTestBench";
			this.btnTestBench.Size = new System.Drawing.Size(274, 38);
			this.btnTestBench.TabIndex = 19;
			this.btnTestBench.Text = "Test Bench";
			this.btnTestBench.UseVisualStyleBackColor = false;
			this.btnTestBench.Click += new System.EventHandler(this.btnTestBench_Click);
			// 
			// tbAlarmHours
			// 
			this.tbAlarmHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
			this.tbAlarmHours.Location = new System.Drawing.Point(125, 23);
			this.tbAlarmHours.Name = "tbAlarmHours";
			this.tbAlarmHours.Size = new System.Drawing.Size(38, 29);
			this.tbAlarmHours.TabIndex = 19;
			this.tbAlarmHours.Text = "0";
			this.tbAlarmHours.TextChanged += new System.EventHandler(this.tbAlarm_TextChanged);
			// 
			// cbAlarm
			// 
			this.cbAlarm.AutoSize = true;
			this.cbAlarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
			this.cbAlarm.Location = new System.Drawing.Point(23, 23);
			this.cbAlarm.Name = "cbAlarm";
			this.cbAlarm.Size = new System.Drawing.Size(59, 28);
			this.cbAlarm.TabIndex = 49;
			this.cbAlarm.Text = "Set";
			this.cbAlarm.UseVisualStyleBackColor = true;
			this.cbAlarm.CheckedChanged += new System.EventHandler(this.cbAlarm_CheckedChanged);
			// 
			// tbTodaysDate
			// 
			this.tbTodaysDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.tbTodaysDate.Location = new System.Drawing.Point(658, 25);
			this.tbTodaysDate.Name = "tbTodaysDate";
			this.tbTodaysDate.Size = new System.Drawing.Size(103, 26);
			this.tbTodaysDate.TabIndex = 15;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(590, 31);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(62, 20);
			this.label10.TabIndex = 57;
			this.label10.Text = "Today:";
			// 
			// tbTime
			// 
			this.tbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.tbTime.Location = new System.Drawing.Point(772, 25);
			this.tbTime.Name = "tbTime";
			this.tbTime.Size = new System.Drawing.Size(103, 26);
			this.tbTime.TabIndex = 16;
			// 
			// btnFnc2
			// 
			this.btnFnc2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFnc2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFnc2.Location = new System.Drawing.Point(89, 12);
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
			this.btnMinimize.Location = new System.Drawing.Point(270, 303);
			this.btnMinimize.Name = "btnMinimize";
			this.btnMinimize.Size = new System.Drawing.Size(235, 61);
			this.btnMinimize.TabIndex = 7;
			this.btnMinimize.Text = "Minimize";
			this.btnMinimize.UseVisualStyleBackColor = false;
			this.btnMinimize.Click += new System.EventHandler(this.Minimize_Click);
			// 
			// btnSunriseSunset
			// 
			this.btnSunriseSunset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnSunriseSunset.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSunriseSunset.Location = new System.Drawing.Point(20, 28);
			this.btnSunriseSunset.Name = "btnSunriseSunset";
			this.btnSunriseSunset.Size = new System.Drawing.Size(117, 37);
			this.btnSunriseSunset.TabIndex = 0;
			this.btnSunriseSunset.Text = "Refresh";
			this.btnSunriseSunset.UseVisualStyleBackColor = false;
			this.btnSunriseSunset.Click += new System.EventHandler(this.btnSunriseSunset_Click);
			// 
			// SunriseLabel
			// 
			this.SunriseLabel.AutoSize = true;
			this.SunriseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SunriseLabel.Location = new System.Drawing.Point(16, 70);
			this.SunriseLabel.Name = "SunriseLabel";
			this.SunriseLabel.Size = new System.Drawing.Size(62, 20);
			this.SunriseLabel.TabIndex = 59;
			this.SunriseLabel.Text = "Today:";
			// 
			// SunsetLabel
			// 
			this.SunsetLabel.AutoSize = true;
			this.SunsetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SunsetLabel.Location = new System.Drawing.Point(16, 100);
			this.SunsetLabel.Name = "SunsetLabel";
			this.SunsetLabel.Size = new System.Drawing.Size(62, 20);
			this.SunsetLabel.TabIndex = 60;
			this.SunsetLabel.Text = "Today:";
			// 
			// MoonriseLabel
			// 
			this.MoonriseLabel.AutoSize = true;
			this.MoonriseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MoonriseLabel.Location = new System.Drawing.Point(16, 129);
			this.MoonriseLabel.Name = "MoonriseLabel";
			this.MoonriseLabel.Size = new System.Drawing.Size(62, 20);
			this.MoonriseLabel.TabIndex = 61;
			this.MoonriseLabel.Text = "Today:";
			// 
			// MoonsetLabel
			// 
			this.MoonsetLabel.AutoSize = true;
			this.MoonsetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MoonsetLabel.Location = new System.Drawing.Point(16, 156);
			this.MoonsetLabel.Name = "MoonsetLabel";
			this.MoonsetLabel.Size = new System.Drawing.Size(62, 20);
			this.MoonsetLabel.TabIndex = 62;
			this.MoonsetLabel.Text = "Today:";
			// 
			// btnTimerSchedules
			// 
			this.btnTimerSchedules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnTimerSchedules.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTimerSchedules.Location = new System.Drawing.Point(21, 303);
			this.btnTimerSchedules.Name = "btnTimerSchedules";
			this.btnTimerSchedules.Size = new System.Drawing.Size(235, 61);
			this.btnTimerSchedules.TabIndex = 3;
			this.btnTimerSchedules.Text = "Timer Schedules";
			this.btnTimerSchedules.UseVisualStyleBackColor = false;
			this.btnTimerSchedules.Click += new System.EventHandler(this.btnTimer_Click);
			// 
			// btnOutdoor
			// 
			this.btnOutdoor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnOutdoor.Enabled = false;
			this.btnOutdoor.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOutdoor.Location = new System.Drawing.Point(1056, 539);
			this.btnOutdoor.Name = "btnOutdoor";
			this.btnOutdoor.Size = new System.Drawing.Size(274, 38);
			this.btnOutdoor.TabIndex = 18;
			this.btnOutdoor.Text = "Outdoor";
			this.btnOutdoor.UseVisualStyleBackColor = false;
			this.btnOutdoor.Click += new System.EventHandler(this.btnOutdoor_Click);
			// 
			// btnSaveJournalEntry
			// 
			this.btnSaveJournalEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnSaveJournalEntry.Location = new System.Drawing.Point(401, 373);
			this.btnSaveJournalEntry.Name = "btnSaveJournalEntry";
			this.btnSaveJournalEntry.Size = new System.Drawing.Size(104, 37);
			this.btnSaveJournalEntry.TabIndex = 21;
			this.btnSaveJournalEntry.Text = "Save";
			this.btnSaveJournalEntry.UseVisualStyleBackColor = true;
			this.btnSaveJournalEntry.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// tbAlarmMinutes
			// 
			this.tbAlarmMinutes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
			this.tbAlarmMinutes.Location = new System.Drawing.Point(126, 58);
			this.tbAlarmMinutes.Name = "tbAlarmMinutes";
			this.tbAlarmMinutes.Size = new System.Drawing.Size(37, 29);
			this.tbAlarmMinutes.TabIndex = 66;
			this.tbAlarmMinutes.Text = "0";
			this.tbAlarmMinutes.TextChanged += new System.EventHandler(this.tbAlarmMinutes_TextChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(82, 29);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(37, 20);
			this.label5.TabIndex = 67;
			this.label5.Text = "Hrs";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(83, 64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(37, 20);
			this.label6.TabIndex = 68;
			this.label6.Text = "Min";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(82, 99);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 20);
			this.label7.TabIndex = 70;
			this.label7.Text = "Sec";
			// 
			// tbAlarmSeconds
			// 
			this.tbAlarmSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
			this.tbAlarmSeconds.Location = new System.Drawing.Point(125, 93);
			this.tbAlarmSeconds.Name = "tbAlarmSeconds";
			this.tbAlarmSeconds.Size = new System.Drawing.Size(38, 29);
			this.tbAlarmSeconds.TabIndex = 69;
			this.tbAlarmSeconds.Text = "0";
			this.tbAlarmSeconds.TextChanged += new System.EventHandler(this.tbAlarmSecondsChanged);
			// 
			// timer2
			// 
			this.timer2.Interval = 1000;
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// tbAlarmTick
			// 
			this.tbAlarmTick.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
			this.tbAlarmTick.Location = new System.Drawing.Point(169, 23);
			this.tbAlarmTick.Name = "tbAlarmTick";
			this.tbAlarmTick.ReadOnly = true;
			this.tbAlarmTick.Size = new System.Drawing.Size(90, 29);
			this.tbAlarmTick.TabIndex = 71;
			// 
			// btnGetTime
			// 
			this.btnGetTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnGetTime.Location = new System.Drawing.Point(859, 440);
			this.btnGetTime.Name = "btnGetTime";
			this.btnGetTime.Size = new System.Drawing.Size(173, 37);
			this.btnGetTime.TabIndex = 13;
			this.btnGetTime.Text = "Get Time";
			this.btnGetTime.UseVisualStyleBackColor = true;
			this.btnGetTime.Click += new System.EventHandler(this.btnGetTime_Click);
			// 
			// tbQuoteOfTheDay
			// 
			this.tbQuoteOfTheDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbQuoteOfTheDay.Location = new System.Drawing.Point(1056, 25);
			this.tbQuoteOfTheDay.Multiline = true;
			this.tbQuoteOfTheDay.Name = "tbQuoteOfTheDay";
			this.tbQuoteOfTheDay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbQuoteOfTheDay.Size = new System.Drawing.Size(286, 92);
			this.tbQuoteOfTheDay.TabIndex = 77;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(1052, 120);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(115, 20);
			this.label11.TabIndex = 78;
			this.label11.Text = "Quote of Day";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.tbAlarmTick);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.tbAlarmSeconds);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.tbAlarmMinutes);
			this.groupBox2.Controls.Add(this.cbAlarm);
			this.groupBox2.Controls.Add(this.tbAlarmHours);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(533, 297);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(285, 146);
			this.groupBox2.TabIndex = 79;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Alarm";
			// 
			// btnFnc4
			// 
			this.btnFnc4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFnc4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFnc4.Location = new System.Drawing.Point(221, 12);
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
			this.btnFnc5.Location = new System.Drawing.Point(287, 12);
			this.btnFnc5.Name = "btnFnc5";
			this.btnFnc5.Size = new System.Drawing.Size(52, 47);
			this.btnFnc5.TabIndex = 26;
			this.btnFnc5.Text = "F5";
			this.btnFnc5.UseVisualStyleBackColor = false;
			this.btnFnc5.Click += new System.EventHandler(this.btnFcn5_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.NextSrssLabel);
			this.groupBox3.Controls.Add(this.btnNextSunrise);
			this.groupBox3.Controls.Add(this.MoonsetLabel);
			this.groupBox3.Controls.Add(this.MoonriseLabel);
			this.groupBox3.Controls.Add(this.SunsetLabel);
			this.groupBox3.Controls.Add(this.SunriseLabel);
			this.groupBox3.Controls.Add(this.btnSunriseSunset);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(1056, 157);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(278, 220);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Sunrise Sunset";
			// 
			// NextSrssLabel
			// 
			this.NextSrssLabel.AutoSize = true;
			this.NextSrssLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.NextSrssLabel.Location = new System.Drawing.Point(17, 182);
			this.NextSrssLabel.Name = "NextSrssLabel";
			this.NextSrssLabel.Size = new System.Drawing.Size(0, 20);
			this.NextSrssLabel.TabIndex = 1;
			// 
			// btnNextSunrise
			// 
			this.btnNextSunrise.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnNextSunrise.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNextSunrise.Location = new System.Drawing.Point(143, 28);
			this.btnNextSunrise.Name = "btnNextSunrise";
			this.btnNextSunrise.Size = new System.Drawing.Size(117, 37);
			this.btnNextSunrise.TabIndex = 1;
			this.btnNextSunrise.Text = "Next";
			this.btnNextSunrise.UseVisualStyleBackColor = false;
			this.btnNextSunrise.Click += new System.EventHandler(this.btnNextSunrise_Click);
			// 
			// btnSendSort
			// 
			this.btnSendSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnSendSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSendSort.Location = new System.Drawing.Point(1060, 396);
			this.btnSendSort.Name = "btnSendSort";
			this.btnSendSort.Size = new System.Drawing.Size(274, 38);
			this.btnSendSort.TabIndex = 15;
			this.btnSendSort.Text = "Sort";
			this.btnSendSort.UseVisualStyleBackColor = false;
			this.btnSendSort.Click += new System.EventHandler(this.btnSendSort_Click);
			// 
			// timer3
			// 
			this.timer3.Interval = 6000000;
			this.timer3.Tick += new System.EventHandler(this.timer3_tick);
			// 
			// tbJournalEntry
			// 
			this.tbJournalEntry.AcceptsReturn = true;
			this.tbJournalEntry.AcceptsTab = true;
			this.tbJournalEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbJournalEntry.Location = new System.Drawing.Point(272, 420);
			this.tbJournalEntry.Multiline = true;
			this.tbJournalEntry.Name = "tbJournalEntry";
			this.tbJournalEntry.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbJournalEntry.Size = new System.Drawing.Size(233, 201);
			this.tbJournalEntry.TabIndex = 84;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label12.Location = new System.Drawing.Point(283, 384);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(112, 20);
			this.label12.TabIndex = 85;
			this.label12.Text = "Daily Journal";
			// 
			// btnUnused
			// 
			this.btnUnused.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnUnused.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUnused.Location = new System.Drawing.Point(23, 224);
			this.btnUnused.Name = "btnUnused";
			this.btnUnused.Size = new System.Drawing.Size(235, 61);
			this.btnUnused.TabIndex = 2;
			this.btnUnused.Text = "Unused";
			this.btnUnused.UseVisualStyleBackColor = false;
			// 
			// FrmSampleClient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(1356, 642);
			this.Controls.Add(this.btnUnused);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.tbJournalEntry);
			this.Controls.Add(this.btnSendSort);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.btnFnc5);
			this.Controls.Add(this.btnFnc4);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.tbQuoteOfTheDay);
			this.Controls.Add(this.btnGetTime);
			this.Controls.Add(this.btnSaveJournalEntry);
			this.Controls.Add(this.btnOutdoor);
			this.Controls.Add(this.btnTimerSchedules);
			this.Controls.Add(this.btnMinimize);
			this.Controls.Add(this.btnFnc2);
			this.Controls.Add(this.tbTime);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.tbTodaysDate);
			this.Controls.Add(this.btnTestBench);
			this.Controls.Add(this.tbSendMsg);
			this.Controls.Add(this.btnExit2Shell);
			this.Controls.Add(this.btnReportTimeUp);
			this.Controls.Add(this.bSetClientTime);
			this.Controls.Add(this.btnSendMsg);
			this.Controls.Add(this.btnSendStatus);
			this.Controls.Add(this.btnShutdownClient);
			this.Controls.Add(this.btnRebootClient);
			this.Controls.Add(this.lbAvailClients);
			this.Controls.Add(this.btnCabin);
			this.Controls.Add(this.btnSettings);
			this.Controls.Add(this.btnGarageForm);
			this.Controls.Add(this.btnFnc1);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnFnc3);
			this.Controls.Add(this.btnRescan);
			this.Controls.Add(this.btnMngServer);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnAssignFunc);
			this.Controls.Add(this.tbReceived);
			this.Controls.Add(this.btnConnect);
			this.MaximizeBox = false;
			this.Name = "FrmSampleClient";
			this.ShowIcon = false;
			this.Text = "SampleClient";
			this.Load += new System.EventHandler(this.FrmSampleClient_Load);
			((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbReceived;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label1;
        private System.Data.DataTable table;
        private System.Data.SqlClient.SqlConnection conn;
//        private System.Data.SqlClient.SqlCommand cmd;


        // desktop
        //string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Daniel\\dev\\Client-SQL-DB.mdf;Integrated Security=True;Connect Timeout=30";
        // laptop

        //string currentconnectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Daniel\\dev\\Client-SQL.mdf;Integrated Security=True;Connect Timeout=30";
        string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Daniel\\dev\\Client-SQL.mdf;Integrated Security=True;Connect Timeout=30";

        private System.Windows.Forms.TextBox tbConnected;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAssignFunc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnMngServer;
        private System.Windows.Forms.Button btnRescan;
        private System.Windows.Forms.Button btnFnc3;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnFnc1;
        private System.Windows.Forms.Button btnGarageForm;
        private System.Windows.Forms.Button btnSettings;
		private System.Windows.Forms.Button btnCabin;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ComboBox cbIPAdress;
		private System.Windows.Forms.ListBox lbAvailClients;
		private System.Windows.Forms.Button btnRebootClient;
		private System.Windows.Forms.Button btnShutdownClient;
		private System.Windows.Forms.Button btnSendStatus;
		private System.Windows.Forms.Button btnSendMsg;
		private System.Windows.Forms.Button bSetClientTime;
		private System.Windows.Forms.Button btnReportTimeUp;
		private System.Windows.Forms.Button btnExit2Shell;
		private System.Windows.Forms.TextBox tbSendMsg;
		private System.Windows.Forms.Button btnTestBench;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cbWhichWinClient;
		private System.Windows.Forms.TextBox tbAlarmHours;
		private System.Windows.Forms.CheckBox cbAlarm;
		private System.Windows.Forms.TextBox tbTodaysDate;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox tbTime;
		private System.Windows.Forms.Button btnFnc2;
		private System.Windows.Forms.Button btnMinimize;
		private System.Windows.Forms.Button btnSunriseSunset;
		private System.Windows.Forms.Label SunriseLabel;
		private System.Windows.Forms.Label SunsetLabel;
		private System.Windows.Forms.Label MoonriseLabel;
		private System.Windows.Forms.Label MoonsetLabel;
		private System.Windows.Forms.Button btnTimerSchedules;
		private System.Windows.Forms.Button btnOutdoor;
		private System.Windows.Forms.Button btnSaveJournalEntry;
		private System.Windows.Forms.TextBox tbAlarmMinutes;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbAlarmSeconds;
		private System.Windows.Forms.Timer timer2;
		private System.Windows.Forms.TextBox tbAlarmTick;
		private System.Windows.Forms.Button btnGetTime;
		private System.Windows.Forms.TextBox tbQuoteOfTheDay;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnFnc4;
		private System.Windows.Forms.Button btnFnc5;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnNextSunrise;
		private System.Windows.Forms.Label NextSrssLabel;
		private System.Windows.Forms.Button btnSendSort;
		private System.Windows.Forms.Timer timer3;
		private System.Windows.Forms.TextBox tbJournalEntry;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button btnUnused;
	}
}

