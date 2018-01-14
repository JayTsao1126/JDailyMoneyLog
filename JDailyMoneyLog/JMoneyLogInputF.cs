using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JDailyMoneyLog
{
    public partial class JMoneyLogInputF : Form
    {
        UpdateMoneyInfoDelegate updateMoneyInfo = null;
        private int SurrentSerialNo = 0;

        public JMoneyLogInputF()
        {
            InitializeComponent();

            SurrentSerialNo = 0;   //預設模式為新增

            cbType.Items.Clear();
            cbType.Items.AddRange(new string[] { "生活支出", "固定支出", "特別支出", "收入", "轉帳" });
            cbType.SelectedIndex = 0;

            //cbItem.Items.Clear();
            //cbItem.Items.AddRange(new string[] { "飲食", "寵物", "服裝", "用品", "交通", "娛樂", "醫療", "文書", "其他" });
            //cbItem.SelectedIndex = 0;

            cbSource.Items.Clear();
            cbSource.Items.AddRange(new string[] { "無", "現金", "信用卡", "薪資帳戶", "固支帳戶", "儲蓄帳戶" });
            cbSource.SelectedIndex = 0;

            cbTarget.Items.Clear();
            cbTarget.Items.AddRange(new string[] { "無", "現金", "信用卡", "薪資帳戶", "固支帳戶", "儲蓄帳戶" });
            cbTarget.SelectedIndex = 0;
        }

        public void DisplayThis(JMoneyLog log, UpdateMoneyInfoDelegate updateMoneyInfoCallback)
        {
            updateMoneyInfo = updateMoneyInfoCallback;
            if (log != null)
            {
                //Edit
                btnAddOK.Text = "OK";
                btnAbort.Text = "Cancel";

                SurrentSerialNo = log.SerialNo;  //設定模式為編輯

                dtpDate.Value = log.Date;
                switch (log.Type)
                {
                    case "生活支出":
                        cbType.SelectedIndex = 0;
                        break;
                    case "固定支出":
                        cbType.SelectedIndex = 1;
                        break;
                    case "特別支出":
                        cbType.SelectedIndex = 2;
                        break;
                    case "收入":
                        cbType.SelectedIndex = 3;
                        break;
                    case "轉帳":
                        cbType.SelectedIndex = 4;
                        break;
                }
                cbItem.Text = log.Item;
                tbValue.Text = log.Amount.ToString();
                cbSource.Text = log.Source;
                cbTarget.Text = log.Target;
                tbContents.Text = log.Remark;
            }
            else
            {
                //Add
                btnAddOK.Text = "Add";
                btnAbort.Text = "Close";
            }

            this.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SurrentSerialNo > 0)
            {
                //編輯
                GlobalVar.MyMoney.Update(new JMoneyLog() { SerialNo = SurrentSerialNo, Date = dtpDate.Value.Date,
                    Type = cbType.Text, Item = cbItem.Text, Amount = int.Parse(tbValue.Text), Source = cbSource.Text,
                    Target = cbTarget.Text, Remark = tbContents.Text });
                this.Close();
            }
            else
            { 
                //新增
                int iValue = int.Parse(tbValue.Text);
                if (iValue > 0)
                {
                    GlobalVar.MyMoney.Add(dtpDate.Value.Date, cbType.Text, cbItem.Text, iValue, cbSource.Text, cbTarget.Text, tbContents.Text);
                    ResetData();
                }
            }
            if(updateMoneyInfo != null)
            {
                updateMoneyInfo.Invoke();
            }
        }

        private void ResetData()
        {
            tbContents.Text = "";
            tbValue.Text = "0";
            cbType.SelectedIndex = 0;
            cbItem.SelectedIndex = 0;
            cbSource.SelectedIndex = 1;
            cbTarget.SelectedIndex = 0;
        }

        private void tbValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 使用 Char.IsDigit 方法 : 指示指定的 Unicode 字元是否分類為十進位數字。
            // e.KeyChar == (Char)48 ~ 57 -----> 0~9

            // Char.IsControl 方法 : 指示指定的 Unicode 字元是否分類為控制字元。
            // e.KeyChar == (Char)8 -----------> Backpace
            // e.KeyChar == (Char)13-----------> Enter

            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void JMoneyLogInputF_Load(object sender, EventArgs e)
        {
            if (SurrentSerialNo.Equals(0))
            {
                dtpDate.Value = DateTime.Now;
                ResetData();
            }
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.Text.Contains("生活支出"))
            {
                cbItem.Items.Clear();
                cbItem.Items.AddRange(new string[] { "飲食", "寵物", "服裝", "用品", "交通", "娛樂", "醫療", "文書", "其他"});
                cbItem.SelectedIndex = 0;
                cbSource.SelectedIndex = 1; //現金
                cbTarget.SelectedIndex = 0; //無
            }
            else if (cb.Text.Contains("固定支出"))
            {
                cbItem.Items.Clear();
                cbItem.Items.AddRange(new string[] { "水費", "電費", "天然氣", "電話", "手機", "停車位", "教育費", "稅金", "保險", "房貸", "車貸", "信貸", "管理費", "第四台", "其他" });
                cbItem.SelectedIndex = 0;
                cbSource.SelectedIndex = 4; //固定支出帳戶
                cbTarget.SelectedIndex = 0; //無
            }
            else if (cb.Text.Contains("特別支出"))
            {
                cbItem.Items.Clear();
                cbItem.Items.AddRange(new string[] { "其他" });
                cbItem.SelectedIndex = 0;
                cbSource.SelectedIndex = 5; //儲蓄帳戶
                cbTarget.SelectedIndex = 0; //無
            }
            else if (cb.Text.Contains("收入"))
            {
                cbItem.Items.Clear();
                cbItem.Items.AddRange(new string[] { "薪資", "獎金", "差旅費", "其他" });
                cbItem.SelectedIndex = 0;
                cbSource.SelectedIndex = 0; //無
                cbTarget.SelectedIndex = 3; //薪資帳戶
            }
            else if (cb.Text.Contains("轉帳"))
            {
                cbItem.Items.Clear();
                cbItem.Items.AddRange(new string[] { "提現", "繳信用卡", "轉固支", "轉儲蓄", "其他" });
                cbItem.SelectedIndex = 0;
                cbSource.SelectedIndex = 3; //薪資帳戶
                cbTarget.SelectedIndex = 1; //現金
            }
        }
    }

}
