
namespace EpServerEngineSampleClient
{
	partial class TimerSchedule
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
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lbClientType = new System.Windows.Forms.ListBox();
			this.tbReceived = new System.Windows.Forms.TextBox();
			this.btnShow = new System.Windows.Forms.Button();
			this.btnSendRecs = new System.Windows.Forms.Button();
			this.CGridView = new System.Windows.Forms.DataGridView();
			this.btnLoadXML = new System.Windows.Forms.Button();
			this.btnUpdateChart = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.btnAddRecord = new System.Windows.Forms.Button();
			this.btnDeleteRecord = new System.Windows.Forms.Button();
			this.btnChart2Cdata = new System.Windows.Forms.Button();
			this.btnClearNonRecs = new System.Windows.Forms.Button();
			this.btnCdata2XML = new System.Windows.Forms.Button();
			this.btnClearTarget = new System.Windows.Forms.Button();
			this.btnTarget2Disk = new System.Windows.Forms.Button();
			this.btnWriteXML = new System.Windows.Forms.Button();
			this.btnSort = new System.Windows.Forms.Button();
			this.btnDispSort = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.CGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// btnRefresh
			// 
			this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRefresh.Location = new System.Drawing.Point(16, 282);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(160, 42);
			this.btnRefresh.TabIndex = 0;
			this.btnRefresh.Text = "Refresh";
			this.btnRefresh.UseVisualStyleBackColor = false;
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// btnOK
			// 
			this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOK.Location = new System.Drawing.Point(1013, 543);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(160, 42);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(1138, 608);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(160, 42);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lbClientType
			// 
			this.lbClientType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbClientType.FormattingEnabled = true;
			this.lbClientType.ItemHeight = 18;
			this.lbClientType.Items.AddRange(new object[] {
            "garage",
            "cabin",
            "testbench",
            "outdoor"});
			this.lbClientType.Location = new System.Drawing.Point(12, 73);
			this.lbClientType.Name = "lbClientType";
			this.lbClientType.Size = new System.Drawing.Size(160, 94);
			this.lbClientType.TabIndex = 3;
			this.lbClientType.SelectedIndexChanged += new System.EventHandler(this.lbClientType_SelectedIndexChanged);
			// 
			// tbReceived
			// 
			this.tbReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbReceived.Location = new System.Drawing.Point(16, 543);
			this.tbReceived.Multiline = true;
			this.tbReceived.Name = "tbReceived";
			this.tbReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbReceived.Size = new System.Drawing.Size(272, 107);
			this.tbReceived.TabIndex = 5;
			// 
			// btnShow
			// 
			this.btnShow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShow.Location = new System.Drawing.Point(16, 382);
			this.btnShow.Name = "btnShow";
			this.btnShow.Size = new System.Drawing.Size(160, 42);
			this.btnShow.TabIndex = 11;
			this.btnShow.Text = "Show";
			this.btnShow.UseVisualStyleBackColor = false;
			this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
			// 
			// btnSendRecs
			// 
			this.btnSendRecs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnSendRecs.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSendRecs.Location = new System.Drawing.Point(16, 332);
			this.btnSendRecs.Name = "btnSendRecs";
			this.btnSendRecs.Size = new System.Drawing.Size(160, 42);
			this.btnSendRecs.TabIndex = 12;
			this.btnSendRecs.Text = "Send Recs";
			this.btnSendRecs.UseVisualStyleBackColor = false;
			this.btnSendRecs.Click += new System.EventHandler(this.btnSendRecs_Click);
			// 
			// CGridView
			// 
			this.CGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.CGridView.Location = new System.Drawing.Point(202, 43);
			this.CGridView.Name = "CGridView";
			this.CGridView.Size = new System.Drawing.Size(971, 481);
			this.CGridView.TabIndex = 13;
			this.CGridView.SelectionChanged += new System.EventHandler(this.SelectionChanged);
			// 
			// btnLoadXML
			// 
			this.btnLoadXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnLoadXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLoadXML.Location = new System.Drawing.Point(16, 432);
			this.btnLoadXML.Name = "btnLoadXML";
			this.btnLoadXML.Size = new System.Drawing.Size(160, 42);
			this.btnLoadXML.TabIndex = 14;
			this.btnLoadXML.Text = "Load XML";
			this.btnLoadXML.UseVisualStyleBackColor = false;
			this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);
			// 
			// btnUpdateChart
			// 
			this.btnUpdateChart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnUpdateChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUpdateChart.Location = new System.Drawing.Point(16, 233);
			this.btnUpdateChart.Name = "btnUpdateChart";
			this.btnUpdateChart.Size = new System.Drawing.Size(160, 41);
			this.btnUpdateChart.TabIndex = 15;
			this.btnUpdateChart.Text = "To Chart";
			this.btnUpdateChart.UseVisualStyleBackColor = false;
			this.btnUpdateChart.Click += new System.EventHandler(this.btnUpdateChart_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnHelp.Location = new System.Drawing.Point(834, 544);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(160, 41);
			this.btnHelp.TabIndex = 16;
			this.btnHelp.Text = "Help";
			this.btnHelp.UseVisualStyleBackColor = false;
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 20);
			this.label1.TabIndex = 17;
			this.label1.Text = "Type";
			// 
			// btnAddRecord
			// 
			this.btnAddRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnAddRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAddRecord.Location = new System.Drawing.Point(312, 544);
			this.btnAddRecord.Name = "btnAddRecord";
			this.btnAddRecord.Size = new System.Drawing.Size(160, 42);
			this.btnAddRecord.TabIndex = 19;
			this.btnAddRecord.Text = "Add Rec";
			this.btnAddRecord.UseVisualStyleBackColor = false;
			this.btnAddRecord.Click += new System.EventHandler(this.btnAddRecord_Click);
			// 
			// btnDeleteRecord
			// 
			this.btnDeleteRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnDeleteRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDeleteRecord.Location = new System.Drawing.Point(487, 544);
			this.btnDeleteRecord.Name = "btnDeleteRecord";
			this.btnDeleteRecord.Size = new System.Drawing.Size(160, 42);
			this.btnDeleteRecord.TabIndex = 20;
			this.btnDeleteRecord.Text = "Del Rec";
			this.btnDeleteRecord.UseVisualStyleBackColor = false;
			this.btnDeleteRecord.Click += new System.EventHandler(this.btnDeleteRecord_Click);
			// 
			// btnChart2Cdata
			// 
			this.btnChart2Cdata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnChart2Cdata.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnChart2Cdata.Location = new System.Drawing.Point(16, 184);
			this.btnChart2Cdata.Name = "btnChart2Cdata";
			this.btnChart2Cdata.Size = new System.Drawing.Size(160, 41);
			this.btnChart2Cdata.TabIndex = 21;
			this.btnChart2Cdata.Text = "To Cdata";
			this.btnChart2Cdata.UseVisualStyleBackColor = false;
			this.btnChart2Cdata.Click += new System.EventHandler(this.btnChart2Cdata_Click);
			// 
			// btnClearNonRecs
			// 
			this.btnClearNonRecs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnClearNonRecs.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClearNonRecs.Location = new System.Drawing.Point(662, 544);
			this.btnClearNonRecs.Name = "btnClearNonRecs";
			this.btnClearNonRecs.Size = new System.Drawing.Size(160, 42);
			this.btnClearNonRecs.TabIndex = 22;
			this.btnClearNonRecs.Text = "Clear Non";
			this.btnClearNonRecs.UseVisualStyleBackColor = false;
			this.btnClearNonRecs.Click += new System.EventHandler(this.btnClearNonRecs_Click);
			// 
			// btnCdata2XML
			// 
			this.btnCdata2XML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnCdata2XML.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCdata2XML.Location = new System.Drawing.Point(16, 482);
			this.btnCdata2XML.Name = "btnCdata2XML";
			this.btnCdata2XML.Size = new System.Drawing.Size(160, 42);
			this.btnCdata2XML.TabIndex = 23;
			this.btnCdata2XML.Text = "Cdata 2 XML";
			this.btnCdata2XML.UseVisualStyleBackColor = false;
			this.btnCdata2XML.Click += new System.EventHandler(this.btnCdata2XML_Click);
			// 
			// btnClearTarget
			// 
			this.btnClearTarget.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnClearTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnClearTarget.Location = new System.Drawing.Point(312, 608);
			this.btnClearTarget.Name = "btnClearTarget";
			this.btnClearTarget.Size = new System.Drawing.Size(160, 42);
			this.btnClearTarget.TabIndex = 24;
			this.btnClearTarget.Text = "Clear Target";
			this.btnClearTarget.UseVisualStyleBackColor = false;
			this.btnClearTarget.Click += new System.EventHandler(this.btnClearTarget_Click);
			// 
			// btnTarget2Disk
			// 
			this.btnTarget2Disk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnTarget2Disk.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnTarget2Disk.Location = new System.Drawing.Point(487, 608);
			this.btnTarget2Disk.Name = "btnTarget2Disk";
			this.btnTarget2Disk.Size = new System.Drawing.Size(175, 42);
			this.btnTarget2Disk.TabIndex = 25;
			this.btnTarget2Disk.Text = "Target 2 Disk";
			this.btnTarget2Disk.UseVisualStyleBackColor = false;
			this.btnTarget2Disk.Click += new System.EventHandler(this.btnTarget2Disk_Click);
			// 
			// btnWriteXML
			// 
			this.btnWriteXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnWriteXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnWriteXML.Location = new System.Drawing.Point(679, 608);
			this.btnWriteXML.Name = "btnWriteXML";
			this.btnWriteXML.Size = new System.Drawing.Size(175, 42);
			this.btnWriteXML.TabIndex = 26;
			this.btnWriteXML.Text = "Write to XML";
			this.btnWriteXML.UseVisualStyleBackColor = false;
			this.btnWriteXML.Click += new System.EventHandler(this.btnWriteXML_Click);
			// 
			// btnSort
			// 
			this.btnSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSort.Location = new System.Drawing.Point(860, 608);
			this.btnSort.Name = "btnSort";
			this.btnSort.Size = new System.Drawing.Size(127, 42);
			this.btnSort.TabIndex = 27;
			this.btnSort.Text = "Sort";
			this.btnSort.UseVisualStyleBackColor = false;
			this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
			// 
			// btnDispSort
			// 
			this.btnDispSort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnDispSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDispSort.Location = new System.Drawing.Point(993, 608);
			this.btnDispSort.Name = "btnDispSort";
			this.btnDispSort.Size = new System.Drawing.Size(127, 42);
			this.btnDispSort.TabIndex = 28;
			this.btnDispSort.Text = "Disp Sort";
			this.btnDispSort.UseVisualStyleBackColor = false;
			this.btnDispSort.Click += new System.EventHandler(this.btnDispSort_Click);
			// 
			// TimerSchedule
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(1310, 688);
			this.Controls.Add(this.btnDispSort);
			this.Controls.Add(this.btnSort);
			this.Controls.Add(this.btnWriteXML);
			this.Controls.Add(this.btnTarget2Disk);
			this.Controls.Add(this.btnClearTarget);
			this.Controls.Add(this.btnCdata2XML);
			this.Controls.Add(this.btnClearNonRecs);
			this.Controls.Add(this.btnChart2Cdata);
			this.Controls.Add(this.btnDeleteRecord);
			this.Controls.Add(this.btnAddRecord);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnUpdateChart);
			this.Controls.Add(this.btnLoadXML);
			this.Controls.Add(this.CGridView);
			this.Controls.Add(this.btnSendRecs);
			this.Controls.Add(this.btnShow);
			this.Controls.Add(this.tbReceived);
			this.Controls.Add(this.lbClientType);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnRefresh);
			this.Name = "TimerSchedule";
			this.Text = "TimerSchedule";
			((System.ComponentModel.ISupportInitialize)(this.CGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ListBox lbClientType;
		private System.Windows.Forms.TextBox tbReceived;
		private System.Windows.Forms.Button btnShow;
		private System.Windows.Forms.Button btnSendRecs;
		private System.Windows.Forms.DataGridView CGridView;
		private System.Windows.Forms.Button btnLoadXML;
		private System.Windows.Forms.Button btnUpdateChart;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnAddRecord;
		private System.Windows.Forms.Button btnDeleteRecord;
		private System.Windows.Forms.Button btnChart2Cdata;
		private System.Windows.Forms.Button btnClearNonRecs;
		private System.Windows.Forms.Button btnCdata2XML;
		private System.Windows.Forms.Button btnClearTarget;
		private System.Windows.Forms.Button btnTarget2Disk;
		private System.Windows.Forms.Button btnWriteXML;
		private System.Windows.Forms.Button btnSort;
		private System.Windows.Forms.Button btnDispSort;
	}
}