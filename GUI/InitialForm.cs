using AUS2_MichalMurin_HashFile.Presenter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class InitialForm : Form
    {
        public Presenter? presenter { get; set; }
        public InitialForm()
        {
            InitializeComponent(); 
            presenter = new Presenter();
            if (presenter.TryToInitializeFromFile())
                RunMain();
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            if (staticCheckBox.Checked)
            {
                presenter = new Presenter(AUS2_MichalMurin_HashFile.Service.HashType.StaticHash, (int)BlockFaktorNum.Value, (int)BlockCountNum.Value);
            }
            if (DynamicCheckBox.Checked)
            {
                presenter = new Presenter(AUS2_MichalMurin_HashFile.Service.HashType.DynamicHash, (int)BlockFaktorNum.Value);
            }
            RunMain();
        }

        private void RunMain()
        {
            var main = new MainWindowView(presenter!);
            Hide();
            main.ShowDialog();
            Close();
            Dispose();
        }

        private void staticCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (staticCheckBox.Checked)
            {
                DynamicCheckBox.Checked = false;
                BlockCountNum.Enabled = true;
            }
            else
            {
                DynamicCheckBox.Checked = true;
                BlockCountNum.Enabled = false;
            }
        }

        private void DynamicCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DynamicCheckBox.Checked)
            {
                staticCheckBox.Checked = false;
                BlockCountNum.Enabled = false;
            }
            else
            {
                staticCheckBox.Checked = true;
                BlockCountNum.Enabled = true;
            }
        }
    }
}
