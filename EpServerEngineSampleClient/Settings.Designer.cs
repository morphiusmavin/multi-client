
namespace EpServerEngineSampleClient
{
	partial class Settings
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
			this.cbSilentMode = new System.Windows.Forms.CheckBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.chPlayChimes = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(23, 402);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(117, 402);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// cbSilentMode
			// 
			this.cbSilentMode.AutoSize = true;
			this.cbSilentMode.Location = new System.Drawing.Point(32, 36);
			this.cbSilentMode.Name = "cbSilentMode";
			this.cbSilentMode.Size = new System.Drawing.Size(82, 17);
			this.cbSilentMode.TabIndex = 2;
			this.cbSilentMode.Text = "Silent Mode";
			this.cbSilentMode.UseVisualStyleBackColor = true;
			this.cbSilentMode.CheckedChanged += new System.EventHandler(this.ccSilentMode);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(32, 118);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(160, 20);
			this.textBox1.TabIndex = 3;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(32, 153);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(160, 20);
			this.textBox2.TabIndex = 4;
			// 
			// chPlayChimes
			// 
			this.chPlayChimes.AutoSize = true;
			this.chPlayChimes.Location = new System.Drawing.Point(32, 68);
			this.chPlayChimes.Name = "chPlayChimes";
			this.chPlayChimes.Size = new System.Drawing.Size(83, 17);
			this.chPlayChimes.TabIndex = 5;
			this.chPlayChimes.Text = "Play Chimes";
			this.chPlayChimes.UseVisualStyleBackColor = true;
			this.chPlayChimes.CheckedChanged += new System.EventHandler(this.chPlayChimes_CheckedChanged);
			// 
			// Settings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(246, 450);
			this.Controls.Add(this.chPlayChimes);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.cbSilentMode);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Name = "Settings";
			this.Text = "Settings";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.CheckBox cbSilentMode;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.CheckBox chPlayChimes;
	}
}