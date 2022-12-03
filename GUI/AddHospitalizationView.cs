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
    public partial class AddHospitalizationView : Form
    {
        public Presenter presenter { get; set; }
        public AddHospitalizationView(Presenter pPresenter)
        {
            InitializeComponent();
            presenter = pPresenter;
        }

        private void HospitalizationCancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddHospitalizationBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DiagnosisTextBox.Text) && !string.IsNullOrEmpty(rodneCislo.Text))
            {
                var startDate = StartDatePicker.Value.Date;
                DateTime endDate = DateTime.MaxValue;
                if (EndDatePicker.Checked)
                    endDate = EndDatePicker.Value.Date;
                var diagnosis = DiagnosisTextBox.Text;
                var patientBirthNum = rodneCislo.Text;
                var success = presenter.addHospitalization(patientBirthNum, diagnosis, startDate, endDate);
                if (!success)
                    MessageBox.Show("Hospitalizaci sa nepodarilo pridat", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
