namespace GUI
{
    partial class MainWindowView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindowView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.GenerateBtn = new System.Windows.Forms.ToolStripButton();
            this.GenerateProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.DaslieBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.pridajPacientaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pridajHospitalizaciuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.ukonciHospitalizaciuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vymazHospitalizaciuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vymazPacientaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.Uloha1GroupBox = new System.Windows.Forms.GroupBox();
            this.uloha1TextBox = new System.Windows.Forms.TextBox();
            this.Uloha1Btn = new System.Windows.Forms.Button();
            this.Uloha1PatientLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Uloha2ID = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.Uloah2Pacient = new System.Windows.Forms.TextBox();
            this.Uloha2Btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.Uloha1GroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Uloha2ID)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GenerateBtn,
            this.GenerateProgressBar,
            this.toolStripSeparator3,
            this.DaslieBtn,
            this.toolStripSeparator4,
            this.toolStripSplitButton1,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(450, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.GenerateBtn.Image = ((System.Drawing.Image)(resources.GetObject("GenerateBtn.Image")));
            this.GenerateBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(79, 24);
            this.GenerateBtn.Text = "Generuj Data";
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // GenerateProgressBar
            // 
            this.GenerateProgressBar.Name = "GenerateProgressBar";
            this.GenerateProgressBar.Size = new System.Drawing.Size(100, 24);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // DaslieBtn
            // 
            this.DaslieBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DaslieBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pridajPacientaToolStripMenuItem,
            this.pridajHospitalizaciuToolStripMenuItem});
            this.DaslieBtn.Image = ((System.Drawing.Image)(resources.GetObject("DaslieBtn.Image")));
            this.DaslieBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DaslieBtn.Name = "DaslieBtn";
            this.DaslieBtn.Size = new System.Drawing.Size(33, 24);
            this.DaslieBtn.Text = "Dalsie";
            // 
            // pridajPacientaToolStripMenuItem
            // 
            this.pridajPacientaToolStripMenuItem.Name = "pridajPacientaToolStripMenuItem";
            this.pridajPacientaToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.pridajPacientaToolStripMenuItem.Text = "Pridaj Pacienta";
            this.pridajPacientaToolStripMenuItem.Click += new System.EventHandler(this.PridajPacientaToolStripMenuItem_Click);
            // 
            // pridajHospitalizaciuToolStripMenuItem
            // 
            this.pridajHospitalizaciuToolStripMenuItem.Name = "pridajHospitalizaciuToolStripMenuItem";
            this.pridajHospitalizaciuToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.pridajHospitalizaciuToolStripMenuItem.Text = "Pridaj Hospitalizaciu";
            this.pridajHospitalizaciuToolStripMenuItem.Click += new System.EventHandler(this.pridajHospitalizaciuToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ukonciHospitalizaciuToolStripMenuItem,
            this.vymazHospitalizaciuToolStripMenuItem,
            this.vymazPacientaToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(36, 24);
            this.toolStripSplitButton1.Text = "Zrus/Ukonci";
            // 
            // ukonciHospitalizaciuToolStripMenuItem
            // 
            this.ukonciHospitalizaciuToolStripMenuItem.Name = "ukonciHospitalizaciuToolStripMenuItem";
            this.ukonciHospitalizaciuToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.ukonciHospitalizaciuToolStripMenuItem.Text = "Ukonci hospitalizaciu";
            this.ukonciHospitalizaciuToolStripMenuItem.Click += new System.EventHandler(this.ukonciHospitalizaciuToolStripMenuItem_Click_1);
            // 
            // vymazHospitalizaciuToolStripMenuItem
            // 
            this.vymazHospitalizaciuToolStripMenuItem.Name = "vymazHospitalizaciuToolStripMenuItem";
            this.vymazHospitalizaciuToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.vymazHospitalizaciuToolStripMenuItem.Text = "Vymaz hospitalizaciu";
            this.vymazHospitalizaciuToolStripMenuItem.Click += new System.EventHandler(this.vymazHospitalizaciuToolStripMenuItem_Click);
            // 
            // vymazPacientaToolStripMenuItem
            // 
            this.vymazPacientaToolStripMenuItem.Name = "vymazPacientaToolStripMenuItem";
            this.vymazPacientaToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.vymazPacientaToolStripMenuItem.Text = "Vymaz Pacienta";
            this.vymazPacientaToolStripMenuItem.Click += new System.EventHandler(this.vymazPacientaToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // Uloha1GroupBox
            // 
            this.Uloha1GroupBox.Controls.Add(this.uloha1TextBox);
            this.Uloha1GroupBox.Controls.Add(this.Uloha1Btn);
            this.Uloha1GroupBox.Controls.Add(this.Uloha1PatientLabel);
            this.Uloha1GroupBox.Location = new System.Drawing.Point(12, 45);
            this.Uloha1GroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Uloha1GroupBox.Name = "Uloha1GroupBox";
            this.Uloha1GroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Uloha1GroupBox.Size = new System.Drawing.Size(416, 132);
            this.Uloha1GroupBox.TabIndex = 6;
            this.Uloha1GroupBox.TabStop = false;
            this.Uloha1GroupBox.Text = "Uloha 1";
            // 
            // uloha1TextBox
            // 
            this.uloha1TextBox.Location = new System.Drawing.Point(138, 35);
            this.uloha1TextBox.Name = "uloha1TextBox";
            this.uloha1TextBox.Size = new System.Drawing.Size(199, 23);
            this.uloha1TextBox.TabIndex = 5;
            // 
            // Uloha1Btn
            // 
            this.Uloha1Btn.Location = new System.Drawing.Point(206, 72);
            this.Uloha1Btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Uloha1Btn.Name = "Uloha1Btn";
            this.Uloha1Btn.Size = new System.Drawing.Size(131, 38);
            this.Uloha1Btn.TabIndex = 4;
            this.Uloha1Btn.Text = "Vypis udaje";
            this.Uloha1Btn.UseVisualStyleBackColor = true;
            this.Uloha1Btn.Click += new System.EventHandler(this.Uloha1Btn_Click);
            // 
            // Uloha1PatientLabel
            // 
            this.Uloha1PatientLabel.AutoSize = true;
            this.Uloha1PatientLabel.Location = new System.Drawing.Point(40, 38);
            this.Uloha1PatientLabel.Name = "Uloha1PatientLabel";
            this.Uloha1PatientLabel.Size = new System.Drawing.Size(46, 15);
            this.Uloha1PatientLabel.TabIndex = 3;
            this.Uloha1PatientLabel.Text = "Pacient";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Uloha2ID);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Uloah2Pacient);
            this.groupBox2.Controls.Add(this.Uloha2Btn);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 181);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(416, 184);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Uloha 2";
            // 
            // Uloha2ID
            // 
            this.Uloha2ID.Location = new System.Drawing.Point(138, 37);
            this.Uloha2ID.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Uloha2ID.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Uloha2ID.Name = "Uloha2ID";
            this.Uloha2ID.Size = new System.Drawing.Size(120, 23);
            this.Uloha2ID.TabIndex = 8;
            this.Uloha2ID.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "ID hospitalizacie";
            // 
            // Uloah2Pacient
            // 
            this.Uloah2Pacient.Location = new System.Drawing.Point(138, 66);
            this.Uloah2Pacient.Name = "Uloah2Pacient";
            this.Uloah2Pacient.Size = new System.Drawing.Size(199, 23);
            this.Uloah2Pacient.TabIndex = 5;
            // 
            // Uloha2Btn
            // 
            this.Uloha2Btn.Location = new System.Drawing.Point(211, 112);
            this.Uloha2Btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Uloha2Btn.Name = "Uloha2Btn";
            this.Uloha2Btn.Size = new System.Drawing.Size(131, 38);
            this.Uloha2Btn.TabIndex = 4;
            this.Uloha2Btn.Text = "Vypis udaje";
            this.Uloha2Btn.UseVisualStyleBackColor = true;
            this.Uloha2Btn.Click += new System.EventHandler(this.Uloha2Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pacient";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 39);
            this.button1.TabIndex = 17;
            this.button1.Text = "Sekvencny vypis";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainWindowView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 452);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Uloha1GroupBox);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainWindowView";
            this.Text = "Zdravotna Karta";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.Uloha1GroupBox.ResumeLayout(false);
            this.Uloha1GroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Uloha2ID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton GenerateBtn;
        private ToolStripProgressBar GenerateProgressBar;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripDropDownButton DaslieBtn;
        private ToolStripMenuItem pridajPacientaToolStripMenuItem;
        private ToolStripMenuItem pridajHospitalizaciuToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private GroupBox Uloha1GroupBox;
        private Button Uloha1Btn;
        private Label Uloha1PatientLabel;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripMenuItem ukonciHospitalizaciuToolStripMenuItem;
        private ToolStripMenuItem vymazHospitalizaciuToolStripMenuItem;
        private ToolStripMenuItem vymazPacientaToolStripMenuItem;
        private TextBox uloha1TextBox;
        private GroupBox groupBox2;
        private NumericUpDown Uloha2ID;
        private Label label2;
        private TextBox Uloah2Pacient;
        private Button Uloha2Btn;
        private Label label1;
        private Button button1;
    }
}