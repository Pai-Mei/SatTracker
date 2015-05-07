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
	public partial class fmFilter : Form
	{
		public Double Min = -1;
		public Double Max = -1;

		public fmFilter()
		{
			InitializeComponent();
		}

		private bool CheckFilter()
		{
			if (radioButton2.Checked)
			{
				if (comboBoxOrbit.SelectedIndex > 0 && comboBoxOrbit.SelectedIndex < comboBoxOrbit.Items.Count)
				{
					if (comboBoxOrbit.SelectedIndex == 0)
					{
						Min = 0;
						Max = 2000;
					}
					else if (comboBoxOrbit.SelectedIndex == 1)
					{
						Min = 35586;
						Max = 35886;
					}
					return true;
				}
				else
				{
					MessageBox.Show("Выберите орбиту");
					return false;
				}
			}
			if (radioButton3.Checked)
			{
				if (!Double.TryParse(textBoxFrom.Text, out Min)
					|| !Double.TryParse(textBoxTo.Text, out Max))
				{
					MessageBox.Show("Данные не валидны!");
					return false;
				}
				if (Min > Max)
				{
					var tmp = Min;
					Min = Max;
					Max = Min;
				}
				return true;
			}
			return radioButton1.Checked;
		}

		private void Checking()
		{
			comboBoxOrbit.Enabled = radioButton2.Checked;
			textBoxFrom.Enabled = radioButton3.Checked;
			textBoxTo.Enabled = radioButton3.Checked;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			CheckFilter();	
		}
		
		private void radioButton3_CheckedChanged(object sender, EventArgs e)
		{
			Checking();
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			Checking();
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			Checking();
		}
	}
}
