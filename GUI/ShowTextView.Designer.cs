namespace GUI
{
    partial class ShowTextView
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
            this.ListBoxShow = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ListBoxShow
            // 
            this.ListBoxShow.FormattingEnabled = true;
            this.ListBoxShow.ItemHeight = 20;
            this.ListBoxShow.Location = new System.Drawing.Point(12, 3);
            this.ListBoxShow.Name = "ListBoxShow";
            this.ListBoxShow.Size = new System.Drawing.Size(1444, 764);
            this.ListBoxShow.TabIndex = 0;
            // 
            // ShowTextView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 778);
            this.Controls.Add(this.ListBoxShow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ShowTextView";
            this.Text = "Vypis";
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox ListBoxShow;
    }
}