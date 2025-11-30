using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // 차트 필수
using Oracle.DataAccess.Client;

namespace WindowsFormsApp4
{
    public partial class FormStats : Form
    {
        private string connectionString = "User Id=BANK_MANAGER; Password=1234; Data Source=localhost:1521/XE;";

        public FormStats()
        {
            InitializeComponent();

            // 1. 날짜 포맷 (년-월)
            cmbMonth.Format = DateTimePickerFormat.Custom;
            cmbMonth.CustomFormat = "yyyy-MM";
            cmbMonth.ShowUpDown = true;

            // 2. 시작 시 이번 달 조회
            cmbMonth.Value = DateTime.Now;
            LoadBudgetVsExpenseChart();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadBudgetVsExpenseChart();
        }

        // =========================================================
        // 예산 vs 지출 비교 차트 그리기
        // =========================================================
        private void LoadBudgetVsExpenseChart()
        {
            // 1. 차트 초기화
            chartStats.Series.Clear();
            chartStats.Titles.Clear();
            chartStats.ChartAreas[0].AxisX.Interval = 1; // 모든 카테고리 이름 표시

            // 2. 제목 설정
            string targetMonth = cmbMonth.Value.ToString("yyyy-MM");
            Title title = chartStats.Titles.Add($"{targetMonth} 예산 대비 지출 현황");
            title.Font = new Font("맑은 고딕", 16, FontStyle.Bold);

            // 3. 시리즈 2개 생성 (예산 막대, 지출 막대)
            Series seriesBudget = new Series("예산(목표)");
            seriesBudget.ChartType = SeriesChartType.Bar; // 가로 막대
            seriesBudget.Color = Color.LightGray; // 예산은 은은하게
            seriesBudget.IsValueShownAsLabel = true;

            Series seriesExpense = new Series("지출(실제)");
            seriesExpense.ChartType = SeriesChartType.Bar;
            seriesExpense.Color = Color.Salmon; // 지출은 눈에 띄게
            seriesExpense.IsValueShownAsLabel = true;

            // 4. DB 조회 (FULL OUTER JOIN 사용)
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // ★ SQL 설명:
                    // 1) B: 해당 월의 카테고리별 예산 합계
                    // 2) T: 해당 월의 카테고리별 지출 합계
                    // 3) FULL OUTER JOIN: 예산만 있거나 지출만 있는 경우도 모두 표시
                    string sql = @"
                        SELECT 
                            NVL(B.NAME, T.NAME) AS 카테고리,
                            NVL(B.BUDGET_AMT, 0) AS 예산,
                            NVL(T.EXPENSE_AMT, 0) AS 지출
                        FROM 
                            ( -- 1. 예산 서브쿼리
                                SELECT C.CATEGORY_ID, C.NAME, SUM(B.AMOUNT) AS BUDGET_AMT
                                FROM BUDGETS B
                                JOIN CATEGORIES C ON B.CATEGORY_ID = C.CATEGORY_ID
                                WHERE B.YYYYMM = :Ym
                                GROUP BY C.CATEGORY_ID, C.NAME
                            ) B
                            FULL OUTER JOIN 
                            ( -- 2. 지출 서브쿼리
                                SELECT C.CATEGORY_ID, C.NAME, SUM(TR.AMOUNT) AS EXPENSE_AMT
                                FROM TRANSACTIONS TR
                                JOIN SUB_CATEGORIES S ON TR.SUB_ID = S.SUB_ID
                                JOIN CATEGORIES C ON S.CATEGORY_ID = C.CATEGORY_ID
                                WHERE TO_CHAR(TR.TX_DATE, 'YYYYMM') = :Ym
                                  AND C.TYPE = 'EXPENSE'
                                GROUP BY C.CATEGORY_ID, C.NAME
                            ) T
                            ON B.CATEGORY_ID = T.CATEGORY_ID
                        ORDER BY 지출 DESC
                    ";

                    OracleCommand cmd = new OracleCommand(sql, conn);
                    // 파라미터 :Ym (예: '202511') -> 위아래 두 군데 다 들어감
                    string strYm = cmbMonth.Value.ToString("yyyyMM");
                    cmd.Parameters.Add(":Ym", strYm);

                    // 주의: 오라클에서 파라미터를 여러 번 쓸 때는 순서대로 넣어줘야 하거나,
                    // BindByName을 true로 해야 함. 안전하게 BindByName 사용 추천.
                    cmd.BindByName = true;

                    OracleDataReader rd = cmd.ExecuteReader();

                    bool hasData = false;
                    while (rd.Read())
                    {
                        hasData = true;
                        string catName = rd["카테고리"].ToString();
                        long budget = Convert.ToInt64(rd["예산"]);
                        long expense = Convert.ToInt64(rd["지출"]);

                        // 차트에 데이터 추가
                        seriesBudget.Points.AddXY(catName, budget);

                        int idx = seriesExpense.Points.AddXY(catName, expense);

                        // ★ 시각적 효과: 예산 초과 시 빨간색 경고!
                        if (expense > budget && budget > 0)
                        {
                            seriesExpense.Points[idx].Color = Color.Red;
                            seriesExpense.Points[idx].Label = $"{expense:N0} (초과!)";
                        }
                    }

                    // 차트에 시리즈 등록
                    chartStats.Series.Add(seriesBudget);
                    chartStats.Series.Add(seriesExpense);

                    if (!hasData)
                    {
                        MessageBox.Show("해당 월에 데이터가 없습니다.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("통계 로딩 오류: " + ex.Message);
                }
            }
        }
    }
}