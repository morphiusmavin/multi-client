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
	public partial class WifiIOT : Form
	{
		private bool m_wait = false;
		private INetworkClient m_client;
		ServerCmds svrcmd = new ServerCmds();
		private List<int> CurrentList = new List<int>();
		private byte[] recv_buff;
		private bool m_pause = false;
		bool[] status = new bool[7];
		bool[] prev_status = new bool[7];
		List<String> on_label_list = new List<String>();
		List<String> off_label_list = new List<String>();
		List<String> esp_msg_list = new List<String>();
		public System.Collections.Generic.List<ButtonList> button_list;
		public WifiIOT(string xml_file_location, INetworkClient client)
		{
			InitializeComponent();
			m_client = client;
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

			prev_status[0] = false;
			prev_status[1] = false;
			prev_status[2] = false;
			prev_status[3] = false;
			prev_status[4] = false;
			prev_status[5] = false;
			prev_status[6] = false;
			recv_buff = new byte[200];

			on_label_list.Add("ALL_NORTH_ON");
			on_label_list.Add("ALL_SOUTH_ON");
			on_label_list.Add("ALL_EAST_ON");
			on_label_list.Add("ALL_WEST_ON");
			on_label_list.Add("ALL_MIDDLE_ON");
			on_label_list.Add("TWO_OFFICE_ON");
			on_label_list.Add("ALL_LIGHTS_ON");

			off_label_list.Add("ALL_NORTH_OFF");
			off_label_list.Add("ALL_SOUTH_OFF");
			off_label_list.Add("ALL_EAST_OFF");
			off_label_list.Add("ALL_WEST_OFF");
			off_label_list.Add("ALL_MIDDLE_OFF");
			off_label_list.Add("TWO_OFFICE_OFF");
			off_label_list.Add("ALL_LIGHTS_OFF");

			esp_msg_list.Add("ESP_MSG0");
			esp_msg_list.Add("ESP_MSG1");
			esp_msg_list.Add("ESP_MSG2");
			esp_msg_list.Add("ESP_MSG3");
			esp_msg_list.Add("ESP_MSG4");
			esp_msg_list.Add("ESP_MSG5");
			esp_msg_list.Add("ESP_MSG6");
			esp_msg_list.Add("ESP_MSG7");

			button_list = new List<ButtonList>();
			Control sCtl = this.btnNorth;
			//for (int i = 0; i < this.Controls.Count; i++)
			for (int i = 0; i < 7; i++)
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

			if (m_wait == true && m_pause == false)
			{
				int type_msg = (int)bytes[0];
				int i = 0;
				string msg = svrcmd.GetName(type_msg);
				if(msg != "SERVER_UPTIME")
					AddMsg(msg);
				if (msg == "ESP_CLIENT_STATUS")
				{
					AddMsg("len: " + bytes.Length.ToString());
					string str = "";
					for (i = 0;i < bytes.Length;i++)
					{
						//AddMsg(bytes[i].ToString());
						if(bytes[i] != 0)
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
		// pass in -1 to tell this to check all of the status array
		// or pass in a 0->5 to set a single status item
		private void IfStatusChanged(int which)
		{
			//AddMsg(which.ToString());
			if (which == -1)
				for (int i = 0; i < 7; i++)
				{
					//if (status[i] != prev_status[i])
					if (true)
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
			else if (which > -1 && which < 7)
			{
				//if (status[which] != prev_status[which])
				if (true)
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
			byte[] update_rate = BitConverter.GetBytes(which);
			byte[] bytes = new byte[update_rate.Count() + 2];
			System.Buffer.BlockCopy(update_rate, 0, bytes, 2, update_rate.Count());
			bytes[0] = svrcmd.GetCmdIndexB("ESP_SEND_CMD");
			Packet packet = new Packet(bytes, 0, bytes.Count(), false);
			AddMsg("adc update rate: " + update_rate.ToString());
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}
		private void ChangeStatus(int i)
		{
			status[i] = !status[i];
			//AddMsg("status changed: " + i.ToString());
			//IfStatusChanged();
		}
		// this is actually north
		private void btnNorth_Click(object sender, EventArgs e)
		{
			int which = 2;
			int sec = 3;
			int third = 4;
			byte[] test = BitConverter.GetBytes(which);
			byte[] test2 = BitConverter.GetBytes(sec);
			byte[] test3 = BitConverter.GetBytes(third);
			byte[] bytes = new byte[test.Count() + test2.Count() + test3.Count() + 2];
			System.Buffer.BlockCopy(test, 0, bytes, 2, test.Count());
			System.Buffer.BlockCopy(test2, 0, bytes, 4, test.Count());
			System.Buffer.BlockCopy(test3, 0, bytes, 6, test.Count());
			bytes[0] = svrcmd.GetCmdIndexB("ESP_MSG0");
			Packet packet = new Packet(bytes, 0, bytes.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		private void btnSouth_Click(object sender, EventArgs e)
		{
			int which = 5;
			int sec = 6;
			int third = 7;
			byte[] test = BitConverter.GetBytes(which);
			byte[] test2 = BitConverter.GetBytes(sec);
			byte[] test3 = BitConverter.GetBytes(third);
			byte[] bytes = new byte[test.Count() + test2.Count() + test3.Count() + 2];
			System.Buffer.BlockCopy(test, 0, bytes, 2, test.Count());
			System.Buffer.BlockCopy(test2, 0, bytes, 4, test.Count());
			System.Buffer.BlockCopy(test3, 0, bytes, 6, test.Count());
			bytes[0] = svrcmd.GetCmdIndexB("ESP_MSG1");
			Packet packet = new Packet(bytes, 0, bytes.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		private void btnEast_Click(object sender, EventArgs e)
		{
			int which = 8;
			int sec = 9;
			int third = 10;
			byte[] test = BitConverter.GetBytes(which);
			byte[] test2 = BitConverter.GetBytes(sec);
			byte[] test3 = BitConverter.GetBytes(third);
			byte[] bytes = new byte[test.Count() + test2.Count() + test3.Count() + 2];
			System.Buffer.BlockCopy(test, 0, bytes, 2, test.Count());
			System.Buffer.BlockCopy(test2, 0, bytes, 4, test.Count());
			System.Buffer.BlockCopy(test3, 0, bytes, 6, test.Count());
			bytes[0] = svrcmd.GetCmdIndexB("ESP_MSG2");
			Packet packet = new Packet(bytes, 0, bytes.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}

		private void btnWest_Click(object sender, EventArgs e)
		{
			SendCmd(3);
		}

		private void btnMiddle_Click(object sender, EventArgs e)
		{
			SendCmd(4);
		}

		private void btnOffice_Click(object sender, EventArgs e)
		{
			SendCmd(5);
		}

		private void btnPoll_Click(object sender, EventArgs e)
		{
			SendCmd(6);
		}

		private void btnPollStatus_Click(object sender, EventArgs e)
		{
			int poll_cmd = svrcmd.GetCmdIndexI("ESP_POLL_CLIENTS");
			svrcmd.Send_Cmd(poll_cmd);
		}

		private void btnClear_Click_1(object sender, EventArgs e)
		{
			tbAddMsg.Clear();
		}
	}
}
