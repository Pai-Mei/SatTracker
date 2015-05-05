namespace SatTracker
{
	partial class fmViewer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.anT = new Tao.Platform.Windows.SimpleOpenGlControl();
			this.SuspendLayout();
			// 
			// anT
			// 
			this.anT.AccumBits = ((byte)(0));
			this.anT.AutoCheckErrors = false;
			this.anT.AutoFinish = false;
			this.anT.AutoMakeCurrent = true;
			this.anT.AutoSwapBuffers = true;
			this.anT.BackColor = System.Drawing.Color.Black;
			this.anT.ColorBits = ((byte)(32));
			this.anT.DepthBits = ((byte)(16));
			this.anT.Dock = System.Windows.Forms.DockStyle.Fill;
			this.anT.Location = new System.Drawing.Point(0, 0);
			this.anT.Name = "anT";
			this.anT.Size = new System.Drawing.Size(585, 362);
			this.anT.StencilBits = ((byte)(0));
			this.anT.TabIndex = 0;
			this.anT.Scroll += new System.Windows.Forms.ScrollEventHandler(this.anT_Scroll);
			this.anT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.anT_MouseDown);
			this.anT.MouseMove += new System.Windows.Forms.MouseEventHandler(this.anT_MouseMove);
			this.anT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.anT_MouseUp);
			// 
			// fmViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(585, 362);
			this.Controls.Add(this.anT);
			this.Name = "fmViewer";
			this.Text = "Визуализация";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmViewer_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		private Tao.Platform.Windows.SimpleOpenGlControl anT;
	}
}

