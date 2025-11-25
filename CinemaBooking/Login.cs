using System;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace CinemaBooking
{
    public partial class Login : Form
    {
     
        private readonly PackageServices svc = new PackageServices();

        public Login()
        {
            InitializeComponent();
            svc.createConnection();   
        }
        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            var user = textBoxUsername.Text.Trim();
            var pass = textBoxParola.Text.Trim();

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Introduceti username si parola.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            buttonLogin.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                bool ok = await svc.LoginManagerAsync(user, pass);

                if (ok)
                {
                    var frm = new FormManagerCinema();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Username sau parola incorecte.",
                        "Autentificare esuata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la conectare:\n" + ex.Message,
                    "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                buttonLogin.Enabled = true;
            }
        }
    }
}
