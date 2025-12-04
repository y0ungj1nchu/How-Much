using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace WindowsFormsApp4
{
    public partial class Formtransaction : Form
    {
        private OracleConnection conn;
        private DataTable table;
        private bool isLoading = false;

        public Formtransaction()
        {
            InitializeComponent();
            InitializeDB();
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

        private void InitializeDB()
        {
            conn = new OracleConnection(
                "User Id=BANK_MANAGER; Password=1234; Data Source=localhost:1521/XE;");
        }

        private void InitializeFilters()
        {
            IncomeExpensecombobox.Items.Add("지출");
            IncomeExpensecombobox.Items.Add("수입");
            IncomeExpensecombobox.Items.Add("전체");
            IncomeExpensecombobox.SelectedIndex = 2;
        }

        private void LoadPayTypes()
        {
            using (OracleCommand cmd = new OracleCommand(
                "SELECT TM_ID, NAME FROM TRANSACTION_METHODS ORDER BY TM_ID", conn))
            {
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();

                cmbTrasactionType.DataSource = dt;
                cmbTrasactionType.DisplayMember = "NAME";
                cmbTrasactionType.ValueMember = "TM_ID";

                cmbTrasactionType.SelectedIndex = -1;
            }
        }

        private void LoadCategories()
        {
            using (OracleCommand cmd = new OracleCommand(
                "SELECT CATEGORY_ID, NAME FROM CATEGORIES ORDER BY CATEGORY_ID", conn))
            {
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();

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
            using (OracleCommand cmd = new OracleCommand(
                "SELECT SUB_ID, NAME FROM SUB_CATEGORIES WHERE CATEGORY_ID = :p_cat ORDER BY SUB_ID", conn))
            {
                cmd.Parameters.Add(":p_cat", categoryID);

                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();

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

        private void LoadTransactions()
        {
            isLoading = true;

            string sql =
                "SELECT " +
                "T.TX_ID AS 거래ID, " +
                "T.TM_ID AS TM_ID_RAW, " +
                "C.CATEGORY_ID AS CAT_ID_RAW, " +
                "S.SUB_ID AS SUB_ID_RAW, " +
                "TO_CHAR(T.TX_DATE, 'YYYY-MM-DD') AS 날짜, " +
                "M.NAME AS 거래수단, " +
                "S.NAME AS 항목, " +
                "T.AMOUNT AS 금액, " +
                "T.MEMO AS 메모, " +
                "CASE WHEN C.TYPE = 'EXPENSE' THEN '지출' ELSE '수입' END AS 수입지출 " +
                "FROM TRANSACTIONS T " +
                "JOIN TRANSACTION_METHODS M ON M.TM_ID = T.TM_ID " +
                "JOIN SUB_CATEGORIES S ON S.SUB_ID = T.SUB_ID " +
                "JOIN CATEGORIES C ON C.CATEGORY_ID = S.CATEGORY_ID " +
                "WHERE T.TX_DATE BETWEEN :p_start AND :p_end ";

            // 수입/지출 필터만 유지
            if (IncomeExpensecombobox.SelectedIndex == 0)
                sql += "AND C.TYPE = 'EXPENSE' ";
            else if (IncomeExpensecombobox.SelectedIndex == 1)
                sql += "AND C.TYPE = 'INCOME' ";

            sql += "ORDER BY T.TX_DATE DESC";

            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add(":p_start", dtpStartDate.Value.Date);
                cmd.Parameters.Add(":p_end", dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1));

                conn.Open();
                table = new DataTable();
                table.Load(cmd.ExecuteReader());
                conn.Close();
            }

            dgvTransaction.DataSource = table;

            dgvTransaction.Columns["거래ID"].Visible = false;
            dgvTransaction.Columns["TM_ID_RAW"].Visible = false;
            dgvTransaction.Columns["CAT_ID_RAW"].Visible = false;
            dgvTransaction.Columns["SUB_ID_RAW"].Visible = false;

            dgvTransaction.Columns["날짜"].DisplayIndex = 0;

            isLoading = false;
            UpdateTotals();
        }

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

            // 필터 제거 → 변경 이벤트에서 LoadTransactions 제거됨
        }

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

            // 거래수단 선택
            cmbTrasactionType.SelectedValue =
                Convert.ToInt32(row["TM_ID_RAW"]);

            // 카테고리 선택
            cmbMainCategory.SelectedValue =
                Convert.ToInt32(row["CAT_ID_RAW"]);

            // 서브카테고리는 카테고리 로드 후 선택 필요
            int subId = Convert.ToInt32(row["SUB_ID_RAW"]);

            this.BeginInvoke(new Action(() =>
            {
                cmbSubCategory.SelectedValue = subId;
            }));
        }

        private void UpdateTotals()
        {
            decimal income = 0, expense = 0;

            foreach (DataRow row in table.Rows)
            {
                decimal amt = Convert.ToDecimal(row["금액"]);
                string type = row["수입지출"].ToString();

                if (type == "지출") expense += amt;
                else income += amt;
            }

            lblIncomeAmount.Text = "+" + income.ToString("N0");
            lblExpenseAmount.Text = "-" + expense.ToString("N0");

            lblIncomeAmount.ForeColor = Color.Green;
            lblExpenseAmount.ForeColor = Color.Red;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OracleCommand cmd = new OracleCommand(
                "INSERT INTO TRANSACTIONS (TX_ID, TM_ID, SUB_ID, AMOUNT, TX_DATE, MEMO) VALUES (SEQ_TX.NEXTVAL, :tm, :sub, :amt, SYSDATE, :memo)", conn))
            {
                cmd.Parameters.Add(":tm", cmbTrasactionType.SelectedValue);
                cmd.Parameters.Add(":sub", cmbSubCategory.SelectedValue);
                cmd.Parameters.Add(":amt", Convert.ToInt32(txtAmount.Text));
                cmd.Parameters.Add(":memo", txtMemo.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadTransactions();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTransaction.CurrentRow == null) return;

            int txId = Convert.ToInt32(dgvTransaction.CurrentRow.Cells["거래ID"].Value);

            using (OracleCommand cmd = new OracleCommand(
                "UPDATE TRANSACTIONS SET TM_ID = :tm, SUB_ID = :sub, AMOUNT = :amt, MEMO = :memo WHERE TX_ID = :id", conn))
            {
                cmd.Parameters.Add(":tm", cmbTrasactionType.SelectedValue);
                cmd.Parameters.Add(":sub", cmbSubCategory.SelectedValue);
                cmd.Parameters.Add(":amt", Convert.ToInt32(txtAmount.Text));
                cmd.Parameters.Add(":memo", txtMemo.Text);
                cmd.Parameters.Add(":id", txId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadTransactions();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTransaction.CurrentRow == null) return;

            int txId = Convert.ToInt32(dgvTransaction.CurrentRow.Cells["거래ID"].Value);

            using (OracleCommand cmd = new OracleCommand(
                "DELETE FROM TRANSACTIONS WHERE TX_ID = :id", conn))
            {
                cmd.Parameters.Add(":id", txId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadTransactions();
        }

        private void ALLBtn_Click(object sender, EventArgs e)
        {
            IncomeExpensecombobox.SelectedIndex = 2;

            LoadTransactions();
        }
    }
}
