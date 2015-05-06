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
			(this.MdiParent as fmMain).SetStatus("Обновление данных...");
			var data = (this.MdiParent as fmMain).ST.GetSatellites(new string[] {sats[listBox1.SelectedIndex].Orbit.SatNoradId});
			selSats.AddRange(data);
			textBox1.Text = data.First().Orbit.SatNoradId;
			(this.MdiParent as fmMain).SetStatus("Данные обновлены.");
			(this.MdiParent as fmMain).Viewer.Draw();
		}

		private void fmSatInfo_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			(this.MdiParent as fmMain).SelectedSats.Clear();
		}

	}
}
