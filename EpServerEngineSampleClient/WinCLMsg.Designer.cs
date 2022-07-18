
namespace EpServerEngineSampleClient
{
	partial class WinCLMsg
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
			this.cbCmd = new System.Windows.Forms.ComboBox();
			this.cbDest = new System.Windows.Forms.ComboBox();
			this.tbTextToSend = new System.Windows.Forms.TextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.tbReceived = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cbCmd
			// 
			this.cbCmd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.cbCmd.FormattingEnabled = true;
			this.cbCmd.Items.AddRange(new object[] {
            "SEND_MESSAGE",
            "SEND_STATUS",
            "SET_TIME",
            "GET_TIME",
            "DISCONNECT"});
			this.cbCmd.Location = new System.Drawing.Point(238, 44);
			this.cbCmd.Name = "cbCmd";
			this.cbCmd.Size = new System.Drawing.Size(212, 21);
			this.cbCmd.TabIndex = 0;
			this.cbCmd.SelectedIndexChanged += new System.EventHandler(this.cbCmd_SelectedIndexChanged);
			// 
			// cbDest
			// 
			this.cbDest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.cbDest.FormattingEnabled = true;
			this.cbDest.Items.AddRange(new object[] {
            "Second Windows",
            "Win7-x86"});
			this.cbDest.Location = new System.Drawing.Point(238, 97);
			this.cbDest.Name = "cbDest";
			this.cbDest.Size = new System.Drawing.Size(212, 21);
			this.cbDest.TabIndex = 1;
			this.cbDest.SelectedIndexChanged += new System.EventHandler(this.cbDest_SelectedIndexChanged);
			// 
			// tbTextToSend
			// 
			this.tbTextToSend.Location = new System.Drawing.Point(238, 181);
			this.tbTextToSend.Multiline = true;
			this.tbTextToSend.Name = "tbTextToSend";
			this.tbTextToSend.Size = new System.Drawing.Size(212, 88);
			this.tbTextToSend.TabIndex = 2;
			// 
			// btnSend
			// 
			this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
			this.btnSend.Location = new System.Drawing.Point(52, 39);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(96, 46);
			this.btnSend.TabIndex = 3;
			this.btnSend.Text = "Send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// tbReceived
			// 
			this.tbReceived.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbReceived.Location = new System.Drawing.Point(473, 12);
			this.tbReceived.Multiline = true;
			this.tbReceived.Name = "tbReceived";
			this.tbReceived.ReadOnly = true;
			this.tbReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbReceived.Size = new System.Drawing.Size(302, 257);
			this.tbReceived.TabIndex = 17;
			this.tbReceived.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(234, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(149, 20);
			this.label1.TabIndex = 18;
			this.label1.Text = "Message to Send";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(234, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(176, 20);
			this.label2.TabIndex = 19;
			this.label2.Text = "Win Client to send to";
			// 
			// WinCLMsg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(787, 281);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbReceived);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.tbTextToSend);
			this.Controls.Add(this.cbDest);
			this.Controls.Add(this.cbCmd);
			this.Name = "WinCLMsg";
			this.Text = "WinCLMsg";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cbCmd;
		private System.Windows.Forms.ComboBox cbDest;
		private System.Windows.Forms.TextBox tbTextToSend;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.TextBox tbReceived;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}