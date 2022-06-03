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
	public partial class ClientDest : Form
	{
		private INetworkClient m_client;
		private bool m_wait = false;
		ServerCmds svrcmd = new ServerCmds();
		private bool m_pause = false;
		private List<ClientsAvail> clients_avail;
		int sindex, iparam;
		int timer_seconds = 0;

		public ClientDest()
		{
			InitializeComponent();
			sindex = 0;
			cbTimerSeconds.SelectedIndex = 0;
			iparam = 200;
		}
		public void SetClient(INetworkClient client)
		{
			m_client = client;
			svrcmd.SetClient(m_client);
		}
		/*		public void setClientsAvail(List<ClientsAvail> ca)
				{
					clients_avail = ca;
					foreach (ClientsAvail cl in clients_avail)
					{
						AddMsg(cl.label);
						if (cl.socket > 0)
						{
							AddMsg(cl.socket.ToString());
							cbSource.Items.Add(cl.label);
							cbDest.Items.Add(cl.label);
						}
					}
				}
		*/
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
		private void cbTimerSeconds_SelectedIndexChanged(object sender, EventArgs e)
		{
			int val = cbTimerSeconds.SelectedIndex;
			switch (val)
			{
				case 0:
					timer_seconds = 1;
					break;
				case 1:
					timer_seconds = 2;
					break;
				case 2:
					timer_seconds = 3;
					break;
				case 3:
					timer_seconds = 4;
					break;
				case 4:
					timer_seconds = 5;
					break;
				case 5:
					timer_seconds = 10;
					break;
				case 6:
					timer_seconds = 15;
					break;
				case 7:
					timer_seconds = 20;
					break;
				default:
					timer_seconds = 1;
					break;
			}
			AddMsg("timer set to: " + timer_seconds.ToString());
		}
		private void btnSetTimer_Click(object sender, EventArgs e)
		{
			SendCmd("SET_TIMER", sindex, timer_seconds);
		}
		private void btnStartTimer_Click(object sender, EventArgs e)
		{
			SendCmd("START_TIMER", sindex, iparam);
		}
		private void btnStopTimer_Click(object sender, EventArgs e)
		{
			SendCmd("STOP_TIMER", sindex, iparam);
		}
		private void SendCmd(string cmd, int sindex, int dindex)
		{
			AddMsg(cmd + " " + sindex.ToString() + " " + dindex.ToString());
			int offset = svrcmd.GetCmdIndexI(cmd);
			svrcmd.Send_ClCmd(offset, sindex, dindex);
		}
		private void cbSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			sindex = cbSource.SelectedIndex;
			AddMsg("source: " + sindex.ToString());
		}
		
	}
}
