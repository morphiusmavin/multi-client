using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
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
	public partial class DS1620Mgt : Form
	{
		private int client_no;
		private int[] types = new int[4];
		private string new_filename;
		int interval = 7;
		private INetworkClient m_client;
		ServerCmds svrcmd = new ServerCmds();
		int target;

		public DS1620Mgt(INetworkClient client)
		{
			InitializeComponent();
			m_client = client;
			svrcmd.SetClient(m_client);
			client_no = 0;
			types[0] = (ushort)Properties.Settings.Default["ds_server"];
			types[1] = (ushort)Properties.Settings.Default["ds_154"];
			types[2] = (ushort)Properties.Settings.Default["ds_147"];
			types[3] = (ushort)Properties.Settings.Default["ds_150"];
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
		private void btnRenameFile_Click(object sender, EventArgs e)
		{
			if (tbNewFileName.Text == "")
			{
				AddMsg("must enter new filename");
				return;
			}
			new_filename = tbNewFileName.Text;
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("RENAME_D_DATA"), target, new_filename);
		}
		private void cbClientNames_SelectedIndexChanged(object sender, EventArgs e)
		{
			client_no = cbClientNames.SelectedIndex;
			set_checked(types[client_no]);
			switch(client_no)
			{
				case 0:
					target = 8;		// server
					break;
				case 1:
					target = 2;		// 154
					break;
				case 2:
					target = 3;		// 147
					break;
				case 3:
					target = 4;		// 150
					break;
				case 4:
					target = 0;
					break;
				default:
					target = 0;
					break;
			}
		}
		private void set_checked(int type)
		{
			chkDS1.Checked = ((type & 0x01) == 1);
			chkDS2.Checked = ((type & 0x02) == 2);
			chkDS3.Checked = ((type & 0x04) == 4);
			chkDS4.Checked = ((type & 0x08) == 8);
			chkDS5.Checked = ((type & 0x10) == 0x10);
			chkDS6.Checked = ((type & 0x20) == 0x20);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			switch(client_no)
			{
				case 0:
					break;
				case 1:
					break;
				case 2:
					break;
				case 3:
					break;
				default:
					break;
			}
		}

		private void Cancel_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			tbReceived.Clear();
		}

		private void chkDS1_CheckedChanged(object sender, EventArgs e)
		{
			bool test = chkDS1.Checked;
			int mask = 1;
			if (test)
				types[client_no] |= mask;
			else types[client_no] &= ~mask;
			AddMsg(types[client_no].ToString() + " " + test.ToString());
		}

		private void chkDS2_CheckedChanged(object sender, EventArgs e)
		{
			bool test = chkDS2.Checked;
			int mask = 2;
			if (test)
				types[client_no] |= mask;
			else types[client_no] &= ~mask;
		}

		private void chkDS3_CheckedChanged(object sender, EventArgs e)
		{
			bool test = chkDS3.Checked;
			int mask = 4;
			if (test)
				types[client_no] |= mask;
			else types[client_no] &= ~mask;
		}

		private void chkDS4_CheckedChanged(object sender, EventArgs e)
		{
			bool test = chkDS4.Checked;
			int mask = 8;
			if (test)
				types[client_no] |= mask;
			else types[client_no] &= ~mask;
		}

		private void chkDS5_CheckedChanged(object sender, EventArgs e)
		{
			bool test = chkDS5.Checked;
			int mask = 0x10;
			if (test)
				types[client_no] |= mask;
			else types[client_no] &= ~mask;
		}

		private void chkDS6_CheckedChanged(object sender, EventArgs e)
		{
			bool test = chkDS6.Checked;
			int mask = 0x20;
			if (test)
				types[client_no] |= mask;
			else types[client_no] &= ~mask;
		}

		private void btnValidDS_Click(object sender, EventArgs e)
		{
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SET_VALID_DS"), target, types[client_no]);
		}

		private void btnApplyInterval_Click(object sender, EventArgs e)
		{
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SET_DS_INTERVAL"),target,interval);
		}

		private void rbInterval1_CheckedChanged(object sender, EventArgs e)
		{
			interval = 0;
		}

		private void rbInterval2_CheckedChanged(object sender, EventArgs e)
		{
			interval = 1;
		}

		private void rbInterval3_CheckedChanged(object sender, EventArgs e)
		{
			interval = 2;
		}

		private void rbInterval4_CheckedChanged(object sender, EventArgs e)
		{
			interval = 3;
		}

		private void rbInterval5_CheckedChanged(object sender, EventArgs e)
		{
			interval = 4;
		}

		private void rbInterval6_CheckedChanged(object sender, EventArgs e)
		{
			interval = 5;
		}

		private void rbInterval7_CheckedChanged(object sender, EventArgs e)
		{
			interval = 6;
		}

		private void rbInterval8_CheckedChanged(object sender, EventArgs e)
		{
			interval = 7;
		}

		private void btnShow_Click(object sender, EventArgs e)
		{
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("DLLIST_SHOW"), target, interval);
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("DLLIST_SAVE"), target, interval);
		}

		private void rbInterval9_CheckedChanged(object sender, EventArgs e)
		{
			interval = 8;
		}
	}
}
