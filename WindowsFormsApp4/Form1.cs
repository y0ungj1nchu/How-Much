using System;
using System.Data;
using System.Windows.Forms;
using WindowsFormsApp4;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private DataTable incomeTable;
        private DataTable budgetTable;

        private void LoadForm(Form frm)
        {
            panelMain.Controls.Clear();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(frm);
            frm.Show();
        }

        private void btntransaction_Click(object sender, EventArgs e)
        {
            LoadForm(new Formtransaction());
        }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            LoadForm(new FormBudget());
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            LoadForm(new FormStats());
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            LoadForm(new FormAccount());
        }

    }
}
