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
			DESK_LIGHT,
			EAST_LIGHT,
			NORTHWEST_LIGHT,
			SOUTHEAST_LIGHT,
			MIDDLE_LIGHT,
			WEST_LIGHT,
			NORTHEAST_LIGHT,
			SOUTHWEST_LIGHT,
			BENCH_24V_1,
			BENCH_24V_2,
			BENCH_12V_1,
			BENCH_12V_2,
			BLANK,
			BENCH_5V_1,
			BENCH_5V_2,
			BENCH_3V3_1,
			BENCH_3V3_2,
			BENCH_LIGHT1,
			BENCH_LIGHT2,
			CHICK_WATER,
			SET_CHICK_WATER_ON,
			SET_CHICK_WATER_OFF,
			CHICK_WATER_ENABLE,
			GET_TEMP4,
			SHUTDOWN_IOBOX,
			REBOOT_IOBOX,
			SET_TIME,
			GET_TIME,
			DISCONNECT,
			BAD_MSG,
			SEND_TIMEUP,
			SET_PARAMS,
			UPTIME_MSG,
			SEND_CONFIG,
			SEND_STATUS,
			GET_VERSION,
			UPDATE_CONFIG,
			SEND_CLIENT_LIST,
			GET_CONFIG2,
			SHELL_AND_RENAME,
			EXIT_TO_SHELL,
			UPDATE_STATUS,
			SEND_MESSAGE,
			CLIENT_RECONNECT,
			DB_LOOKUP,
			SET_TIMER,
			START_TIMER1,
			START_TIMER2,
			STOP_TIMER,
			SET_NEXT_CLIENT,
			SEND_NEXT_CLIENT,
			UPDATE_CLIENT_INFO,
			AREYOUTHERE,
			YESIMHERE
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
		public void Send_ClCmd(int msg, int index, bool iparam)
		{
			var temp = index;
			var temp2 = iparam?1:0;
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
		public int Send_ClCmd(int msg, int index, int iparam)
		{
			var temp = index;
			var temp2 = iparam & 0x00FF;
			var temp3 = iparam >> 8;
			byte[] atemp = BitConverter.GetBytes(temp);
			byte[] a2temp = BitConverter.GetBytes(temp2);
			byte[] a3temp = BitConverter.GetBytes(temp3);
			//AddMsg(atemp.Count().ToString());
			byte[] ctemp = new byte[atemp.Count() + a2temp.Count() + a3temp.Count() + 2];
			string cmsg = GetName(msg);
			ctemp[0] = GetCmdIndexB(cmsg);
			//AddMsg(ctemp.Count().ToString());
			System.Buffer.BlockCopy(atemp, 0, ctemp, 2, atemp.Count());
			System.Buffer.BlockCopy(a2temp, 0, ctemp, 4, a2temp.Count());
			System.Buffer.BlockCopy(a3temp, 0, ctemp, 6, a2temp.Count());
			Packet packet = new Packet(ctemp, 0, ctemp.Count(), false);
			if (m_client.IsConnectionAlive)
			{
				m_client.Send(packet);
			}
			return ctemp.Count();
		}
		public void Send_ClCmd(int msg, int index, long iparam)
		{
			var temp = index;
			byte[] bytes = BitConverter.GetBytes(iparam);
			byte[] atemp = BitConverter.GetBytes(temp);     // index
			byte[] dtemp = new byte[bytes.Count() * 2];
			int j = 0;
			for (int i = 0; i < dtemp.Length - 1; i += 2)
			{
				dtemp[i] = bytes[j++];
				dtemp[i + 1] = 0;
			}
			byte[] ctemp = new byte[dtemp.Count() + atemp.Count() + 2];
			string cmsg = GetName(msg);
			ctemp[0] = GetCmdIndexB(cmsg);
			System.Buffer.BlockCopy(atemp, 0, ctemp, 2, atemp.Count());
			System.Buffer.BlockCopy(dtemp, 0, ctemp, 4, bytes.Count());
			Packet packet = new Packet(ctemp, 0, ctemp.Count(), false);
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
