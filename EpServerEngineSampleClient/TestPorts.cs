using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EpServerEngine.cs;

namespace EpServerEngineSampleClient
{
	public partial class TestPorts : Form
	{
		private int selected_bank = 0;
		private int selected_port = 0;
		private int selected_lcd_value = 0;
		private bool onoff = false;
		private bool prev_next_flag = true;
		private bool send_debug = false;
		INetworkClient m_client = new IocpTcpClient();
		ServerCmds svrcmd = new ServerCmds();
		private List<bool> port_list = new List<bool>();
		private int brightness = 100;
		private bool test_engine_temps = true;
		private bool led_test_mode = false;
		public void SetClient(INetworkClient client)
		{
			m_client = client;
		}
		public TestPorts()
		{
			InitializeComponent();
			foreach(string port_name in lbPortList.Items)
			{
				//AddMsg(port_name.ToString());
				port_list.Add(false);
			}
			//foreach (int port in lbPortList.Items)
			//{
			//port_list.Add()
			//}
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
		private void btnToggle_Click(object sender, EventArgs e)
		{
			int index = selected_bank * 8 + selected_port;
			int ionoff = 0;
			//AddMsg(index.ToString());
			port_list[index] = !port_list[index];
			ionoff = port_list[index] ? 1 : 0;
			Send_Msg(index, ionoff);
		}

		private void lbPortList_SelectedIndexChanged(object sender, EventArgs e)
		{
			int i = lbPortList.SelectedIndex;
			selected_bank = i / 8;
			selected_port = i % 8;
			//AddMsg(i.ToString() + " " + selected_port.ToString() + " " + selected_bank.ToString());
			lblBankSelected.Text = "Bank " + selected_bank.ToString();
			lblPortSelected.Text = "Port " + selected_port.ToString();
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			if (prev_next_flag)
			{
				if (++selected_port > 7)
				{
					selected_port = 0;
					if (++selected_bank > 4)
					{
						selected_bank = 0;
					}
				}
			}
			prev_next_flag = !prev_next_flag;
			int index = selected_bank * 8 + selected_port;
			port_list[index] = !port_list[index];
			Send_Msg(index, port_list[index]?1:0);
			AddMsg(selected_bank.ToString() + " " + selected_port.ToString());
		}

		private void btnPrev_Click(object sender, EventArgs e)
		{
			if (prev_next_flag)
			{
				if (--selected_port < 0)
				{
					selected_port = 7;
					if (--selected_bank < 0)
					{
						selected_bank = 4;
					}
				}
			}
			prev_next_flag = !prev_next_flag;
			int index = selected_bank * 8 + selected_port;
			port_list[index] = !port_list[index];
			Send_Msg(index, port_list[index] ? 1 : 0);
			AddMsg(selected_bank.ToString() + " " + selected_port.ToString());

		}
		private int Send_Msg(int index, int ionoff)
		{
			string cmd = "TEST_IO_PORT";
			byte[] bank = BitConverter.GetBytes(selected_bank);
			byte[] port = BitConverter.GetBytes(selected_port);
			byte[] onoff = BitConverter.GetBytes(ionoff);
			byte[] bytes = new byte[onoff.Count() + port.Count() + bank.Count() + 2];
			bytes[0] = svrcmd.GetCmdIndexB(cmd);
			System.Buffer.BlockCopy(bank, 0, bytes, 2, bank.Count());
			System.Buffer.BlockCopy(port, 0, bytes, 4, port.Count());
			System.Buffer.BlockCopy(onoff, 0, bytes, 6, port.Count());
			Packet packet = new Packet(bytes, 0, bytes.Count(), false);
			m_client.Send(packet);
			AddMsg(index.ToString() + ": " + lbPortList.Items[index].ToString() + " = " + port_list[index].ToString());
			return 0;
		}

		private void btnFPGAstatus_Click(object sender, EventArgs e)
		{
			string cmd = "SEND_RT_FPGA_STATUS";
			byte[] bytes = new byte[2];
			bytes[0] = svrcmd.GetCmdIndexB(cmd);
			Packet packet = new Packet(bytes, 0, bytes.Count(), false);
			m_client.Send(packet);
			//AddMsg("send FPGA status");
		}

		private void btnTestLCDPWM_Click(object sender, EventArgs e)
		{
			string cmd = "SEND_LCD_PWM";
			byte[] lcd_value = BitConverter.GetBytes(selected_lcd_value);
			byte[] bytes = new byte[lcd_value.Count() + 2];
			bytes[0] = svrcmd.GetCmdIndexB(cmd);
			System.Buffer.BlockCopy(lcd_value, 0, bytes, 2, lcd_value.Count());
			Packet packet = new Packet(bytes, 0, bytes.Count(), false);
			m_client.Send(packet);
			//AddMsg("SEND_LCD_PWM");
			//AddMsg(lcd_value.ToString());

		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			selected_lcd_value = listBox1.SelectedIndex;
		}

		private void cbSendDebug_CheckedChanged(object sender, EventArgs e)
		{
			send_debug = cbSendDebug.Checked;
			string cmd = "SEND_DEBUG_MSG";
			int value = send_debug ? 1 : 0;
			byte[] bval = BitConverter.GetBytes(value);
			byte[] bytes = new byte[bval.Count() + 2];
			bytes[0] = svrcmd.GetCmdIndexB(cmd);
			System.Buffer.BlockCopy(bval, 0, bytes, 2, bval.Count());
			Packet packet = new Packet(bytes, 0, bytes.Count(), false);
			m_client.Send(packet);
		}

		private void btnTestRPMMPHBrightness_Click(object sender, EventArgs e)
		{
			string cmd = "SET_RPM_MPH_BRIGHTNESS";
			byte[] bval = BitConverter.GetBytes(brightness);
			byte[] bytes = new byte[bval.Count() + 2];
			bytes[0] = svrcmd.GetCmdIndexB(cmd);
			System.Buffer.BlockCopy(bval, 0, bytes, 2, bval.Count());
			Packet packet = new Packet(bytes, 0, bytes.Count(), false);
			m_client.Send(packet);
		}

		private void lbRPMMPHBrightness_SelectedIndexChanged(object sender, EventArgs e)
		{
			brightness = lbRPMMPHBrightness.SelectedIndex;
		}

		private void btnTestEngineTemps_Click(object sender, EventArgs e)
		{
			string cmd = "TEST_ENGINE_TEMPS";
			int value = test_engine_temps ? 1 : 0;
			byte[] bval = BitConverter.GetBytes(value);
			byte[] bytes = new byte[bval.Count() + 2];
			bytes[0] = svrcmd.GetCmdIndexB(cmd);
			System.Buffer.BlockCopy(bval, 0, bytes, 2, bval.Count());
			Packet packet = new Packet(bytes, 0, bytes.Count(), false);
			m_client.Send(packet);
			if (test_engine_temps)
				btnTestEngineTemps.Text = "Turn Off Test";
			else btnTestEngineTemps.Text = "Test Engine Temps";
			test_engine_temps = !test_engine_temps;
		}

		private void btnTestRPM_Click(object sender, EventArgs e)
		{
		}

		private void btnTestMode_Click(object sender, EventArgs e)
		{
			led_test_mode = !led_test_mode;
			string cmd = "LCD_TEST_MODE";
			int itest_mode = led_test_mode ? 1 : 0;
			byte[] ibytes = BitConverter.GetBytes(itest_mode);
			byte[] bytes = new byte[ibytes.Count() + 2];
			bytes[0] = svrcmd.GetCmdIndexB(cmd);
			System.Buffer.BlockCopy(ibytes, 0, bytes, 2, ibytes.Count());
			Packet packet = new Packet(bytes, 0, bytes.Count(), false);
			m_client.Send(packet);
			lbTestingLEDs.Visible = led_test_mode ? true : false;
		}
	}
}
