using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JDailyMoneyLog
{
    public partial class DML_MainF : Form
    {
        UpdateMoneyInfoDelegate updateMoneyInfo = null;

        public DML_MainF()
        {
            InitializeComponent();

            updateMoneyInfo = new UpdateMoneyInfoDelegate(UpdateMoneyInfoCallback);
        }

        private void DML_MainF_Load(object sender, EventArgs e)
        {
            //更新主畫面
            UpdateMoneyInfoCallback();
        }

        private void DML_MainF_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        /// <summary>
        /// 更新主畫面的回呼涵式
        /// </summary>
        private void UpdateMoneyInfoCallback()
        {
            //更新消費紀錄清單
            var ds = GlobalVar.MyMoney.GetMoneyLogList();
            if(ds.Count > 0)
            {
                dgvMoneyLog.DataSource = null;
                dgvMoneyLog.DataSource = ds;
            }

            //更新資產統計表
            tvMoneyStatus.Nodes.Clear();

            //資產
            UpdateMoneyStatus(GlobalVar.MyMoney.GetAssetsInfo(), 0);
            //收入
            UpdateMoneyStatus(GlobalVar.MyMoney.GetIncomeInfo(), 1);
            //支出
            UpdateMoneyStatus(GlobalVar.MyMoney.GetExpenseInfo(), 2);

            tvMoneyStatus.ExpandAll();
        }

        private void UpdateMoneyStatus(Dictionary<string, int> dictionary, int imgidx)
        {
            KeyValuePair<string, int> pair = dictionary.First();    //取出第一筆資料
            var sAssets = $"{pair.Key} : {pair.Value:C0}";
            TreeNode tnAssets = new TreeNode(sAssets, imgidx, imgidx);
            dictionary.Remove(pair.Key);    //移除第一筆資料
            foreach (KeyValuePair<string, int> item in dictionary)
            {
                tnAssets.Nodes.Add(item.Key, $"{item.Key} : {item.Value:C0}", imgidx, imgidx);
                if (item.Value < 0)
                {
                    tnAssets.Nodes[item.Key].ForeColor = Color.Red;
                }
            }
            tvMoneyStatus.Nodes.Add(tnAssets);
        }

        private void ResetStorageMoney(object sender, EventArgs e)
        {
            using (JStorageInitialF dialog = new JStorageInitialF())
            {
                dialog.UpdateMoneyInfoCallback += new UpdateMoneyInfoDelegate(updateMoneyInfo);
                dialog.ShowDialog();
            }
        }

        private void AddMoneyLog(object sender, EventArgs e)
        {
            using (JMoneyLogInputF dialog = new JMoneyLogInputF())
            {
                //dialog.ShowDialog();
                dialog.DisplayThis(null, updateMoneyInfo);
            }
        }

        private void EditMoneyLog(object sender, EventArgs e)
        {
            int col = dgvMoneyLog.CurrentCell.ColumnIndex;
            int row = dgvMoneyLog.CurrentCell.RowIndex;


            int index = dgvMoneyLog.CurrentRow.Index;
            int i = (int)dgvMoneyLog.Rows[index].Cells["SerialNo"].Value;
            using (JMoneyLogInputF dialog = new JMoneyLogInputF())
            {
                dialog.DisplayThis(new JMoneyLog()
                {
                    SerialNo = (int)dgvMoneyLog.Rows[index].Cells["SerialNo"].Value,
                    Date = (DateTime)dgvMoneyLog.Rows[index].Cells["Date"].Value,
                    Type = (string)dgvMoneyLog.Rows[index].Cells["Type"].Value,
                    Item = (string)dgvMoneyLog.Rows[index].Cells["Item"].Value,
                    Amount = (int)dgvMoneyLog.Rows[index].Cells["Amount"].Value,
                    Source = (string)dgvMoneyLog.Rows[index].Cells["Source"].Value,
                    Target = (string)dgvMoneyLog.Rows[index].Cells["Target"].Value,
                    Remark = (string)dgvMoneyLog.Rows[index].Cells["Remark"].Value
                }, updateMoneyInfo);
            }
        }

        private void DeleteMoneyLog(object sender, EventArgs e)
        {
            int index = dgvMoneyLog.CurrentRow.Index;
            int sn = (int)dgvMoneyLog.Rows[index].Cells["SerialNo"].Value;
            GlobalVar.MyMoney.Delete(sn);
            UpdateMoneyInfoCallback();
        }

        private void SearchMoneyLog(object sender, EventArgs e)
        {
            //TEST - 查詢 2018/01/05 的所有交易紀錄 - test OK.
            //var ds = GlobalVar.MyMoney.GetMoneyLogList(2018,1,5);
            //if (ds.Count > 0)
            //{
            //    dgvMoneyLog.DataSource = null;
            //    dgvMoneyLog.DataSource = ds;
            //}
            //else
            //{
            //    MessageBox.Show("查無資料!");
            //}
        }
    }
}
