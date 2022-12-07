
namespace ODBMgmt
{
	partial class Form1
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.btnCreateXML = new System.Windows.Forms.Button();
			this.btnCreateDAT = new System.Windows.Forms.Button();
			this.tbFileName = new System.Windows.Forms.TextBox();
			this.btnCreateCSV = new System.Windows.Forms.Button();
			this.btn2Grid = new System.Windows.Forms.Button();
			this.btnGrid2Data = new System.Windows.Forms.Button();
			this.btnReadDATFile = new System.Windows.Forms.Button();
			this.btnXML2grid = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(181, 60);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(1050, 529);
			this.dataGridView1.TabIndex = 0;
			// 
			// btnCreateXML
			// 
			this.btnCreateXML.Location = new System.Drawing.Point(21, 60);
			this.btnCreateXML.Name = "btnCreateXML";
			this.btnCreateXML.Size = new System.Drawing.Size(139, 23);
			this.btnCreateXML.TabIndex = 1;
			this.btnCreateXML.Text = "Create XML from CSV";
			this.btnCreateXML.UseVisualStyleBackColor = true;
			this.btnCreateXML.Click += new System.EventHandler(this.btnCreateXML_Click);
			// 
			// btnCreateDAT
			// 
			this.btnCreateDAT.Location = new System.Drawing.Point(21, 108);
			this.btnCreateDAT.Name = "btnCreateDAT";
			this.btnCreateDAT.Size = new System.Drawing.Size(139, 23);
			this.btnCreateDAT.TabIndex = 2;
			this.btnCreateDAT.Text = "Create DAT from CSV";
			this.btnCreateDAT.UseVisualStyleBackColor = true;
			this.btnCreateDAT.Click += new System.EventHandler(this.btnCreateDAT_Click);
			// 
			// tbFileName
			// 
			this.tbFileName.Location = new System.Drawing.Point(150, 15);
			this.tbFileName.Name = "tbFileName";
			this.tbFileName.Size = new System.Drawing.Size(322, 20);
			this.tbFileName.TabIndex = 3;
			// 
			// btnCreateCSV
			// 
			this.btnCreateCSV.Location = new System.Drawing.Point(21, 154);
			this.btnCreateCSV.Name = "btnCreateCSV";
			this.btnCreateCSV.Size = new System.Drawing.Size(139, 23);
			this.btnCreateCSV.TabIndex = 4;
			this.btnCreateCSV.Text = "Create blank CSV";
			this.btnCreateCSV.UseVisualStyleBackColor = true;
			this.btnCreateCSV.Click += new System.EventHandler(this.btnCreateCSV_Click);
			// 
			// btn2Grid
			// 
			this.btn2Grid.Location = new System.Drawing.Point(21, 202);
			this.btn2Grid.Name = "btn2Grid";
			this.btn2Grid.Size = new System.Drawing.Size(139, 23);
			this.btn2Grid.TabIndex = 5;
			this.btn2Grid.Text = "Data 2 Grid";
			this.btn2Grid.UseVisualStyleBackColor = true;
			this.btn2Grid.Click += new System.EventHandler(this.btn2Grid_Click);
			// 
			// btnGrid2Data
			// 
			this.btnGrid2Data.Location = new System.Drawing.Point(21, 247);
			this.btnGrid2Data.Name = "btnGrid2Data";
			this.btnGrid2Data.Size = new System.Drawing.Size(139, 23);
			this.btnGrid2Data.TabIndex = 6;
			this.btnGrid2Data.Text = "Grid 2 Data";
			this.btnGrid2Data.UseVisualStyleBackColor = true;
			this.btnGrid2Data.Click += new System.EventHandler(this.btnGrid2Data_Click);
			// 
			// btnReadDATFile
			// 
			this.btnReadDATFile.Location = new System.Drawing.Point(21, 295);
			this.btnReadDATFile.Name = "btnReadDATFile";
			this.btnReadDATFile.Size = new System.Drawing.Size(139, 23);
			this.btnReadDATFile.TabIndex = 7;
			this.btnReadDATFile.Text = "Read DAT to grid";
			this.btnReadDATFile.UseVisualStyleBackColor = true;
			this.btnReadDATFile.Click += new System.EventHandler(this.btnReadDATFile_Click);
			// 
			// btnXML2grid
			// 
			this.btnXML2grid.Location = new System.Drawing.Point(21, 345);
			this.btnXML2grid.Name = "btnXML2grid";
			this.btnXML2grid.Size = new System.Drawing.Size(139, 23);
			this.btnXML2grid.TabIndex = 8;
			this.btnXML2grid.Text = "Read XML to grid";
			this.btnXML2grid.UseVisualStyleBackColor = true;
			this.btnXML2grid.Click += new System.EventHandler(this.btnXML2grid_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1247, 601);
			this.Controls.Add(this.btnXML2grid);
			this.Controls.Add(this.btnReadDATFile);
			this.Controls.Add(this.btnGrid2Data);
			this.Controls.Add(this.btn2Grid);
			this.Controls.Add(this.btnCreateCSV);
			this.Controls.Add(this.tbFileName);
			this.Controls.Add(this.btnCreateDAT);
			this.Controls.Add(this.btnCreateXML);
			this.Controls.Add(this.dataGridView1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button btnCreateXML;
		private System.Windows.Forms.Button btnCreateDAT;
		private System.Windows.Forms.TextBox tbFileName;
		private System.Windows.Forms.Button btnCreateCSV;
		private System.Windows.Forms.Button btn2Grid;
		private System.Windows.Forms.Button btnGrid2Data;
		private System.Windows.Forms.Button btnReadDATFile;
		private System.Windows.Forms.Button btnXML2grid;
	}
}

