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
		public fmSatInfo Info;
		public fmViewer Viewer;
		private SpaceTrack.SpaceTrack m_ST;

		public List<Eci> SatPos = new List<Eci>();
		public List<Satellite> Sats = new List<Satellite>();
		public List<Satellite> SelectedSats = new List<Satellite>();
		public SpaceTrack.SpaceTrack ST { get { return m_ST; } }

		public fmMain()
		{
			InitializeComponent();
			Info = new fmSatInfo();
			Viewer = new fmViewer();
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
			Viewer.MdiParent = this;
			Viewer.Show();
			Info.MdiParent = this;
			Info.DesktopLocation = new Point(500, 1);
			Info.Show();
		}

		private void выходToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		internal void SetStatus(string p)
		{
			this.StatusText.Text = p;
		}

		private void fmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach(var form in this.MdiChildren)
			{
				form.Dispose();
			}
			Application.Exit();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			Viewer.InitGL();
		}

		private void симуляцияToolStripMenuItem_Click(object sender, EventArgs e)
		{
			fmSim sim = new fmSim(Sats.ToList());
			sim.MdiParent = this;
			sim.Show();
		}

		private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}
	}
}
