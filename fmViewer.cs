using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;
using Zeptomoby.OrbitTools;

namespace SatTracker
{

	public partial class fmViewer : Form
	{
		public DateTime CurrentTimeStamp = DateTime.Now;//new DateTime(DateTime.Now.Year, 6, 21);

		public Eci SatPos;

		float DayAngle = 0;
		float YearAngle = 0;
		const float EarthR = 6378f;
		const float EarthPolarK = 0.0033528f;
		float GridP = 1000.0f;

		Image img;
		Bitmap image;
		System.Drawing.Imaging.BitmapData bitmapdata;
		uint texObject;
			
		Timer timer = new Timer();

		CenterViewCamera cam = new CenterViewCamera();
		private void InitGL()
		{
			Glut.glutInit();
			Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

			Gl.glClearColor(255, 255, 255, 1);

			Gl.glViewport(0, 0, anT.Width, anT.Height);

			Gl.glShadeModel(Gl.GL_SMOOTH);

			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();

			Glu.gluPerspective(45, (float)anT.Width / (float)anT.Height, 100, 10000000);

			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();

			
			Gl.glEnable(Gl.GL_DEPTH_TEST);
			Gl.glEnable(Gl.GL_LIGHTING);
			Gl.glEnable(Gl.GL_LIGHT0);
			Gl.glEnable(Gl.GL_NORMALIZE);
			Gl.glEnable(Gl.GL_DEPTH_TEST);
			Gl.glLightModelf(Gl.GL_LIGHT_MODEL_TWO_SIDE, Gl.GL_TRUE);

			Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
			Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);
			Gl.glEnable(Gl.GL_BLEND);
			Gl.glEnable(Gl.GL_LINE_SMOOTH);

			Gl.glDepthFunc(Gl.GL_LEQUAL);       // Тип теста глубины
			Gl.glHint(Gl.GL_PERSPECTIVE_CORRECTION_HINT, Gl.GL_NICEST); // Улучшенные вычисления перспективы
			Gl.glLineWidth(1.0f);

			cam.Radius = 15000;
			cam.HAngle = 0;
			cam.VAngle = 0;
			cam.Center = new Vector3D(0, 0, 0);
			//Position_Camera(0, 6, -15, 0, 3, 0, 0, 1, 0); // Вот тут в инициализации
			// укажем начальную позицию камеры, взгляда и вертикального вектора.

			img = Image.FromFile("EarthMap.jpg");
			bitmapdata = new System.Drawing.Imaging.BitmapData();
			image = new Bitmap(img, img.Width, img.Height);
			image.RotateFlip(RotateFlipType.RotateNoneFlipY);

			Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
			bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
				System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			Gl.glGenTextures(1, out texObject);
		}

		public fmViewer()
		{
			InitializeComponent();
			anT.InitializeContexts();
			timer.Interval = 50;
			timer.Tick += (s, e) =>
			{
				mouse_Events();
				cam.Look();
				Draw();
			};
			timer.Start();

			string str1 = "ISS (ZARYA)";
			string str2 = "1 25544U 98067A   15011.75047383  .00015267  00000-0  24546-3 0  7134";
			string str3 = "2 25544 051.6471 144.9217 0006128 239.4164 268.3488 15.53189343923690";

			Tle tle0 = new Tle(str1, str2, str3);
			Satellite sat = new Satellite(tle0);
			SatPos = sat.PositionEci(0);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			InitGL();
		}

		private void Draw()
		{
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
			Gl.glClearColor(0, 0, 0, 1);
			Gl.glLoadIdentity();
			Gl.glColor3i(255, 0, 0);


			this.Text = cam.Radius.ToString("#0.00");
			cam.Look(); // Обновляем взгляд камеры

			float[] light_diffuse = new float[] { 1.0f, 1.0f, 1.0f };
			float[] light_position = new float[] { 100000.0f, 0.0f, 0.0f, 0.0f };
			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_AMBIENT_AND_DIFFUSE, light_diffuse);
			Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, light_position);

			Gl.glPushMatrix();
			
			Gl.glEnable(Gl.GL_LIGHT0);

			DrawEarth();

			Gl.glPopMatrix();			

			var w = cam.Radius / 500;
			DrawAxis(cam.Radius*2, w);
		    DrawItem(w*2);


			Gl.glDisable(Gl.GL_LIGHT0);
			Gl.glFlush();

			anT.Invalidate();
		}

	    private void DrawItems(double w)
	    {
            float[] MatrixColor = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColor);
	        for (double j = -Math.PI; j < Math.PI; j += Math.PI / 64)
	        {
	            for (double i = -Math.PI; i < Math.PI; i += Math.PI/64)
	            {
	                float R = 10000.0f;
	                Gl.glPushMatrix();
	                Gl.glTranslated(Math.Sin(i) * Math.Cos(j) * R, Math.Cos(i) * Math.Cos(j) * R, Math.Sin(j)* R);
	                Glut.glutSolidSphere(w, 5, 5);
	                Gl.glPopMatrix();
	            }
	        }
	        Glut.glutSolidSphere(w, 5, 5);
        }

		private void DrawItem(double w)
		{
			float[] MatrixColor = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
			Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColor);
			Gl.glPushMatrix();
			Gl.glTranslated(SatPos.Position.X, SatPos.Position.Z, SatPos.Position.Y);
			Glut.glutSolidSphere(w, 5, 5);
			Gl.glPopMatrix();
		}

        private void DrawGrid(int x, float quad_size)
		{
			float[] MatrixOXOYColor = new float[] { 1, 1, 1, 1f };

			Gl.glBegin(Gl.GL_LINES);

			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixOXOYColor);
		
			// Рисуем сетку 1х1 вдоль осей
			for (float i = -x; i <= x; i += 1)
			{
				Gl.glBegin(Gl.GL_LINES);
				// Ось Х
				Gl.glVertex3f(-x * GridP * quad_size, 0, i * GridP * quad_size);
				Gl.glVertex3f(x * GridP * quad_size, 0, i * GridP * quad_size);

				// Ось Z
				Gl.glVertex3f(i * GridP * quad_size, 0, -x * GridP * quad_size);
				Gl.glVertex3f(i * GridP * quad_size, 0, x * GridP * quad_size);
				Gl.glEnd();
			}
		}

		private void DrawAxis(float x, float width)
		{
			float[] MatrixColorOX = new float[] { 1, 0, 0, 1 };
			float[] MatrixColorOZ = new float[] { 0, 1, 0, 1 };
			float[] MatrixColorOY = new float[] { 0, 0, 1, 1 };
			float[] MatrixOXOYColor = new float[] { 1, 1, 1, 0.5f };
			// x - количество или длина сетки, quad_size - размер клетки
			Gl.glPushMatrix(); // Рисуем оси координат, цвет объявлен в самом начале
			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColorOX);
			Gl.glTranslated((-x * 2) / 2, 0, 0);
			Gl.glRotated(90, 0, 1, 0);
			Glut.glutSolidCylinder(width, x * 2, 12, 12);
			Gl.glPopMatrix();

			Gl.glPushMatrix();
			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColorOZ);
			Gl.glTranslated(0, 0, (-x * 2) / 2);
			Glut.glutSolidCylinder(width, x * 2, 12, 12);
			Gl.glPopMatrix();

			Gl.glPushMatrix();
			Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColorOY);
			Gl.glTranslated(0, x / 2, 0);
			Gl.glRotated(90, 1, 0, 0);
			Glut.glutSolidCylinder(width, x, 12, 12);
			Gl.glPopMatrix();
		}

		private float GetYearAngle(DateTime CurrentDatetime)
		{
			var StartDate = new DateTime(CurrentTimeStamp.Year, 6, 21);
			var Delta = StartDate - CurrentTimeStamp;
			return Delta.Days / 365.25f * 360;
		}

		private float GetDayAngle(DateTime CurrentDateTime)
		{
			var YaerAngle = GetYearAngle(CurrentDateTime);
			var Minutes = CurrentTimeStamp.TimeOfDay.TotalMinutes - 720;
			return (float)(Minutes / (1440) * 360) - YearAngle;
		}

		private void DrawEarth()
		{
			float[] MatrixColor = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
			//CurrentTimeStamp = CurrentTimeStamp.AddDays(1);
			//CurrentTimeStamp = CurrentTimeStamp.AddMinutes(10);
			Gl.glRotated(270, 1, 0, 0);

			Gl.glRotated(YearAngle, 0, 0, 1);
			YearAngle = GetYearAngle(CurrentTimeStamp);// += 1f;

			Gl.glRotated(-23.439281, 0, 1, 0);	//наклон оси Земли относительно оси Солнца
			Gl.glRotated(90, 0, 0, 1);

			Gl.glScalef(1f, 1f, 1f - EarthPolarK);

			Gl.glRotated(DayAngle, 0, 0, 1);
			DayAngle = GetDayAngle(CurrentTimeStamp);//+= 10;
			Gl.glEnable(Gl.GL_TEXTURE_2D);
			// создаем привязку к только что созданной текстуре
			//Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);
			Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColor);
			// устанавливаем режим фильтрации и повторения текстуры
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
			Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
			Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_MODULATE);

			Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, (int)Gl.GL_RGB8, image.Width, image.Height, 0, Gl.GL_BGR_EXT, Gl.GL_UNSIGNED_BYTE, bitmapdata.Scan0);

			Gl.glEnable(Gl.GL_BLEND);
			Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

			Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);

			// рисуем сферу с помощью библиотеки FreeGLUT 
			//Glut.glutSolidSphere(EarthR, 90, 90);
			Glu.GLUquadric quad = Glu.gluNewQuadric();
			Glu.gluQuadricTexture(quad, 1);
			Glu.gluSphere(quad, EarthR, 90, 90);

			Gl.glDisable(Gl.GL_TEXTURE_2D);
		}

		private void mouse_Events()
		{
			if (mouseRotate == true) // Если нажата левая кнопка мыши
			{
				anT.Cursor = System.Windows.Forms.Cursors.SizeAll; //меняем указатель

				cam.VAngle += (float)(myMouseXcoordVar - myMouseXcoord) / 100;
				cam.HAngle += (float)(myMouseYcoordVar - myMouseYcoord) / 100;

				//cam.Rotate_Position((float)(myMouseYcoordVar - myMouseYcoord), 0, 1, 0); // крутим камеру, в моем случае вид у нее от третьего лица
				//cam.Vert_Rotate_Position((float)(myMouseXcoordVar - myMouseXcoord));

				myMouseYcoord = myMouseYcoordVar;
				myMouseXcoord = myMouseXcoordVar;
			}
			else if (mouseMove == true)
			{
				if (Math.Abs(myMouseXcoordVar - myMouseXcoord) > 20)
					cam.Radius += cam.Radius * (float)(myMouseXcoordVar - myMouseXcoord) / 1000;
				if (cam.Radius < EarthR + 20)
					cam.Radius = EarthR + 20;
			}
			else
			{
				anT.Cursor = System.Windows.Forms.Cursors.Default; // возвращаем курсор
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

			Gl.glLoadIdentity();
			Gl.glColor3f(1.0f, 0, 0);

			Gl.glPushMatrix();
			Gl.glTranslated(0, 0, -6);
			Gl.glRotated(45, 1, 1, 0);

			// рисуем сферу с помощью библиотеки FreeGLUT 
			Glut.glutWireSphere(2, 32, 32);

			Gl.glPopMatrix();
			Gl.glFlush();
			anT.Invalidate();
		}

		private void anT_MouseMove(object sender, MouseEventArgs e)
		{
			myMouseXcoordVar = e.Y;
			myMouseYcoordVar = e.X;
			WheelDelta = e.Delta;
		}

		private void anT_MouseUp(object sender, MouseEventArgs e)
		{
			mouseRotate = false;
			mouseMove = false;
		}

		private void anT_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				mouseRotate = true; // Если нажата левая кнопка мыши

			if (e.Button == MouseButtons.Left)
				mouseMove = true; // Если нажата средняя кнопка мыши
			WheelDelta = e.Delta;
			myMouseYcoord = e.X; // Передаем в нашу глобальную переменную позицию мыши по Х
			myMouseXcoord = e.Y;
		}

		public int WheelDelta { get; set; }

		public int myMouseXcoordVar { get; set; }

		public int myMouseYcoordVar { get; set; }

		public bool mouseRotate { get; set; }

		public bool mouseMove { get; set; }

		public int myMouseYcoord { get; set; }

		public int myMouseXcoord { get; set; }

		public int rot_cam_X { get; set; }

		private void Form1_Resize(object sender, EventArgs e)
		{
			Gl.glViewport(0, 0, anT.Width, anT.Height);
			Gl.glMatrixMode(Gl.GL_PROJECTION);
			Gl.glLoadIdentity();

			Glu.gluPerspective(45, (float)anT.Width / (float)anT.Height, 0.1, Double.MaxValue);

			Gl.glMatrixMode(Gl.GL_MODELVIEW);
			Gl.glLoadIdentity();
		}

		private void anT_Scroll(object sender, ScrollEventArgs e)
		{
			var Delta = e.NewValue - e.OldValue;
			WheelDelta = Delta;
		}
	}



	
}
