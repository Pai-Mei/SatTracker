using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeptomoby.OrbitTools;

namespace SatTracker
{
	public class SatVector: Vector
		{
			public Satellite Satellite { get; set; }

			public SatVector(Double x, Double y, Double z):base(x, y, z)
			{

			}

			public SatVector(Vector vect, Satellite sat):base(vect.X, vect.Y, vect.Z)
			{
				Satellite = sat;
			}
		}

	public class GeomentryContainer<T> where T : Vector
	{
		public Vector Min { get; set; }
		public Vector Max { get; set; }

		private Double m_Step;
		private Double m_Radius;
		private int m_HalfSize;

		private List<T>[, ,] m_Cells;

		public GeomentryContainer(Double MaxRadius, Double step = 100)
		{
			m_HalfSize = (int)Math.Floor(MaxRadius / step) + 1;
			this.m_Step = step;
			this.m_Radius = step * m_HalfSize;
			m_Cells = new List<T>[m_HalfSize * 2, m_HalfSize * 2, m_HalfSize * 2];
		}

		public List<T> GetData(Double x, Double y, Double z)
		{
			int xIndex = (int)Math.Floor(x / m_Step) + m_HalfSize;
			int yIndex = (int)Math.Floor(y / m_Step) + m_HalfSize;
			int zIndex = (int)Math.Floor(z / m_Step) + m_HalfSize;
			if (xIndex >= m_HalfSize * 2 || yIndex >= m_HalfSize * 2 || zIndex >= m_HalfSize * 2)
				return null;
			if (m_Cells[xIndex, yIndex, zIndex] == null)
				m_Cells[xIndex, yIndex, zIndex] = new List<T>();
			return m_Cells[xIndex, yIndex, zIndex];
		}

		public List<T> GetData(T v)
		{
			return GetData(v.X, v.Y, v.Z);
		}

		public void Add(T sv)
		{
			if (sv == null)
				return;
			var cell = GetData(sv);
			if(cell != null)
				cell.Add(sv);
		}

		public void Remove(T sv)
		{
			if (sv == null)
				return;
			var cell = GetData(sv);
			if (cell == null) return;
			if(cell.Contains(sv))
				cell.Remove(sv);
		}
	}

	public class CrashEmulation
	{
		public class SituationEventArgs : EventArgs
		{
			public TimeSpan EllapsedTime { get; set; }

			public SituationEventArgs()
			{

			}

			public SituationEventArgs(TimeSpan Time)
			{
				this.EllapsedTime = Time;
			}
		}

		public class CrashEventArgs : EventArgs
		{
			public List<Satellite> Sats { get; set; }
			public TimeSpan Time { get; set; }
			public Double Dist { get; set; }
			public CrashEventArgs()
			{

			}

			public CrashEventArgs(List<Satellite> Sats, Double dist, TimeSpan Time)
			{
				this.Sats = Sats;
				this.Time = Time;
				this.Dist = dist;
			}
		}

		private SatVector[] positions;
		private GeomentryContainer<SatVector> map;

		private List<Satellite> m_sats;

		private TimeSpan SimulationTime;
		private DateTime SimulationStart;
		private TimeSpan SimulationStep;
		private double CrashDistance;

		private bool flagDemoCrash = false;

		public CrashEmulation(List<Satellite> sats, DateTime SimStartTime, TimeSpan step, Double CrashDist)
		{
			this.SimulationStart = SimStartTime;
			this.m_sats = sats;
			this.SimulationStep = step;
			this.CrashDistance = CrashDist;
			map = new GeomentryContainer<SatVector>(200000, 1000);
		}

		private bool flagStop = false;

		public void Start()
		{
			flagStop = false;
			var worker = new BackgroundWorker();
			worker.DoWork += (s, args) => 
			{
				while(!flagStop) Step(); 
			};
			worker.RunWorkerCompleted += (s, args) => {  };
			worker.RunWorkerAsync();
		}

		public void Stop()
		{
			flagStop = true;
		}

		private void Step()
		{
			
				if (positions == null)
					positions = new SatVector[m_sats.Count];
				Parallel.For(0, m_sats.Count, (i) =>
				{
					try
					{
						var startEpoch = SimulationStart.Subtract(m_sats[i].Orbit.EpochTime);
						while (startEpoch > m_sats[i].Orbit.Period)
							startEpoch = startEpoch.Subtract(m_sats[i].Orbit.Period);
						map.Remove(positions[i]);
						positions[i] = new SatVector(m_sats[i].PositionEci(startEpoch.TotalMinutes + SimulationTime.TotalMinutes).Position, m_sats[i]);
						map.Add(positions[i]);
					}
					catch
					{

					}
				});
			try
			{
				for (int i = 0; i < positions.Length; i++)
				{
					var p = positions[i];
					var ps = map.GetData(p);
					if (ps == null)
						continue;
					foreach (var item in ps)
					{
						if (item == null)
							continue;
						var dist = (p.X - item.X) * (p.X - item.X) +
							(p.Y - item.Y) * (p.Y - item.Y) +
							(p.Z - item.Z) * (p.Z - item.Z);
						if (dist > 0 && dist < CrashDistance * CrashDistance && p.Satellite.Name.Trim() != item.Satellite.Name.Trim())
						{
							OnCrash(new List<Satellite>() { p.Satellite, item.Satellite }, Math.Sqrt(dist), SimulationTime);
							Stop();
						}
					}
				}
			}
			catch
			{

			}
			OnStep(SimulationTime);
			SimulationTime = SimulationTime.Add(SimulationStep);
		}

		public event EventHandler<SituationEventArgs> StepSituiation;
		public event EventHandler<CrashEventArgs> Crash;

		protected virtual void OnStep(TimeSpan EllapsedTime)
		{
			if (StepSituiation != null)
				StepSituiation(this, new SituationEventArgs(EllapsedTime));
		}

		protected virtual void OnCrash(List<Satellite> sats,Double dist, TimeSpan Time)
		{
			if (Crash != null)
				Crash(this, new CrashEventArgs(sats, dist, Time));
		}

	}
}
