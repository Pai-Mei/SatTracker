using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeptomoby.OrbitTools;


namespace SatTracker
{
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

			public CrashEventArgs()
			{

			}

			public CrashEventArgs(List<Satellite> Sats, TimeSpan Time)
			{
				this.Sats = Sats;
				this.Time = Time;
			}
		}

		private List<Satellite> m_sats;
		private Eci[] positions;

		private TimeSpan SimulationTime;
		private DateTime SimulationStart;
		private TimeSpan SimulationStep;
		private double CrashDistance;

		public CrashEmulation(List<Satellite> sats, DateTime SimStartTime, TimeSpan step, Double CrashDist)
		{
			this.SimulationStart = SimStartTime;
			this.m_sats = sats;
			this.SimulationStep = step;
			this.CrashDistance = CrashDist;
		}

		private bool flagStop = false;

		public void Start()
		{
			flagStop = false;
			var worker = new BackgroundWorker();
			worker.DoWork += (s, args) => { while(!flagStop) Step(); };
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
				positions = new Eci[m_sats.Count];
			Parallel.For(0, m_sats.Count, (i) =>
			{
				try
				{
					var startEpoch = SimulationStart.Subtract(m_sats[i].Orbit.EpochTime);
					while (startEpoch > m_sats[i].Orbit.Period)
						startEpoch = startEpoch.Subtract(m_sats[i].Orbit.Period);
					positions[i] = m_sats[i].PositionEci(startEpoch.TotalMinutes + SimulationTime.TotalMinutes);
				}
				catch
				{

				}
			});
			for (int i = 0; i < positions.Length; i++)
			{
				for (int j = 0; j < positions.Length; j++)
				{
					if (i == j)
						continue;
					var v = new Vector(positions[i].Position);
					v.Sub(positions[j].Position);
					if (v.Magnitude() < CrashDistance)
						OnCrash(new List<Satellite>() { m_sats[i], m_sats[j] }, SimulationTime);
				}
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

		protected virtual void OnCrash(List<Satellite> sats, TimeSpan Time)
		{
			if (Crash != null)
				Crash(this, new CrashEventArgs(sats, Time));
		}

	}
}
