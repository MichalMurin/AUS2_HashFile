using System.Windows.Forms;
using AUS2_MichalMurin_HashFile.Presenter;

namespace GUI
{
    /// <summary>
    /// Zakladna trieda na zobrazenie grafickeho okna pre pouzivatela
    /// </summary>
    public partial class MainWindowView : Form
    {
        public Presenter presenter { get; set; }
        public MainWindowView(Presenter pPresenter)
        {
            InitializeComponent();
            presenter = pPresenter;
            //presenter = new Presenter();
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            GenerateProgressBar.Value = 0;
            Form dialog = new GenerateDataView(presenter);
            dialog.ShowDialog();
            GenerateProgressBar.Value = 100;
        }

        private void PridajPacientaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form dialog = new AddPatientView(presenter);
            dialog.ShowDialog();
        }

        private void pridajHospitalizaciuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form dialog = new AddHospitalizationView(presenter);
            dialog.ShowDialog();
        }

        private void Uloha1Btn_Click(object sender, EventArgs e)
        {
            // vypisat udaje o pacientovi podla rodneho cisla
            if(!string.IsNullOrEmpty(uloha1TextBox.Text))
            {
                var result = presenter.GetPatientData(uloha1TextBox.Text);
                if (result.Item1)
                {
                    Form dialog = new ShowTextView(result.Item2!);
                    dialog.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Pacient sa nenasiel", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Zadali ste nespravne udaje", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ukonciHospitalizaciuToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form dialog = new EndHospitalizationView(presenter);
            dialog.ShowDialog();
        }

        private void Uloha2Btn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Uloah2Pacient.Text))
            {
                var birthNum = Uloah2Pacient.Text;
                byte insuranceCode = (byte)Uloha2ID.Value;
                var result = presenter.getHospitalization(birthNum, insuranceCode);
                if (result.Item1)
                {
                    Form dialog = new ShowTextView(result.Item2!);
                    dialog.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Hospitalizacia sa nenasla", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Zadali ste nespravne udaje", "CHYBA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vymazHospitalizaciuToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Form dialog = new DeleteHospitalizationView(presenter);
           dialog.ShowDialog();
        }

        private void vymazPacientaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Form dialog = new DeletePatientView(presenter);
           dialog.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = presenter.GetSequenceData();
            Form dialog = new ShowTextView(result);
            dialog.ShowDialog();            
        }
    }
}