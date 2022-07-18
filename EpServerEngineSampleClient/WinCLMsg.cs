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
	public partial class WinCLMsg : Form
	{
		private INetworkClient m_client;
		private bool m_wait = false;
		ServerCmds svrcmd = new ServerCmds();
		private bool m_pause = false;

		int cmd = 0;
		int dest = 1;
		public void Enable_Dlg(bool wait)
		{
			m_wait = wait;
		}
		public WinCLMsg()
		{
			InitializeComponent();
			cbCmd.SelectedIndex = 0;
			cbDest.SelectedIndex = 0;
		}
		public void SetClient(INetworkClient client)
		{
			m_client = client;
			svrcmd.SetClient(m_client);
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
		private void btnSend_Click(object sender, EventArgs e)
		{
			string cmd = cbCmd.SelectedItem.ToString();
			AddMsg(cmd + " " + dest.ToString());
			int offset = svrcmd.GetCmdIndexI(cmd);
			offset = svrcmd.GetCmdIndexI(cmd);
			svrcmd.Send_ClCmd(offset, dest, tbTextToSend.Text);
		}
		private void cbCmd_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmd = cbCmd.SelectedIndex;
			AddMsg(cmd.ToString());
		}
		private void cbDest_SelectedIndexChanged(object sender, EventArgs e)
		{
			dest = cbDest.SelectedIndex;
			AddMsg(dest.ToString());
		}
	}
}
