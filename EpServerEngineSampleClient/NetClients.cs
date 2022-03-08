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
	public partial class NetClients : Form
	{
		private bool m_wait = false;
		private INetworkClient m_client;
		ServerCmds svrcmd = new ServerCmds();
		public NetClients()
		{
			InitializeComponent();
		}

		private void NetClients_Load(object sender, EventArgs e)
		{

		}
		public void SetClient(INetworkClient client)
		{
			m_client = client;
			svrcmd.SetClient(m_client);
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
			//AddMsg("len: " + bytes.Length.ToString());

			//AddMsg(bytes[0].ToString() + " " + bytes[1].ToString());

			//AddMsg(bytes[0].ToString() + " " + bytes[1].ToString() + " " + bytes[2].ToString() + " " + bytes[3].ToString() + " " + bytes[4].ToString() + " " + bytes[5].ToString());
			//AddMsg(bytes[6].ToString() + " " + bytes[7].ToString() + " " + bytes[8].ToString() + " " + bytes[9].ToString() + " " + bytes[10].ToString() + " " + bytes[11].ToString());

			//			for (int i = 0; i < bytes.Length; i++)
			//				AddMsg(bytes[i].ToString());

			if (m_wait == true)
			{
				int type_msg = (int)bytes[0];
				int i = 0;
				string msg = svrcmd.GetName(type_msg);
				if (msg != "SERVER_UPTIME")
					AddMsg(msg);
				if (msg == "ESP_CLIENT_STATUS")
				{
					AddMsg("len: " + bytes.Length.ToString());
					string str = "";
					for (i = 0; i < bytes.Length; i++)
					{
						//AddMsg(bytes[i].ToString());
						if (bytes[i] != 0)
							str += bytes[i].ToString();
					}
					AddMsg(str);
					//					AddMsg(bytes[2].ToString() + " " + bytes[3].ToString() + " " + bytes[4].ToString());
					//					AddMsg(bytes[5].ToString() + " " + bytes[6].ToString() + " " + bytes[7].ToString());
					switch (bytes[4])
					{
						case 49:
							AddMsg("esp cmd 0");
							break;
						case 50:
							AddMsg("esp cmd 1");
							break;
						case 51:
							AddMsg("esp cmd 2");
							break;
						case 52:
							AddMsg("esp cmd 3");
							break;
						case 53:
							AddMsg("esp cmd 4");
							break;
						case 54:
							AddMsg("esp cmd 5");
							break;
						case 55:
							AddMsg("esp cmd 6");
							break;
						case 56:
							AddMsg("esp cmd 6");
							break;
					}
				}
			}
		}

		private void btnReboot0_Click(object sender, EventArgs e)
		{

		}

		private void btnReboot1_Click(object sender, EventArgs e)
		{

		}

		private void btnReboot2_Click(object sender, EventArgs e)
		{

		}

		private void btnReboot3_Click(object sender, EventArgs e)
		{

		}
	}
}
