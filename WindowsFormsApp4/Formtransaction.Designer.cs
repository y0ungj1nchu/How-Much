namespace WindowsFormsApp4
{
    partial class Formtransaction
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Panel panelInput;

        private System.Windows.Forms.DateTimePicker dtpEndDate;

        private System.Windows.Forms.DataGridView dgvTransaction;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.SearchDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.panelInput = new System.Windows.Forms.Panel();
            this.lblExpenseAmount = new System.Windows.Forms.Label();
            this.lblIncomeAmount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.IncomeExpensecombobox = new System.Windows.Forms.ComboBox();
            this.lblExpeseIncome = new System.Windows.Forms.Label();
            this.cmbTrasactionType = new System.Windows.Forms.ComboBox();
            this.cmbSubCategory = new System.Windows.Forms.ComboBox();
            this.cmbMainCategory = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.transaction = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblMemo = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.dgvTransaction = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.보기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.지출ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.수입ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.전체ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.카테고리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.거래수단ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.수입ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.지출ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTop.SuspendLayout();
            this.panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransaction)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelTop.Controls.Add(this.SearchDate);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.dtpStartDate);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.dtpEndDate);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 33);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1350, 82);
            this.panelTop.TabIndex = 2;
            // 
            // SearchDate
            // 
            this.SearchDate.AutoSize = true;
            this.SearchDate.Font = new System.Drawing.Font("굴림", 9F);
            this.SearchDate.Location = new System.Drawing.Point(794, 32);
            this.SearchDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SearchDate.Name = "SearchDate";
            this.SearchDate.Size = new System.Drawing.Size(80, 18);
            this.SearchDate.TabIndex = 7;
            this.SearchDate.Text = "검색일자";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 13F);
            this.label1.Location = new System.Drawing.Point(1082, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 38);
            this.label1.TabIndex = 6;
            this.label1.Text = "~";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "yyyy/MM/dd";
            this.dtpStartDate.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(888, 22);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(170, 34);
            this.dtpStartDate.TabIndex = 5;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 22);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(290, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📘 거래 내역 관리";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "yyyy/MM/dd";
            this.dtpEndDate.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(1134, 22);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(170, 34);
            this.dtpEndDate.TabIndex = 4;
            // 
            // panelInput
            // 
            this.panelInput.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelInput.Controls.Add(this.lblExpenseAmount);
            this.panelInput.Controls.Add(this.lblIncomeAmount);
            this.panelInput.Controls.Add(this.label5);
            this.panelInput.Controls.Add(this.label4);
            this.panelInput.Controls.Add(this.IncomeExpensecombobox);
            this.panelInput.Controls.Add(this.lblExpeseIncome);
            this.panelInput.Controls.Add(this.cmbTrasactionType);
            this.panelInput.Controls.Add(this.cmbSubCategory);
            this.panelInput.Controls.Add(this.cmbMainCategory);
            this.panelInput.Controls.Add(this.btnAdd);
            this.panelInput.Controls.Add(this.btnUpdate);
            this.panelInput.Controls.Add(this.btnDelete);
            this.panelInput.Controls.Add(this.transaction);
            this.panelInput.Controls.Add(this.lblCategory);
            this.panelInput.Controls.Add(this.lblAmount);
            this.panelInput.Controls.Add(this.lblMemo);
            this.panelInput.Controls.Add(this.txtAmount);
            this.panelInput.Controls.Add(this.txtMemo);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInput.Location = new System.Drawing.Point(0, 115);
            this.panelInput.Margin = new System.Windows.Forms.Padding(4);
            this.panelInput.Name = "panelInput";
            this.panelInput.Padding = new System.Windows.Forms.Padding(30);
            this.panelInput.Size = new System.Drawing.Size(1350, 250);
            this.panelInput.TabIndex = 1;
            // 
            // lblExpenseAmount
            // 
            this.lblExpenseAmount.AutoSize = true;
            this.lblExpenseAmount.Location = new System.Drawing.Point(643, 197);
            this.lblExpenseAmount.Name = "lblExpenseAmount";
            this.lblExpenseAmount.Size = new System.Drawing.Size(18, 18);
            this.lblExpenseAmount.TabIndex = 0;
            this.lblExpenseAmount.Text = "0";
            // 
            // lblIncomeAmount
            // 
            this.lblIncomeAmount.AutoSize = true;
            this.lblIncomeAmount.Location = new System.Drawing.Point(845, 195);
            this.lblIncomeAmount.Name = "lblIncomeAmount";
            this.lblIncomeAmount.Size = new System.Drawing.Size(18, 18);
            this.lblIncomeAmount.TabIndex = 24;
            this.lblIncomeAmount.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(769, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "총 수입";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(569, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 18);
            this.label4.TabIndex = 21;
            this.label4.Text = "총 지출";
            // 
            // IncomeExpensecombobox
            // 
            this.IncomeExpensecombobox.FormattingEnabled = true;
            this.IncomeExpensecombobox.Location = new System.Drawing.Point(284, 33);
            this.IncomeExpensecombobox.Name = "IncomeExpensecombobox";
            this.IncomeExpensecombobox.Size = new System.Drawing.Size(218, 26);
            this.IncomeExpensecombobox.TabIndex = 20;
            // 
            // lblExpeseIncome
            // 
            this.lblExpeseIncome.AutoSize = true;
            this.lblExpeseIncome.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblExpeseIncome.Location = new System.Drawing.Point(146, 32);
            this.lblExpeseIncome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExpeseIncome.Name = "lblExpeseIncome";
            this.lblExpeseIncome.Size = new System.Drawing.Size(100, 28);
            this.lblExpeseIncome.TabIndex = 19;
            this.lblExpeseIncome.Text = "수입/지출";
            // 
            // cmbTrasactionType
            // 
            this.cmbTrasactionType.FormattingEnabled = true;
            this.cmbTrasactionType.Location = new System.Drawing.Point(281, 81);
            this.cmbTrasactionType.Name = "cmbTrasactionType";
            this.cmbTrasactionType.Size = new System.Drawing.Size(218, 26);
            this.cmbTrasactionType.TabIndex = 18;
            // 
            // cmbSubCategory
            // 
            this.cmbSubCategory.FormattingEnabled = true;
            this.cmbSubCategory.Location = new System.Drawing.Point(408, 137);
            this.cmbSubCategory.Name = "cmbSubCategory";
            this.cmbSubCategory.Size = new System.Drawing.Size(91, 26);
            this.cmbSubCategory.TabIndex = 17;
            // 
            // cmbMainCategory
            // 
            this.cmbMainCategory.FormattingEnabled = true;
            this.cmbMainCategory.Location = new System.Drawing.Point(281, 137);
            this.cmbMainCategory.Name = "cmbMainCategory";
            this.cmbMainCategory.Size = new System.Drawing.Size(97, 26);
            this.cmbMainCategory.TabIndex = 14;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Silver;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(1061, 26);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(135, 52);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "추가";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Silver;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.Location = new System.Drawing.Point(1061, 93);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(135, 52);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "수정";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Silver;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(1061, 160);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(135, 52);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // transaction
            // 
            this.transaction.AutoSize = true;
            this.transaction.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.transaction.Location = new System.Drawing.Point(146, 79);
            this.transaction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.transaction.Name = "transaction";
            this.transaction.Size = new System.Drawing.Size(92, 28);
            this.transaction.TabIndex = 0;
            this.transaction.Text = "거래수단";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblCategory.Location = new System.Drawing.Point(146, 133);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(92, 28);
            this.lblCategory.TabIndex = 1;
            this.lblCategory.Text = "카테고리";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblAmount.Location = new System.Drawing.Point(146, 189);
            this.lblAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(52, 28);
            this.lblAmount.TabIndex = 2;
            this.lblAmount.Text = "금액";
            // 
            // lblMemo
            // 
            this.lblMemo.AutoSize = true;
            this.lblMemo.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblMemo.Location = new System.Drawing.Point(567, 26);
            this.lblMemo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(52, 28);
            this.lblMemo.TabIndex = 3;
            this.lblMemo.Text = "메모";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtAmount.Location = new System.Drawing.Point(281, 185);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(218, 34);
            this.txtAmount.TabIndex = 6;
            // 
            // txtMemo
            // 
            this.txtMemo.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtMemo.Location = new System.Drawing.Point(571, 58);
            this.txtMemo.Margin = new System.Windows.Forms.Padding(4);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(397, 118);
            this.txtMemo.TabIndex = 7;
            // 
            // dgvTransaction
            // 
            this.dgvTransaction.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransaction.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTransaction.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTransaction.ColumnHeadersHeight = 36;
            this.dgvTransaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransaction.EnableHeadersVisualStyles = false;
            this.dgvTransaction.Location = new System.Drawing.Point(0, 365);
            this.dgvTransaction.Margin = new System.Windows.Forms.Padding(4);
            this.dgvTransaction.Name = "dgvTransaction";
            this.dgvTransaction.RowHeadersWidth = 62;
            this.dgvTransaction.RowTemplate.Height = 28;
            this.dgvTransaction.Size = new System.Drawing.Size(1350, 535);
            this.dgvTransaction.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.보기ToolStripMenuItem,
            this.카테고리ToolStripMenuItem,
            this.거래수단ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1350, 33);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 보기ToolStripMenuItem
            // 
            this.보기ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.지출ToolStripMenuItem1,
            this.수입ToolStripMenuItem,
            this.전체ToolStripMenuItem});
            this.보기ToolStripMenuItem.Name = "보기ToolStripMenuItem";
            this.보기ToolStripMenuItem.Size = new System.Drawing.Size(64, 29);
            this.보기ToolStripMenuItem.Text = "보기";
            // 
            // 지출ToolStripMenuItem1
            // 
            this.지출ToolStripMenuItem1.Name = "지출ToolStripMenuItem1";
            this.지출ToolStripMenuItem1.Size = new System.Drawing.Size(270, 34);
            this.지출ToolStripMenuItem1.Text = "지출";
            this.지출ToolStripMenuItem1.Click += new System.EventHandler(this.지출ToolStripMenuItem1_Click);
            // 
            // 수입ToolStripMenuItem
            // 
            this.수입ToolStripMenuItem.Name = "수입ToolStripMenuItem";
            this.수입ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.수입ToolStripMenuItem.Text = "수입";
            this.수입ToolStripMenuItem.Click += new System.EventHandler(this.수입ToolStripMenuItem_Click);
            // 
            // 전체ToolStripMenuItem
            // 
            this.전체ToolStripMenuItem.Name = "전체ToolStripMenuItem";
            this.전체ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.전체ToolStripMenuItem.Text = "전체";
            this.전체ToolStripMenuItem.Click += new System.EventHandler(this.전체ToolStripMenuItem_Click);
            // 
            // 카테고리ToolStripMenuItem
            // 
            this.카테고리ToolStripMenuItem.Name = "카테고리ToolStripMenuItem";
            this.카테고리ToolStripMenuItem.Size = new System.Drawing.Size(100, 29);
            this.카테고리ToolStripMenuItem.Text = "카테고리";
            this.카테고리ToolStripMenuItem.Click += new System.EventHandler(this.카테고리검색ToolStripMenuItem_Click);
            // 
            // 거래수단ToolStripMenuItem
            // 
            this.거래수단ToolStripMenuItem.Name = "거래수단ToolStripMenuItem";
            this.거래수단ToolStripMenuItem.Size = new System.Drawing.Size(100, 29);
            this.거래수단ToolStripMenuItem.Text = "거래수단";
            this.거래수단ToolStripMenuItem.Click += new System.EventHandler(this.거래수단ToolStripMenuItem_Click);
            // 
            // 수입ToolStripMenuItem1
            // 
            this.수입ToolStripMenuItem1.Name = "수입ToolStripMenuItem1";
            this.수입ToolStripMenuItem1.Size = new System.Drawing.Size(270, 34);
            // 
            // 지출ToolStripMenuItem
            // 
            this.지출ToolStripMenuItem.Name = "지출ToolStripMenuItem";
            this.지출ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.지출ToolStripMenuItem.Text = "지출";
            // 
            // Formtransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1350, 900);
            this.Controls.Add(this.dgvTransaction);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Formtransaction";
            this.Text = "FormIncome";
            this.Load += new System.EventHandler(this.Formtransaction_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransaction)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label SearchDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblExpenseAmount;
        private System.Windows.Forms.Label lblIncomeAmount;
        private System.Windows.Forms.ComboBox IncomeExpensecombobox;
        private System.Windows.Forms.Label lblExpeseIncome;
        private System.Windows.Forms.ComboBox cmbTrasactionType;
        private System.Windows.Forms.ComboBox cmbSubCategory;
        private System.Windows.Forms.ComboBox cmbMainCategory;
        private System.Windows.Forms.Label transaction;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblMemo;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 보기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 카테고리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 거래수단ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 수입ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 지출ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 지출ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 수입ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 전체ToolStripMenuItem;
    }
}