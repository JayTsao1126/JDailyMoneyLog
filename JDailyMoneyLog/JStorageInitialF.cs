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
    public partial class JStorageInitialF : Form
    {
        public event UpdateMoneyInfoDelegate UpdateMoneyInfoCallback = null;

        public JStorageInitialF()
        {
            InitializeComponent();
        }

        private void JStorageInitialF_Shown(object sender, EventArgs e)
        {
            tbCash.Text = GlobalVar.MyMoney.GetStorageAmount("現金").ToString();
            tbSwipe.Text = GlobalVar.MyMoney.GetStorageAmount("信用卡").ToString();
            tbSalary.Text = GlobalVar.MyMoney.GetStorageAmount("薪資帳戶").ToString();
            tbFixedChaged.Text = GlobalVar.MyMoney.GetStorageAmount("固支帳戶").ToString();
            tbSavlings.Text = GlobalVar.MyMoney.GetStorageAmount("儲蓄帳戶").ToString();
        }

        private void JStorageInitialF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.DialogResult.Equals(DialogResult.OK))
            {
                GlobalVar.MyMoney.SetStorageAmount("現金", int.Parse(tbCash.Text));
                GlobalVar.MyMoney.SetStorageAmount("信用卡", int.Parse(tbSwipe.Text));
                GlobalVar.MyMoney.SetStorageAmount("薪資帳戶", int.Parse(tbSalary.Text));
                GlobalVar.MyMoney.SetStorageAmount("固支帳戶", int.Parse(tbFixedChaged.Text));
                GlobalVar.MyMoney.SetStorageAmount("儲蓄帳戶", int.Parse(tbSavlings.Text));
                UpdateMoneyInfoCallback();
            }
        }

        private void tbCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.KeyChar == (Char)8 -----------> Backpace
            // e.KeyChar == (Char)13-----------> Enter
            // e.KeyChar == (Char)45-----------> - (負號)
            // e.KeyChar == (Char)48 ~ 57 -----> 0~9

            //if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            if(e.KeyChar.Equals((char)8) || e.KeyChar.Equals((char)13) || e.KeyChar.Equals((char)45) ||
                ((e.KeyChar >=(char)48) && (e.KeyChar <= (char)57)))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void JStorageInitialF_Load(object sender, EventArgs e)
        {
            tbCash.ShortcutsEnabled = false;  // 不啟用快速鍵
            tbSwipe.ShortcutsEnabled = false;  // 不啟用快速鍵
            tbSalary.ShortcutsEnabled = false;  // 不啟用快速鍵
            tbFixedChaged.ShortcutsEnabled = false;  // 不啟用快速鍵
            tbSavlings.ShortcutsEnabled = false;  // 不啟用快速鍵
        }

    }
}
