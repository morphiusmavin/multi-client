
namespace EpServerEngineSampleClient
{
	partial class DeleteRec
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
			this.tbRecord = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tbEndingRec = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbRecord
			// 
			this.tbRecord.Location = new System.Drawing.Point(21, 41);
			this.tbRecord.Name = "tbRecord";
			this.tbRecord.Size = new System.Drawing.Size(100, 20);
			this.tbRecord.TabIndex = 0;
			this.tbRecord.TextChanged += new System.EventHandler(this.tbRecord_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(127, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Starting Record to Delete";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(21, 142);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(114, 142);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// tbEndingRec
			// 
			this.tbEndingRec.Location = new System.Drawing.Point(21, 106);
			this.tbEndingRec.Name = "tbEndingRec";
			this.tbEndingRec.Size = new System.Drawing.Size(100, 20);
			this.tbEndingRec.TabIndex = 5;
			this.tbEndingRec.TextChanged += new System.EventHandler(this.tbEndingRec_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(184, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "No. Records to Delete (blank if just 1)";
			// 
			// DeleteRec
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(233, 180);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbEndingRec);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbRecord);
			this.Name = "DeleteRec";
			this.Text = "DeleteRec";
			this.Load += new System.EventHandler(this.DeleteRec_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbRecord;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox tbEndingRec;
		private System.Windows.Forms.Label label2;
	}
}