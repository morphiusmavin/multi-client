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
	public partial class GarageForm : Form
	{
		private INetworkClient m_client;
		private bool m_wait = false;
		ServerCmds svrcmd = new ServerCmds();
		private List<int> CurrentList = new List<int>();
		public System.Collections.Generic.List<GPSlist> gps_list;
		private byte[] recv_buff;
		private bool m_pause = false;
		bool[] status = new bool[8];
		bool[] prev_status = new bool[8];
		List<String> on_label_list = new List<String>();
		List<String> off_label_list = new List<String>();
		public System.Collections.Generic.List<ButtonList> button_list;
		public GarageForm(string xml_file_location, INetworkClient client)
		{
			InitializeComponent();
			m_client = client;
			svrcmd.SetClient(m_client);
			
			XmlReader xmlfile = null;
			DataSet ds = new DataSet();
			var filePath = xml_file_location;
			xmlfile = XmlReader.Create(filePath, new XmlReaderSettings());
			ds.ReadXml(xmlfile);
			status[0] = false;
			status[1] = false;
			status[2] = false;
			status[3] = false;
			status[4] = false;
			status[5] = false;
			status[6] = false;
			status[7] = false;

			prev_status[0] = false;
			prev_status[1] = false;
			prev_status[2] = false;
			prev_status[3] = false;
			prev_status[4] = false;
			prev_status[5] = false;
			prev_status[6] = false;
			prev_status[7] = false;
			recv_buff = new byte[200];

			on_label_list.Add("ALL_NORTH_ON");
			on_label_list.Add("ALL_SOUTH_ON");
			on_label_list.Add("ALL_EAST_ON");
			on_label_list.Add("ALL_WEST_ON");
			on_label_list.Add("ALL_MIDDLE_ON");
			on_label_list.Add("ALL_OFFICE_ON");
			on_label_list.Add("ALL_LIGHTS_ON");
			on_label_list.Add("WORK_ON");

			off_label_list.Add("ALL_NORTH_OFF");
			off_label_list.Add("ALL_SOUTH_OFF");
			off_label_list.Add("ALL_EAST_OFF");
			off_label_list.Add("ALL_WEST_OFF");
			off_label_list.Add("ALL_MIDDLE_OFF");
			off_label_list.Add("ALL_OFFICE_OFF");
			off_label_list.Add("ALL_LIGHTS_OFF");
			off_label_list.Add("WORK_OFF");

			button_list = new List<ButtonList>();
			Control sCtl = this.btnNorth;
			//for (int i = 0; i < this.Controls.Count; i++)
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
			/*
			foreach (ButtonList btn in button_list)
			{
				AddMsg(btn.Name + " " + btn.TabOrder.ToString());
			}
			*/
		}
		public void Enable_Dlg(bool wait)
		{
			m_wait = wait;
			//if (wait)
				//tbAddMsg.Clear();
		}
		public void OnReceived(INetworkClient client, Packet receivedPacket)
		{
			Process_Msg(receivedPacket.PacketRaw);
			//AddMsg(receivedPacket.ToString());
		}
		delegate void AddMsg_Involk(string message);
		public void AddMsg(string message)
		{
			if (tbAddMsg.InvokeRequired)
			{
				AddMsg_Involk CI = new AddMsg_Involk(AddMsg);
				tbAddMsg.Invoke(CI, message);
			}
			else
			{
				//tbReceived.Text += message + "\r\n";
				tbAddMsg.AppendText(message + "\r\n");
			}
		}
		String StringFromByteArr(byte[] bytes)
		{
			char[] chars = new char[bytes.Length / sizeof(char)];
			System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}
		public void Process_Msg(byte[] bytes)
		{
			if (m_wait == true && m_pause == false)
			{
				int type_msg = (int)bytes[0];
				int i = 0;
				string msg = svrcmd.GetName(type_msg);
				if (msg == "UPDATE_ALL")
				{
					String res = "";
					AddMsg("UPDATE_ALL: len: " + (i = bytes.Length).ToString());
					i = bytes.Length;
					uint l = 0;
					uint m = 0;
					uint k = 0;
					uint n = 1;
					uint o = 0;
					String temp = "";
					for (int j = 2; j < i; j += 2,n++)
					{
						k = bytes[j];
						if (k > 0)
							k -= (uint)48;
						temp += k.ToString();
						l = uint.Parse(temp);
					}
					AddMsg(temp + " " + l.ToString());
					m = l;
					//m &= 63;
					int len = temp.Length * 4;
					len = len < 9 ? len : 8;
					for(k = 0;k < 8;k++)
					{
						//AddMsg(m.ToString());
						o = m & 1;
						if (o > 0)
						{
							res += '1';
							status[k] = true;
						}
						else
						{
							res += '0';
							status[k] = false;
						}
						m >>= 1;
					}
					IfStatusChanged(-1);
					//AddMsg(res);
					/*
					for(int ix = 0;ix < 6;ix++)
					{
						AddMsg(status[ix].ToString());
					}
					*/
					
				}
				if (msg == "UPDATE_STATUS")
				{
					//AddMsg(msg);
					int which = (int)bytes[2] - 48;
					int onoff = (int)bytes[6] - 48;
					AddMsg(which.ToString() + " " + onoff.ToString());
					if (which > -1 && which < 8)
					{
						status[which] = (onoff == 1 ? status[which] = true : status[which] = false);
						AddMsg(status[which].ToString() + " " + which.ToString() + " " + onoff.ToString());
					}
					IfStatusChanged(which);
				}
			}
		}
		// pass in -1 to tell this to check all of the status array
		// or pass in a 0->5 to set a single status item
		private void IfStatusChanged(int which)
		{
			AddMsg(which.ToString());
			if (which == -1)
				for (int i = 0; i < 8; i++)
				{
					//if (status[i] != prev_status[i])
					if(true)
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
				if(true)
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
			string cmd = onoff ? on_label_list[which] : off_label_list[which];
			//AddMsg(cmd);
			int offset = svrcmd.GetCmdIndexI(cmd);
			//AddMsg(offset.ToString());
			offset = svrcmd.GetCmdIndexI(cmd);
			//AddMsg(which.ToString() + " " + cmd + " " + offset.ToString());
			//svrcmd.Send_Cmd(offset);
			svrcmd.Send_ClCmd(offset, 9, "test");
			prev_status[which] = status[which];
			ChangeStatus(which);
			IfStatusChanged(which);
		}
		private void ChangeStatus(int i)
		{
			status[i] = !status[i];
			AddMsg("status changed: " + i.ToString());
			//IfStatusChanged();
		}
		// this is actually north
		private void btnNorth_Click(object sender, EventArgs e)
		{
			SendCmd(0, !status[0]);
		}

		private void btnSouth_Click(object sender, EventArgs e)
		{
			SendCmd(1, !status[1]);
		}

		private void btnEast_Click(object sender, EventArgs e)
		{
			SendCmd(2, !status[2]);
		}

		private void btnWest_Click(object sender, EventArgs e)
		{
			SendCmd(3, !status[3]);
		}

		private void btnMiddle_Click(object sender, EventArgs e)
		{
			SendCmd(4, !status[4]);
		}

		private void btnOffice_Click(object sender, EventArgs e)
		{
			SendCmd(5, !status[5]);
		}

		private void btnAll_Click(object sender, EventArgs e)
		{
			SendCmd(6, !status[6]);
			int i = 0;
			bool st = status[6];
			if (st)
			{
				for (i = 0; i < 8; i++)
				{
					status[i] = true;
					button_list[i].Ctl.Text = "ON";
					button_list[i].Ctl.BackColor = Color.Aqua;
				}
			}
			else
			{
				for (i = 0; i < 8; i++)
				{
					status[i] = false;
					button_list[i].Ctl.Text = "OFF";
					button_list[i].Ctl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
				}
			}
		}

		private void btnPollStatus_Click(object sender, EventArgs e)
		{
//			SendPollStatus();
		}

		private void btnClear_Click_1(object sender, EventArgs e)
		{
			SendCmd(7, !status[7]);
		}

		private void SendPollStatus()
		{
			string cmd = "POLL_STATUS";
			int offset = svrcmd.GetCmdIndexI(cmd);
			offset = svrcmd.GetCmdIndexI(cmd);
			//AddMsg(offset.ToString());
			svrcmd.Send_Cmd(offset);
		}
	}
}
