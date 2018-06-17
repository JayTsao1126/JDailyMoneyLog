namespace JDailyMoneyLog
{
    partial class JMoneyLogInputF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbContents = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbSource = new System.Windows.Forms.ComboBox();
            this.cbTarget = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbItem = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAddOK = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 16);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "日期";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy\\MM\\dd";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(72, 12);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 33);
            this.dtpDate.TabIndex = 4;
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "生活支出",
            "固定支出",
            "特別支出",
            "收入",
            "轉帳"});
            this.cbType.Location = new System.Drawing.Point(72, 51);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(200, 32);
            this.cbType.TabIndex = 5;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "分類";
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(72, 127);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(200, 33);
            this.tbValue.TabIndex = 7;
            this.tbValue.Text = "0";
            this.tbValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbValue_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "金額";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 244);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "備註";
            // 
            // tbContents
            // 
            this.tbContents.Location = new System.Drawing.Point(72, 242);
            this.tbContents.Name = "tbContents";
            this.tbContents.Size = new System.Drawing.Size(500, 33);
            this.tbContents.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 168);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "扣帳";
            // 
            // cbSource
            // 
            this.cbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSource.FormattingEnabled = true;
            this.cbSource.Items.AddRange(new object[] {
            "無",
            "現金",
            "信用卡",
            "薪資帳戶",
            "固支帳戶",
            "儲蓄帳戶"});
            this.cbSource.Location = new System.Drawing.Point(72, 166);
            this.cbSource.Name = "cbSource";
            this.cbSource.Size = new System.Drawing.Size(200, 32);
            this.cbSource.TabIndex = 12;
            // 
            // cbTarget
            // 
            this.cbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTarget.FormattingEnabled = true;
            this.cbTarget.Items.AddRange(new object[] {
            "無",
            "現金",
            "信用卡",
            "薪資帳戶",
            "固支帳戶",
            "儲蓄帳戶"});
            this.cbTarget.Location = new System.Drawing.Point(72, 204);
            this.cbTarget.Name = "cbTarget";
            this.cbTarget.Size = new System.Drawing.Size(200, 32);
            this.cbTarget.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 206);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 24);
            this.label6.TabIndex = 14;
            this.label6.Text = "入帳";
            // 
            // cbItem
            // 
            this.cbItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItem.FormattingEnabled = true;
            this.cbItem.Location = new System.Drawing.Point(72, 89);
            this.cbItem.Name = "cbItem";
            this.cbItem.Size = new System.Drawing.Size(200, 32);
            this.cbItem.TabIndex = 15;
            this.cbItem.SelectedIndexChanged += new System.EventHandler(this.cbItem_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 92);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 24);
            this.label7.TabIndex = 16;
            this.label7.Text = "項目";
            // 
            // btnAddOK
            // 
            this.btnAddOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddOK.Font = new System.Drawing.Font("微軟正黑體", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAddOK.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnAddOK.Location = new System.Drawing.Point(288, 12);
            this.btnAddOK.Name = "btnAddOK";
            this.btnAddOK.Size = new System.Drawing.Size(284, 224);
            this.btnAddOK.TabIndex = 17;
            this.btnAddOK.Text = "Add";
            this.btnAddOK.UseVisualStyleBackColor = true;
            this.btnAddOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnAbort.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAbort.Font = new System.Drawing.Font("微軟正黑體", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAbort.ForeColor = System.Drawing.Color.Red;
            this.btnAbort.Location = new System.Drawing.Point(19, 294);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(553, 105);
            this.btnAbort.TabIndex = 18;
            this.btnAbort.Text = "Close";
            this.btnAbort.UseVisualStyleBackColor = true;
            // 
            // JMoneyLogInputF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 411);
            this.Controls.Add(this.btnAbort);
            this.Controls.Add(this.btnAddOK);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbItem);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbTarget);
            this.Controls.Add(this.cbSource);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbContents);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "JMoneyLogInputF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新增項目";
            this.Load += new System.EventHandler(this.JMoneyLogInputF_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbContents;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbSource;
        private System.Windows.Forms.ComboBox cbTarget;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAddOK;
        private System.Windows.Forms.Button btnAbort;
    }
}