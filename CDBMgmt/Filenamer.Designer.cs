
namespace CDBMgmt
{
	partial class Filenamer
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
			this.tbFilename = new System.Windows.Forms.TextBox();
			this.Prompt_label = new System.Windows.Forms.Label();
			this.Ext_Label = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbFilename
			// 
			this.tbFilename.Location = new System.Drawing.Point(35, 55);
			this.tbFilename.Name = "tbFilename";
			this.tbFilename.Size = new System.Drawing.Size(100, 20);
			this.tbFilename.TabIndex = 0;
			this.tbFilename.TextChanged += new System.EventHandler(this.tbFilename_TextChanged);
			// 
			// Prompt_label
			// 
			this.Prompt_label.AutoSize = true;
			this.Prompt_label.Location = new System.Drawing.Point(32, 23);
			this.Prompt_label.Name = "Prompt_label";
			this.Prompt_label.Size = new System.Drawing.Size(35, 13);
			this.Prompt_label.TabIndex = 1;
			this.Prompt_label.Text = "label1";
			// 
			// Ext_Label
			// 
			this.Ext_Label.AutoSize = true;
			this.Ext_Label.Location = new System.Drawing.Point(141, 58);
			this.Ext_Label.Name = "Ext_Label";
			this.Ext_Label.Size = new System.Drawing.Size(35, 13);
			this.Ext_Label.TabIndex = 2;
			this.Ext_Label.Text = "label2";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(12, 125);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(101, 125);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// Filenamer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(335, 160);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.Ext_Label);
			this.Controls.Add(this.Prompt_label);
			this.Controls.Add(this.tbFilename);
			this.Name = "Filenamer";
			this.Text = "Filenamer";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbFilename;
		private System.Windows.Forms.Label Prompt_label;
		private System.Windows.Forms.Label Ext_Label;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
	}
}