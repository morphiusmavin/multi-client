using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ODBMgmt
{
    public partial class Form1 : Form
    {
        private List<String> temp;
        private List<Odata> mycdata = null;

        string initial_directory = @"C:\Users\Daniel\ClientProgramData\odata\";
        public Form1()
        {
            InitializeComponent();
            Odata item = new Odata();
            mycdata = new List<Odata>();
            mycdata.Add(item);
        }
        private void btnDiff_Click(object sender, EventArgs e)
        {
            XmlReader xmlfile = null;
            int lbindex = 0;
            Odata item;
            string filename = "cdata";
            string tfilename;
            tfilename = ChooseXMLFileName();
            if (tfilename == "")
                return;
        
            if (!File.Exists(tfilename))
            {
                MessageBox.Show("can't find file: " + tfilename);
                return;
            }
            DataSet ds = new DataSet();
            var filePath = tfilename;
            xmlfile = XmlReader.Create(filePath, new XmlReaderSettings());
            ds.ReadXml(xmlfile);
            mycdata.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //string temp = "";
                item = new Odata();
                item.label = dr.ItemArray[0].ToString();
                //item.index = lbindex;
                item.port = Convert.ToInt16(dr.ItemArray[0]);
                item.onoff = Convert.ToInt16(dr.ItemArray[1]);
                item.input_port = Convert.ToInt16(dr.ItemArray[2]);
                item.input_type = Convert.ToInt16(dr.ItemArray[3]);
                item.type = Convert.ToInt16(dr.ItemArray[4]);
                item.time_delay = Convert.ToInt16(dr.ItemArray[5]);
                item.time_left = Convert.ToInt16(dr.ItemArray[6]);
                item.pulse_time = Convert.ToInt16(dr.ItemArray[7]);
                item.reset = Convert.ToInt16(dr.ItemArray[8]);
                item.label = dr.ItemArray[9].ToString();
                if (item.port > -1)
                    mycdata.Add(item);
                item = null;
                lbindex++;
            }
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void btnCreateXML_Click(object sender, EventArgs e)
		{
            string tfilename;
            // can't have the 1st line blank
            tfilename = ChooseCSVFileName();
            if (tfilename == "")
                return;
            String[] file = File.ReadAllLines(tfilename);
            String xml = "";
            XElement top = new XElement("Table",
            from items in file
            let fields = items.Split(',')
            select new XElement("O_DATA",
            new XElement("port", fields[0]),
            new XElement("onoff", fields[1]),
            new XElement("input_port", fields[2]),
            new XElement("input_type", fields[3]),
            new XElement("type", fields[4]),
            new XElement("time_delay", fields[5]),
            new XElement("time_left", fields[6]),
            new XElement("pulse_time", fields[7]),
            new XElement("reset", fields[8]),
            new XElement("label", fields[9])
            )
            );
            int i = tfilename.IndexOf('.');
            tfilename = tfilename.Remove(i);
            tfilename += ".xml";
            File.WriteAllText(tfilename, xml + top.ToString());

            MessageBox.Show("created: " + tfilename);
        }
		private void btnCreateDAT_Click(object sender, EventArgs e)
		{
/*
            char label[OLABELSIZE];
            UCHAR port;
            UCHAR onoff;            // current state: 1 if on; 0 if off
            UCHAR input_port;       // input port which affects this output (if not set to 0xFF)
            UCHAR input_type;       // 
                                    // 
            UCHAR type;             // see below
            UINT time_delay;        // when type 2-4 this is used as the time delay
            UINT time_left;         // gets set to time_delay and then counts down
            UCHAR pulse_time;       // not used
            UCHAR reset;
*/
            string tfilename;
            // can't have the 1st line blank
            byte id = 170;
            int i, j, k;
            int val;
            int len;
            char zero = '\0';
            string tstring2;
            string tstring3;
            string label;
            string temp = new string(' ', 30);

            tfilename = ChooseCSVFileName();
            if (tfilename == "")
                return;
            string fileName2 = tfilename;
            i = fileName2.IndexOf('.');
            fileName2 = fileName2.Remove(i);
            fileName2 += ".dat";

            if (File.Exists(fileName2))
            {
                MessageBox.Show("deleting original " + fileName2);
                File.Delete(fileName2);
            }
            String[] file = File.ReadAllLines(tfilename);
            j = 0;
            StringBuilder tstring = new StringBuilder();
            using (BinaryWriter binWriter = new BinaryWriter(File.Open(fileName2, FileMode.Create)))
            {
                binWriter.Write(id);
                for (i = 0; i < 20; i++)
                {
                    string[] words2 = file[i].Split(',');
                    j = 0;
                    foreach (var word in words2)
                    {
                        tstring.Append(word);
                        tstring.Append(',');
                        tstring2 = word;
                        j++;
                        if (j > 8)
                            break;
                    }
                    label = words2[9];
                    byte[] bytes = Encoding.ASCII.GetBytes(label);
                    len = label.Length;
                    binWriter.Write(bytes, 0, len);

                    for (k = 0; k < 32 - len; k++)
                        binWriter.Write(zero);

                    tstring2 = tstring.ToString();
                    tstring.Clear();
                    string[] words = tstring2.Split(',');
                    k = 0;
                    foreach (var word in words)
                    {
                        tstring3 = word;
                        if (word != "")
                        {
                            val = int.Parse(word);
                            binWriter.Write(val);
                        }
                        k++;
                    }
                }

                binWriter.Close();
            }
            MessageBox.Show("done");

        }
		private void btnCreateCSV_Click(object sender, EventArgs e)
		{
            string tfilename;
            // can't have the 1st line blank
            tfilename = CreateCSVFileName();
            if (tfilename == "")
                return;
            if (File.Exists(tfilename))
            {
                string message = "File exists. Do you want to overwrite?";
                string title = "Overwrite File";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    this.Close();
                    return;
                }
            }
            int index = 0;
            int port = 0;
            string line = "0,0,0,0,0,0,0,temp";
            string line2;
            using (StreamWriter sw = File.CreateText(tfilename))
            {
                for (int i = 0; i < 40; i++)
                {
                    line2 = index.ToString() + ',' + port.ToString() + ',' + line + index.ToString();
                    sw.WriteLine(line2);
                    index++;
                }
                sw.Close();
                MessageBox.Show(tfilename + " created");
            }
        }
		private void btn2Grid_Click(object sender, EventArgs e)
		{
            DataTable dt = GetDataTable();
            foreach (Odata td in mycdata)
            {
                if (td.port > -1)
                    dt.Rows.Add(td.port.ToString(), td.onoff.ToString(), td.input_port.ToString(), td.input_type.ToString(),
                        td.type.ToString(), td.time_delay.ToString(), td.time_left.ToString(), td.pulse_time.ToString(),
                        td.reset.ToString(), td.label);
            }
            //          Step 5: Binding the datatable to datagrid:
            dataGridView1.DataSource = dt;
        }
		private void btnGrid2Data_Click(object sender, EventArgs e)
		{
            Odata tdata = null;
            int i = 0;
            string val = "";
            mycdata.Clear();
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                tdata = new Odata();
                tdata.port = i;
                if (dr.Cells[1].Value != null)
                {
                    val = (string)dr.Cells[2].Value;
                    tdata.onoff = int.Parse(val);
                    val = (string)dr.Cells[3].Value;
                    tdata.input_port = int.Parse(val);
                    val = (string)dr.Cells[4].Value;
                    tdata.input_type = int.Parse(val);
                    val = (string)dr.Cells[5].Value;
                    tdata.type = int.Parse(val);
                    val = (string)dr.Cells[6].Value;
                    tdata.time_delay = int.Parse(val);
                    val = (string)dr.Cells[7].Value;
                    tdata.time_left = int.Parse(val);
                    val = (string)dr.Cells[8].Value;
                    tdata.pulse_time = int.Parse(val);
                    tdata.label = (string)dr.Cells[9].Value;
                    mycdata.Add(tdata);
                    tdata = null;
                    i++;
                }
            }
        }
		private void btnReadDATFile_Click(object sender, EventArgs e)
		{
            byte[] bytes = new byte[30];
            byte[] bytes2 = new byte[36];
            byte[] ibytes = new byte[4];
            string label;
            char temp;
            int i, j, k, l;
            int res;
            byte[] id = new byte[1];
            StringBuilder builder;
            Odata item = null;
            string tfilename;
            // can't have the 1st line blank
            tfilename = ChooseDATFileName();
            if (tfilename == "")
                return;
           
            if (!File.Exists(tfilename))
            {
                MessageBox.Show("can't find file: " + tfilename);
                return;
            }
            mycdata.Clear();


            if (!File.Exists(tfilename))
            {
                MessageBox.Show("can't find " + tfilename);
                return;
            }

            DataTable dt = GetDataTable();

            using (BinaryReader binReader = new BinaryReader(File.Open(tfilename, FileMode.Open)))
            {
                id = binReader.ReadBytes(1);
                if (id[0] != 170)
                {
                    MessageBox.Show("bad file format in " + tfilename);
                    return;
                }
                for (j = 0; j < 20; j++)
                {
                    bytes = binReader.ReadBytes(30);
                    builder = new StringBuilder();
                    for (i = 0; i < 30; i++)
                    {
                        if (bytes[i] == 0)
                            bytes[i] = 32;
                        temp = (char)bytes[i];
                        builder.Append(temp);
                    }
                    label = builder.ToString();
                    int index = label.IndexOf(' ');
                    if (index > 0)
                    {
                        label = label.Remove(index);
                    }
                    bytes2 = binReader.ReadBytes(38);   // this is weird - seems like with 9 ints this should be 36
                    k = 2;
                    item = new Odata();
                    for (i = 0; i < 9; i++)
                    {
                        l = i * 4;
                        for (k = 2; k < 6; k++)
                            ibytes[k - 2] = bytes2[k + l];
                        res = BitConverter.ToInt32(ibytes, 0);
                        k = 0;
                        switch (i)
                        {
                            case 0:
                                item.port = res;
                                break;
                            case 1:
                                item.onoff = res;
                                break;
                            case 2:
                                item.input_port = res;
                                break;
                            case 3:
                                item.input_type = res;
                                break;
                            case 4:
                                item.type = res;
                                break;
                            case 5:
                                item.time_delay = res;
                                break;
                            case 6:
                                item.time_left = res;
                                break;
                            case 7:
                                item.pulse_time = res;
                                break;
                            case 8:
                                item.reset = res;
                                break;
                            default:
                                break;
                        }
                    }
                    item.label = label;
                    mycdata.Add(item);
                    item = null;
                }
                foreach (Odata td in mycdata)
                {
                    dt.Rows.Add(td.port.ToString(), td.onoff.ToString(), td.input_port.ToString(), td.input_type.ToString(),
                        td.type.ToString(), td.time_delay.ToString(), td.time_left.ToString(), td.pulse_time.ToString(),
                        td.reset.ToString(), td.label);
                }
                //          Step 5: Binding the datatable to datagrid:
                dataGridView1.DataSource = dt;
            }
        }
		private void btnXML2grid_Click(object sender, EventArgs e)
		{
            XmlReader xmlfile = null;
            int lbindex = 0;
            Odata item;
            string tfilename;
            tfilename = ChooseXMLFileName();
            if (tfilename == "")
                return;
           
            if (!File.Exists(tfilename))
            {
                MessageBox.Show("can't find file: " + tfilename);
                return;
            }
            DataSet ds = new DataSet();
            var filePath = tfilename;
            xmlfile = XmlReader.Create(filePath, new XmlReaderSettings());
            ds.ReadXml(xmlfile);
            mycdata.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                item = new Odata();
                item.label = dr.ItemArray[0].ToString();
                //item.index = lbindex;
                item.port = Convert.ToInt16(dr.ItemArray[0]);
                item.onoff = Convert.ToInt16(dr.ItemArray[1]);
                item.input_port = Convert.ToInt16(dr.ItemArray[2]);
                item.input_type = Convert.ToInt16(dr.ItemArray[3]);
                item.type = Convert.ToInt16(dr.ItemArray[4]);
                item.time_delay = Convert.ToInt16(dr.ItemArray[5]);
                item.time_left = Convert.ToInt16(dr.ItemArray[6]);
                item.pulse_time = Convert.ToInt16(dr.ItemArray[7]);
                item.reset = Convert.ToInt16(dr.ItemArray[8]);
                item.label = dr.ItemArray[9].ToString();
                if (item.port > -1)
                    mycdata.Add(item);
                item = null;
                lbindex++;
            }
            dataGridView1.DataSource = ds.Tables[0];
        }
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            //          Step 2: Create column name or heading by mentioning the datatype.
            DataColumn dc0 = new DataColumn("Port", typeof(string));
            DataColumn dc1 = new DataColumn("onoff", typeof(string));
            DataColumn dc2 = new DataColumn("Input Port", typeof(string));
            DataColumn dc3 = new DataColumn("Input Type", typeof(string));
            DataColumn dc4 = new DataColumn("Type", typeof(string));
            DataColumn dc5 = new DataColumn("Time Delay", typeof(string));
            DataColumn dc6 = new DataColumn("Time Left", typeof(string));
            DataColumn dc7 = new DataColumn("Pulse Time", typeof(string));
            DataColumn dc8 = new DataColumn("Reset", typeof(string));
            DataColumn dc9 = new DataColumn("Label", typeof(string));

            //          Step 3: Adding these Columns to the DataTable,
            dt.Columns.Add(dc0);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            dt.Columns.Add(dc8);
            dt.Columns.Add(dc9);
            return dt;
        }
        private string CreateCSVFileName()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "csv file|*.csv|CSV file|*.CSV";
            saveFileDialog1.Title = "Save a csv file";
            saveFileDialog1.InitialDirectory = initial_directory;
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                tbFileName.Text = saveFileDialog1.FileName;
                return saveFileDialog1.FileName;
            }
            else return "";
        }
        private string CreateXMLFileName()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML file|*.XML|xml file|*.xml";
            saveFileDialog1.Title = "Create an XML file";
            saveFileDialog1.InitialDirectory = initial_directory;
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                tbFileName.Text = saveFileDialog1.FileName;
                return saveFileDialog1.FileName;
            }
            else return "";
        }
        private string ChooseXMLFileName()
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog
            {
                InitialDirectory = initial_directory,
                Title = "Browse XML Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xml",
                Filter = "xml files (*.xml)|*.XML",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = openFileDialog2.FileName;
                return openFileDialog2.FileName;
            }
            else return "";

        }
        private string ChooseCSVFileName()
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog
            {
                InitialDirectory = initial_directory,
                Title = "Browse CSV Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "csv",
                Filter = "csv files (*.csv)|*.CSV",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = openFileDialog2.FileName;
                return openFileDialog2.FileName;
            }
            else return "";

        }
        private string ChooseDATFileName()
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog
            {
                InitialDirectory = initial_directory,
                Title = "Browse dat Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "dat",
                Filter = "dat files (*.dat)|*.DAT",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = openFileDialog2.FileName;
                return openFileDialog2.FileName;
            }
            else return "";

        }
    }
}
