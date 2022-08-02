
namespace EpServerEngineSampleClient
{
	partial class ClientDest
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
			this.cbSource = new System.Windows.Forms.ComboBox();
			this.tbReceived = new System.Windows.Forms.TextBox();
			this.cbTimerSeconds = new System.Windows.Forms.ComboBox();
			this.btnSetTimer = new System.Windows.Forms.Button();
			this.btnStartTimer = new System.Windows.Forms.Button();
			this.btnStopTimer = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnStartTimer2 = new System.Windows.Forms.Button();
			this.tbWaterOffTime = new System.Windows.Forms.TextBox();
			this.tbWaterOnTime = new System.Windows.Forms.TextBox();
			this.btnOnTime = new System.Windows.Forms.Button();
			this.btnClrScrn = new System.Windows.Forms.Button();
			this.btnOffTime = new System.Windows.Forms.Button();
			this.cbWaterEnabled = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbSource
			// 
			this.cbSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbSource.FormattingEnabled = true;
			this.cbSource.Items.AddRange(new object[] {
            "TS-client 1 (154)",
            "TS-client 2 (147)",
            "TS-client 3 (150)",
            "TS-client 4 (151)",
            "TS-client 5 (155)",
            "TS-client 6 (145)",
            "TS-server (146)"});
			this.cbSource.Location = new System.Drawing.Point(13, 67);
			this.cbSource.Name = "cbSource";
			this.cbSource.Size = new System.Drawing.Size(166, 26);
			this.cbSource.TabIndex = 0;
			this.cbSource.SelectedIndexChanged += new System.EventHandler(this.cbSource_SelectedIndexChanged);
			// 
			// tbReceived
			// 
			this.tbReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbReceived.Location = new System.Drawing.Point(10, 275);
			this.tbReceived.Margin = new System.Windows.Forms.Padding(1);
			this.tbReceived.Multiline = true;
			this.tbReceived.Name = "tbReceived";
			this.tbReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbReceived.Size = new System.Drawing.Size(414, 245);
			this.tbReceived.TabIndex = 3;
			// 
			// cbTimerSeconds
			// 
			this.cbTimerSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbTimerSeconds.FormattingEnabled = true;
			this.cbTimerSeconds.Items.AddRange(new object[] {
            "1 second",
            "2 seconds",
            "3 seconds",
            "4 seconds",
            "5 seconds",
            "10 seconds",
            "15 seconds",
            "20 seconds"});
			this.cbTimerSeconds.Location = new System.Drawing.Point(13, 21);
			this.cbTimerSeconds.Margin = new System.Windows.Forms.Padding(1);
			this.cbTimerSeconds.Name = "cbTimerSeconds";
			this.cbTimerSeconds.Size = new System.Drawing.Size(166, 26);
			this.cbTimerSeconds.TabIndex = 4;
			this.cbTimerSeconds.SelectedIndexChanged += new System.EventHandler(this.cbTimerSeconds_SelectedIndexChanged);
			// 
			// btnSetTimer
			// 
			this.btnSetTimer.Location = new System.Drawing.Point(316, 28);
			this.btnSetTimer.Name = "btnSetTimer";
			this.btnSetTimer.Size = new System.Drawing.Size(75, 23);
			this.btnSetTimer.TabIndex = 5;
			this.btnSetTimer.Text = "Set Timer";
			this.btnSetTimer.UseVisualStyleBackColor = true;
			this.btnSetTimer.Click += new System.EventHandler(this.btnSetTimer_Click);
			// 
			// btnStartTimer
			// 
			this.btnStartTimer.Location = new System.Drawing.Point(12, 138);
			this.btnStartTimer.Name = "btnStartTimer";
			this.btnStartTimer.Size = new System.Drawing.Size(75, 23);
			this.btnStartTimer.TabIndex = 6;
			this.btnStartTimer.Text = "Start Timer 1";
			this.btnStartTimer.UseVisualStyleBackColor = true;
			this.btnStartTimer.Click += new System.EventHandler(this.btnStartTimer_Click);
			// 
			// btnStopTimer
			// 
			this.btnStopTimer.Location = new System.Drawing.Point(12, 198);
			this.btnStopTimer.Name = "btnStopTimer";
			this.btnStopTimer.Size = new System.Drawing.Size(75, 23);
			this.btnStopTimer.TabIndex = 7;
			this.btnStopTimer.Text = "Stop Timer";
			this.btnStopTimer.UseVisualStyleBackColor = true;
			this.btnStopTimer.Click += new System.EventHandler(this.btnStopTimer_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cbSource);
			this.groupBox1.Controls.Add(this.cbTimerSeconds);
			this.groupBox1.Controls.Add(this.btnSetTimer);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(412, 115);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Set Timer";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(200, 74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Destination";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(200, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Timer Seconds";
			// 
			// btnStartTimer2
			// 
			this.btnStartTimer2.Location = new System.Drawing.Point(12, 167);
			this.btnStartTimer2.Name = "btnStartTimer2";
			this.btnStartTimer2.Size = new System.Drawing.Size(75, 23);
			this.btnStartTimer2.TabIndex = 9;
			this.btnStartTimer2.Text = "Start Timer 2";
			this.btnStartTimer2.UseVisualStyleBackColor = true;
			this.btnStartTimer2.Click += new System.EventHandler(this.btnStartTimer2_Click);
			// 
			// tbWaterOffTime
			// 
			this.tbWaterOffTime.Location = new System.Drawing.Point(306, 167);
			this.tbWaterOffTime.Name = "tbWaterOffTime";
			this.tbWaterOffTime.Size = new System.Drawing.Size(100, 20);
			this.tbWaterOffTime.TabIndex = 10;
			this.tbWaterOffTime.TextChanged += new System.EventHandler(this.tbWaterOffTime_TextChanged);
			// 
			// tbWaterOnTime
			// 
			this.tbWaterOnTime.Location = new System.Drawing.Point(306, 198);
			this.tbWaterOnTime.Name = "tbWaterOnTime";
			this.tbWaterOnTime.Size = new System.Drawing.Size(100, 20);
			this.tbWaterOnTime.TabIndex = 11;
			// 
			// btnOnTime
			// 
			this.btnOnTime.Location = new System.Drawing.Point(181, 198);
			this.btnOnTime.Name = "btnOnTime";
			this.btnOnTime.Size = new System.Drawing.Size(100, 23);
			this.btnOnTime.TabIndex = 12;
			this.btnOnTime.Text = "On Time";
			this.btnOnTime.UseVisualStyleBackColor = true;
			this.btnOnTime.Click += new System.EventHandler(this.btnWaterTimer_Click);
			// 
			// btnClrScrn
			// 
			this.btnClrScrn.Location = new System.Drawing.Point(306, 233);
			this.btnClrScrn.Name = "btnClrScrn";
			this.btnClrScrn.Size = new System.Drawing.Size(97, 23);
			this.btnClrScrn.TabIndex = 13;
			this.btnClrScrn.Text = "Clear Screen";
			this.btnClrScrn.UseVisualStyleBackColor = true;
			this.btnClrScrn.Click += new System.EventHandler(this.btnClrScrn_Click);
			// 
			// btnOffTime
			// 
			this.btnOffTime.Location = new System.Drawing.Point(181, 167);
			this.btnOffTime.Name = "btnOffTime";
			this.btnOffTime.Size = new System.Drawing.Size(100, 23);
			this.btnOffTime.TabIndex = 14;
			this.btnOffTime.Text = "Off Time";
			this.btnOffTime.UseVisualStyleBackColor = true;
			this.btnOffTime.Click += new System.EventHandler(this.button1_Click);
			// 
			// cbWaterEnabled
			// 
			this.cbWaterEnabled.AutoSize = true;
			this.cbWaterEnabled.Location = new System.Drawing.Point(181, 142);
			this.cbWaterEnabled.Name = "cbWaterEnabled";
			this.cbWaterEnabled.Size = new System.Drawing.Size(126, 17);
			this.cbWaterEnabled.TabIndex = 15;
			this.cbWaterEnabled.Text = "Water Timer Enabled";
			this.cbWaterEnabled.UseVisualStyleBackColor = true;
			this.cbWaterEnabled.CheckedChanged += new System.EventHandler(this.WaterEnableCheckChanged);
			// 
			// ClientDest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(434, 530);
			this.Controls.Add(this.cbWaterEnabled);
			this.Controls.Add(this.btnOffTime);
			this.Controls.Add(this.btnClrScrn);
			this.Controls.Add(this.btnOnTime);
			this.Controls.Add(this.tbWaterOnTime);
			this.Controls.Add(this.tbWaterOffTime);
			this.Controls.Add(this.btnStartTimer2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnStopTimer);
			this.Controls.Add(this.btnStartTimer);
			this.Controls.Add(this.tbReceived);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
			this.Name = "ClientDest";
			this.Text = "Timers";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cbSource;
		private System.Windows.Forms.TextBox tbReceived;
		private System.Windows.Forms.ComboBox cbTimerSeconds;
		private System.Windows.Forms.Button btnSetTimer;
		private System.Windows.Forms.Button btnStartTimer;
		private System.Windows.Forms.Button btnStopTimer;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnStartTimer2;
		private System.Windows.Forms.TextBox tbWaterOffTime;
		private System.Windows.Forms.TextBox tbWaterOnTime;
		private System.Windows.Forms.Button btnOnTime;
		private System.Windows.Forms.Button btnClrScrn;
		private System.Windows.Forms.Button btnOffTime;
		private System.Windows.Forms.CheckBox cbWaterEnabled;
	}
}