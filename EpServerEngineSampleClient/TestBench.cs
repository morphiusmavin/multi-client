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
	public partial class TestBench : Form
	{
		private INetworkClient m_client;
		private bool m_wait = false;
		ServerCmds svrcmd = new ServerCmds();
		private List<int> CurrentList = new List<int>();
		private byte[] recv_buff;
		private bool m_pause = false;
		List<String> on_label_list = new List<String>();
		public System.Collections.Generic.List<ButtonList> button_list;
		public TestBench(string xml_file_location, INetworkClient client)
		{
			InitializeComponent();
			m_client = client;
			svrcmd.SetClient(m_client);
			
			XmlReader xmlfile = null;
			DataSet ds = new DataSet();
			var filePath = xml_file_location;
			xmlfile = XmlReader.Create(filePath, new XmlReaderSettings());
			ds.ReadXml(xmlfile);
			recv_buff = new byte[200];

			on_label_list.Add("BENCH_24V_1");
			on_label_list.Add("BENCH_24V_2");
			on_label_list.Add("BENCH_12V_1");
			on_label_list.Add("BENCH_12V_2");
			on_label_list.Add("BENCH_5V_1");
			on_label_list.Add("BENCH_5V_2");
			on_label_list.Add("BENCH_3V3_1");
			on_label_list.Add("BENCH_3V3_2");
			on_label_list.Add("BENCH_LIGHT1");
			on_label_list.Add("BENCH_LIGHT2");
			on_label_list.Add("BATTERY_HEATER");

			button_list = new List<ButtonList>();
			Control sCtl = this.btn24v1;
			//for (int i = 0; i < this.Controls.Count; i++)
			for (int i = 0; i < 11; i++)
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
		
			if(false)
			foreach (ButtonList btn in button_list)
			{
				AddMsg(btn.Name + " " + btn.TabOrder.ToString());
			}
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
						//status[which] = (onoff == 1 ? status[which] = true : status[which] = false);
						//AddMsg(status[which].ToString() + " " + which.ToString() + " " + onoff.ToString());
					}
					//IfStatusChanged(which);
				}
			}
		}
		// pass in -1 to tell this to check all of the status array
		// or pass in a 0->5 to set a single status item
		
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
			bool ret;
			string cmd = on_label_list[which];
			//AddMsg("cmd: " + cmd);
			int offset = svrcmd.GetCmdIndexI(cmd);
			//AddMsg("offset: " + offset.ToString());
			//AddMsg(svrcmd.GetState(offset).ToString());
			ret = svrcmd.Change_PortCmd(offset, 3);
			//AddMsg(ret.ToString());
			return ret;
		}
		private bool SendCmd(int which, bool onoff)
		{
			string cmd = on_label_list[which];
			//AddMsg("cmd: " + cmd);
			int offset = svrcmd.GetCmdIndexI(cmd);
			//AddMsg("offset: " + offset.ToString());
			//AddMsg(svrcmd.GetState(offset).ToString());
			return svrcmd.Change_PortCmd(offset, 2);
		}
		private void btn24v1_Click(object sender, EventArgs e)
		{
			ToggleButton(0, SendCmd(0));
		}
		private void btn24v2_Click(object sender, EventArgs e)
		{
			ToggleButton(1, SendCmd(1));
		}
		private void btn12v1_Click(object sender, EventArgs e)
		{
			ToggleButton(2, SendCmd(2));
		}
		private void btn12v2_Click(object sender, EventArgs e)
		{
			ToggleButton(3, SendCmd(3));
		}
		private void btn5v1_Click(object sender, EventArgs e)
		{
			ToggleButton(4, SendCmd(4));
		}
		private void btn5v2_Click(object sender, EventArgs e)
		{
			ToggleButton(5, SendCmd(5));
		}
		private void btn3v31_Click(object sender, EventArgs e)
		{
			ToggleButton(6, SendCmd(6));
		}
		private void btn3v32_Click(object sender, EventArgs e)
		{
			ToggleButton(7, SendCmd(7));
		}
		private void btnBenchLight1_Click(object sender, EventArgs e)
		{
			ToggleButton(8, SendCmd(8));
		}
		private void btnBenchLight2_Click(object sender, EventArgs e)
		{
			ToggleButton(9, SendCmd(9));
		}
		private void btnBatteryHeater_Click(object sender, EventArgs e)
		{
			ToggleButton(10, SendCmd(10));
		}
		private void LoadEvent(object sender, EventArgs e)
		{
			for (int i = 0; i < 16; i++)
			{
				ToggleButton(i, svrcmd.GetState(svrcmd.GetCmdIndexI(on_label_list[i])));
			}
		}
	}
}
