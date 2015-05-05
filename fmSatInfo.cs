using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SatTracker
{
	public partial class fmSatInfo : Form
	{
		public fmSatInfo()
		{
			InitializeComponent();
		}

		private void fmSatInfo_Load(object sender, EventArgs e)
		{
			var sats = (this.MdiParent as fmMain).Sats;
			foreach (var sat in sats)
			{
				listBox1.Items.Add(sat.Name);
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			var sats = (this.MdiParent as fmMain).Sats;
			var selSats = (this.MdiParent as fmMain).SelectedSats;
			selSats.Clear();
			selSats.Add(sats[listBox1.SelectedIndex]);
		}

	}
}
