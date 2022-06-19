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

	public partial class SetNextClient : Form
	{
		private INetworkClient m_client;
		private bool m_wait = false;
		ServerCmds svrcmd = new ServerCmds();
		private bool m_pause = false;
		private List<ClientsAvail> clients_avail;
		int sindex, dindex;

		public SetNextClient()
		{
			InitializeComponent();
			sindex = dindex = 0;
		}
		public void SetClient(INetworkClient client)
		{
			m_client = client;
			svrcmd.SetClient(m_client);
		}

		private void cbSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			sindex = cbSource.SelectedIndex;
			sindex++;
		}

		private void cbNextClient_SelectedIndexChanged(object sender, EventArgs e)
		{
			dindex = cbNextClient.SelectedIndex;
			dindex++;
		}

		private void btnSetNextClient_Click(object sender, EventArgs e)
		{
			SendCmd("SET_NEXT_CLIENT", sindex, dindex);
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			SendCmd("SEND_NEXT_CLIENT", sindex, dindex);
		}

		private void SendCmd(string cmd, int sindex, int dindex)
		{
			int offset = svrcmd.GetCmdIndexI(cmd);
			svrcmd.Send_ClCmd(offset, sindex, dindex);
		}
	}
}
