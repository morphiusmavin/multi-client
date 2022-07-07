namespace EpServerEngineSampleClient
{
	partial class PlayerDlg
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
			this.components = new System.ComponentModel.Container();
			this.lbPlayList = new System.Windows.Forms.ListBox();
			this.btnPlay = new System.Windows.Forms.Button();
			this.tbAddMsg = new System.Windows.Forms.TextBox();
			this.btn_Prev = new System.Windows.Forms.Button();
			this.btn_Next = new System.Windows.Forms.Button();
			this.btnLoop = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnPlayAll = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.tbTimeOut = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chbRandom = new System.Windows.Forms.CheckBox();
			this.tbTimeLeft = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lbPlayList
			// 
			this.lbPlayList.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbPlayList.FormattingEnabled = true;
			this.lbPlayList.ItemHeight = 29;
			this.lbPlayList.Location = new System.Drawing.Point(27, 12);
			this.lbPlayList.Name = "lbPlayList";
			this.lbPlayList.Size = new System.Drawing.Size(782, 439);
			this.lbPlayList.TabIndex = 0;
			this.lbPlayList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// btnPlay
			// 
			this.btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPlay.Location = new System.Drawing.Point(147, 504);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(105, 38);
			this.btnPlay.TabIndex = 1;
			this.btnPlay.Text = "Play";
			this.btnPlay.UseVisualStyleBackColor = true;
			this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
			// 
			// tbAddMsg
			// 
			this.tbAddMsg.Enabled = false;
			this.tbAddMsg.Location = new System.Drawing.Point(-2, 434);
			this.tbAddMsg.Multiline = true;
			this.tbAddMsg.Name = "tbAddMsg";
			this.tbAddMsg.Size = new System.Drawing.Size(32, 17);
			this.tbAddMsg.TabIndex = 2;
			this.tbAddMsg.Visible = false;
			// 
			// btn_Prev
			// 
			this.btn_Prev.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Prev.Location = new System.Drawing.Point(267, 504);
			this.btn_Prev.Name = "btn_Prev";
			this.btn_Prev.Size = new System.Drawing.Size(105, 38);
			this.btn_Prev.TabIndex = 3;
			this.btn_Prev.Text = "Prev";
			this.btn_Prev.UseVisualStyleBackColor = true;
			this.btn_Prev.Click += new System.EventHandler(this.Prev_Click);
			// 
			// btn_Next
			// 
			this.btn_Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_Next.Location = new System.Drawing.Point(267, 460);
			this.btn_Next.Name = "btn_Next";
			this.btn_Next.Size = new System.Drawing.Size(105, 38);
			this.btn_Next.TabIndex = 4;
			this.btn_Next.Text = "Next";
			this.btn_Next.UseVisualStyleBackColor = true;
			this.btn_Next.Click += new System.EventHandler(this.Next_Click);
			// 
			// btnLoop
			// 
			this.btnLoop.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLoop.Location = new System.Drawing.Point(27, 504);
			this.btnLoop.Name = "btnLoop";
			this.btnLoop.Size = new System.Drawing.Size(105, 38);
			this.btnLoop.TabIndex = 5;
			this.btnLoop.Text = "Loop";
			this.btnLoop.UseVisualStyleBackColor = true;
			this.btnLoop.Click += new System.EventHandler(this.btnLoop_Click);
			// 
			// btnStop
			// 
			this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStop.Location = new System.Drawing.Point(147, 459);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(105, 38);
			this.btnStop.TabIndex = 6;
			this.btnStop.Text = "Stop";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnPlayAll
			// 
			this.btnPlayAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnPlayAll.Location = new System.Drawing.Point(27, 460);
			this.btnPlayAll.Name = "btnPlayAll";
			this.btnPlayAll.Size = new System.Drawing.Size(105, 38);
			this.btnPlayAll.TabIndex = 7;
			this.btnPlayAll.Text = "Play All";
			this.btnPlayAll.UseVisualStyleBackColor = true;
			this.btnPlayAll.Click += new System.EventHandler(this.btnPlayAll_Click);
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.myTimerTick);
			// 
			// tbTimeOut
			// 
			this.tbTimeOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
			this.tbTimeOut.Location = new System.Drawing.Point(709, 461);
			this.tbTimeOut.Name = "tbTimeOut";
			this.tbTimeOut.Size = new System.Drawing.Size(100, 35);
			this.tbTimeOut.TabIndex = 8;
			this.tbTimeOut.TextChanged += new System.EventHandler(this.timeoutChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(478, 464);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(232, 29);
			this.label1.TabIndex = 9;
			this.label1.Text = "Timeout (seconds)";
			// 
			// chbRandom
			// 
			this.chbRandom.AutoSize = true;
			this.chbRandom.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
			this.chbRandom.Location = new System.Drawing.Point(390, 506);
			this.chbRandom.Name = "chbRandom";
			this.chbRandom.Size = new System.Drawing.Size(129, 33);
			this.chbRandom.TabIndex = 10;
			this.chbRandom.Text = "Random";
			this.chbRandom.UseVisualStyleBackColor = true;
			this.chbRandom.CheckedChanged += new System.EventHandler(this.RandomChanged);
			// 
			// tbTimeLeft
			// 
			this.tbTimeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
			this.tbTimeLeft.Location = new System.Drawing.Point(709, 506);
			this.tbTimeLeft.Name = "tbTimeLeft";
			this.tbTimeLeft.Size = new System.Drawing.Size(100, 35);
			this.tbTimeLeft.TabIndex = 11;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
			this.label2.Location = new System.Drawing.Point(597, 509);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(106, 29);
			this.label2.TabIndex = 12;
			this.label2.Text = "time left";
			// 
			// PlayerDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.ClientSize = new System.Drawing.Size(821, 554);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbTimeLeft);
			this.Controls.Add(this.chbRandom);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbTimeOut);
			this.Controls.Add(this.btnPlayAll);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnLoop);
			this.Controls.Add(this.btn_Next);
			this.Controls.Add(this.btn_Prev);
			this.Controls.Add(this.tbAddMsg);
			this.Controls.Add(this.btnPlay);
			this.Controls.Add(this.lbPlayList);
			this.Name = "PlayerDlg";
			this.Text = "PlayerDlg";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lbPlayList;
		private System.Windows.Forms.Button btnPlay;
		private System.Windows.Forms.TextBox tbAddMsg;
        private System.Windows.Forms.Button btn_Prev;
        private System.Windows.Forms.Button btn_Next;
		private System.Windows.Forms.Button btnLoop;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnPlayAll;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.TextBox tbTimeOut;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chbRandom;
		private System.Windows.Forms.TextBox tbTimeLeft;
		private System.Windows.Forms.Label label2;
	}
}