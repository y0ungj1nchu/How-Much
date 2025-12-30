using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Formtransaction : Form
    {
        private DataTable table;
        private bool isLoading = false;
        Searchcategory searchcategory;

        private TransactionRepository repo = new TransactionRepository();

        public Formtransaction()
        {
            InitializeComponent();
            InitializeFilters();
            LoadPayTypes();
            LoadCategories();
            HookEvents();
        }

        private void Formtransaction_Load(object sender, EventArgs e)
        {
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.Value = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day
            );

            LoadTransactions();
        }

        // =========================================================
        // 필터 초기화 (기존 그대로)
        // =========================================================
        private void InitializeFilters()
        {
            IncomeExpensecombobox.Items.Add("지출");
            IncomeExpensecombobox.Items.Add("수입");
            IncomeExpensecombobox.SelectedIndex = -1;
        }

        // =========================================================
        // 거래수단 로드
        // =========================================================
        private void LoadPayTypes()
        {
            using (var conn = DatabaseManager.GetConnection())
            using (var cmd = new Oracle.DataAccess.Client.OracleCommand(
                "SELECT METHOD_ID, NAME FROM PAY_METHODS ORDER BY METHOD_ID", conn))
            {
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                cmbTrasactionType.DataSource = dt;
                cmbTrasactionType.DisplayMember = "NAME";
                cmbTrasactionType.ValueMember = "METHOD_ID";
                cmbTrasactionType.SelectedIndex = -1;
            }
        }

        // =========================================================
        // 카테고리 로드
        // =========================================================
        private void LoadCategories()
        {
            using (var conn = DatabaseManager.GetConnection())
            using (var cmd = new Oracle.DataAccess.Client.OracleCommand(
                "SELECT CATEGORY_ID, NAME FROM CATEGORIES ORDER BY CATEGORY_ID", conn))
            {
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                cmbMainCategory.DataSource = dt;
                cmbMainCategory.DisplayMember = "NAME";
                cmbMainCategory.ValueMember = "CATEGORY_ID";
                cmbMainCategory.SelectedIndex = -1;
            }

            cmbMainCategory.SelectedIndexChanged += (s, e) =>
            {
                if (isLoading) return;

                if (cmbMainCategory.SelectedIndex >= 0)
                    LoadSubCategories(Convert.ToInt32(cmbMainCategory.SelectedValue));
                else
                    ClearSubCategories();
            };

            ClearSubCategories();
        }

        private void LoadSubCategories(int categoryID)
        {
            using (var conn = DatabaseManager.GetConnection())
            using (var cmd = new Oracle.DataAccess.Client.OracleCommand(
                "SELECT SUB_ID, NAME FROM SUB_CATEGORIES WHERE CATEGORY_ID = :p_cat ORDER BY SUB_ID", conn))
            {
                cmd.Parameters.Add(":p_cat", categoryID);

                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                cmbSubCategory.DataSource = dt;
                cmbSubCategory.DisplayMember = "NAME";
                cmbSubCategory.ValueMember = "SUB_ID";
                cmbSubCategory.SelectedIndex = -1;
            }
        }

        private void ClearSubCategories()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SUB_ID");
            dt.Columns.Add("NAME");

            cmbSubCategory.DataSource = dt;
            cmbSubCategory.SelectedIndex = -1;
        }

        // =========================================================
        // 거래 조회 (Repository 사용 / SQL 의미 동일)
        // =========================================================
        private void LoadTransactions()
        {
            isLoading = true;

            table = repo.GetTransactions(
                dtpStartDate.Value.Date,
                dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1)
            );

            dgvTransaction.DataSource = table;

            dgvTransaction.Columns["거래ID"].Visible = false;
            dgvTransaction.Columns["METHOD_ID_RAW"].Visible = false;
            dgvTransaction.Columns["CAT_ID_RAW"].Visible = false;
            dgvTransaction.Columns["SUB_ID_RAW"].Visible = false;

            dgvTransaction.Columns["날짜"].DisplayIndex = 0;

            isLoading = false;
            UpdateTotals();
        }

        // =========================================================
        // 이벤트
        // =========================================================
        private void HookEvents()
        {
            dgvTransaction.CellFormatting += DgvTransaction_CellFormatting;
            dgvTransaction.CellClick += DgvTransaction_CellClick;

            dtpStartDate.ValueChanged += (s, e) => { if (!isLoading) LoadTransactions(); };
            dtpEndDate.ValueChanged += (s, e) => { if (!isLoading) LoadTransactions(); };

            IncomeExpensecombobox.SelectedIndexChanged += (s, e) =>
            {
                if (!isLoading) LoadTransactions();
            };
        }

        // =========================================================
        // 필터 (기존 로직 그대로)
        // =========================================================
        private void ApplyViewFilter(string type)
        {
            if (table == null) return;

            DataView dv = new DataView(table);

            if (type == "지출")
                dv.RowFilter = "수입지출 = '지출'";
            else if (type == "수입")
                dv.RowFilter = "수입지출 = '수입'";
            else
                dv.RowFilter = "";

            dgvTransaction.DataSource = dv;
            UpdateTotalsFromView(dv);
        }

        private void ApplyCategoryFilter(int subId)
        {
            if (table == null) return;

            DataView dv = new DataView(table);
            dv.RowFilter = $"SUB_ID_RAW = {subId}";

            dgvTransaction.DataSource = dv;
            UpdateTotalsFromView(dv);
        }

        private void ApplyPayTypeFilter(int methodId)
        {
            if (table == null) return;

            DataView dv = new DataView(table);
            dv.RowFilter = $"METHOD_ID_RAW = {methodId}";

            dgvTransaction.DataSource = dv;
            UpdateTotalsFromView(dv);
        }

        // =========================================================
        // 합계 계산 (Calculator 사용)
        // =========================================================
        private void UpdateTotals()
        {
            var (income, expense) =
                TransactionCalculator.Calculate(new DataView(table));

            lblIncomeAmount.Text = "+" + income.ToString("N0");
            lblExpenseAmount.Text = "-" + expense.ToString("N0");

            lblIncomeAmount.ForeColor = Color.Green;
            lblExpenseAmount.ForeColor = Color.Red;
        }

        private void UpdateTotalsFromView(DataView dv)
        {
            var (income, expense) = TransactionCalculator.Calculate(dv);

            lblIncomeAmount.Text = "+" + income.ToString("N0");
            lblExpenseAmount.Text = "-" + expense.ToString("N0");
        }

        // =========================================================
        // DataGridView
        // =========================================================
        private void DgvTransaction_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (isLoading) return;
            if (dgvTransaction.Columns[e.ColumnIndex].HeaderText != "금액") return;
            if (e.Value == null) return;

            decimal amount = Convert.ToDecimal(e.Value);
            string type = dgvTransaction.Rows[e.RowIndex].Cells["수입지출"].Value.ToString();

            if (type == "지출")
            {
                e.Value = "-" + amount.ToString("N0");
                e.CellStyle.ForeColor = Color.Red;
            }
            else
            {
                e.Value = "+" + amount.ToString("N0");
                e.CellStyle.ForeColor = Color.Green;
            }
        }

        private void DgvTransaction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataRow row = table.Rows[e.RowIndex];

            txtAmount.Text = row["금액"].ToString();
            txtMemo.Text = row["메모"].ToString();

            cmbTrasactionType.SelectedValue = row["METHOD_ID_RAW"];
            cmbMainCategory.SelectedValue = row["CAT_ID_RAW"];

            int subId = Convert.ToInt32(row["SUB_ID_RAW"]);
            BeginInvoke(new Action(() =>
            {
                cmbSubCategory.SelectedValue = subId;
            }));
        }

        // =========================================================
        // CRUD (기능 동일)
        // =========================================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            repo.Insert(
                Convert.ToInt32(cmbTrasactionType.SelectedValue),
                Convert.ToInt32(cmbSubCategory.SelectedValue),
                Convert.ToInt32(txtAmount.Text),
                txtMemo.Text
            );

            LoadTransactions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTransaction.CurrentRow == null) return;

            int txId = Convert.ToInt32(dgvTransaction.CurrentRow.Cells["거래ID"].Value);
            repo.Delete(txId);

            LoadTransactions();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTransaction.CurrentRow == null) return;

            int txId = Convert.ToInt32(dgvTransaction.CurrentRow.Cells["거래ID"].Value);

            using (var conn = DatabaseManager.GetConnection())
            using (var cmd = new Oracle.DataAccess.Client.OracleCommand(
                "UPDATE TRANSACTIONS SET METHOD_ID = :tm, SUB_ID = :sub, AMOUNT = :amt, MEMO = :memo WHERE TX_ID = :id", conn))
            {
                cmd.Parameters.Add(":tm", cmbTrasactionType.SelectedValue);
                cmd.Parameters.Add(":sub", cmbSubCategory.SelectedValue);
                cmd.Parameters.Add(":amt", Convert.ToInt32(txtAmount.Text));
                cmd.Parameters.Add(":memo", txtMemo.Text);
                cmd.Parameters.Add(":id", txId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadTransactions();
        }


        // =========================================================
        // 메뉴
        // =========================================================
        private void 카테고리검색ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Searchcategory sc = new Searchcategory();
            sc.CategorySelected += ApplyCategoryFilter;
            sc.ShowDialog();
        }

        private void 거래수단ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Formtransactionmethod fm = new Formtransactionmethod();
            fm.MethodSelected += ApplyPayTypeFilter;
            fm.ShowDialog();
        }

        private void 지출ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ApplyViewFilter("지출");
        }

        private void 전체ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyViewFilter("전체");
        }
        private void 수입ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyViewFilter("수입");
        }



    }
}
