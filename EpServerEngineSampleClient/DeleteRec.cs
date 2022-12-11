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
		public DeleteRec()
		{
			InitializeComponent();
		}

		public int GetDelRecNo()
		{
			return delrecno;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void tbRecord_TextChanged(object sender, EventArgs e)
		{
			int i,j;
			string temp = tbRecord.Text;
			if (temp.Contains(','))
			{
				string[] words = temp.Split(' ');
				i = 0;
				j = 0;
				foreach (var word in words)
				{
					switch (i)
					{
						case 0:
							j = int.Parse(word);
							break;
					}
				}
			}
						delrecno = int.Parse(tbRecord.Text);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void DeleteRec_Load(object sender, EventArgs e)
		{

		}
	}
}
