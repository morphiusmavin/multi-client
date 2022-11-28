
namespace CDBMgmt
{
	partial class AddRecord
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
			this.tbOnHour = new System.Windows.Forms.TextBox();
			this.tbOnMinute = new System.Windows.Forms.TextBox();
			this.tbOnSecond = new System.Windows.Forms.TextBox();
			this.tbOffSecond = new System.Windows.Forms.TextBox();
			this.tbOffMinute = new System.Windows.Forms.TextBox();
			this.tbOffHour = new System.Windows.Forms.TextBox();
			this.tbPort = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.tbLabel = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbOnHour
			// 
			this.tbOnHour.Location = new System.Drawing.Point(146, 140);
			this.tbOnHour.Name = "tbOnHour";
			this.tbOnHour.Size = new System.Drawing.Size(100, 20);
			this.tbOnHour.TabIndex = 0;
			// 
			// tbOnMinute
			// 
			this.tbOnMinute.Location = new System.Drawing.Point(301, 140);
			this.tbOnMinute.Name = "tbOnMinute";
			this.tbOnMinute.Size = new System.Drawing.Size(100, 20);
			this.tbOnMinute.TabIndex = 1;
			// 
			// tbOnSecond
			// 
			this.tbOnSecond.Location = new System.Drawing.Point(458, 140);
			this.tbOnSecond.Name = "tbOnSecond";
			this.tbOnSecond.Size = new System.Drawing.Size(100, 20);
			this.tbOnSecond.TabIndex = 2;
			// 
			// tbOffSecond
			// 
			this.tbOffSecond.Location = new System.Drawing.Point(458, 193);
			this.tbOffSecond.Name = "tbOffSecond";
			this.tbOffSecond.Size = new System.Drawing.Size(100, 20);
			this.tbOffSecond.TabIndex = 5;
			// 
			// tbOffMinute
			// 
			this.tbOffMinute.Location = new System.Drawing.Point(301, 193);
			this.tbOffMinute.Name = "tbOffMinute";
			this.tbOffMinute.Size = new System.Drawing.Size(100, 20);
			this.tbOffMinute.TabIndex = 4;
			// 
			// tbOffHour
			// 
			this.tbOffHour.Location = new System.Drawing.Point(146, 193);
			this.tbOffHour.Name = "tbOffHour";
			this.tbOffHour.Size = new System.Drawing.Size(100, 20);
			this.tbOffHour.TabIndex = 3;
			// 
			// tbPort
			// 
			this.tbPort.Location = new System.Drawing.Point(146, 240);
			this.tbPort.Name = "tbPort";
			this.tbPort.Size = new System.Drawing.Size(100, 20);
			this.tbPort.TabIndex = 6;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(146, 296);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(90, 29);
			this.btnOK.TabIndex = 7;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
			// 
			// tbLabel
			// 
			this.tbLabel.Location = new System.Drawing.Point(182, 76);
			this.tbLabel.Name = "tbLabel";
			this.tbLabel.Size = new System.Drawing.Size(145, 20);
			this.tbLabel.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(129, 79);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(36, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Label ";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(71, 143);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "On Time";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(71, 193);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Off Time";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(485, 115);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(44, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Second";
			this.label4.Click += new System.EventHandler(this.label4_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(330, 115);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(39, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Minute";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(179, 115);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(30, 13);
			this.label6.TabIndex = 14;
			this.label6.Text = "Hour";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(71, 240);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(26, 13);
			this.label7.TabIndex = 15;
			this.label7.Text = "Port";
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(270, 296);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(90, 29);
			this.btnCancel.TabIndex = 16;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
			// 
			// AddRecord
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbLabel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tbPort);
			this.Controls.Add(this.tbOffSecond);
			this.Controls.Add(this.tbOffMinute);
			this.Controls.Add(this.tbOffHour);
			this.Controls.Add(this.tbOnSecond);
			this.Controls.Add(this.tbOnMinute);
			this.Controls.Add(this.tbOnHour);
			this.Name = "AddRecord";
			this.Text = "AddRecord";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbOnHour;
		private System.Windows.Forms.TextBox tbOnMinute;
		private System.Windows.Forms.TextBox tbOnSecond;
		private System.Windows.Forms.TextBox tbOffSecond;
		private System.Windows.Forms.TextBox tbOffMinute;
		private System.Windows.Forms.TextBox tbOffHour;
		private System.Windows.Forms.TextBox tbPort;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox tbLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnCancel;
	}
}