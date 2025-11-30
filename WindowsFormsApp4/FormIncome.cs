using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client; // Oracle 라이브러리

namespace WindowsFormsApp4
{
    public partial class FormIncome : Form
    {
        private DataTable incomeTable;
        private string connectionString = "User Id=BANK_MANAGER; Password=1234; Data Source=localhost:1521/XE;";
        private bool isLoading = false;

        // ★ 조회 모드 상태 변수 (true: 월별 / false: 일별)
        private bool isMonthViewMode = true;

        public FormIncome()
        {
            InitializeComponent();
            isLoading = true;

            InitializeIncomeTable();

            LoadMainCategories();
            LoadPaymentMethods();

            // 1. 초기 로드: 오늘 날짜 기준 '이번 달 전체 수입' 조회
            dtpDate.Value = DateTime.Now;
            isMonthViewMode = true; // 월별 모드 강제
            LoadIncomeFromDB();

            isLoading = false;
        }

        // =========================================================
        // ★ 조회 로직 수정 (월별/일별 분기 처리)
        // =========================================================
        private void LoadIncomeFromDB()
        {
            incomeTable.Rows.Clear();
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string dateCondition = "";
                    string dateParamValue = "";

                    // ★ 모드에 따라 SQL 조건문과 파라미터 변경
                    if (isMonthViewMode)
                    {
                        // [월별 보기] YYYY-MM
                        dateCondition = "AND TO_CHAR(T.TX_DATE, 'YYYY-MM') = :DateParam";
                        dateParamValue = dtpDate.Value.ToString("yyyy-MM");
                    }
                    else
                    {
                        // [일별 보기] YYYY-MM-DD
                        dateCondition = "AND TRUNC(T.TX_DATE) = TO_DATE(:DateParam, 'YYYY-MM-DD')";
                        dateParamValue = dtpDate.Value.ToString("yyyy-MM-dd");
                    }

                    string sql = $@"
                        SELECT 
                            T.TX_ID         AS 거래ID,
                            TO_CHAR(T.TX_DATE, 'YYYY-MM-DD') AS 날짜,
                            PM.NAME         AS 결제수단,
                            PM.PM_ID        AS 결제수단ID,
                            C.NAME          AS 항목,
                            C.CATEGORY_ID   AS 항목ID,
                            S.NAME          AS 세부내역,
                            S.SUB_ID        AS 세부내역ID,
                            T.AMOUNT        AS 금액,
                            T.MEMO          AS 메모
                        FROM TRANSACTIONS T
                        JOIN PAYMENT_METHODS PM ON T.PM_ID = PM.PM_ID
                        JOIN SUB_CATEGORIES S   ON T.SUB_ID = S.SUB_ID
                        JOIN CATEGORIES C       ON S.CATEGORY_ID = C.CATEGORY_ID
                        WHERE C.TYPE = 'INCOME'  -- ★ 수입만 필터링
                          {dateCondition}        -- ★ 날짜 조건 삽입
                        ORDER BY T.TX_DATE DESC, T.TX_ID DESC";

                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.Parameters.Add(":DateParam", dateParamValue);

                    OracleDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        incomeTable.Rows.Add(
                            rd["거래ID"], rd["날짜"], rd["결제수단"], rd["결제수단ID"],
                            rd["항목"], rd["항목ID"], rd["세부내역"], rd["세부내역ID"],
                            rd["금액"], rd["메모"]
                        );
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        // =========================================================
        // ★ 이벤트 1: 날짜 변경 -> 일별 보기
        // =========================================================
        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            isMonthViewMode = false;
            LoadIncomeFromDB();
        }

        // =========================================================
        // ★ 이벤트 2: 월별 보기 버튼 클릭 -> 월별 보기
        // =========================================================
        private void Monthbtn_Click(object sender, EventArgs e)
        {
            isMonthViewMode = true;
            LoadIncomeFromDB();
        }

        // ---------------------------------------------------------
        // (이하 데이터 로딩 및 CRUD 로직은 동일)
        // ---------------------------------------------------------

        private void LoadMainCategories()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT CATEGORY_ID, NAME FROM CATEGORIES WHERE TYPE = 'INCOME' ORDER BY CATEGORY_ID";
                    OracleDataAdapter da = new OracleDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbMainCategory.DataSource = dt;
                    cmbMainCategory.DisplayMember = "NAME";
                    cmbMainCategory.ValueMember = "CATEGORY_ID";
                    cmbMainCategory.SelectedIndex = -1;
                }
                catch (Exception ex) { MessageBox.Show("대분류 로딩 실패: " + ex.Message); }
            }
        }

        private void LoadSubCategories(int mainId)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT SUB_ID, NAME FROM SUB_CATEGORIES WHERE CATEGORY_ID = :MainID ORDER BY NAME";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.Parameters.Add(":MainID", mainId);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbSubCategory.DataSource = dt;
                    cmbSubCategory.DisplayMember = "NAME";
                    cmbSubCategory.ValueMember = "SUB_ID";
                    cmbSubCategory.SelectedIndex = -1;
                }
                catch (Exception ex) { MessageBox.Show("소분류 로딩 실패: " + ex.Message); }
            }
        }

        private void LoadPaymentMethods()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT PM_ID, NAME FROM PAYMENT_METHODS ORDER BY NAME";
                    OracleDataAdapter da = new OracleDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbPayType.DataSource = dt;
                    cmbPayType.DisplayMember = "NAME";
                    cmbPayType.ValueMember = "PM_ID";
                    cmbPayType.SelectedIndex = -1;
                }
                catch (Exception ex) { MessageBox.Show("결제수단 로딩 실패: " + ex.Message); }
            }
        }

        private void cmbMainCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (cmbMainCategory.SelectedIndex == -1 || cmbMainCategory.SelectedValue == null) return;
            try
            {
                int selectedId = Convert.ToInt32(cmbMainCategory.SelectedValue);
                LoadSubCategories(selectedId);
            }
            catch { }
        }
        private void cmbSubCategory_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbPayType_SelectedIndexChanged(object sender, EventArgs e) { }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbSubCategory.SelectedIndex == -1 || cmbPayType.SelectedIndex == -1)
            {
                MessageBox.Show("카테고리와 결제수단을 선택해주세요.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("금액을 입력하세요.");
                return;
            }

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO TRANSACTIONS (TX_ID, TX_DATE, PM_ID, SUB_ID, AMOUNT, MEMO)
                        VALUES (SEQ_TX.NEXTVAL, TO_DATE(:Dt, 'YYYY-MM-DD'), :PM, :Sub, :Amt, :Memo)";

                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.Parameters.Add(":Dt", dtpDate.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.Add(":PM", Convert.ToInt32(cmbPayType.SelectedValue));
                    cmd.Parameters.Add(":Sub", Convert.ToInt32(cmbSubCategory.SelectedValue));
                    cmd.Parameters.Add(":Amt", int.Parse(txtAmount.Text));
                    cmd.Parameters.Add(":Memo", txtMemo.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("저장되었습니다.");
                    LoadIncomeFromDB();
                    ClearInput();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvIncome.CurrentRow == null) return;
            int txId = Convert.ToInt32(dgvIncome.CurrentRow.Cells["거래ID"].Value);

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        UPDATE TRANSACTIONS 
                        SET TX_DATE = TO_DATE(:Dt, 'YYYY-MM-DD'), 
                            PM_ID = :PM, 
                            SUB_ID = :Sub, 
                            AMOUNT = :Amt, 
                            MEMO = :Memo 
                        WHERE TX_ID = :ID";

                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.Parameters.Add(":Dt", dtpDate.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.Add(":PM", Convert.ToInt32(cmbPayType.SelectedValue));
                    cmd.Parameters.Add(":Sub", Convert.ToInt32(cmbSubCategory.SelectedValue));
                    cmd.Parameters.Add(":Amt", int.Parse(txtAmount.Text));
                    cmd.Parameters.Add(":Memo", txtMemo.Text);
                    cmd.Parameters.Add(":ID", txId);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("수정되었습니다.");
                    LoadIncomeFromDB();
                    ClearInput();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvIncome.CurrentRow == null) return;
            int txId = Convert.ToInt32(dgvIncome.CurrentRow.Cells["거래ID"].Value);

            if (MessageBox.Show("삭제하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        OracleCommand cmd = new OracleCommand("DELETE FROM TRANSACTIONS WHERE TX_ID = :ID", conn);
                        cmd.Parameters.Add(":ID", txId);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                LoadIncomeFromDB();
                ClearInput();
            }
        }

        // 검색 버튼은 수동 새로고침용
        private void SearchBtn_Click(object sender, EventArgs e)
        {
            LoadIncomeFromDB();
        }

        private void dgvIncome_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            isLoading = true;

            try
            {
                dtpDate.Value = DateTime.Parse(dgvIncome.Rows[e.RowIndex].Cells["날짜"].Value.ToString());
                txtAmount.Text = dgvIncome.Rows[e.RowIndex].Cells["금액"].Value.ToString();
                txtMemo.Text = dgvIncome.Rows[e.RowIndex].Cells["메모"].Value.ToString();

                cmbPayType.SelectedValue = Convert.ToInt32(dgvIncome.Rows[e.RowIndex].Cells["결제수단ID"].Value);

                int mainId = Convert.ToInt32(dgvIncome.Rows[e.RowIndex].Cells["항목ID"].Value);
                cmbMainCategory.SelectedValue = mainId;

                LoadSubCategories(mainId);
                cmbSubCategory.SelectedValue = Convert.ToInt32(dgvIncome.Rows[e.RowIndex].Cells["세부내역ID"].Value);
            }
            catch { }

            isLoading = false;
        }

        private void InitializeIncomeTable()
        {
            incomeTable = new DataTable();
            incomeTable.Columns.Add("거래ID", typeof(int));
            incomeTable.Columns.Add("날짜", typeof(string));
            incomeTable.Columns.Add("결제수단", typeof(string));
            incomeTable.Columns.Add("결제수단ID", typeof(int));
            incomeTable.Columns.Add("항목", typeof(string));
            incomeTable.Columns.Add("항목ID", typeof(int));
            incomeTable.Columns.Add("세부내역", typeof(string));
            incomeTable.Columns.Add("세부내역ID", typeof(int));
            incomeTable.Columns.Add("금액", typeof(int));
            incomeTable.Columns.Add("메모", typeof(string));

            dgvIncome.DataSource = incomeTable;

            dgvIncome.Columns["거래ID"].Visible = false;
            dgvIncome.Columns["결제수단ID"].Visible = false;
            dgvIncome.Columns["항목ID"].Visible = false;
            dgvIncome.Columns["세부내역ID"].Visible = false;

            dgvIncome.Columns["금액"].DefaultCellStyle.Format = "N0";
            dgvIncome.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvIncome.CellClick += dgvIncome_CellClick;

            // ★ 이벤트 연결 (dtpDate_ValueChanged는 여기서 연결하거나 디자이너에서 연결)
            dtpDate.ValueChanged += dtpDate_ValueChanged;

            cmbMainCategory.SelectedIndexChanged += cmbMainCategory_SelectedIndexChanged;
            cmbSubCategory.SelectedIndexChanged += cmbSubCategory_SelectedIndexChanged;
            cmbPayType.SelectedIndexChanged += cmbPayType_SelectedIndexChanged;
        }

        private void ClearInput()
        {
            cmbMainCategory.SelectedIndex = -1;
            cmbSubCategory.DataSource = null;
            cmbPayType.SelectedIndex = -1;
            txtAmount.Clear();
            txtMemo.Clear();
        }
    }
}