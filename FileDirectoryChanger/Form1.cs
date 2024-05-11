namespace FileDirectoryChanger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool closeState = false;
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closeState) e.Cancel = true;
            this.Hide();

        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void gizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void çýkýþToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeState = true;
            Application.Exit();
        }
    }
}
