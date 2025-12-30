using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace WindowsFormsApp4
{
    public partial class Searchcategory : Form
    {
        // 👉 Formtransaction에 전달될 이벤트
        public event Action<int> CategorySelected;

        private string connectionString =
            "User Id=BANK_MANAGER; Password=1234; Data Source=localhost:1521/XE;";

        public Searchcategory()
        {
            InitializeComponent();
            InitializeListView();
            LoadCategoryTree();
        }

        // =========================================================
        // 리스트뷰 기본 설정
        // =========================================================
        private void InitializeListView()
        {
            listView1.View = View.List;
            listView1.FullRowSelect = true;
            listView1.HideSelection = false;
            listView1.Font = new Font("맑은 고딕", 11);

            listView1.DoubleClick += listView1_DoubleClick;
        }

        // =========================================================
        // 대분류 + 소분류 트리 구조 로드
        // =========================================================
        private void LoadCategoryTree()
        {
            listView1.Items.Clear();

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                    SELECT
                        C.CATEGORY_ID,
                        C.NAME AS CATEGORY_NAME,
                        S.SUB_ID,
                        S.NAME AS SUB_NAME
                    FROM SUB_CATEGORIES S
                    JOIN CATEGORIES C
                      ON S.CATEGORY_ID = C.CATEGORY_ID
                    ORDER BY C.CATEGORY_ID, S.SUB_ID
                ";

                OracleCommand cmd = new OracleCommand(sql, conn);
                OracleDataReader rd = cmd.ExecuteReader();

                string lastCategory = "";

                while (rd.Read())
                {
                    string category = rd["CATEGORY_NAME"].ToString();
                    string sub = rd["SUB_NAME"].ToString();
                    int categoryId = Convert.ToInt32(rd["CATEGORY_ID"]);
                    int subId = Convert.ToInt32(rd["SUB_ID"]);

                    // =====================================================================
                    // ① 대분류 추가 (한 번만)
                    // =====================================================================
                    if (category != lastCategory)
                    {
                        ListViewItem header = new ListViewItem(category);
                        header.Font = new Font("맑은 고딕", 11, FontStyle.Bold);
                        header.ForeColor = Color.FromArgb(25, 70, 150);
                        header.Tag = $"C:{categoryId}";

                        listView1.Items.Add(header);

                        lastCategory = category;
                    }

                    // =====================================================================
                    // ② 소분류 추가 (들여쓰기 + '-' 표시)
                    // =====================================================================
                    ListViewItem child = new ListViewItem("    - " + sub);
                    child.Tag = $"S:{subId}";

                    listView1.Items.Add(child);
                }
            }
        }

        // =========================================================
        // 더블클릭 → 소분류 선택
        // =========================================================
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            ListViewItem item = listView1.SelectedItems[0];
            string tag = item.Tag.ToString();

            // 대분류 클릭 시 선택 불가
            if (tag.StartsWith("C:"))
            {
                MessageBox.Show("소분류를 선택하세요.");
                return;
            }

            // 소분류 ID 추출
            int subId = Convert.ToInt32(tag.Replace("S:", ""));

            // Formtransaction으로 전달
            CategorySelected?.Invoke(subId);
            this.Close();
        }
    }
}
