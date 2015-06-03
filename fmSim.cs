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
        private bool isSim = false;
		private CrashEmulation sim;
		private List<Satellite> satelites;

		delegate void StepSituation();
		delegate void CrashSituation();

		public fmSim(List<Satellite> sats)
		{
			satelites = sats;
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
            fmClusterSettings Csets = new fmClusterSettings();
            Csets.ShowDialog();
            if (Csets.Nodes != null && Csets.Nodes.Count > 0)
            {
                var sets = (this.MdiParent as fmMain).Settings;
                var stepTime = new TimeSpan(0, 0, 0, 0, (int)(sets.StepTime.TotalMilliseconds / Csets.Nodes.Count));
                sim = new CrashEmulation(satelites, DateTime.UtcNow, stepTime, sets.CriticalDistance);
                sim.Crash += sim_Crash;
                sim.StepSituiation += sim_StepSituiation;
            }
            else
            {
                var sets = (this.MdiParent as fmMain).Settings;
                sim = new CrashEmulation(satelites, DateTime.UtcNow, sets.StepTime, sets.CriticalDistance);
                sim.Crash += sim_Crash;
                sim.StepSituiation += sim_StepSituiation;
            }
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			richTextBox1.Text += "Симуляция началась.\n";
			sim.Start();
            isSim = true;
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			richTextBox1.Text += "Симуляция завершена.\n";
			sim.Stop();
            isSim = false;
		}

        private void fmSim_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSim)
                e.Cancel = true;
        }
	}
}
