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
using System.Xml.Linq;

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
			//btnLoadXML_Click(new object(), new EventArgs());
			init_lists();
			
		}
		private void LoadEvent(object sender, EventArgs e)
		{
			init_lists();
			//AddMsg("loaded");
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
			testbench_list.Add("BATTERY_HEATER");

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

			//for (int i = 0; i < 8; i++)
				//AddMsg(svrcmd.GetCmdIndexI(cabin_list[i]).ToString());
			btnRefresh.Enabled = false;
			btnShow.Enabled = false;
			port = -1;
			type = -1;
		}
		private void add_garage_list()
		{
			/*
			lbPort.Items.Clear();
			lbPort.Items.Add("DESK_LIGHT");
			lbPort.Items.Add("EAST_LIGHT");
			lbPort.Items.Add("NORTHWEST_LIGHT");
			lbPort.Items.Add("SOUTHEAST_LIGHT");
			lbPort.Items.Add("MIDDLE_LIGHT");
			lbPort.Items.Add("WEST_LIGHT");
			lbPort.Items.Add("NORTHEAST_LIGHT");
			lbPort.Items.Add("SOUTHWEST_LIGHT");
			*/
		}
		private void add_testbench_list()
		{
			/*
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
			*/
		}
		private void add_cabin_list()
		{
			/*
			lbPort.Items.Clear();
			lbPort.Items.Add("CABIN1");
			lbPort.Items.Add("CABIN2");
			lbPort.Items.Add("CABIN3");
			lbPort.Items.Add("CABIN4");
			lbPort.Items.Add("CABIN5");
			lbPort.Items.Add("CABIN6");
			lbPort.Items.Add("CABIN7");
			lbPort.Items.Add("CABIN8");
			*/
		}
		private void add_outdoor_list()
		{
			/*
			lbPort.Items.Clear();
			lbPort.Items.Add("COOP1_LIGHT");
			lbPort.Items.Add("COOP1_HEATER");
			lbPort.Items.Add("COOP2_LIGHT");
			lbPort.Items.Add("COOP2_HEATER");
			lbPort.Items.Add("OUTDOOR_LIGHT1");
			lbPort.Items.Add("OUTDOOR_LIGHT2");
			*/
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
								cdata_temp.state = j;
								break;
								case 3:
								j = int.Parse(word);
								cdata_temp.on_hour = j;
								break;
							case 4:
								j = int.Parse(word);
								cdata_temp.on_minute = j;
								break;
							case 5:
								j = int.Parse(word);
								cdata_temp.on_second = j;
								break;
							case 6:
								j = int.Parse(word);
								cdata_temp.off_hour = j;
								break;
							case 7:
								j = int.Parse(word);
								cdata_temp.off_minute = j;
								break;
							case 8:
								j = int.Parse(word);
								cdata_temp.off_second = j;
								break;
							case 9:
								cdata_temp.label = word;
								//AddMsg(word);
								//AddMsg("");
								//cdata_temp.label += '\n';
								break;
							default:
								AddMsg("?");
								break;
						}
						i++;
					}
					mycdata.Add(cdata_temp);
					//AddMsg(mycdata.Count().ToString());
					cdata_temp = null;
					//btnUpdateChart_Click(new object(), new EventArgs());
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
			//AddMsg(index.ToString());
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
			btnRefresh.Enabled = true;
			btnShow.Enabled = true;
			port = -1;
			//AddMsg("type: " + type.ToString());
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
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SHOW_CLLIST"), type, "");
		}
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			mycdata.Clear();
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("GET_ALL_CLLIST"), type, "");
		}
		private void btnLoadXML_Click(object sender, EventArgs e)
		{
			DataSet ds = new DataSet();
			XmlReader xmlfile = null;
			int port;
			int lbindex = 0;
			string tfilename = ChooseXMLFileName();
			if (tfilename == "")
				tfilename = m_xml_file_location;

			Cdata item = null;
			mycdata.Clear();
			xmlfile = XmlReader.Create(tfilename, new XmlReaderSettings());
			ds.ReadXml(xmlfile);
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				//string temp = "";
				port = Convert.ToInt16(dr.ItemArray[2]);
				if (port > -1)
				{
					item = new Cdata();

					item.index = Convert.ToInt16(dr.ItemArray[0]);
					item.port = Convert.ToInt16(dr.ItemArray[1]);
					item.state = Convert.ToInt16(dr.ItemArray[2]);
					item.on_hour = Convert.ToInt16(dr.ItemArray[3]);
					item.on_minute = Convert.ToInt16(dr.ItemArray[4]);
					item.on_second = Convert.ToInt16(dr.ItemArray[5]);
					item.off_hour = Convert.ToInt16(dr.ItemArray[6]);
					item.off_minute = Convert.ToInt16(dr.ItemArray[7]);
					item.off_second = Convert.ToInt16(dr.ItemArray[8]);
					item.label = dr.ItemArray[9].ToString();
					//AddMsg(item.label);
					mycdata.Add(item);
					item = null;
					lbindex++;
				}
			}
			CGridView.DataSource = ds.Tables[0];
		}
		// puts cdata in chart
		private void btnUpdateChart_Click(object sender, EventArgs e)
		{
			DataTable dt = GetDataTable();
			foreach (Cdata td in mycdata)
			{
				dt.Rows.Add(td.index.ToString(), td.port.ToString(), td.state.ToString(), td.on_hour.ToString(),
					td.on_minute.ToString(), td.on_second.ToString(), td.off_hour.ToString(), td.off_minute.ToString(),
					td.off_second.ToString(), td.label);
				//td.label += '\n';
				//AddMsg(td.label);
			}
			CGridView.DataSource = dt;
		}
		private void btnHelp_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Must have one of the types selected (garage/cabin/testbench/outdoor) to enable buttons\nUpdate: update chart with current Cdata\nRefresh: get data from server/client in type listbox\nLoad XML: load data from C:\\Users\\Daniel\\ClientProgramData\\cdata.xml");
		}
		private void btnAddRecord_Click(object sender, EventArgs e)
		{
			int i;
			Cdata addcdata = null;
			Cdata temp = null;
			int norecs;

			AddRecord addrec = new AddRecord();
			addrec.SetCdata(mycdata[mycdata.Count() - 1]);
			if (addrec.ShowDialog(this) == DialogResult.OK)
			{
				addcdata = addrec.GetCdata();
				norecs = addrec.GetNoRecs();
				int count = mycdata.Count();
				for (i = 0; i < norecs; i++)
				{
					temp = new Cdata();
					temp.on_hour = addcdata.on_hour;
					temp.on_minute = addcdata.on_minute;
					temp.on_second = addcdata.on_second;
					temp.off_hour = addcdata.off_hour;
					temp.off_minute = addcdata.off_minute;
					temp.off_second = addcdata.off_second;
					temp.port = addcdata.port;
					temp.index = count + i;
					temp.label += "temp" + i.ToString();
					mycdata.Add(temp);
					temp = null;
				}
			}
			else
			{
				return;
			}
			addrec.Dispose();
		
			btnUpdateChart_Click(new object(), new EventArgs());
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
				AddMsg("rec to del: " + delrecno.ToString());
			}
			else
			{
				//                this.txtResult.Text = "Cancelled";
			}
			delrec.Dispose();
		}
		private void btnChart2Cdata_Click(object sender, EventArgs e)
		{
			Cdata tdata = null;
			int i = 0;
			string val = "";
			mycdata.Clear();
			foreach (DataGridViewRow dr in CGridView.Rows)
			{
				tdata = new Cdata();
				tdata.index = i;
				if (dr.Cells[1].Value != null)
				{
					val = (string)dr.Cells[1].Value;
					tdata.port = int.Parse(val);
					val = (string)dr.Cells[2].Value;
					tdata.state = int.Parse(val);
					val = (string)dr.Cells[3].Value;
					tdata.on_hour = int.Parse(val);
					val = (string)dr.Cells[4].Value;
					tdata.on_minute = int.Parse(val);
					val = (string)dr.Cells[5].Value;
					tdata.on_second = int.Parse(val);
					val = (string)dr.Cells[6].Value;
					tdata.off_hour = int.Parse(val);
					val = (string)dr.Cells[7].Value;
					tdata.off_minute = int.Parse(val);
					val = (string)dr.Cells[8].Value;
					tdata.off_second = int.Parse(val);
					tdata.label = (string)dr.Cells[9].Value;
					//AddMsg(tdata.label);
					mycdata.Add(tdata);
					tdata = null;
					i++;
				}
			}
		}
		private void SelectionChanged(object sender, EventArgs e)
		{
			//AddMsg("row: " + e.ToString());
		}
		private void btnClearNonRecs_Click(object sender, EventArgs e)
		{
			ClearNonRecords();
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
		private void btnSendRecs_Click(object sender, EventArgs e)
		{
			int i;
			//byte[] label2 = new byte[60];
			byte[] label;
			byte[] data = new byte[80];
			//tbReceived.Clear();
			Array.Clear(data, 0, data.Length);
			if (type > -1)		// TODO: check to make sure no times start or stop at the same time
			{
				foreach (Cdata cd in mycdata)
				{
					if (cd.port > -1)
					//if(true)
					{
						//Array.Clear(label2, 0, label2.Length);

						label = BytesFromString(cd.label);
						data[0] = GetByteFromInt(cd.index);
						data[1] = GetByteFromInt(cd.port);
						data[2] = GetByteFromInt(cd.state);
						data[3] = GetByteFromInt(cd.on_hour);
						data[4] = GetByteFromInt(cd.on_minute);
						data[5] = GetByteFromInt(cd.on_second);
						data[6] = GetByteFromInt(cd.off_hour);
						data[7] = GetByteFromInt(cd.off_minute);
						data[8] = GetByteFromInt(cd.off_second);
						/*
						AddMsg(label.Length.ToString());
						for (i = 0; i < label.Length; i++)
							AddMsg(label[i].ToString());
						*/
						System.Buffer.BlockCopy(label, 0, data, 10, label.Length);
						int ret = svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SET_CLLIST"), type, data);
						//AddMsg(ret.ToString());
					}
				}
			}
		}
		private void btnClearTarget_Click(object sender, EventArgs e)
		{
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("CLEAR_CLLIST"), type, "");
		}
		private void btnTarget2Disk_Click(object sender, EventArgs e)
		{
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SAVE_CLLIST"), type, "");
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
		private string ChooseXMLFileName()
		{
			OpenFileDialog openFileDialog2 = new OpenFileDialog
			{
				InitialDirectory = @"C:\Users\Daniel\ClientProgramData",
				Title = "Browse XML Files",

				CheckFileExists = true,
				CheckPathExists = true,

				DefaultExt = "xml",
				Filter = "xml files (*.xml)|*.XML",
				FilterIndex = 2,
				RestoreDirectory = true,

				ReadOnlyChecked = true,
				ShowReadOnly = true
			};

			if (openFileDialog2.ShowDialog() == DialogResult.OK)
			{
				//tbFileName.Text = openFileDialog2.FileName;
				return openFileDialog2.FileName;
			}
			else return "";

		}
		private string CreateXMLFileName()
		{
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Filter = "XML file|*.XML|xml file|*.xml";
			saveFileDialog1.Title = "Create an XML file";
			saveFileDialog1.InitialDirectory = @"C:\Users\Daniel\ClientProgramData";
			saveFileDialog1.ShowDialog();

			// If the file name is not an empty string open it for saving.
			if (saveFileDialog1.FileName != "")
			{
				return saveFileDialog1.FileName;
			}
			else return "";
		}
		private void ClearNonRecords()
		{
			Cdata[] temp_cdata = new Cdata[20];
			int count = mycdata.Count();
			int i;
			int k = 0;
			for (i = 0; i < count; i++)
			{
				if (mycdata[i].port != -1)
				{
					temp_cdata[i] = mycdata[i];
					k++;
				}
			}
			mycdata.Clear();
			for (int j = 0; j < k; j++)
			{
				mycdata.Add(temp_cdata[j]);
			}
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
		public static byte GetByteFromInt(int i)
		{
			byte[] bytes = BitConverter.GetBytes(i);
			return bytes[0];
		}
		private void btnWriteXML_Click(object sender, EventArgs e)
		{
			string tfilename;
			tfilename = CreateXMLFileName();
			if (tfilename == "")
				return;
			int count = mycdata.Count;
			String[] file = new string[20];
			int i = 0;
			if (mycdata.Count() == 0)
			{
				MessageBox.Show("no data in mycdata");
				return;
			}
			foreach (Cdata td in mycdata)
			{
				//if (td.port > -1)
				file[i] = td.index.ToString() + "," + td.port.ToString() + "," + td.state.ToString() + "," + td.on_hour.ToString() + "," +
					td.on_minute.ToString() + "," + td.on_second.ToString() + "," + td.off_hour.ToString() + "," + td.off_minute.ToString() + "," +
					td.off_second.ToString() + "," + td.label;
				i++;
			}
			string line = "0,0,0,0,0,0,0,temp";
			string line2;
			int index = count;
			for (int j = 0; j < 20 - count; j++)
			{
				line2 = index.ToString() + ',' + "-1" + ',' + line + index.ToString();
				file[index] = line2;
				index++;
			}
			String xml = "";
			i = 0;
			AddMsg("");
			XElement top = new XElement("Table",
			from items in file
			let fields = items.Split(',')
			select new XElement("C_DATA",
			new XElement("index", fields[0]),
			new XElement("port", fields[1]),
			new XElement("state", fields[2]),
			new XElement("on_hour", fields[3]),
			new XElement("on_minute", fields[4]),
			new XElement("on_second", fields[5]),
			new XElement("off_hour", fields[6]),
			new XElement("off_minute", fields[7]),
			new XElement("off_second", fields[8]),
			new XElement("label", fields[9])
			)
			);
			File.WriteAllText(tfilename, xml + top.ToString());
			MessageBox.Show("created: " + tfilename);
		}
		private void btnSort_Click(object sender, EventArgs e)
		{
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SORT_CLLIST"), type, "");
		}
		private void btnDispSort_Click(object sender, EventArgs e)
		{
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("DISPLAY_CLLIST_SORT"), type, "");
		}
		private void LoadForm(object sender, EventArgs e)
		{
			tbReceived.Clear();
		}
		byte[] BytesFromString(String str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			byte[] bytes2 = new byte[bytes.Count() / 2];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			for (int i = 0; i < bytes2.Count(); i++)
			{
				bytes2[i] = bytes[i * 2];
			}
			return bytes2;
		}
	}
}