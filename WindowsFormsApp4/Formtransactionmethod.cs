using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace WindowsFormsApp4
{
    public partial class Formtransactionmethod : Form
    {
        public event Action<int> MethodSelected;

        private string connectionString =
            "User Id=BANK_MANAGER; Password=1234; Data Source=localhost:1521/XE;";

        public Formtransactionmethod()
        {
            InitializeComponent();
            InitializeListView();
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

            // 더블클릭 이벤트
            listView1.DoubleClick += listView1_DoubleClick;
        }

        // =========================================================
        // 거래수단 로드 (PAY_METHODS)
        // =========================================================
        private void LoadPayMethods()
        {
            listView1.Items.Clear();

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                string sql = @"
                    SELECT METHOD_ID, NAME
                    FROM PAY_METHODS
                    ORDER BY METHOD_ID
                ";

                using (OracleCommand cmd = new OracleCommand(sql, conn))
                using (OracleDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        int methodId = Convert.ToInt32(rd["METHOD_ID"]);
                        string name = rd["NAME"].ToString();

                        ListViewItem item = new ListViewItem(name);
                        item.Tag = methodId;   // METHOD_ID 저장

                        listView1.Items.Add(item);
                    }
                }
            }
        }

        // 폼 로드시 자동 로드
        private void Formtransactionmethod_Load(object sender, EventArgs e)
        {
            LoadPayMethods();
        }

        // =========================================================
        // 리스트뷰 더블클릭 → METHOD_ID 반환
        // =========================================================
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            ListViewItem selected = listView1.SelectedItems[0];
            int methodId = Convert.ToInt32(selected.Tag);

            MethodSelected?.Invoke(methodId); // Formtransaction 으로 전달
            this.Close();
        }
    }
}
