
namespace EpServerEngineSampleClient
{
	partial class GraphParams
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
			this.tbAxisInterval = new System.Windows.Forms.TextBox();
			this.tbYValuesPerPoint = new System.Windows.Forms.TextBox();
			this.tbMarkerStep = new System.Windows.Forms.TextBox();
			this.tbNoRecs = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(61, 229);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(153, 229);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// tbAxisInterval
			// 
			this.tbAxisInterval.Location = new System.Drawing.Point(128, 18);
			this.tbAxisInterval.Name = "tbAxisInterval";
			this.tbAxisInterval.Size = new System.Drawing.Size(100, 20);
			this.tbAxisInterval.TabIndex = 2;
			this.tbAxisInterval.TextChanged += new System.EventHandler(this.tbAxisInterval_TextChanged);
			// 
			// tbYValuesPerPoint
			// 
			this.tbYValuesPerPoint.Location = new System.Drawing.Point(128, 61);
			this.tbYValuesPerPoint.Name = "tbYValuesPerPoint";
			this.tbYValuesPerPoint.Size = new System.Drawing.Size(100, 20);
			this.tbYValuesPerPoint.TabIndex = 3;
			this.tbYValuesPerPoint.TextChanged += new System.EventHandler(this.tbYValuesPerPoint_TextChanged);
			// 
			// tbMarkerStep
			// 
			this.tbMarkerStep.Location = new System.Drawing.Point(128, 104);
			this.tbMarkerStep.Name = "tbMarkerStep";
			this.tbMarkerStep.Size = new System.Drawing.Size(100, 20);
			this.tbMarkerStep.TabIndex = 4;
			this.tbMarkerStep.TextChanged += new System.EventHandler(this.tbMarkerStep_TextChanged);
			// 
			// tbNoRecs
			// 
			this.tbNoRecs.Location = new System.Drawing.Point(128, 147);
			this.tbNoRecs.Name = "tbNoRecs";
			this.tbNoRecs.Size = new System.Drawing.Size(100, 20);
			this.tbNoRecs.TabIndex = 5;
			this.tbNoRecs.TextChanged += new System.EventHandler(this.tbNoRecs_TextChanged);
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(128, 190);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(100, 20);
			this.textBox5.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Axis Interval";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(95, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Y Values Per Point";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(25, 107);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 13);
			this.label3.TabIndex = 9;
			this.label3.Text = "Marker Step";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(33, 151);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(52, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "No. Recs";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(55, 193);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(35, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "label5";
			// 
			// GraphParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(258, 282);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.tbNoRecs);
			this.Controls.Add(this.tbMarkerStep);
			this.Controls.Add(this.tbYValuesPerPoint);
			this.Controls.Add(this.tbAxisInterval);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Name = "GraphParams";
			this.Text = "GraphParams";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox tbAxisInterval;
		private System.Windows.Forms.TextBox tbYValuesPerPoint;
		private System.Windows.Forms.TextBox tbMarkerStep;
		private System.Windows.Forms.TextBox tbNoRecs;
		private System.Windows.Forms.TextBox textBox5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
	}
}