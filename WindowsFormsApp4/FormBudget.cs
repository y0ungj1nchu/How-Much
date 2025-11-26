using System;
using System.Data;
using System.Windows.Forms;
using Oracle.DataAccess.Client; // Oracle 라이브러리

namespace WindowsFormsApp4
{
    public partial class FormBudget : Form
    {
        // ▼ 본인 DB 정보로 수정 필수
        private string connectionString = "User Id=BANK_MANAGER; Password=1234; Data Source=localhost:1521/XE;";

        public FormBudget()
        {
            InitializeComponent();

            // 1. 리스트뷰 컬럼 세팅
            InitializeListView();

            // 2. 콤보박스(카테고리) 채우기
            LoadCategoryData();

            // 3. 시작 시 이번 달 예산 조회
            cmbMonth.Value = DateTime.Now; // 날짜 선택기 오늘로 설정
            LoadBudgetData();
        }

        // =========================================================
        // 1. 초기 설정 (리스트뷰 컬럼 & 카테고리 로딩)
        // =========================================================
        private void InitializeListView()
        {
            // 리스트뷰 설정 (디자인 창에서 이미 했다면 생략 가능하지만 안전하게)
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            // 컬럼 추가 (폭은 적절히 조절)
            listView1.Columns.Add("년월", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("카테고리", 150, HorizontalAlignment.Center);
            listView1.Columns.Add("예산 금액", 150, HorizontalAlignment.Right);
            listView1.Columns.Add("메모", 300, HorizontalAlignment.Left);
        }

        private void LoadCategoryData()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // '지출(EXPENSE)' 카테고리만 예산 설정 가능
                    string sql = "SELECT CATEGORY_ID, NAME FROM CATEGORIES WHERE TYPE = 'EXPENSE' ORDER BY CATEGORY_ID";

                    OracleDataAdapter da = new OracleDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbMainCategory.DataSource = dt;
                    cmbMainCategory.DisplayMember = "NAME";
                    cmbMainCategory.ValueMember = "CATEGORY_ID";

                    cmbMainCategory.SelectedIndex = -1; // 초기 선택 없음
                }
                catch (Exception ex) { MessageBox.Show("카테고리 로딩 실패: " + ex.Message); }
            }
        }

        // =========================================================
        // 2. 조회 (READ) -> 리스트뷰에 뿌리기
        // =========================================================
        private void LoadBudgetData()
        {
            listView1.Items.Clear(); // 기존 목록 초기화

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 선택된 년월(YYYYMM) 문자열 만들기
                    string targetMonth = cmbMonth.Value.ToString("yyyyMM");

                    string sql = @"
                        SELECT 
                            B.BUDGET_ID, 
                            B.YYYYMM, 
                            C.NAME AS CATEGORY_NAME, 
                            C.CATEGORY_ID,
                            B.AMOUNT,
                            '메모 기능 없음' AS MEMO -- 예산 테이블에 메모 컬럼이 없어서 임시 처리
                        FROM BUDGETS B
                        JOIN CATEGORIES C ON B.CATEGORY_ID = C.CATEGORY_ID
                        WHERE B.YYYYMM = :TargetMonth
                        ORDER BY C.NAME ASC";

                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.Parameters.Add(":TargetMonth", targetMonth);

                    OracleDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        // ★ 리스트뷰 아이템 생성
                        // 1번째 컬럼: 년월 (보여주기용 포맷팅 yyyy-MM)
                        string yyyymm = rd["YYYYMM"].ToString();
                        string displayMonth = yyyymm.Substring(0, 4) + "-" + yyyymm.Substring(4, 2);

                        ListViewItem item = new ListViewItem(displayMonth);

                        // 서브 아이템 추가 (카테고리, 금액, 메모)
                        item.SubItems.Add(rd["CATEGORY_NAME"].ToString());
                        item.SubItems.Add(string.Format("{0:N0}", rd["AMOUNT"])); // 천단위 콤마
                        item.SubItems.Add(rd["MEMO"].ToString());

                        // ★ 핵심: 화면엔 안 보이지만 중요한 ID값들을 Tag에 숨겨둠
                        item.Tag = Convert.ToInt32(rd["BUDGET_ID"]);

                        // 리스트뷰에 추가
                        listView1.Items.Add(item);
                    }
                }
                catch (Exception ex) { MessageBox.Show("조회 실패: " + ex.Message); }
            }
        }

        // 날짜 선택 변경 시 자동 조회
        private void cmbMonth_ValueChanged(object sender, EventArgs e)
        {
            LoadBudgetData();
        }

        // =========================================================
        // 3. 추가 (INSERT)
        // =========================================================
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (cmbMainCategory.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("카테고리와 금액을 입력하세요.");
                return;
            }

            string targetMonth = cmbMonth.Value.ToString("yyyyMM");

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 중복 체크 (이미 해당 월에 해당 카테고리 예산이 있는지)
                    string checkSql = "SELECT COUNT(*) FROM BUDGETS WHERE YYYYMM = :Ym AND CATEGORY_ID = :CatID";
                    OracleCommand checkCmd = new OracleCommand(checkSql, conn);
                    checkCmd.Parameters.Add(":Ym", targetMonth);
                    checkCmd.Parameters.Add(":CatID", Convert.ToInt32(cmbMainCategory.SelectedValue));

                    if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("이미 등록된 카테고리입니다. 수정 기능을 이용해주세요.");
                        return;
                    }

                    // 저장
                    string sql = @"
                        INSERT INTO BUDGETS (BUDGET_ID, CATEGORY_ID, YYYYMM, AMOUNT)
                        VALUES (SEQ_BUDGETS.NEXTVAL, :CatID, :Ym, :Amt)";

                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.Parameters.Add(":CatID", Convert.ToInt32(cmbMainCategory.SelectedValue));
                    cmd.Parameters.Add(":Ym", targetMonth);
                    cmd.Parameters.Add(":Amt", int.Parse(txtAmount.Text));

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("예산이 추가되었습니다.");
                    LoadBudgetData();
                    ClearInput();
                }
                catch (Exception ex) { MessageBox.Show("추가 실패: " + ex.Message); }
            }
        }

        // =========================================================
        // 4. 수정 (UPDATE)
        // =========================================================
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("수정할 예산을 선택하세요.");
                return;
            }

            // ★ Tag에 숨겨둔 ID 꺼내오기
            int budgetId = Convert.ToInt32(listView1.SelectedItems[0].Tag);
            string targetMonth = cmbMonth.Value.ToString("yyyyMM");

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        UPDATE BUDGETS 
                        SET CATEGORY_ID = :CatID, 
                            YYYYMM = :Ym, 
                            AMOUNT = :Amt
                        WHERE BUDGET_ID = :ID";

                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.Parameters.Add(":CatID", Convert.ToInt32(cmbMainCategory.SelectedValue));
                    cmd.Parameters.Add(":Ym", targetMonth);
                    cmd.Parameters.Add(":Amt", int.Parse(txtAmount.Text));
                    cmd.Parameters.Add(":ID", budgetId);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("수정되었습니다.");
                    LoadBudgetData();
                    ClearInput();
                }
                catch (Exception ex) { MessageBox.Show("수정 실패: " + ex.Message); }
            }
        }

        // =========================================================
        // 5. 삭제 (DELETE)
        // =========================================================
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("삭제할 예산을 선택하세요.");
                return;
            }

            if (MessageBox.Show("정말 삭제하시겠습니까?", "확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int budgetId = Convert.ToInt32(listView1.SelectedItems[0].Tag);

                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string sql = "DELETE FROM BUDGETS WHERE BUDGET_ID = :ID";
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        cmd.Parameters.Add(":ID", budgetId);
                        cmd.ExecuteNonQuery();

                        LoadBudgetData();
                        ClearInput();
                    }
                    catch (Exception ex) { MessageBox.Show("삭제 실패: " + ex.Message); }
                }
            }
        }

        // =========================================================
        // 6. 리스트뷰 클릭 시 입력창 채우기
        // =========================================================
        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;

            ListViewItem item = listView1.SelectedItems[0];

            // 1. 카테고리 이름으로 콤보박스 선택
            string categoryName = item.SubItems[1].Text;
            cmbMainCategory.Text = categoryName;

            // 2. 금액 (쉼표 제거 후 텍스트박스에 넣기)
            txtAmount.Text = item.SubItems[2].Text.Replace(",", "");

            // 3. 메모 (DB에 없어서 그냥 비워두거나 UI 값 넣음)
            txtMemo.Text = item.SubItems[3].Text;
        }

        private void ClearInput()
        {
            cmbMainCategory.SelectedIndex = -1;
            txtAmount.Clear();
            txtMemo.Clear();
            listView1.SelectedItems.Clear(); // 선택 해제
        }
    }
}