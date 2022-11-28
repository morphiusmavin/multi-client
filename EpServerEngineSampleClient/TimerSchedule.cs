﻿using System;
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
		List<String> outdoor_list;
		private List<Cdata> mycdata;
		int type, port;
		ServerCmds svrcmd;
		INetworkClient m_client = null;
		private int iResult = 0;
		string m_xml_file_location;
		public Cdata m_cdata;	// inconsistent accessability: field type Cdata is less accessable than m_cdata
		
		public int GetResult()
		{
			return iResult;
		}
		public TimerSchedule(string xml_file_location, INetworkClient client)
		{
			InitializeComponent();
			m_xml_file_location = xml_file_location;
			m_client = client;
			garage_list = new List<String>();
			cabin_list = new List<String>();
			testbench_list = new List<String>();
			outdoor_list = new List<String>();

			svrcmd = new ServerCmds();
			svrcmd.SetClient(m_client);
			mycdata = new List<Cdata>();
			btnLoadXML_Click(new object(), new EventArgs());
			if (false)
			foreach(Cdata cd in mycdata)
			{
				AddMsg(cd.label + " " + cd.index.ToString() + " " + cd.port.ToString() + " " + cd.on_hour.ToString() + " " + cd.on_minute.ToString());
			}
			init_lists();
		}
		private void init_lists()
		{
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

			outdoor_list.Add("COOP1_LIGHT");
			outdoor_list.Add("COOP1_HEATER");
			outdoor_list.Add("COOP2_LIGHT");
			outdoor_list.Add("COOP2_HEATER");
			outdoor_list.Add("OUTDOOR_LIGHT1");
			outdoor_list.Add("OUTDOOR_LIGHT2");

			for (int i = 0; i < 8; i++)
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
		private void add_outdoor_list()
		{
			lbPort.Items.Clear();
			lbPort.Items.Add("COOP1_LIGHT");
			lbPort.Items.Add("COOP1_HEATER");
			lbPort.Items.Add("COOP2_LIGHT");
			lbPort.Items.Add("COOP2_HEATER");
			lbPort.Items.Add("OUTDOOR_LIGHT1");
			lbPort.Items.Add("OUTDOOR_LIGHT2");
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
				case "REPLY_CLLIST":		// this happens once for each record sent 
					string[] words = ret.Split(' ');
					i = 0;
					int j;
				Cdata cdata_temp = new Cdata();
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
				mycdata.Add(cdata_temp);
				AddMsg(mycdata.Count().ToString());
				cdata_temp = null;
				break;

				default:
					break;
			}
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

				case 3:
					add_outdoor_list();
					type = 4;
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
					port -= 25;	// TODO: 13 is 'BLANK'
					//AddMsg("port: " + port.ToString());
					break;
				case 2:
					//AddMsg(testbench_list[port]);
					port = svrcmd.GetCmdIndexI(testbench_list[port]);
					port -= 9;
					break;
				case 4:
					port = svrcmd.GetCmdIndexI(outdoor_list[port]);
					port -= 34;
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
			/*
			foreach(Cdata cd in cdata)
			{
				AddMsg(cd.label);
				AddMsg("");

				AddMsg(cd.index.ToString() + " " + cd.port.ToString() + " " + cd.state.ToString() + 
					" " + cd.on_hour.ToString() + 
					" " + cd.on_minute.ToString() + " " + cd.on_second.ToString() +
					" " + cd.off_hour.ToString() + " " + cd.off_minute.ToString() + " " + cd.off_second.ToString());
			}
			cdata.Clear();
			*/
		}
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			mycdata.Clear();
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("GET_ALL_CLLIST"), type, "");
		}
		private void btnSingle_Click(object sender, EventArgs e)
		{
			if (port > -1 && type > -1)
				svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("GET_CLLIST"), type, port);
		}
		private void btnLoadXML_Click(object sender, EventArgs e)
		{
			DataSet ds = new DataSet();
			XmlReader xmlfile = null;
			int port;
			int lbindex = 0;
			var filePath = m_xml_file_location;
			Cdata item = null;
			xmlfile = XmlReader.Create(filePath, new XmlReaderSettings());
			ds.ReadXml(xmlfile);
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				//string temp = "";
				port = Convert.ToInt16(dr.ItemArray[2]);
				if (port > -1)
				{
					item = new Cdata();

					item.label = dr.ItemArray[0].ToString();
					item.index = Convert.ToInt16(dr.ItemArray[1]);
					item.port = Convert.ToInt16(dr.ItemArray[2]);
					item.state = Convert.ToInt16(dr.ItemArray[3]);
					item.on_hour = Convert.ToInt16(dr.ItemArray[4]);
					item.on_minute = Convert.ToInt16(dr.ItemArray[5]);
					item.on_second = Convert.ToInt16(dr.ItemArray[6]);
					item.off_hour = Convert.ToInt16(dr.ItemArray[7]);
					item.off_minute = Convert.ToInt16(dr.ItemArray[8]);
					//item.off_second = Convert.ToInt16(dr.ItemArray[9]);
					mycdata.Add(item);
					item = null;
					lbindex++;
				}
			}
			CGridView.DataSource = ds.Tables[0];
		}
		private DataTable GetDataTable()
		{
			DataTable dt = new DataTable();
			//          Step 2: Create column name or heading by mentioning the datatype.
			DataColumn dc0 = new DataColumn("Index", typeof(string));
			DataColumn dc1 = new DataColumn("Port", typeof(string));
			DataColumn dc2 = new DataColumn("State", typeof(string));
			DataColumn dc3 = new DataColumn("On Hour", typeof(string));
			DataColumn dc4 = new DataColumn("On Minute", typeof(string));
			DataColumn dc5 = new DataColumn("On Second", typeof(string));
			DataColumn dc6 = new DataColumn("Off Hour", typeof(string));
			DataColumn dc7 = new DataColumn("Off Minute", typeof(string));
			DataColumn dc8 = new DataColumn("Off Second", typeof(string));
			DataColumn dc9 = new DataColumn("Label", typeof(string));

			//          Step 3: Adding these Columns to the DataTable,
			dt.Columns.Add(dc0);
			dt.Columns.Add(dc1);
			dt.Columns.Add(dc2);
			dt.Columns.Add(dc3);
			dt.Columns.Add(dc4);
			dt.Columns.Add(dc5);
			dt.Columns.Add(dc6);
			dt.Columns.Add(dc7);
			dt.Columns.Add(dc8);
			dt.Columns.Add(dc9);
			return dt;
		}
		private void btnUpdateChart_Click(object sender, EventArgs e)
		{
			DataTable dt = GetDataTable();
			foreach (Cdata td in mycdata)
			{
				dt.Rows.Add(td.index.ToString(), td.port.ToString(), td.state.ToString(), td.on_hour.ToString(),
					td.on_minute.ToString(), td.on_second.ToString(), td.off_hour.ToString(), td.off_minute.ToString(),
					td.off_second.ToString(), td.label);
			}
			//          Step 5: Binding the datatable to datagrid:
			CGridView.DataSource = dt;
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Must have one of the types selected (garage/cabin/testbench/outdoor) to enable buttons\nUpdate: update chart with current Cdata\nRefresh: get data from server/client in type listbox\nLoad XML: load data from C:\\Users\\Daniel\\dev\\cdata.xml");
		}

		private void btnAddRecord_Click(object sender, EventArgs e)
		{
			Cdata cdata = new Cdata();
			AddRecord addrec = new AddRecord(cdata);
			
			//addrec.SetClient(m_client);
			addrec.SetCdata(cdata);
			addrec.StartPosition = FormStartPosition.Manual;
			addrec.Location = new Point(100, 10);

			if (addrec.ShowDialog(this) == DialogResult.OK)
			{
				cdata = addrec.GetCdata();
				AddMsg(cdata.on_hour.ToString());
				AddMsg(cdata.on_minute.ToString());
			}
			else
			{
				//                this.txtResult.Text = "Cancelled";
			}
			addrec.Dispose();
			mycdata.Add(cdata);
		}

		private void btnDeleteRecord_Click(object sender, EventArgs e)
		{
			int delrecno = 0;
			DeleteRec delrec = new DeleteRec();
			delrec.StartPosition = FormStartPosition.Manual;
			delrec.Location = new Point(100, 10);

			if (delrec.ShowDialog(this) == DialogResult.OK)
			{
				delrecno = delrec.GetDelRecNo();
				AddMsg(delrecno.ToString());
			}
			else
			{
				//                this.txtResult.Text = "Cancelled";
			}
			delrec.Dispose();
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
