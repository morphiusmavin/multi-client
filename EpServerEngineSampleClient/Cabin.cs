using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using EpServerEngine.cs;
using System.Diagnostics;
using System.Globalization;
using EpLibrary.cs;
using System.IO;
using System.Xml.Serialization;
using System.Drawing;
using System.Timers;
using static EpServerEngineSampleClient.FrmSampleClient;

namespace EpServerEngineSampleClient
{
	public partial class Cabin : Form
	{
		private INetworkClient m_client;
		private int timer_tick = 145;
		ServerCmds svrcmd = new ServerCmds();
		List<String> on_label_list = new List<String>();
		//List<String> off_label_list = new List<String>();
		public System.Collections.Generic.List<ButtonList> button_list;
		public Cabin(INetworkClient client, bool primary_wincl)
		{
			InitializeComponent();
			m_client = client;
			svrcmd.SetClient(m_client);
			svrcmd.SetPrimaryWinCl(primary_wincl);
		
			on_label_list.Add("CABIN1");
			on_label_list.Add("CABIN2");
			on_label_list.Add("CABIN3");
			on_label_list.Add("CABIN4");
			on_label_list.Add("CABIN5");
			on_label_list.Add("CABIN6");
			on_label_list.Add("CABIN7");
			on_label_list.Add("CABIN8");

			Control sCtl = this.btn1;
			//for (int i = 0; i < this.Controls.Count; i++)
			button_list = new List<ButtonList>();
			for (int i = 0; i < 8; i++)
			{
				if (sCtl.GetType() == typeof(Button))
				{
					button_list.Add(new ButtonList()
					{
						TabOrder = sCtl.TabIndex,
						Ctl = (Button)sCtl,
						Enabled = sCtl.Enabled,
						Name = sCtl.Name
					});
					sCtl = GetNextControl(sCtl, true);
				}
			}
			tbStatus.Text = "300";
		}
		private void ToggleButton(int which, bool state)
		{
			if (state)
			{
				button_list[which].Ctl.Text = "ON";
				button_list[which].Ctl.BackColor = Color.Aqua;
			}
			else
			{
				button_list[which].Ctl.Text = "OFF";
				button_list[which].Ctl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			}
		}
		private bool SendCmd(int which)
		{
			string cmd = on_label_list[which];
			//AddMsg("cmd: " + cmd);
			int offset = svrcmd.GetCmdIndexI(cmd);
			//AddMsg("offset: " + offset.ToString());
			//AddMsg(svrcmd.GetState(offset).ToString());
			return svrcmd.Change_PortCmd(offset, 2);
		}
		private bool SendCmd(int which, bool onoff)
		{
			string cmd = on_label_list[which];
			int offset = svrcmd.GetCmdIndexI(cmd);
			return svrcmd.Change_PortCmd(offset, 2, onoff);		// this is bad
		}
		private void btn1_Click(object sender, EventArgs e)
		{
			ToggleButton(0, SendCmd(0));
		}
		private void btn2_Click(object sender, EventArgs e)
		{
			ToggleButton(1, SendCmd(1));
		}
		private void btn3_Click(object sender, EventArgs e)
		{
			ToggleButton(2, SendCmd(2));
		}
		private void btn4_Click(object sender, EventArgs e)
		{
			ToggleButton(3, SendCmd(3));
		}
		private void btn5_Click(object sender, EventArgs e)
		{
			ToggleButton(4, SendCmd(4));
		}
		private void btn6_Click(object sender, EventArgs e)
		{
			ToggleButton(5, SendCmd(5));
		}
		private void btn7_Click(object sender, EventArgs e)
		{
			ToggleButton(6, SendCmd(6));
		}
		private void btn8_Click(object sender, EventArgs e)
		{
			ToggleButton(7, SendCmd(7));
		}
		private void LoadEvent(object sender, EventArgs e)
		{
			for (int i = 0; i < 8; i++)
			{
				ToggleButton(i, svrcmd.GetState(svrcmd.GetCmdIndexI(on_label_list[i])));
			}
			tbStatus.Text = "300";
			timer_tick = 300;
			timer1.Enabled = false;
		}

		private void btnTimer_Click(object sender, EventArgs e)
		{
			int seconds = timer_tick;
			byte[] data = new byte[4];      // data array must be 2x of what's sent
			uint x = (uint)seconds >> 8;
			//AddMsg(x.ToString());
			data[0] = (byte)x;
			x = (uint)seconds;
			//AddMsg(x.ToString());
			data[1] = (byte)x;
			int ret = svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("TURN_ALL_LIGHTS_OFF"), 2, data);
			if (this.Visible)
				this.Close();
		}
		private void TimerTick(object sender, EventArgs e)
		{
			int j;
			if (--timer_tick == 0)
			{
				for (j = 0; j < 8; j++)
					if (svrcmd.GetState(svrcmd.GetCmdIndexI(on_label_list[j])))
					{
						tbStatus.Text = j.ToString();
						ToggleButton(j, SendCmd(j));
					}
				timer1.Enabled = false;
				//timer_tick = int.Parse(tbStatus.Text);
				if(this.Visible)
					this.Close();
			}
			tbStatus.Text = timer_tick.ToString();

		}
		private void tbStatusChanged(object sender, EventArgs e)
		{
			timer_tick = int.Parse(tbStatus.Text);
		}
	}
}
