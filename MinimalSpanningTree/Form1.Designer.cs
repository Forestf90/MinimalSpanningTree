namespace MinimalSpanningTree
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.generateButton = new System.Windows.Forms.Button();
            this.conectivityButton = new System.Windows.Forms.Button();
            this.mstButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioGrid = new System.Windows.Forms.RadioButton();
            this.radioCircle = new System.Windows.Forms.RadioButton();
            this.radioRandom = new System.Windows.Forms.RadioButton();
            this.numericMin = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericMax = new System.Windows.Forms.NumericUpDown();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.checkCon = new System.Windows.Forms.CheckBox();
            this.radioPropability = new System.Windows.Forms.RadioButton();
            this.radioRange = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMax)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Location = new System.Drawing.Point(407, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(369, 393);
            this.panel1.TabIndex = 0;
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.DoubleClick += new System.EventHandler(this.panel1_DoubleClick);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(30, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 67);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(267, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(62, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter number of vertices";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkCon);
            this.groupBox2.Controls.Add(this.radioRange);
            this.groupBox2.Controls.Add(this.radioPropability);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numericMax);
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numericMin);
            this.groupBox2.Location = new System.Drawing.Point(30, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(303, 111);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(78, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "50%";
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.Window;
            this.trackBar1.Location = new System.Drawing.Point(20, 43);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(147, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.Value = 50;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // generateButton
            // 
            this.generateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.generateButton.Location = new System.Drawing.Point(203, 202);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(135, 23);
            this.generateButton.TabIndex = 3;
            this.generateButton.Text = "Generate graph";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // conectivityButton
            // 
            this.conectivityButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.conectivityButton.Location = new System.Drawing.Point(203, 231);
            this.conectivityButton.Name = "conectivityButton";
            this.conectivityButton.Size = new System.Drawing.Size(135, 23);
            this.conectivityButton.TabIndex = 8;
            this.conectivityButton.Text = "Check conectivity";
            this.conectivityButton.UseVisualStyleBackColor = true;
            this.conectivityButton.Click += new System.EventHandler(this.conectivityButton_Click);
            // 
            // mstButton
            // 
            this.mstButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mstButton.Location = new System.Drawing.Point(111, 415);
            this.mstButton.Name = "mstButton";
            this.mstButton.Size = new System.Drawing.Size(142, 28);
            this.mstButton.TabIndex = 12;
            this.mstButton.Text = "MST Animation";
            this.mstButton.UseVisualStyleBackColor = true;
            this.mstButton.Click += new System.EventHandler(this.mstButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.Location = new System.Drawing.Point(729, 411);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(47, 20);
            this.clearButton.TabIndex = 14;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(407, 411);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(49, 17);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Grid";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(30, 308);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(303, 96);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Visible = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(20, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(266, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Edit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(20, 35);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(267, 20);
            this.textBox2.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(78, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Edit edge cost (0, 20)";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioGrid);
            this.groupBox4.Controls.Add(this.radioCircle);
            this.groupBox4.Controls.Add(this.radioRandom);
            this.groupBox4.Location = new System.Drawing.Point(30, 202);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(167, 100);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Drawing";
            // 
            // radioGrid
            // 
            this.radioGrid.AutoSize = true;
            this.radioGrid.Location = new System.Drawing.Point(6, 80);
            this.radioGrid.Name = "radioGrid";
            this.radioGrid.Size = new System.Drawing.Size(44, 17);
            this.radioGrid.TabIndex = 2;
            this.radioGrid.Text = "Grid";
            this.radioGrid.UseVisualStyleBackColor = true;
            // 
            // radioCircle
            // 
            this.radioCircle.AutoSize = true;
            this.radioCircle.Location = new System.Drawing.Point(6, 54);
            this.radioCircle.Name = "radioCircle";
            this.radioCircle.Size = new System.Drawing.Size(51, 17);
            this.radioCircle.TabIndex = 1;
            this.radioCircle.Text = "Circle";
            this.radioCircle.UseVisualStyleBackColor = true;
            // 
            // radioRandom
            // 
            this.radioRandom.AutoSize = true;
            this.radioRandom.Checked = true;
            this.radioRandom.Location = new System.Drawing.Point(6, 28);
            this.radioRandom.Name = "radioRandom";
            this.radioRandom.Size = new System.Drawing.Size(65, 17);
            this.radioRandom.TabIndex = 0;
            this.radioRandom.TabStop = true;
            this.radioRandom.Text = "Random";
            this.radioRandom.UseVisualStyleBackColor = true;
            // 
            // numericMin
            // 
            this.numericMin.Enabled = false;
            this.numericMin.Location = new System.Drawing.Point(253, 36);
            this.numericMin.Margin = new System.Windows.Forms.Padding(2);
            this.numericMin.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericMin.Name = "numericMin";
            this.numericMin.Size = new System.Drawing.Size(45, 20);
            this.numericMin.TabIndex = 18;
            this.numericMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(173, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Min edges:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(173, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Max edges:";
            // 
            // numericMax
            // 
            this.numericMax.Enabled = false;
            this.numericMax.Location = new System.Drawing.Point(253, 64);
            this.numericMax.Margin = new System.Windows.Forms.Padding(2);
            this.numericMax.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericMax.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericMax.Name = "numericMax";
            this.numericMax.Size = new System.Drawing.Size(44, 20);
            this.numericMax.TabIndex = 20;
            this.numericMax.Tag = "";
            this.numericMax.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // buttonNext
            // 
            this.buttonNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNext.Location = new System.Drawing.Point(259, 415);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(74, 28);
            this.buttonNext.TabIndex = 22;
            this.buttonNext.Text = "Next Step";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrev
            // 
            this.buttonPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrev.Location = new System.Drawing.Point(30, 412);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(73, 28);
            this.buttonPrev.TabIndex = 23;
            this.buttonPrev.Text = "Prev step";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // checkCon
            // 
            this.checkCon.AutoSize = true;
            this.checkCon.Enabled = false;
            this.checkCon.Location = new System.Drawing.Point(176, 88);
            this.checkCon.Name = "checkCon";
            this.checkCon.Size = new System.Drawing.Size(107, 17);
            this.checkCon.TabIndex = 24;
            this.checkCon.Text = "Force conectivity";
            this.checkCon.UseVisualStyleBackColor = true;
            // 
            // radioPropability
            // 
            this.radioPropability.AutoSize = true;
            this.radioPropability.Checked = true;
            this.radioPropability.Location = new System.Drawing.Point(20, 19);
            this.radioPropability.Name = "radioPropability";
            this.radioPropability.Size = new System.Drawing.Size(100, 17);
            this.radioPropability.TabIndex = 4;
            this.radioPropability.TabStop = true;
            this.radioPropability.Text = "Edge propability";
            this.radioPropability.UseVisualStyleBackColor = true;
            this.radioPropability.CheckedChanged += new System.EventHandler(this.radioPropability_CheckedChanged);
            // 
            // radioRange
            // 
            this.radioRange.AutoSize = true;
            this.radioRange.Location = new System.Drawing.Point(168, 19);
            this.radioRange.Name = "radioRange";
            this.radioRange.Size = new System.Drawing.Size(80, 17);
            this.radioRange.TabIndex = 5;
            this.radioRange.Text = "Edge range";
            this.radioRange.UseVisualStyleBackColor = true;
            this.radioRange.CheckedChanged += new System.EventHandler(this.radioRange_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(799, 474);
            this.Controls.Add(this.buttonPrev);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.mstButton);
            this.Controls.Add(this.conectivityButton);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(815, 513);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MST 2020 Edition";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button conectivityButton;
        private System.Windows.Forms.Button mstButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioRandom;
        private System.Windows.Forms.RadioButton radioGrid;
        private System.Windows.Forms.RadioButton radioCircle;
        private System.Windows.Forms.NumericUpDown numericMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericMax;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.CheckBox checkCon;
        private System.Windows.Forms.RadioButton radioRange;
        private System.Windows.Forms.RadioButton radioPropability;
    }
}

