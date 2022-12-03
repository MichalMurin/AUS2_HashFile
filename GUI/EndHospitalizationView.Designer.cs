namespace GUI
{
    partial class EndHospitalizationView
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
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.PatientsLabel = new System.Windows.Forms.Label();
            this.UkoncenieGroupBox = new System.Windows.Forms.GroupBox();
            this.EndHospitalizationLabel = new System.Windows.Forms.Label();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.UkoncenieHospRodCislo = new System.Windows.Forms.TextBox();
            this.UkoncenieGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // endDatePicker
            // 
            this.endDatePicker.Location = new System.Drawing.Point(27, 70);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(240, 23);
            this.endDatePicker.TabIndex = 2;
            // 
            // PatientsLabel
            // 
            this.PatientsLabel.AutoSize = true;
            this.PatientsLabel.Location = new System.Drawing.Point(27, 19);
            this.PatientsLabel.Name = "PatientsLabel";
            this.PatientsLabel.Size = new System.Drawing.Size(46, 15);
            this.PatientsLabel.TabIndex = 4;
            this.PatientsLabel.Text = "Pacient";
            // 
            // UkoncenieGroupBox
            // 
            this.UkoncenieGroupBox.Controls.Add(this.UkoncenieHospRodCislo);
            this.UkoncenieGroupBox.Controls.Add(this.EndHospitalizationLabel);
            this.UkoncenieGroupBox.Controls.Add(this.PatientsLabel);
            this.UkoncenieGroupBox.Controls.Add(this.endDatePicker);
            this.UkoncenieGroupBox.Location = new System.Drawing.Point(24, 33);
            this.UkoncenieGroupBox.Name = "UkoncenieGroupBox";
            this.UkoncenieGroupBox.Size = new System.Drawing.Size(371, 108);
            this.UkoncenieGroupBox.TabIndex = 5;
            this.UkoncenieGroupBox.TabStop = false;
            // 
            // EndHospitalizationLabel
            // 
            this.EndHospitalizationLabel.AutoSize = true;
            this.EndHospitalizationLabel.Location = new System.Drawing.Point(27, 52);
            this.EndHospitalizationLabel.Name = "EndHospitalizationLabel";
            this.EndHospitalizationLabel.Size = new System.Drawing.Size(137, 15);
            this.EndHospitalizationLabel.TabIndex = 5;
            this.EndHospitalizationLabel.Text = "Ukoncenie hospitalizacie";
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(51, 156);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(119, 47);
            this.CancelBtn.TabIndex = 6;
            this.CancelBtn.Text = "Zrusit";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(256, 156);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(119, 47);
            this.SaveBtn.TabIndex = 7;
            this.SaveBtn.Text = "Ulozit";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // UkoncenieHospRodCislo
            // 
            this.UkoncenieHospRodCislo.Location = new System.Drawing.Point(92, 16);
            this.UkoncenieHospRodCislo.Name = "UkoncenieHospRodCislo";
            this.UkoncenieHospRodCislo.Size = new System.Drawing.Size(233, 23);
            this.UkoncenieHospRodCislo.TabIndex = 6;
            // 
            // EndHospitalizationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 226);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.UkoncenieGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EndHospitalizationView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ukoncenie hospitalizacie";
            this.UkoncenieGroupBox.ResumeLayout(false);
            this.UkoncenieGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DateTimePicker endDatePicker;
        private Label PatientsLabel;
        private GroupBox UkoncenieGroupBox;
        private Label EndHospitalizationLabel;
        private Button CancelBtn;
        private Button SaveBtn;
        private TextBox UkoncenieHospRodCislo;
    }
}