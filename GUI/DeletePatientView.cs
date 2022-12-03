using AUS2_MichalMurin_HashFile.Presenter;
using GUI;
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
    public partial class DeletePatientView : Form
    {
        Presenter presenter { get; set; }
        public DeletePatientView(Presenter pPresenter)
        {
            InitializeComponent();
            presenter = pPresenter;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rodneCislo.Text))
            {
                if (!presenter.deletePatient(rodneCislo.Text))
                    MessageBox.Show("Nepodarilo sa vymazat pacienta", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
            {
                MessageBox.Show("Zadali ste nespravne udaje", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


