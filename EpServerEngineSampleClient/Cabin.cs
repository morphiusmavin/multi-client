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
		private bool m_wait = false;
		ServerCmds svrcmd = new ServerCmds();
		List<String> on_label_list = new List<String>();
		//List<String> off_label_list = new List<String>();
		public System.Collections.Generic.List<ButtonList> button_list;
		public Cabin(INetworkClient client)
		{
			InitializeComponent();
			m_client = client;
			svrcmd.SetClient(m_client);
		
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
			return svrcmd.Change_PortCmd(offset, 2, onoff);
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

		private void btnAllOn_Click(object sender, EventArgs e)
		{
			for(int i = 0;i < 8;i++)
			{
				ToggleButton(i, SendCmd(i));
			}
		}

		private void btnAllOff_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < 8; i++)
			{
				ToggleButton(i, SendCmd(i));
			}
		}

		private void LoadEvent(object sender, EventArgs e)
		{
			for (int i = 0; i < 8; i++)
			{
				ToggleButton(i, svrcmd.GetState(svrcmd.GetCmdIndexI(on_label_list[i])));
			}
		}
	}
}
