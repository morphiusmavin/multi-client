using System;
using System.Collections.Generic;
using System.Data;
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
using System.Collections;
using System.Xml.Serialization;
using System.Drawing;
using System.Timers;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace EpServerEngineSampleClient
{
    public partial class FrmSampleClient : Form, INetworkClientCallback
    {

        ConfigParams cfg_params = new ConfigParams();
        private DlgSetParams dlgsetparams = null;
        private bool valid_cfg = false;
        ServerCmds svrcmd = new ServerCmds();
        INetworkClient m_client = new IocpTcpClient();

        //private PlayerDlg playdlg = null;
        private GarageForm garageform = null;
        private TestBench testbench = null;
        private Cabin cabin = null;
        private WinCLMsg winclmsg = null;
        private ClientDest clientdest = null;
        private SetNextClient setnextclient = null;
        private int AvailClientCurrentSection = 0;
        private bool clients_inited = false;
        private bool[] status = new bool[8];
        private List<ClientParams> client_params;
        private List<ClientsAvail> clients_avail;
        private List<Sunrise_lines> sunrise_lines;
        private List<Sunrise_sunset> sunrise_sunsets;
        private int i = 0;
        private int selected_address = 0;
        private int disconnect_attempts = 0;
        private string m_hostname;
        private string m_portno;
        private int server_up_seconds = 0;
        private bool client_connected = false;
        private int timer_offset;
        private string sendmsgtext;
        int tick = 0;
        int connected_tick = 0;
        int which_winclient = -1;
        Int64 alarm_tick = 0;
        Boolean midnight_flag = false;
        string[] sunrises;
        string[] sunsets;
        int sunrise_hour;
        int sunrise_minutes;
        int sunset_hour;
        int sunset_minutes;
        bool oneoff = true;
        bool oneoff2 = true;
        bool oneoff3 = true;
        bool oneoff4 = true;

        private string xml_dialog1_location = "c:\\Users\\daniel\\dev\\uiformat1.xml";
        private string xml_dialog2_location = "c:\\Users\\daniel\\dev\\uiformat2.xml";
        private string xml_dialog3_location = "c:\\Users\\daniel\\dev\\uiformat3.xml";
        private string xml_dialog4_location = "c:\\Users\\daniel\\dev\\uiformat4.xml";
        private string xml_dialog5_location = "c:\\Users\\daniel\\dev\\uiformat5.xml";
        private string xml_dialog6_location = "c:\\Users\\daniel\\dev\\uiformat6.xml";
        private string xml_params_location = "c:\\Users\\daniel\\dev\\ClientParams.xml";
        private string xml_clients_avail_location = "c:\\Users\\daniel\\dev\\ClientsAvail.xml";
        private string sunrisesunset_location = "c:\\Users\\daniel\\sunrisesunset_Nov.txt";
        private int hour;
        private int minute;
        private DateTime now;
		string sunrise, sunset;

        /* remove the min/max/close buttons in the 'frame' */
        /* or you can just set 'Control Box' to false in the properties pane for the form */
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private const int WS_CAPTION = 0x00C00000;

        // Removes the close button in the caption bar
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;

                // This disables the close button
                // myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;

                // this appears to completely remove the close button
                myCp.Style &= WS_CAPTION;

                return myCp;
            }
        }

        public FrmSampleClient()
        {
            InitializeComponent();
            svrcmd.SetClient(m_client);
            dlgsetparams = new DlgSetParams(cfg_params);
            dlgsetparams.SetClient(m_client);
            cbIPAdress.Enabled = true;
            tbReceived.Enabled = true;
            tbPort.Enabled = true;
            string sunrise_list = "c:\\users\\daniel\\sunrises.txt";
            string sunset_list = "c:\\users\\daniel\\sunsets.txt";
            for (int i = 0; i < 8; i++)
            {
                status[i] = false;
            }
            tbReceived.Clear();
            cbWhichWinClient.SelectedIndex = 0;
            client_params = new List<ClientParams>();
            ClientParams item = null;
            DataSet ds = new DataSet();
            //XmlReader xmlFile = XmlReader.Create(File.Exists(xml_file2_location_laptop) ? xml_file2_location_laptop : xml_file2_location_desktop);
            XmlReader xmlFile = XmlReader.Create(xml_params_location);
            ds.ReadXml(xmlFile);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                item = new ClientParams();
                item.AutoConn = Convert.ToBoolean(dr.ItemArray[0]);
                item.IPAdress = dr.ItemArray[1].ToString();
                item.PortNo = Convert.ToUInt16(dr.ItemArray[2]);
                item.Primary = Convert.ToBoolean(dr.ItemArray[3]);
                item.AttemptsToConnect = Convert.ToInt16(dr.ItemArray[4]);
                //AddMsg(item.IPAdress + " " + item.PortNo.ToString());
                client_params.Add(item);
                item = null;
            }
           
            clients_avail = new List<ClientsAvail>();
            ClientsAvail item3 = null;
            //AddMsg("adding clients avail...");
            DataSet ds2 = new DataSet();
            //XmlReader xmlFile = XmlReader.Create(File.Exists(xml_file2_location_laptop) ? xml_file2_location_laptop : xml_file2_location_desktop);
            xmlFile = XmlReader.Create(xml_clients_avail_location);
            ds2.ReadXml(xmlFile);
            int lb_index = 0;
            garageform = new GarageForm("c:\\users\\daniel\\dev\\adc_list.xml", m_client);
            testbench = new TestBench("c:\\users\\daniel\\dev\\adc_list.xml", m_client);
            cabin = new Cabin(m_client);
            btnGarageForm.Enabled = false;
            btnTestBench.Enabled = false;
            btnCabin.Enabled = false;
            btnFnc1.Enabled = false;
            btnFnc2.Enabled = false;
            btnFnc3.Enabled = false;

            foreach (DataRow dr in ds2.Tables[0].Rows)
            {
                //string temp = "";
                item3 = new ClientsAvail();
                item3.lbindex = -1;
                //                item2.index = Convert.ToInt16(dr.ItemArray[1]);
                item3.index = lb_index;
                item3.ip_addr = dr.ItemArray[0].ToString();
                //temp += item2.ip_addr;
                //temp += "  ";
                item3.label = dr.ItemArray[1].ToString();
                //temp += item2.label;
                //temp += "  ";
                item3.socket = Convert.ToInt16(dr.ItemArray[2]);
                //temp += item2.socket.ToString();
                item3.type = Convert.ToInt16(dr.ItemArray[3]);  // type is: 0 - win client; 1 - TS_CLIENT; 2 - TS_SERVER (only one of these)
                                                                //AddMsg(item2.label.ToString() + " " + item2.ip_addr.ToString() + " " + item2.socket.ToString());
                clients_avail.Add(item3);
                item3 = null;
                lb_index++;
            }

            bool found = false;

            for (int i = 0; i < client_params.Count(); i++)
            {
                //                if (client_params[i].AutoConn == true)
                if (client_params[i].Primary)
                {
                    m_hostname = cbIPAdress.Text = client_params[i].IPAdress;
                    selected_address = i;
                    m_portno = tbPort.Text = client_params[i].PortNo.ToString();
                    //AddMsg("primary: " + m_hostname + " " + m_portno.ToLower());
                    found = true;
                }
                cbIPAdress.Items.Add(client_params[i].IPAdress);
                //AddMsg("selected address: " + selected_address.ToString() + " selected address: " + selected_address.ToString());
                found = true;
            }

            if (!found)
            {
                AddMsg("no primary address found in xml file");
            }

            sunrises = new string[32];
            sunsets = new string[32];
            now = DateTime.Now;
            string t2date = now.Date.ToString();
            
            int space = t2date.IndexOf(" ");
            t2date = t2date.Remove(space);
            tbTodaysDate.Text = t2date;
            sunrise_sunsets = new List<Sunrise_sunset>();
            for (int i = 0; i < 31; i++)
                sunrise_sunsets.Add(new Sunrise_sunset());
            AddMsg("count: " + sunrise_sunsets.Count().ToString());
            sunrise_lines = new List<Sunrise_lines>();
            int j = 0;
            Sunrise_lines temp2;

            if (File.Exists(sunrisesunset_location))
			{
                string[] lines = File.ReadAllLines(sunrisesunset_location);
                //AddMsg(lines.Length.ToString());
                int no_days = 0;
             
				foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        char first = line[0];
                        if (first != ' ' && first != '\t')
                        {
                            if (first > 47 && first < 58)
                                no_days = int.Parse(line);
                            else
                            {
                                temp2 = new Sunrise_lines();
                                temp2.date = no_days;
                                temp2.line = line;
                                temp2.index = i;
                                //AddMsg(temp2.date.ToString() + " " + temp2.line + " " + temp2.index.ToString());
                                sunrise_lines.Add(temp2);
                                temp2 = null;
                                i++;
                            }
                        }
                    }
				}
              
                //AddMsg("no days: " + no_days.ToString());

                foreach (Sunrise_lines srl in sunrise_lines)
                {
					if (srl.line.Contains("Twi A: "))
					{
						if (srl.line.Contains("pm"))
						{
							sunrise_sunsets[srl.date-1].AstTwiEnd = srl.line;
						}
						else sunrise_sunsets[srl.date-1].AstTwiStart = srl.line;
					}
					else if (srl.line.Contains("Twi N: "))
					{
						if (srl.line.Contains("pm"))
						{
							sunrise_sunsets[srl.date-1].NautTwiEnd = srl.line;
						}
						else sunrise_sunsets[srl.date-1].NautTwiStart = srl.line;
					}
					else if (srl.line.Contains("Twi: "))
					{
						if (srl.line.Contains("pm"))
						{
							sunrise_sunsets[srl.date-1].CivilTwiEnd = srl.line;
						}
						else sunrise_sunsets[srl.date-1].CivilTwiStart = srl.line;
					}
					else if (srl.line.Contains("Sunrise: "))
					{
						sunrise_sunsets[srl.date-1].sunrise = srl.line;
					}
					else if (srl.line.Contains("Moonrise: "))
					{
						sunrise_sunsets[srl.date-1].moonrise = srl.line;
					}
					else if (srl.line.Contains("Moonset: "))
					{
						sunrise_sunsets[srl.date-1].moonset = srl.line;
					}
					else if (srl.line.Contains("Sunset: "))
					{
						sunrise_sunsets[srl.date-1].sunset = srl.line;
					}
                }
            }
            i = 1;
            //foreach (Sunrise_sunset srss in sunrise_sunsets)
                //AddMsg(i++.ToString() + "  " + srss.sunrise + "  " + srss.sunset + "  " + srss.AstTwiStart);

            if (File.Exists(sunrise_list))
            {
                // Read a text file line by line.  
                string[] lines = File.ReadAllLines(sunrise_list);
                int i = 0;
                foreach (string line in lines)
                {
                    //AddMsg(line);
                    sunrises[i++] = line;
                }
				calc_sunrise();
            }
            if (File.Exists(sunset_list))
            {
                // Read entire text file content in one string    
                string[] lines3 = File.ReadAllLines(sunset_list);
                int i = 0;
                foreach (string line in lines3)
                {
                    //AddMsg(line);
                    sunsets[i++] = line;
                }
				calc_sunset();
            }
        }

		private void calc_sunset()
		{
			sunset = sunsets[now.Day - 1].ToString();
			int space = sunset.IndexOf(" ");
			sunset = sunset.Remove(space);
			tbSunset.Text = sunset;
            string ts = sunset.Substring(0, 1);
            //AddMsg(ts2);
            sunset_hour = int.Parse(ts);
            sunset_hour += 12;
            AddMsg("sunset:" + sunset_hour.ToString());
            ts = sunset.Substring(2, 2);
            sunset_minutes = int.Parse(ts);
            AddMsg("sunset: " + sunset_minutes.ToString());
        }

        private void calc_sunrise()
		{
			sunrise = sunrises[now.Day - 1].ToString();
            //AddMsg(sunrise);
			int space = sunrise.IndexOf(" ");
			sunrise = sunrise.Remove(space);
			tbSunrise.Text = sunrise;
			string ts = sunrise.Substring(0, 1);
			//AddMsg(ts);
			sunrise_hour = int.Parse(ts);
			//AddMsg(sunrise_hour.ToString());
			ts = sunrise.Substring(2, 2);
			sunrise_minutes = int.Parse(ts);
			//AddMsg(sunrise_minutes.ToString());
		}

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (which_winclient > -1)
            {
                /*
                if (which_winclient == 0)
                    playdlg = new PlayerDlg("g:\\rock\\wavefiles", m_client);
                else if (which_winclient == 1)
                    playdlg = new PlayerDlg("c:\\Users\\daniel\\Music\\WavFiles", m_client);
                */
                if (which_winclient > 0)    // only Second_Windows does the set time in the timer callback
                    clients_inited = true;
                if (!client_connected)      // let's connect here! (see timer callback at end of file)
                {
                    m_hostname = cbIPAdress.Items[selected_address].ToString();
                    m_portno = tbPort.Text;
                    AddMsg("trying to connect to:    " + m_hostname + ":" + m_portno.ToString() + "...");
                    ClientOps ops = new ClientOps(this, m_hostname, m_portno);
                    // set the timeout to 5ms - by default it's 0 which causes it to wait a long time
                    // and slows down the UI
                    ops.ConnectionTimeOut = 500;
                    m_client.Connect(ops);
                    disconnect_attempts++;
                    timer1.Enabled = true;
                    tick = 0;
                    btnConnect.Text = "Disconnect";
                    client_connected = true;
                    connected_tick = 0;
                    //AddMsg(GetLocalIPAddress());
                }
                else
                {
                    //playdlg.Dispose();
                    svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("DISCONNECT"), 8, " ");
                    disconnect_attempts = 0;
                    AddMsg("disconnecting");
                    btnConnect.Text = "Connect";
                    timer1.Enabled = false;
                    client_connected = false;
                    connect_buttons(false);
                    m_client.Disconnect();
                }
            }
            else AddMsg("chose which client this is");
        }
        public void OnConnected(INetworkClient client, ConnectStatus status)
        {
            //AddMsg(client.HostName);
            if (client.HostName == m_hostname)
            {
                if (m_client.IsConnectionAlive)
                {
                    tbConnected.Text = "connected";     // comment all these out in debug
                                                        //            cblistCommon.Enabled = true;      this one stays commneted out
                    btnConnect.Text = "Disconnect";
                    cbIPAdress.Enabled = false;     /// from here to MPH should be commented out when in debugger
					tbPort.Enabled = false;
                    btnRescan.Enabled = true;
                    tbServerTime.Text = "";
                    //AddMsg("server_up_seconds: " + server_up_seconds.ToString());
                    //btnShowParams.Enabled = valid_cfg;
                    clients_avail[8].socket = 1;        // 8 is _SERVER (this is bad!)
                    timer1.Enabled = true;
                    AddMsg("connected");
                    connect_buttons(true);
                }
            }
            else AddMsg(client.HostName);
        }
        private void connect_buttons(bool btnstate)
		{
            btnGarageForm.Enabled = btnstate;
            btnTestBench.Enabled = btnstate;
            btnCabin.Enabled = btnstate;
            btnFnc1.Enabled = btnstate;
            btnFnc2.Enabled = btnstate;
            btnFnc3.Enabled = btnstate;
        }
        public void OnDisconnect(INetworkClient client)
        {
            if (client.HostName == m_hostname)
            {
                cbIPAdress.Enabled = true;
                tbPort.Enabled = true;
                btnConnect.Text = "Connect";
                tbConnected.Text = "not connected";
                //btnShutdown.Enabled = false;
                btnGarageForm.Enabled = false;
                connect_buttons(false);
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            AddMsg("closing...");
            if (m_client.IsConnectionAlive)
            {
            }
            //play_aliens_clip();
            garageform.Dispose();
            testbench.Dispose();
            //winclmsg.Dispose();

            //setnextclient.Dispose();
            //            if(player_active)
            //              playdlg.Dispose();
            base.OnClosed(e);
        }
        public void OnReceived(INetworkClient client, Packet receivedPacket)
        {
            // anything that gets sent here gets sent to home server if it's up
            /*
                        if (player_active && playdlg.Visible == true)
                        {
                            playdlg.Process_Msg(receivedPacket.PacketRaw);
                        }

                        if (slist.Visible == true)
                        {
                            slist.Process_Msg(receivedPacket.PacketRaw);
                        }
            */
            if (garageform.Visible == true)
            {
                garageform.Process_Msg(receivedPacket.PacketRaw);
            }
            else if (testbench.Visible == true)
            {
                testbench.Process_Msg(receivedPacket.PacketRaw);
            }
            else
                Process_Msg(receivedPacket.PacketRaw);
        }
        private void RedrawClientListBox()
        {
            lbAvailClients.Items.Clear();
            int i = 0;
            //AddMsg("redraw: ");
            foreach (ClientsAvail j in clients_avail)
            {
                //AddMsg(j.ip_addr + " " + j.label + " " + j.socket.ToString() + " " + j.type);
                if (j.socket > 0 && j.type != 0)
                {
                    string temp = j.label + " " + j.ip_addr + " " + j.socket.ToString();
                    lbAvailClients.Items.Add(temp);
                    j.lbindex = i;
                    i++;
                }
                else j.lbindex = -1;
            }
        }
        private void Process_Msg(byte[] bytes)
        {
            string substr;
            int type_msg;
            string ret = null;
            int i = 0;

            char[] chars = new char[bytes.Length / sizeof(char) + 2];
            char[] chars2 = new char[bytes.Length / sizeof(char)];
            // src srcoffset dest destoffset len
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            type_msg = chars[0];
            System.Buffer.BlockCopy(bytes, 2, chars2, 0, bytes.Length - 2);
            ret = new string(chars2);

            //            string str = Enum.GetName(typeof(msg_types), type_msg);
            string str = svrcmd.GetName(type_msg);
            //AddMsg(ret + " " + str + " " + type_msg.ToString() + bytes.Length.ToString());

            switch (str)
            {
                /*
                case "AREYOUTHERE":
                    tbServerTime.Text = ret;
                    svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("YESIMHERE"), 8, ret);
                    connected_tick = 0;
                    break;
                */
                case "YESIMHERE":
                    //AddMsg(ret);
                    AddMsg("yes im here");
                    break;

                case "UPTIME_MSG":
                    string[] words = ret.Split(' ');
                    i = 0;
                    int j = 0;
                    foreach (var word in words)
                    {
                        switch (i)
                        {
                            case 0:
                                j = int.Parse(word);
                                //AddMsg(word + " " + j.ToString());
                                AddMsg(clients_avail[j].label + " uptime:");
                                //AddMsg(word);
                                break;
                            case 1:
                                j = int.Parse(word);
                                if (j > 0)
                                    AddMsg("days: " + j.ToString());
                                break;
                            case 2:
                                j = int.Parse(word);
                                if (j > 0)
                                    AddMsg("hours: " + j.ToString());
                                break;
                            case 3:
                                j = int.Parse(word);
                                AddMsg("minutes: " + j.ToString());
                                break;
                            case 4:
                                j = int.Parse(word);
                                AddMsg("seconds: " + j.ToString());
                                break;
                            default:
                                AddMsg("?");
                                break;
                        }
                        i++;
                    }//AddMsg("uptime_msg");
                    break;

                case "SEND_MESSAGE":
                    AddMsg("str: " + str + " " + str.Length.ToString());
                    AddMsg(ret + " " + str + " " + type_msg.ToString() + bytes.Length.ToString());
                    AddMsg(ret);
                    ListMsg(ret, false);
                    break;

                case "CURRENT_TIME":
                    ListMsg(ret, true);
                    break;

                case "SERVER_UPTIME":
                    substr = ret.Substring(0, 2);
                    server_up_seconds++;
                    if (substr == "0h")
                    {
                        substr = ret.Substring(3, ret.Length - 3);
                        tbServerTime.Text = substr;
                    }
                    else
                        tbServerTime.Text = ret;

                    if (server_up_seconds == 2)
                        SetTime(9);

                    if (client_params[selected_address].AutoConn == true && server_up_seconds == 4)
                    {
                        if (dlgsetparams == null)
                        {
                            AddMsg("newing dlgsetparams: " + cfg_params.engine_temp_limit.ToString());
                            dlgsetparams = new DlgSetParams(cfg_params);
                            dlgsetparams.SetClient(m_client);
                            dlgsetparams.SetParams(cfg_params);
                            // SET_PARAMS asks the server to load all the params from the config file
                            // and send them back to here via the SEND_CONFIG msg
                            timer_offset = svrcmd.GetCmdIndexI("SET_PARAMS");
                            svrcmd.Send_Cmd(timer_offset);
                            AddMsg(cfg_params.engine_temp_limit.ToString());
                            AddMsg("cfg_params in dlgsetparams set: " + dlgsetparams.GetSet());
                            btnFnc3.Enabled = true;
                        }
                    }
                    break;

                case "SEND_CLIENT_LIST":
                    words = ret.Split(' ');
                    i = 0;
                    j = 0;
                    int sock = -1;
                    //AddMsg(ret);
                    string clmsg = " ";
                    bool avail = false;
                    //AddMsg("SEND_CLIENT_LIST ");
                    foreach (var word in words)
                    {
                        switch (i)
                        {
                            case 0:     // index into clients_avail list
                                j = int.Parse(word);
                                //AddMsg(j.ToString());
                                clmsg = word + "  ";
                                break;
                            case 1:     // ip address
                                        //AddMsg(word);
                                clmsg += word + "  ";
                                break;
                            case 2:     // port no.
                                        //if(clients_avail[i].socket < 0)
                                        //avail = true;
                                sock = clients_avail[j].socket = int.Parse(word);
                                //AddMsg(clients_avail[j].socket.ToString());
                                clmsg += word + " " + sock.ToString();
                                //if(avail)
                                RedrawClientListBox();
                                //AddMsg(clmsg);
                                break;
                            default:
                                AddMsg("?");
                                break;
                        }
                        i++;
                    }
                    break;

                case "SEND_CONFIG":
                    string[] words2 = ret.Split(' ');
                    i = 0;
                    AddMsg("send config");
                    //AddMsg(ret);

                    foreach (var word in words2)
                    {
                        //                    temp = int.Parse(word);
                        switch (i)
                        {
                            case 0:
                                cfg_params.rpm_mph_update_rate = int.Parse(word);
                                break;
                            case 1:
                                cfg_params.FPGAXmitRate = int.Parse(word);
                                break;
                            case 2:
                                cfg_params.high_rev_limit = int.Parse(word);
                                AddMsg("hi rev: " + cfg_params.high_rev_limit.ToString());
                                break;
                            case 3:
                                cfg_params.low_rev_limit = int.Parse(word);
                                AddMsg("lo rev: " + cfg_params.low_rev_limit.ToString());
                                break;
                            case 4:
                                cfg_params.fan_on = int.Parse(word);
                                AddMsg("fan on: " + cfg_params.fan_on.ToString());
                                substr = dlgsetparams.get_temp_str(cfg_params.fan_on).ToString();
                                AddMsg("fan on: " + substr);
                                AddMsg("");
                                break;
                            case 5:
                                cfg_params.fan_off = int.Parse(word);
                                AddMsg("fan off: " + cfg_params.fan_off.ToString());
                                substr = dlgsetparams.get_temp_str(cfg_params.fan_off).ToString();
                                AddMsg("fan off: " + substr);
                                AddMsg("");
                                break;
                            case 6:
                                cfg_params.lights_on_value = int.Parse(word);
                                AddMsg("lights on value: " + cfg_params.lights_on_value.ToString());
                                //AddMsg("");
                                break;
                            case 7:
                                cfg_params.lights_off_value = int.Parse(word);
                                AddMsg("lights off value: " + cfg_params.lights_off_value.ToString());
                                //AddMsg("");
                                break;
                            case 8:
                                cfg_params.adc_rate = int.Parse(word);
                                AddMsg("adc_rate: " + cfg_params.adc_rate.ToString());
                                //AddMsg("");
                                break;
                            case 9:
                                cfg_params.rt_value_select = int.Parse(word);
                                AddMsg("rt_value_select: " + cfg_params.rt_value_select.ToString());
                                //AddMsg("");
                                break;
                            case 10:
                                cfg_params.lights_on_delay = int.Parse(word);
                                AddMsg("lights on delay: " + cfg_params.lights_on_delay.ToString());
                                break;
                            case 11:
                                cfg_params.engine_temp_limit = int.Parse(word);
                                AddMsg("temp limit: " + cfg_params.engine_temp_limit.ToString());
                                substr = dlgsetparams.get_temp_str(cfg_params.engine_temp_limit).ToString();
                                AddMsg("temp limit: " + substr);
                                AddMsg("");
                                break;
                            case 12:
                                cfg_params.battery_box_temp = int.Parse(word);
                                substr = dlgsetparams.get_temp_str(cfg_params.battery_box_temp).ToString();
                                AddMsg("battery box temp: " + substr);
                                AddMsg("");
                                break;
                            case 13:
                                cfg_params.test_bank = int.Parse(word);
                                AddMsg("test bank: " + cfg_params.test_bank.ToString());
                                break;
                            case 14:
                                cfg_params.password_timeout = int.Parse(word);
                                AddMsg("pswd timeout: " + cfg_params.password_timeout.ToString());
                                break;
                            case 15:
                                cfg_params.password_retries = int.Parse(word);
                                AddMsg("pswd retries: " + cfg_params.password_retries.ToString());
                                break;
                            case 16:
                                cfg_params.baudrate3 = int.Parse(word);
                                AddMsg("baudrate3: " + cfg_params.baudrate3.ToString());
                                break;
                            case 17:
                                AddMsg(word);
                                cfg_params.password = word;
                                AddMsg("password: " + cfg_params.password);
                                break;

                            default:
                                break;
                        }
                        //                        MessageBox.Show(int.Parse(word).ToString());
                        i++;
                    }
                    valid_cfg = true;
                    break;

                case "GET_TIME":
                    ListMsg(ret, true);
                    break;

                case "SEND_STATUS":
                    AddMsg(ret);
                    break;

                default:
                    break;
            }
        }
        byte[] BytesFromString(String str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        public void OnSent(INetworkClient client, SendStatus status, Packet sentPacket)
        {
            switch (status)
            {
                case SendStatus.SUCCESS:
                    Debug.WriteLine("SEND Success");
                    break;
                case SendStatus.FAIL_CONNECTION_CLOSING:
                    AddMsg(status.ToString());
                    Debug.WriteLine("SEND failed due to connection closing");
                    break;
                case SendStatus.FAIL_INVALID_PACKET:
                    AddMsg(status.ToString());
                    Debug.WriteLine("SEND failed due to invalid socket");
                    break;
                case SendStatus.FAIL_NOT_CONNECTED:
                    AddMsg(status.ToString());
                    Debug.WriteLine("SEND failed due to no connection");
                    break;
                case SendStatus.FAIL_SOCKET_ERROR:
                    AddMsg(status.ToString());
                    Debug.WriteLine("SEND Socket Error");
                    break;
            }
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
        String StringFromByteArr(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        private void connect(object sender, EventArgs e)
        {
            btnConnect_Click(sender, e);
        }
        // start/stop engine
        // Insert logic for processing found files here.
        private void UpdateClientInfo()
        {
            string msg = "UPDATE_CLIENT_INFO";
            int param = 1;
            int icmd = svrcmd.GetCmdIndexI(msg);
            foreach (ClientsAvail cl in clients_avail)
            {
                if (cl.type == 1 && cl.socket > 0)
                {
                    svrcmd.Send_ClCmd(icmd, cl.index, cl.index);
                }
            }
        }
        private void SendTimeup(int which)      // not used
        {
            string msg = "SEND_TIMEUP";
            int icmd = svrcmd.GetCmdIndexI(msg);
            foreach (ClientsAvail cl in clients_avail)
            {
                if (cl.type > 0 && cl.socket > 0 && cl.index == which)
                {
                    svrcmd.Send_ClCmd(icmd, cl.index, cl.index);
                    //AddMsg(icmd.ToString());
                }
            }
        }
        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }
        private void ClearScreen(object sender, EventArgs e)
        {
            tbReceived.Clear();
        }
        private void GarageFormClick(object sender, EventArgs e)
        {
            garageform.Enable_Dlg(true);
            //garageform.SetStatus(status);
            garageform.StartPosition = FormStartPosition.Manual;
            garageform.Location = new Point(100, 10);
            if (garageform.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
            garageform.Enable_Dlg(false);
            //status = garageform.GetStatus();
        }
        private void myTimerTick(object sender, EventArgs e)
        {
            now = DateTime.Now;
            tick++;
            connected_tick++;
   //         if ((tick % 15) == 0)
   //         {
			//	foreach (ClientsAvail cl in clients_avail)
			//	{
			//		svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("AREYOUTHERE"), cl.index, " ");
			//	}
			//}
            if ((tick % 5) == 0)
            {
                //AddMsg(tick.ToString());
                hour = now.Hour;
                minute = now.Minute;
                string tTime = now.TimeOfDay.ToString();
                tTime = tTime.Substring(0, 8);
                tbTime.Text = tTime;
                if (hour == sunrise_hour && minute == sunrise_minutes && oneoff)
                {
                    play_sunrise_clip();
                    oneoff = false;     // have to do this because the player is synchronous
                    AddMsg("play sunrise");
                    //tbSunrise.Text = "now";
                }
                if (hour == sunrise_hour && minute == sunrise_minutes - 3 && oneoff2)
                {
                    play_tone(0);
                    oneoff2 = false;
                }
                if (hour == sunrise_hour && minute == sunrise_minutes - 2 && oneoff3)
                {
                    play_tone(1);
                    oneoff3 = false;
                }
                if (hour == sunrise_hour && minute == sunrise_minutes - 1 && oneoff4)
                {
                    play_tone(2);
                    oneoff4 = false;
                }
            }
            if (cbAlarm.Checked == true)
            {
                alarm_tick--;
                tbAlarm.Text = alarm_tick.ToString();
                if (alarm_tick == 0)
                {
                    cbAlarm.Checked = false;
                    System.Media.SoundPlayer player;
                    string song = "c:\\users\\Daniel\\Music\\White Bird.wav";
                    player = new System.Media.SoundPlayer();
                    player.SoundLocation = song;
                    player.Play();
                    player.Dispose();
                }
            }
            if (tick == 3)
            {
                svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SEND_CLIENT_LIST"), 8, "test");
                AddMsg("send client list");
                RedrawClientListBox();

            }
            if (clients_inited == false && tick == 10)
            {
                AddMsg("set time");
                foreach (ClientsAvail cl in clients_avail)
                {
                    if ((cl.type == 1 || cl.type == 2) && cl.socket > 0)  // set the time on any server/clients in the active list
                    {
                        AddMsg(cl.label);
                        SetTime(cl.index);
                    }
                }
            }
            if (clients_inited == false && tick == 15)
            {
                UpdateClientInfo();
                clients_inited = true;
            }
            if (tick > 1600)
            {
                //value.TimeOfDay.Ticks == 0;
                if (hour == 0 && midnight_flag == false)
                {
                    //AddMsg("it is now midnight");
                    midnight_flag = true;
                    DateTime now2 = DateTime.Now;
                    string t2date = now2.Date.ToString();
                    int space = t2date.IndexOf(" ");
                    t2date = t2date.Remove(space);
                    tbTodaysDate.Text = t2date;
                }
                else if (hour > 0 && midnight_flag == true)
                {
                    midnight_flag = false;
                    calc_sunrise();
                    calc_sunset();
                    tbSunrise.Text = sunrise;
                    tbSunset.Text = sunset;
                    oneoff = oneoff2 = oneoff3 = oneoff4 = true;
                }
                tick = 36;
            }
        }
        void play_sunrise_clip()
        {
            //AddMsg("playing sunrise.wav...");
            System.Media.SoundPlayer player;
            string song = "c:\\users\\Daniel\\Music\\sunrise.wav";
            player = new System.Media.SoundPlayer();
            player.SoundLocation = song;
            player.Play();
            player.Dispose();
        }
        void play_tone(int which)
        {
            //AddMsg("playing sunrise.wav...");
            System.Media.SoundPlayer player;
            string song = "c:\\users\\Daniel\\Music\\";
            switch(which)
			{
                case 0:
                    song += "tone440.wav";
                    break;
                case 1:
                    song += "tone490.wav";
                    break;
                case 2:
                    song += "tone537.wav";
                    break;
                default:
                    song += "tone440.wav";
                    break;
			}
            player = new System.Media.SoundPlayer();
            player.SoundLocation = song;
            player.Play();
            player.Dispose();
        }
        private void IPAddressChanged(object sender, EventArgs e)
        {
            if (!client_connected)
            {
                selected_address = cbIPAdress.SelectedIndex;
                m_hostname = client_params[selected_address].IPAdress;
                tbPort.Text = m_portno = client_params[selected_address].PortNo.ToString();
                AddMsg(selected_address.ToString() + " " + m_hostname + " " + m_portno.ToString());
            }
        }
        private void FrmSampleClient_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
            AddMsg("loaded");
        }
        private void SetTime(int dest)
        {
            if (m_client.IsConnectionAlive)
            {
                DateTime localDate = DateTime.Now;
                String cultureName = "en-US";
                var culture = new CultureInfo(cultureName);
                AddMsg(localDate.ToString(culture));
                int temp1 = dest;
                byte[] bytes1 = BitConverter.GetBytes(temp1);
                byte[] bytes2 = BytesFromString(localDate.ToString(culture));
                byte[] bytes3 = new byte[bytes1.Count() + bytes2.Length + 2];
                System.Buffer.BlockCopy(bytes1, 0, bytes3, 2, bytes1.Count());
                System.Buffer.BlockCopy(bytes2, 0, bytes3, 4, bytes2.Length);
                string set_time = "SET_TIME";
                bytes3[0] = svrcmd.GetCmdIndexB(set_time);
                Packet packet = new Packet(bytes3, 0, bytes3.Count(), false);
                m_client.Send(packet);
            }
        }
        private void ListMsg(string msg, bool show_date)
        {
            string temp = "";
            DateTime localDate = DateTime.Now;
            String cultureName = "en-US";
            var culture = new CultureInfo(cultureName);
            temp = localDate.ToString(culture);
            if (show_date)
                AddMsg(msg + " " + temp);
            else
            {
                int index = temp.IndexOf(' ');
                //AddMsg(index.ToString());
                temp = temp.Substring(index);
                AddMsg(msg + " " + temp);
            }
        }
        private void AvailClientSelIndexChanged(object sender, EventArgs e)
        {
            AvailClientCurrentSection = lbAvailClients.SelectedIndex;
        }
        private void btnRebootClient_Click(object sender, EventArgs e)
        {
            SendClientMsg(svrcmd.GetCmdIndexI("REBOOT_IOBOX"), " ", true);
        }
        private void btnShutdownClient_Click(object sender, EventArgs e)
        {
            SendClientMsg(svrcmd.GetCmdIndexI("SHUTDOWN_IOBOX"), " ", true);
        }
        private void SendClientMsg(int msg, string param, bool remove)
        {
            foreach (ClientsAvail cl in clients_avail)
            {
                //AddMsg(cl.label + " " + cl.lbindex.ToString());
                if (lbAvailClients.SelectedIndex > -1 && cl.lbindex == lbAvailClients.SelectedIndex)
                {
                    //AddMsg("send msg: " + cl.label + " " + cl.index);
                    if (remove)
                    {
                        cl.lbindex = -1;
                        cl.socket = -1;
                    }
                    svrcmd.Send_ClCmd(msg, cl.index, param);
                    //AddMsg(cl.index.ToString());
                    // if cl.index == server then set disconnected flag

                    //if ((cl.index == 8) && (msg == REBOOT_IOBOX))
                    if (false)
                    {
                        btnConnect.Text = "Connect";
                        timer1.Enabled = false;
                        client_connected = false;
                    }
                    RedrawClientListBox();
                    if (!remove)
                    {
                        lbAvailClients.SetSelected(cl.lbindex, true);
                    }
                }
            }
        }
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            SendClientMsg(svrcmd.GetCmdIndexI("SEND_MESSAGE"), sendmsgtext, false);
        }
        private void bSetClientTime_Click(object sender, EventArgs e)
        {
            foreach (ClientsAvail cl in clients_avail)
            {
                if (lbAvailClients.SelectedIndex > -1 && cl.lbindex == lbAvailClients.SelectedIndex)
                {
                    AddMsg(cl.label);
                    SetTime(cl.index);
                }
            }
        }
        private void btnReportTimeUp_Click(object sender, EventArgs e)
        {
            foreach (ClientsAvail cl in clients_avail)
            {
                if (lbAvailClients.SelectedIndex > -1 && cl.lbindex == lbAvailClients.SelectedIndex)
                //  if(cl.socket > 0)   // to do all at once
                {
                    //AddMsg(cl.label + " " + cl.index.ToString() + " " + cl.lbindex.ToString());
                    svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SEND_TIMEUP"), cl.index, " ");
                }
            }
        }
        private void tbSendMsg_TextChanged(object sender, EventArgs e)
        {
            sendmsgtext = tbSendMsg.Text;
        }
        private void cbWhichWinClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            which_winclient = cbWhichWinClient.SelectedIndex;
            AddMsg(which_winclient.ToString());
        }
        private void btnWinClMsg_Click(object sender, EventArgs e)
        {
            winclmsg = new WinCLMsg();
            winclmsg.SetClient(m_client);
            winclmsg.Enable_Dlg(true);
            winclmsg.StartPosition = FormStartPosition.Manual;
            winclmsg.Location = new Point(100, 10);

            if (winclmsg.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
                //                this.txtResult.Text = "Cancelled";
            }
            winclmsg.Enable_Dlg(false);
            winclmsg.Dispose();
        }
        private void tbAlarm_TextChanged(object sender, EventArgs e)
        {
            alarm_tick = Int64.Parse(tbAlarm.Text);
            //AddMsg(alarm_tick.ToString());
        }
		private void btnTestBench_Click(object sender, EventArgs e)
		{
            testbench.Enable_Dlg(true);
            testbench.StartPosition = FormStartPosition.Manual;
            testbench.Location = new Point(100, 10);
            if (testbench.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
            testbench.Enable_Dlg(false);
        }
		private void Cabin_Click(object sender, EventArgs e)
		{
            cabin.StartPosition = FormStartPosition.Manual;
            cabin.Location = new Point(100, 10);
            if (cabin.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
        }
		private void btnTimers_Click(object sender, EventArgs e)
		{
            clientdest = new ClientDest();
            clientdest.SetClient(m_client);
            clientdest.StartPosition = FormStartPosition.Manual;
            clientdest.Location = new Point(100, 10);
            if (clientdest.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
            clientdest.Dispose();
        }
		private void btnSetNextClient_Click(object sender, EventArgs e)
		{
            setnextclient = new SetNextClient();
            setnextclient.SetClient(m_client);
            setnextclient.StartPosition = FormStartPosition.Manual;
            setnextclient.Location = new Point(100, 10);
            if (setnextclient.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
            setnextclient.Dispose();
        }
        private void BtnAssignFunction(object sender, EventArgs e)
        {
            int func = 0;          
            EasyButtonForm easyButton = new EasyButtonForm();
            easyButton.StartPosition = FormStartPosition.Manual;
            easyButton.Location = new Point(100, 0);
            if(easyButton.ShowDialog(this) == DialogResult.OK)
			{
                func = easyButton.getFunc();
                switch (func)
                {
                    case 1:
                        //AddMsg("func 1");
                        Properties.Settings.Default["func1_type"] = easyButton.getType();
                        Properties.Settings.Default["func1_port"] = easyButton.getPort();
                        btnFnc1.Text = easyButton.getType().ToString() + " " + easyButton.getPort().ToString();

                        break;
                    case 2:
                        //AddMsg("func 2");
                        Properties.Settings.Default["func2_type"] = easyButton.getType();
                        Properties.Settings.Default["func2_port"] = easyButton.getPort();
                        btnFnc2.Text = easyButton.getType().ToString() + " " + easyButton.getPort().ToString();
                        break;
                    case 3:
                        //AddMsg("func 2");
                        Properties.Settings.Default["func3_type"] = easyButton.getType();
                        Properties.Settings.Default["func3_port"] = easyButton.getPort();
                        btnFnc3.Text = easyButton.getType().ToString() + " " + easyButton.getPort().ToString();
                        break;
                    default:
                        break;
                }
                Properties.Settings.Default.Save();
                //AddMsg("type: " + easyButton.getType().ToString());
                //AddMsg("port: " + easyButton.getPort().ToString());
            }
            else
			{

			}
            easyButton.Dispose();
        }
        private void Function1Click(object sender, EventArgs e)
		{
            int type, port;
            //AddMsg(Properties.Settings.Default["func1_type"].ToString());
            //AddMsg(Properties.Settings.Default["func1_port"].ToString());
            type = (int)Properties.Settings.Default["func1_type"];
            port = (int)Properties.Settings.Default["func1_port"];
            svrcmd.Change_PortCmd(port, type);
        }
        private void Function2Click(object sender, EventArgs e)
		{
            int type, port;
            //AddMsg(Properties.Settings.Default["func2_type"].ToString());
            //AddMsg(Properties.Settings.Default["func2_port"].ToString());
            type = (int)Properties.Settings.Default["func2_type"];
            port = (int)Properties.Settings.Default["func2_port"];
            svrcmd.Change_PortCmd(port, type);
        }
        private void Function3Click(object sender, EventArgs e)
        {
            int type, port;
			//AddMsg(Properties.Settings.Default["func3_type"].ToString());
			//AddMsg(Properties.Settings.Default["func3_port"].ToString());
            type = (int)Properties.Settings.Default["func3_type"];
            port = (int)Properties.Settings.Default["func3_port"];
            svrcmd.Change_PortCmd(port, type);
        }
        private void Exit2Shell_Click(object sender, EventArgs e)
		{
            SendClientMsg(svrcmd.GetCmdIndexI("EXIT_TO_SHELL"), " ", true);
        }
		private void btnShellandRename_Click(object sender, EventArgs e)
		{
            SendClientMsg(svrcmd.GetCmdIndexI("SHELL_AND_RENAME"), " ", true);
        }
		private void btnSendStatus_Click(object sender, EventArgs e)
		{
            SendClientMsg(svrcmd.GetCmdIndexI("SEND_STATUS"), "status", false);
        }
		private void Minimize_Click(object sender, EventArgs e)
		{
            this.WindowState = FormWindowState.Minimized;
        }
		private void btnTest_Click(object sender, EventArgs e)
		{
            now = DateTime.Now;
            AddMsg("start " + sunrise_sunsets[now.Day - 1].AstTwiStart);
            AddMsg("start " + sunrise_sunsets[now.Day - 1].NautTwiStart);
            AddMsg("start " + sunrise_sunsets[now.Day - 1].CivilTwiStart);
            AddMsg(sunrise_sunsets[now.Day - 1].sunrise);
            AddMsg(sunrise_sunsets[now.Day - 1].sunset);
            AddMsg("end " + sunrise_sunsets[now.Day - 1].CivilTwiEnd);
            AddMsg("end " + sunrise_sunsets[now.Day - 1].NautTwiEnd);
            AddMsg("end " + sunrise_sunsets[now.Day - 1].AstTwiEnd);
            AddMsg(sunrise_sunsets[now.Day - 1].moonrise);
            AddMsg(sunrise_sunsets[now.Day - 1].moonset);
            /*
            Properties.Settings.Default["EAST_LIGHT"] = true;
            Properties.Settings.Default.Save();
            AddMsg(Properties.Settings.Default["EAST_LIGHT"].ToString());
            */
        }
        private void RescanClients_Click(object sender, EventArgs e)
		{
            svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SEND_CLIENT_LIST"), 8, "test");
            //SendClientMsg(svrcmd.GetCmdIndexI("SEND_CLIENT_LIST"), "send client list", true);
            AddMsg("send client list");
        }
		private void btnMngServer_Click(object sender, EventArgs e)
		{
            DialogResult res;
            int iResult = 0;
            ManageServer dlg = new ManageServer(m_client);
            res = dlg.ShowDialog(this);
            iResult = dlg.GetResult();
            if (res == DialogResult.OK)
            {
                if (client_params[selected_address].AutoConn == true)
                    cfg_params = dlgsetparams.GetParams();
            }
            else if (res == DialogResult.Abort)
            {
                AddMsg("closing connection and exiting " + client_connected.ToString());
                /*
                if (client_connected)
                    m_client.Disconnect();
                garageform.Dispose();
                testbench.Dispose();
                base.OnClosed(e);
                */
                if (client_connected)
                {
                    svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("DISCONNECT"), 8, " ");
                    disconnect_attempts = 0;
                    AddMsg("disconnecting");
                    btnConnect.Text = "Connect";
                    timer1.Enabled = false;
                    client_connected = false;
                    btnTestBench.Enabled = false;
                    btnGarageForm.Enabled = false;
                    //play_aliens_clip();
                    m_client.Disconnect();
                }
                garageform.Dispose();
                testbench.Dispose();
                this.Close();
            }
            if(iResult == 55)
                this.WindowState = FormWindowState.Minimized;
        }
    }
}