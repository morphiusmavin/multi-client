using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Xml.Serialization;
using System.Drawing;
using System.Timers;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Xml.Linq;

namespace CDBMgmt
{
	public partial class CDBMgmt : Form
	{
        private List<String> temp;
        private List<Tdata> mycdata = null;
         private int selected_row = -1;
        public CDBMgmt()
		{
			InitializeComponent();
            Tdata item = new Tdata();
            mycdata = new List<Tdata>();
            mycdata.Add(item);
            /*
            string xml_file_location = "c:\\users\\Daniel\\dev\\cdata2.xml";
            Tdata item;
            mycdata = new List<Tdata>();
            XmlReader xmlfile = null;
            int lbindex = 0;
            DataSet ds = new DataSet();
            var filePath = xml_file_location;
            xmlfile = XmlReader.Create(filePath, new XmlReaderSettings());
            ds.ReadXml(xmlfile);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //string temp = "";
                item = new Tdata();
                item.label = dr.ItemArray[0].ToString();
                //item.index = lbindex;
                item.index = Convert.ToInt16(dr.ItemArray[0]);
                item.port = Convert.ToInt16(dr.ItemArray[1]);
                item.state = Convert.ToInt16(dr.ItemArray[2]);
                item.on_hour = Convert.ToInt16(dr.ItemArray[3]);
                item.on_minute = Convert.ToInt16(dr.ItemArray[4]);
                item.on_second = Convert.ToInt16(dr.ItemArray[5]);
                item.off_hour = Convert.ToInt16(dr.ItemArray[6]);
                item.off_minute = Convert.ToInt16(dr.ItemArray[7]);
                item.off_second = Convert.ToInt16(dr.ItemArray[8]);
                item.label = dr.ItemArray[9].ToString();
                mycdata.Add(item);
                item = null;
                lbindex++;
            }
            dataGridView1.DataSource = ds.Tables[0];
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if(dc.ColumnName != "label")
                    dc.MaxLength = 3;
            }
            */
        }
        // create an XML file from a cdata.csv 
		private void btncdata_Click(object sender, EventArgs e)
		{
            string tfilename;
            // can't have the 1st line blank
            string filename = "cdata";
            Filenamer fn = new Filenamer("create an XML file from a csv file", "csv");
            if (fn.ShowDialog(this) == DialogResult.OK)
            {
                filename = fn.GetFilename();
            }
            else
            {
            }
            fn.Dispose();
            tfilename = filename + ".csv";
            if(!File.Exists(@"C:\Users\Daniel\dev\" + tfilename))
			{
                MessageBox.Show("can't find file: " + filename + ".csv");
                return;
			}
            String[] file = File.ReadAllLines(@"C:\Users\Daniel\dev\" + filename + ".csv");
            String xml = "";
            XElement top = new XElement("Table",
            from items in file
            let fields = items.Split(',')
            select new XElement("C_DATA",
            new XElement("index", fields[0]),
            new XElement("port", fields[1]),
            new XElement("state", fields[2]),
            new XElement("on_hour", fields[3]),
            new XElement("on_minute", fields[4]),
            new XElement("on_second", fields[5]),
            new XElement("off_hour", fields[6]),
            new XElement("off_minute", fields[7]),
            new XElement("off_second", fields[8]),
            new XElement("label", fields[9])
            )
            );
            File.WriteAllText(@"C:\Users\Daniel\dev\" + filename + ".xml", xml + top.ToString());
            MessageBox.Show("created: C:\\Users\\Daniel\\dev\\" + filename + ".xml");
        }

        // sunrise sunset data
		private void btntdata_Click(object sender, EventArgs e)
		{
            String[] file = File.ReadAllLines(@"C:\Users\Daniel\dev\tdata.csv");
            foreach (String sr in file)
            {
                int i = sr.IndexOf('?');
                i--;
                int j = sr.IndexOf(',', i);
                String temp2 = sr.Remove(i, j - i);
                i = temp2.IndexOf('?');
                i--;
                j = temp2.IndexOf(',', i);
                temp2 = temp2.Remove(i, j - i);
                i = temp2.IndexOf('?');
                temp2 = temp2.Remove(i, 1);
                i = temp2.IndexOf('(');
                j = temp2.IndexOf(')');
                temp2 = temp2.Remove(i, j - i + 1);
                temp.Add(temp2);
            }
            String xml = "";
            XElement top = new XElement("Table",
            from items in temp
            let fields = items.Split(',')
            select new XElement("T_DATA",
            new XElement("Index", fields[0]),
            new XElement("Sunrise", fields[1]),
            new XElement("Sunset", fields[2]),
            new XElement("Length", fields[3]),
            new XElement("Diff", fields[4]),
            new XElement("AstStart", fields[5]),
            new XElement("AstEnd", fields[6]),
            new XElement("NautStart", fields[7]),
            new XElement("NautEnd", fields[8]),
            new XElement("CivilStart", fields[9]),
            new XElement("CivilEnd", fields[10]),
            new XElement("Time", fields[11]),
            new XElement("MilMiles", fields[12])
            )
            );
            File.WriteAllText(@"C:\Users\Daniel\dev\tdata.xml", xml + top.ToString());
            MessageBox.Show("C:\\Users\\Daniel\\dev\\tdata.xml");
        }

		private void CellClick(object sender, DataGridViewCellEventArgs e)
		{
            int row = 0;
            selected_row = e.RowIndex;
            int col = e.ColumnIndex;
            DataGridViewRow dr = dataGridView1.Rows[row];
            DataGridViewCell dc = dr.Cells[col];
            string index = dc.Value.ToString();
        }

        private void LoadForm(object sender, EventArgs e)
		{

		}

		private void RowEnter(object sender, DataGridViewCellEventArgs e)
		{
            int row = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[row];
            DataGridViewCell dc = dr.Cells[0];
            string index = dc.Value.ToString();
		}

        // gets data from gridview and puts in mycdata
		private void btnUpdate_Click(object sender, EventArgs e)
		{
            Tdata tdata = null;
            int i = 0;
            string val = "";
            mycdata.Clear();
            foreach (DataGridViewRow dr in dataGridView1.Rows)
			{
                tdata = new Tdata();
                tdata.index = i;
                if (dr.Cells[1].Value != null)
                {
                    val = (string)dr.Cells[1].Value;
                    tdata.port = int.Parse(val);
                    val = (string)dr.Cells[2].Value;
                    tdata.state = int.Parse(val);
                    val = (string)dr.Cells[3].Value;
                    tdata.on_hour = int.Parse(val);
                    val = (string)dr.Cells[4].Value;
                    tdata.on_minute = int.Parse(val);
                    val = (string)dr.Cells[5].Value;
                    tdata.on_second = int.Parse(val);
                    val = (string)dr.Cells[6].Value;
                    tdata.off_hour = int.Parse(val);
                    val = (string)dr.Cells[7].Value;
                    tdata.off_minute = int.Parse(val);
                    val = (string)dr.Cells[8].Value;
                    tdata.off_second = int.Parse(val);
                    tdata.label = (string)dr.Cells[9].Value;
                    mycdata.Add(tdata);
                    tdata = null;
                    i++;
                }
            }
        }

        // gets data from XML and puts in mycdata and displays in grid
		private void btnDiff_Click(object sender, EventArgs e)
		{
            XmlReader xmlfile = null;
            int lbindex = 0;
            Tdata item;
            string filename = "cdata";
            string tfilename;
            Filenamer fn = new Filenamer("gets data from an XML file in dev dir and displays in grid", ".xml");
            if (fn.ShowDialog(this) == DialogResult.OK)
            {
                filename = fn.GetFilename();
            }
            else
            {
            }
            fn.Dispose();
            tfilename = filename + ".xml";
            if (!File.Exists(@"C:\Users\Daniel\dev\" + tfilename))
            {
                MessageBox.Show("can't find file: " + tfilename);
                return;
            }
            DataSet ds = new DataSet();
            var filePath = "c:\\users\\Daniel\\dev\\" + tfilename;
            xmlfile = XmlReader.Create(filePath, new XmlReaderSettings());
            ds.ReadXml(xmlfile);
            mycdata.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //string temp = "";
                item = new Tdata();
                item.label = dr.ItemArray[0].ToString();
                //item.index = lbindex;
                item.index = Convert.ToInt16(dr.ItemArray[0]);
                item.port = Convert.ToInt16(dr.ItemArray[1]);
                item.state = Convert.ToInt16(dr.ItemArray[2]);
                item.on_hour = Convert.ToInt16(dr.ItemArray[3]);
                item.on_minute = Convert.ToInt16(dr.ItemArray[4]);
                item.on_second = Convert.ToInt16(dr.ItemArray[5]);
                item.off_hour = Convert.ToInt16(dr.ItemArray[6]);
                item.off_minute = Convert.ToInt16(dr.ItemArray[7]);
                item.off_second = Convert.ToInt16(dr.ItemArray[8]);
                item.label = dr.ItemArray[9].ToString();
                if(item.port > -1)
                    mycdata.Add(item);
                item = null;
                lbindex++;
            }
            dataGridView1.DataSource = ds.Tables[0];
        }
             
        // gets data from mycdata and puts in grid
        private void btnDiff3_Click(object sender, EventArgs e)
		{
            DataTable dt = GetDataTable();
            foreach (Tdata td in mycdata)
            {
                if(td.port > -1)
                dt.Rows.Add(td.index.ToString(), td.port.ToString(), td.state.ToString(), td.on_hour.ToString(), 
                    td.on_minute.ToString(), td.on_second.ToString(), td.off_hour.ToString(), td.off_minute.ToString(), 
                    td.off_second.ToString(), td.label);
            }
//          Step 5: Binding the datatable to datagrid:
            dataGridView1.DataSource = dt;
        }

        // create a cdata.dat binary flatfile from a csv common delimited text file
        private void btnCdata2_Click(object sender, EventArgs e)
        {
            string tfilename;
            // can't have the 1st line blank
            string filename = "cdata";
            Filenamer fn = new Filenamer("create an cdata.dat flatfile from a csv file", "");
            if (fn.ShowDialog(this) == DialogResult.OK)
            {
                filename = fn.GetFilename();
            }
            else
            {
            }
            fn.Dispose();
            tfilename = @"C:\Users\Daniel\dev\" + filename + ".csv";
            if (!File.Exists(tfilename))
            {
                MessageBox.Show("can't find file: " + tfilename);
                return;
            }
            String[] file = File.ReadAllLines(tfilename);
			byte id = 170;
			int i,j,k;
			int val;
            int len;
            char zero = '\0';
            string tstring2;
            string tstring3;
            string label;
            string temp = new string(' ',30);
            string fileName2 = @"C:\Users\Daniel\dev\" + filename + ".dat";
            if(File.Exists(fileName2))
			{
                MessageBox.Show("deleting original " + fileName2);
                File.Delete(fileName2);
			}
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
                    binWriter.Write(bytes,0,len);
                  
                    for (k = 0; k < 32 - len; k++)
                        binWriter.Write(zero);
                    
                    tstring2 = tstring.ToString();
                    tstring.Clear();
                    string[] words = tstring2.Split(',');
                    foreach (var word in words)
                    {
                        tstring3 = word;
                        if (word != "")
                        {
                            val = int.Parse(word);
                            binWriter.Write(val);
                        }
                    }
                }
                    
                binWriter.Close();
            }
            MessageBox.Show("done");
        }

        // retrieve db from cdata.dat bin file and display in grid
        private void btnReadCdata_Click(object sender, EventArgs e)
        {
            byte[] bytes = new byte[30];
            byte[] bytes2 = new byte[36];
            byte[] ibytes = new byte[4];
            string label;
            char temp;
            int i,j,k,l;
            int res;
            byte[] id = new byte[1];
            StringBuilder builder;
            Tdata item = null;
            string tfilename;
            // can't have the 1st line blank
            string filename = "cdata";
            Filenamer fn = new Filenamer("read cdata.dat flatfile and display in grid", ".dat");
            if (fn.ShowDialog(this) == DialogResult.OK)
            {
                filename = fn.GetFilename();
            }
            else
            {
            }
            fn.Dispose();
            tfilename = @"C:\Users\Daniel\dev\" + filename + ".dat";
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
                if(id[0] != 170)
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
                    item = new Tdata();
                    for (i = 0;i < 9;i++)
					{
                        l = i * 4;
                        for(k = 2;k < 6;k++)
                            ibytes[k - 2] = bytes2[k+l];
                        res = BitConverter.ToInt32(ibytes, 0);
                        k = 0;
                        switch (i)
                        {
                            case 0:
                                item.index = res;
                                break;
                            case 1:
                                item.port = res;
                                break;
                            case 2:
                                item.state = res;
                                break;
                            case 3:
                                item.on_hour = res;
                                break;
                            case 4:
                                item.on_minute = res;
                                break;
                            case 5:
                                item.on_second = res;
                                break;
                            case 6:
                                item.off_hour = res;
                                break;
                            case 7:
                                item.off_minute = res;
                                break;
                            case 8:
                                item.off_second = res;
                                break;
                            default:
                                break;
                        }
					}
                    item.label = label;
                    mycdata.Add(item);
                    item = null;
                }
                foreach (Tdata td in mycdata)
                {
                    dt.Rows.Add(td.index.ToString(), td.port.ToString(), td.state.ToString(), td.on_hour.ToString(),
                        td.on_minute.ToString(), td.on_second.ToString(), td.off_hour.ToString(), td.off_minute.ToString(),
                        td.off_second.ToString(), td.label);
                }
                //          Step 5: Binding the datatable to datagrid:
                dataGridView1.DataSource = dt;
            }
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            Tdata cdata = new Tdata();
            AddRecord addrec = new AddRecord(cdata);

            //addrec.SetClient(m_client);
            addrec.SetCdata(cdata);
            addrec.StartPosition = FormStartPosition.Manual;
            addrec.Location = new Point(100, 10);

            if (addrec.ShowDialog(this) == DialogResult.OK)
            {
                cdata = addrec.GetCdata();
            }
            else
            {
                //                this.txtResult.Text = "Cancelled";
            }
            addrec.Dispose();
            int count = mycdata.Count();
            cdata.index = count;
            mycdata.Add(cdata);
            btnDiff3_Click(new object(), new EventArgs());
        }
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            //          Step 2: Create column name or heading by mentioning the datatype.
            DataColumn dc0 = new DataColumn("Index", typeof(string));
            DataColumn dc1 = new DataColumn("Port", typeof(string));
            DataColumn dc2 = new DataColumn("State", typeof(string));
            DataColumn dc3 = new DataColumn("On Hour", typeof(string));
            DataColumn dc4 = new DataColumn("On Minute", typeof(string));
            DataColumn dc5 = new DataColumn("On Second", typeof(string));
            DataColumn dc6 = new DataColumn("Off Hour", typeof(string));
            DataColumn dc7 = new DataColumn("Off Minute", typeof(string));
            DataColumn dc8 = new DataColumn("Off Second", typeof(string));
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

        // creates a new blank csv file
		private void btnCreateNew_Click(object sender, EventArgs e)
		{
            string tfilename;
            // can't have the 1st line blank
            string filename = "cdata";
            Filenamer fn = new Filenamer("Create a New csv file", ".csv");
            if (fn.ShowDialog(this) == DialogResult.OK)
            {
                filename = fn.GetFilename();
            }
            else
            {
            }
            fn.Dispose();
            tfilename = @"C:\Users\Daniel\dev\" + filename + ".csv";
            if (File.Exists(tfilename))
            {
                MessageBox.Show(tfilename + " already exists");
                return;
            }
            int index = 0;
            int port = 0;
            string line = "0,0,0,0,0,0,0,temp";
            string line2;
            using (StreamWriter sw = File.CreateText(tfilename))
            {
                for (int i = 0; i < 20; i++)
                {
                    line2 = index.ToString() + ',' + port.ToString() + ',' + line + index.ToString();
                    sw.WriteLine(line2);
                    index++;
                    port = -1;
                }
                sw.Close();
                MessageBox.Show(tfilename + " created");
            }
        }

        // writes current data to XML file
		private void btnCurrent2XML_Click(object sender, EventArgs e)
		{
            string tfilename;
            // can't have the 1st line blank
            string filename = "cdata";
            Filenamer fn = new Filenamer("create an XML file from current data", "XML");
            if (fn.ShowDialog(this) == DialogResult.OK)
            {
                filename = fn.GetFilename();
            }
            else
            {
            }
            fn.Dispose();
            tfilename = filename + ".csv";
            if (File.Exists(@"C:\Users\Daniel\dev\" + tfilename))
            {
                MessageBox.Show(filename + ".XML already exists");
                return;
            }
            int count = mycdata.Count;
            String[] file = new string[20];
            int i = 0;
            foreach (Tdata td in mycdata)
            {
                //if (td.port > -1)
                    file[i] = td.index.ToString() +","+ td.port.ToString() + "," + td.state.ToString() + "," + td.on_hour.ToString() + "," +
                        td.on_minute.ToString() + "," + td.on_second.ToString() + "," + td.off_hour.ToString() + "," + td.off_minute.ToString() + "," +
                        td.off_second.ToString() + "," + td.label;
                i++;
            }
            string line = "0,0,0,0,0,0,0,temp";
            string line2;
            int index = count;
            for (int j = 0; j < 20-count; j++)
            {
                line2 = index.ToString() + ',' + "-1" + ',' + line + index.ToString();
                file[index] = line2;
                index++;
            }
            String xml = "";
            XElement top = new XElement("Table",
            from items in file
            let fields = items.Split(',')
            select new XElement("C_DATA",
            new XElement("index", fields[0]),
            new XElement("port", fields[1]),
            new XElement("state", fields[2]),
            new XElement("on_hour", fields[3]),
            new XElement("on_minute", fields[4]),
            new XElement("on_second", fields[5]),
            new XElement("off_hour", fields[6]),
            new XElement("off_minute", fields[7]),
            new XElement("off_second", fields[8]),
            new XElement("label", fields[9])
            )
            );
            File.WriteAllText(@"C:\Users\Daniel\dev\" + filename + ".xml", xml + top.ToString());
            MessageBox.Show("created: C:\\Users\\Daniel\\dev\\" + filename + ".xml");
        }
	}
}
