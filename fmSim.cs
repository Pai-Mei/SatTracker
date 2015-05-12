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
	public partial class fmSim : Form
	{
		private CrashEmulation sim;

		delegate void StepSituation();
		delegate void CrashSituation();

		public fmSim(List<Satellite> sats)
		{
			sim = new CrashEmulation(sats, DateTime.UtcNow, new TimeSpan(0, 1, 0), 0.2);
			sim.Crash += sim_Crash;
			sim.StepSituiation += sim_StepSituiation;
			InitializeComponent();
		}

		void sim_StepSituiation(object sender, CrashEmulation.SituationEventArgs e)
		{
			if (statusStrip1.InvokeRequired)
			{
				this.Invoke(new Action(() => { SimTimeText.Text = e.EllapsedTime.ToString(); }));
			}
			else
			{
				SimTimeText.Text = e.EllapsedTime.ToString();
			}
		}

		void sim_Crash(object sender, CrashEmulation.CrashEventArgs e)
		{
			string text = "Спутники " + e.Sats[0].Name.TrimEnd('\r') + " и " + e.Sats[1].Name.TrimEnd('\r') + " разойдустся на расстоянии "+e.Dist.ToString("0.00")+"км. в " + e.Time.ToString() + "\n";
			if (richTextBox1.InvokeRequired)
			{
				this.Invoke(new Action(() => { richTextBox1.Text += text; }));
			} else 
			{
				richTextBox1.Text += text;
			}
		}

		private void fmSim_Load(object sender, EventArgs e)
		{
			
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			richTextBox1.Text += "Симуляция началась.\n";
			sim.Start();
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			richTextBox1.Text += "Симуляция завершена.\n";
			sim.Stop();
		}
	}
}
