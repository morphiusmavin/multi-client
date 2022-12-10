
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.btnDiff = new System.Windows.Forms.Button();
			this.btnDiff3 = new System.Windows.Forms.Button();
			this.btnCdata2 = new System.Windows.Forms.Button();
			this.btnReadCdata = new System.Windows.Forms.Button();
			this.btnAddRecord = new System.Windows.Forms.Button();
			this.btnCreateNew = new System.Windows.Forms.Button();
			this.btnCurrent2XML = new System.Windows.Forms.Button();
			this.tbFileName = new System.Windows.Forms.TextBox();
			this.btnClearNon = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// btncdata
			// 
			this.btncdata.Location = new System.Drawing.Point(336, 476);
			this.btncdata.Name = "btncdata";
			this.btncdata.Size = new System.Drawing.Size(122, 23);
			this.btncdata.TabIndex = 0;
			this.btncdata.Text = "Create XML from csv";
			this.btncdata.UseVisualStyleBackColor = true;
			this.btncdata.Click += new System.EventHandler(this.btncdata_Click);
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
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(54, 476);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(120, 23);
			this.btnUpdate.TabIndex = 5;
			this.btnUpdate.Text = "Grid to Current Data";
			this.btnUpdate.UseVisualStyleBackColor = true;
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// btnDiff
			// 
			this.btnDiff.Location = new System.Drawing.Point(195, 476);
			this.btnDiff.Name = "btnDiff";
			this.btnDiff.Size = new System.Drawing.Size(122, 23);
			this.btnDiff.TabIndex = 6;
			this.btnDiff.Text = "Display from XML";
			this.btnDiff.UseVisualStyleBackColor = true;
			this.btnDiff.Click += new System.EventHandler(this.btnDiff_Click);
			// 
			// btnDiff3
			// 
			this.btnDiff3.Location = new System.Drawing.Point(54, 517);
			this.btnDiff3.Name = "btnDiff3";
			this.btnDiff3.Size = new System.Drawing.Size(120, 23);
			this.btnDiff3.TabIndex = 8;
			this.btnDiff3.Text = "Current data to grid";
			this.btnDiff3.UseVisualStyleBackColor = true;
			this.btnDiff3.Click += new System.EventHandler(this.btnDiff3_Click);
			// 
			// btnCdata2
			// 
			this.btnCdata2.Location = new System.Drawing.Point(481, 476);
			this.btnCdata2.Name = "btnCdata2";
			this.btnCdata2.Size = new System.Drawing.Size(120, 23);
			this.btnCdata2.TabIndex = 9;
			this.btnCdata2.Text = "create cdata flat file";
			this.btnCdata2.UseVisualStyleBackColor = true;
			this.btnCdata2.Click += new System.EventHandler(this.btnCdata2_Click);
			// 
			// btnReadCdata
			// 
			this.btnReadCdata.Location = new System.Drawing.Point(481, 519);
			this.btnReadCdata.Name = "btnReadCdata";
			this.btnReadCdata.Size = new System.Drawing.Size(120, 23);
			this.btnReadCdata.TabIndex = 10;
			this.btnReadCdata.Text = "read cdata flat file";
			this.btnReadCdata.UseVisualStyleBackColor = true;
			this.btnReadCdata.Click += new System.EventHandler(this.btnReadCdata_Click);
			// 
			// btnAddRecord
			// 
			this.btnAddRecord.Location = new System.Drawing.Point(632, 476);
			this.btnAddRecord.Name = "btnAddRecord";
			this.btnAddRecord.Size = new System.Drawing.Size(120, 23);
			this.btnAddRecord.TabIndex = 11;
			this.btnAddRecord.Text = "Add Rec.";
			this.btnAddRecord.UseVisualStyleBackColor = true;
			this.btnAddRecord.Click += new System.EventHandler(this.btnAddRecord_Click);
			// 
			// btnCreateNew
			// 
			this.btnCreateNew.Location = new System.Drawing.Point(336, 517);
			this.btnCreateNew.Name = "btnCreateNew";
			this.btnCreateNew.Size = new System.Drawing.Size(122, 23);
			this.btnCreateNew.TabIndex = 12;
			this.btnCreateNew.Text = "Create a New csv file";
			this.btnCreateNew.UseVisualStyleBackColor = true;
			this.btnCreateNew.Click += new System.EventHandler(this.btnCreateNew_Click);
			// 
			// btnCurrent2XML
			// 
			this.btnCurrent2XML.Location = new System.Drawing.Point(195, 517);
			this.btnCurrent2XML.Name = "btnCurrent2XML";
			this.btnCurrent2XML.Size = new System.Drawing.Size(122, 23);
			this.btnCurrent2XML.TabIndex = 13;
			this.btnCurrent2XML.Text = "Current data to XML";
			this.btnCurrent2XML.UseVisualStyleBackColor = true;
			this.btnCurrent2XML.Click += new System.EventHandler(this.btnCurrent2XML_Click);
			// 
			// tbFileName
			// 
			this.tbFileName.Location = new System.Drawing.Point(818, 521);
			this.tbFileName.Name = "tbFileName";
			this.tbFileName.Size = new System.Drawing.Size(223, 20);
			this.tbFileName.TabIndex = 15;
			// 
			// btnClearNon
			// 
			this.btnClearNon.Location = new System.Drawing.Point(632, 519);
			this.btnClearNon.Name = "btnClearNon";
			this.btnClearNon.Size = new System.Drawing.Size(120, 23);
			this.btnClearNon.TabIndex = 17;
			this.btnClearNon.Text = "Clear Non";
			this.btnClearNon.UseVisualStyleBackColor = true;
			this.btnClearNon.Click += new System.EventHandler(this.btnClearNon_Click);
			// 
			// CDBMgmt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1236, 628);
			this.Controls.Add(this.btnClearNon);
			this.Controls.Add(this.tbFileName);
			this.Controls.Add(this.btnCurrent2XML);
			this.Controls.Add(this.btnCreateNew);
			this.Controls.Add(this.btnAddRecord);
			this.Controls.Add(this.btnReadCdata);
			this.Controls.Add(this.btnCdata2);
			this.Controls.Add(this.btnDiff3);
			this.Controls.Add(this.btnDiff);
			this.Controls.Add(this.btnUpdate);
			this.Controls.Add(this.dataGridView1);
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
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.Button btnDiff;
		private System.Windows.Forms.Button btnDiff3;
		private System.Windows.Forms.Button btnCdata2;
		private System.Windows.Forms.Button btnReadCdata;
		private System.Windows.Forms.Button btnAddRecord;
		private System.Windows.Forms.Button btnCreateNew;
		private System.Windows.Forms.Button btnCurrent2XML;
		private System.Windows.Forms.TextBox tbFileName;
		private System.Windows.Forms.Button btnClearNon;
	}
}

