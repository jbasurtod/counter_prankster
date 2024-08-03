using Counter_Pranker;
using System.Diagnostics;

namespace Counter_Prankster_Namespace
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            pictureBox1.Image = Counter_Pranker.Properties.Resources.counter_prankster;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ModifierKeys == Keys.Alt)
            {
                e.Cancel = true;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void beginBtn_Click(object sender, EventArgs e)
        {
            beginBtn.Enabled = false;
            this.Hide();
            SetItUp();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/jbasurtod";

            try
            {
                // Open the URL in the default browser
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                // Handle exceptions if necessary (e.g., log the error or notify the user)
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
