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
        private DataTable expenseTable;
        private DataTable budgetTable;

        private void LoadForm(Form frm)
        {
            panelMain.Controls.Clear();
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(frm);
            frm.Show();
        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            LoadForm(new FormIncome());
        }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            LoadForm(new FormBudget());
        }

        private void btnExpense_Click(object sender, EventArgs e)
        {
            LoadForm(new FormExpense());
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            LoadForm(new FormStats(incomeTable, expenseTable, budgetTable));
        }
    }
}
