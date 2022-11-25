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
			WATER_HEATER,
			BATTERY_HEATER,
			WATER_PUMP,
			WATER_VALVE1,
			WATER_VALVE2,
			WATER_VALVE3,
			CABIN1,
			CABIN2,
			CABIN3,
			CABIN4,
			CABIN5,
			CABIN6,
			CABIN7,
			CABIN8,
			COOP1_LIGHT,
			COOP1_HEATER,
			COOP2_LIGHT,
			COOP2_HEATER,
			OUTDOOR_LIGHT1,
			OUTDOOR_LIGHT2,
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
			YESIMHERE,
			GET_CLLIST,
			GET_ALL_CLLIST,
			REPLY_CLLIST,
			SET_CLLIST,
			SAVE_CLLIST,
			NO_CLLIST_REC
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
		public bool Change_PortCmd(int msg, int index, bool iparam)
		{
			Send_ClCmd(msg, index, iparam);
			switch(GetName(msg))
			{
				case "DESK_LIGHT":
					Properties.Settings.Default["DESK_LIGHT"] = iparam;
					break;
				case "EAST_LIGHT":
					Properties.Settings.Default["EAST_LIGHT"] = iparam;
					break;
				case "NORTHWEST_LIGHT":
					Properties.Settings.Default["NORTHWEST_LIGHT"] = iparam;
					break;
				case "SOUTHEAST_LIGHT":
					Properties.Settings.Default["SOUTHEAST_LIGHT"] = iparam;
					break;
				case "MIDDLE_LIGHT":
					Properties.Settings.Default["MIDDLE_LIGHT"] = iparam;
					break;
				case "WEST_LIGHT":
					Properties.Settings.Default["WEST_LIGHT"] = iparam;
					break;
				case "NORTHEAST_LIGHT":
					Properties.Settings.Default["NORTHEAST_LIGHT"] = iparam;
					break;
				case "SOUTHWEST_LIGHT":
					Properties.Settings.Default["SOUTHWEST_LIGHT"] = iparam;
					break;
				case "BENCH_24V_1":
					Properties.Settings.Default["BENCH_24V_1"] = iparam;
					break;
				case "BENCH_24V_2":
					Properties.Settings.Default["BENCH_24V_2"] = iparam;
					break;
				case "BENCH_12V_1":
					Properties.Settings.Default["BENCH_12V_1"] = iparam;
					break;
				case "BENCH_12V_2":
					Properties.Settings.Default["BENCH_12V_2"] = iparam;
					break;
				case "BENCH_5V_1":
					Properties.Settings.Default["BENCH_5V_1"] = iparam;
					break;
				case "BENCH_5V_2":
					Properties.Settings.Default["BENCH_5V_2"] = iparam;
					break;
				case "BENCH_3V3_1":
					Properties.Settings.Default["BENCH_3V3_1"] = iparam;
					break;
				case "BENCH_3V3_2":
					Properties.Settings.Default["BENCH_3V3_2"] = iparam;
					break;
				case "BENCH_LIGHT1":
					Properties.Settings.Default["BENCH_LIGHT1"] = iparam;
					break;
				case "BENCH_LIGHT2":
					Properties.Settings.Default["BENCH_LIGHT2"] = iparam;
					break;
				case "CHICK_WATER":
					Properties.Settings.Default["CHICK_WATER"] = iparam;
					break;
				case "CABIN1":
					Properties.Settings.Default["CABIN1"] = iparam;
					break;
				case "CABIN2":
					Properties.Settings.Default["CABIN2"] = iparam;
					break;
				case "CABIN3":
					Properties.Settings.Default["CABIN3"] = iparam;
					break;
				case "CABIN4":
					Properties.Settings.Default["CABIN4"] = iparam;
					break;
				case "CABIN5":
					Properties.Settings.Default["CABIN5"] = iparam;
					break;
				case "CABIN6":
					Properties.Settings.Default["CABIN6"] = iparam;
					break;
				case "CABIN7":
					Properties.Settings.Default["CABIN7"] = iparam;
					break;
				case "CABIN8":
					Properties.Settings.Default["CABIN8"] = iparam;
					break;
				case "COOP1_LIGHT":
					Properties.Settings.Default["COOP1_LIGHT"] = iparam;
					break;
				case "COOP1_HEATER":
					Properties.Settings.Default["COOP1_HEATER"] = iparam;
					break;
				case "COOP2_LIGHT":
					Properties.Settings.Default["COOP2_LIGHT"] = iparam;
					break;
				case "COOP2_HEATER":
					Properties.Settings.Default["COOP2_HEATER"] = iparam;
					break;
				case "WATER_HEATER":
					Properties.Settings.Default["WATER_HEATER"] = iparam;
					break;
				case "BATTERY_HEATER":
					Properties.Settings.Default["BATTERY_HEATER"] = iparam;
					break;
				case "WATER_PUMP":
					Properties.Settings.Default["WATER_PUMP"] = iparam;
					break;
				case "WATER_VALVE1":
					Properties.Settings.Default["WATER_VALVE1"] = iparam;
					break;
				case "WATER_VALVE2":
					Properties.Settings.Default["WATER_VALVE2"] = iparam;
					break;
				case "WATER_VALVE3":
					Properties.Settings.Default["WATER_VALVE3"] = iparam;
					break;
				case "OUTDOOR_LIGHT1":
					Properties.Settings.Default["OUTDOOR_LIGHT1"] = iparam;
					break;
				case "OUTDOOR_LIGHT2":
					Properties.Settings.Default["OUTDOOR_LIGHT2"] = iparam;
					break;
				default:
					break;
			}
			Properties.Settings.Default.Save();
			return iparam;
		}
		public bool Change_PortCmd(int msg, int index)
		{
			bool current_state = GetState(msg);
			current_state = !current_state;
			Send_ClCmd(msg, index, current_state);
			switch(GetName(msg))
			{
				case "DESK_LIGHT":
					Properties.Settings.Default["DESK_LIGHT"] = current_state;
					break;
				case "EAST_LIGHT":
					Properties.Settings.Default["EAST_LIGHT"] = current_state;
					break;
				case "NORTHWEST_LIGHT":
					Properties.Settings.Default["NORTHWEST_LIGHT"] = current_state;
					break;
				case "SOUTHEAST_LIGHT":
					Properties.Settings.Default["SOUTHEAST_LIGHT"] = current_state;
					break;
				case "MIDDLE_LIGHT":
					Properties.Settings.Default["MIDDLE_LIGHT"] = current_state;
					break;
				case "WEST_LIGHT":
					Properties.Settings.Default["WEST_LIGHT"] = current_state;
					break;
				case "NORTHEAST_LIGHT":
					Properties.Settings.Default["NORTHEAST_LIGHT"] = current_state;
					break;
				case "SOUTHWEST_LIGHT":
					Properties.Settings.Default["SOUTHWEST_LIGHT"] = current_state;
					break;
				case "BENCH_24V_1":
					Properties.Settings.Default["BENCH_24V_1"] = current_state;
					break;
				case "BENCH_24V_2":
					Properties.Settings.Default["BENCH_24V_2"] = current_state;
					break;
				case "BENCH_12V_1":
					Properties.Settings.Default["BENCH_12V_1"] = current_state;
					break;
				case "BENCH_12V_2":
					Properties.Settings.Default["BENCH_12V_2"] = current_state;
					break;
				case "BENCH_5V_1":
					Properties.Settings.Default["BENCH_5V_1"] = current_state;
					break;
				case "BENCH_5V_2":
					Properties.Settings.Default["BENCH_5V_2"] = current_state;
					break;
				case "BENCH_3V3_1":
					Properties.Settings.Default["BENCH_3V3_1"] = current_state;
					break;
				case "BENCH_3V3_2":
					Properties.Settings.Default["BENCH_3V3_2"] = current_state;
					break;
				case "BENCH_LIGHT1":
					Properties.Settings.Default["BENCH_LIGHT1"] = current_state;
					break;
				case "BENCH_LIGHT2":
					Properties.Settings.Default["BENCH_LIGHT2"] = current_state;
					break;
				case "CHICK_WATER":
					Properties.Settings.Default["CHICK_WATER"] = current_state;
					break;
				case "CABIN1":
					Properties.Settings.Default["CABIN1"] = current_state;
					break;
				case "CABIN2":
					Properties.Settings.Default["CABIN2"] = current_state;
					break;
				case "CABIN3":
					Properties.Settings.Default["CABIN3"] = current_state;
					break;
				case "CABIN4":
					Properties.Settings.Default["CABIN4"] = current_state;
					break;
				case "CABIN5":
					Properties.Settings.Default["CABIN5"] = current_state;
					break;
				case "CABIN6":
					Properties.Settings.Default["CABIN6"] = current_state;
					break;
				case "CABIN7":
					Properties.Settings.Default["CABIN7"] = current_state;
					break;
				case "CABIN8":
					Properties.Settings.Default["CABIN8"] = current_state;
					break;
				case "COOP1_LIGHT":
					Properties.Settings.Default["COOP1_LIGHT"] = current_state;
					break;
				case "COOP1_HEATER":
					Properties.Settings.Default["COOP1_HEATER"] = current_state;
					break;
				case "COOP2_LIGHT":
					Properties.Settings.Default["COOP2_LIGHT"] = current_state;
					break;
				case "COOP2_HEATER":
					Properties.Settings.Default["COOP2_HEATER"] = current_state;
					break;
				case "WATER_HEATER":
					Properties.Settings.Default["WATER_HEATER"] = current_state;
					break;
				case "BATTERY_HEATER":
					Properties.Settings.Default["BATTERY_HEATER"] = current_state;
					break;
				case "WATER_PUMP":
					Properties.Settings.Default["WATER_PUMP"] = current_state;
					break;
				case "WATER_VALVE1":
					Properties.Settings.Default["WATER_VALVE1"] = current_state;
					break;
				case "WATER_VALVE2":
					Properties.Settings.Default["WATER_VALVE2"] = current_state;
					break;
				case "WATER_VALVE3":
					Properties.Settings.Default["WATER_VALVE3"] = current_state;
					break;
				case "OUTDOOR_LIGHT1":
					Properties.Settings.Default["OUTDOOR_LIGHT1"] = current_state;
					break;
				case "OUTDOOR_LIGHT2":
					Properties.Settings.Default["OUTDOOR_LIGHT2"] = current_state;
					break;
				default:
					break;
			}
			Properties.Settings.Default.Save();
			return current_state;
		}
		public bool GetState(int msg)
		{
			bool current_state = false;
			switch(GetName(msg))
			{
				case "DESK_LIGHT":
					current_state = (bool)Properties.Settings.Default["DESK_LIGHT"];
					break;
				case "EAST_LIGHT":
					current_state = (bool)Properties.Settings.Default["EAST_LIGHT"];
					break;
				case "NORTHWEST_LIGHT":
					current_state = (bool)Properties.Settings.Default["NORTHWEST_LIGHT"];
					break;
				case "SOUTHEAST_LIGHT":
					current_state = (bool)Properties.Settings.Default["SOUTHEAST_LIGHT"];
					break;
				case "MIDDLE_LIGHT":
					current_state = (bool)Properties.Settings.Default["MIDDLE_LIGHT"];
					break;
				case "WEST_LIGHT":
					current_state = (bool)Properties.Settings.Default["WEST_LIGHT"];
					break;
				case "NORTHEAST_LIGHT":
					current_state = (bool)Properties.Settings.Default["NORTHEAST_LIGHT"];
					break;
				case "SOUTHWEST_LIGHT":
					current_state = (bool)Properties.Settings.Default["SOUTHWEST_LIGHT"];
					break;
				case "BENCH_24V_1":
					current_state = (bool)Properties.Settings.Default["BENCH_24V_1"];
					break;
				case "BENCH_24V_2":
					current_state = (bool)Properties.Settings.Default["BENCH_24V_2"];
					break;
				case "BENCH_12V_1":
					current_state = (bool)Properties.Settings.Default["BENCH_12V_1"];
					break;
				case "BENCH_12V_2":
					current_state = (bool)Properties.Settings.Default["BENCH_12V_2"];
					break;
				case "BENCH_5V_1":
					current_state = (bool)Properties.Settings.Default["BENCH_5V_1"];
					break;
				case "BENCH_5V_2":
					current_state = (bool)Properties.Settings.Default["BENCH_5V_2"];
					break;
				case "BENCH_3V3_1":
					current_state = (bool)Properties.Settings.Default["BENCH_3V3_1"];
					break;
				case "BENCH_3V3_2":
					current_state = (bool)Properties.Settings.Default["BENCH_3V3_2"];
					break;
				case "BENCH_LIGHT1":
					current_state = (bool)Properties.Settings.Default["BENCH_LIGHT1"];
					break;
				case "BENCH_LIGHT2":
					current_state = (bool)Properties.Settings.Default["BENCH_LIGHT2"];
					break;
				case "CHICK_WATER":
					current_state = (bool)Properties.Settings.Default["CHICK_WATER"];
					break;
				case "CABIN1":
					current_state = (bool)Properties.Settings.Default["CABIN1"];
					break;
				case "CABIN2":
					current_state = (bool)Properties.Settings.Default["CABIN2"];
					break;
				case "CABIN3":
					current_state = (bool)Properties.Settings.Default["CABIN3"];
					break;
				case "CABIN4":
					current_state = (bool)Properties.Settings.Default["CABIN4"];
					break;
				case "CABIN5":
					current_state = (bool)Properties.Settings.Default["CABIN5"];
					break;
				case "CABIN6":
					current_state = (bool)Properties.Settings.Default["CABIN6"];
					break;
				case "CABIN7":
					current_state = (bool)Properties.Settings.Default["CABIN7"];
					break;
				case "CABIN8":
					current_state = (bool)Properties.Settings.Default["CABIN8"];
					break;
					case "COOP1_LIGHT":
					current_state = (bool)Properties.Settings.Default["COOP1_LIGHT"];
					break;
				case "COOP1_HEATER":
					current_state = (bool)Properties.Settings.Default["COOP1_HEATER"];
					break;
				case "COOP2_LIGHT":
					current_state = (bool)Properties.Settings.Default["COOP2_LIGHT"];
					break;
				case "COOP2_HEATER":
					current_state = (bool)Properties.Settings.Default["COOP2_HEATER"];
					break;
				case "WATER_HEATER":
					current_state = (bool)Properties.Settings.Default["WATER_HEATER"];
					break;
				case "BATTERY_HEATER":
					current_state = (bool)Properties.Settings.Default["BATTERY_HEATER"];
					break;
				case "WATER_PUMP":
					current_state = (bool)Properties.Settings.Default["WATER_PUMP"];
					break;
				case "WATER_VALVE1":
					current_state = (bool)Properties.Settings.Default["WATER_VALVE1"];
					break;
				case "WATER_VALVE2":
					current_state = (bool)Properties.Settings.Default["WATER_VALVE2"];
					break;
				case "WATER_VALVE3":
					current_state = (bool)Properties.Settings.Default["WATER_VALVE3"];
					break;
				case "OUTDOOR_LIGHT1":
					current_state = (bool)Properties.Settings.Default["OUTDOOR_LIGHT1"];
					break;
				case "OUTDOOR_LIGHT2":
					current_state = (bool)Properties.Settings.Default["OUTDOOR_LIGHT2"];
					break;
				default:
					current_state = false;
					break;
			}
			return current_state;
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
			int temp = index;
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
		public void Send_ClCmd(int msg, int index, byte[] bytes)
		{
			// bytes should be send as 2x as what's needed
			var temp = index;
			byte[] atemp = BitConverter.GetBytes(temp);     // index
			byte[] dtemp = new byte[bytes.Count() * 2];
			byte[] ctemp = new byte[dtemp.Count() + atemp.Count() + 2];
			int j = 0;
			for (int i = 0; i < dtemp.Length - 1; i += 2)
			{
				dtemp[i] = bytes[j++];
			}
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
