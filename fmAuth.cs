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
		public SpaceTrack.SpaceTrack ST;

		public fmAuth()
		{
			InitializeComponent();
		}

		delegate void ProgressChangeCallback(SpaceTrack.SpaceTrack.ProgressEventArgs args);
		delegate void StatusChangeCallback(SpaceTrack.SpaceTrack.StatusEventArgs args);

		private void ProgressChange(SpaceTrack.SpaceTrack.ProgressEventArgs args)
		{
			progressBar1.Maximum = args.MaxValue;
			progressBar1.Value = args.CurrentValue;
		}

		private void StatusChange(SpaceTrack.SpaceTrack.StatusEventArgs args)
		{
			StatusText.Text = args.Status;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			button1.Enabled = false;
			this.DialogResult = System.Windows.Forms.DialogResult.None;
			ST = new SpaceTrack.SpaceTrack(textBoxLogin.Text, textBoxPassword.Text);
			ST.Progress += (s, args) => {
				if (!progressBar1.InvokeRequired)
				{
					ProgressChange(args);
				}
				else
				{
					this.Invoke(new ProgressChangeCallback(ProgressChange), args);
				}
			};
			ST.Status += (s, args) => 
			{
				if (statusStrip1.InvokeRequired)
				{
					StatusChange(args);
				}
				else
				{
					this.Invoke(new StatusChangeCallback(StatusChange), args);
				}
			};
			if (!ST.Authentication())
			{
				DialogResult = System.Windows.Forms.DialogResult.Abort;
				MessageBox.Show("Авторизация не удалась!\n Проверьте логин и пароль.", "Ошибка авторизации!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
				return;
			}
			fmFilter filt = new fmFilter();
			if(checkBoxRefresh.Checked)
				filt.ShowDialog();
			BackgroundWorker bw = new BackgroundWorker();
			bw.DoWork += (s, args) => { 
				if(filt.Max == -1 && filt.Min == -1)
					ST.Load(checkBoxRefresh.Checked); 
				else
					ST.Load(filt.Min, filt.Max); 
			};
			bw.RunWorkerCompleted += (s, args) => 
			{
				DialogResult = System.Windows.Forms.DialogResult.OK;
				this.Close(); 
			};
			bw.RunWorkerAsync();
			
		}

		private void fmAuth_Load(object sender, EventArgs e)
		{

		}
	}
}
