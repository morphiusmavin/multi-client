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
		bool[] status = new bool[8];
		List<String> on_label_list = new List<String>();
		//List<String> off_label_list = new List<String>();
		public System.Collections.Generic.List<ButtonList> button_list;
		public Cabin(INetworkClient client)
		{
			InitializeComponent();
			m_client = client;
			svrcmd.SetClient(m_client);
			status[0] = false;
			status[1] = false;
			status[2] = false;
			status[3] = false;
			status[4] = false;
			status[5] = false;
			status[6] = false;
			status[7] = false;
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
		private void SendCmd(int which)
		{
			string cmd = on_label_list[which];
			//AddMsg(cmd);
			int offset = svrcmd.GetCmdIndexI(cmd);
			//AddMsg(offset.ToString());
			offset = svrcmd.GetCmdIndexI(cmd);
			//AddMsg(which.ToString() + " " + cmd + " " + offset.ToString());
			//svrcmd.Send_Cmd(offset);
			ChangeStatus(which);
			svrcmd.Send_ClCmd(offset, 2, status[which]);       // TODO: set this to whatever client (in this case server) is offset 8 in assign_client_table.c
			IfStatusChanged(which);
		}
		private void ChangeStatus(int i)
		{
			status[i] = !status[i];
			//AddMsg("status changed: " + i.ToString());
			//IfStatusChanged();
		}
		private void IfStatusChanged(int which)
		{
			//AddMsg(which.ToString());
			if (which == -1)
				for (int i = 0; i < 8; i++)
				{
					//if (status[i] != prev_status[i])
					if (true)
					{
						if (status[i])
						{
							button_list[i].Ctl.Text = "ON";
							button_list[i].Ctl.BackColor = Color.Aqua;
						}
						else
						{
							button_list[i].Ctl.Text = "OFF";
							button_list[i].Ctl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
						}
					}
				}
			else if (which > -1 && which < 8)
			{
				//if (status[which] != prev_status[which])
				if (true)
				{
					if (status[which])
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
			}
			//AddMsg("done");
		}
		private void SendCmd(int which, bool onoff)
		{
			string cmd = on_label_list[which];
			//AddMsg(cmd);
			int offset = svrcmd.GetCmdIndexI(cmd);
			//AddMsg(offset.ToString());
			offset = svrcmd.GetCmdIndexI(cmd);
			//AddMsg(which.ToString() + " " + cmd + " " + offset.ToString());
			//svrcmd.Send_Cmd(offset);
			status[which] = onoff;
			svrcmd.Send_ClCmd(offset, 2, onoff);       // TODO: set this to whatever client (in this case server) is offset 8 in assign_client_table.c
		}
		private void btn1_Click(object sender, EventArgs e)
		{
			SendCmd(0);
		}

		private void btn2_Click(object sender, EventArgs e)
		{
			SendCmd(1);
		}

		private void btn3_Click(object sender, EventArgs e)
		{
			SendCmd(2);
		}

		private void btn4_Click(object sender, EventArgs e)
		{
			SendCmd(3);
		}

		private void btn5_Click(object sender, EventArgs e)
		{
			SendCmd(4);
		}

		private void btn6_Click(object sender, EventArgs e)
		{
			SendCmd(5);
		}

		private void btn7_Click(object sender, EventArgs e)
		{
			SendCmd(6);
		}

		private void btn8_Click(object sender, EventArgs e)
		{
			SendCmd(7);
		}
	}
}
