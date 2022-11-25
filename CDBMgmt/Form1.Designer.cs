
namespace CDBMgmt
{
	partial class CDBMgmt
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
			this.btncdata = new System.Windows.Forms.Button();
			this.btntdata = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.btnDiff = new System.Windows.Forms.Button();
			this.btnDiff2 = new System.Windows.Forms.Button();
			this.btnDiff3 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// btncdata
			// 
			this.btncdata.Location = new System.Drawing.Point(12, 593);
			this.btncdata.Name = "btncdata";
			this.btncdata.Size = new System.Drawing.Size(75, 23);
			this.btncdata.TabIndex = 0;
			this.btncdata.Text = "cdata";
			this.btncdata.UseVisualStyleBackColor = true;
			this.btncdata.Click += new System.EventHandler(this.btncdata_Click);
			// 
			// btntdata
			// 
			this.btntdata.Location = new System.Drawing.Point(102, 593);
			this.btntdata.Name = "btntdata";
			this.btntdata.Size = new System.Drawing.Size(75, 23);
			this.btntdata.TabIndex = 1;
			this.btntdata.Text = "tdata";
			this.btntdata.UseVisualStyleBackColor = true;
			this.btntdata.Click += new System.EventHandler(this.btntdata_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 577);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(476, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "cdata & tdata creates, respectively, a cdata.xml from a cdata.csv and a tdata.xml" +
    " from a tdata.csv file";
			// 
			// dataGridView1
			// 
			this.dataGridView1.Location = new System.Drawing.Point(49, 24);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(1163, 424);
			this.dataGridView1.TabIndex = 3;
			this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CellClick);
			this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.RowEnter);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(49, 489);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(63, 20);
			this.textBox1.TabIndex = 4;
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(568, 497);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(75, 23);
			this.btnUpdate.TabIndex = 5;
			this.btnUpdate.Text = "Update";
			this.btnUpdate.UseVisualStyleBackColor = true;
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// btnDiff
			// 
			this.btnDiff.Location = new System.Drawing.Point(673, 497);
			this.btnDiff.Name = "btnDiff";
			this.btnDiff.Size = new System.Drawing.Size(75, 23);
			this.btnDiff.TabIndex = 6;
			this.btnDiff.Text = "Diff";
			this.btnDiff.UseVisualStyleBackColor = true;
			this.btnDiff.Click += new System.EventHandler(this.btnDiff_Click);
			// 
			// btnDiff2
			// 
			this.btnDiff2.Location = new System.Drawing.Point(777, 497);
			this.btnDiff2.Name = "btnDiff2";
			this.btnDiff2.Size = new System.Drawing.Size(75, 23);
			this.btnDiff2.TabIndex = 7;
			this.btnDiff2.Text = "Diff";
			this.btnDiff2.UseVisualStyleBackColor = true;
			this.btnDiff2.Click += new System.EventHandler(this.btnDiff2_Click);
			// 
			// btnDiff3
			// 
			this.btnDiff3.Location = new System.Drawing.Point(882, 497);
			this.btnDiff3.Name = "btnDiff3";
			this.btnDiff3.Size = new System.Drawing.Size(75, 23);
			this.btnDiff3.TabIndex = 8;
			this.btnDiff3.Text = "Diff";
			this.btnDiff3.UseVisualStyleBackColor = true;
			this.btnDiff3.Click += new System.EventHandler(this.btnDiff3_Click);
			// 
			// CDBMgmt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1328, 628);
			this.Controls.Add(this.btnDiff3);
			this.Controls.Add(this.btnDiff2);
			this.Controls.Add(this.btnDiff);
			this.Controls.Add(this.btnUpdate);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btntdata);
			this.Controls.Add(this.btncdata);
			this.Name = "CDBMgmt";
			this.Text = "CDBMgmt";
			this.Load += new System.EventHandler(this.LoadForm);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btncdata;
		private System.Windows.Forms.Button btntdata;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Button btnDiff;
		private System.Windows.Forms.Button btnDiff2;
		private System.Windows.Forms.Button btnDiff3;
	}
}

