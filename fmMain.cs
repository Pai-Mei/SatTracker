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
		
		public List<Eci> SatPos = new List<Eci>();
		public List<Satellite> Sats = new List<Satellite>();

		public fmMain()
		{
			InitializeComponent();
		}

		private void fmMain_Load(object sender, EventArgs e)
		{
			fmAuth auth = new fmAuth();
			
			if (auth.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				return;
			auth.Dispose();

			fmViewer f = new fmViewer();
			f.MdiParent = this;
			f.Show();
		}
	}
}
