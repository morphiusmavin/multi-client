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
	public partial class Settings : Form
	{
		private bool silent_mode;
		private bool play_chimes;
		public Settings()
		{
			InitializeComponent();
			silent_mode = (bool)Properties.Settings.Default["silent_mode"];
			cbSilentMode.Checked = silent_mode;
			play_chimes = (bool)Properties.Settings.Default["play_chimes"];
			chPlayChimes.Checked = play_chimes;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default["silent_mode"] = silent_mode;
			Properties.Settings.Default["play_chimes"] = play_chimes;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void ccSilentMode(object sender, EventArgs e)
		{
			if (cbSilentMode.Checked)
				silent_mode = true;
			else silent_mode = false;
		}

		private void chPlayChimes_CheckedChanged(object sender, EventArgs e)
		{
			if (chPlayChimes.Checked)
				play_chimes = true;
			else play_chimes = false;
		}
	}
}
