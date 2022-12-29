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
	public partial class EasyButtonForm : Form
	{
		List<String> garage_list;
		List<String> cabin_list;
		List<String> testbench_list;
		List<String> outdoor_list;
		//List<int> ports;
		List<String> ports1;
		List<String> ports2;
		List<String> ports3;
		int func, type, port;
		ServerCmds svrcmd;

		public EasyButtonForm()
		{
			InitializeComponent();
			garage_list = new List<String>();
			cabin_list = new List<String>();
			testbench_list = new List<String>();
			outdoor_list = new List<String>();

			svrcmd = new ServerCmds();

			garage_list.Add("DESK_LIGHT");
			garage_list.Add("EAST_LIGHT");
			garage_list.Add("NORTHWEST_LIGHT");
			garage_list.Add("SOUTHEAST_LIGHT");
			garage_list.Add("MIDDLE_LIGHT");
			garage_list.Add("WEST_LIGHT");
			garage_list.Add("NORTHEAST_LIGHT");
			garage_list.Add("SOUTHWEST_LIGHT");

			cabin_list.Add("BENCH_24V_1");
			cabin_list.Add("BENCH_24V_2");
			cabin_list.Add("BENCH_12V_1");
			cabin_list.Add("BENCH_12V_2");
			cabin_list.Add("BENCH_5V_1");
			cabin_list.Add("BENCH_5V_2");
			cabin_list.Add("BENCH_3V3_1");
			cabin_list.Add("BENCH_3V3_2");
			cabin_list.Add("BENCH_LIGHT1");
			cabin_list.Add("BENCH_LIGHT2");

			testbench_list.Add("CABIN1");
			testbench_list.Add("CABIN2");
			testbench_list.Add("CABIN3");
			testbench_list.Add("CABIN4");
			testbench_list.Add("CABIN5");
			testbench_list.Add("CABIN6");
			testbench_list.Add("CABIN7");
			testbench_list.Add("CABIN8");
			
			outdoor_list.Add("COOP1_LIGHT");
			outdoor_list.Add("COOP1_HEATER");
			outdoor_list.Add("COOP2_LIGHT");
			outdoor_list.Add("COOP2_HEATER");
			outdoor_list.Add("OUTDOOR_LIGHT1");
			outdoor_list.Add("OUTDOOR_LIGHT2");

			func = 0;
			type = 0;
			port = 0;
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
		private void add_outdoor_list()
		{
			lbPort.Items.Add("COOP1_LIGHT");
			lbPort.Items.Add("COOP1_HEATER");
			lbPort.Items.Add("COOP2_LIGHT");
			lbPort.Items.Add("COOP2_HEATER");
			lbPort.Items.Add("OUTDOOR_LIGHT1");
			lbPort.Items.Add("OUTDOOR_LIGHT2");
		}
		delegate void AddMsg_Involk(string message);
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
		public int getFunc()
		{
			return func;
		}
		public int getType()
		{
			return type;
		}
		public int getPort()
		{
			return port;
		}
		private void lbClientType_SelectedIndexChanged(object sender, EventArgs e)
		{
			int index = lbClientType.SelectedIndex;
			AddMsg(index.ToString());
			switch(index)
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
				case 3:
					add_outdoor_list();
					type = 3;   // testbench
					break;

				default:
					type = 0;
					break;
			}
		}

		private void lbPort_SelectedIndexChanged(object sender, EventArgs e)
		{
			port = lbPort.SelectedIndex;
			//AddMsg(port.ToString());
			switch(type)
			{
				case 8:
					AddMsg(garage_list[port]);
					port = svrcmd.GetCmdIndexI(garage_list[port]);
					break;
				case 3:
					AddMsg(cabin_list[port]);
					port = svrcmd.GetCmdIndexI(cabin_list[port]);
					break;
				case 2:
					AddMsg(testbench_list[port]);
					port = svrcmd.GetCmdIndexI(testbench_list[port]);
					break;
				case 4:
					port = svrcmd.GetCmdIndexI(outdoor_list[port]);
					break;
				default:
					AddMsg("what?");
					break;
			}
			AddMsg(port.ToString());
		}

		private void btnAssign_Click(object sender, EventArgs e)
		{
			AddMsg("func: " + func.ToString());
			AddMsg("type: " + type.ToString());
			AddMsg("port: " + port.ToString());
		}

		private void rbFunc1_CheckedChanged(object sender, EventArgs e)
		{
			func = 1;
		}

		private void rbFunc2_CheckedChanged(object sender, EventArgs e)
		{
			func = 2;
		}

		private void rbFunc3_CheckedChanged(object sender, EventArgs e)
		{
			func = 3;
		}

		private void rbFunc4_CheckChanged(object sender, EventArgs e)
		{
			func = 4;
		}

		private void rbFunc5_CheckChanged(object sender, EventArgs e)
		{
			func = 5;
		}

		private void btnQuit_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
            this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
