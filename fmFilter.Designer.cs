namespace SatTracker
{
	partial class fmFilter
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
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.comboBoxOrbit = new System.Windows.Forms.ComboBox();
			this.textBoxFrom = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxTo = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(12, 12);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(91, 17);
			this.radioButton1.TabIndex = 1;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Все объекты";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(12, 35);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(97, 17);
			this.radioButton2.TabIndex = 2;
			this.radioButton2.Text = "Все на орбите";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new System.Drawing.Point(12, 85);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(83, 17);
			this.radioButton3.TabIndex = 3;
			this.radioButton3.Text = "Все между:";
			this.radioButton3.UseVisualStyleBackColor = true;
			this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
			// 
			// comboBoxOrbit
			// 
			this.comboBoxOrbit.Enabled = false;
			this.comboBoxOrbit.FormattingEnabled = true;
			this.comboBoxOrbit.Items.AddRange(new object[] {
            "Низкая околоземная",
            "Геостационарная"});
			this.comboBoxOrbit.Location = new System.Drawing.Point(12, 58);
			this.comboBoxOrbit.Name = "comboBoxOrbit";
			this.comboBoxOrbit.Size = new System.Drawing.Size(160, 21);
			this.comboBoxOrbit.TabIndex = 4;
			// 
			// textBoxFrom
			// 
			this.textBoxFrom.Enabled = false;
			this.textBoxFrom.Location = new System.Drawing.Point(41, 112);
			this.textBoxFrom.Name = "textBoxFrom";
			this.textBoxFrom.Size = new System.Drawing.Size(47, 20);
			this.textBoxFrom.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 115);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(23, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "От:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(94, 115);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(25, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "До:";
			// 
			// textBoxTo
			// 
			this.textBoxTo.Enabled = false;
			this.textBoxTo.Location = new System.Drawing.Point(125, 112);
			this.textBoxTo.Name = "textBoxTo";
			this.textBoxTo.Size = new System.Drawing.Size(47, 20);
			this.textBoxTo.TabIndex = 8;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 139);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(160, 23);
			this.button1.TabIndex = 9;
			this.button1.Text = "ОК";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// fmFilter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(184, 174);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBoxTo);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxFrom);
			this.Controls.Add(this.comboBoxOrbit);
			this.Controls.Add(this.radioButton3);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.Name = "fmFilter";
			this.Text = "Фильтр";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmFilter_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.ComboBox comboBoxOrbit;
		private System.Windows.Forms.TextBox textBoxFrom;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxTo;
		private System.Windows.Forms.Button button1;
	}
}