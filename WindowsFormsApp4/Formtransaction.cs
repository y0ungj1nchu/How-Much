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

            // 🔹 이번달 1일 ~ 마지막날 자동 설정
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                                    DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            LoadTransactions();
            HookEvents();
        }

        // ==============================================================  
        // 🔵 DB 연결 설정  
        // ==============================================================  
        private void InitializeDB()
        {
            conn = new OracleConnection(
                "User Id=BANK_MANAGER; Password=1234; Data Source=localhost:1521/XE;");
        }

        // ==============================================================  
        // 🔵 필터 설정 (수입/지출/전체)
        // ==============================================================  
        private void InitializeFilters()
        {
            IncomeExpensecombobox.Items.Add("지출");
            IncomeExpensecombobox.Items.Add("수입");
            IncomeExpensecombobox.Items.Add("전체");
            IncomeExpensecombobox.SelectedIndex = 2; // 전체 기본
        }

        // ==============================================================  
        // 🔵 결제수단 로드  
        // ==============================================================  
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
            }
        }

        // ==============================================================  
        // 🔵 카테고리 로드  
        // ==============================================================  
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
            }

            cmbMainCategory.SelectedIndexChanged += (s, e) =>
            {
                if (cmbMainCategory.SelectedValue != null)
                    LoadSubCategories(Convert.ToInt32(cmbMainCategory.SelectedValue));
            };

            if (cmbMainCategory.Items.Count > 0)
                LoadSubCategories(Convert.ToInt32(cmbMainCategory.SelectedValue));
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
            }
        }

        // ==============================================================  
        // 🔵 거래내역 로드  
        // ==============================================================  
        private void LoadTransactions()
        {
            isLoading = true;

            string sql =
                "SELECT T.TX_ID AS 거래ID, " +
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

            // 거래ID 숨기기
            if (dgvTransaction.Columns.Contains("거래ID"))
                dgvTransaction.Columns["거래ID"].Visible = false;

            // 날짜 맨 앞
            dgvTransaction.Columns["날짜"].DisplayIndex = 0;

            isLoading = false;
            UpdateTotals();
        }

        // ==============================================================  
        // 🔵 이벤트 연결  
        // ==============================================================  
        private void HookEvents()
        {
            dgvTransaction.CellFormatting += DgvTransaction_CellFormatting;
            dgvTransaction.CellClick += DgvTransaction_CellClick;

            dtpStartDate.ValueChanged += (s, e) => LoadTransactions();
            dtpEndDate.ValueChanged += (s, e) => LoadTransactions();
            IncomeExpensecombobox.SelectedIndexChanged += (s, e) => LoadTransactions();
        }

        // ==============================================================  
        // 🔵 금액 색깔 설정  
        // ==============================================================  
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

        // ==============================================================  
        // 🔵 셀 클릭 시 입력창에 출력  
        // ==============================================================  
        private void DgvTransaction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txtAmount.Text = table.Rows[e.RowIndex]["금액"].ToString();
            txtMemo.Text = table.Rows[e.RowIndex]["메모"].ToString();
        }

        // ==============================================================  
        // 🔵 총액 계산  
        // ==============================================================  
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

        // ==============================================================  
        // 🔵 **공통: 거래수단이 '전체'이면 추가/수정/삭제 금지**  
        // ==============================================================  
        private bool ValidateBeforeEdit()
        {
            if (IncomeExpensecombobox.SelectedIndex == 2) // 전체
            {
                MessageBox.Show("수입 또는 지출을 선택해주세요.", "알림",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // ==============================================================  
        // 🔵 거래 추가  
        // ==============================================================  
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateBeforeEdit()) return;

            using (OracleCommand cmd = new OracleCommand(
                "INSERT INTO TRANSACTIONS (TX_ID, TM_ID, SUB_ID, AMOUNT, TX_DATE, MEMO) " +
                "VALUES (SEQ_TX.NEXTVAL, :tm, :sub, :amt, SYSDATE, :memo)", conn))
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

        // ==============================================================  
        // 🔵 거래 수정  
        // ==============================================================  
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateBeforeEdit()) return;
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

        // ==============================================================  
        // 🔵 거래 삭제  
        // ==============================================================  
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!ValidateBeforeEdit()) return;
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
    }
}
