
namespace EpServerEngineSampleClient
{
	partial class TimerSchedule
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
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lbClientType = new System.Windows.Forms.ListBox();
			this.lbPort = new System.Windows.Forms.ListBox();
			this.tbReceived = new System.Windows.Forms.TextBox();
			this.cbHourOn = new System.Windows.Forms.ComboBox();
			this.cbMinuteOn = new System.Windows.Forms.ComboBox();
			this.cbSecondOn = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.cbSecondOff = new System.Windows.Forms.ComboBox();
			this.cbMinuteOff = new System.Windows.Forms.ComboBox();
			this.cbHourOff = new System.Windows.Forms.ComboBox();
			this.btnShow = new System.Windows.Forms.Button();
			this.btnSingle = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnRefresh
			// 
			this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRefresh.Location = new System.Drawing.Point(12, 272);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(160, 42);
			this.btnRefresh.TabIndex = 0;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = false;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// btnOK
			// 
			this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOK.Location = new System.Drawing.Point(12, 554);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(160, 42);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(12, 506);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(160, 42);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lbClientType
			// 
			this.lbClientType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbClientType.FormattingEnabled = true;
			this.lbClientType.ItemHeight = 18;
			this.lbClientType.Items.AddRange(new object[] {
            "garage",
            "cabin",
            "testbench"});
			this.lbClientType.Location = new System.Drawing.Point(33, 40);
			this.lbClientType.Name = "lbClientType";
			this.lbClientType.Size = new System.Drawing.Size(120, 94);
			this.lbClientType.TabIndex = 3;
			this.lbClientType.SelectedIndexChanged += new System.EventHandler(this.lbClientType_SelectedIndexChanged);
			// 
			// lbPort
			// 
			this.lbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbPort.FormattingEnabled = true;
			this.lbPort.ItemHeight = 18;
			this.lbPort.Location = new System.Drawing.Point(190, 40);
			this.lbPort.Name = "lbPort";
			this.lbPort.Size = new System.Drawing.Size(189, 256);
			this.lbPort.TabIndex = 4;
			this.lbPort.SelectedIndexChanged += new System.EventHandler(this.lbPort_SelectedIndexChanged);
			// 
			// tbReceived
			// 
			this.tbReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbReceived.Location = new System.Drawing.Point(190, 321);
			this.tbReceived.Multiline = true;
			this.tbReceived.Name = "tbReceived";
			this.tbReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbReceived.Size = new System.Drawing.Size(567, 272);
			this.tbReceived.TabIndex = 5;
			// 
			// cbHourOn
			// 
			this.cbHourOn.FormattingEnabled = true;
			this.cbHourOn.Location = new System.Drawing.Point(36, 46);
			this.cbHourOn.Name = "cbHourOn";
			this.cbHourOn.Size = new System.Drawing.Size(68, 21);
			this.cbHourOn.TabIndex = 6;
			// 
			// cbMinuteOn
			// 
			this.cbMinuteOn.FormattingEnabled = true;
			this.cbMinuteOn.Location = new System.Drawing.Point(123, 46);
			this.cbMinuteOn.Name = "cbMinuteOn";
			this.cbMinuteOn.Size = new System.Drawing.Size(68, 21);
			this.cbMinuteOn.TabIndex = 7;
			// 
			// cbSecondOn
			// 
			this.cbSecondOn.FormattingEnabled = true;
			this.cbSecondOn.Location = new System.Drawing.Point(206, 46);
			this.cbSecondOn.Name = "cbSecondOn";
			this.cbSecondOn.Size = new System.Drawing.Size(68, 21);
			this.cbSecondOn.TabIndex = 8;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cbSecondOn);
			this.groupBox1.Controls.Add(this.cbMinuteOn);
			this.groupBox1.Controls.Add(this.cbHourOn);
			this.groupBox1.Location = new System.Drawing.Point(434, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(323, 82);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Start Time";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(215, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(55, 16);
			this.label3.TabIndex = 11;
			this.label3.Text = "Second";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(130, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 16);
			this.label2.TabIndex = 10;
			this.label2.Text = "Minute";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(47, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 16);
			this.label1.TabIndex = 9;
			this.label1.Text = "Hour";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.cbSecondOff);
			this.groupBox2.Controls.Add(this.cbMinuteOff);
			this.groupBox2.Controls.Add(this.cbHourOff);
			this.groupBox2.Location = new System.Drawing.Point(434, 143);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(323, 82);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "End Time";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(215, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 16);
			this.label4.TabIndex = 11;
			this.label4.Text = "Second";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(130, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 16);
			this.label5.TabIndex = 10;
			this.label5.Text = "Minute";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(47, 24);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(37, 16);
			this.label6.TabIndex = 9;
			this.label6.Text = "Hour";
			// 
			// cbSecondOff
			// 
			this.cbSecondOff.FormattingEnabled = true;
			this.cbSecondOff.Location = new System.Drawing.Point(206, 46);
			this.cbSecondOff.Name = "cbSecondOff";
			this.cbSecondOff.Size = new System.Drawing.Size(68, 21);
			this.cbSecondOff.TabIndex = 8;
			// 
			// cbMinuteOff
			// 
			this.cbMinuteOff.FormattingEnabled = true;
			this.cbMinuteOff.Location = new System.Drawing.Point(123, 46);
			this.cbMinuteOff.Name = "cbMinuteOff";
			this.cbMinuteOff.Size = new System.Drawing.Size(68, 21);
			this.cbMinuteOff.TabIndex = 7;
			// 
			// cbHourOff
			// 
			this.cbHourOff.FormattingEnabled = true;
			this.cbHourOff.Location = new System.Drawing.Point(36, 46);
			this.cbHourOff.Name = "cbHourOff";
			this.cbHourOff.Size = new System.Drawing.Size(68, 21);
			this.cbHourOff.TabIndex = 6;
			// 
			// btnShow
			// 
			this.btnShow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShow.Location = new System.Drawing.Point(12, 377);
			this.btnShow.Name = "btnShow";
			this.btnShow.Size = new System.Drawing.Size(160, 42);
			this.btnShow.TabIndex = 11;
			this.btnShow.Text = "Show";
			this.btnShow.UseVisualStyleBackColor = false;
			this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
			// 
			// btnSingle
			// 
			this.btnSingle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnSingle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSingle.Location = new System.Drawing.Point(12, 327);
			this.btnSingle.Name = "btnSingle";
			this.btnSingle.Size = new System.Drawing.Size(160, 42);
			this.btnSingle.TabIndex = 12;
			this.btnSingle.Text = "Single Rec";
			this.btnSingle.UseVisualStyleBackColor = false;
			this.btnSingle.Click += new System.EventHandler(this.btnSingle_Click);
			// 
			// TimerSchedule
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(800, 620);
			this.Controls.Add(this.btnSingle);
			this.Controls.Add(this.btnShow);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tbReceived);
			this.Controls.Add(this.lbPort);
			this.Controls.Add(this.lbClientType);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnRefresh);
			this.Name = "TimerSchedule";
			this.Text = "TimerSchedule";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ListBox lbClientType;
		private System.Windows.Forms.ListBox lbPort;
		private System.Windows.Forms.TextBox tbReceived;
		private System.Windows.Forms.ComboBox cbHourOn;
		private System.Windows.Forms.ComboBox cbMinuteOn;
		private System.Windows.Forms.ComboBox cbSecondOn;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cbSecondOff;
		private System.Windows.Forms.ComboBox cbMinuteOff;
		private System.Windows.Forms.ComboBox cbHourOff;
		private System.Windows.Forms.Button btnShow;
		private System.Windows.Forms.Button btnSingle;
	}
}