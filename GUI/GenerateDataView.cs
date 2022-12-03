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
    public partial class GenerateDataView : Form
    {
        public Presenter presenter { get; set; }
        public GenerateDataView(Presenter pPresenter)
        {
            InitializeComponent();
            presenter = pPresenter;
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            int patientsNum = (int)PatientsNumeric.Value;
            presenter.generateData(patientsNum);
            Close();
        }
    }
}
