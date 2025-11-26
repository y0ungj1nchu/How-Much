namespace WindowsFormsApp4
{
    partial class FormBudget
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.Panel panelInput;

        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblAmount;

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
            this.panelTop = new System.Windows.Forms.Panel();
            this.cmbMonth = new System.Windows.Forms.DateTimePicker();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelInput = new System.Windows.Forms.Panel();
            this.cmbMainCategory = new System.Windows.Forms.ComboBox();
            this.lblMemo = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.panelTop.SuspendLayout();
            this.panelInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panelTop.Controls.Add(this.cmbMonth);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(4);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 93);
            this.panelTop.TabIndex = 2;
            // 
            // cmbMonth
            // 
            this.cmbMonth.CustomFormat = "yyyy/MM";
            this.cmbMonth.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.cmbMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cmbMonth.Location = new System.Drawing.Point(1026, 22);
            this.cmbMonth.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(130, 34);
            this.cmbMonth.TabIndex = 6;
            this.cmbMonth.Value = new System.DateTime(2025, 11, 24, 0, 0, 0, 0);
            this.cmbMonth.ValueChanged += new System.EventHandler(this.cmbMonth_ValueChanged);
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
            this.lblTitle.Text = "💰 예산 관리";
            // 
            // panelInput
            // 
            this.panelInput.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelInput.Controls.Add(this.cmbMainCategory);
            this.panelInput.Controls.Add(this.lblMemo);
            this.panelInput.Controls.Add(this.txtMemo);
            this.panelInput.Controls.Add(this.button1);
            this.panelInput.Controls.Add(this.button2);
            this.panelInput.Controls.Add(this.button3);
            this.panelInput.Controls.Add(this.lblCategory);
            this.panelInput.Controls.Add(this.lblAmount);
            this.panelInput.Controls.Add(this.txtAmount);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInput.Location = new System.Drawing.Point(0, 93);
            this.panelInput.Margin = new System.Windows.Forms.Padding(4);
            this.panelInput.Name = "panelInput";
            this.panelInput.Padding = new System.Windows.Forms.Padding(30);
            this.panelInput.Size = new System.Drawing.Size(1200, 227);
            this.panelInput.TabIndex = 1;
            // 
            // cmbMainCategory
            // 
            this.cmbMainCategory.FormattingEnabled = true;
            this.cmbMainCategory.Location = new System.Drawing.Point(199, 55);
            this.cmbMainCategory.Name = "cmbMainCategory";
            this.cmbMainCategory.Size = new System.Drawing.Size(302, 26);
            this.cmbMainCategory.TabIndex = 12;
            // 
            // lblMemo
            // 
            this.lblMemo.AutoSize = true;
            this.lblMemo.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblMemo.Location = new System.Drawing.Point(534, 19);
            this.lblMemo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(52, 28);
            this.lblMemo.TabIndex = 15;
            this.lblMemo.Text = "메모";
            // 
            // txtMemo
            // 
            this.txtMemo.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtMemo.Location = new System.Drawing.Point(534, 64);
            this.txtMemo.Margin = new System.Windows.Forms.Padding(4);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(421, 118);
            this.txtMemo.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(991, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 52);
            this.button1.TabIndex = 12;
            this.button1.Text = "추가";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.button2.Location = new System.Drawing.Point(991, 83);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 52);
            this.button2.TabIndex = 13;
            this.button2.Text = "수정";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Silver;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(991, 150);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(135, 52);
            this.button3.TabIndex = 14;
            this.button3.Text = "삭제";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblCategory.Location = new System.Drawing.Point(64, 50);
            this.lblCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(92, 28);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "카테고리";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.lblAmount.Location = new System.Drawing.Point(64, 122);
            this.lblAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(99, 28);
            this.lblAmount.TabIndex = 1;
            this.lblAmount.Text = "예산 금액";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtAmount.Location = new System.Drawing.Point(199, 118);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(302, 34);
            this.txtAmount.TabIndex = 4;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 320);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1200, 580);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
            // 
            // FormBudget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1200, 900);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormBudget";
            this.Text = "FormBudget";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.DateTimePicker cmbMonth;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblMemo;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.ComboBox cmbMainCategory;
        private System.Windows.Forms.TextBox txtAmount;
    }
}
