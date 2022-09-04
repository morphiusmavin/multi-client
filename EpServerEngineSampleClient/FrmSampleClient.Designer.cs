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
			this.btn_PlayList = new System.Windows.Forms.Button();
			this.tbServerTime = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cbWhichWinClient = new System.Windows.Forms.ComboBox();
			this.cbIPAdress = new System.Windows.Forms.ComboBox();
			this.btnShutdown = new System.Windows.Forms.Button();
			this.btnRescan = new System.Windows.Forms.Button();
			this.btnShowParams = new System.Windows.Forms.Button();
			this.btnDBMgmt = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnGetTime = new System.Windows.Forms.Button();
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
			this.btnWaitReboot = new System.Windows.Forms.Button();
			this.tbSendMsg = new System.Windows.Forms.TextBox();
			this.btnCabinLights = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.btnWinClMsg = new System.Windows.Forms.Button();
			this.tbAlarm = new System.Windows.Forms.TextBox();
			this.cbAlarm = new System.Windows.Forms.CheckBox();
			this.tbTest = new System.Windows.Forms.TextBox();
			this.tbTest2 = new System.Windows.Forms.TextBox();
			this.tbTest3 = new System.Windows.Forms.TextBox();
			this.tbTest4 = new System.Windows.Forms.TextBox();
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
			this.tbReceived.Location = new System.Drawing.Point(523, 297);
			this.tbReceived.Multiline = true;
			this.tbReceived.Name = "tbReceived";
			this.tbReceived.ReadOnly = true;
			this.tbReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbReceived.Size = new System.Drawing.Size(300, 233);
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
			this.btnConnect.TabIndex = 15;
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
			// btn_PlayList
			// 
			this.btn_PlayList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btn_PlayList.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_PlayList.Location = new System.Drawing.Point(15, 266);
			this.btn_PlayList.Name = "btn_PlayList";
			this.btn_PlayList.Size = new System.Drawing.Size(233, 61);
			this.btn_PlayList.TabIndex = 4;
			this.btn_PlayList.Text = "Unused";
			this.btn_PlayList.UseVisualStyleBackColor = false;
			this.btn_PlayList.Click += new System.EventHandler(this.Btn_PlayList_Click);
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
			// btnShutdown
			// 
			this.btnShutdown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnShutdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShutdown.Location = new System.Drawing.Point(15, 32);
			this.btnShutdown.Name = "btnShutdown";
			this.btnShutdown.Size = new System.Drawing.Size(235, 61);
			this.btnShutdown.TabIndex = 14;
			this.btnShutdown.Text = "Manage Server";
			this.btnShutdown.UseVisualStyleBackColor = false;
			this.btnShutdown.Click += new System.EventHandler(this.ShutdownServer);
			// 
			// btnRescan
			// 
			this.btnRescan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnRescan.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRescan.Location = new System.Drawing.Point(15, 110);
			this.btnRescan.Name = "btnRescan";
			this.btnRescan.Size = new System.Drawing.Size(235, 61);
			this.btnRescan.TabIndex = 0;
			this.btnRescan.Text = "Re-scan Clients";
			this.btnRescan.UseVisualStyleBackColor = false;
			this.btnRescan.Click += new System.EventHandler(this.RebootServer);
			// 
			// btnShowParams
			// 
			this.btnShowParams.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnShowParams.Enabled = false;
			this.btnShowParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShowParams.Location = new System.Drawing.Point(266, 110);
			this.btnShowParams.Name = "btnShowParams";
			this.btnShowParams.Size = new System.Drawing.Size(235, 61);
			this.btnShowParams.TabIndex = 1;
			this.btnShowParams.Text = "Show Params";
			this.btnShowParams.UseVisualStyleBackColor = false;
			this.btnShowParams.Click += new System.EventHandler(this.ShowParamsClick);
			// 
			// btnDBMgmt
			// 
			this.btnDBMgmt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnDBMgmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDBMgmt.Location = new System.Drawing.Point(15, 188);
			this.btnDBMgmt.Name = "btnDBMgmt";
			this.btnDBMgmt.Size = new System.Drawing.Size(235, 61);
			this.btnDBMgmt.TabIndex = 2;
			this.btnDBMgmt.Text = "Set Next Client";
			this.btnDBMgmt.UseVisualStyleBackColor = false;
			this.btnDBMgmt.Click += new System.EventHandler(this.DBMgmt);
			// 
			// btnClear
			// 
			this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClear.Location = new System.Drawing.Point(262, 422);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(235, 61);
			this.btnClear.TabIndex = 8;
			this.btnClear.Text = "Clear Screen";
			this.btnClear.UseVisualStyleBackColor = false;
			this.btnClear.Click += new System.EventHandler(this.ClearScreen);
			// 
			// btnGetTime
			// 
			this.btnGetTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnGetTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGetTime.Location = new System.Drawing.Point(15, 344);
			this.btnGetTime.Name = "btnGetTime";
			this.btnGetTime.Size = new System.Drawing.Size(235, 61);
			this.btnGetTime.TabIndex = 6;
			this.btnGetTime.Text = "Unused";
			this.btnGetTime.UseVisualStyleBackColor = false;
			this.btnGetTime.Click += new System.EventHandler(this.GetTime);
			// 
			// btnGarageForm
			// 
			this.btnGarageForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnGarageForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnGarageForm.Location = new System.Drawing.Point(266, 188);
			this.btnGarageForm.Name = "btnGarageForm";
			this.btnGarageForm.Size = new System.Drawing.Size(235, 61);
			this.btnGarageForm.TabIndex = 3;
			this.btnGarageForm.Text = "Garage Lights";
			this.btnGarageForm.UseVisualStyleBackColor = false;
			this.btnGarageForm.Click += new System.EventHandler(this.GarageFormClick);
			// 
			// btnHomeSvr
			// 
			this.btnHomeSvr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnHomeSvr.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnHomeSvr.Location = new System.Drawing.Point(15, 422);
			this.btnHomeSvr.Name = "btnHomeSvr";
			this.btnHomeSvr.Size = new System.Drawing.Size(235, 61);
			this.btnHomeSvr.TabIndex = 10;
			this.btnHomeSvr.Text = "Timers";
			this.btnHomeSvr.UseVisualStyleBackColor = false;
			this.btnHomeSvr.Click += new System.EventHandler(this.btnAVR_Click);
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
			this.DialogOne.Click += new System.EventHandler(this.Dialog1_Click);
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
			this.btnRebootClient.TabIndex = 36;
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
			this.btnShutdownClient.TabIndex = 37;
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
			this.btnSendStatus.TabIndex = 38;
			this.btnSendStatus.Text = "Get Status";
			this.btnSendStatus.UseVisualStyleBackColor = true;
			this.btnSendStatus.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnSendMsg
			// 
			this.btnSendMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnSendMsg.Location = new System.Drawing.Point(897, 507);
			this.btnSendMsg.Name = "btnSendMsg";
			this.btnSendMsg.Size = new System.Drawing.Size(219, 37);
			this.btnSendMsg.TabIndex = 39;
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
			this.bSetClientTime.TabIndex = 41;
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
			this.btnReportTimeUp.TabIndex = 42;
			this.btnReportTimeUp.Text = "Report Time Up";
			this.btnReportTimeUp.UseVisualStyleBackColor = true;
			this.btnReportTimeUp.Click += new System.EventHandler(this.btnReportTimeUp_Click);
			// 
			// btnWaitReboot
			// 
			this.btnWaitReboot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnWaitReboot.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnWaitReboot.Location = new System.Drawing.Point(897, 318);
			this.btnWaitReboot.Name = "btnWaitReboot";
			this.btnWaitReboot.Size = new System.Drawing.Size(219, 35);
			this.btnWaitReboot.TabIndex = 43;
			this.btnWaitReboot.Text = "Exit to Shell";
			this.btnWaitReboot.UseVisualStyleBackColor = false;
			this.btnWaitReboot.Click += new System.EventHandler(this.btnWaitReboot_Click);
			// 
			// tbSendMsg
			// 
			this.tbSendMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbSendMsg.Location = new System.Drawing.Point(523, 591);
			this.tbSendMsg.Name = "tbSendMsg";
			this.tbSendMsg.Size = new System.Drawing.Size(300, 26);
			this.tbSendMsg.TabIndex = 44;
			this.tbSendMsg.TextChanged += new System.EventHandler(this.tbSendMsg_TextChanged);
			// 
			// btnCabinLights
			// 
			this.btnCabinLights.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnCabinLights.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCabinLights.Location = new System.Drawing.Point(268, 266);
			this.btnCabinLights.Name = "btnCabinLights";
			this.btnCabinLights.Size = new System.Drawing.Size(233, 61);
			this.btnCabinLights.TabIndex = 45;
			this.btnCabinLights.Text = "Test Bench";
			this.btnCabinLights.UseVisualStyleBackColor = false;
			this.btnCabinLights.Click += new System.EventHandler(this.btnCabinLights_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.button1.Location = new System.Drawing.Point(897, 412);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(219, 37);
			this.button1.TabIndex = 46;
			this.button1.Text = "Shell and Rename";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// btnWinClMsg
			// 
			this.btnWinClMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnWinClMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnWinClMsg.Location = new System.Drawing.Point(15, 496);
			this.btnWinClMsg.Name = "btnWinClMsg";
			this.btnWinClMsg.Size = new System.Drawing.Size(235, 61);
			this.btnWinClMsg.TabIndex = 47;
			this.btnWinClMsg.Text = "Win Cl Msg";
			this.btnWinClMsg.UseVisualStyleBackColor = false;
			this.btnWinClMsg.Click += new System.EventHandler(this.btnWinClMsg_Click);
			// 
			// tbAlarm
			// 
			this.tbAlarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
			this.tbAlarm.Location = new System.Drawing.Point(619, 546);
			this.tbAlarm.Name = "tbAlarm";
			this.tbAlarm.Size = new System.Drawing.Size(202, 29);
			this.tbAlarm.TabIndex = 48;
			this.tbAlarm.TextChanged += new System.EventHandler(this.tbAlarm_TextChanged);
			// 
			// cbAlarm
			// 
			this.cbAlarm.AutoSize = true;
			this.cbAlarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
			this.cbAlarm.Location = new System.Drawing.Point(530, 546);
			this.cbAlarm.Name = "cbAlarm";
			this.cbAlarm.Size = new System.Drawing.Size(83, 28);
			this.cbAlarm.TabIndex = 49;
			this.cbAlarm.Text = "Alarm";
			this.cbAlarm.UseVisualStyleBackColor = true;
			// 
			// tbTest
			// 
			this.tbTest.Enabled = false;
			this.tbTest.Location = new System.Drawing.Point(306, 520);
			this.tbTest.Name = "tbTest";
			this.tbTest.Size = new System.Drawing.Size(100, 20);
			this.tbTest.TabIndex = 50;
			this.tbTest.Visible = false;
			// 
			// tbTest2
			// 
			this.tbTest2.Enabled = false;
			this.tbTest2.Location = new System.Drawing.Point(306, 555);
			this.tbTest2.Name = "tbTest2";
			this.tbTest2.Size = new System.Drawing.Size(100, 20);
			this.tbTest2.TabIndex = 51;
			this.tbTest2.Visible = false;
			// 
			// tbTest3
			// 
			this.tbTest3.Enabled = false;
			this.tbTest3.Location = new System.Drawing.Point(306, 597);
			this.tbTest3.Name = "tbTest3";
			this.tbTest3.Size = new System.Drawing.Size(100, 20);
			this.tbTest3.TabIndex = 52;
			this.tbTest3.Visible = false;
			// 
			// tbTest4
			// 
			this.tbTest4.Enabled = false;
			this.tbTest4.Location = new System.Drawing.Point(306, 638);
			this.tbTest4.Name = "tbTest4";
			this.tbTest4.Size = new System.Drawing.Size(100, 20);
			this.tbTest4.TabIndex = 53;
			this.tbTest4.Visible = false;
			// 
			// FrmSampleClient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(1140, 688);
			this.Controls.Add(this.tbTest4);
			this.Controls.Add(this.tbTest3);
			this.Controls.Add(this.tbTest2);
			this.Controls.Add(this.tbTest);
			this.Controls.Add(this.cbAlarm);
			this.Controls.Add(this.tbAlarm);
			this.Controls.Add(this.btnWinClMsg);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnCabinLights);
			this.Controls.Add(this.tbSendMsg);
			this.Controls.Add(this.btnWaitReboot);
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
			this.Controls.Add(this.btnGetTime);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnDBMgmt);
			this.Controls.Add(this.btnShowParams);
			this.Controls.Add(this.btnRescan);
			this.Controls.Add(this.btnShutdown);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btn_PlayList);
			this.Controls.Add(this.tbReceived);
			this.Controls.Add(this.btnConnect);
			this.Name = "FrmSampleClient";
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
        private System.Windows.Forms.Button btn_PlayList;
        private System.Windows.Forms.TextBox tbServerTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnShutdown;
        private System.Windows.Forms.Button btnRescan;
        private System.Windows.Forms.Button btnShowParams;
        private System.Windows.Forms.Button btnDBMgmt;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnGetTime;
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
		private System.Windows.Forms.Button btnWaitReboot;
		private System.Windows.Forms.TextBox tbSendMsg;
		private System.Windows.Forms.Button btnCabinLights;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cbWhichWinClient;
		private System.Windows.Forms.Button btnWinClMsg;
		private System.Windows.Forms.TextBox tbAlarm;
		private System.Windows.Forms.CheckBox cbAlarm;
		private System.Windows.Forms.TextBox tbTest;
		private System.Windows.Forms.TextBox tbTest2;
		private System.Windows.Forms.TextBox tbTest3;
		private System.Windows.Forms.TextBox tbTest4;
	}
}

