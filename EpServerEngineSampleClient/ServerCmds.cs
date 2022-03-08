﻿using System;
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
			GET_TEMP4,
			SHUTDOWN_IOBOX,
			REBOOT_IOBOX,
			SET_TIME,
			GET_TIME,
			DISCONNECT,
			BAD_MSG,
			CURRENT_TIME,
			SET_PARAMS,
			EXIT_PROGRAM,
			SERVER_UPTIME,
			SEND_CONFIG,
			NAV_UP,
			NAV_DOWN,
			NAV_SIDE,
			NAV_CLICK,
			NAV_CLOSE,
			SEND_STATUS,
			SERVER_UP,
			SERVER_DOWN,
			UPLOAD_NEW,
			UPLOAD_OTHER,
			UPLOAD_NEW_PARAM,
			GET_VERSION,
			UPDATE_CONFIG,
			SEND_CLIENT_LIST,
			GET_CONFIG2,
			CLIENT_CONNECTED,
			SERVER_CONNECTED,
			SET_KEYMODE,
			SHELL_AND_RENAME,
			TEST_IO_PORT,
			SEND_DEBUG_MSG,
			UPDATE_STATUS,
			UPDATE_ALL,
			SEND_MSG
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
			} while (cmd2 != cmd);
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
		public bool connection_alive()
		{
			return m_client.IsConnectionAlive;
		}
	}
}
