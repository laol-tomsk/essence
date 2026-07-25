using System;
using System.Windows.Forms;
using System.Diagnostics;


namespace Alpha.WinForms
{
    public partial class MainForm : Form
    {       
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonOpenAlphasTable_Click(object sender, EventArgs e)
        {
            new Form1().ShowDialog();
        }

        private void buttonOpenWPTable_Click(object sender, EventArgs e)
        {
            new WorkProductsTable().ShowDialog();
        }

        private void buttonOpenActivitiesTable_Click(object sender, EventArgs e)
        {
            new ActivitiesTable().ShowDialog();
        }

        private void import_Click(object sender, EventArgs e)
        {
            Process.Start("import.exe").WaitForExit();
            Application.Restart();
        }

        private void export_Click(object sender, EventArgs e)
        {
            Process.Start("export.exe");
        }
    }
}
