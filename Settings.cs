using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SatTracker
{
	public class Settings : ICloneable
	{
		private static readonly String m_SettingsFilePath = Environment.CurrentDirectory + "\\settings.xml";

		public Color ObjectsColor { get; set; }
		public Color OrbitColor { get; set; }
		public Color AxisXColor { get; set; }
		public Color AxisYColor { get; set; }
		public Color AxisZColor { get; set; }

		public Boolean SimVisualization { get; set; }
		public Double CriticalDistance { get; set; }
		public TimeSpan StepTime { get; set; }

		public Settings()
		{

		}

		public static Settings Load()
		{
			Settings res = null;
			try
			{
				res = Xml.Xml.Load(m_SettingsFilePath, typeof(Settings)) as Settings;
				if (res == null)
					throw new Exception();
			} catch {
				res = new Settings();
				res.AxisXColor = Color.Red;
				res.AxisYColor = Color.Green;
				res.AxisZColor = Color.Blue;
				res.ObjectsColor = Color.White;
				res.OrbitColor = Color.White;
				res.SimVisualization = false;
				res.StepTime = new TimeSpan(0, 1, 0);
				res.CriticalDistance = 0.1;
			}
			return res;
		}

		public void Save()
		{
			Xml.Xml.Save(m_SettingsFilePath, this, typeof(Settings));
		}

		public object Clone()
		{
			return new Settings()
			{
				ObjectsColor = this.ObjectsColor,
				OrbitColor = this.OrbitColor,
				AxisXColor = this.AxisXColor,
				AxisYColor = this.AxisYColor,
				AxisZColor = this.AxisZColor,
				CriticalDistance = this.CriticalDistance,
				SimVisualization = this.SimVisualization,
				StepTime = this.StepTime
			};
		}
	}
}
