using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Oracle.DataAccess.Client;

namespace WindowsFormsApp4
{
    public partial class FormStats : Form
    {
        private string connectionString =
            "User Id=BANK_MANAGER; Password=1234; Data Source=localhost:1521/XE;";

        public FormStats()
        {
            InitializeComponent();

            // 기본 조회 기간: 이번 달
            dtpStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEnd.Value = DateTime.Now;

            LoadIncomeExpenseLine();
            LoadTotalBalanceLine();
        }

        // =========================================================
        // 조회 버튼
        // =========================================================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadIncomeExpenseLine();
            LoadTotalBalanceLine();
        }

        // =========================================================
        // 1️⃣ 날짜별 수입 / 지출 꺾은선 그래프
        // =========================================================
        private void LoadIncomeExpenseLine()
        {
            chartIncomeExpense.Series.Clear();
            chartIncomeExpense.Titles.Clear();

            Series sIncome = new Series("수입")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.MediumSeaGreen,
                BorderWidth = 3
            };

            Series sExpense = new Series("지출")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.Salmon,
                BorderWidth = 3
            };

            chartIncomeExpense.Series.Add(sIncome);
            chartIncomeExpense.Series.Add(sExpense);

            string sql = @"
                SELECT
                    TO_CHAR(TX_DATE, 'YYYY-MM-DD') AS TX_DAY,
                    SUM(CASE WHEN C.TYPE = 'INCOME'  THEN T.AMOUNT ELSE 0 END) AS INCOME_AMT,
                    SUM(CASE WHEN C.TYPE = 'EXPENSE' THEN T.AMOUNT ELSE 0 END) AS EXPENSE_AMT
                FROM TRANSACTIONS T
                JOIN SUB_CATEGORIES S ON T.SUB_ID = S.SUB_ID
                JOIN CATEGORIES C ON S.CATEGORY_ID = C.CATEGORY_ID
                WHERE TX_DATE BETWEEN :START_DATE AND :END_DATE
                GROUP BY TO_CHAR(TX_DATE, 'YYYY-MM-DD')
                ORDER BY TX_DAY
            ";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.BindByName = true;
                cmd.Parameters.Add(":START_DATE", dtpStart.Value.Date);
                cmd.Parameters.Add(":END_DATE", dtpEnd.Value.Date.AddDays(1).AddSeconds(-1));

                OracleDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    string day = rd["TX_DAY"].ToString();
                    double income = Convert.ToDouble(rd["INCOME_AMT"]);
                    double expense = Convert.ToDouble(rd["EXPENSE_AMT"]);

                    sIncome.Points.AddXY(day, income);
                    sExpense.Points.AddXY(day, expense);
                }
            }

            chartIncomeExpense.Titles.Add("📈 기간별 수입 · 지출 추이");
        }

        // =========================================================
        // 2️⃣ 날짜별 총 잔액 변화 꺾은선 그래프
        // =========================================================
        private void LoadTotalBalanceLine()
        {
            chartBalance.Series.Clear();
            chartBalance.Titles.Clear();

            Series sBalance = new Series("총 잔액")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.SteelBlue,
                BorderWidth = 3
            };

            chartBalance.Series.Add(sBalance);

            double baseBalance = GetInitialTotalBalance();

            string sql = @"
                SELECT
                    TX_DAY,
                    SUM(DELTA) OVER (ORDER BY TX_DAY)
                        + :BASE_BALANCE AS TOTAL_BALANCE
                FROM (
                    SELECT
                        TO_CHAR(TX_DATE, 'YYYY-MM-DD') AS TX_DAY,
                        SUM(
                            CASE
                                WHEN C.TYPE = 'INCOME'  THEN T.AMOUNT
                                WHEN C.TYPE = 'EXPENSE' THEN -T.AMOUNT
                            END
                        ) AS DELTA
                    FROM TRANSACTIONS T
                    JOIN SUB_CATEGORIES S ON T.SUB_ID = S.SUB_ID
                    JOIN CATEGORIES C ON S.CATEGORY_ID = C.CATEGORY_ID
                    WHERE TX_DATE BETWEEN :START_DATE AND :END_DATE
                    GROUP BY TO_CHAR(TX_DATE, 'YYYY-MM-DD')
                )
                ORDER BY TX_DAY
            ";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.BindByName = true;
                cmd.Parameters.Add(":BASE_BALANCE", baseBalance);
                cmd.Parameters.Add(":START_DATE", dtpStart.Value.Date);
                cmd.Parameters.Add(":END_DATE", dtpEnd.Value.Date.AddDays(1).AddSeconds(-1));

                OracleDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    string day = rd["TX_DAY"].ToString();
                    double balance = Convert.ToDouble(rd["TOTAL_BALANCE"]);

                    sBalance.Points.AddXY(day, balance);
                }
            }

            chartBalance.Titles.Add("💰 기간별 총 자산 잔액 변화");
        }

        // =========================================================
        // 초기 총 자산 잔액
        // =========================================================
        private double GetInitialTotalBalance()
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                OracleCommand cmd =
                    new OracleCommand("SELECT NVL(SUM(BALANCE),0) FROM ASSETS", conn);

                return Convert.ToDouble(cmd.ExecuteScalar());
            }
        }
    }
}
