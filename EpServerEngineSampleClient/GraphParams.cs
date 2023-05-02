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
	public partial class GraphParams : Form
	{
		int chart_noRec = 10;
		decimal chart_min = 0;		// might want to use these in the future
		decimal chart_max = 100;
		int m_AxisX_Interval = 30;
		int m_YValuesPerPoint = 10;
		int m_MarkerStep = 10;
		int m_initial_noRecs;

		public GraphParams()
		{
			InitializeComponent();
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
		public int GetAxisInterval()
		{
			return m_AxisX_Interval;
		}
		public int GetYValuesPerPoint()
		{
			return m_YValuesPerPoint;
		}
		public int GetMarkerSteps()
		{
			return m_MarkerStep;
		}
		public int GetNoRecs()
		{
			return chart_noRec;
		}
		public void SetAxisInterval(int interval)
		{
			m_AxisX_Interval = interval;
			tbAxisInterval.Text = interval.ToString();
		}
		public void SetYValuesPerPoint(int Yvals)
		{
			m_YValuesPerPoint = Yvals;
			tbYValuesPerPoint.Text = Yvals.ToString();
		}
		public void SetMarkerSteps(int steps)
		{
			m_MarkerStep = steps;
			tbMarkerStep.Text = steps.ToString();
		}
		public void SetNoRecs(int recs)
		{
			chart_noRec = recs;
			m_initial_noRecs = recs;
			tbNoRecs.Text = chart_noRec.ToString();
		}
		private void tbAxisInterval_TextChanged(object sender, EventArgs e)
		{
			m_AxisX_Interval = int.Parse(tbAxisInterval.Text);
		}

		private void tbYValuesPerPoint_TextChanged(object sender, EventArgs e)
		{
			m_YValuesPerPoint = int.Parse(tbYValuesPerPoint.Text);
			if(m_YValuesPerPoint > 32)
			{
				MessageBox.Show("must be <= 32");
				m_YValuesPerPoint = 32;
				tbYValuesPerPoint.Text = m_YValuesPerPoint.ToString();
			}
		}

		private void tbMarkerStep_TextChanged(object sender, EventArgs e)
		{
			m_MarkerStep = int.Parse(tbMarkerStep.Text);
		}

		private void tbNoRecs_TextChanged(object sender, EventArgs e)
		{
			chart_noRec = int.Parse(tbNoRecs.Text);
			if(chart_noRec > m_initial_noRecs)
			{
				MessageBox.Show("can't have more that initial no. recs");
				chart_noRec = m_initial_noRecs;
				tbNoRecs.Text = chart_noRec.ToString();
			}
		}
	}
}
