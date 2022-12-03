namespace GUI
{
    partial class AddPatientView
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
            this.components = new System.ComponentModel.Container();
            this.AddPatientBtn = new System.Windows.Forms.Button();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.SureNameTextBox = new System.Windows.Forms.TextBox();
            this.BirthNumberTextBox = new System.Windows.Forms.TextBox();
            this.AddPatienLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.PatientDataGroupBox = new System.Windows.Forms.GroupBox();
            this.InsuranceLabel = new System.Windows.Forms.Label();
            this.BirthNumLabel = new System.Windows.Forms.Label();
            this.SureNameLabel = new System.Windows.Forms.Label();
            this.CancelAddPatientBtn = new System.Windows.Forms.Button();
            this.presenterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kodPoistovne = new System.Windows.Forms.NumericUpDown();
            this.PatientDataGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.presenterBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kodPoistovne)).BeginInit();
            this.SuspendLayout();
            // 
            // AddPatientBtn
            // 
            this.AddPatientBtn.Location = new System.Drawing.Point(168, 209);
            this.AddPatientBtn.Name = "AddPatientBtn";
            this.AddPatientBtn.Size = new System.Drawing.Size(128, 33);
            this.AddPatientBtn.TabIndex = 0;
            this.AddPatientBtn.Text = "Pridat";
            this.AddPatientBtn.UseVisualStyleBackColor = true;
            this.AddPatientBtn.Click += new System.EventHandler(this.AddPatientBtn_Click);
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(125, 25);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(159, 23);
            this.NameTextBox.TabIndex = 1;
            // 
            // SureNameTextBox
            // 
            this.SureNameTextBox.Location = new System.Drawing.Point(125, 54);
            this.SureNameTextBox.Name = "SureNameTextBox";
            this.SureNameTextBox.Size = new System.Drawing.Size(159, 23);
            this.SureNameTextBox.TabIndex = 2;
            // 
            // BirthNumberTextBox
            // 
            this.BirthNumberTextBox.Location = new System.Drawing.Point(125, 81);
            this.BirthNumberTextBox.Name = "BirthNumberTextBox";
            this.BirthNumberTextBox.Size = new System.Drawing.Size(159, 23);
            this.BirthNumberTextBox.TabIndex = 3;
            // 
            // AddPatienLabel
            // 
            this.AddPatienLabel.AutoSize = true;
            this.AddPatienLabel.Location = new System.Drawing.Point(12, 9);
            this.AddPatienLabel.Name = "AddPatienLabel";
            this.AddPatienLabel.Size = new System.Drawing.Size(125, 15);
            this.AddPatienLabel.TabIndex = 8;
            this.AddPatienLabel.Text = "Vypiste udaje pacienta";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(30, 25);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(38, 15);
            this.NameLabel.TabIndex = 9;
            this.NameLabel.Text = "Meno";
            // 
            // PatientDataGroupBox
            // 
            this.PatientDataGroupBox.Controls.Add(this.kodPoistovne);
            this.PatientDataGroupBox.Controls.Add(this.InsuranceLabel);
            this.PatientDataGroupBox.Controls.Add(this.BirthNumLabel);
            this.PatientDataGroupBox.Controls.Add(this.SureNameLabel);
            this.PatientDataGroupBox.Controls.Add(this.NameLabel);
            this.PatientDataGroupBox.Controls.Add(this.BirthNumberTextBox);
            this.PatientDataGroupBox.Controls.Add(this.SureNameTextBox);
            this.PatientDataGroupBox.Controls.Add(this.NameTextBox);
            this.PatientDataGroupBox.Location = new System.Drawing.Point(12, 40);
            this.PatientDataGroupBox.Name = "PatientDataGroupBox";
            this.PatientDataGroupBox.Size = new System.Drawing.Size(299, 157);
            this.PatientDataGroupBox.TabIndex = 10;
            this.PatientDataGroupBox.TabStop = false;
            // 
            // InsuranceLabel
            // 
            this.InsuranceLabel.AutoSize = true;
            this.InsuranceLabel.Location = new System.Drawing.Point(30, 112);
            this.InsuranceLabel.Name = "InsuranceLabel";
            this.InsuranceLabel.Size = new System.Drawing.Size(59, 15);
            this.InsuranceLabel.TabIndex = 12;
            this.InsuranceLabel.Text = "Poistovna";
            // 
            // BirthNumLabel
            // 
            this.BirthNumLabel.AutoSize = true;
            this.BirthNumLabel.Location = new System.Drawing.Point(30, 83);
            this.BirthNumLabel.Name = "BirthNumLabel";
            this.BirthNumLabel.Size = new System.Drawing.Size(68, 15);
            this.BirthNumLabel.TabIndex = 11;
            this.BirthNumLabel.Text = "Rodne cislo";
            // 
            // SureNameLabel
            // 
            this.SureNameLabel.AutoSize = true;
            this.SureNameLabel.Location = new System.Drawing.Point(30, 54);
            this.SureNameLabel.Name = "SureNameLabel";
            this.SureNameLabel.Size = new System.Drawing.Size(59, 15);
            this.SureNameLabel.TabIndex = 10;
            this.SureNameLabel.Text = "Preizvisko";
            // 
            // CancelAddPatientBtn
            // 
            this.CancelAddPatientBtn.Location = new System.Drawing.Point(24, 209);
            this.CancelAddPatientBtn.Name = "CancelAddPatientBtn";
            this.CancelAddPatientBtn.Size = new System.Drawing.Size(128, 33);
            this.CancelAddPatientBtn.TabIndex = 11;
            this.CancelAddPatientBtn.Text = "Zrusit";
            this.CancelAddPatientBtn.UseVisualStyleBackColor = true;
            this.CancelAddPatientBtn.Click += new System.EventHandler(this.CancelAddPatientBtn_Click);
            // 
            // presenterBindingSource
            // 
            this.presenterBindingSource.DataSource = typeof(void);
            // 
            // kodPoistovne
            // 
            this.kodPoistovne.Location = new System.Drawing.Point(126, 110);
            this.kodPoistovne.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.kodPoistovne.Name = "kodPoistovne";
            this.kodPoistovne.Size = new System.Drawing.Size(156, 23);
            this.kodPoistovne.TabIndex = 13;
            // 
            // AddPatientView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 268);
            this.Controls.Add(this.CancelAddPatientBtn);
            this.Controls.Add(this.PatientDataGroupBox);
            this.Controls.Add(this.AddPatienLabel);
            this.Controls.Add(this.AddPatientBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddPatientView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pridaj Pacienta";
            this.PatientDataGroupBox.ResumeLayout(false);
            this.PatientDataGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button AddPatientBtn;
        private TextBox NameTextBox;
        private TextBox SureNameTextBox;
        private TextBox BirthNumberTextBox;
        private Label AddPatienLabel;
        private Label NameLabel;
        private GroupBox PatientDataGroupBox;
        private Label InsuranceLabel;
        private Label BirthNumLabel;
        private Label SureNameLabel;
        private Button CancelAddPatientBtn;
        private BindingSource presenterBindingSource;
        private NumericUpDown kodPoistovne;
    }
}