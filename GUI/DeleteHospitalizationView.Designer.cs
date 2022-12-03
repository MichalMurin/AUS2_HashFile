namespace GUI
{
    partial class DeleteHospitalizationView
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
            this.rodneCislo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.idHosp = new System.Windows.Forms.NumericUpDown();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.idHosp)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pacient";
            // 
            // rodneCislo
            // 
            this.rodneCislo.Location = new System.Drawing.Point(98, 22);
            this.rodneCislo.Name = "rodneCislo";
            this.rodneCislo.Size = new System.Drawing.Size(146, 23);
            this.rodneCislo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "ID Hospitalizacie";
            // 
            // idHosp
            // 
            this.idHosp.Location = new System.Drawing.Point(128, 65);
            this.idHosp.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.idHosp.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.idHosp.Name = "idHosp";
            this.idHosp.Size = new System.Drawing.Size(116, 23);
            this.idHosp.TabIndex = 3;
            this.idHosp.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Location = new System.Drawing.Point(175, 141);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(100, 30);
            this.DeleteBtn.TabIndex = 4;
            this.DeleteBtn.Text = "Vymazat";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.idHosp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rodneCislo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(31, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 109);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // DeleteHospitalizationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 224);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.DeleteBtn);
            this.Name = "DeleteHospitalizationView";
            this.Text = "Vymazat Hospitalizaciu";
            ((System.ComponentModel.ISupportInitialize)(this.idHosp)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label label1;
        private TextBox rodneCislo;
        private Label label2;
        private NumericUpDown idHosp;
        private Button DeleteBtn;
        private GroupBox groupBox1;
    }
}