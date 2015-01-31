using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace SatTracker
{
	class Vector3D
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		public Vector3D()
		{

		}

		public Vector3D(float x, float y, float z)
		{
			X = x; Y = y; Z = z;
		}

		public float Magnitude()
		{
			return (float)Math.Sqrt((this.X * this.X) + (this.Y * this.Y) + (this.Z * this.Z));
		}

		public Vector3D Normalize()
		{
			// Вы спросите, для чего эта ф-я? Мы должны убедиться, что наш вектор нормализирован.
			// Вектор нормализирован - значит, его длинна равна 1. Например,
			// вектор (2, 0, 0) после нормализации будет (1, 0, 0).

			// Вычислим величину нормали
			float magnitude = Magnitude();

			// Теперь у нас есть величина, и мы можем разделить наш вектор на его величину.
			// Это сделает длинну вектора равной единице, так с ним будет легче работать.
			var x = this.X / magnitude;
			var y = this.Y / magnitude;
			var z = this.Z / magnitude;

			return new Vector3D(x, y, z);
		}

		public Vector3D Cross(Vector3D vector)
		{
			var X = ((this.Y * vector.Z) - (this.Z * vector.Y));
			var Y = ((this.Z * vector.X) - (this.X * vector.Z));
			var Z = ((this.X * vector.Y) - (this.Y * vector.X));

			return new Vector3D(X, Y, Z);
		}

		public static Vector3D operator +(Vector3D a, Vector3D b)
		{
			if((a == null) || (b == null))
				throw new ArgumentNullException();
			return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static Vector3D operator -(Vector3D a, Vector3D b)
		{
			if((a == null) || (b == null))
				throw new ArgumentNullException();
			return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public static Vector3D operator -(Vector3D a)
		{
			if((a == null))
				throw new ArgumentNullException();
			return new Vector3D(-a.X, -a.Y, -a.Z);
		}

		public static Vector3D operator *(Vector3D a, float scalar)
		{
			if((a == null))
				throw new ArgumentNullException();
			return new Vector3D(a.X*scalar, a.Y*scalar, a.Z*scalar);
		}
	}

	abstract class Camera
	{
		protected Vector3D Position { get; set; }
		protected Vector3D View { get; set; }
		protected Vector3D Up { get; set; }

		public virtual void Look()
		{
			Glu.gluLookAt(Position.X, Position.Y, Position.Z,
						  View.X, View.Y, View.Z,
						  Up.X, Up.Y, Up.Z);
		}

	}

	class CenterViewCamera : Camera
	{
		public Vector3D Center { get; set; }
		public float Radius { get; set; }
		public float VAngle { get; set; }
		public float HAngle { get; set; }

		public override void Look()
		{
			
			base.View = GetViewVector();
			base.Up = GetUpVector();
			base.Position = Center - View*Radius;
			base.Look();
		}

		private Vector3D GetUpVector()
		{
			return new Vector3D(0, 1, 0);
		}

		private Vector3D GetViewVector()
		{
			var view = new Vector3D(1, 0, 0);

			view.X = - (float)(Math.Cos(HAngle) * Math.Cos(VAngle));
			
			view.Z = - (float)(Math.Sin(HAngle) * Math.Cos(VAngle));

			view.Y = - (float)Math.Sin(VAngle);

			return view;
		}
	}

	class FirstPersonViewCamera : Camera
	{
		public Vector3D Center { get; set; }
		public float VAngle { get; set; }
		public float HAngle { get; set; }

		public override void Look()
		{
			base.Position = Center;
			base.View = GetViewVector();
			base.Up = GetUpVector();
			base.Look();
		}

		private Vector3D GetUpVector()
		{
			return new Vector3D(0, 1, 0);
		}

		private Vector3D GetViewVector()
		{
			var view = new Vector3D(1, 0, 0);

			view.X = (float)(Math.Cos(HAngle) * Math.Cos(VAngle));

			view.Z = (float)(Math.Sin(HAngle) * Math.Cos(VAngle));

			view.Y = (float)Math.Sin(VAngle);

			return view;
		}
	}
}
