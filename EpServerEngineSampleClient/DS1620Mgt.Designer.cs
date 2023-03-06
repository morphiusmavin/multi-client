
namespace EpServerEngineSampleClient
{
	partial class DS1620Mgt
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
			this.Cancel = new System.Windows.Forms.Button();
			this.tbNewFileName = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.cbClientNames = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnRenameFile = new System.Windows.Forms.Button();
			this.tbReceived = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.chkDS1 = new System.Windows.Forms.CheckBox();
			this.chkDS2 = new System.Windows.Forms.CheckBox();
			this.chkDS3 = new System.Windows.Forms.CheckBox();
			this.chkDS6 = new System.Windows.Forms.CheckBox();
			this.chkDS5 = new System.Windows.Forms.CheckBox();
			this.chkDS4 = new System.Windows.Forms.CheckBox();
			this.btnValidDS = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rbInterval8 = new System.Windows.Forms.RadioButton();
			this.rbInterval7 = new System.Windows.Forms.RadioButton();
			this.rbInterval6 = new System.Windows.Forms.RadioButton();
			this.rbInterval5 = new System.Windows.Forms.RadioButton();
			this.rbInterval4 = new System.Windows.Forms.RadioButton();
			this.rbInterval3 = new System.Windows.Forms.RadioButton();
			this.rbInterval2 = new System.Windows.Forms.RadioButton();
			this.rbInterval1 = new System.Windows.Forms.RadioButton();
			this.btnApplyInterval = new System.Windows.Forms.Button();
			this.btnShow = new System.Windows.Forms.Button();
			this.btnReset = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOK.Location = new System.Drawing.Point(510, 464);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(117, 43);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// Cancel
			// 
			this.Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Cancel.Location = new System.Drawing.Point(659, 464);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(117, 43);
			this.Cancel.TabIndex = 1;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = false;
			this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// tbNewFileName
			// 
			this.tbNewFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.tbNewFileName.Location = new System.Drawing.Point(308, 338);
			this.tbNewFileName.Name = "tbNewFileName";
			this.tbNewFileName.Size = new System.Drawing.Size(203, 35);
			this.tbNewFileName.TabIndex = 2;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label12.Location = new System.Drawing.Point(327, 306);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(158, 20);
			this.label12.TabIndex = 86;
			this.label12.Text = "Rename ddata.dat";
			// 
			// cbClientNames
			// 
			this.cbClientNames.FormattingEnabled = true;
			this.cbClientNames.Items.AddRange(new object[] {
            "Server (146)",
            "Cabin (154)",
            "Testbench (147)",
            "Outdoor (150)"});
			this.cbClientNames.Location = new System.Drawing.Point(133, 23);
			this.cbClientNames.Name = "cbClientNames";
			this.cbClientNames.Size = new System.Drawing.Size(159, 21);
			this.cbClientNames.TabIndex = 88;
			this.cbClientNames.SelectedIndexChanged += new System.EventHandler(this.cbClientNames_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(15, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 20);
			this.label1.TabIndex = 89;
			this.label1.Text = "Client/Server";
			// 
			// btnRenameFile
			// 
			this.btnRenameFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnRenameFile.Location = new System.Drawing.Point(308, 394);
			this.btnRenameFile.Name = "btnRenameFile";
			this.btnRenameFile.Size = new System.Drawing.Size(203, 38);
			this.btnRenameFile.TabIndex = 92;
			this.btnRenameFile.Text = "Rename File";
			this.btnRenameFile.UseVisualStyleBackColor = true;
			this.btnRenameFile.Click += new System.EventHandler(this.btnRenameFile_Click);
			// 
			// tbReceived
			// 
			this.tbReceived.Location = new System.Drawing.Point(535, 25);
			this.tbReceived.Multiline = true;
			this.tbReceived.Name = "tbReceived";
			this.tbReceived.Size = new System.Drawing.Size(249, 331);
			this.tbReceived.TabIndex = 93;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(620, 372);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 94;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// chkDS1
			// 
			this.chkDS1.AutoSize = true;
			this.chkDS1.Location = new System.Drawing.Point(323, 27);
			this.chkDS1.Name = "chkDS1";
			this.chkDS1.Size = new System.Drawing.Size(32, 17);
			this.chkDS1.TabIndex = 95;
			this.chkDS1.Text = "1";
			this.chkDS1.UseVisualStyleBackColor = true;
			this.chkDS1.CheckedChanged += new System.EventHandler(this.chkDS1_CheckedChanged);
			// 
			// chkDS2
			// 
			this.chkDS2.AutoSize = true;
			this.chkDS2.Location = new System.Drawing.Point(323, 50);
			this.chkDS2.Name = "chkDS2";
			this.chkDS2.Size = new System.Drawing.Size(32, 17);
			this.chkDS2.TabIndex = 96;
			this.chkDS2.Text = "2";
			this.chkDS2.UseVisualStyleBackColor = true;
			this.chkDS2.CheckedChanged += new System.EventHandler(this.chkDS2_CheckedChanged);
			// 
			// chkDS3
			// 
			this.chkDS3.AutoSize = true;
			this.chkDS3.Location = new System.Drawing.Point(323, 73);
			this.chkDS3.Name = "chkDS3";
			this.chkDS3.Size = new System.Drawing.Size(32, 17);
			this.chkDS3.TabIndex = 97;
			this.chkDS3.Text = "3";
			this.chkDS3.UseVisualStyleBackColor = true;
			this.chkDS3.CheckedChanged += new System.EventHandler(this.chkDS3_CheckedChanged);
			// 
			// chkDS6
			// 
			this.chkDS6.AutoSize = true;
			this.chkDS6.Location = new System.Drawing.Point(323, 143);
			this.chkDS6.Name = "chkDS6";
			this.chkDS6.Size = new System.Drawing.Size(32, 17);
			this.chkDS6.TabIndex = 100;
			this.chkDS6.Text = "6";
			this.chkDS6.UseVisualStyleBackColor = true;
			this.chkDS6.CheckedChanged += new System.EventHandler(this.chkDS6_CheckedChanged);
			// 
			// chkDS5
			// 
			this.chkDS5.AutoSize = true;
			this.chkDS5.Location = new System.Drawing.Point(323, 120);
			this.chkDS5.Name = "chkDS5";
			this.chkDS5.Size = new System.Drawing.Size(32, 17);
			this.chkDS5.TabIndex = 99;
			this.chkDS5.Text = "5";
			this.chkDS5.UseVisualStyleBackColor = true;
			this.chkDS5.CheckedChanged += new System.EventHandler(this.chkDS5_CheckedChanged);
			// 
			// chkDS4
			// 
			this.chkDS4.AutoSize = true;
			this.chkDS4.Location = new System.Drawing.Point(323, 97);
			this.chkDS4.Name = "chkDS4";
			this.chkDS4.Size = new System.Drawing.Size(32, 17);
			this.chkDS4.TabIndex = 98;
			this.chkDS4.Text = "4";
			this.chkDS4.UseVisualStyleBackColor = true;
			this.chkDS4.CheckedChanged += new System.EventHandler(this.chkDS4_CheckedChanged);
			// 
			// btnValidDS
			// 
			this.btnValidDS.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnValidDS.Location = new System.Drawing.Point(19, 61);
			this.btnValidDS.Name = "btnValidDS";
			this.btnValidDS.Size = new System.Drawing.Size(273, 35);
			this.btnValidDS.TabIndex = 101;
			this.btnValidDS.Text = "Apply Valid DS1620\'s";
			this.btnValidDS.UseVisualStyleBackColor = true;
			this.btnValidDS.Click += new System.EventHandler(this.btnValidDS_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(15, 116);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(281, 20);
			this.label2.TabIndex = 102;
			this.label2.Text = "1->6 are which DS1620\'s are valid";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rbInterval8);
			this.groupBox1.Controls.Add(this.rbInterval7);
			this.groupBox1.Controls.Add(this.rbInterval6);
			this.groupBox1.Controls.Add(this.rbInterval5);
			this.groupBox1.Controls.Add(this.rbInterval4);
			this.groupBox1.Controls.Add(this.rbInterval3);
			this.groupBox1.Controls.Add(this.rbInterval2);
			this.groupBox1.Controls.Add(this.rbInterval1);
			this.groupBox1.Controls.Add(this.btnApplyInterval);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(19, 153);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(246, 354);
			this.groupBox1.TabIndex = 111;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Interval";
			// 
			// rbInterval8
			// 
			this.rbInterval8.AutoSize = true;
			this.rbInterval8.Location = new System.Drawing.Point(24, 238);
			this.rbInterval8.Name = "rbInterval8";
			this.rbInterval8.Size = new System.Drawing.Size(115, 24);
			this.rbInterval8.TabIndex = 119;
			this.rbInterval8.TabStop = true;
			this.rbInterval8.Text = "10 minutes";
			this.rbInterval8.UseVisualStyleBackColor = true;
			this.rbInterval8.CheckedChanged += new System.EventHandler(this.rbInterval8_CheckedChanged);
			// 
			// rbInterval7
			// 
			this.rbInterval7.AutoSize = true;
			this.rbInterval7.Location = new System.Drawing.Point(24, 209);
			this.rbInterval7.Name = "rbInterval7";
			this.rbInterval7.Size = new System.Drawing.Size(105, 24);
			this.rbInterval7.TabIndex = 118;
			this.rbInterval7.TabStop = true;
			this.rbInterval7.Text = "5 minutes";
			this.rbInterval7.UseVisualStyleBackColor = true;
			this.rbInterval7.CheckedChanged += new System.EventHandler(this.rbInterval7_CheckedChanged);
			// 
			// rbInterval6
			// 
			this.rbInterval6.AutoSize = true;
			this.rbInterval6.Location = new System.Drawing.Point(24, 180);
			this.rbInterval6.Name = "rbInterval6";
			this.rbInterval6.Size = new System.Drawing.Size(96, 24);
			this.rbInterval6.TabIndex = 117;
			this.rbInterval6.TabStop = true;
			this.rbInterval6.Text = "1 minute";
			this.rbInterval6.UseVisualStyleBackColor = true;
			this.rbInterval6.CheckedChanged += new System.EventHandler(this.rbInterval6_CheckedChanged);
			// 
			// rbInterval5
			// 
			this.rbInterval5.AutoSize = true;
			this.rbInterval5.Location = new System.Drawing.Point(24, 151);
			this.rbInterval5.Name = "rbInterval5";
			this.rbInterval5.Size = new System.Drawing.Size(119, 24);
			this.rbInterval5.TabIndex = 116;
			this.rbInterval5.TabStop = true;
			this.rbInterval5.Text = "30 seconds";
			this.rbInterval5.UseVisualStyleBackColor = true;
			this.rbInterval5.CheckedChanged += new System.EventHandler(this.rbInterval5_CheckedChanged);
			// 
			// rbInterval4
			// 
			this.rbInterval4.AutoSize = true;
			this.rbInterval4.Location = new System.Drawing.Point(24, 122);
			this.rbInterval4.Name = "rbInterval4";
			this.rbInterval4.Size = new System.Drawing.Size(119, 24);
			this.rbInterval4.TabIndex = 115;
			this.rbInterval4.TabStop = true;
			this.rbInterval4.Text = "15 seconds";
			this.rbInterval4.UseVisualStyleBackColor = true;
			this.rbInterval4.CheckedChanged += new System.EventHandler(this.rbInterval4_CheckedChanged);
			// 
			// rbInterval3
			// 
			this.rbInterval3.AutoSize = true;
			this.rbInterval3.Location = new System.Drawing.Point(24, 93);
			this.rbInterval3.Name = "rbInterval3";
			this.rbInterval3.Size = new System.Drawing.Size(109, 24);
			this.rbInterval3.TabIndex = 114;
			this.rbInterval3.TabStop = true;
			this.rbInterval3.Text = "5 seconds";
			this.rbInterval3.UseVisualStyleBackColor = true;
			this.rbInterval3.CheckedChanged += new System.EventHandler(this.rbInterval3_CheckedChanged);
			// 
			// rbInterval2
			// 
			this.rbInterval2.AutoSize = true;
			this.rbInterval2.Location = new System.Drawing.Point(24, 64);
			this.rbInterval2.Name = "rbInterval2";
			this.rbInterval2.Size = new System.Drawing.Size(100, 24);
			this.rbInterval2.TabIndex = 113;
			this.rbInterval2.TabStop = true;
			this.rbInterval2.Text = "1 second";
			this.rbInterval2.UseVisualStyleBackColor = true;
			this.rbInterval2.CheckedChanged += new System.EventHandler(this.rbInterval2_CheckedChanged);
			// 
			// rbInterval1
			// 
			this.rbInterval1.AutoSize = true;
			this.rbInterval1.Location = new System.Drawing.Point(24, 35);
			this.rbInterval1.Name = "rbInterval1";
			this.rbInterval1.Size = new System.Drawing.Size(115, 24);
			this.rbInterval1.TabIndex = 112;
			this.rbInterval1.TabStop = true;
			this.rbInterval1.Text = "1/2 second";
			this.rbInterval1.UseVisualStyleBackColor = true;
			this.rbInterval1.CheckedChanged += new System.EventHandler(this.rbInterval1_CheckedChanged);
			// 
			// btnApplyInterval
			// 
			this.btnApplyInterval.Location = new System.Drawing.Point(19, 297);
			this.btnApplyInterval.Name = "btnApplyInterval";
			this.btnApplyInterval.Size = new System.Drawing.Size(133, 32);
			this.btnApplyInterval.TabIndex = 112;
			this.btnApplyInterval.Text = "Apply Interval";
			this.btnApplyInterval.UseVisualStyleBackColor = true;
			this.btnApplyInterval.Click += new System.EventHandler(this.btnApplyInterval_Click);
			// 
			// btnShow
			// 
			this.btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnShow.Location = new System.Drawing.Point(386, 209);
			this.btnShow.Name = "btnShow";
			this.btnShow.Size = new System.Drawing.Size(109, 42);
			this.btnShow.TabIndex = 112;
			this.btnShow.Text = "Show";
			this.btnShow.UseVisualStyleBackColor = true;
			this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
			// 
			// btnReset
			// 
			this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnReset.Location = new System.Drawing.Point(386, 143);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(109, 42);
			this.btnReset.TabIndex = 113;
			this.btnReset.Text = "Reset";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// DS1620Mgt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(800, 524);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.btnShow);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnValidDS);
			this.Controls.Add(this.chkDS6);
			this.Controls.Add(this.chkDS5);
			this.Controls.Add(this.chkDS4);
			this.Controls.Add(this.chkDS3);
			this.Controls.Add(this.chkDS2);
			this.Controls.Add(this.chkDS1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tbReceived);
			this.Controls.Add(this.btnRenameFile);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbClientNames);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.tbNewFileName);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.btnOK);
			this.Name = "DS1620Mgt";
			this.Text = "DS1620Mgt";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.TextBox tbNewFileName;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.ComboBox cbClientNames;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnRenameFile;
		private System.Windows.Forms.TextBox tbReceived;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox chkDS1;
		private System.Windows.Forms.CheckBox chkDS2;
		private System.Windows.Forms.CheckBox chkDS3;
		private System.Windows.Forms.CheckBox chkDS6;
		private System.Windows.Forms.CheckBox chkDS5;
		private System.Windows.Forms.CheckBox chkDS4;
		private System.Windows.Forms.Button btnValidDS;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnApplyInterval;
		private System.Windows.Forms.RadioButton rbInterval7;
		private System.Windows.Forms.RadioButton rbInterval6;
		private System.Windows.Forms.RadioButton rbInterval5;
		private System.Windows.Forms.RadioButton rbInterval4;
		private System.Windows.Forms.RadioButton rbInterval3;
		private System.Windows.Forms.RadioButton rbInterval2;
		private System.Windows.Forms.RadioButton rbInterval1;
		private System.Windows.Forms.RadioButton rbInterval8;
		private System.Windows.Forms.Button btnShow;
		private System.Windows.Forms.Button btnReset;
	}
}