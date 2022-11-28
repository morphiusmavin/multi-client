using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CDBMgmt
{
	public partial class Filenamer : Form
	{
		string filename;
		public Filenamer(string prompt, string ext)
		{
			InitializeComponent();
			Prompt_label.Text = prompt;
			Ext_Label.Text = ext;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void tbFilename_TextChanged(object sender, EventArgs e)
		{
			filename = tbFilename.Text;
		}
		public string GetFilename()
		{
			return filename;
		}
	}
}
