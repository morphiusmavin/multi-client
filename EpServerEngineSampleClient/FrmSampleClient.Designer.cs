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
			this.tbServerTime = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cbWhichWinClient = new System.Windows.Forms.ComboBox();
			this.cbIPAdress = new System.Windows.Forms.ComboBox();
			this.btnMngServer = new System.Windows.Forms.Button();
			this.btnRescan = new System.Windows.Forms.Button();
			this.btnFnc3 = new System.Windows.Forms.Button();
			this.btnDBMgmt = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnFnc1 = new System.Windows.Forms.Button();
			this.btnGarageForm = new System.Windows.Forms.Button();
			this.btnHomeSvr = new System.Windows.Forms.Button();
			this.DialogOne = new System.Windows.Forms.Button();
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
			this.btnCabinLights = new System.Windows.Forms.Button();
			this.btnShellandRename = new System.Windows.Forms.Button();
			this.btnWinClMsg = new System.Windows.Forms.Button();
			this.tbAlarm = new System.Windows.Forms.TextBox();
			this.cbAlarm = new System.Windows.Forms.CheckBox();
			this.tbTodaysDate = new System.Windows.Forms.TextBox();
			this.tbSunrise = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.tbSunset = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.tbTime = new System.Windows.Forms.TextBox();
			this.btnFnc2 = new System.Windows.Forms.Button();
			this.btnMinimize = new System.Windows.Forms.Button();
			this.btnTest = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbReceived
			// 
			this.tbReceived.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbReceived.Location = new System.Drawing.Point(526, 412);
			this.tbReceived.Multiline = true;
			this.tbReceived.Name = "tbReceived";
			this.tbReceived.ReadOnly = true;
			this.tbReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbReceived.Size = new System.Drawing.Size(300, 180);
			this.tbReceived.TabIndex = 16;
			this.tbReceived.TabStop = false;
			// 
			// btnConnect
			// 
			this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnConnect.Location = new System.Drawing.Point(266, 32);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(233, 61);
			this.btnConnect.TabIndex = 1;
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
			this.btnAssignFunc.Location = new System.Drawing.Point(15, 266);
			this.btnAssignFunc.Name = "btnAssignFunc";
			this.btnAssignFunc.Size = new System.Drawing.Size(233, 61);
			this.btnAssignFunc.TabIndex = 6;
			this.btnAssignFunc.Text = "Assign Func";
			this.btnAssignFunc.UseVisualStyleBackColor = false;
			this.btnAssignFunc.Click += new System.EventHandler(this.BtnAssignFunction);
			// 
			// tbServerTime
			// 
			this.tbServerTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbServerTime.Location = new System.Drawing.Point(103, 147);
			this.tbServerTime.Name = "tbServerTime";
			this.tbServerTime.ReadOnly = true;
			this.tbServerTime.Size = new System.Drawing.Size(170, 29);
			this.tbServerTime.TabIndex = 30;
			this.tbServerTime.TabStop = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(8, 155);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(94, 20);
			this.label5.TabIndex = 31;
			this.label5.Text = "Server Up:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.cbWhichWinClient);
			this.groupBox1.Controls.Add(this.cbIPAdress);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.tbServerTime);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.tbConnected);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.tbPort);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(523, 30);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(300, 243);
			this.groupBox1.TabIndex = 34;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "TCP Status";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(8, 198);
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
			this.cbWhichWinClient.Location = new System.Drawing.Point(101, 192);
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
			this.btnMngServer.Location = new System.Drawing.Point(15, 32);
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
			this.btnRescan.Location = new System.Drawing.Point(15, 110);
			this.btnRescan.Name = "btnRescan";
			this.btnRescan.Size = new System.Drawing.Size(235, 61);
			this.btnRescan.TabIndex = 2;
			this.btnRescan.Text = "Re-scan Clients";
			this.btnRescan.UseVisualStyleBackColor = false;
			this.btnRescan.Click += new System.EventHandler(this.RebootServer);
			// 
			// btnFnc3
			// 
			this.btnFnc3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFnc3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFnc3.Location = new System.Drawing.Point(12, 498);
			this.btnFnc3.Name = "btnFnc3";
			this.btnFnc3.Size = new System.Drawing.Size(235, 61);
			this.btnFnc3.TabIndex = 12;
			this.btnFnc3.Text = "Function 3";
			this.btnFnc3.UseVisualStyleBackColor = false;
			this.btnFnc3.Click += new System.EventHandler(this.Function3Click);
			// 
			// btnDBMgmt
			// 
			this.btnDBMgmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnDBMgmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDBMgmt.Location = new System.Drawing.Point(15, 188);
			this.btnDBMgmt.Name = "btnDBMgmt";
			this.btnDBMgmt.Size = new System.Drawing.Size(235, 61);
			this.btnDBMgmt.TabIndex = 4;
			this.btnDBMgmt.Text = "Set Next Client";
			this.btnDBMgmt.UseVisualStyleBackColor = false;
			this.btnDBMgmt.Click += new System.EventHandler(this.btnSetNextClient_Click);
			// 
			// btnClear
			// 
			this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClear.Location = new System.Drawing.Point(263, 422);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(235, 61);
			this.btnClear.TabIndex = 11;
			this.btnClear.Text = "Clear Screen";
			this.btnClear.UseVisualStyleBackColor = false;
			this.btnClear.Click += new System.EventHandler(this.ClearScreen);
			// 
			// btnFnc1
			// 
			this.btnFnc1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFnc1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFnc1.Location = new System.Drawing.Point(15, 344);
			this.btnFnc1.Name = "btnFnc1";
			this.btnFnc1.Size = new System.Drawing.Size(235, 61);
			this.btnFnc1.TabIndex = 8;
			this.btnFnc1.Text = "Function 1";
			this.btnFnc1.UseVisualStyleBackColor = false;
			this.btnFnc1.Click += new System.EventHandler(this.Function1Click);
			// 
			// btnGarageForm
			// 
			this.btnGarageForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnGarageForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGarageForm.Location = new System.Drawing.Point(266, 188);
			this.btnGarageForm.Name = "btnGarageForm";
			this.btnGarageForm.Size = new System.Drawing.Size(235, 61);
			this.btnGarageForm.TabIndex = 5;
			this.btnGarageForm.Text = "Garage Lights";
			this.btnGarageForm.UseVisualStyleBackColor = false;
			this.btnGarageForm.Click += new System.EventHandler(this.GarageFormClick);
			// 
			// btnHomeSvr
			// 
			this.btnHomeSvr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnHomeSvr.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnHomeSvr.Location = new System.Drawing.Point(266, 110);
			this.btnHomeSvr.Name = "btnHomeSvr";
			this.btnHomeSvr.Size = new System.Drawing.Size(235, 61);
			this.btnHomeSvr.TabIndex = 3;
			this.btnHomeSvr.Text = "Timers";
			this.btnHomeSvr.UseVisualStyleBackColor = false;
			this.btnHomeSvr.Click += new System.EventHandler(this.btnTimers_Click);
			// 
			// DialogOne
			// 
			this.DialogOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.DialogOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DialogOne.Location = new System.Drawing.Point(264, 344);
			this.DialogOne.Name = "DialogOne";
			this.DialogOne.Size = new System.Drawing.Size(235, 61);
			this.DialogOne.TabIndex = 9;
			this.DialogOne.Text = "Cabin";
			this.DialogOne.UseVisualStyleBackColor = false;
			this.DialogOne.Click += new System.EventHandler(this.Cabin_Click);
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
			this.lbAvailClients.Location = new System.Drawing.Point(871, 32);
			this.lbAvailClients.Name = "lbAvailClients";
			this.lbAvailClients.Size = new System.Drawing.Size(245, 199);
			this.lbAvailClients.TabIndex = 35;
			this.lbAvailClients.SelectedIndexChanged += new System.EventHandler(this.AvailClientSelIndexChanged);
			// 
			// btnRebootClient
			// 
			this.btnRebootClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnRebootClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnRebootClient.Location = new System.Drawing.Point(897, 272);
			this.btnRebootClient.Name = "btnRebootClient";
			this.btnRebootClient.Size = new System.Drawing.Size(219, 35);
			this.btnRebootClient.TabIndex = 21;
			this.btnRebootClient.Text = "Reboot";
			this.btnRebootClient.UseVisualStyleBackColor = false;
			this.btnRebootClient.Click += new System.EventHandler(this.btnRebootClient_Click);
			// 
			// btnShutdownClient
			// 
			this.btnShutdownClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnShutdownClient.Location = new System.Drawing.Point(897, 364);
			this.btnShutdownClient.Name = "btnShutdownClient";
			this.btnShutdownClient.Size = new System.Drawing.Size(219, 37);
			this.btnShutdownClient.TabIndex = 23;
			this.btnShutdownClient.Text = "Shutdown";
			this.btnShutdownClient.UseVisualStyleBackColor = true;
			this.btnShutdownClient.Click += new System.EventHandler(this.btnShutdownClient_Click);
			// 
			// btnSendStatus
			// 
			this.btnSendStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnSendStatus.Location = new System.Drawing.Point(897, 459);
			this.btnSendStatus.Name = "btnSendStatus";
			this.btnSendStatus.Size = new System.Drawing.Size(219, 37);
			this.btnSendStatus.TabIndex = 25;
			this.btnSendStatus.Text = "Get Status";
			this.btnSendStatus.UseVisualStyleBackColor = true;
			this.btnSendStatus.Click += new System.EventHandler(this.btnSendStatus_Click);
			// 
			// btnSendMsg
			// 
			this.btnSendMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnSendMsg.Location = new System.Drawing.Point(897, 507);
			this.btnSendMsg.Name = "btnSendMsg";
			this.btnSendMsg.Size = new System.Drawing.Size(219, 37);
			this.btnSendMsg.TabIndex = 26;
			this.btnSendMsg.Text = "Send Message";
			this.btnSendMsg.UseVisualStyleBackColor = true;
			this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
			// 
			// bSetClientTime
			// 
			this.bSetClientTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.bSetClientTime.Location = new System.Drawing.Point(897, 555);
			this.bSetClientTime.Name = "bSetClientTime";
			this.bSetClientTime.Size = new System.Drawing.Size(219, 37);
			this.bSetClientTime.TabIndex = 27;
			this.bSetClientTime.Text = "Set Time";
			this.bSetClientTime.UseVisualStyleBackColor = true;
			this.bSetClientTime.Click += new System.EventHandler(this.bSetClientTime_Click);
			// 
			// btnReportTimeUp
			// 
			this.btnReportTimeUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnReportTimeUp.Location = new System.Drawing.Point(897, 603);
			this.btnReportTimeUp.Name = "btnReportTimeUp";
			this.btnReportTimeUp.Size = new System.Drawing.Size(219, 37);
			this.btnReportTimeUp.TabIndex = 28;
			this.btnReportTimeUp.Text = "Report Time Up";
			this.btnReportTimeUp.UseVisualStyleBackColor = true;
			this.btnReportTimeUp.Click += new System.EventHandler(this.btnReportTimeUp_Click);
			// 
			// btnExit2Shell
			// 
			this.btnExit2Shell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnExit2Shell.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnExit2Shell.Location = new System.Drawing.Point(897, 318);
			this.btnExit2Shell.Name = "btnExit2Shell";
			this.btnExit2Shell.Size = new System.Drawing.Size(219, 35);
			this.btnExit2Shell.TabIndex = 22;
			this.btnExit2Shell.Text = "Exit to Shell";
			this.btnExit2Shell.UseVisualStyleBackColor = false;
			this.btnExit2Shell.Click += new System.EventHandler(this.Exit2Shell_Click);
			// 
			// tbSendMsg
			// 
			this.tbSendMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbSendMsg.Location = new System.Drawing.Point(528, 648);
			this.tbSendMsg.Name = "tbSendMsg";
			this.tbSendMsg.Size = new System.Drawing.Size(300, 26);
			this.tbSendMsg.TabIndex = 20;
			this.tbSendMsg.TextChanged += new System.EventHandler(this.tbSendMsg_TextChanged);
			// 
			// btnCabinLights
			// 
			this.btnCabinLights.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnCabinLights.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCabinLights.Location = new System.Drawing.Point(268, 266);
			this.btnCabinLights.Name = "btnCabinLights";
			this.btnCabinLights.Size = new System.Drawing.Size(233, 61);
			this.btnCabinLights.TabIndex = 7;
			this.btnCabinLights.Text = "Test Bench";
			this.btnCabinLights.UseVisualStyleBackColor = false;
			this.btnCabinLights.Click += new System.EventHandler(this.btnTestBench_Click);
			// 
			// btnShellandRename
			// 
			this.btnShellandRename.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnShellandRename.Location = new System.Drawing.Point(897, 412);
			this.btnShellandRename.Name = "btnShellandRename";
			this.btnShellandRename.Size = new System.Drawing.Size(219, 37);
			this.btnShellandRename.TabIndex = 24;
			this.btnShellandRename.Text = "Shell and Rename";
			this.btnShellandRename.UseVisualStyleBackColor = true;
			this.btnShellandRename.Click += new System.EventHandler(this.btnShellandRename_Click);
			// 
			// btnWinClMsg
			// 
			this.btnWinClMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnWinClMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnWinClMsg.Location = new System.Drawing.Point(262, 498);
			this.btnWinClMsg.Name = "btnWinClMsg";
			this.btnWinClMsg.Size = new System.Drawing.Size(235, 61);
			this.btnWinClMsg.TabIndex = 13;
			this.btnWinClMsg.Text = "Win Cl Msg";
			this.btnWinClMsg.UseVisualStyleBackColor = false;
			this.btnWinClMsg.Click += new System.EventHandler(this.btnWinClMsg_Click);
			// 
			// tbAlarm
			// 
			this.tbAlarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
			this.tbAlarm.Location = new System.Drawing.Point(624, 603);
			this.tbAlarm.Name = "tbAlarm";
			this.tbAlarm.Size = new System.Drawing.Size(202, 29);
			this.tbAlarm.TabIndex = 19;
			this.tbAlarm.TextChanged += new System.EventHandler(this.tbAlarm_TextChanged);
			// 
			// cbAlarm
			// 
			this.cbAlarm.AutoSize = true;
			this.cbAlarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
			this.cbAlarm.Location = new System.Drawing.Point(535, 603);
			this.cbAlarm.Name = "cbAlarm";
			this.cbAlarm.Size = new System.Drawing.Size(83, 28);
			this.cbAlarm.TabIndex = 49;
			this.cbAlarm.Text = "Alarm";
			this.cbAlarm.UseVisualStyleBackColor = true;
			// 
			// tbTodaysDate
			// 
			this.tbTodaysDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.tbTodaysDate.Location = new System.Drawing.Point(613, 287);
			this.tbTodaysDate.Name = "tbTodaysDate";
			this.tbTodaysDate.Size = new System.Drawing.Size(103, 26);
			this.tbTodaysDate.TabIndex = 15;
			// 
			// tbSunrise
			// 
			this.tbSunrise.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.tbSunrise.Location = new System.Drawing.Point(613, 327);
			this.tbSunrise.Name = "tbSunrise";
			this.tbSunrise.Size = new System.Drawing.Size(64, 26);
			this.tbSunrise.TabIndex = 17;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(532, 330);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(75, 20);
			this.label6.TabIndex = 52;
			this.label6.Text = "Sunrise:";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(532, 367);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(71, 20);
			this.label7.TabIndex = 54;
			this.label7.Text = "Sunset:";
			// 
			// tbSunset
			// 
			this.tbSunset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.tbSunset.Location = new System.Drawing.Point(613, 364);
			this.tbSunset.Name = "tbSunset";
			this.tbSunset.Size = new System.Drawing.Size(64, 26);
			this.tbSunset.TabIndex = 18;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(531, 293);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(62, 20);
			this.label10.TabIndex = 57;
			this.label10.Text = "Today:";
			// 
			// tbTime
			// 
			this.tbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.tbTime.Location = new System.Drawing.Point(725, 287);
			this.tbTime.Name = "tbTime";
			this.tbTime.Size = new System.Drawing.Size(103, 26);
			this.tbTime.TabIndex = 16;
			// 
			// btnFnc2
			// 
			this.btnFnc2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnFnc2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFnc2.Location = new System.Drawing.Point(13, 422);
			this.btnFnc2.Name = "btnFnc2";
			this.btnFnc2.Size = new System.Drawing.Size(235, 61);
			this.btnFnc2.TabIndex = 10;
			this.btnFnc2.Text = "Function 2";
			this.btnFnc2.UseVisualStyleBackColor = false;
			this.btnFnc2.Click += new System.EventHandler(this.Function2Click);
			// 
			// btnMinimize
			// 
			this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnMinimize.Location = new System.Drawing.Point(12, 579);
			this.btnMinimize.Name = "btnMinimize";
			this.btnMinimize.Size = new System.Drawing.Size(235, 61);
			this.btnMinimize.TabIndex = 14;
			this.btnMinimize.Text = "Minimize";
			this.btnMinimize.UseVisualStyleBackColor = false;
			this.btnMinimize.Click += new System.EventHandler(this.Minimize_Click);
			// 
			// btnTest
			// 
			this.btnTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTest.Location = new System.Drawing.Point(262, 579);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(235, 61);
			this.btnTest.TabIndex = 58;
			this.btnTest.Text = "Test";
			this.btnTest.UseVisualStyleBackColor = false;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// FrmSampleClient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(1140, 688);
			this.Controls.Add(this.btnTest);
			this.Controls.Add(this.btnMinimize);
			this.Controls.Add(this.btnFnc2);
			this.Controls.Add(this.tbTime);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.tbSunset);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.tbSunrise);
			this.Controls.Add(this.tbTodaysDate);
			this.Controls.Add(this.cbAlarm);
			this.Controls.Add(this.tbAlarm);
			this.Controls.Add(this.btnWinClMsg);
			this.Controls.Add(this.btnShellandRename);
			this.Controls.Add(this.btnCabinLights);
			this.Controls.Add(this.tbSendMsg);
			this.Controls.Add(this.btnExit2Shell);
			this.Controls.Add(this.btnReportTimeUp);
			this.Controls.Add(this.bSetClientTime);
			this.Controls.Add(this.btnSendMsg);
			this.Controls.Add(this.btnSendStatus);
			this.Controls.Add(this.btnShutdownClient);
			this.Controls.Add(this.btnRebootClient);
			this.Controls.Add(this.lbAvailClients);
			this.Controls.Add(this.DialogOne);
			this.Controls.Add(this.btnHomeSvr);
			this.Controls.Add(this.btnGarageForm);
			this.Controls.Add(this.btnFnc1);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnDBMgmt);
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
        private System.Windows.Forms.TextBox tbServerTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnMngServer;
        private System.Windows.Forms.Button btnRescan;
        private System.Windows.Forms.Button btnFnc3;
        private System.Windows.Forms.Button btnDBMgmt;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnFnc1;
        private System.Windows.Forms.Button btnGarageForm;
        private System.Windows.Forms.Button btnHomeSvr;
		private System.Windows.Forms.Button DialogOne;
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
		private System.Windows.Forms.Button btnCabinLights;
		private System.Windows.Forms.Button btnShellandRename;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cbWhichWinClient;
		private System.Windows.Forms.Button btnWinClMsg;
		private System.Windows.Forms.TextBox tbAlarm;
		private System.Windows.Forms.CheckBox cbAlarm;
		private System.Windows.Forms.TextBox tbTodaysDate;
		private System.Windows.Forms.TextBox tbSunrise;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbSunset;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox tbTime;
		private System.Windows.Forms.Button btnFnc2;
		private System.Windows.Forms.Button btnMinimize;
		private System.Windows.Forms.Button btnTest;
	}
}

