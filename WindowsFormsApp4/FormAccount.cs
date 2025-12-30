using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace WindowsFormsApp4
{
    public partial class FormAccount : Form
    {
        private OracleConnection conn;
        private DataTable assetTable;
        private bool isLoading = false;

        public FormAccount()
        {
            InitializeComponent();
            InitializeDB();
            LoadAssets();   // ✔ 자산 목록 로드

            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.Value = DateTime.Now;

            HookEvents();
            LoadAccounts(); // ✔ 자산 기준 잔액 계산
        }

        // ----------------------------
        // DB 연결
        // ----------------------------
        private void InitializeDB()
        {
            conn = new OracleConnection(
                "User Id=BANK_MANAGER; Password=1234; Data Source=localhost:1521/XE;");
        }

        // ----------------------------
        // 자산 목록 로드 (콤보박스)
        // ----------------------------
        private void LoadAssets()
        {
            using (OracleCommand cmd = new OracleCommand(
                "SELECT ASSET_ID, NAME FROM ASSETS ORDER BY ASSET_ID", conn))
            {
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();

                cmbMethod.DataSource = dt;
                cmbMethod.DisplayMember = "NAME";
                cmbMethod.ValueMember = "ASSET_ID";
                cmbMethod.SelectedIndex = -1;
            }
        }

        // ----------------------------
        // 자산 집계 로드 (ASSET 단위 잔액)
        // ----------------------------
        private void LoadAccounts()
        {
            if (conn == null) InitializeDB();

            isLoading = true;

            string sql = @"
                SELECT
                    A.ASSET_ID,
                    A.NAME AS 자산명,
                    A.TYPE AS 자산유형,
                    A.BALANCE AS 초기잔액,

                    NVL(SUM(CASE WHEN C.TYPE = 'INCOME'  THEN T.AMOUNT END), 0) AS 기간수입,
                    NVL(SUM(CASE WHEN C.TYPE = 'EXPENSE' THEN T.AMOUNT END), 0) AS 기간지출,

                    (SELECT NVL(SUM(T2.AMOUNT),0)
                     FROM TRANSACTIONS T2
                     JOIN SUB_CATEGORIES S2 ON S2.SUB_ID = T2.SUB_ID
                     JOIN CATEGORIES C2 ON C2.CATEGORY_ID = S2.CATEGORY_ID
                     JOIN PAY_METHODS M2 ON M2.METHOD_ID = T2.METHOD_ID
                     WHERE M2.ASSET_ID = A.ASSET_ID
                       AND C2.TYPE = 'INCOME'
                    ) AS 총수입,

                    (SELECT NVL(SUM(T3.AMOUNT),0)
                     FROM TRANSACTIONS T3
                     JOIN SUB_CATEGORIES S3 ON S3.SUB_ID = T3.SUB_ID
                     JOIN CATEGORIES C3 ON C3.CATEGORY_ID = S3.CATEGORY_ID
                     JOIN PAY_METHODS M3 ON M3.METHOD_ID = T3.METHOD_ID
                     WHERE M3.ASSET_ID = A.ASSET_ID
                       AND C3.TYPE = 'EXPENSE'
                    ) AS 총지출,

                    A.BALANCE
                      + (SELECT NVL(SUM(T2.AMOUNT),0)
                         FROM TRANSACTIONS T2
                         JOIN SUB_CATEGORIES S2 ON S2.SUB_ID = T2.SUB_ID
                         JOIN CATEGORIES C2 ON C2.CATEGORY_ID = S2.CATEGORY_ID
                         JOIN PAY_METHODS M2 ON M2.METHOD_ID = T2.METHOD_ID
                         WHERE M2.ASSET_ID = A.ASSET_ID
                           AND C2.TYPE = 'INCOME')
                      - (SELECT NVL(SUM(T3.AMOUNT),0)
                         FROM TRANSACTIONS T3
                         JOIN SUB_CATEGORIES S3 ON S3.SUB_ID = T3.SUB_ID
                         JOIN CATEGORIES C3 ON C3.CATEGORY_ID = S3.CATEGORY_ID
                         JOIN PAY_METHODS M3 ON M3.METHOD_ID = T3.METHOD_ID
                         WHERE M3.ASSET_ID = A.ASSET_ID
                           AND C3.TYPE = 'EXPENSE')
                      AS 현재잔액

                FROM ASSETS A
                LEFT JOIN PAY_METHODS M ON M.ASSET_ID = A.ASSET_ID
                LEFT JOIN TRANSACTIONS T 
                    ON T.METHOD_ID = M.METHOD_ID
                    AND T.TX_DATE BETWEEN :p_start AND :p_end   -- 기간 흐름만 날짜필터
                LEFT JOIN SUB_CATEGORIES S ON S.SUB_ID = T.SUB_ID
                LEFT JOIN CATEGORIES C ON C.CATEGORY_ID = S.CATEGORY_ID
                GROUP BY A.ASSET_ID, A.NAME, A.TYPE, A.BALANCE
                ORDER BY A.ASSET_ID
            ";

            using (OracleCommand cmd = new OracleCommand(sql, conn))
            {
                cmd.Parameters.Add(":p_start", dtpStartDate.Value.Date);
                cmd.Parameters.Add(":p_end", dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1));

                conn.Open();
                assetTable = new DataTable();
                assetTable.Load(cmd.ExecuteReader());
                conn.Close();
            }

            dgvAccount.DataSource = assetTable;

            if (dgvAccount.Columns["ASSET_ID"] != null)
                dgvAccount.Columns["ASSET_ID"].Visible = false;

            ApplyFilters();
            isLoading = false;
        }

        // ----------------------------
        // 필터 적용 (자산 기준)
        // ----------------------------
        private void ApplyFilters()
        {
            if (assetTable == null) return;

            DataView dv = new DataView(assetTable);

            if (cmbMethod.SelectedIndex >= 0)
            {
                int assetId = Convert.ToInt32(cmbMethod.SelectedValue);
                dv.RowFilter = $"ASSET_ID = {assetId}";
            }

            dgvAccount.DataSource = dv;
            UpdateTotalFromView(dv);
        }

        // ----------------------------
        // 총 합계 표시
        // ----------------------------
        private void UpdateTotalFromView(DataView dv)
        {
            decimal total = 0;

            foreach (DataRowView rv in dv)
            {
                if (rv["현재잔액"] != DBNull.Value)
                    total += Convert.ToDecimal(rv["현재잔액"]);
            }

            lblTotalAsset.Text = total.ToString("N0") + " 원";

            lblTotalAsset.ForeColor = total < 0 ? Color.Red : Color.Green;
        }

        // ----------------------------
        // 이벤트 연결
        // ----------------------------
        private void HookEvents()
        {
            dgvAccount.CellFormatting += DgvAccount_CellFormatting;

            dtpStartDate.ValueChanged += (s, e) =>
            {
                if (!isLoading) LoadAccounts();
            };

            dtpEndDate.ValueChanged += (s, e) =>
            {
                if (!isLoading) LoadAccounts();
            };

            cmbMethod.SelectedIndexChanged += (s, e) =>
            {
                if (!isLoading) ApplyFilters();
            };

            btnRefresh.Click += (s, e) => LoadAccounts();
        }

        // ----------------------------
        // 숫자 포맷 색상 적용
        // ----------------------------
        private void DgvAccount_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string header = dgvAccount.Columns[e.ColumnIndex].HeaderText;

            if (header == "기간수입" || header == "기간지출" || header == "현재잔액")
            {
                if (e.Value == null || e.Value == DBNull.Value) return;

                decimal value = Convert.ToDecimal(e.Value);
                e.Value = value.ToString("N0");

                if (header == "현재잔액")
                    e.CellStyle.ForeColor = value < 0 ? Color.Red : Color.Green;
            }
        }
    }
}
