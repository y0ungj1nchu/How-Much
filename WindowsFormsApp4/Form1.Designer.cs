namespace WindowsFormsApp4
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelNav = new System.Windows.Forms.Panel();
            this.btnExpense = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.btnBudget = new System.Windows.Forms.Button();
            this.btnIncome = new System.Windows.Forms.Button();
            this.lblLogo = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNav
            // 
            this.panelNav.BackColor = System.Drawing.Color.White;
            this.panelNav.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNav.Controls.Add(this.btnExpense);
            this.panelNav.Controls.Add(this.btnStats);
            this.panelNav.Controls.Add(this.btnBudget);
            this.panelNav.Controls.Add(this.btnIncome);
            this.panelNav.Controls.Add(this.lblLogo);
            this.panelNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelNav.Location = new System.Drawing.Point(0, 0);
            this.panelNav.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelNav.Name = "panelNav";
            this.panelNav.Size = new System.Drawing.Size(232, 720);
            this.panelNav.TabIndex = 0;
            // 
            // btnExpense
            // 
            this.btnExpense.FlatAppearance.BorderSize = 0;
            this.btnExpense.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnExpense.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExpense.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.btnExpense.Location = new System.Drawing.Point(-2, 111);
            this.btnExpense.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExpense.Name = "btnExpense";
            this.btnExpense.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnExpense.Size = new System.Drawing.Size(285, 66);
            this.btnExpense.TabIndex = 3;
            this.btnExpense.Text = "📤   지출내역";
            this.btnExpense.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExpense.UseVisualStyleBackColor = true;
            this.btnExpense.Click += new System.EventHandler(this.btnExpense_Click);
            // 
            // btnStats
            // 
            this.btnStats.FlatAppearance.BorderSize = 0;
            this.btnStats.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStats.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.btnStats.Location = new System.Drawing.Point(0, 336);
            this.btnStats.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStats.Name = "btnStats";
            this.btnStats.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnStats.Size = new System.Drawing.Size(285, 66);
            this.btnStats.TabIndex = 4;
            this.btnStats.Text = "📊   결산내역";
            this.btnStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStats.UseVisualStyleBackColor = true;
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);
            // 
            // btnBudget
            // 
            this.btnBudget.FlatAppearance.BorderSize = 0;
            this.btnBudget.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnBudget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBudget.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.btnBudget.Location = new System.Drawing.Point(0, 261);
            this.btnBudget.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBudget.Name = "btnBudget";
            this.btnBudget.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnBudget.Size = new System.Drawing.Size(285, 66);
            this.btnBudget.TabIndex = 2;
            this.btnBudget.Text = "💰   예산관리";
            this.btnBudget.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBudget.UseVisualStyleBackColor = true;
            this.btnBudget.Click += new System.EventHandler(this.btnBudget_Click);
            // 
            // btnIncome
            // 
            this.btnIncome.FlatAppearance.BorderSize = 0;
            this.btnIncome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnIncome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIncome.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.btnIncome.Location = new System.Drawing.Point(-2, 186);
            this.btnIncome.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnIncome.Name = "btnIncome";
            this.btnIncome.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.btnIncome.Size = new System.Drawing.Size(285, 66);
            this.btnIncome.TabIndex = 1;
            this.btnIncome.Text = "📥   수입내역";
            this.btnIncome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIncome.UseVisualStyleBackColor = true;
            this.btnIncome.Click += new System.EventHandler(this.btnIncome_Click);
            // 
            // lblLogo
            // 
            this.lblLogo.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblLogo.Location = new System.Drawing.Point(-33, 24);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(285, 48);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "📘 가계부";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(232, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1268, 720);
            this.panelMain.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1500, 720);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelNav);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "가계부 관리 시스템";
            this.panelNav.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelNav;
        private System.Windows.Forms.Button btnStats;
        private System.Windows.Forms.Button btnExpense;
        private System.Windows.Forms.Button btnBudget;
        private System.Windows.Forms.Button btnIncome;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Panel panelMain;
    }
}
