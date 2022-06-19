
namespace EpServerEngineSampleClient
{
	partial class SetNextClient
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
			this.cbNextClient = new System.Windows.Forms.ComboBox();
			this.btnSetNextClient = new System.Windows.Forms.Button();
			this.btnSend = new System.Windows.Forms.Button();
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
            "STOP"});
			this.cbSource.Location = new System.Drawing.Point(37, 29);
			this.cbSource.Name = "cbSource";
			this.cbSource.Size = new System.Drawing.Size(166, 26);
			this.cbSource.TabIndex = 1;
			this.cbSource.SelectedIndexChanged += new System.EventHandler(this.cbSource_SelectedIndexChanged);
			// 
			// cbNextClient
			// 
			this.cbNextClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cbNextClient.FormattingEnabled = true;
			this.cbNextClient.Items.AddRange(new object[] {
            "TS-client 1 (154)",
            "TS-client 2 (147)",
            "TS-client 3 (150)",
            "TS-client 4 (151)",
            "TS-client 5 (155)",
            "TS-client 6 (145)",
            "STOP"});
			this.cbNextClient.Location = new System.Drawing.Point(243, 29);
			this.cbNextClient.Name = "cbNextClient";
			this.cbNextClient.Size = new System.Drawing.Size(166, 26);
			this.cbNextClient.TabIndex = 2;
			this.cbNextClient.SelectedIndexChanged += new System.EventHandler(this.cbNextClient_SelectedIndexChanged);
			// 
			// btnSetNextClient
			// 
			this.btnSetNextClient.Location = new System.Drawing.Point(37, 226);
			this.btnSetNextClient.Name = "btnSetNextClient";
			this.btnSetNextClient.Size = new System.Drawing.Size(75, 23);
			this.btnSetNextClient.TabIndex = 3;
			this.btnSetNextClient.Text = "Set";
			this.btnSetNextClient.UseVisualStyleBackColor = true;
			this.btnSetNextClient.Click += new System.EventHandler(this.btnSetNextClient_Click);
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(128, 226);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(75, 23);
			this.btnSend.TabIndex = 4;
			this.btnSend.Text = "Send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// SetNextClient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(459, 261);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.btnSetNextClient);
			this.Controls.Add(this.cbNextClient);
			this.Controls.Add(this.cbSource);
			this.Name = "SetNextClient";
			this.Text = "SetNextClient";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox cbSource;
		private System.Windows.Forms.ComboBox cbNextClient;
		private System.Windows.Forms.Button btnSetNextClient;
		private System.Windows.Forms.Button btnSend;
	}
}