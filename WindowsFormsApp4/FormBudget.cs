using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp4
{
    public partial class FormBudget : Form
    {
        private string connectionString =
            "User Id=BANK_MANAGER; Password=1234; Data Source=localhost:1521/XE;";

        // 초기화 완료 여부
        private bool isInitialized = false;

        public FormBudget()
        {
            InitializeComponent();
            this.Load += FormBudget_Load;
        }

        // =========================================================
        // Form Load
        // =========================================================
        private void FormBudget_Load(object sender, EventArgs e)
        {
            try
            {
                LoadCategoryData();

                cmbMonth.Value = DateTime.Now;

                InitializeGrid(dgvThisMonth);
                InitializeChart();

                LoadThisMonthBudget();
                LoadBudgetExpenseChart();

                isInitialized = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Form load error: " + ex.Message);
            }
        }

        // =========================================================
        // DataGridView 초기화
        // =========================================================
        private void InitializeGrid(DataGridView dgv)
        {
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.Columns.Clear();

            dgv.Columns.Add("BUDGET_ID", "ID");
            dgv.Columns["BUDGET_ID"].Visible = false;

            dgv.Columns.Add("YYYYMM", "월");
            dgv.Columns.Add("CATEGORY_NAME", "카테고리");
            dgv.Columns.Add("AMOUNT", "금액");

            dgv.Columns["YYYYMM"].Width = 80;
            dgv.Columns["CATEGORY_NAME"].Width = 200;
            dgv.Columns["AMOUNT"].Width = 120;
            dgv.Columns["AMOUNT"].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            dgv.CellClick -= Dgv_CellClick;
            dgv.CellClick += Dgv_CellClick;
        }

        // =========================================================
        // 카테고리 로드
        // =========================================================
        private void LoadCategoryData()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                    SELECT CATEGORY_ID, NAME
                    FROM CATEGORIES
                    WHERE TYPE = 'EXPENSE'
                    ORDER BY CATEGORY_ID";

                OracleDataAdapter da = new OracleDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbMainCategory.DataSource = dt;
                cmbMainCategory.DisplayMember = "NAME";
                cmbMainCategory.ValueMember = "CATEGORY_ID";
                cmbMainCategory.SelectedIndex = -1;
            }
        }

        // =========================================================
        // 이번 달 예산 로드
        // =========================================================
        private void LoadThisMonthBudget()
        {
            dgvThisMonth.Rows.Clear();

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                string ym = cmbMonth.Value.ToString("yyyyMM");

                string sql = @"
                    SELECT 
                        B.BUDGET_ID,
                        B.YYYYMM,
                        C.NAME AS CATEGORY_NAME,
                        B.AMOUNT
                    FROM BUDGETS B
                    JOIN CATEGORIES C ON B.CATEGORY_ID = C.CATEGORY_ID
                    WHERE B.YYYYMM = :YM
                    ORDER BY C.NAME";

                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add(":YM", ym);

                OracleDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    dgvThisMonth.Rows.Add(
                        rd["BUDGET_ID"],
                        FormatMonth(rd["YYYYMM"].ToString()),
                        rd["CATEGORY_NAME"],
                        string.Format("{0:N0}", rd["AMOUNT"])
                    );
                }
            }
        }

        private string FormatMonth(string yyyymm)
        {
            if (string.IsNullOrEmpty(yyyymm) || yyyymm.Length < 6)
                return yyyymm;

            return yyyymm.Insert(4, "-");
        }

        // =========================================================
        // Chart 초기화
        // =========================================================
        private void InitializeChart()
        {
            chartBudget.Series.Clear();
            chartBudget.ChartAreas.Clear();

            ChartArea area = new ChartArea();
            area.AxisX.Title = "날짜";
            area.AxisY.Title = "금액";
            area.AxisX.Interval = 1;

            chartBudget.ChartAreas.Add(area);

            Series budgetSeries = new Series("예산")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3
            };

            Series expenseSeries = new Series("지출")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3
            };

            chartBudget.Series.Add(budgetSeries);
            chartBudget.Series.Add(expenseSeries);
        }

        // =========================================================
        // 예산 vs 지출 그래프 로드
        // =========================================================
        private void LoadBudgetExpenseChart()
        {
            chartBudget.Series["예산"].Points.Clear();
            chartBudget.Series["지출"].Points.Clear();

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                string ym = cmbMonth.Value.ToString("yyyyMM");

                // 1️⃣ 이번 달 총 예산
                string budgetSql = @"
                    SELECT NVL(SUM(AMOUNT), 0)
                    FROM BUDGETS
                    WHERE YYYYMM = :YM";

                OracleCommand budgetCmd = new OracleCommand(budgetSql, conn);
                budgetCmd.Parameters.Add(":YM", ym);

                decimal totalBudget =
                    Convert.ToDecimal(budgetCmd.ExecuteScalar());

                // 2️⃣ 날짜별 지출
                string expenseSql = @"
                    SELECT
                        TO_CHAR(TX_DATE, 'DD') AS DAY,
                        SUM(AMOUNT) AS TOTAL
                    FROM TRANSACTIONS
                    WHERE TO_CHAR(TX_DATE, 'YYYYMM') = :YM
                    GROUP BY TO_CHAR(TX_DATE, 'DD')
                    ORDER BY DAY";

                OracleCommand expenseCmd = new OracleCommand(expenseSql, conn);
                expenseCmd.Parameters.Add(":YM", ym);

                OracleDataReader rd = expenseCmd.ExecuteReader();

                while (rd.Read())
                {
                    string day = rd["DAY"].ToString();
                    decimal expense = Convert.ToDecimal(rd["TOTAL"]);

                    chartBudget.Series["예산"].Points.AddXY(day, totalBudget);
                    chartBudget.Series["지출"].Points.AddXY(day, expense);
                }
            }
        }

        // =========================================================
        // 월 변경
        // =========================================================
        private void cmbMonth_ValueChanged(object sender, EventArgs e)
        {
            if (!isInitialized) return;

            LoadThisMonthBudget();
            LoadBudgetExpenseChart();
        }

        // =========================================================
        // Grid 클릭 → 입력창 채우기
        // =========================================================
        private void Dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvThisMonth.Rows[e.RowIndex];

            cmbMainCategory.Text = row.Cells["CATEGORY_NAME"].Value.ToString();
            txtAmount.Text = row.Cells["AMOUNT"].Value.ToString().Replace(",", "");
        }

        // =========================================================
        // 추가
        // =========================================================
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (cmbMainCategory.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("카테고리와 금액을 입력하세요.");
                return;
            }

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                string ym = cmbMonth.Value.ToString("yyyyMM");

                string checkSql = @"
                    SELECT COUNT(*)
                    FROM BUDGETS
                    WHERE YYYYMM = :YM AND CATEGORY_ID = :CID";

                OracleCommand check = new OracleCommand(checkSql, conn);
                check.Parameters.Add(":YM", ym);
                check.Parameters.Add(":CID", cmbMainCategory.SelectedValue);

                if (Convert.ToInt32(check.ExecuteScalar()) > 0)
                {
                    MessageBox.Show("이미 등록된 카테고리입니다.");
                    return;
                }

                string sql = @"
                    INSERT INTO BUDGETS
                    (BUDGET_ID, CATEGORY_ID, YYYYMM, AMOUNT)
                    VALUES
                    (SEQ_BUDGETS.NEXTVAL, :CID, :YM, :AMT)";

                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add(":CID", cmbMainCategory.SelectedValue);
                cmd.Parameters.Add(":YM", ym);
                cmd.Parameters.Add(":AMT", int.Parse(txtAmount.Text));

                cmd.ExecuteNonQuery();
            }

            LoadThisMonthBudget();
            LoadBudgetExpenseChart();
            ClearInput();
        }

        // =========================================================
        // 수정
        // =========================================================
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dgvThisMonth.SelectedRows.Count == 0)
            {
                MessageBox.Show("수정할 항목을 선택하세요.");
                return;
            }

            int id =
                Convert.ToInt32(dgvThisMonth.SelectedRows[0].Cells["BUDGET_ID"].Value);

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                    UPDATE BUDGETS
                    SET CATEGORY_ID = :CID,
                        AMOUNT = :AMT
                    WHERE BUDGET_ID = :ID";

                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add(":CID", cmbMainCategory.SelectedValue);
                cmd.Parameters.Add(":AMT", int.Parse(txtAmount.Text));
                cmd.Parameters.Add(":ID", id);

                cmd.ExecuteNonQuery();
            }

            LoadThisMonthBudget();
            LoadBudgetExpenseChart();
            ClearInput();
        }

        // =========================================================
        // 삭제
        // =========================================================
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dgvThisMonth.SelectedRows.Count == 0)
            {
                MessageBox.Show("삭제할 항목을 선택하세요.");
                return;
            }

            int id =
                Convert.ToInt32(dgvThisMonth.SelectedRows[0].Cells["BUDGET_ID"].Value);

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                string sql = "DELETE FROM BUDGETS WHERE BUDGET_ID = :ID";

                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.Parameters.Add(":ID", id);
                cmd.ExecuteNonQuery();
            }

            LoadThisMonthBudget();
            LoadBudgetExpenseChart();
            ClearInput();
        }

        private void ClearInput()
        {
            cmbMainCategory.SelectedIndex = -1;
            txtAmount.Clear();
        }
    }
}
