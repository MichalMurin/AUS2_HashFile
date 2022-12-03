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
    public partial class AddPatientView : Form
    {
        public Presenter presenter { get; set; }
        public AddPatientView(Presenter pPresenter)
        {
            InitializeComponent();
            presenter = pPresenter;
        }

        private void AddPatientBtn_Click(object sender, EventArgs e)
        {       
            
            if (NameTextBox.Text.Length > 0 && SureNameTextBox.Text.Length>0
                && NameTextBox.Text.Length <= 15
                && SureNameTextBox.Text.Length <= 20
                && BirthNumberTextBox.Text.Length>9 && BirthNumberTextBox.Text.Length <= 10)
            {
                var name = NameTextBox.Text;
                var surename = SureNameTextBox.Text;
                var birtNum = BirthNumberTextBox.Text;
                byte insuranceCmpny = (byte)kodPoistovne.Value;
                bool success = presenter.addPatient(name, surename, birtNum, insuranceCmpny);
                if (!success)
                    MessageBox.Show("Nepodarilo sa pridat pacienta", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void CancelAddPatientBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
