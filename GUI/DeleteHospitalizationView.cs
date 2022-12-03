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
    public partial class DeleteHospitalizationView : Form
    {
        Presenter presenter { get; set; }
        public DeleteHospitalizationView(Presenter pPresenter)
        {
            InitializeComponent();
            presenter = pPresenter;
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rodneCislo.Text))
            {
                byte id = (byte)idHosp.Value;
                if (!presenter.deleteHospitalization(rodneCislo.Text, id))
                    MessageBox.Show("Nepodarilo sa vymazat hospitalizaciu", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
            {
                MessageBox.Show("Zadali ste nespravne udaje", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

