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
        private Outdoor outdoor = null;
        private TimerSchedule timer_schedule = null;
        private DS1620Mgt ds1620 = null;
        private WinCLMsg winclmsg = null;
        private ClientDest clientdest = null;
        private SetNextClient setnextclient = null;
        private int AvailClientCurrentSection = 0;
        private bool clients_inited = false;
        private bool[] status = new bool[8];
        private List<DS1620_conversions> ds1620_list;
        private List<ClientParams> client_params;
        private List<ClientsAvail> clients_avail;
       
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
        int alarm_hours, alarm_minutes, alarm_seconds;
        Int64 alarm_tick;
        bool client_alert = false;

        private string xml_params_location = "c:\\Users\\daniel\\ClientProgramData\\ClientParams.xml";
        private string xml_clients_avail_location = "c:\\Users\\daniel\\ClientProgramData\\ClientsAvail.xml";
        
        private int hour;
        private int minute;
        private int second;
        bool clk_oneoff = true;

        private DateTime now;
        /* remove the min/max/close buttons in the 'frame' */
        /* or you can just set 'Control Box' to false in the properties pane for the form */
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private const int WS_CAPTION = 0x00C00000;
        // Removes the close button in the caption bar
        string str_quote_of_the_day_file = @"C:\Users\Daniel\Documents\Quote of the Day.txt";
        string str_factoid_file = @"C:\Users\Daniel\Documents\Factoid_List.txt";
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
            timer1.Enabled = true;

            for (int i = 0; i < 8; i++)
            {
                status[i] = false;
            }
            tbReceived.Clear();
            cbWhichWinClient.SelectedIndex = 0;

           
            client_params = new List<ClientParams>();
            ClientParams item = null;
            if(!File.Exists(xml_params_location))
			{
                MessageBox.Show("can't find " + xml_params_location);
                return;
			}
            XmlReader xmlFile = XmlReader.Create(xml_params_location);
            DataSet ds = new DataSet();
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
            if(!File.Exists(xml_clients_avail_location))
			{
                MessageBox.Show("can't find " + xml_clients_avail_location);
                return;
			}
            xmlFile = XmlReader.Create(xml_clients_avail_location);
            ds2.ReadXml(xmlFile);
            int lb_index = 0;
            garageform = new GarageForm("c:\\users\\daniel\\dev\\adc_list.xml", m_client);
            testbench = new TestBench("c:\\users\\daniel\\dev\\adc_list.xml", m_client);
            cabin = new Cabin(m_client);
            outdoor = new Outdoor(m_client);
            timer_schedule = new TimerSchedule("c:\\users\\daniel\\dev\\cdata.xml", m_client);
            ds1620 = new DS1620Mgt(m_client);
            btnGarageForm.Enabled = false;
            btnTestBench.Enabled = false;
            btnCabin.Enabled = false;
            btnTimerSchedules.Enabled = false;
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
                item3.time_string = "";
                item3.flag = 0;
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
            now = DateTime.Now;
            string t2date = now.Date.ToString();
            //string smonth = DateTime.

            
            int space = t2date.IndexOf(" ");
            t2date = t2date.Remove(space);
            tbTodaysDate.Text = t2date;

            i = 0;
            int j = 0;
            i = 0;
            DS1620_conversions ds1 = null;
            ds1620_list = new List<DS1620_conversions>();
            string dslist_filename = @"C:\Users\Daniel\ClientProgramData\dslist.txt";
            String[] file = File.ReadAllLines(dslist_filename);
            // make sure not to change dslist.txt or this won't work
            for (i = 0; i < 360; i++)
            {
                ds1 = new DS1620_conversions();
                
                string[] words2 = file[i].Split(',');
                ds1.temp = words2[0];
                ds1.raw_value = int.Parse(words2[1]);
                ds1620_list.Add(ds1);
            }
            
            //foreach (DS1620_conversions d1 in ds1620_list)
            //AddMsg(d1.raw_value.ToString() + " " + d1.temp.ToString());

            // turn off east light because it is on by default (relay is wired nc)
            //svrcmd.Change_PortCmd(svrcmd.GetCmdIndexI("EAST_LIGHT"), 8);
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
                    //timer1.Enabled = true;
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
                    //timer1.Enabled = false;
                    client_connected = false;
                    connect_buttons(false);
                    m_client.Disconnect();
                    lbAvailClients.Items.Clear();
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
                    //tbServerTime.Text = "";
                    //AddMsg("server_up_seconds: " + server_up_seconds.ToString());
                    //btnShowParams.Enabled = valid_cfg;
                    clients_avail[8].socket = 1;        // 8 is _SERVER (this is bad!)
                    //timer1.Enabled = true;
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
            btnTimerSchedules.Enabled = btnstate;
            btnFnc1.Enabled = btnstate;
            btnFnc2.Enabled = btnstate;
            btnFnc3.Enabled = btnstate;
            btnFnc4.Enabled = btnstate;
            btnFnc5.Enabled = btnstate;
            btnOutdoor.Enabled = btnstate;
        }
        public void OnDisconnect(INetworkClient client)
        {
            if (client.HostName == m_hostname)
            {
                cbIPAdress.Enabled = true;
                tbPort.Enabled = true;
                btnConnect.Text = "Connect";
                tbConnected.Text = "not connected";
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
           
            if (garageform.Visible == true)
            {
                garageform.Process_Msg(receivedPacket.PacketRaw);
            }
            else if (testbench.Visible == true)
            {
                testbench.Process_Msg(receivedPacket.PacketRaw);
            }
            else if (timer_schedule.Visible == true)
            {
                timer_schedule.Process_Msg(receivedPacket.PacketRaw);
            }
            else
                Process_Msg(receivedPacket.PacketRaw);
        }
        private string lookup_DS1620(string val)
		{
            string ret = "N/A";
            foreach(var raw in ds1620_list)
			{
                if (val == raw.raw_value.ToString())
                {
                    ret = raw.temp;
                    return ret;     // if I don't return here it causes a huge memory leak
                }
			}
            return ret;
		}
        private void RedrawClientListBox()
        {
            lbAvailClients.Items.Clear();
            int i = 0;
            int k = 0;
            foreach (ClientsAvail j in clients_avail)
            {
                if (j.socket > 0 && j.type != 0)
                {
                    //string temp = j.label + " " + j.ip_addr + " " + j.socket.ToString();
                    string temp = j.label + "  " + j.time_string;
                 
                    lbAvailClients.Items.Add(temp);
                    j.lbindex = i;
                   
                    //AddMsg(j.ip_addr + " " + j.label + " " + j.socket.ToString() + " " + j.type);
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
            string[] words;
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
                case "DS1620_MSG":
                    //AddMsg(ret.ToString());
                    
                    words = ret.Split(' ');
                    i = 0;
                    foreach(var word in words)
					{
                        switch(i)
						{
                            case 0:
                                //AddMsg("client id: " + word);
                                break;
                            case 1:
                                //AddMsg("sensor: " + word);
                                break;
                            case 2:
                                AddMsg("val: " + lookup_DS1620(word));
                                //AddMsg("val: " + word);
                                break;
						}
                        i++;
					}
                    
                    break;

                case "UPTIME_MSG":
                    //AddMsg("ret: " + ret);
                    words = ret.Split(' ');
                    i = 0;
                    int j = 0;
                    int k = 0;
					int hours = 0;
                    foreach (var word in words)
                    {
                        switch (i)
                        {
                            case 0:
                                j = int.Parse(word);
                                //AddMsg(word + " " + j.ToString());
                                //AddMsg(clients_avail[j].label + " uptime:");
                                //AddMsg(word);
                                clients_avail[j].time_string = " ";
                                clients_avail[j].flag = 0;
                                k = j;
                                break;
                            case 1:
                                j = int.Parse(word);
                                if (j > 0)
                                {
                                    clients_avail[k].time_string += j.ToString() + " days ";
                                }
                                break;
                            case 2:
                                j = int.Parse(word);
								hours = j;
                                if (j > 0)
                                    clients_avail[k].time_string += j.ToString() + " hrs ";
                                break;
                            case 3:
                                j = int.Parse(word);
                                clients_avail[k].time_string += j.ToString() + " mins ";
                                break;
                            case 4:
                                j = int.Parse(word);
								if(hours == 0)
									clients_avail[k].time_string += j.ToString() + " secs";
                                //AddMsg(clients_avail[k].time_string + " " + clients_avail[k].prev_time_string);
                                if (clients_avail[k].time_string == clients_avail[k].prev_time_string)
                                {
                                    AddMsg("Alert 2: " + clients_avail[k].label);
                                }
                                clients_avail[k].prev_time_string = clients_avail[k].time_string;
                                RedrawClientListBox();
                                break;
                            default:
                                AddMsg("?");
                                break;
                        }
                        i++;
                    }//AddMsg("uptime_msg");
                    break;

                case "SEND_MESSAGE":
                    //AddMsg("str: " + str + " " + str.Length.ToString());
                    //AddMsg(ret + " " + str + " " + type_msg.ToString() + bytes.Length.ToString());
                    AddMsg("send msg: " + ret);
                    //ListMsg(ret, false);
                    break;

                case "CURRENT_TIME":
                    //ListMsg(ret, true);
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
                    //ListMsg(ret, true);
                    break;

                case "SEND_STATUS":
                    //AddMsg(ret);
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
        /*private void SendTimeup(int which)      // not used
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
        }*/
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
           
            if(true)
            //if ((tick % 3) == 0)
            {
                //AddMsg(tick.ToString());
                hour = now.Hour;
                minute = now.Minute;
                second = now.Second;
                //AddMsg(hour.ToString() + " " + minute.ToString() + " " + second.ToString());
                string tTime = now.TimeOfDay.ToString();
                tTime = tTime.Substring(0, 8);
                tbTime.Text = tTime;

               
                if (minute == 1 && clk_oneoff)
                {
                    clk_oneoff = false;
               
                    foreach (ClientsAvail cl in clients_avail)
                    {
                        if ((cl.type == 1 || cl.type == 2) && cl.socket > 0)  // set the time on any server/clients in the active list
                        {
                            svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("DLLIST_SAVE"), cl.index, "test");
                            //AddMsg(cl.label);
                        }
                    }
                }
                if (minute == 2)
                    clk_oneoff = true;

                else if (hour == 0 && minute == 0 && second == 0)
                {
                    DateTime now2 = DateTime.Now;
                    string t2date = now2.Date.ToString();
                    int space = t2date.IndexOf(" ");
                    t2date = t2date.Remove(space);
                    tbTodaysDate.Text = t2date;

                    //play_tone(9);
                    //AddMsg("midnight");
                }
                else if (hour == 0 && minute == 1 && second == 0)
                {
                    AddMsg("one minute after midnight");
                }
                else if (tick > 260 && second == 30)
                {
                    //AddMsg("test");
                    ReportAllTimeUp(0);
                }
                else if(client_alert && second % 10 == 0)
				{
                    System.Media.SoundPlayer player;
                    string song = "c:\\users\\Daniel\\Music\\alert.wav";
                    player = new System.Media.SoundPlayer();
                    player.SoundLocation = song;
                    player.Play();
                    player.Dispose();
                }
                /*
                else if((tick <= 120 && second % 5 == 0) || (tick > 120 && tick <= 240 && second == 0) || (tick > 240 && minute % 2 == 0 && second == 0))
                {
                    connected_tick++;
                    if (connected_tick >= lbAvailClients.Items.Count)
                        connected_tick = 0;
                    ReportAllTimeUp(connected_tick);
                }
                */
            }
            if (tick == 2)
            {
                if (m_client.IsConnectionAlive)
                {
                    svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SEND_CLIENT_LIST"), 8, "test");
                    //AddMsg("send client list");
                    RedrawClientListBox();
                }

            }
            if (clients_inited == false && tick == 3)
            {
                //AddMsg("set time");
                if (m_client.IsConnectionAlive)
                {
                    foreach (ClientsAvail cl in clients_avail)
                    {
                        if ((cl.type == 1 || cl.type == 2) && cl.socket > 0)  // set the time on any server/clients in the active list
                        {
                            //AddMsg(cl.label);
                            SetTime(cl.index);
                        }
                    }
                }
            }
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
            //AddMsg("loaded");
        }
        private void SetTime(int dest)
        {
            if (m_client.IsConnectionAlive)
            {
                DateTime localDate = DateTime.Now;
                String cultureName = "en-US";
                var culture = new CultureInfo(cultureName);
                //AddMsg(clients_avail[dest].label);
                //AddMsg(localDate.ToString(culture));
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
        private void SetTime(int dest,int hours, int minutes)
        {
            if (m_client.IsConnectionAlive)
            {
                DateTime localDate = DateTime.Now;
                localDate = localDate.AddHours(hours);
                localDate = localDate.AddMinutes(minutes);
                //AddMsg(localDate.ToString());
                
                String cultureName = "en-US";
                var culture = new CultureInfo(cultureName);
                //AddMsg(clients_avail[dest].label);
                //AddMsg(localDate.ToString(culture));
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
                        //timer1.Enabled = false;
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
            if (tbSendMsg.Text.Length == 0)
                sendmsgtext = "test asdf";
            SendClientMsg(svrcmd.GetCmdIndexI("SEND_MESSAGE"), sendmsgtext, false);
        }
        private void bSetClientTime_Click(object sender, EventArgs e)
        {
            foreach (ClientsAvail cl in clients_avail)
            {
                if (lbAvailClients.SelectedIndex > -1 && cl.lbindex == lbAvailClients.SelectedIndex)
                {
                    //AddMsg(cl.label);
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
                    //svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("GET_CONFIG2"), cl.index, " ");
                }
            }
        }
        private void ReportAllTimeUp(int index)
		{
            foreach (ClientsAvail cl in clients_avail)
            {
                //if(cl.socket > 0 && (cl.type == 2 || cl.type == 1) && cl.lbindex == index)
                if (cl.socket > 0 && cl.type == 1)
                {
                    //AddMsg("testing: " + cl.label + " " + cl.flag.ToString());
                    //svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SEND_TIMEUP"), cl.index, " ");
                    if (cl.flag > 1)
                    {
                        AddMsg("Alert: " + cl.label + " " + (cl.flag - 1).ToString());
                        AlertLabel.Visible = true;
                        AlertLabel.Text = "Alert: " + cl.label + " " + (cl.flag - 1).ToString();
                        AlertLabel.ForeColor = Color.Red;
                        client_alert = true;
                    }
                    cl.flag++;
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
        }       // unused
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
            //svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("RELOAD_CLLIST"), 2, " ");
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
                        //btnFnc1.Text = easyButton.getType().ToString() + " " + easyButton.getPort().ToString();
                        break;
                    case 2:
                        //AddMsg("func 2");
                        Properties.Settings.Default["func2_type"] = easyButton.getType();
                        Properties.Settings.Default["func2_port"] = easyButton.getPort();
                        //btnFnc2.Text = easyButton.getType().ToString() + " " + easyButton.getPort().ToString();
                        break;
                    case 3:
                        //AddMsg("func 2");
                        Properties.Settings.Default["func3_type"] = easyButton.getType();
                        Properties.Settings.Default["func3_port"] = easyButton.getPort();
                        //btnFnc3.Text = easyButton.getType().ToString() + " " + easyButton.getPort().ToString();
                        break;
                    case 4:
                        Properties.Settings.Default["func4_type"] = easyButton.getType();
                        Properties.Settings.Default["func4_port"] = easyButton.getPort();
                        //btnFnc3.Text = easyButton.getType().ToString() + " " + easyButton.getPort().ToString();
                        break;
                    case 5:
                        Properties.Settings.Default["func5_type"] = easyButton.getType();
                        Properties.Settings.Default["func5_port"] = easyButton.getPort();
                        //btnFnc3.Text = easyButton.getType().ToString() + " " + easyButton.getPort().ToString();
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
            type = (int)Properties.Settings.Default["func2_type"];
            port = (int)Properties.Settings.Default["func2_port"];
            svrcmd.Change_PortCmd(port, type);
        }
        private void Function3Click(object sender, EventArgs e)
        {
            int type, port;
            type = (int)Properties.Settings.Default["func3_type"];
            port = (int)Properties.Settings.Default["func3_port"];
            svrcmd.Change_PortCmd(port, type);
        }
        private void btnFnc4_Click(object sender, EventArgs e)
        {
            int type, port;
            type = (int)Properties.Settings.Default["func4_type"];
            port = (int)Properties.Settings.Default["func4_port"];
            svrcmd.Change_PortCmd(port, type);
        }
        private void btnFcn5_Click(object sender, EventArgs e)
        {
            int type, port;
            type = (int)Properties.Settings.Default["func5_type"];
            port = (int)Properties.Settings.Default["func5_port"];
            svrcmd.Change_PortCmd(port, type);
        }
        private void Exit2Shell_Click(object sender, EventArgs e)
		{
            SendClientMsg(svrcmd.GetCmdIndexI("EXIT_TO_SHELL"), " ", true);
            
            AlertLabel.Text = "";
            AlertLabel.Visible = false;
            client_alert = false;
        }
		private void btnSendStatus_Click(object sender, EventArgs e)
		{
            SendClientMsg(svrcmd.GetCmdIndexI("SEND_STATUS"), "status", false);
        }
		private void Minimize_Click(object sender, EventArgs e)
		{
            this.WindowState = FormWindowState.Minimized;
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
                    //timer1.Enabled = false;
                    client_connected = false;
                    btnTestBench.Enabled = false;
                    btnGarageForm.Enabled = false;
                    btnTimerSchedules.Enabled = false;
                    m_client.Disconnect();
                }
                garageform.Dispose();
                testbench.Dispose();
                timer_schedule.Dispose();
                this.Close();
            }
            if(iResult == 55)
                this.WindowState = FormWindowState.Minimized;
        }
        private void btnTimer_Click(object sender, EventArgs e)
		{
            timer_schedule.StartPosition = FormStartPosition.Manual;
            timer_schedule.Location = new Point(100, 10);
            if (timer_schedule.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
        }
		private void btnOutdoor_Click(object sender, EventArgs e)
		{
            outdoor.StartPosition = FormStartPosition.Manual;
            outdoor.Location = new Point(100, 10);
            if (outdoor.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
        }
        private void cbAlarm_CheckedChanged(object sender, EventArgs e)
		{
            if (cbAlarm.Checked)
            {
                if (tbAlarmHours.Text == "" || tbAlarmMinutes.Text == "" || tbAlarmSeconds.Text == "")
                {
                    AddMsg("alarm hours and minutes must be set");
                    timer2.Enabled = false;
                    return;
                }
                alarm_tick = (Int64)(alarm_hours * 3600 + alarm_minutes * 60 + alarm_seconds);
                AddMsg(alarm_seconds.ToString());
                timer2.Enabled = true;
            }
            else timer2.Enabled = false;
        }
		private void tbAlarmSecondsChanged(object sender, EventArgs e)
		{
            alarm_seconds = int.Parse(tbAlarmSeconds.Text);
            if (alarm_seconds > 60)
            {
                alarm_seconds = 60;
                tbAlarmSeconds.Text = "60";
            }
		}
        private void tbAlarm_TextChanged(object sender, EventArgs e)
        {
            alarm_hours = int.Parse(tbAlarmHours.Text);
        }
        private void btnGetTime_Click(object sender, EventArgs e)
        {
            foreach (ClientsAvail cl in clients_avail)
            {
                if (lbAvailClients.SelectedIndex > -1 && cl.lbindex == lbAvailClients.SelectedIndex)
                {
                    svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("GET_TIME"), cl.index, " ");
                }
            }
        }
		private void btnRescan_Click(object sender, EventArgs e)
		{
            ds1620.StartPosition = FormStartPosition.Manual;
            ds1620.Location = new Point(100, 10);
            if (ds1620.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
        }
		private void btnSendSort_Click(object sender, EventArgs e)
		{
            int dest = -1;
            foreach (ClientsAvail cl in clients_avail)
            {
                if (lbAvailClients.SelectedIndex > -1 && cl.lbindex == lbAvailClients.SelectedIndex)
                {
                    dest = cl.index;
                    svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SORT_CLLIST"), dest, "test");
                }
            }
        }
		private void timer3_tick(object sender, EventArgs e)
		{
            int dest = -1;
            foreach (ClientsAvail cl in clients_avail)
            {
                if (cl.socket > 0 && cl.type != 0)
                {
                    dest = cl.index;
                    SetTime(dest);
                }
            }
        }
		private void btnUnused_Click(object sender, EventArgs e)
		{
            
            AlertLabel.Text = "";
            AlertLabel.Visible = false;
            RedrawClientListBox();
            client_alert = false;
        }
		private void btnGetTemp_Click(object sender, EventArgs e)
		{
            foreach (ClientsAvail cl in clients_avail)
            {
                if (lbAvailClients.SelectedIndex > -1 && cl.lbindex == lbAvailClients.SelectedIndex)
                {
                    svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("GET_TEMP4"), cl.index, " ");
                }
            }
        }
		private void tbAlarmMinutes_TextChanged(object sender, EventArgs e)
        {
            alarm_minutes = int.Parse(tbAlarmMinutes.Text);
            if(alarm_minutes > 60)
			{
                alarm_minutes = 60;
                tbAlarmMinutes.Text = "60";
			}
        }
        private void timer2_Tick(object sender, EventArgs e)
		{
            alarm_tick--;
            tbAlarmTick.Text = alarm_tick.ToString();
            if (alarm_tick == 0)
            {
                cbAlarm.Checked = false;
                System.Media.SoundPlayer player;
                string song = "c:\\users\\Daniel\\Music\\White Bird.wav";
                player = new System.Media.SoundPlayer();
                player.SoundLocation = song;
                player.Play();
                player.Dispose();
                tbAlarmHours.Text = tbAlarmMinutes.Text = tbAlarmSeconds.Text = "0";
                timer2.Enabled = false;
            }
        }
    }
}