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
    public partial class EndHospitalizationView : Form
    {
        public Presenter presenter { get; set; }
        private bool _autoLoading;
        public EndHospitalizationView(Presenter pPresenter)
        {
            InitializeComponent();
            presenter = pPresenter;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(UkoncenieHospRodCislo.Text))
            {
                DateTime end = endDatePicker.Value.Date;
                var patient = UkoncenieHospRodCislo.Text;
                var success = presenter.endHospitalization(patient, end);
                if (!success)
                    MessageBox.Show("Nepodarilo sa ukoncit hospitalizaciu", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Zadali ste nespravne udaje", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
