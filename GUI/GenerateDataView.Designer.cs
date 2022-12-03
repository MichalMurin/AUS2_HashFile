namespace GUI
{
    partial class GenerateDataView
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
            this.PatientsNumeric = new System.Windows.Forms.NumericUpDown();
            this.numberOfPatientsLabel = new System.Windows.Forms.Label();
            this.generatingGroupBox = new System.Windows.Forms.GroupBox();
            this.GenerateBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PatientsNumeric)).BeginInit();
            this.generatingGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // PatientsNumeric
            // 
            this.PatientsNumeric.Location = new System.Drawing.Point(167, 26);
            this.PatientsNumeric.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PatientsNumeric.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.PatientsNumeric.Name = "PatientsNumeric";
            this.PatientsNumeric.Size = new System.Drawing.Size(181, 23);
            this.PatientsNumeric.TabIndex = 0;
            // 
            // numberOfPatientsLabel
            // 
            this.numberOfPatientsLabel.AutoSize = true;
            this.numberOfPatientsLabel.Location = new System.Drawing.Point(52, 28);
            this.numberOfPatientsLabel.Name = "numberOfPatientsLabel";
            this.numberOfPatientsLabel.Size = new System.Drawing.Size(92, 15);
            this.numberOfPatientsLabel.TabIndex = 3;
            this.numberOfPatientsLabel.Text = "Pocet pacientov";
            // 
            // generatingGroupBox
            // 
            this.generatingGroupBox.Controls.Add(this.numberOfPatientsLabel);
            this.generatingGroupBox.Controls.Add(this.PatientsNumeric);
            this.generatingGroupBox.Location = new System.Drawing.Point(34, 11);
            this.generatingGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.generatingGroupBox.Name = "generatingGroupBox";
            this.generatingGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.generatingGroupBox.Size = new System.Drawing.Size(375, 69);
            this.generatingGroupBox.TabIndex = 6;
            this.generatingGroupBox.TabStop = false;
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(242, 100);
            this.GenerateBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(167, 46);
            this.GenerateBtn.TabIndex = 7;
            this.GenerateBtn.Text = "Generuj";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // GenerateDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 173);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.generatingGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GenerateDataView";
            this.Text = "Generovanie dat";
            ((System.ComponentModel.ISupportInitialize)(this.PatientsNumeric)).EndInit();
            this.generatingGroupBox.ResumeLayout(false);
            this.generatingGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private NumericUpDown PatientsNumeric;
        private Label numberOfPatientsLabel;
        private GroupBox generatingGroupBox;
        private Button GenerateBtn;
    }
}