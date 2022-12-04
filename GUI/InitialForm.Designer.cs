namespace GUI
{
    partial class InitialForm
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
            this.staticCheckBox = new System.Windows.Forms.CheckBox();
            this.DynamicCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BlockFaktorNum = new System.Windows.Forms.NumericUpDown();
            this.BlockCountNum = new System.Windows.Forms.NumericUpDown();
            this.runBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BlockFaktorNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlockCountNum)).BeginInit();
            this.SuspendLayout();
            // 
            // staticCheckBox
            // 
            this.staticCheckBox.AutoSize = true;
            this.staticCheckBox.Location = new System.Drawing.Point(44, 32);
            this.staticCheckBox.Name = "staticCheckBox";
            this.staticCheckBox.Size = new System.Drawing.Size(123, 19);
            this.staticCheckBox.TabIndex = 0;
            this.staticCheckBox.Text = "Staticke hesovanie";
            this.staticCheckBox.UseVisualStyleBackColor = true;
            this.staticCheckBox.CheckedChanged += new System.EventHandler(this.staticCheckBox_CheckedChanged);
            // 
            // DynamicCheckBox
            // 
            this.DynamicCheckBox.AutoSize = true;
            this.DynamicCheckBox.Checked = true;
            this.DynamicCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DynamicCheckBox.Location = new System.Drawing.Point(44, 72);
            this.DynamicCheckBox.Name = "DynamicCheckBox";
            this.DynamicCheckBox.Size = new System.Drawing.Size(141, 19);
            this.DynamicCheckBox.TabIndex = 1;
            this.DynamicCheckBox.Text = "Dynamicke hesovanie";
            this.DynamicCheckBox.UseVisualStyleBackColor = true;
            this.DynamicCheckBox.CheckedChanged += new System.EventHandler(this.DynamicCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DynamicCheckBox);
            this.groupBox1.Controls.Add(this.staticCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(28, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 121);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Typ hesovania";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Blok-faktor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Pocet blokov";
            // 
            // BlockFaktorNum
            // 
            this.BlockFaktorNum.Location = new System.Drawing.Point(115, 162);
            this.BlockFaktorNum.Maximum = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.BlockFaktorNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BlockFaktorNum.Name = "BlockFaktorNum";
            this.BlockFaktorNum.Size = new System.Drawing.Size(131, 23);
            this.BlockFaktorNum.TabIndex = 5;
            this.BlockFaktorNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // BlockCountNum
            // 
            this.BlockCountNum.Enabled = false;
            this.BlockCountNum.Location = new System.Drawing.Point(115, 199);
            this.BlockCountNum.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.BlockCountNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BlockCountNum.Name = "BlockCountNum";
            this.BlockCountNum.Size = new System.Drawing.Size(131, 23);
            this.BlockCountNum.TabIndex = 6;
            this.BlockCountNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // runBtn
            // 
            this.runBtn.Location = new System.Drawing.Point(151, 239);
            this.runBtn.Name = "runBtn";
            this.runBtn.Size = new System.Drawing.Size(95, 33);
            this.runBtn.TabIndex = 7;
            this.runBtn.Text = "Spusti";
            this.runBtn.UseVisualStyleBackColor = true;
            this.runBtn.Click += new System.EventHandler(this.runBtn_Click);
            // 
            // InitialForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 331);
            this.Controls.Add(this.runBtn);
            this.Controls.Add(this.BlockCountNum);
            this.Controls.Add(this.BlockFaktorNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "InitialForm";
            this.Text = "Hash File";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BlockFaktorNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlockCountNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox staticCheckBox;
        private CheckBox DynamicCheckBox;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private NumericUpDown BlockFaktorNum;
        private NumericUpDown BlockCountNum;
        private Button runBtn;
    }
}