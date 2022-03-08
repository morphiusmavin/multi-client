namespace EpServerEngineSampleClient
{
	partial class TestPorts
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
			this.btnToggle = new System.Windows.Forms.Button();
			this.lbPortList = new System.Windows.Forms.ListBox();
			this.lblBankSelected = new System.Windows.Forms.Label();
			this.lblPortSelected = new System.Windows.Forms.Label();
			this.tbReceived = new System.Windows.Forms.TextBox();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnPrev = new System.Windows.Forms.Button();
			this.btnFPGAstatus = new System.Windows.Forms.Button();
			this.btnTestLCDPWM = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.cbSendDebug = new System.Windows.Forms.CheckBox();
			this.lbRPMMPHBrightness = new System.Windows.Forms.ListBox();
			this.btnTestRPMMPHBrightness = new System.Windows.Forms.Button();
			this.btnTestEngineTemps = new System.Windows.Forms.Button();
			this.btnTestMode = new System.Windows.Forms.Button();
			this.lbTestingLEDs = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnToggle
			// 
			this.btnToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnToggle.Location = new System.Drawing.Point(30, 28);
			this.btnToggle.Name = "btnToggle";
			this.btnToggle.Size = new System.Drawing.Size(113, 46);
			this.btnToggle.TabIndex = 0;
			this.btnToggle.Text = "Toggle";
			this.btnToggle.UseVisualStyleBackColor = true;
			this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
			// 
			// lbPortList
			// 
			this.lbPortList.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbPortList.FormattingEnabled = true;
			this.lbPortList.ItemHeight = 24;
			this.lbPortList.Items.AddRange(new object[] {
            "STARTER",
            "ACCON",
            "FUELPUMP",
            "COOLINGFAN",
            "LHEADLAMP",
            "RHEADLAMP",
            "RUNNINGLIGHTS",
            "LIGHTBAR",
            "LEFTRBLINKER",
            "RIGHTRBLINKER",
            "LEFTFBLINKER",
            "RIGHTFBLINKER",
            "LBRAKELIGHT",
            "RBRAKELIGHT",
            "XLBLINKER",
            "XRBLINKER",
            "LBRIGHTS",
            "RBRIGHTS",
            "TESTOUTPUT18",
            "TESTOUTPUT19",
            "TESTOUTPUT20",
            "WWIPER1",
            "WWIPER2",
            "TRLEFTBLINKER",
            "TRRIGHTBLINKER",
            "INTRUDERALARM",
            "BLINKINDICATE",
            "BATTERYCOMPHEATER",
            "ALARMSPEAKER",
            "BACKUPLIGHTS",
            "TESTOUTPUT31",
            "TESTOUTPUT32",
            "TESTOUTPUT33",
            "TESTOUTPUT34",
            "TESTOUTPUT35",
            "TESTOUTPUT36",
            "TESTOUTPUT37",
            "TESTOUTPUT38",
            "TESTOUTPUT39",
            "NULL2"});
			this.lbPortList.Location = new System.Drawing.Point(32, 131);
			this.lbPortList.Name = "lbPortList";
			this.lbPortList.Size = new System.Drawing.Size(258, 340);
			this.lbPortList.TabIndex = 1;
			this.lbPortList.SelectedIndexChanged += new System.EventHandler(this.lbPortList_SelectedIndexChanged);
			// 
			// lblBankSelected
			// 
			this.lblBankSelected.AutoSize = true;
			this.lblBankSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblBankSelected.Location = new System.Drawing.Point(40, 87);
			this.lblBankSelected.Name = "lblBankSelected";
			this.lblBankSelected.Size = new System.Drawing.Size(56, 24);
			this.lblBankSelected.TabIndex = 2;
			this.lblBankSelected.Text = "Bank";
			// 
			// lblPortSelected
			// 
			this.lblPortSelected.AutoSize = true;
			this.lblPortSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPortSelected.Location = new System.Drawing.Point(150, 87);
			this.lblPortSelected.Name = "lblPortSelected";
			this.lblPortSelected.Size = new System.Drawing.Size(47, 24);
			this.lblPortSelected.TabIndex = 3;
			this.lblPortSelected.Text = "Port";
			// 
			// tbReceived
			// 
			this.tbReceived.Location = new System.Drawing.Point(318, 139);
			this.tbReceived.Multiline = true;
			this.tbReceived.Name = "tbReceived";
			this.tbReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbReceived.Size = new System.Drawing.Size(256, 332);
			this.tbReceived.TabIndex = 4;
			// 
			// btnNext
			// 
			this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNext.Location = new System.Drawing.Point(167, 28);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(113, 46);
			this.btnNext.TabIndex = 5;
			this.btnNext.Text = "Next";
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// btnPrev
			// 
			this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPrev.Location = new System.Drawing.Point(299, 28);
			this.btnPrev.Name = "btnPrev";
			this.btnPrev.Size = new System.Drawing.Size(113, 46);
			this.btnPrev.TabIndex = 6;
			this.btnPrev.Text = "Prev";
			this.btnPrev.UseVisualStyleBackColor = true;
			this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
			// 
			// btnFPGAstatus
			// 
			this.btnFPGAstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFPGAstatus.Location = new System.Drawing.Point(316, 87);
			this.btnFPGAstatus.Name = "btnFPGAstatus";
			this.btnFPGAstatus.Size = new System.Drawing.Size(258, 46);
			this.btnFPGAstatus.TabIndex = 7;
			this.btnFPGAstatus.Text = "Show FPGA Status";
			this.btnFPGAstatus.UseVisualStyleBackColor = true;
			this.btnFPGAstatus.Click += new System.EventHandler(this.btnFPGAstatus_Click);
			// 
			// btnTestLCDPWM
			// 
			this.btnTestLCDPWM.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTestLCDPWM.Location = new System.Drawing.Point(32, 485);
			this.btnTestLCDPWM.Name = "btnTestLCDPWM";
			this.btnTestLCDPWM.Size = new System.Drawing.Size(258, 46);
			this.btnTestLCDPWM.TabIndex = 8;
			this.btnTestLCDPWM.Text = "Test LCD PWM";
			this.btnTestLCDPWM.UseVisualStyleBackColor = true;
			this.btnTestLCDPWM.Click += new System.EventHandler(this.btnTestLCDPWM_Click);
			// 
			// listBox1
			// 
			this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 24;
			this.listBox1.Items.AddRange(new object[] {
            "off",
            "duty_cycle = 12%",
            "duty_cycle = 25%",
            "duty_cycle = 30%",
            "duty_cycle = 50%",
            "duty_cycle = 60%",
            "duty_cycle = 75%",
            "duty_cycle = 80%",
            "on"});
			this.listBox1.Location = new System.Drawing.Point(299, 488);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(275, 124);
			this.listBox1.TabIndex = 9;
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// cbSendDebug
			// 
			this.cbSendDebug.AutoSize = true;
			this.cbSendDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbSendDebug.Location = new System.Drawing.Point(32, 557);
			this.cbSendDebug.Name = "cbSendDebug";
			this.cbSendDebug.Size = new System.Drawing.Size(191, 28);
			this.cbSendDebug.TabIndex = 10;
			this.cbSendDebug.Text = "Send Debug Msg";
			this.cbSendDebug.UseVisualStyleBackColor = true;
			this.cbSendDebug.CheckedChanged += new System.EventHandler(this.cbSendDebug_CheckedChanged);
			// 
			// lbRPMMPHBrightness
			// 
			this.lbRPMMPHBrightness.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbRPMMPHBrightness.FormattingEnabled = true;
			this.lbRPMMPHBrightness.ItemHeight = 24;
			this.lbRPMMPHBrightness.Items.AddRange(new object[] {
            "off",
            "10%",
            "20%",
            "30%",
            "40%",
            "50%",
            "60%",
            "70%",
            "80%",
            "90%",
            "100%"});
			this.lbRPMMPHBrightness.Location = new System.Drawing.Point(299, 626);
			this.lbRPMMPHBrightness.Name = "lbRPMMPHBrightness";
			this.lbRPMMPHBrightness.Size = new System.Drawing.Size(275, 124);
			this.lbRPMMPHBrightness.TabIndex = 12;
			this.lbRPMMPHBrightness.SelectedIndexChanged += new System.EventHandler(this.lbRPMMPHBrightness_SelectedIndexChanged);
			// 
			// btnTestRPMMPHBrightness
			// 
			this.btnTestRPMMPHBrightness.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTestRPMMPHBrightness.Location = new System.Drawing.Point(32, 623);
			this.btnTestRPMMPHBrightness.Name = "btnTestRPMMPHBrightness";
			this.btnTestRPMMPHBrightness.Size = new System.Drawing.Size(258, 46);
			this.btnTestRPMMPHBrightness.TabIndex = 11;
			this.btnTestRPMMPHBrightness.Text = "Test RPM MPH Bright";
			this.btnTestRPMMPHBrightness.UseVisualStyleBackColor = true;
			this.btnTestRPMMPHBrightness.Click += new System.EventHandler(this.btnTestRPMMPHBrightness_Click);
			// 
			// btnTestEngineTemps
			// 
			this.btnTestEngineTemps.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTestEngineTemps.Location = new System.Drawing.Point(32, 693);
			this.btnTestEngineTemps.Name = "btnTestEngineTemps";
			this.btnTestEngineTemps.Size = new System.Drawing.Size(258, 46);
			this.btnTestEngineTemps.TabIndex = 13;
			this.btnTestEngineTemps.Text = "Test Engine Temps";
			this.btnTestEngineTemps.UseVisualStyleBackColor = true;
			this.btnTestEngineTemps.Click += new System.EventHandler(this.btnTestEngineTemps_Click);
			// 
			// btnTestMode
			// 
			this.btnTestMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTestMode.Location = new System.Drawing.Point(32, 761);
			this.btnTestMode.Name = "btnTestMode";
			this.btnTestMode.Size = new System.Drawing.Size(258, 46);
			this.btnTestMode.TabIndex = 14;
			this.btnTestMode.Text = "Test LEDs";
			this.btnTestMode.UseVisualStyleBackColor = true;
			this.btnTestMode.Click += new System.EventHandler(this.btnTestMode_Click);
			// 
			// lbTestingLEDs
			// 
			this.lbTestingLEDs.AutoSize = true;
			this.lbTestingLEDs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbTestingLEDs.Location = new System.Drawing.Point(312, 772);
			this.lbTestingLEDs.Name = "lbTestingLEDs";
			this.lbTestingLEDs.Size = new System.Drawing.Size(152, 24);
			this.lbTestingLEDs.TabIndex = 15;
			this.lbTestingLEDs.Text = "Testing LEDs...";
			this.lbTestingLEDs.Visible = false;
			// 
			// TestPorts
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(600, 820);
			this.Controls.Add(this.lbTestingLEDs);
			this.Controls.Add(this.btnTestMode);
			this.Controls.Add(this.btnTestEngineTemps);
			this.Controls.Add(this.lbRPMMPHBrightness);
			this.Controls.Add(this.btnTestRPMMPHBrightness);
			this.Controls.Add(this.cbSendDebug);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.btnTestLCDPWM);
			this.Controls.Add(this.btnFPGAstatus);
			this.Controls.Add(this.btnPrev);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.tbReceived);
			this.Controls.Add(this.lblPortSelected);
			this.Controls.Add(this.lblBankSelected);
			this.Controls.Add(this.lbPortList);
			this.Controls.Add(this.btnToggle);
			this.Name = "TestPorts";
			this.Text = "TestPorts";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnToggle;
		private System.Windows.Forms.ListBox lbPortList;
		private System.Windows.Forms.Label lblBankSelected;
		private System.Windows.Forms.Label lblPortSelected;
		private System.Windows.Forms.TextBox tbReceived;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnPrev;
		private System.Windows.Forms.Button btnFPGAstatus;
		private System.Windows.Forms.Button btnTestLCDPWM;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.CheckBox cbSendDebug;
		private System.Windows.Forms.ListBox lbRPMMPHBrightness;
		private System.Windows.Forms.Button btnTestRPMMPHBrightness;
		private System.Windows.Forms.Button btnTestEngineTemps;
		private System.Windows.Forms.Button btnTestMode;
		private System.Windows.Forms.Label lbTestingLEDs;
	}
}