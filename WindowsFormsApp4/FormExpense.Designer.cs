namespace WindowsFormsApp4
{
    partial class FormExpense
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Panel panelInput;

        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblMemo;

        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtMemo;

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;

        private System.Windows.Forms.DataGridView dgvExpense;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.panelInput = new System.Windows.Forms.Panel();
            this.cmbSubCategory = new System.Windows.Forms.ComboBox();
            this.cmbPayType = new System.Windows.Forms.ComboBox();
            this.cmbMainCategory = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblMemo = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvExpense = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.panelInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpense)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.IndianRed;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.dtpDate);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 82);
            this.panelTop.TabIndex = 2;
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
            this.lblTitle.Text = "💸 지출 내역 관리";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy/MM/dd";
            this.dtpDate.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(1000, 22);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(172, 34);
            this.dtpDate.TabIndex = 4;
            // 
            // panelInput
            // 
            this.panelInput.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelInput.Controls.Add(this.cmbSubCategory);
            this.panelInput.Controls.Add(this.cmbPayType);
            this.panelInput.Controls.Add(this.cmbMainCategory);
            this.panelInput.Controls.Add(this.lblDate);
            this.panelInput.Controls.Add(this.lblCategory);
            this.panelInput.Controls.Add(this.lblAmount);
            this.panelInput.Controls.Add(this.lblMemo);
            this.panelInput.Controls.Add(this.txtAmount);
            this.panelInput.Controls.Add(this.txtMemo);
            this.panelInput.Controls.Add(this.btnAdd);
            this.panelInput.Controls.Add(this.btnUpdate);
            this.panelInput.Controls.Add(this.btnDelete);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInput.Location = new System.Drawing.Point(0, 82);
            this.panelInput.Margin = new System.Windows.Forms.Padding(4);
            this.panelInput.Name = "panelInput";
            this.panelInput.Padding = new System.Windows.Forms.Padding(30);
            this.panelInput.Size = new System.Drawing.Size(1200, 233);
            this.panelInput.TabIndex = 1;
            // 
            // cmbSubCategory
            // 
            this.cmbSubCategory.FormattingEnabled = true;
            this.cmbSubCategory.Location = new System.Drawing.Point(339, 95);
            this.cmbSubCategory.Name = "cmbSubCategory";
            this.cmbSubCategory.Size = new System.Drawing.Size(124, 26);
            this.cmbSubCategory.TabIndex = 13;
            this.cmbSubCategory.SelectedIndexChanged += new System.EventHandler(this.cmbSubCategory_SelectedIndexChanged);
            // 
            // cmbPayType
            // 
            this.cmbPayType.FormattingEnabled = true;
            this.cmbPayType.Location = new System.Drawing.Point(165, 35);
            this.cmbPayType.Name = "cmbPayType";
            this.cmbPayType.Size = new System.Drawing.Size(298, 26);
            this.cmbPayType.TabIndex = 12;
            this.cmbPayType.SelectedIndexChanged += new System.EventHandler(this.cmbPayType_SelectedIndexChanged);
            // 
            // cmbMainCategory
            // 
            this.cmbMainCategory.FormattingEnabled = true;
            this.cmbMainCategory.Location = new System.Drawing.Point(165, 95);
            this.cmbMainCategory.Name = "cmbMainCategory";
            this.cmbMainCategory.Size = new System.Drawing.Size(124, 26);
            this.cmbMainCategory.TabIndex = 11;
            this.cmbMainCategory.SelectedIndexChanged += new System.EventHandler(this.cmbMainCategory_SelectedIndexChanged);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblDate.Location = new System.Drawing.Point(30, 30);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(92, 28);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "결제수단";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblCategory.Location = new System.Drawing.Point(30, 90);
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
            this.lblAmount.Location = new System.Drawing.Point(30, 150);
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
            this.lblMemo.Location = new System.Drawing.Point(525, 30);
            this.lblMemo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(52, 28);
            this.lblMemo.TabIndex = 3;
            this.lblMemo.Text = "메모";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtAmount.Location = new System.Drawing.Point(165, 146);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(298, 34);
            this.txtAmount.TabIndex = 6;
            // 
            // txtMemo
            // 
            this.txtMemo.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtMemo.Location = new System.Drawing.Point(525, 75);
            this.txtMemo.Margin = new System.Windows.Forms.Padding(4);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(421, 118);
            this.txtMemo.TabIndex = 7;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Silver;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(1000, 24);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(135, 52);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "추가";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Silver;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.Location = new System.Drawing.Point(1000, 92);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(135, 52);
            this.btnUpdate.TabIndex = 9;
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
            this.btnDelete.Location = new System.Drawing.Point(1000, 159);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(135, 52);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dgvExpense
            // 
            this.dgvExpense.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExpense.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExpense.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExpense.ColumnHeadersHeight = 36;
            this.dgvExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExpense.EnableHeadersVisualStyles = false;
            this.dgvExpense.Location = new System.Drawing.Point(0, 315);
            this.dgvExpense.Margin = new System.Windows.Forms.Padding(4);
            this.dgvExpense.Name = "dgvExpense";
            this.dgvExpense.RowHeadersWidth = 62;
            this.dgvExpense.RowTemplate.Height = 28;
            this.dgvExpense.Size = new System.Drawing.Size(1200, 585);
            this.dgvExpense.TabIndex = 0;
            this.dgvExpense.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExpense_CellClick);
            // 
            // FormExpense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1200, 900);
            this.Controls.Add(this.dgvExpense);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormExpense";
            this.Text = "FormExpense";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpense)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.ComboBox cmbPayType;
        private System.Windows.Forms.ComboBox cmbMainCategory;
        private System.Windows.Forms.ComboBox cmbSubCategory;
    }
}
