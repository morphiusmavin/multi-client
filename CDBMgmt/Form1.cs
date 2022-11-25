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
        }
// test
		private void btncdata_Click(object sender, EventArgs e)
		{
            String[] file = File.ReadAllLines(@"C:\Users\Daniel\dev\cdata.csv");
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
            File.WriteAllText(@"C:\Users\Daniel\dev\cdata.xml", xml + top.ToString());
            MessageBox.Show("C:|\\Users\\Daniel\\dev\\cdata.xml");
        }

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

		private void btnDiff_Click(object sender, EventArgs e)
		{
            XmlReader xmlfile = null;
            int lbindex = 0;
            Tdata item;
            DataSet ds = new DataSet();
            var filePath = "c:\\users\\Daniel\\dev\\cdata3.xml";
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
                mycdata.Add(item);
                item = null;
                lbindex++;
            }
            dataGridView1.DataSource = ds.Tables[0];
        }

		private void btnDiff2_Click(object sender, EventArgs e)
		{
            XmlReader xmlfile = null;
            DataSet ds = new DataSet();
            var filePath = "c:\\users\\Daniel\\dev\\cdata3.xml";
            xmlfile = XmlReader.Create(filePath, new XmlReaderSettings());
            ds.ReadXml(xmlfile);
            dataGridView1.DataSource = ds.Tables[0];
        }

		private void btnDiff3_Click(object sender, EventArgs e)
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
}
