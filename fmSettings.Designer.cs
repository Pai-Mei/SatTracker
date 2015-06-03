namespace SatTracker
{
	partial class fmSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonAxisZ = new System.Windows.Forms.Button();
            this.buttonAxisY = new System.Windows.Forms.Button();
            this.buttonAxisX = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonObjects = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonOrbit = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxSimView = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxTimeUnits = new System.Windows.Forms.ComboBox();
            this.numberStep = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxDist = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberStep)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Цвет объектов:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonAxisZ);
            this.groupBox1.Controls.Add(this.buttonAxisY);
            this.groupBox1.Controls.Add(this.buttonAxisX);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 109);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Цвет осей";
            // 
            // buttonAxisZ
            // 
            this.buttonAxisZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAxisZ.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAxisZ.Location = new System.Drawing.Point(67, 78);
            this.buttonAxisZ.Name = "buttonAxisZ";
            this.buttonAxisZ.Size = new System.Drawing.Size(127, 23);
            this.buttonAxisZ.TabIndex = 7;
            this.buttonAxisZ.UseVisualStyleBackColor = true;
            this.buttonAxisZ.Click += new System.EventHandler(this.buttonAxisZ_Click);
            // 
            // buttonAxisY
            // 
            this.buttonAxisY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAxisY.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAxisY.Location = new System.Drawing.Point(67, 49);
            this.buttonAxisY.Name = "buttonAxisY";
            this.buttonAxisY.Size = new System.Drawing.Size(127, 23);
            this.buttonAxisY.TabIndex = 6;
            this.buttonAxisY.UseVisualStyleBackColor = true;
            this.buttonAxisY.Click += new System.EventHandler(this.buttonAxisY_Click);
            // 
            // buttonAxisX
            // 
            this.buttonAxisX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAxisX.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAxisX.Location = new System.Drawing.Point(67, 20);
            this.buttonAxisX.Name = "buttonAxisX";
            this.buttonAxisX.Size = new System.Drawing.Size(127, 23);
            this.buttonAxisX.TabIndex = 5;
            this.buttonAxisX.UseVisualStyleBackColor = true;
            this.buttonAxisX.Click += new System.EventHandler(this.buttonAxisX_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Z:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Y:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Х:";
            // 
            // buttonObjects
            // 
            this.buttonObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonObjects.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonObjects.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonObjects.Location = new System.Drawing.Point(101, 4);
            this.buttonObjects.Name = "buttonObjects";
            this.buttonObjects.Size = new System.Drawing.Size(105, 23);
            this.buttonObjects.TabIndex = 8;
            this.buttonObjects.UseVisualStyleBackColor = true;
            this.buttonObjects.Click += new System.EventHandler(this.buttonObjects_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Цвет орбит:";
            // 
            // buttonOrbit
            // 
            this.buttonOrbit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOrbit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOrbit.Location = new System.Drawing.Point(101, 33);
            this.buttonOrbit.Name = "buttonOrbit";
            this.buttonOrbit.Size = new System.Drawing.Size(105, 23);
            this.buttonOrbit.TabIndex = 10;
            this.buttonOrbit.UseVisualStyleBackColor = true;
            this.buttonOrbit.Click += new System.EventHandler(this.buttonOrbit_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(137, 284);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(56, 284);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // checkBoxSimView
            // 
            this.checkBoxSimView.AutoSize = true;
            this.checkBoxSimView.Location = new System.Drawing.Point(6, 19);
            this.checkBoxSimView.Name = "checkBoxSimView";
            this.checkBoxSimView.Size = new System.Drawing.Size(150, 17);
            this.checkBoxSimView.TabIndex = 13;
            this.checkBoxSimView.Text = "Визулизация симуляции";
            this.checkBoxSimView.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxTimeUnits);
            this.groupBox2.Controls.Add(this.numberStep);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textBoxDist);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.checkBoxSimView);
            this.groupBox2.Location = new System.Drawing.Point(12, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Симуляция";
            // 
            // comboBoxTimeUnits
            // 
            this.comboBoxTimeUnits.FormattingEnabled = true;
            this.comboBoxTimeUnits.Items.AddRange(new object[] {
            "сек.",
            "мин.",
            "ч."});
            this.comboBoxTimeUnits.Location = new System.Drawing.Point(114, 68);
            this.comboBoxTimeUnits.Name = "comboBoxTimeUnits";
            this.comboBoxTimeUnits.Size = new System.Drawing.Size(80, 21);
            this.comboBoxTimeUnits.TabIndex = 18;
            this.comboBoxTimeUnits.Text = "мин.";
            // 
            // numberStep
            // 
            this.numberStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numberStep.Location = new System.Drawing.Point(36, 69);
            this.numberStep.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numberStep.Name = "numberStep";
            this.numberStep.Size = new System.Drawing.Size(72, 20);
            this.numberStep.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Шаг:";
            // 
            // textBoxDist
            // 
            this.textBoxDist.Location = new System.Drawing.Point(89, 42);
            this.textBoxDist.Name = "textBoxDist";
            this.textBoxDist.Size = new System.Drawing.Size(105, 20);
            this.textBoxDist.TabIndex = 15;
            this.textBoxDist.Text = "0,1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Точность(км):";
            // 
            // fmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 319);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOrbit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonObjects);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmSettings";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.fmSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberStep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonAxisZ;
		private System.Windows.Forms.Button buttonAxisY;
		private System.Windows.Forms.Button buttonAxisX;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonObjects;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button buttonOrbit;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.CheckBox checkBoxSimView;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox comboBoxTimeUnits;
		private System.Windows.Forms.NumericUpDown numberStep;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxDist;
		private System.Windows.Forms.Label label6;
	}
}