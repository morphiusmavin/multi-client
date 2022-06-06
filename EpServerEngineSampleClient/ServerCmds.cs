using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpLibrary.cs;
using EpServerEngine.cs;

namespace EpServerEngineSampleClient
{
	class ServerCmds
	{
		enum Server_cmds
		{
			NON_CMD,
			ALL_LIGHTS_ON,
			ALL_LIGHTS_OFF,
			ALL_NORTH_ON,
			ALL_SOUTH_ON,
			ALL_MIDDLE_ON,
			ALL_NORTH_OFF,
			ALL_SOUTH_OFF,
			ALL_MIDDLE_OFF,
			ALL_EAST_ON,
			ALL_EAST_OFF,
			ALL_WEST_ON,
			ALL_WEST_OFF,
			BLANK,
			ALL_OFFICE_ON,
			ALL_OFFICE_OFF,
			WORK_ON,
			WORK_OFF,
			GET_TEMP4,
			SHUTDOWN_IOBOX,
			REBOOT_IOBOX,
			WAIT_REBOOT_IOBOX,
			SET_TIME,
			GET_TIME,
			DISCONNECT,
			BAD_MSG,
			SEND_TIMEUP,
			SET_PARAMS,
			EXIT_PROGRAM,
			UPTIME_MSG,
			SEND_CONFIG,
			SEND_STATUS,
			GET_VERSION,
			UPDATE_CONFIG,
			SEND_CLIENT_LIST,
			GET_CONFIG2,
			SHELL_AND_RENAME,
			UPDATE_STATUS,
			SEND_MSG,
			CLIENT_RECONNECT,
			DB_LOOKUP,
			SET_TIMER,
			START_TIMER,
			STOP_TIMER,
			UPDATE_CLIENT_LIST
		}

		public ServerCmds()
		{

		}
		private INetworkClient m_client;
		public void SetClient(INetworkClient client)
		{
			m_client = client;
		}
		//Queue<int> qt = new Queue<int>();

		byte[] BytesFromString(String str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		public byte GetCmdIndexB(string cmd)
		{
			byte i = 0;
			string cmd2 = "";
			do
			{
				cmd2 = Enum.GetName(typeof(Server_cmds), i);
				i++;
			} while (cmd2 != cmd);
			i--;
			return i;
		}
		public int GetCmdIndexI(string cmd)
		{
			int i = 0;
			string cmd2 = "";
			do
			{
				cmd2 = Enum.GetName(typeof(Server_cmds), i);
				i++;
			} while (cmd2 != cmd && i < 200);
			i--;
			return i;
		}
		//public int GetCount()
		//{
		//    Array n = Enum.GetValues(typeof(ServerCmds));
		//    return n.Length;
		//}
		public string GetName(int cmd)
		{
			string cmd2 = "";
			cmd++;
			int i = 0;
			do
			{
				cmd2 = Enum.GetName(typeof(Server_cmds), i);
				i++;
			} while (i != cmd);
			return cmd2;
		}
		public void Send_Cmd(int sendcmd)
		{
						//string test = " ";
						//byte[] bytes = BytesFromString(test);
			byte[] bytes = new byte[2];
			if (m_client.IsConnectionAlive)
			{
				bytes.SetValue((byte)sendcmd, 0);
				Packet packet = new Packet(bytes, 0, bytes.Count(), false);
				m_client.Send(packet);
			}
		}
		public void Send_ClCmd(int msg, int index, string param)
		{
			var temp = index;
			byte[] atemp = BitConverter.GetBytes(temp);
			byte[] btemp = BytesFromString(param);
			byte[] ctemp = new byte[atemp.Count() + btemp.Length + 2];
			string cmsg = GetName(msg);
			ctemp[0] = GetCmdIndexB(cmsg);
			System.Buffer.BlockCopy(atemp, 0, ctemp, 2, atemp.Count());
			System.Buffer.BlockCopy(btemp, 0, ctemp, 4, btemp.Length);
			Packet packet = new Packet(ctemp, 0, ctemp.Count(), false);
			//AddMsg(ctemp.Count().ToString() + " " + temp.ToString());
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
		}
		public void Send_ClCmd(int msg, int index, int iparam)
		{
			var temp = index;
			var temp2 = iparam;
			byte[] atemp = BitConverter.GetBytes(temp);
			byte[] a2temp = BitConverter.GetBytes(temp2);
			byte[] ctemp = new byte[atemp.Count() + a2temp.Count() + 2];
			string cmsg = GetName(msg);
			ctemp[0] = GetCmdIndexB(cmsg);
			System.Buffer.BlockCopy(atemp, 0, ctemp, 2, atemp.Count());
			System.Buffer.BlockCopy(a2temp, 0, ctemp, 4, a2temp.Count());
			Packet packet = new Packet(ctemp, 0, ctemp.Count(), false);
			//AddMsg(ctemp.Count().ToString() + " " + temp.ToString());
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}

		}
		public bool connection_alive()
		{
			return m_client.IsConnectionAlive;
		}
	}
}
