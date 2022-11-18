using System;
using System.Collections.Generic;
using System.Data;
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
using System.Collections;
using System.Xml.Serialization;
using System.Drawing;
using System.Timers;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace EpServerEngineSampleClient
{
	public partial class TimerSchedule : Form
	{
		List<String> garage_list;
		List<String> cabin_list;
		List<String> testbench_list;
		List<cdata> cdata;
		int type, port;
		ServerCmds svrcmd;
		INetworkClient m_client = null;
		private int iResult = 0;
		public int GetResult()
		{
			return iResult;
		}
		public TimerSchedule(INetworkClient client)
		{
			InitializeComponent();
			m_client = client;
			garage_list = new List<String>();
			cabin_list = new List<String>();
			testbench_list = new List<String>();

			svrcmd = new ServerCmds();
			svrcmd.SetClient(m_client);
			cdata = new List<cdata>();

			garage_list.Add("DESK_LIGHT");
			garage_list.Add("EAST_LIGHT");
			garage_list.Add("NORTHWEST_LIGHT");
			garage_list.Add("SOUTHEAST_LIGHT");
			garage_list.Add("MIDDLE_LIGHT");
			garage_list.Add("WEST_LIGHT");
			garage_list.Add("NORTHEAST_LIGHT");
			garage_list.Add("SOUTHWEST_LIGHT");

			testbench_list.Add("BENCH_24V_1");
			testbench_list.Add("BENCH_24V_2");
			testbench_list.Add("BENCH_12V_1");
			testbench_list.Add("BENCH_12V_2");
			testbench_list.Add("BENCH_5V_1");
			testbench_list.Add("BENCH_5V_2");
			testbench_list.Add("BENCH_3V3_1");
			testbench_list.Add("BENCH_3V3_2");
			testbench_list.Add("BENCH_LIGHT1");
			testbench_list.Add("BENCH_LIGHT2");

			cabin_list.Add("CABIN1");
			cabin_list.Add("CABIN2");
			cabin_list.Add("CABIN3");
			cabin_list.Add("CABIN4");
			cabin_list.Add("CABIN5");
			cabin_list.Add("CABIN6");
			cabin_list.Add("CABIN7");
			cabin_list.Add("CABIN8");
			for(int i = 0;i < 8;i++)
				AddMsg(svrcmd.GetCmdIndexI(cabin_list[i]).ToString());
			btnRefresh.Enabled = false;
			btnShow.Enabled = false;
			port = -1;
			type = -1;
		}
		private void add_garage_list()
		{
			lbPort.Items.Clear();
			lbPort.Items.Add("DESK_LIGHT");
			lbPort.Items.Add("EAST_LIGHT");
			lbPort.Items.Add("NORTHWEST_LIGHT");
			lbPort.Items.Add("SOUTHEAST_LIGHT");
			lbPort.Items.Add("MIDDLE_LIGHT");
			lbPort.Items.Add("WEST_LIGHT");
			lbPort.Items.Add("NORTHEAST_LIGHT");
			lbPort.Items.Add("SOUTHWEST_LIGHT");
		}
		private void add_testbench_list()
		{
			lbPort.Items.Clear();
			lbPort.Items.Add("BENCH_24V_1");
			lbPort.Items.Add("BENCH_24V_2");
			lbPort.Items.Add("BENCH_12V_1");
			lbPort.Items.Add("BENCH_12V_2");
			lbPort.Items.Add("BENCH_5V_1");
			lbPort.Items.Add("BENCH_5V_2");
			lbPort.Items.Add("BENCH_3V3_1");
			lbPort.Items.Add("BENCH_3V3_2");
			lbPort.Items.Add("BENCH_LIGHT1");
			lbPort.Items.Add("BENCH_LIGHT2");
		}
		private void add_cabin_list()
		{
			lbPort.Items.Clear();
			lbPort.Items.Add("CABIN1");
			lbPort.Items.Add("CABIN2");
			lbPort.Items.Add("CABIN3");
			lbPort.Items.Add("CABIN4");
			lbPort.Items.Add("CABIN5");
			lbPort.Items.Add("CABIN6");
			lbPort.Items.Add("CABIN7");
			lbPort.Items.Add("CABIN8");
		}
		delegate void AddMsg_Involk(string message);
		public void OnReceived(INetworkClient client, Packet receivedPacket)
		{
			Process_Msg(receivedPacket.PacketRaw);
			//AddMsg("size: " + (packetSize = receivedPacket.PacketByteSize).ToString());
			//AddMsg(receivedPacket.ToString());
		}
		public void Process_Msg(byte[] bytes)
		{
			int i;
			string ret = null;
			char[] chars = new char[bytes.Length + 2];
			char[] chars2 = new char[bytes.Length];
			System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			int type_msg = chars[0];
			System.Buffer.BlockCopy(bytes, 2, chars2, 0, bytes.Length - 2);
			ret = new string(chars2);
			string str = svrcmd.GetName(type_msg);
			switch (str)
			{
				case "NO_CLLIST_REC":
					break;
				case "REPLY_CLLIST":
					string[] words = ret.Split(' ');
					i = 0;
					int j;
				cdata cdata_temp = new cdata();
				foreach (var word in words)
				{
					switch (i)
					{
						case 0:
							j = int.Parse(word);
							cdata_temp.index = j;
							break;
						case 1:
							j = int.Parse(word);
							cdata_temp.port = j;
							break;
						case 2:
							j = int.Parse(word);
							cdata_temp.on_hour = j;
							break;
						case 3:
							j = int.Parse(word);
							cdata_temp.on_minute = j;
							break;
						case 4:
							j = int.Parse(word);
							cdata_temp.on_second = j;
							break;
						case 5:
							j = int.Parse(word);
							cdata_temp.off_hour = j;
							break;
						case 6:
							j = int.Parse(word);
							cdata_temp.off_minute = j;
							break;
						case 7:
							j = int.Parse(word);
							cdata_temp.off_second = j;
							break;
						case 8:
							cdata_temp.label = word;
							//AddMsg(word);
							//AddMsg("");
							break;
						default:
							AddMsg("?");
							break;
					}
					i++;
				}
				cdata.Add(cdata_temp);
				cdata_temp = null;
				break;

				default:
					break;

			}
			AddMsg(cdata.Count().ToString());
		}
		// pass in -1 to tell this to check all of the status array
		// or pass in a 0->5 to set a single status item
		private void lbClientType_SelectedIndexChanged(object sender, EventArgs e)
		{
			int index = lbClientType.SelectedIndex;
			AddMsg(index.ToString());
			switch (index)
			{
				case 0:
					add_garage_list();
					type = 8;   // server
					break;

				case 1:
					add_cabin_list();
					type = 2;   // cabin
					break;

				case 2:
					add_testbench_list();
					type = 3;   // testbench
					break;

				default:
					type = -1;
					break;
			}
			btnRefresh.Enabled = false;
			btnShow.Enabled = false;
			port = -1;
			AddMsg("type: " + type.ToString());
		}
		private void lbPort_SelectedIndexChanged(object sender, EventArgs e)
		{
			port = lbPort.SelectedIndex;
			//AddMsg(port.ToString());
			switch (type)
			{
				case 8:
					//AddMsg(garage_list[port]);
					port = svrcmd.GetCmdIndexI(garage_list[port]);
					break;
				case 3:
					//AddMsg(cabin_list[port]);
					port = svrcmd.GetCmdIndexI(cabin_list[port]);
					port -= 25;
					//AddMsg("port: " + port.ToString());
					break;
				case 2:
					//AddMsg(testbench_list[port]);
					port = svrcmd.GetCmdIndexI(testbench_list[port]);
					port -= 9;
					break;
				default:
					AddMsg("what?");
					break;
			}
			//AddMsg(port.ToString());
			btnRefresh.Enabled = true;
			btnShow.Enabled = true;
		}
		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
		private void btnShow_Click(object sender, EventArgs e)
		{
			tbReceived.Clear();
			foreach(cdata cd in cdata)
			{
				AddMsg(cd.label);
				AddMsg("");

				AddMsg(cd.index.ToString() + " " + cd.port.ToString() + " " + cd.state.ToString() + 
					" " + cd.on_hour.ToString() + 
					" " + cd.on_minute.ToString() + " " + cd.on_second.ToString() +
					" " + cd.off_hour.ToString() + " " + cd.off_minute.ToString() + " " + cd.off_second.ToString());
			}
			cdata.Clear();
		}
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("GET_ALL_CLLIST"), type, "");
		}
		private void btnSingle_Click(object sender, EventArgs e)
		{
			if (port > -1 && type > -1)
				svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("GET_CLLIST"), type, port);
		}
		public void AddMsg(string message)
		{
			if (tbReceived.InvokeRequired)
			{
				AddMsg_Involk CI = new AddMsg_Involk(AddMsg);
				tbReceived.Invoke(CI, message);
			}
			else
			{
				//tbReceived.Text += message + "\r\n";
				tbReceived.AppendText(message + "\r\n");
			}
		}
	}
}
