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
        private string password = "";

        private PlayerDlg playdlg = null;
        private GarageForm garageform = null;
        private TestBench testbench = null;
        private WinCLMsg winclmsg = null;
//        private BluetoothForm bluetoothform = null;
        private ClientDest clientdest = null;
        private SetNextClient setnextclient = null;
        private Child_Scrolling_List slist = null;
        private int AvailClientCurrentSection = 0;
        private bool player_active = false;
        private bool clients_inited = false;

        private List<ClientParams> client_params;
        private List<ClientsAvail> clients_avail;
        private int i = 0;
        private int selected_address = 0;
        private int please_lets_disconnect = 0;
        private int disconnect_attempts = 0;
        private string m_hostname;
        private string m_portno;
        private string m_hostname2;
        private string m_portno2;
        private int server_up_seconds = 0;
        private bool client_connected = false;
        private bool home_svr_connected = false;
        private int timer_offset;
        private string sendmsgtext;
        int tick = 0;
        int connected_tick = 0;
        int which_winclient = -1;
        Int64 alarm_tick = 0;

        private string xml_dialog1_location = "c:\\Users\\daniel\\dev\\uiformat1.xml";
        private string xml_dialog2_location = "c:\\Users\\daniel\\dev\\uiformat2.xml";
        private string xml_dialog3_location = "c:\\Users\\daniel\\dev\\uiformat3.xml";
        private string xml_dialog4_location = "c:\\Users\\daniel\\dev\\uiformat4.xml";
        private string xml_dialog5_location = "c:\\Users\\daniel\\dev\\uiformat5.xml";
        private string xml_dialog6_location = "c:\\Users\\daniel\\dev\\uiformat6.xml";
        private string xml_params_location = "c:\\Users\\daniel\\dev\\ClientParams.xml";
        private string xml_clients_avail_location = "c:\\Users\\daniel\\dev\\ClientsAvail.xml";

        public FrmSampleClient()
        {
            InitializeComponent();
            svrcmd.SetClient(m_client);
            dlgsetparams = new DlgSetParams(cfg_params);
            dlgsetparams.SetClient(m_client);
            cbIPAdress.Enabled = true;
            tbReceived.Enabled = true;
            tbPort.Enabled = true;
            btnShutdown.Enabled = false;
            //btnReboot.Enabled = false;
            btnShowParams.Enabled = false;
            btn_PlayList.Enabled = true;
            btnGetTime.Enabled = false;

            tbReceived.Clear();

            garageform = new GarageForm("c:\\users\\daniel\\dev\\adc_list.xml", m_client);
            testbench = new TestBench("c:\\users\\daniel\\dev\\adc_list.xml", m_client);
            winclmsg = new WinCLMsg();
            winclmsg.SetClient(m_client);
            //bluetoothform = new BluetoothForm("c:\\users\\daniel\\dev\\adc_list.xml");
            clientdest = new ClientDest();
            clientdest.SetClient(m_client);
            setnextclient = new SetNextClient();
            setnextclient.SetClient(m_client);
            cbWhichWinClient.SelectedIndex = 0;

            slist = new Child_Scrolling_List(m_client);
            slist.Enable_Dlg(false);

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
            ClientsAvail item2 = null;
            //AddMsg("adding clients avail...");
            DataSet ds2 = new DataSet();
            //XmlReader xmlFile = XmlReader.Create(File.Exists(xml_file2_location_laptop) ? xml_file2_location_laptop : xml_file2_location_desktop);
            xmlFile = XmlReader.Create(xml_clients_avail_location);
            ds2.ReadXml(xmlFile);
            int lb_index = 0;
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {
                //string temp = "";
                item2 = new ClientsAvail();
                item2.lbindex = -1;
//                item2.index = Convert.ToInt16(dr.ItemArray[1]);
                item2.index = lb_index;
                item2.ip_addr = dr.ItemArray[0].ToString();
                //temp += item2.ip_addr;
                //temp += "  ";
                item2.label = dr.ItemArray[1].ToString();
                //temp += item2.label;
                //temp += "  ";
                item2.socket = Convert.ToInt16(dr.ItemArray[2]);
                //temp += item2.socket.ToString();
                item2.type = Convert.ToInt16(dr.ItemArray[3]);  // type is: 0 - win client; 1 - TS_CLIENT; 2 - TS_SERVER (only one of these)
				//AddMsg(item2.label.ToString() + " " + item2.ip_addr.ToString() + " " + item2.socket.ToString());
                clients_avail.Add(item2);
                item2 = null;
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
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (which_winclient > -1)
            {
                if (which_winclient == 0)
                    playdlg = new PlayerDlg("g:\\rock\\wavefiles", m_client);
                else if (which_winclient == 1)
                    playdlg = new PlayerDlg("c:\\Users\\daniel\\Music\\WavFiles", m_client);
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
                    please_lets_disconnect = 0;
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
                    playdlg.Dispose();
                    svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("DISCONNECT"), 8, " ");
                    please_lets_disconnect = 1; // let's disconnect here!
                    disconnect_attempts = 0;
                    AddMsg("disconnecting");
                    btnConnect.Text = "Connect";
                    timer1.Enabled = false;
                    client_connected = false;
                    //play_aliens_clip();
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
                    btnGarageForm.Enabled = true;
                    cbIPAdress.Enabled = false;     /// from here to MPH should be commented out when in debugger
					tbPort.Enabled = false;

                    btnShutdown.Enabled = true;
                    btnRescan.Enabled = true;
                    tbServerTime.Text = "";

                    btn_PlayList.Enabled = true;
                    btnGetTime.Enabled = true;
                    //AddMsg("server_up_seconds: " + server_up_seconds.ToString());
                    //btnShowParams.Enabled = valid_cfg;
                    btnShowParams.Enabled = true;
                    clients_avail[8].socket = 1;        // 8 is _SERVER (this is bad!)
                    timer1.Enabled = true;
                    AddMsg("connected");
                }
            }
            else AddMsg(client.HostName);
        }
        public void OnDisconnect(INetworkClient client)
        {
            if (client.HostName == m_hostname)
            {
                cbIPAdress.Enabled = true;
                tbPort.Enabled = true;
                btnConnect.Text = "Connect";
                tbConnected.Text = "not connected";
                btnShutdown.Enabled = false;

                //AddMsg("disconnected 1");
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            AddMsg("closing...");
            if (m_client.IsConnectionAlive)
            {
                please_lets_disconnect = 1;
            }
            //play_aliens_clip();
            garageform.Dispose();
            testbench.Dispose();
            winclmsg.Dispose();
            //bluetoothform.Dispose();
            clientdest.Dispose();
            setnextclient.Dispose();
            if(player_active)
                playdlg.Dispose();
            base.OnClosed(e);
        }
        public void OnReceived(INetworkClient client, Packet receivedPacket)
        {
            // anything that gets sent here gets sent to home server if it's up
            if (player_active && playdlg.Visible == true)
            {
                playdlg.Process_Msg(receivedPacket.PacketRaw);
            }
            else if (slist.Visible == true)
            {
                slist.Process_Msg(receivedPacket.PacketRaw);
            }
            else if (garageform.Visible == true)
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
                case "AREYOUTHERE":
                    tbServerTime.Text = ret;
                    svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("YESIMHERE"), 8, ret);
                    connected_tick = 0;
                    break;

                case "UPTIME_MSG":
                    //                    ret = ret.Substring(1);
                    AddMsg("uptime_msg");
                    AddMsg(ret);
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
                            btnShowParams.Enabled = true;
                        }
                    }
                    break;

                case "SEND_CLIENT_LIST":
                    string[] words = ret.Split(' ');
                    i = 0;
                    int j = 0;
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
        private void ShutdownServer(object sender, EventArgs e)
        {
            ManageServer dlg = new ManageServer(m_client);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (client_params[selected_address].AutoConn == true)
                    cfg_params = dlgsetparams.GetParams();
                please_lets_disconnect = 1;
            }
            else
            {
            }
        }
        private void RebootServer(object sender, EventArgs e)       // "test"
        {
            svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SEND_CLIENT_LIST"), 8, "test");
            //SendClientMsg(svrcmd.GetCmdIndexI("SEND_CLIENT_LIST"), "send client list", true);
            AddMsg("send client list");
        }
        // Insert logic for processing found files here.
        public static void ProcessFile(string path)
        {
            //AddMsg("Processed file " + path);
        }
        // DlgSetParams dialog
        private void ShowParamsClick(object sender, EventArgs e)
        {
            return;
            if (m_client.IsConnectionAlive)
            {
                if (dlgsetparams.ShowDialog(this) == DialogResult.OK)
                {
                    //AddMsg("new password: " + cfg_params.password);
                    cfg_params = dlgsetparams.GetParams();
                    byte[] rpm_mph = BitConverter.GetBytes(cfg_params.si_rpm_mph_update_rate);
                    byte[] fpga = BitConverter.GetBytes(cfg_params.si_FPGAXmitRate);
                    byte[] high_rev = BitConverter.GetBytes(cfg_params.si_high_rev_limit);
                    byte[] low_rev = BitConverter.GetBytes(cfg_params.si_low_rev_limit);
                    byte[] fan_on = BitConverter.GetBytes(cfg_params.fan_on);
                    byte[] fan_off = BitConverter.GetBytes(cfg_params.fan_off);
                    byte[] ben = BitConverter.GetBytes(cfg_params.lights_on_value);
                    byte[] b1 = BitConverter.GetBytes(cfg_params.lights_off_value);
                    byte[] b2 = BitConverter.GetBytes(cfg_params.adc_rate);
                    byte[] b3 = BitConverter.GetBytes(cfg_params.rt_value_select);
                    byte[] lights = BitConverter.GetBytes(cfg_params.si_lights_on_delay);
                    byte[] limit = BitConverter.GetBytes(cfg_params.engine_temp_limit);
                    byte[] batt = BitConverter.GetBytes(cfg_params.battery_box_temp);
                    byte[] test = BitConverter.GetBytes(cfg_params.si_test_bank);
                    byte[] pswd_time = BitConverter.GetBytes(cfg_params.si_password_timeout);
                    byte[] pswd_retries = BitConverter.GetBytes(cfg_params.si_password_retries);
                    byte[] baudrate3 = BitConverter.GetBytes(cfg_params.si_baudrate3);
                    //byte[] password = BytesFromString(cfg_params.password);

                    byte[] bytes = new byte[rpm_mph.Count() + fpga.Count() + high_rev.Count()
                        + low_rev.Count() + fan_on.Count() + fan_off.Count() + ben.Count() + b1.Count()
                        + b2.Count() + b3.Count() + lights.Count() + limit.Count() + batt.Count()
                        + test.Count() + pswd_time.Count() + pswd_retries.Count() + baudrate3.Count()/* + password.Count()*/ + 2];

                    bytes[0] = svrcmd.GetCmdIndexB("UPDATE_CONFIG");
                    //System.Buffer.BlockCopy(src, src_offset, dest, dest_offset,count)
                    System.Buffer.BlockCopy(rpm_mph, 0, bytes, 2, rpm_mph.Count());
                    System.Buffer.BlockCopy(fpga, 0, bytes, 4, fpga.Count());
                    System.Buffer.BlockCopy(high_rev, 0, bytes, 6, high_rev.Count());
                    System.Buffer.BlockCopy(low_rev, 0, bytes, 8, low_rev.Count());
                    System.Buffer.BlockCopy(fan_on, 0, bytes, 10, fan_on.Count());
                    System.Buffer.BlockCopy(fan_off, 0, bytes, 12, fan_off.Count());
                    System.Buffer.BlockCopy(ben, 0, bytes, 14, ben.Count());
                    System.Buffer.BlockCopy(b1, 0, bytes, 16, b1.Count());
                    System.Buffer.BlockCopy(b2, 0, bytes, 18, b2.Count());
                    System.Buffer.BlockCopy(b3, 0, bytes, 20, b3.Count());
                    System.Buffer.BlockCopy(lights, 0, bytes, 22, lights.Count());
                    System.Buffer.BlockCopy(limit, 0, bytes, 24, limit.Count());
                    System.Buffer.BlockCopy(batt, 0, bytes, 26, batt.Count());
                    System.Buffer.BlockCopy(test, 0, bytes, 28, test.Count());
                    System.Buffer.BlockCopy(pswd_time, 0, bytes, 30, pswd_time.Count());
                    System.Buffer.BlockCopy(pswd_retries, 0, bytes, 32, pswd_retries.Count());
                    System.Buffer.BlockCopy(baudrate3, 0, bytes, 34, baudrate3.Count());

                    //					System.Buffer.BlockCopy(password, 0, bytes, 36, password.Count());

                    Packet packet = new Packet(bytes, 0, bytes.Count(), false);
                    m_client.Send(packet);
                    /*
					bytes = new byte[rpm_mph.Count() + 2];
					bytes[0] = svrcmd.GetCmdIndexB("SET_RPM_MPH_RATE");
					System.Buffer.BlockCopy(rpm_mph, 0, bytes, 2, rpm_mph.Count());
					packet = new Packet(bytes, 0, bytes.Count(), false);
					m_client.Send(packet);

					bytes = new byte[rpm_mph.Count() + 2];
					bytes[0] = svrcmd.GetCmdIndexB("SET_FPGA_RATE");
					System.Buffer.BlockCopy(rpm_mph, 0, bytes, 2, rpm_mph.Count());
					packet = new Packet(bytes, 0, bytes.Count(), false);
					m_client.Send(packet);
					*/
                }
            }
        }
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
        private void SendTimeup()
        {
            string msg = "SEND_TIMEUP";
            int icmd = svrcmd.GetCmdIndexI(msg);
            foreach (ClientsAvail cl in clients_avail)
            {
                if (cl.type > 0 && cl.socket > 0)
                {
                    svrcmd.Send_ClCmd(icmd, cl.index, cl.index);
                    AddMsg(icmd.ToString());
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
        private void DBMgmt(object sender, EventArgs e) // "test2"
        {
            setnextclient.StartPosition = FormStartPosition.Manual;
            setnextclient.Location = new Point(100, 10);
            if (setnextclient.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
        }
        private void ClearScreen(object sender, EventArgs e)
        {
            tbReceived.Clear();
        }
        private void GetTime(object sender, EventArgs e)
        {

            byte test = Convert.ToByte(tbTest.Text);
            byte test2 = Convert.ToByte(tbTest2.Text);
            byte test3 = Convert.ToByte(tbTest3.Text);
            byte test4 = Convert.ToByte(tbTest4.Text);

            byte[] bytes = new byte[8];
            bytes[0] = test;
            bytes[1] = test2;
            bytes[2] = test3;
            bytes[3] = test4;
            AddMsg(test.ToString());
            AddMsg(test2.ToString());
            AddMsg(test3.ToString());
            AddMsg(test4.ToString());
            AddMsg(bytes.Length.ToString());

            string cmd = "DB_LOOKUP";
            int offset = svrcmd.GetCmdIndexI(cmd);
            svrcmd.Send_ClCmd(offset, 3, bytes);
        }
        private void GarageFormClick(object sender, EventArgs e)
        {
            garageform.Enable_Dlg(true);
            garageform.StartPosition = FormStartPosition.Manual;
            garageform.Location = new Point(100, 10);
            if (garageform.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
            garageform.Enable_Dlg(false);
        }
        private void btnAVR_Click(object sender, EventArgs e)		// test3
        {
            //clientdest.Enable_Dlg(true);
            clientdest.StartPosition = FormStartPosition.Manual;
            clientdest.Location = new Point(100, 10);
            if (clientdest.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
            }
        }
        private void Dialog1_Click(object sender, EventArgs e)
        {
            
        }
        private void myTimerTick(object sender, EventArgs e)
        {
            tick++;
            connected_tick++;
            if(cbAlarm.Checked == true)
			{
                alarm_tick--;
                tbAlarm.Text = alarm_tick.ToString();
                if(alarm_tick == 0)
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
            //AddMsg(connected_tick.ToString());
            /*
            if(connected_tick == 10 && clients_avail[8].socket > 0)
			{
                playdlg.Dispose();
                AddMsg("disconnecting");
                btnConnect.Text = "Connect";
                timer1.Enabled = false;
                client_connected = false;
                //play_aliens_clip();
                m_client.Disconnect();
                tbServerTime.Text = "offline";
            }
            */
            if (tick == 5)
            {
                svrcmd.Send_ClCmd(svrcmd.GetCmdIndexI("SEND_CLIENT_LIST"), 8, "test");
                AddMsg("send client list");
                RedrawClientListBox();
                
            }
            if (clients_inited == false && tick == 30)
            {
                AddMsg("set time");
                foreach (ClientsAvail cl in clients_avail)
                {
                    if (cl.type == 1 && cl.socket > 0)  // set the time on any server/clients in the active list
                    {
                        AddMsg(cl.label);
                        SetTime(cl.index);
                    }
                }

            }
            if (clients_inited == false && tick == 35)
			{
                UpdateClientInfo();
                clients_inited = true;
            }
            //if (tick > 300)
            if(tick > 60)
			{
                if (!player_active)
                {
                    SendTimeup();
                    //btnReportTimeUp_Click(new object(), new EventArgs());
                    tick = 36;
                }
			}
        }
        void play_aliens_clip()
	{
        System.Media.SoundPlayer player;
        string song = "";
        Random r = new Random();
        int rInt = r.Next(0, 49); //for ints
            switch (rInt)
            {
                case 0:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\alien_kill2.wav";
                    break;
                case 1:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\GameOverMan.wav";
                    break;
                case 2:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\day_on_the_farm.wav";
                    break;
                case 3:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\DRAKE.wav";
                    break;
                case 4:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\sweethearts.wav";
                    break;
                case 5:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\illegal_aliens.wav";
                    break;
                case 6:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\express_elevator.wav";
                    break;
                case 7:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\knives_sharp_sticks.wav";
                    break;
                case 8:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\stop_yer_grinnin.wav";
                    break;
                case 9:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\comin_outa_the_gd_walls.wav";
                    break;
                case 10:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\pretty_shit_now_man.wav";
                    break;
                case 11:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\chicken_shit.wav";
                    break;
                case 12:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\count_me_out.wav";
                    break;
                case 13:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\cut_the_power.wav";
                    break;
                case 14:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\grease.wav";
                    break;
                case 15:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\17_hours.wav";
                    break;
                case 16:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\somethin_movin.wav";
                    break;
                case 17:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\dry_heat.wav";
                    break;
                case 18:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\hes_comin_in.wav";
                    break;
                case 19:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\cornbread.wav";
                    break;
                case 20:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\RampClosing.wav";
                    break;
                case 21:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\PrepareForDustOff.wav";
                    break;
                case 22:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\Werspaski.wav";
                    break;
                case 23:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\LetsRock.wav";
                    break;
                case 24:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\CloseEncounters.wav";
                    break;
                case 25:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\YouHeardTheMan.wav";
                    break;
                case 26:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\FlameUnitsOnly.wav";
                    break;
                case 27:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\HarshLanguage.wav";
                    break;
                case 28:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\IsHeFnCrazy.wav";
                    break;
                case 29:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\CantHaveAnyFiring.wav";
                    break;
                case 30:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\StandardLightArmour.wav";
                    break;
                case 31:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\WhatDoThoseFire.wav";
                    break;
                case 32:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\BuildAFire.wav";
                    break;
                case 33:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\WhatAreWeGonnaDo.wav";
                    break;
                case 34:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\Explosion.wav";
                    break;
                case 35:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\HesJustAGrunt.wav";
                    break;
                case 36:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\CurrentEvents.wav";
                    break;
                case 37:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\ArbitrarilyExterminate.wav";
                    break;
                case 38:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\TheyCanBillMe.wav";
                    break;
                case 39:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\SubstantialDollarValue.wav";
                    break;
                case 40:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\NukeTheEntireSite.wav";
                    break;
                case 41:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\WhatAreWeTalkingAboutThisFor.wav";
                    break;
                case 42:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\ThisCantBeHappinen.wav";
                    break;
                case 43:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\YouCantHelpThem.wav";
                    break;
                case 44:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\WaitUpKillYou.wav";
                    break;
                case 45:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\ShutTheGDDoor.wav";
                    break;
                case 46:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\RipleyWhatTheHell.wav";
                    break;
                case 47:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\SargeIsGone.wav";
                    break;
                case 48:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\SpaskiCroweDown.wav";
                    break;
                case 49:
                    song = "c:\\users\\Daniel\\Music\\WavFiles2\\I_Only_Work_Here.wav";
                    break;
                default:
                    break;
            }
            
            int id = song.LastIndexOf("\\");
            string tsong = song.Substring(id + 1);
            AddMsg(tsong);
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
            //AddMsg("loaded");
        }
        private void Btn_PlayList_Click(object sender, EventArgs e)
        {
            playdlg.Enable_Dlg(true);
            player_active = true;

            playdlg.StartPosition = FormStartPosition.Manual;
            playdlg.Location = new Point(100, 10);

            if (playdlg.ShowDialog(this) == DialogResult.OK)
            {
            }
            else
            {
                //                this.txtResult.Text = "Cancelled";
            }
            playdlg.Enable_Dlg(false);
            player_active = false;
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
        private void button1_Click(object sender, EventArgs e)		// get status
        {
            SendClientMsg(svrcmd.GetCmdIndexI("SEND_STATUS"), "status", false);
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
                    if(false)
                    { 
                        btnConnect.Text = "Connect";
                        timer1.Enabled = false;
                        client_connected = false;
                    }
                    RedrawClientListBox();
					if(!remove)
					{
                        lbAvailClients.SetSelected(cl.lbindex,true);
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
		private void btnWaitReboot_Click(object sender, EventArgs e)
		{
            SendClientMsg(svrcmd.GetCmdIndexI("EXIT_TO_SHELL"), " ", true);
        }
		private void tbSendMsg_TextChanged(object sender, EventArgs e)
		{
            sendmsgtext = tbSendMsg.Text;
		}
		private void btnCabinLights_Click(object sender, EventArgs e)
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
		private void button1_Click_1(object sender, EventArgs e)
		{
            SendClientMsg(svrcmd.GetCmdIndexI("SHELL_AND_RENAME"), " ", true);
        }
		private void cbWhichWinClient_SelectedIndexChanged(object sender, EventArgs e)
		{
            which_winclient = cbWhichWinClient.SelectedIndex;
            AddMsg(which_winclient.ToString());
		}
		private void btnWinClMsg_Click(object sender, EventArgs e)
		{
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
        }

		private void tbAlarm_TextChanged(object sender, EventArgs e)
		{
            alarm_tick = Int64.Parse(tbAlarm.Text);
            //AddMsg(alarm_tick.ToString());
		}
	}
}