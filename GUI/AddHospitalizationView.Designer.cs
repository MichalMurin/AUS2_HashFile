namespace GUI
{
    partial class AddHospitalizationView
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EndDateLabel = new System.Windows.Forms.Label();
            this.EndDatePicker = new System.Windows.Forms.DateTimePicker();
            this.StartDateLabel = new System.Windows.Forms.Label();
            this.DiagnosisLabel = new System.Windows.Forms.Label();
            this.PatientLabel = new System.Windows.Forms.Label();
            this.StartDatePicker = new System.Windows.Forms.DateTimePicker();
            this.DiagnosisTextBox = new System.Windows.Forms.TextBox();
            this.HospitalizationCancelBtn = new System.Windows.Forms.Button();
            this.AddHospitalizationBtn = new System.Windows.Forms.Button();
            this.rodneCislo = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rodneCislo);
            this.groupBox1.Controls.Add(this.EndDateLabel);
            this.groupBox1.Controls.Add(this.EndDatePicker);
            this.groupBox1.Controls.Add(this.StartDateLabel);
            this.groupBox1.Controls.Add(this.DiagnosisLabel);
            this.groupBox1.Controls.Add(this.PatientLabel);
            this.groupBox1.Controls.Add(this.StartDatePicker);
            this.groupBox1.Controls.Add(this.DiagnosisTextBox);
            this.groupBox1.Location = new System.Drawing.Point(24, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 350);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zadajte udaje";
            // 
            // EndDateLabel
            // 
            this.EndDateLabel.AutoSize = true;
            this.EndDateLabel.Location = new System.Drawing.Point(18, 249);
            this.EndDateLabel.Name = "EndDateLabel";
            this.EndDateLabel.Size = new System.Drawing.Size(117, 15);
            this.EndDateLabel.TabIndex = 10;
            this.EndDateLabel.Text = "Koniec hospitalizacie";
            // 
            // EndDatePicker
            // 
            this.EndDatePicker.Checked = false;
            this.EndDatePicker.Location = new System.Drawing.Point(18, 267);
            this.EndDatePicker.Name = "EndDatePicker";
            this.EndDatePicker.ShowCheckBox = true;
            this.EndDatePicker.Size = new System.Drawing.Size(357, 23);
            this.EndDatePicker.TabIndex = 4;
            // 
            // StartDateLabel
            // 
            this.StartDateLabel.AutoSize = true;
            this.StartDateLabel.Location = new System.Drawing.Point(18, 188);
            this.StartDateLabel.Name = "StartDateLabel";
            this.StartDateLabel.Size = new System.Drawing.Size(126, 15);
            this.StartDateLabel.TabIndex = 9;
            this.StartDateLabel.Text = "Zaciatok hospitalizacie";
            // 
            // DiagnosisLabel
            // 
            this.DiagnosisLabel.AutoSize = true;
            this.DiagnosisLabel.Location = new System.Drawing.Point(7, 68);
            this.DiagnosisLabel.Name = "DiagnosisLabel";
            this.DiagnosisLabel.Size = new System.Drawing.Size(56, 15);
            this.DiagnosisLabel.TabIndex = 8;
            this.DiagnosisLabel.Text = "Diagnoza";
            // 
            // PatientLabel
            // 
            this.PatientLabel.AutoSize = true;
            this.PatientLabel.Location = new System.Drawing.Point(7, 25);
            this.PatientLabel.Name = "PatientLabel";
            this.PatientLabel.Size = new System.Drawing.Size(46, 15);
            this.PatientLabel.TabIndex = 6;
            this.PatientLabel.Text = "Pacient";
            // 
            // StartDatePicker
            // 
            this.StartDatePicker.Location = new System.Drawing.Point(18, 206);
            this.StartDatePicker.Name = "StartDatePicker";
            this.StartDatePicker.Size = new System.Drawing.Size(357, 23);
            this.StartDatePicker.TabIndex = 3;
            // 
            // DiagnosisTextBox
            // 
            this.DiagnosisTextBox.Location = new System.Drawing.Point(81, 65);
            this.DiagnosisTextBox.Multiline = true;
            this.DiagnosisTextBox.Name = "DiagnosisTextBox";
            this.DiagnosisTextBox.Size = new System.Drawing.Size(294, 99);
            this.DiagnosisTextBox.TabIndex = 0;
            // 
            // HospitalizationCancelBtn
            // 
            this.HospitalizationCancelBtn.Location = new System.Drawing.Point(30, 400);
            this.HospitalizationCancelBtn.Name = "HospitalizationCancelBtn";
            this.HospitalizationCancelBtn.Size = new System.Drawing.Size(140, 45);
            this.HospitalizationCancelBtn.TabIndex = 1;
            this.HospitalizationCancelBtn.Text = "Zrusit";
            this.HospitalizationCancelBtn.UseVisualStyleBackColor = true;
            this.HospitalizationCancelBtn.Click += new System.EventHandler(this.HospitalizationCancelBtn_Click);
            // 
            // AddHospitalizationBtn
            // 
            this.AddHospitalizationBtn.Location = new System.Drawing.Point(258, 400);
            this.AddHospitalizationBtn.Name = "AddHospitalizationBtn";
            this.AddHospitalizationBtn.Size = new System.Drawing.Size(140, 45);
            this.AddHospitalizationBtn.TabIndex = 2;
            this.AddHospitalizationBtn.Text = "Pridat";
            this.AddHospitalizationBtn.UseVisualStyleBackColor = true;
            this.AddHospitalizationBtn.Click += new System.EventHandler(this.AddHospitalizationBtn_Click);
            // 
            // rodneCislo
            // 
            this.rodneCislo.Location = new System.Drawing.Point(81, 23);
            this.rodneCislo.Name = "rodneCislo";
            this.rodneCislo.Size = new System.Drawing.Size(292, 23);
            this.rodneCislo.TabIndex = 11;
            // 
            // AddHospitalizationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 476);
            this.Controls.Add(this.AddHospitalizationBtn);
            this.Controls.Add(this.HospitalizationCancelBtn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddHospitalizationView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hospitalizacia";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private TextBox DiagnosisTextBox;
        private DateTimePicker EndDatePicker;
        private DateTimePicker StartDatePicker;
        private Label StartDateLabel;
        private Label DiagnosisLabel;
        private Label PatientLabel;
        private Button HospitalizationCancelBtn;
        private Button AddHospitalizationBtn;
        private Label EndDateLabel;
        private TextBox rodneCislo;
    }
}