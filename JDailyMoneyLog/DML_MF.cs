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
using System.Windows.Forms.DataVisualization.Charting;

namespace JDailyMoneyLog
{
    public partial class DML_MainF : Form
    {
        UpdateMoneyInfoDelegate updateMoneyInfo = null;

        private Series _series = new Series();
        private bool is3D;

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
            CreateChart(GlobalVar.MyMoney.GetExpenseInfo());

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

        void CreateChart(Dictionary<string, int> dictionary)
        {
            KeyValuePair<string, int> pair = dictionary.First();    //取出第一筆資料
            dictionary.Remove(pair.Key);    //移除第一筆資料

            string[] xValues = dictionary.Keys.ToArray();
            int[] yValues = dictionary.Values.ToArray();

            //ChartAreas,Series,Legends 基本設定-------------------------------------------------
            Chart Chart1 = new Chart();
            Chart1.ChartAreas.Add("ChartArea1"); //圖表區域集合
            Chart1.Legends.Add("Legends1"); //圖例集合說明
            Chart1.Series.Add("Series1"); //數據序列集合

            //設定 Chart-------------------------------------------------------------------------
            Chart1.Width = 600;
            Chart1.Height = 600;
            Title title = new Title();
            title.Text = "支出統計圖";
            title.Alignment = ContentAlignment.MiddleCenter;
            title.Font = new System.Drawing.Font("Trebuchet MS", 14F, FontStyle.Bold);
            Chart1.Titles.Add(title);

            //設定 ChartArea1--------------------------------------------------------------------
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = is3D;
            Chart1.ChartAreas[0].AxisX.Interval = 1;

            //設定 Legends-------------------------------------------------------------------------                
            //Chart1.Legends["Legends1"].DockedToChartArea = "ChartArea1"; //顯示在圖表內
            Chart1.Legends["Legends1"].Docking = Docking.Bottom; //自訂顯示位置
            //背景色
            Chart1.Legends["Legends1"].BackColor = Color.FromArgb(235, 235, 235);
            //斜線背景
            Chart1.Legends["Legends1"].BackHatchStyle = ChartHatchStyle.DarkDownwardDiagonal;
            Chart1.Legends["Legends1"].BorderWidth = 1;
            Chart1.Legends["Legends1"].BorderColor = Color.FromArgb(200, 200, 200);

            //設定 Series1-----------------------------------------------------------------------
            Chart1.Series["Series1"].ChartType = SeriesChartType.Pie;
            //Chart1.Series["Series1"].ChartType = SeriesChartType.Doughnut;
            Chart1.Series["Series1"].Points.DataBindXY(xValues, yValues);
            Chart1.Series["Series1"].LegendText = "#VALX:    [ #PERCENT{P1} ]"; //X軸 + 百分比
            Chart1.Series["Series1"].Label = "#VALX\n#PERCENT{P1}"; //X軸 + 百分比
            //Chart1.Series["Series1"].LabelForeColor = Color.FromArgb(0, 90, 255); //字體顏色
            //字體設定
            Chart1.Series["Series1"].Font = new System.Drawing.Font("Trebuchet MS", 10, System.Drawing.FontStyle.Bold);
            Chart1.Series["Series1"].Points.FindMaxByValue().LabelForeColor = Color.Red;
            //Chart1.Series["Series1"].Points.FindMaxByValue().Color = Color.Red;
            //Chart1.Series["Series1"].Points.FindMaxByValue()["Exploded"] = "true";
            Chart1.Series["Series1"].BorderColor = Color.FromArgb(255, 101, 101, 101);

            //Chart1.Series["Series1"]["DoughnutRadius"] = "80"; // ChartType為Doughnut時，Set Doughnut hole size
            //Chart1.Series["Series1"]["PieLabelStyle"] = "Inside"; //數值顯示在圓餅內
            Chart1.Series["Series1"]["PieLabelStyle"] = "Outside"; //數值顯示在圓餅外
            //Chart1.Series["Series1"]["PieLabelStyle"] = "Disabled"; //不顯示數值
            //設定圓餅效果，除 Default 外其他效果3D不適用
            Chart1.Series["Series1"]["PieDrawingStyle"] = "Default";
            //Chart1.Series["Series1"]["PieDrawingStyle"] = "SoftEdge";
            //Chart1.Series["Series1"]["PieDrawingStyle"] = "Concave";

            //Random rnd = new Random();  //亂數產生區塊顏色
            //foreach (DataPoint point in Chart1.Series["Series1"].Points)
            //{
            //    //pie 顏色
            //    point.Color = Color.FromArgb(150, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)); 
            //}
            
            tabPage2.Controls.Add(Chart1);
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

        private void 開啟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select file";
            dialog.InitialDirectory = ".\\";
            dialog.Filter = "Json files|*.json";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                GlobalVar.SYSParm.MoneyLogFilePath = dialog.FileName;
                //載入 Money Log 資料
                GlobalVar.MyMoney.MoneyLogFilePath = GlobalVar.SYSParm.MoneyLogFilePath;
                GlobalVar.MyMoney.Load(GlobalVar.SYSParm.MoneyLogFilePath);
                //更新主畫面
                UpdateMoneyInfoCallback();
            }
        }
    }
}
