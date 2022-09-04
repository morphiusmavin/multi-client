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
		//private List<int> CurrentList = new List<int>();
		private bool m_pause = false;
		bool[] button_status = new bool[8];
		bool allon = false;
		int single_select = 0;
		int timer_tick = 0;
		int no_lights = 8;
		bool[] status = new bool[8];

		List<String> on_label_list = new List<String>();
		//List<String> off_label_list = new List<String>();
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

			button_status[0] = true;
			button_status[1] = true;
			button_status[2] = true;
			button_status[3] = true;
			button_status[4] = true;
			button_status[5] = true;
			button_status[6] = true;
			button_status[7] = true;

			on_label_list.Add("EAST_LIGHT");
			on_label_list.Add("NORTHWEST_LIGHT");
			on_label_list.Add("SOUTHEAST_LIGHT");
			on_label_list.Add("MIDDLE_LIGHT");
			on_label_list.Add("WEST_LIGHT");
			on_label_list.Add("NORTHEAST_LIGHT");
			on_label_list.Add("SOUTHWEST_LIGHT");
			on_label_list.Add("asdf");

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
					//AddMsg(button_list[i].Name);
					sCtl = GetNextControl(sCtl, true);
				}
			}
		}
		public void Enable_Dlg(bool wait)
		{
			m_wait = wait;
		}
		public void SetStatus(bool[] _status)
		{
			status = _status;
/*
			for(int i = 0;i < 8;i++)
			{
				ToggleButton(i);
			}
*/
		}
		public bool[] GetStatus()
		{
			return status;
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
			svrcmd.Send_ClCmd(offset, 8, status[which]);       // TODO: set this to whatever client (in this case server) is offset 8 in assign_client_table.c
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
			svrcmd.Send_ClCmd(offset, 8, onoff);       // TODO: set this to whatever client (in this case server) is offset 8 in assign_client_table.c
		}
		private void ChangeStatus(int i)
		{
			status[i] = !status[i];
			//AddMsg("status changed: " + i.ToString());
			//IfStatusChanged();
		}
		// this is actually north
		private void ToggleButton(int which)
		{
			if (button_status[which])
			{
				button_list[which].Ctl.Text = "ON";
				button_list[which].Ctl.BackColor = Color.Aqua;
			}
			else
			{
				button_list[which].Ctl.Text = "OFF";
				button_list[which].Ctl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			}
			button_status[which] = !button_status[which];
		}
		private void btnNorth_Click(object sender, EventArgs e)
		{
			SendCmd(1);
			SendCmd(5);
			ToggleButton(0);
		}
		private void btnSouth_Click(object sender, EventArgs e)
		{
			SendCmd(2);
			SendCmd(6);
			ToggleButton(1);
		}
		private void btnEast_Click(object sender, EventArgs e)
		{
			SendCmd(0);
			SendCmd(2);
			SendCmd(5);
			ToggleButton(2);
		}
		private void btnWest_Click(object sender, EventArgs e)
		{
			SendCmd(1);
			SendCmd(4);
			SendCmd(6);
			ToggleButton(3);
		}
		private void btnMiddle_Click(object sender, EventArgs e)
		{
			SendCmd(4);
			SendCmd(3);
			SendCmd(0);
			ToggleButton(4);
		}
		private void btnOffice_Click(object sender, EventArgs e)
		{
			SendCmd(4);
			SendCmd(6);
			ToggleButton(5);
		}
		private void btnBench_Click(object sender, EventArgs e)
		{
			SendCmd(7);
			ToggleButton(6);
		}
		private void btnAll_Click(object sender, EventArgs e)
		{
			int i;
			allon = !allon;
			SendCmd(0, allon);
			SendCmd(1, allon);
			SendCmd(2, allon);
			SendCmd(3, allon);
			SendCmd(4, allon);
			SendCmd(5, allon);
			SendCmd(6, allon);
			SendCmd(7, allon);
			if (allon)
			{
				for (i = 0; i < no_lights; i++)
				{
					status[i] = true;
					button_list[i].Ctl.Text = "ON";
					button_list[i].Ctl.BackColor = Color.Aqua;
				}
			}
			else
			{
				for (i = 0; i < no_lights; i++)
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
		/*
			EAST_LIGHT,
			NORTHWEST_LIGHT,
			SOUTHEAST_LIGHT,
			MIDDLE_LIGHT,
			WEST_LIGHT,
			NORTHEAST_LIGHT,
			SOUTHWEST_LIGHT,
		*/
		private void btnClear_Click_1(object sender, EventArgs e)		// rotate thru all single lights
		{
			int prev;
			if (single_select > 0)
				prev = single_select - 1;
			else prev = 6;
			//AddMsg("on: " + single_select.ToString() + " off: " + prev.ToString());
			string cmd = on_label_list[single_select];
			int offset = svrcmd.GetCmdIndexI(cmd);
			offset = svrcmd.GetCmdIndexI(cmd);
			svrcmd.Send_ClCmd(offset, 8, true);
			cmd = on_label_list[prev];
			offset = svrcmd.GetCmdIndexI(cmd);
			svrcmd.Send_ClCmd(offset, 8, false);
			if (++single_select > 7)
				single_select = 0;
		}
		private void SendPollStatus()
		{
			string cmd = "POLL_STATUS";
			int offset = svrcmd.GetCmdIndexI(cmd);
			offset = svrcmd.GetCmdIndexI(cmd);
			//AddMsg(offset.ToString());
			svrcmd.Send_Cmd(offset);
		}
		private void btnClrScr_Click(object sender, EventArgs e)
		{
			tbAddMsg.Clear();
		}
		private void myTimerTick(object sender, EventArgs e)
		{
			int i;
			if (++timer_tick > 20)
			{
				for (i = 0; i < 7; i++)
				{
					status[i] = true;
				}
				SendCmd(0);
				SendCmd(1);
				SendCmd(2);
				SendCmd(3);
				SendCmd(4);
				SendCmd(5);
				SendCmd(6);
				timer1.Enabled = false;
				timer_tick = 0;
				for (i = 0; i < 7; i++)
				{
					status[i] = false;
					button_list[i].Ctl.Text = "OFF";
					button_list[i].Ctl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
				}
				for (i = 0; i < 7; i++)
				{
					status[i] = false;
				}
			}
			AddMsg(timer_tick.ToString());
		}
		private void btnTimer_Click(object sender, EventArgs e)
		{
			timer1.Enabled = true;
		}

		
	}
}
