namespace WindowsFormsApp4
{
    partial class FormStats
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 =
                new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 =
                new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 =
                new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 =
                new System.Windows.Forms.DataVisualization.Charting.Legend();

            this.panelTop = new System.Windows.Forms.Panel();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();

            this.panelContent = new System.Windows.Forms.Panel();
            this.panelTopChart = new System.Windows.Forms.Panel();
            this.panelBottomChart = new System.Windows.Forms.Panel();

            this.chartIncomeExpense = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartBalance = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.panelTop.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelTopChart.SuspendLayout();
            this.panelBottomChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartIncomeExpense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBalance)).BeginInit();
            this.SuspendLayout();

            // =====================================================
            // panelTop
            // =====================================================
            this.panelTop.BackColor = System.Drawing.Color.Peru;
            this.panelTop.Controls.Add(this.dtpStart);
            this.panelTop.Controls.Add(this.dtpEnd);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 100;

            // =====================================================
            // lblTitle
            // =====================================================
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 28);
            this.lblTitle.Text = "📈 기간별 자산 흐름 분석";

            // =====================================================
            // dtpStart
            // =====================================================
            this.dtpStart.CustomFormat = "yyyy-MM-dd";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.dtpStart.Location = new System.Drawing.Point(820, 34);
            this.dtpStart.Width = 140;

            // =====================================================
            // dtpEnd
            // =====================================================
            this.dtpEnd.CustomFormat = "yyyy-MM-dd";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.dtpEnd.Location = new System.Drawing.Point(980, 34);
            this.dtpEnd.Width = 140;

            // =====================================================
            // btnSearch
            // =====================================================
            this.btnSearch.Text = "조회";
            this.btnSearch.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.BackColor = System.Drawing.Color.SaddleBrown;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(1140, 30);
            this.btnSearch.Size = new System.Drawing.Size(100, 40);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // =====================================================
            // panelContent
            // =====================================================
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Controls.Add(this.panelBottomChart);
            this.panelContent.Controls.Add(this.panelTopChart);

            // =====================================================
            // panelTopChart (수입/지출)
            // =====================================================
            this.panelTopChart.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopChart.Height = 380;
            this.panelTopChart.Padding = new System.Windows.Forms.Padding(20);
            this.panelTopChart.Controls.Add(this.chartIncomeExpense);

            // =====================================================
            // chartIncomeExpense
            // =====================================================
            chartArea1.AxisX.Interval = 1;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.LabelStyle.Format = "N0";
            chartArea1.Name = "IncomeExpenseArea";

            this.chartIncomeExpense.ChartAreas.Add(chartArea1);
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            this.chartIncomeExpense.Legends.Add(legend1);
            this.chartIncomeExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartIncomeExpense.BackColor = System.Drawing.Color.WhiteSmoke;

            // =====================================================
            // panelBottomChart (총 잔액)
            // =====================================================
            this.panelBottomChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBottomChart.Padding = new System.Windows.Forms.Padding(20);
            this.panelBottomChart.Controls.Add(this.chartBalance);

            // =====================================================
            // chartBalance
            // =====================================================
            chartArea2.AxisX.Interval = 1;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisY.LabelStyle.Format = "N0";
            chartArea2.Name = "BalanceArea";

            this.chartBalance.ChartAreas.Add(chartArea2);
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            this.chartBalance.Legends.Add(legend2);
            this.chartBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBalance.BackColor = System.Drawing.Color.WhiteSmoke;

            // =====================================================
            // FormStats
            // =====================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1350, 900);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormStats";
            this.Text = "FormStats";

            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelTopChart.ResumeLayout(false);
            this.panelBottomChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartIncomeExpense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartBalance)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnSearch;

        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelTopChart;
        private System.Windows.Forms.Panel panelBottomChart;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartIncomeExpense;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBalance;
    }
}
