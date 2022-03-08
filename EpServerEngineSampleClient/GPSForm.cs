using System;
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
	public partial class GPSForm : Form
	{
		private INetworkClient m_client;
		private bool m_wait = false;
		ServerCmds svrcmd = new ServerCmds();
		//private List<int> CurrentList = new List<int>();
		public System.Collections.Generic.List<GPSlist> gps_list;
		
		private int north = 0;

		private byte[] recv_buff = new byte[4];
		private bool m_pause = false;
	
		public GPSForm(string xml_file_location, INetworkClient client)
		{
			InitializeComponent();
			m_client = client;
			svrcmd.SetClient(m_client);
		}
		public void OnReceived(INetworkClient client, Packet receivedPacket)
		{
			Process_Msg(receivedPacket.PacketRaw);
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
		public void Enable_Dlg(bool wait)
		{
			m_wait = wait;
			if (wait)
				tbAddMsg.Clear();
		}
		String StringFromByteArr(byte[] bytes)
		{
			char[] chars = new char[bytes.Length / sizeof(char)];
			System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}

		public void Process_Msg(byte[] bytes)
		{
			/*
			if (m_wait == true && m_pause == false)
			{
				int type_msg = (int)bytes[0];
				System.Buffer.BlockCopy(bytes, 2, recv_buff, 0, bytes.Length-2);
				string msg = svrcmd.GetName(type_msg);
				AddMsg(msg);
			
				switch (msg)
				{
					default:
						//AddMsg(msg);
						break;
				}
			}
			*/
		}
			
		public static byte[] ReadFile(string filePath)
		{
			byte[] buffer;
			FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
			try
			{
				int length = (int)fileStream.Length;  // get file length
				buffer = new byte[length];            // create buffer
				int count;                            // actual number of bytes read
				int sum = 0;                          // total number of bytes read

				// read until Read method returns 0 (end of the stream has been reached)
				while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
					sum += count;  // sum is a buffer offset for next reading
			}
			finally
			{
				fileStream.Close();
			}
			return buffer;
		}

		private void btnNorthLights_Click(object sender, EventArgs e)
		{
			//AddMsg("all North on");
			byte[] bnorth = BitConverter.GetBytes(north);
			byte[] north1 = new byte[bnorth.Count() + 2];
			System.Buffer.BlockCopy(bnorth, 0, north1, 2, bnorth.Count());
			north1[0] = svrcmd.GetCmdIndexB("ALL_NORTH_ON");
			Packet packet = new Packet(north1, 0, north1.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		private void btnSouthLights_Click(object sender, EventArgs e)
		{
			//AddMsg("all South on");
			byte[] bnorth = BitConverter.GetBytes(north);
			byte[] north1 = new byte[bnorth.Count() + 2];
			System.Buffer.BlockCopy(bnorth, 0, north1, 2, bnorth.Count());
			north1[0] = svrcmd.GetCmdIndexB("ALL_SOUTH_ON");
			Packet packet = new Packet(north1, 0, north1.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		private void btnMiddle_Click(object sender, EventArgs e)
		{
			AddMsg("all middle on");
			byte[] bnorth = BitConverter.GetBytes(north);
			byte[] north1 = new byte[bnorth.Count() + 2];
			System.Buffer.BlockCopy(bnorth, 0, north1, 2, bnorth.Count());
			north1[0] = svrcmd.GetCmdIndexB("ALL_MIDDLE_ON");
			Packet packet = new Packet(north1, 0, north1.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		private void btnEast_Click(object sender, EventArgs e)
		{
			//AddMsg("all East on");
			byte[] bnorth = BitConverter.GetBytes(north);
			byte[] north1 = new byte[bnorth.Count() + 2];
			System.Buffer.BlockCopy(bnorth, 0, north1, 2, bnorth.Count());
			north1[0] = svrcmd.GetCmdIndexB("ALL_EAST_ON");
			Packet packet = new Packet(north1, 0, north1.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		// West off
		private void button2_Click(object sender, EventArgs e)
		{
			//AddMsg("all West off");
			byte[] bnorth = BitConverter.GetBytes(north);
			byte[] north1 = new byte[bnorth.Count() + 2];
			System.Buffer.BlockCopy(bnorth, 0, north1, 2, bnorth.Count());
			north1[0] = svrcmd.GetCmdIndexB("ALL_WEST_OFF");
			Packet packet = new Packet(north1, 0, north1.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		// north off
		private void button6_Click(object sender, EventArgs e)
		{
			//AddMsg("north off");
			byte[] bnorth = BitConverter.GetBytes(north);
			byte[] north1 = new byte[bnorth.Count() + 2];
			System.Buffer.BlockCopy(bnorth, 0, north1, 2, bnorth.Count());
			north1[0] = svrcmd.GetCmdIndexB("ALL_NORTH_OFF");
			Packet packet = new Packet(north1, 0, north1.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		// south off
		private void button5_Click(object sender, EventArgs e)
		{
			//AddMsg("south off");
			byte[] bnorth = BitConverter.GetBytes(north);
			byte[] north1 = new byte[bnorth.Count() + 2];
			System.Buffer.BlockCopy(bnorth, 0, north1, 2, bnorth.Count());
			north1[0] = svrcmd.GetCmdIndexB("ALL_SOUTH_OFF");
			Packet packet = new Packet(north1, 0, north1.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		// middle off
		private void button4_Click(object sender, EventArgs e)
		{
			//AddMsg("middle off");
			byte[] bnorth = BitConverter.GetBytes(north);
			byte[] north1 = new byte[bnorth.Count() + 2];
			System.Buffer.BlockCopy(bnorth, 0, north1, 2, bnorth.Count());
			north1[0] = svrcmd.GetCmdIndexB("ALL_MIDDLE_OFF");
			Packet packet = new Packet(north1, 0, north1.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		// East off
		private void button3_Click(object sender, EventArgs e)
		{
			//AddMsg("East off");
			byte[] bnorth = BitConverter.GetBytes(north);
			byte[] north1 = new byte[bnorth.Count() + 2];
			System.Buffer.BlockCopy(bnorth, 0, north1, 2, bnorth.Count());
			north1[0] = svrcmd.GetCmdIndexB("ALL_EAST_OFF");
			Packet packet = new Packet(north1, 0, north1.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		// all off
		private void button1_Click(object sender, EventArgs e)
		{
			//AddMsg("All off");
			byte[] bnorth = BitConverter.GetBytes(north);
			byte[] north1 = new byte[bnorth.Count() + 2];
			System.Buffer.BlockCopy(bnorth, 0, north1, 2, bnorth.Count());
			north1[0] = svrcmd.GetCmdIndexB("ALL_LIGHTS_OFF");
			Packet packet = new Packet(north1, 0, north1.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		private void btnWest_Click(object sender, EventArgs e)
		{
			//AddMsg("All West on");
			byte[] bnorth = BitConverter.GetBytes(north);
			byte[] north1 = new byte[bnorth.Count() + 2];
			System.Buffer.BlockCopy(bnorth, 0, north1, 2, bnorth.Count());
			north1[0] = svrcmd.GetCmdIndexB("ALL_WEST_ON");
			Packet packet = new Packet(north1, 0, north1.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		private void btnAllLights_Click(object sender, EventArgs e)
		{
			//AddMsg("All on");
			byte[] bnorth = BitConverter.GetBytes(north);
			byte[] north1 = new byte[bnorth.Count() + 2];
			System.Buffer.BlockCopy(bnorth, 0, north1, 2, bnorth.Count());
			north1[0] = svrcmd.GetCmdIndexB("ALL_LIGHTS_ON");
			Packet packet = new Packet(north1, 0, north1.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}
	}
}
