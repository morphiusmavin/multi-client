using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EpServerEngineSampleClient
{
	public partial class AddRecord : Form
	{
		private Cdata m_cdata;
		public AddRecord(Cdata cdata)
		{
			InitializeComponent();
			m_cdata = cdata;
			tbOnHour.Text = "0";
			tbOnMinute.Text = "0";
			tbOnSecond.Text = "0";
			tbOffHour.Text = "0";
			tbOffMinute.Text = "0";
			tbOffSecond.Text = "0";
			tbPort.Text = "0";
		}

		public void SetCdata(Cdata cdata)
		{
			m_cdata = cdata;
		}
		public Cdata GetCdata()
		{
			return m_cdata;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			m_cdata.port = int.Parse(tbPort.Text);
			m_cdata.on_hour = int.Parse(tbOnHour.Text);
			m_cdata.on_minute = int.Parse(tbOnMinute.Text);
			m_cdata.on_second = int.Parse(tbOnSecond.Text);
			m_cdata.off_hour = int.Parse(tbOffHour.Text);
			m_cdata.off_minute = int.Parse(tbOffMinute.Text);
			m_cdata.off_second = int.Parse(tbOffSecond.Text);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
