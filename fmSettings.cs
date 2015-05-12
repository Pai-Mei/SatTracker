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
	public partial class fmSettings : Form
	{
		public Settings sets { get; set; }

		public fmSettings()
		{
			InitializeComponent();
		}

		private Color GetColor()
		{
			ColorDialog cd = new ColorDialog();
			if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				return cd.Color;				
			return Color.Empty;
		}

		private void SetColor(Control control)
		{
			var color = GetColor();
			if (color != Color.Empty && sets != null)
			{
				sets.ObjectsColor = color;
				control.BackColor = color;
			}
		}

		private void fmSettings_Load(object sender, EventArgs e)
		{
			var currentSets = (this.MdiParent as fmMain).Settings;
			sets = currentSets.Clone() as Settings;

			buttonAxisX.BackColor = sets.AxisXColor;
			buttonAxisY.BackColor = sets.AxisYColor;
			buttonAxisZ.BackColor = sets.AxisZColor;
			buttonObjects.BackColor = sets.ObjectsColor;
			buttonOrbit.BackColor = sets.OrbitColor;
			checkBoxSimView.Checked = sets.SimVisualization;
			textBoxDist.Text = sets.CriticalDistance.ToString("0.000");
			numberStep.Value = (Decimal)sets.StepTime.TotalMinutes;
			comboBoxTimeUnits.SelectedIndex = 1;
			
		}

		private void buttonObjects_Click(object sender, EventArgs e)
		{
			SetColor(buttonObjects);
		}

		private void buttonOrbit_Click(object sender, EventArgs e)
		{
			var color = GetColor();
			if (color != Color.Empty && sets != null)
			{
				sets.OrbitColor = color;
				buttonOrbit.BackColor = color;
			}
		}

		private void buttonAxisX_Click(object sender, EventArgs e)
		{
			var color = GetColor();
			if (color != Color.Empty)
			{
				sets.AxisXColor = color;
				buttonAxisX.BackColor = color;
			}
		}

		private void buttonAxisY_Click(object sender, EventArgs e)
		{
			var color = GetColor();
			if (color != Color.Empty)
			{
				sets.AxisYColor = color;
				buttonAxisY.BackColor = color;
			}
		}

		private void buttonAxisZ_Click(object sender, EventArgs e)
		{
			var color = GetColor();
			if (color != Color.Empty)
			{
				sets.AxisZColor = color;
				buttonAxisZ.BackColor = color;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			double d = 0;
			if (!Double.TryParse(textBoxDist.Text, out d))
				return;
			sets.SimVisualization = checkBoxSimView.Checked;
			sets.CriticalDistance = d;
			switch (comboBoxTimeUnits.SelectedIndex)
			{
				case 1:
					sets.StepTime = new TimeSpan(0, (Int32)numberStep.Value, 0);
					break;
				case 2:
					sets.StepTime = new TimeSpan((Int32)numberStep.Value, 0, 0);
					break;
				default:
					sets.StepTime = new TimeSpan(0, 0, (Int32)numberStep.Value);
					break;
			}
			(this.MdiParent as fmMain).Settings = sets;
		}
	}
}
