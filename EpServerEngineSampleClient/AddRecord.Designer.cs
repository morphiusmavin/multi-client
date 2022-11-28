
namespace EpServerEngineSampleClient
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
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tbOnHour = new System.Windows.Forms.TextBox();
			this.tbOnMinute = new System.Windows.Forms.TextBox();
			this.tbOnSecond = new System.Windows.Forms.TextBox();
			this.tbOffSecond = new System.Windows.Forms.TextBox();
			this.tbOffMinute = new System.Windows.Forms.TextBox();
			this.tbOffHour = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tbPort = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(247, 133);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(344, 133);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// tbOnHour
			// 
			this.tbOnHour.Location = new System.Drawing.Point(103, 52);
			this.tbOnHour.Name = "tbOnHour";
			this.tbOnHour.Size = new System.Drawing.Size(100, 20);
			this.tbOnHour.TabIndex = 2;
			// 
			// tbOnMinute
			// 
			this.tbOnMinute.Location = new System.Drawing.Point(232, 52);
			this.tbOnMinute.Name = "tbOnMinute";
			this.tbOnMinute.Size = new System.Drawing.Size(100, 20);
			this.tbOnMinute.TabIndex = 3;
			// 
			// tbOnSecond
			// 
			this.tbOnSecond.Location = new System.Drawing.Point(367, 52);
			this.tbOnSecond.Name = "tbOnSecond";
			this.tbOnSecond.Size = new System.Drawing.Size(100, 20);
			this.tbOnSecond.TabIndex = 4;
			// 
			// tbOffSecond
			// 
			this.tbOffSecond.Location = new System.Drawing.Point(367, 89);
			this.tbOffSecond.Name = "tbOffSecond";
			this.tbOffSecond.Size = new System.Drawing.Size(100, 20);
			this.tbOffSecond.TabIndex = 7;
			// 
			// tbOffMinute
			// 
			this.tbOffMinute.Location = new System.Drawing.Point(232, 89);
			this.tbOffMinute.Name = "tbOffMinute";
			this.tbOffMinute.Size = new System.Drawing.Size(100, 20);
			this.tbOffMinute.TabIndex = 6;
			// 
			// tbOffHour
			// 
			this.tbOffHour.Location = new System.Drawing.Point(103, 89);
			this.tbOffHour.Name = "tbOffHour";
			this.tbOffHour.Size = new System.Drawing.Size(100, 20);
			this.tbOffHour.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 59);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 8;
			this.label1.Text = "On Time";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Off Time";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(136, 25);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Hour";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(261, 25);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Minute";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(395, 25);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(44, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Second";
			// 
			// tbPort
			// 
			this.tbPort.Location = new System.Drawing.Point(103, 133);
			this.tbPort.Name = "tbPort";
			this.tbPort.Size = new System.Drawing.Size(100, 20);
			this.tbPort.TabIndex = 13;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(19, 133);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(26, 13);
			this.label6.TabIndex = 14;
			this.label6.Text = "Port";
			// 
			// AddRecord
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(505, 196);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.tbPort);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbOffSecond);
			this.Controls.Add(this.tbOffMinute);
			this.Controls.Add(this.tbOffHour);
			this.Controls.Add(this.tbOnSecond);
			this.Controls.Add(this.tbOnMinute);
			this.Controls.Add(this.tbOnHour);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Name = "AddRecord";
			this.Text = "AddRecord";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox tbOnHour;
		private System.Windows.Forms.TextBox tbOnMinute;
		private System.Windows.Forms.TextBox tbOnSecond;
		private System.Windows.Forms.TextBox tbOffSecond;
		private System.Windows.Forms.TextBox tbOffMinute;
		private System.Windows.Forms.TextBox tbOffHour;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbPort;
		private System.Windows.Forms.Label label6;
	}
}