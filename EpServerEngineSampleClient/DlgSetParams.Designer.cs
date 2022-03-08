namespace EpServerEngineSampleClient
{
    partial class DlgSetParams
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
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbTempLimit = new System.Windows.Forms.ComboBox();
			this.label17 = new System.Windows.Forms.Label();
			this.cbFanOff = new System.Windows.Forms.ComboBox();
			this.cbFanOn = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.cbHighRevLimit = new System.Windows.Forms.ComboBox();
			this.cbLowRevLimit = new System.Windows.Forms.ComboBox();
			this.cbLightsOnDelay = new System.Windows.Forms.ComboBox();
			this.cbFPGAXmitRate = new System.Windows.Forms.ComboBox();
			this.cbRPM_MPHUpdateRate = new System.Windows.Forms.ComboBox();
			this.cbTestBank = new System.Windows.Forms.ComboBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.cbPasswordTimeout = new System.Windows.Forms.ComboBox();
			this.label18 = new System.Windows.Forms.Label();
			this.cbPasswordRetries = new System.Windows.Forms.ComboBox();
			this.btnChangePassword = new System.Windows.Forms.Button();
			this.tbNewPasssword = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.cbBaudRate3 = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.btnHighRev = new System.Windows.Forms.Button();
			this.btnLowRev = new System.Windows.Forms.Button();
			this.btnXmitRate = new System.Windows.Forms.Button();
			this.btnRPMMPH = new System.Windows.Forms.Button();
			this.btnRevLimitOR = new System.Windows.Forms.Button();
			this.btnFPOR = new System.Windows.Forms.Button();
			this.btnTestMode = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.btnScrollDown = new System.Windows.Forms.Button();
			this.btnScrollUp = new System.Windows.Forms.Button();
			this.btnShiftLeft = new System.Windows.Forms.Button();
			this.btnShiftRight = new System.Windows.Forms.Button();
			this.tbDebug = new System.Windows.Forms.TextBox();
			this.btnClearLCD = new System.Windows.Forms.Button();
			this.btnShowFPGAStatus = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.cbLightsOnValue = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbLightsOffValue = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cbADCRate = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnDimmer = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOK.Location = new System.Drawing.Point(666, 570);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(99, 41);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.OKClicked);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(775, 570);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(99, 41);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.CancelClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(154, 25);
			this.label1.TabIndex = 5;
			this.label1.Text = "Fan On Temp";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(24, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(155, 25);
			this.label2.TabIndex = 6;
			this.label2.Text = "Fan Off Temp";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cbTempLimit);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.cbFanOff);
			this.groupBox1.Controls.Add(this.cbFanOn);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(435, 169);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Temperatures";
			// 
			// cbTempLimit
			// 
			this.cbTempLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbTempLimit.FormattingEnabled = true;
			this.cbTempLimit.Location = new System.Drawing.Point(299, 114);
			this.cbTempLimit.Name = "cbTempLimit";
			this.cbTempLimit.Size = new System.Drawing.Size(106, 33);
			this.cbTempLimit.TabIndex = 42;
			this.cbTempLimit.SelectedIndexChanged += new System.EventHandler(this.cbTempLimit_selected_index_changed);
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label17.Location = new System.Drawing.Point(24, 119);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(207, 25);
			this.label17.TabIndex = 41;
			this.label17.Text = "Engine Temp Limit";
			// 
			// cbFanOff
			// 
			this.cbFanOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbFanOff.FormattingEnabled = true;
			this.cbFanOff.Location = new System.Drawing.Point(298, 70);
			this.cbFanOff.Name = "cbFanOff";
			this.cbFanOff.Size = new System.Drawing.Size(106, 33);
			this.cbFanOff.TabIndex = 31;
			this.cbFanOff.SelectedIndexChanged += new System.EventHandler(this.cbFanOff_SelectedIndexChanged);
			// 
			// cbFanOn
			// 
			this.cbFanOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbFanOn.FormattingEnabled = true;
			this.cbFanOn.Location = new System.Drawing.Point(298, 27);
			this.cbFanOn.Name = "cbFanOn";
			this.cbFanOn.Size = new System.Drawing.Size(106, 33);
			this.cbFanOn.TabIndex = 30;
			this.cbFanOn.SelectedIndexChanged += new System.EventHandler(this.cbFanOn_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(521, 197);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(0, 25);
			this.label7.TabIndex = 19;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(543, 271);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(0, 25);
			this.label8.TabIndex = 20;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(487, 26);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(180, 25);
			this.label11.TabIndex = 23;
			this.label11.Text = "Lights On Delay";
			// 
			// cbHighRevLimit
			// 
			this.cbHighRevLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbHighRevLimit.FormattingEnabled = true;
			this.cbHighRevLimit.Items.AddRange(new object[] {
            "6000",
            "5800",
            "5600",
            "5400",
            "5200",
            "5000",
            "4800",
            "4600",
            "4200",
            "4000"});
			this.cbHighRevLimit.Location = new System.Drawing.Point(271, 26);
			this.cbHighRevLimit.Name = "cbHighRevLimit";
			this.cbHighRevLimit.Size = new System.Drawing.Size(133, 33);
			this.cbHighRevLimit.TabIndex = 31;
			this.cbHighRevLimit.SelectedIndexChanged += new System.EventHandler(this.cbHighRevLimit_SelectedIndexChanged);
			// 
			// cbLowRevLimit
			// 
			this.cbLowRevLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbLowRevLimit.FormattingEnabled = true;
			this.cbLowRevLimit.Items.AddRange(new object[] {
            "4500",
            "4400",
            "4300",
            "4200",
            "4100",
            "4000",
            "3900",
            "3800",
            "3700"});
			this.cbLowRevLimit.Location = new System.Drawing.Point(271, 70);
			this.cbLowRevLimit.Name = "cbLowRevLimit";
			this.cbLowRevLimit.Size = new System.Drawing.Size(134, 33);
			this.cbLowRevLimit.TabIndex = 32;
			this.cbLowRevLimit.SelectedIndexChanged += new System.EventHandler(this.cbLowRevLimit_SelectedIndexChanged);
			// 
			// cbLightsOnDelay
			// 
			this.cbLightsOnDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbLightsOnDelay.FormattingEnabled = true;
			this.cbLightsOnDelay.Items.AddRange(new object[] {
            "1 second",
            "2 seconds",
            "3 seconds",
            "5 seconds",
            "10 seconds",
            "15 seconds",
            "30 seconds",
            "1 minute",
            "2 minutes",
            "5 minutes",
            "10 minutes",
            "30 minutes",
            "1 hour"});
			this.cbLightsOnDelay.Location = new System.Drawing.Point(725, 20);
			this.cbLightsOnDelay.Name = "cbLightsOnDelay";
			this.cbLightsOnDelay.Size = new System.Drawing.Size(141, 33);
			this.cbLightsOnDelay.TabIndex = 33;
			this.cbLightsOnDelay.SelectedIndexChanged += new System.EventHandler(this.cbLightsOnDelay_SelectedIndexChanged);
			// 
			// cbFPGAXmitRate
			// 
			this.cbFPGAXmitRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbFPGAXmitRate.FormattingEnabled = true;
			this.cbFPGAXmitRate.Items.AddRange(new object[] {
            "1000",
            "900",
            "800",
            "700",
            "600",
            "500",
            "400"});
			this.cbFPGAXmitRate.Location = new System.Drawing.Point(271, 115);
			this.cbFPGAXmitRate.Name = "cbFPGAXmitRate";
			this.cbFPGAXmitRate.Size = new System.Drawing.Size(134, 33);
			this.cbFPGAXmitRate.TabIndex = 34;
			this.cbFPGAXmitRate.SelectedIndexChanged += new System.EventHandler(this.cbFPGAXmitRate_SelectedIndexChanged);
			// 
			// cbRPM_MPHUpdateRate
			// 
			this.cbRPM_MPHUpdateRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbRPM_MPHUpdateRate.FormattingEnabled = true;
			this.cbRPM_MPHUpdateRate.Items.AddRange(new object[] {
            "1000",
            "900",
            "800",
            "700",
            "600",
            "500",
            "400",
            "300",
            "200",
            "100",
            "50"});
			this.cbRPM_MPHUpdateRate.Location = new System.Drawing.Point(272, 161);
			this.cbRPM_MPHUpdateRate.Name = "cbRPM_MPHUpdateRate";
			this.cbRPM_MPHUpdateRate.Size = new System.Drawing.Size(133, 33);
			this.cbRPM_MPHUpdateRate.TabIndex = 35;
			this.cbRPM_MPHUpdateRate.SelectedIndexChanged += new System.EventHandler(this.cbRPMUpdateRate_SelectedIndexChanged);
			// 
			// cbTestBank
			// 
			this.cbTestBank.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbTestBank.FormattingEnabled = true;
			this.cbTestBank.Items.AddRange(new object[] {
            "On",
            "Off"});
			this.cbTestBank.Location = new System.Drawing.Point(725, 199);
			this.cbTestBank.Name = "cbTestBank";
			this.cbTestBank.Size = new System.Drawing.Size(141, 33);
			this.cbTestBank.TabIndex = 37;
			this.cbTestBank.SelectedIndexChanged += new System.EventHandler(this.cbTestBank_SelectedIndexChanged);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.Location = new System.Drawing.Point(487, 205);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(143, 25);
			this.label15.TabIndex = 38;
			this.label15.Text = "Serial Port 2";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.Location = new System.Drawing.Point(487, 246);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(212, 25);
			this.label16.TabIndex = 40;
			this.label16.Text = "Password Timeout:";
			// 
			// cbPasswordTimeout
			// 
			this.cbPasswordTimeout.AutoCompleteCustomSource.AddRange(new string[] {
            "10 seconds",
            "15 seconds",
            "30 seconds",
            "1 minute",
            "2 minutes",
            "5 minutes",
            "10 minutes"});
			this.cbPasswordTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbPasswordTimeout.FormattingEnabled = true;
			this.cbPasswordTimeout.Items.AddRange(new object[] {
            "10 seconds",
            "15 seconds",
            "30 seconds",
            "1 minute",
            "2 minutes",
            "5 minutes",
            "10 minutes"});
			this.cbPasswordTimeout.Location = new System.Drawing.Point(725, 244);
			this.cbPasswordTimeout.Name = "cbPasswordTimeout";
			this.cbPasswordTimeout.Size = new System.Drawing.Size(141, 33);
			this.cbPasswordTimeout.TabIndex = 39;
			this.cbPasswordTimeout.SelectedIndexChanged += new System.EventHandler(this.cbPasswordTimeout_SelectedIndexChanged);
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label18.Location = new System.Drawing.Point(487, 292);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(203, 25);
			this.label18.TabIndex = 42;
			this.label18.Text = "Password Retries:";
			// 
			// cbPasswordRetries
			// 
			this.cbPasswordRetries.AutoCompleteCustomSource.AddRange(new string[] {
            "2",
            "3",
            "4",
            "5",
            "6"});
			this.cbPasswordRetries.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbPasswordRetries.FormattingEnabled = true;
			this.cbPasswordRetries.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6"});
			this.cbPasswordRetries.Location = new System.Drawing.Point(725, 289);
			this.cbPasswordRetries.Name = "cbPasswordRetries";
			this.cbPasswordRetries.Size = new System.Drawing.Size(141, 33);
			this.cbPasswordRetries.TabIndex = 41;
			this.cbPasswordRetries.SelectedIndexChanged += new System.EventHandler(this.cbPasswordRetries_SelectedIndexChanged);
			// 
			// btnChangePassword
			// 
			this.btnChangePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnChangePassword.Location = new System.Drawing.Point(11, 517);
			this.btnChangePassword.Name = "btnChangePassword";
			this.btnChangePassword.Size = new System.Drawing.Size(223, 34);
			this.btnChangePassword.TabIndex = 43;
			this.btnChangePassword.Text = "Change Password";
			this.btnChangePassword.UseVisualStyleBackColor = true;
			this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Clicked);
			// 
			// tbNewPasssword
			// 
			this.tbNewPasssword.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbNewPasssword.Location = new System.Drawing.Point(11, 557);
			this.tbNewPasssword.Name = "tbNewPasssword";
			this.tbNewPasssword.Size = new System.Drawing.Size(223, 29);
			this.tbNewPasssword.TabIndex = 44;
			this.tbNewPasssword.Visible = false;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label19.Location = new System.Drawing.Point(482, 337);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(199, 25);
			this.label19.TabIndex = 47;
			this.label19.Text = "Comm3 Baudrate:";
			// 
			// cbBaudRate3
			// 
			this.cbBaudRate3.AutoCompleteCustomSource.AddRange(new string[] {
            "2",
            "3",
            "4",
            "5",
            "6"});
			this.cbBaudRate3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbBaudRate3.FormattingEnabled = true;
			this.cbBaudRate3.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400"});
			this.cbBaudRate3.Location = new System.Drawing.Point(726, 334);
			this.cbBaudRate3.Name = "cbBaudRate3";
			this.cbBaudRate3.Size = new System.Drawing.Size(141, 33);
			this.cbBaudRate3.TabIndex = 46;
			this.cbBaudRate3.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.Location = new System.Drawing.Point(557, 570);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(99, 41);
			this.button1.TabIndex = 49;
			this.button1.Text = "Refresh";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnHighRev
			// 
			this.btnHighRev.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnHighRev.Location = new System.Drawing.Point(31, 26);
			this.btnHighRev.Name = "btnHighRev";
			this.btnHighRev.Size = new System.Drawing.Size(222, 33);
			this.btnHighRev.TabIndex = 51;
			this.btnHighRev.Text = "Update High Rev";
			this.btnHighRev.UseVisualStyleBackColor = true;
			this.btnHighRev.Click += new System.EventHandler(this.btnHighRev_Click);
			// 
			// btnLowRev
			// 
			this.btnLowRev.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLowRev.Location = new System.Drawing.Point(31, 70);
			this.btnLowRev.Name = "btnLowRev";
			this.btnLowRev.Size = new System.Drawing.Size(222, 33);
			this.btnLowRev.TabIndex = 52;
			this.btnLowRev.Text = "Update Low Rev";
			this.btnLowRev.UseVisualStyleBackColor = true;
			this.btnLowRev.Click += new System.EventHandler(this.btnLowRev_Click);
			// 
			// btnXmitRate
			// 
			this.btnXmitRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnXmitRate.Location = new System.Drawing.Point(31, 115);
			this.btnXmitRate.Name = "btnXmitRate";
			this.btnXmitRate.Size = new System.Drawing.Size(222, 33);
			this.btnXmitRate.TabIndex = 53;
			this.btnXmitRate.Text = "Update Xmit Rate";
			this.btnXmitRate.UseVisualStyleBackColor = true;
			this.btnXmitRate.Click += new System.EventHandler(this.btnXmitRate_Click);
			// 
			// btnRPMMPH
			// 
			this.btnRPMMPH.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRPMMPH.Location = new System.Drawing.Point(31, 161);
			this.btnRPMMPH.Name = "btnRPMMPH";
			this.btnRPMMPH.Size = new System.Drawing.Size(222, 33);
			this.btnRPMMPH.TabIndex = 54;
			this.btnRPMMPH.Text = "RPM/MPH Rate";
			this.btnRPMMPH.UseVisualStyleBackColor = true;
			this.btnRPMMPH.Click += new System.EventHandler(this.btnRPMMPH_Click);
			// 
			// btnRevLimitOR
			// 
			this.btnRevLimitOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRevLimitOR.Location = new System.Drawing.Point(216, 246);
			this.btnRevLimitOR.Name = "btnRevLimitOR";
			this.btnRevLimitOR.Size = new System.Drawing.Size(189, 34);
			this.btnRevLimitOR.TabIndex = 55;
			this.btnRevLimitOR.Text = "Set RLOR";
			this.btnRevLimitOR.UseVisualStyleBackColor = true;
			this.btnRevLimitOR.Click += new System.EventHandler(this.btnRevLimitOR_Click);
			// 
			// btnFPOR
			// 
			this.btnFPOR.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFPOR.Location = new System.Drawing.Point(31, 246);
			this.btnFPOR.Name = "btnFPOR";
			this.btnFPOR.Size = new System.Drawing.Size(178, 34);
			this.btnFPOR.TabIndex = 56;
			this.btnFPOR.Text = "Set FPOR";
			this.btnFPOR.UseVisualStyleBackColor = true;
			this.btnFPOR.Click += new System.EventHandler(this.btnFPOR_Click);
			// 
			// btnTestMode
			// 
			this.btnTestMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTestMode.Location = new System.Drawing.Point(251, 518);
			this.btnTestMode.Name = "btnTestMode";
			this.btnTestMode.Size = new System.Drawing.Size(196, 33);
			this.btnTestMode.TabIndex = 57;
			this.btnTestMode.Text = "RPM/MPH LCDs";
			this.btnTestMode.UseVisualStyleBackColor = true;
			this.btnTestMode.Click += new System.EventHandler(this.btnTestMode_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(49, 34);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(69, 25);
			this.label9.TabIndex = 58;
			this.label9.Text = "scroll";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(56, 75);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(57, 25);
			this.label10.TabIndex = 59;
			this.label10.Text = "shift";
			// 
			// btnScrollDown
			// 
			this.btnScrollDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnScrollDown.Location = new System.Drawing.Point(213, 28);
			this.btnScrollDown.Name = "btnScrollDown";
			this.btnScrollDown.Size = new System.Drawing.Size(84, 33);
			this.btnScrollDown.TabIndex = 60;
			this.btnScrollDown.Text = "Down";
			this.btnScrollDown.UseVisualStyleBackColor = true;
			this.btnScrollDown.Click += new System.EventHandler(this.btnScrollDown_Click);
			// 
			// btnScrollUp
			// 
			this.btnScrollUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnScrollUp.Location = new System.Drawing.Point(127, 28);
			this.btnScrollUp.Name = "btnScrollUp";
			this.btnScrollUp.Size = new System.Drawing.Size(75, 33);
			this.btnScrollUp.TabIndex = 61;
			this.btnScrollUp.Text = "Up";
			this.btnScrollUp.UseVisualStyleBackColor = true;
			this.btnScrollUp.Click += new System.EventHandler(this.btnScrollUp_Click);
			// 
			// btnShiftLeft
			// 
			this.btnShiftLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShiftLeft.Location = new System.Drawing.Point(127, 71);
			this.btnShiftLeft.Name = "btnShiftLeft";
			this.btnShiftLeft.Size = new System.Drawing.Size(75, 33);
			this.btnShiftLeft.TabIndex = 62;
			this.btnShiftLeft.Text = "Left";
			this.btnShiftLeft.UseVisualStyleBackColor = true;
			this.btnShiftLeft.Click += new System.EventHandler(this.btnShiftLeft_Click);
			// 
			// btnShiftRight
			// 
			this.btnShiftRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShiftRight.Location = new System.Drawing.Point(213, 70);
			this.btnShiftRight.Name = "btnShiftRight";
			this.btnShiftRight.Size = new System.Drawing.Size(84, 33);
			this.btnShiftRight.TabIndex = 63;
			this.btnShiftRight.Text = "Right";
			this.btnShiftRight.UseVisualStyleBackColor = true;
			this.btnShiftRight.Click += new System.EventHandler(this.btnShiftRight_Click);
			// 
			// tbDebug
			// 
			this.tbDebug.Location = new System.Drawing.Point(12, 641);
			this.tbDebug.Multiline = true;
			this.tbDebug.Name = "tbDebug";
			this.tbDebug.Size = new System.Drawing.Size(373, 205);
			this.tbDebug.TabIndex = 64;
			// 
			// btnClearLCD
			// 
			this.btnClearLCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClearLCD.Location = new System.Drawing.Point(127, 110);
			this.btnClearLCD.Name = "btnClearLCD";
			this.btnClearLCD.Size = new System.Drawing.Size(169, 34);
			this.btnClearLCD.TabIndex = 65;
			this.btnClearLCD.Text = "Clear LCD";
			this.btnClearLCD.UseVisualStyleBackColor = true;
			this.btnClearLCD.Click += new System.EventHandler(this.btnClearLCD_Click);
			// 
			// btnShowFPGAStatus
			// 
			this.btnShowFPGAStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShowFPGAStatus.Location = new System.Drawing.Point(31, 203);
			this.btnShowFPGAStatus.Name = "btnShowFPGAStatus";
			this.btnShowFPGAStatus.Size = new System.Drawing.Size(373, 34);
			this.btnShowFPGAStatus.TabIndex = 66;
			this.btnShowFPGAStatus.Text = "Show FPGA Status";
			this.btnShowFPGAStatus.UseVisualStyleBackColor = true;
			this.btnShowFPGAStatus.Click += new System.EventHandler(this.btnShowFPGAStatus_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnXmitRate);
			this.groupBox2.Controls.Add(this.btnShowFPGAStatus);
			this.groupBox2.Controls.Add(this.cbHighRevLimit);
			this.groupBox2.Controls.Add(this.cbLowRevLimit);
			this.groupBox2.Controls.Add(this.cbFPGAXmitRate);
			this.groupBox2.Controls.Add(this.cbRPM_MPHUpdateRate);
			this.groupBox2.Controls.Add(this.btnHighRev);
			this.groupBox2.Controls.Add(this.btnLowRev);
			this.groupBox2.Controls.Add(this.btnRPMMPH);
			this.groupBox2.Controls.Add(this.btnRevLimitOR);
			this.groupBox2.Controls.Add(this.btnFPOR);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(12, 197);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(435, 298);
			this.groupBox2.TabIndex = 67;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "FPGA";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.btnClearLCD);
			this.groupBox3.Controls.Add(this.btnScrollUp);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.btnShiftRight);
			this.groupBox3.Controls.Add(this.btnScrollDown);
			this.groupBox3.Controls.Add(this.btnShiftLeft);
			this.groupBox3.Controls.Add(this.label10);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(483, 392);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(383, 161);
			this.groupBox3.TabIndex = 68;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "IO Box LCD";
			// 
			// cbLightsOnValue
			// 
			this.cbLightsOnValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbLightsOnValue.FormattingEnabled = true;
			this.cbLightsOnValue.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
			this.cbLightsOnValue.Location = new System.Drawing.Point(725, 66);
			this.cbLightsOnValue.Name = "cbLightsOnValue";
			this.cbLightsOnValue.Size = new System.Drawing.Size(141, 33);
			this.cbLightsOnValue.TabIndex = 70;
			this.cbLightsOnValue.SelectedIndexChanged += new System.EventHandler(this.cbLightsOnValue_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(487, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(180, 25);
			this.label3.TabIndex = 69;
			this.label3.Text = "Lights On Value";
			// 
			// cbLightsOffValue
			// 
			this.cbLightsOffValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbLightsOffValue.FormattingEnabled = true;
			this.cbLightsOffValue.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
			this.cbLightsOffValue.Location = new System.Drawing.Point(725, 110);
			this.cbLightsOffValue.Name = "cbLightsOffValue";
			this.cbLightsOffValue.Size = new System.Drawing.Size(141, 33);
			this.cbLightsOffValue.TabIndex = 72;
			this.cbLightsOffValue.SelectedIndexChanged += new System.EventHandler(this.cbLightsOffValue_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(487, 116);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(181, 25);
			this.label4.TabIndex = 71;
			this.label4.Text = "Lights Off Value";
			// 
			// cbADCRate
			// 
			this.cbADCRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbADCRate.FormattingEnabled = true;
			this.cbADCRate.Items.AddRange(new object[] {
            "1000ms",
            "500ms",
            "250ms",
            "125ms",
            "75ms",
            "30ms",
            "20ms",
            "10ms"});
			this.cbADCRate.Location = new System.Drawing.Point(725, 156);
			this.cbADCRate.Name = "cbADCRate";
			this.cbADCRate.Size = new System.Drawing.Size(141, 33);
			this.cbADCRate.TabIndex = 74;
			this.cbADCRate.SelectedIndexChanged += new System.EventHandler(this.cbADCRate_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(487, 162);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(115, 25);
			this.label5.TabIndex = 73;
			this.label5.Text = "ADC Rate";
			// 
			// btnDimmer
			// 
			this.btnDimmer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDimmer.Location = new System.Drawing.Point(350, 570);
			this.btnDimmer.Name = "btnDimmer";
			this.btnDimmer.Size = new System.Drawing.Size(193, 41);
			this.btnDimmer.TabIndex = 75;
			this.btnDimmer.Text = "Load Dimmers";
			this.btnDimmer.UseVisualStyleBackColor = true;
			this.btnDimmer.Click += new System.EventHandler(this.btnDimmer_Click);
			// 
			// DlgSetParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(887, 858);
			this.Controls.Add(this.btnDimmer);
			this.Controls.Add(this.cbADCRate);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cbLightsOffValue);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.cbLightsOnValue);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.tbDebug);
			this.Controls.Add(this.btnTestMode);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.cbBaudRate3);
			this.Controls.Add(this.tbNewPasssword);
			this.Controls.Add(this.btnChangePassword);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.cbPasswordRetries);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.cbPasswordTimeout);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.cbTestBank);
			this.Controls.Add(this.cbLightsOnDelay);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Name = "DlgSetParams";
			this.Text = "DlgSetParams";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbFanOn;
        private System.Windows.Forms.ComboBox cbFanOff;
        private System.Windows.Forms.ComboBox cbHighRevLimit;
        private System.Windows.Forms.ComboBox cbLowRevLimit;
        private System.Windows.Forms.ComboBox cbLightsOnDelay;
        private System.Windows.Forms.ComboBox cbFPGAXmitRate;
        private System.Windows.Forms.ComboBox cbRPM_MPHUpdateRate;
        private System.Windows.Forms.ComboBox cbTestBank;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbTempLimit;
        private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.ComboBox cbPasswordTimeout;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.ComboBox cbPasswordRetries;
		private System.Windows.Forms.Button btnChangePassword;
		private System.Windows.Forms.TextBox tbNewPasssword;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.ComboBox cbBaudRate3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnHighRev;
		private System.Windows.Forms.Button btnLowRev;
		private System.Windows.Forms.Button btnXmitRate;
		private System.Windows.Forms.Button btnRPMMPH;
		private System.Windows.Forms.Button btnRevLimitOR;
		private System.Windows.Forms.Button btnFPOR;
		private System.Windows.Forms.Button btnTestMode;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button btnScrollDown;
		private System.Windows.Forms.Button btnScrollUp;
		private System.Windows.Forms.Button btnShiftLeft;
		private System.Windows.Forms.Button btnShiftRight;
		private System.Windows.Forms.TextBox tbDebug;
		private System.Windows.Forms.Button btnClearLCD;
		private System.Windows.Forms.Button btnShowFPGAStatus;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox cbLightsOnValue;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbLightsOffValue;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cbADCRate;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnDimmer;
	}
}