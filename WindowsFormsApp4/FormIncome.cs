using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client; // Oracle 라이브러리

namespace WindowsFormsApp4
{
    public partial class FormIncome : Form
    {
        private DataTable incomeTable;

        // ▼ 본인 DB 정보로 수정 필수
        private string connectionString = "User Id=BANK_MANAGER; Password=1234; Data Source=localhost:1521/XE;";

        private bool isLoading = false;

        public FormIncome()
        {
            InitializeComponent();
            isLoading = true;

            // 1. 테이블 및 그리드 설정
            InitializeIncomeTable();

            // 2. 기초 데이터 로딩 (콤보박스 채우기)
            LoadMainCategories();
            LoadPaymentMethods();

            // 3. 오늘 날짜 내역 조회
            dtpDate.Value = DateTime.Now;
            LoadIncomeFromDB();

            isLoading = false;
        }

        // =========================================================
        // 1. 데이터 로딩 (DB에서 목록 가져오기)
        // =========================================================

        // [대분류] 불러오기 (★ 수입 카테고리만!)
        private void LoadMainCategories()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // ★ 핵심 변경: WHERE TYPE = 'INCOME' (수입만 가져옴)
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

        // [소분류] 불러오기
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

        // [결제수단] 불러오기 (입금 계좌 등)
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

        // =========================================================
        // 2. 콤보박스 이벤트 (대분류 선택 시 -> 소분류 갱신)
        // =========================================================
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

        // 디자이너 오류 방지용 빈 함수들
        private void cmbSubCategory_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbPayType_SelectedIndexChanged(object sender, EventArgs e) { }


        // =========================================================
        // 3. 조회 (READ) - 수입 내역만
        // =========================================================
        private void LoadIncomeFromDB()
        {
            incomeTable.Rows.Clear();
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"
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
                        WHERE C.TYPE = 'INCOME'
                          AND TRUNC(T.TX_DATE) = TO_DATE(:검색날짜, 'YYYY-MM-DD')
                        ORDER BY T.TX_ID DESC";

                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.Parameters.Add(":검색날짜", dtpDate.Value.ToString("yyyy-MM-dd"));
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
        // 4. 버튼 이벤트 (추가/수정/삭제)
        // =========================================================
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

        // =========================================================
        // 5. 기타 UI 이벤트
        // =========================================================
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
            // 사용자 친화적인 컬럼명 사용
            incomeTable.Columns.Add("거래ID", typeof(int));
            incomeTable.Columns.Add("날짜", typeof(string));
            incomeTable.Columns.Add("결제수단", typeof(string));
            incomeTable.Columns.Add("결제수단ID", typeof(int));
            incomeTable.Columns.Add("항목", typeof(string));      // 대분류
            incomeTable.Columns.Add("항목ID", typeof(int));
            incomeTable.Columns.Add("세부내역", typeof(string));  // 소분류
            incomeTable.Columns.Add("세부내역ID", typeof(int));
            incomeTable.Columns.Add("금액", typeof(int));
            incomeTable.Columns.Add("메모", typeof(string));

            dgvIncome.DataSource = incomeTable;

            // 숨김 처리
            dgvIncome.Columns["거래ID"].Visible = false;
            dgvIncome.Columns["결제수단ID"].Visible = false;
            dgvIncome.Columns["항목ID"].Visible = false;
            dgvIncome.Columns["세부내역ID"].Visible = false;

            // 스타일
            dgvIncome.Columns["금액"].DefaultCellStyle.Format = "N0";
            dgvIncome.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 이벤트
            dgvIncome.CellClick += dgvIncome_CellClick;
            dtpDate.ValueChanged += (s, e) => LoadIncomeFromDB();
            cmbMainCategory.SelectedIndexChanged += cmbMainCategory_SelectedIndexChanged;
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