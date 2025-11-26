namespace WindowsFormsApp4
{
    partial class FormStats
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Label lblMonth;

        private System.Windows.Forms.Button btnLoad;

        private System.Windows.Forms.DataVisualization.Charting.Chart chartStats;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panelTop = new System.Windows.Forms.Panel();
            this.cmbMonth = new System.Windows.Forms.DateTimePicker();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.chartStats = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStats)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.Peru;
            this.panelTop.Controls.Add(this.cmbMonth);
            this.panelTop.Controls.Add(this.lblMonth);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.btnLoad);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 82);
            this.panelTop.TabIndex = 2;
            // 
            // cmbMonth
            // 
            this.cmbMonth.CustomFormat = "yyyy/MM";
            this.cmbMonth.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.cmbMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cmbMonth.Location = new System.Drawing.Point(858, 24);
            this.cmbMonth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(136, 34);
            this.cmbMonth.TabIndex = 7;
            this.cmbMonth.Value = new System.DateTime(2025, 11, 24, 0, 0, 0, 0);
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblMonth.Location = new System.Drawing.Point(771, 29);
            this.lblMonth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(79, 28);
            this.lblMonth.TabIndex = 0;
            this.lblMonth.Text = "월 선택";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 22);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(215, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 자료 통계";
            // 
            // btnLoad
            // 
            this.btnLoad.BackColor = System.Drawing.Color.Silver;
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnLoad.ForeColor = System.Drawing.Color.Black;
            this.btnLoad.Location = new System.Drawing.Point(1002, 18);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(180, 50);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "통계 불러오기";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // chartStats
            // 
            chartArea1.Name = "ChartArea1";
            this.chartStats.ChartAreas.Add(chartArea1);
            this.chartStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartStats.Location = new System.Drawing.Point(0, 82);
            this.chartStats.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chartStats.Name = "chartStats";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chartStats.Series.Add(series1);
            this.chartStats.Size = new System.Drawing.Size(1200, 818);
            this.chartStats.TabIndex = 0;
            // 
            // FormStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1200, 900);
            this.Controls.Add(this.chartStats);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormStats";
            this.Text = "FormStats";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStats)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.DateTimePicker cmbMonth;
    }
}
