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
	public partial class DeleteRec : Form
	{
		private int delrecno;
		private int norecs;
		public DeleteRec()
		{
			InitializeComponent();
			norecs = delrecno = 0;
		}

		public int GetDelRecNo()
		{
			return delrecno;
		}
		public int GetNoRecs()
		{
			return norecs;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void tbRecord_TextChanged(object sender, EventArgs e)
		{
			delrecno = int.Parse(tbRecord.Text);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			norecs = delrecno = 0;
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void DeleteRec_Load(object sender, EventArgs e)
		{

		}

		private void tbEndingRec_TextChanged(object sender, EventArgs e)
		{
			norecs = int.Parse(tbEndingRec.Text);
		}
	}
}
