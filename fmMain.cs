using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zeptomoby.OrbitTools;

namespace SatTracker
{
	public partial class fmMain : Form
	{
		private fmSatInfo m_Info;
		private fmViewer m_Viewer;
		private SpaceTrack.SpaceTrack m_ST;

		public List<Eci> SatPos = new List<Eci>();
		public List<Satellite> Sats = new List<Satellite>();
		public List<Satellite> SelectedSats = new List<Satellite>();

		public fmMain()
		{
			InitializeComponent();
			m_Info = new fmSatInfo();
			m_Viewer = new fmViewer();
		}

		private void fmMain_Load(object sender, EventArgs e)
		{
			fmAuth auth = new fmAuth();

			if (auth.ShowDialog() != System.Windows.Forms.DialogResult.OK)
			{
				this.Close();
				return;
			}
			m_ST = auth.ST;
			Sats = m_ST.AllSats;
			auth.Dispose();
			m_Viewer.MdiParent = this;
			m_Viewer.Show();
			m_Info.MdiParent = this;
			m_Info.DesktopLocation = new Point(500, 1);
			m_Info.Show();
		}

		private void выходToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
