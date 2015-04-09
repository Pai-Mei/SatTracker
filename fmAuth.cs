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
	public partial class fmAuth : Form
	{
		public fmAuth()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			//SpaceTrack.SpaceTrack st = new SpaceTrack.SpaceTrack(textBoxLogin.Text, textBoxPassword.Text);
			SpaceTrack.SpaceTrack st = new SpaceTrack.SpaceTrack("stratarozumu@gmail.com", "StrataRozumu-e47c8");
			this.Close();
		}
	}
}
