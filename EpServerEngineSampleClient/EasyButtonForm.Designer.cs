
namespace EpServerEngineSampleClient
{
	partial class EasyButtonForm
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
			this.lbClientType = new System.Windows.Forms.ListBox();
			this.lbPort = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnAssign = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.rbFunc1 = new System.Windows.Forms.RadioButton();
			this.rbFunc2 = new System.Windows.Forms.RadioButton();
			this.rbFunc3 = new System.Windows.Forms.RadioButton();
			this.btnQuit = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tbReceived = new System.Windows.Forms.TextBox();
			this.rbFunc4 = new System.Windows.Forms.RadioButton();
			this.rbFunc5 = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// lbClientType
			// 
			this.lbClientType.FormattingEnabled = true;
			this.lbClientType.Items.AddRange(new object[] {
            "Garage Lights",
            "Cabin Lights",
            "Testbench",
            "Outdoor"});
			this.lbClientType.Location = new System.Drawing.Point(41, 52);
			this.lbClientType.Name = "lbClientType";
			this.lbClientType.Size = new System.Drawing.Size(120, 56);
			this.lbClientType.TabIndex = 0;
			this.lbClientType.SelectedIndexChanged += new System.EventHandler(this.lbClientType_SelectedIndexChanged);
			// 
			// lbPort
			// 
			this.lbPort.FormattingEnabled = true;
			this.lbPort.Location = new System.Drawing.Point(197, 52);
			this.lbPort.Name = "lbPort";
			this.lbPort.Size = new System.Drawing.Size(120, 173);
			this.lbPort.TabIndex = 1;
			this.lbPort.SelectedIndexChanged += new System.EventHandler(this.lbPort_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(38, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Client Type";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(194, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(26, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Port";
			// 
			// btnAssign
			// 
			this.btnAssign.Location = new System.Drawing.Point(39, 239);
			this.btnAssign.Name = "btnAssign";
			this.btnAssign.Size = new System.Drawing.Size(75, 23);
			this.btnAssign.TabIndex = 4;
			this.btnAssign.Text = "Assign";
			this.btnAssign.UseVisualStyleBackColor = true;
			this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(351, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Function";
			// 
			// rbFunc1
			// 
			this.rbFunc1.AutoSize = true;
			this.rbFunc1.Location = new System.Drawing.Point(354, 59);
			this.rbFunc1.Name = "rbFunc1";
			this.rbFunc1.Size = new System.Drawing.Size(75, 17);
			this.rbFunc1.TabIndex = 10;
			this.rbFunc1.TabStop = true;
			this.rbFunc1.Text = "Function 1";
			this.rbFunc1.UseVisualStyleBackColor = true;
			this.rbFunc1.CheckedChanged += new System.EventHandler(this.rbFunc1_CheckedChanged);
			// 
			// rbFunc2
			// 
			this.rbFunc2.AutoSize = true;
			this.rbFunc2.Location = new System.Drawing.Point(354, 82);
			this.rbFunc2.Name = "rbFunc2";
			this.rbFunc2.Size = new System.Drawing.Size(75, 17);
			this.rbFunc2.TabIndex = 11;
			this.rbFunc2.TabStop = true;
			this.rbFunc2.Text = "Function 2";
			this.rbFunc2.UseVisualStyleBackColor = true;
			this.rbFunc2.CheckedChanged += new System.EventHandler(this.rbFunc2_CheckedChanged);
			// 
			// rbFunc3
			// 
			this.rbFunc3.AutoSize = true;
			this.rbFunc3.Location = new System.Drawing.Point(354, 105);
			this.rbFunc3.Name = "rbFunc3";
			this.rbFunc3.Size = new System.Drawing.Size(75, 17);
			this.rbFunc3.TabIndex = 12;
			this.rbFunc3.TabStop = true;
			this.rbFunc3.Text = "Function 3";
			this.rbFunc3.UseVisualStyleBackColor = true;
			this.rbFunc3.CheckedChanged += new System.EventHandler(this.rbFunc3_CheckedChanged);
			// 
			// btnQuit
			// 
			this.btnQuit.Location = new System.Drawing.Point(136, 239);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(75, 23);
			this.btnQuit.TabIndex = 13;
			this.btnQuit.Text = "OK";
			this.btnQuit.UseVisualStyleBackColor = true;
			this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(240, 239);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 14;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// tbReceived
			// 
			this.tbReceived.Location = new System.Drawing.Point(41, 286);
			this.tbReceived.Multiline = true;
			this.tbReceived.Name = "tbReceived";
			this.tbReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbReceived.Size = new System.Drawing.Size(401, 141);
			this.tbReceived.TabIndex = 15;
			// 
			// rbFunc4
			// 
			this.rbFunc4.AutoSize = true;
			this.rbFunc4.Location = new System.Drawing.Point(354, 128);
			this.rbFunc4.Name = "rbFunc4";
			this.rbFunc4.Size = new System.Drawing.Size(75, 17);
			this.rbFunc4.TabIndex = 16;
			this.rbFunc4.TabStop = true;
			this.rbFunc4.Text = "Function 4";
			this.rbFunc4.UseVisualStyleBackColor = true;
			this.rbFunc4.CheckedChanged += new System.EventHandler(this.rbFunc4_CheckChanged);
			// 
			// rbFunc5
			// 
			this.rbFunc5.AutoSize = true;
			this.rbFunc5.Location = new System.Drawing.Point(354, 151);
			this.rbFunc5.Name = "rbFunc5";
			this.rbFunc5.Size = new System.Drawing.Size(75, 17);
			this.rbFunc5.TabIndex = 17;
			this.rbFunc5.TabStop = true;
			this.rbFunc5.Text = "Function 5";
			this.rbFunc5.UseVisualStyleBackColor = true;
			this.rbFunc5.CheckedChanged += new System.EventHandler(this.rbFunc5_CheckChanged);
			// 
			// EasyButtonForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(476, 461);
			this.Controls.Add(this.rbFunc5);
			this.Controls.Add(this.rbFunc4);
			this.Controls.Add(this.tbReceived);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnQuit);
			this.Controls.Add(this.rbFunc3);
			this.Controls.Add(this.rbFunc2);
			this.Controls.Add(this.rbFunc1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnAssign);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lbPort);
			this.Controls.Add(this.lbClientType);
			this.Name = "EasyButtonForm";
			this.Text = "EasyButtonForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lbClientType;
		private System.Windows.Forms.ListBox lbPort;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnAssign;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton rbFunc1;
		private System.Windows.Forms.RadioButton rbFunc2;
		private System.Windows.Forms.RadioButton rbFunc3;
		private System.Windows.Forms.Button btnQuit;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox tbReceived;
		private System.Windows.Forms.RadioButton rbFunc4;
		private System.Windows.Forms.RadioButton rbFunc5;
	}
}