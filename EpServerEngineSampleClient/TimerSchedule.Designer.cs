
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
			this.lbPort = new System.Windows.Forms.ListBox();
			this.tbReceived = new System.Windows.Forms.TextBox();
			this.btnShow = new System.Windows.Forms.Button();
			this.btnSingle = new System.Windows.Forms.Button();
			this.CGridView = new System.Windows.Forms.DataGridView();
			this.btnLoadXML = new System.Windows.Forms.Button();
			this.btnUpdateChart = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnAddRecord = new System.Windows.Forms.Button();
			this.btnDeleteRecord = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.CGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// btnRefresh
			// 
			this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRefresh.Location = new System.Drawing.Point(12, 265);
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
			this.btnOK.Location = new System.Drawing.Point(12, 554);
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
			this.btnCancel.Location = new System.Drawing.Point(12, 506);
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
			// lbPort
			// 
			this.lbPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbPort.FormattingEnabled = true;
			this.lbPort.ItemHeight = 18;
			this.lbPort.Location = new System.Drawing.Point(190, 75);
			this.lbPort.Name = "lbPort";
			this.lbPort.Size = new System.Drawing.Size(161, 220);
			this.lbPort.TabIndex = 4;
			this.lbPort.SelectedIndexChanged += new System.EventHandler(this.lbPort_SelectedIndexChanged);
			// 
			// tbReceived
			// 
			this.tbReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbReceived.Location = new System.Drawing.Point(190, 321);
			this.tbReceived.Multiline = true;
			this.tbReceived.Name = "tbReceived";
			this.tbReceived.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbReceived.Size = new System.Drawing.Size(161, 179);
			this.tbReceived.TabIndex = 5;
			// 
			// btnShow
			// 
			this.btnShow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnShow.Location = new System.Drawing.Point(12, 364);
			this.btnShow.Name = "btnShow";
			this.btnShow.Size = new System.Drawing.Size(160, 42);
			this.btnShow.TabIndex = 11;
			this.btnShow.Text = "Show";
			this.btnShow.UseVisualStyleBackColor = false;
			this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
			// 
			// btnSingle
			// 
			this.btnSingle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnSingle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSingle.Location = new System.Drawing.Point(12, 316);
			this.btnSingle.Name = "btnSingle";
			this.btnSingle.Size = new System.Drawing.Size(160, 42);
			this.btnSingle.TabIndex = 12;
			this.btnSingle.Text = "Single Rec";
			this.btnSingle.UseVisualStyleBackColor = false;
			this.btnSingle.Click += new System.EventHandler(this.btnSingle_Click);
			// 
			// CGridView
			// 
			this.CGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.CGridView.Location = new System.Drawing.Point(368, 43);
			this.CGridView.Name = "CGridView";
			this.CGridView.Size = new System.Drawing.Size(1052, 553);
			this.CGridView.TabIndex = 13;
			// 
			// btnLoadXML
			// 
			this.btnLoadXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnLoadXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLoadXML.Location = new System.Drawing.Point(12, 412);
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
			this.btnUpdateChart.Location = new System.Drawing.Point(12, 213);
			this.btnUpdateChart.Name = "btnUpdateChart";
			this.btnUpdateChart.Size = new System.Drawing.Size(160, 41);
			this.btnUpdateChart.TabIndex = 15;
			this.btnUpdateChart.Text = "Update";
			this.btnUpdateChart.UseVisualStyleBackColor = false;
			this.btnUpdateChart.Click += new System.EventHandler(this.btnUpdateChart_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnHelp.Location = new System.Drawing.Point(12, 459);
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
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(187, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 20);
			this.label2.TabIndex = 18;
			this.label2.Text = "Port";
			// 
			// btnAddRecord
			// 
			this.btnAddRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.btnAddRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAddRecord.Location = new System.Drawing.Point(191, 506);
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
			this.btnDeleteRecord.Location = new System.Drawing.Point(190, 554);
			this.btnDeleteRecord.Name = "btnDeleteRecord";
			this.btnDeleteRecord.Size = new System.Drawing.Size(160, 42);
			this.btnDeleteRecord.TabIndex = 20;
			this.btnDeleteRecord.Text = "Del Rec";
			this.btnDeleteRecord.UseVisualStyleBackColor = false;
			this.btnDeleteRecord.Click += new System.EventHandler(this.btnDeleteRecord_Click);
			// 
			// TimerSchedule
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(1439, 620);
			this.Controls.Add(this.btnDeleteRecord);
			this.Controls.Add(this.btnAddRecord);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnUpdateChart);
			this.Controls.Add(this.btnLoadXML);
			this.Controls.Add(this.CGridView);
			this.Controls.Add(this.btnSingle);
			this.Controls.Add(this.btnShow);
			this.Controls.Add(this.tbReceived);
			this.Controls.Add(this.lbPort);
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
		private System.Windows.Forms.ListBox lbPort;
		private System.Windows.Forms.TextBox tbReceived;
		private System.Windows.Forms.Button btnShow;
		private System.Windows.Forms.Button btnSingle;
		private System.Windows.Forms.DataGridView CGridView;
		private System.Windows.Forms.Button btnLoadXML;
		private System.Windows.Forms.Button btnUpdateChart;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnAddRecord;
		private System.Windows.Forms.Button btnDeleteRecord;
	}
}