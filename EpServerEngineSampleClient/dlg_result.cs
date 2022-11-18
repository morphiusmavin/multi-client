		private void btnTest2_Click(object sender, EventArgs e)
		{
            DialogResult res;
            int iResult = 0;
            TimerSchedule dlg = new TimerSchedule();
            dlg.SetClient(m_client);
            res = dlg.ShowDialog(this);
            iResult = dlg.GetResult();
            if (res == DialogResult.OK)
			{
                AddMsg("dlg OK");
			}else if(res == DialogResult.Abort)
			{
                AddMsg("dlg Abort");
			}else if(res == DialogResult.Cancel)
			{
                AddMsg("dlg Cancel");
			}
            dlg.Close();
        }
