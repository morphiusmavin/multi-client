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
		bool allon = false;
		int single_select = 0;
		int timer_tick = 20;
		int pump_timer_tick = 20;
		int no_lights = 8;

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

			on_label_list.Add("DESK_LIGHT");
			on_label_list.Add("EAST_LIGHT");
			on_label_list.Add("NORTHWEST_LIGHT");
			on_label_list.Add("SOUTHEAST_LIGHT");
			on_label_list.Add("MIDDLE_LIGHT");
			on_label_list.Add("WEST_LIGHT");
			on_label_list.Add("NORTHEAST_LIGHT");
			on_label_list.Add("SOUTHWEST_LIGHT");
			on_label_list.Add("WATER_PUMP");
			on_label_list.Add("WATER_VALVE1");
			on_label_list.Add("WATER_VALVE2");
			on_label_list.Add("WATER_VALVE3");
			on_label_list.Add("WATER_HEATER");

			button_list = new List<ButtonList>();
			Control sCtl = this.btnDesk;
			//for (int i = 0; i < this.Controls.Count; i++)
			for (int i = 0; i < 13; i++)
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
			tbTimer.Text = "300";
		}
		public void Enable_Dlg(bool wait)
		{
			m_wait = wait;
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
							//status[k] = true;
						}
						else
						{
							res += '0';
							//status[k] = false;
						}
						m >>= 1;
					}
					//IfStatusChanged(-1);
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
//						status[which] = (onoff == 1 ? status[which] = true : status[which] = false);
//						AddMsg(status[which].ToString() + " " + which.ToString() + " " + onoff.ToString());
					}
					//IfStatusChanged(which);
				}
			}
		}
		// pass in -1 to tell this to check all of the status array
		// or pass in a 0->5 to set a single status item
		private void IfStatusChanged(int which)
		{
			AddMsg(which.ToString());
/*
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
*/
		}
		private bool SendCmd(int which)
		{
			string cmd = on_label_list[which];
			int offset = svrcmd.GetCmdIndexI(cmd);
			return svrcmd.Change_PortCmd(offset, 8);
		}
		private bool SendCmd(int which, bool onoff)
		{
			string cmd = on_label_list[which];
			int offset = svrcmd.GetCmdIndexI(cmd);
			return svrcmd.Change_PortCmd(offset, 8, onoff);		// this is bad
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
		private void btnClear_Click_1(object sender, EventArgs e)		// rotate thru all single lights
		{
			int prev;
			if (single_select > 0)
				prev = single_select - 1;
			else prev = 7;
			//AddMsg("on: " + single_select.ToString() + " off: " + prev.ToString());
			string cmd = on_label_list[single_select];
			int offset = svrcmd.GetCmdIndexI(cmd);
			svrcmd.Send_ClCmd(offset, 8, true);
			cmd = on_label_list[prev];
			offset = svrcmd.GetCmdIndexI(cmd);
			svrcmd.Send_ClCmd(offset, 8, false);
			if (++single_select > 7)
				single_select = 0;
		}
		private void btnClrScr_Click(object sender, EventArgs e)
		{
			tbAddMsg.Clear();
		}
		private void myTimerTick(object sender, EventArgs e)
		{
			int i,j;
			if (--timer_tick == 0)
			{
				for (j = 0; j < 8; j++)
					if (svrcmd.GetState(svrcmd.GetCmdIndexI(on_label_list[j])))
					{
						ToggleButton(j, SendCmd(j));
					}
				timer1.Enabled = false;
				//timer_tick = int.Parse(tbTimer.Text);
				btnAll.Text = "OFF";
				btnAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
				if(this.Visible)
					this.Close();
			}
			tbTimer.Text = timer_tick.ToString();
			//AddMsg(timer_tick.ToString());
		}
		public static byte GetByteFromInt(int i)
		{
			byte[] bytes = BitConverter.GetBytes(i);
			return bytes[0];
		}
		private void btnTimer_Click(object sender, EventArgs e)
		{
			//timer1.Enabled = true;
			//int seconds = int.Parse(tbTimer.Text);
			int seconds = timer_tick;
			byte[] data = new byte[4];		// data array must be 2x of what's sent
			uint x = (uint)seconds >> 8;
			//AddMsg(x.ToString());
			data[0] = (byte)x;
			x = (uint)seconds;
			//AddMsg(x.ToString());
			data[1] = (byte)x;
			int ret = svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("TURN_ALL_LIGHTS_OFF"), 8, data);
			if (this.Visible)
				this.Close();
		}
		private void btnDesk_Click(object sender, EventArgs e)
		{
			ToggleButton(0, SendCmd(0));
		}
		private void btnEast_Click(object sender, EventArgs e)
		{
			ToggleButton(1, SendCmd(1));
		}
		private void btnNWest_Click(object sender, EventArgs e)
		{
			ToggleButton(2, SendCmd(2));
		}
		private void btnSeast_Click(object sender, EventArgs e)
		{
			ToggleButton(3, SendCmd(3));
		}
		private void btnMiddle_Click(object sender, EventArgs e)
		{
			ToggleButton(4, SendCmd(4));
		}
		private void btnWest_Click(object sender, EventArgs e)
		{
			ToggleButton(5, SendCmd(5));
		}
		private void btnNeast_Click(object sender, EventArgs e)
		{
			ToggleButton(6, SendCmd(6));
		}
		private void btnSWest_Click(object sender, EventArgs e)
		{
			ToggleButton(7, SendCmd(7));
		}
		private void btnWaterHeater_Click(object sender, EventArgs e)
		{
			ToggleButton(12, SendCmd(8));
		}
		private void btnWaterValve1_Click(object sender, EventArgs e)
		{
			ToggleButton(9, SendCmd(9));
		}
		private void btnWaterValve2_Click(object sender, EventArgs e)
		{
			ToggleButton(10, SendCmd(10));
		}
		private void btnWaterValve3_Click(object sender, EventArgs e)
		{
			ToggleButton(11, SendCmd(11));
		}
		private void btnWaterPump_Click(object sender, EventArgs e)
		{
			ToggleButton(8, SendCmd(12));
			AddMsg("pump on");
			timer2.Enabled = true;
			pump_timer_tick = 30;
		}
		private void btnAll_Click_1(object sender, EventArgs e)
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
					//status[i] = true;
					button_list[i].Ctl.Text = "ON";
					button_list[i].Ctl.BackColor = Color.Aqua;
				}
				btnAll.BackColor = Color.Aqua;
				btnAll.Text = "ON";
			}
			else
			{
				for (i = 0; i < no_lights; i++)
				{
					//status[i] = false;
					button_list[i].Ctl.Text = "OFF";
					button_list[i].Ctl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
				}
				btnAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
				btnAll.Text = "OFF";
			}
		}
		private void LoadEvent(object sender, EventArgs e)
		{
			for (int i = 0; i < 8; i++)
			{
				ToggleButton(i, svrcmd.GetState(svrcmd.GetCmdIndexI(on_label_list[i])));
			}
			tbTimer.Text = "300";
			timer_tick = 300;
			timer1.Enabled = false;

		}
		private void tbTimerChanged(object sender, EventArgs e)
		{
			timer_tick = int.Parse(tbTimer.Text);
			if (timer_tick > 65535)
				timer_tick = 65535;

		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			//AddMsg(pump_timer_tick.ToString());
			if (pump_timer_tick-- == 0)
			{
				ToggleButton(8, SendCmd(12,false));
				timer2.Enabled = false;
			}
		}
	}
}
