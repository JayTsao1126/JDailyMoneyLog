using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JDailyMoneyLog
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //系統設定檔路徑
            string SysParmFilePath =  Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "SystemParamters.json");

            #region 載入系統設定

            if (File.Exists(SysParmFilePath))
            {
                string ss = File.ReadAllText(SysParmFilePath, Encoding.Unicode);
                GlobalVar.SYSParm = JsonConvert.DeserializeObject<SystemParamter>(ss);

                //載入 Money Log 資料
                GlobalVar.MyMoney.MoneyLogFilePath = GlobalVar.SYSParm.MoneyLogFilePath;
                GlobalVar.MyMoney.Load(GlobalVar.SYSParm.MoneyLogFilePath);
            }

            #endregion


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DML_MainF());


            #region 儲存系統設定

            string json_data = JsonConvert.SerializeObject(GlobalVar.SYSParm);
            File.WriteAllText(SysParmFilePath, GlobalVar.IndentJsonString(json_data), Encoding.Unicode);

            #endregion
            
        }
    }
}
