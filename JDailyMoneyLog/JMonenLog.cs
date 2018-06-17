using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JDailyMoneyLog
{
    /*
     * Storage = "無", "現金", "信用卡", "薪資帳戶", "固支帳戶", "儲蓄帳戶"
     * Type = "生活支出", "固定支出", "特別支出", "收入", "轉帳"
     * 生活支出 = "飲食", "寵物", "服裝", "用品", "交通", "娛樂", "醫療", "文書", "其他"
     * 固定支出 = "水費", "電費", "天然氣", "電話", "手機", "停車位", "教育費", "稅金", "保險", "房貸", "車貸", "信貸", "管理費", "第四台", "其他"
     * 特別支出 = "其他"
     * 收入 = "薪資", "獎金", "差旅費", "其他"
     * 轉帳 = "提現", "繳信用卡", "轉固支", "轉儲蓄", "其他"
    */
    
    /// <summary>
    /// 帳目紀錄資料結構類別
    /// </summary>
    public class JMoneyLog
    {
        public int SerialNo { get; set; }  //序號(時間戳記) - 【唯一】
        public DateTime Date { get; set; }  //日期
        public string Type { get; set; }  //類型
        public string Item { get; set; }  //項目
        public int Amount { get; set; }     // $ (金額)
        public string Source { get; set; }   // - $ (支出、轉帳)
        public string Target { get; set; }   // + $ (收入、轉帳)
        public string Remark { get; set; }    //補充說明
    }

    /// <summary>
    /// 帳戶資料結構類別
    /// </summary>
    public class JStorageAmount
    {
        public string Storage { get; set; }  //帳戶 - 【唯一】
        public int Amount { get; set; }     // $ (餘額)
    }

    /// <summary>
    /// 帳目管理資料結構類別
    /// </summary>
    public class JMoneyLogs
    {
        public List<JStorageAmount> StorageAmountList { get; set; }     //各帳戶初始金額
        public List<JStorageAmount> StorageBalanceList { get; set; }     //各帳戶目前餘額
        public List<JMoneyLog> MoneyLogList { get; set; }       //帳目紀錄

        public JMoneyLogs()
        {
            StorageAmountList = new List<JStorageAmount>();
            StorageBalanceList = new List<JStorageAmount>();
            MoneyLogList = new List<JMoneyLog>();
        }
    }

    public class MoneyLogComparer : IComparer<JMoneyLog>
    {
        public int Compare(JMoneyLog x, JMoneyLog y)
        {
            if (x.Date > y.Date)
            {
                return -1;
            }
            else if (x.Date < y.Date)
            {
                return 1;
            }
            else
            {
                //日期相同
                if (x.SerialNo > y.SerialNo)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }
    }

    /// <summary>
    /// J專用記帳系統
    /// </summary>
        public class JMoneySystem
    {
        private JMoneyLogs JMoney = null;
        //private List<JStorageAmount> StorageBalanceList = null;     //各帳戶目前餘額(即時計算)
        //private string MoneyLogFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "DataSet", "MoneyLogs.json");

        public string MoneyLogFilePath { get; set; }

        public JMoneySystem()
        {
            JMoney = new JMoneyLogs();
            //StorageBalanceList = new List<JStorageAmount>();
            MoneyLogFilePath = string.Empty;
        }

        /// <summary>
        /// 載入 Money Log 資料檔
        /// </summary>
        /// <param name="file_path"></param>
        public void Load(string file_path)
        {
            if (File.Exists(file_path))
            {
                string ss = File.ReadAllText(file_path, Encoding.Unicode);
                JMoney = JsonConvert.DeserializeObject<JMoneyLogs>(ss);
                JMoney.MoneyLogList.Sort(new MoneyLogComparer()); //帳目紀錄排序
                RecalcStorageBalance();
            }
        }

        /// <summary>
        /// 重新計算帳戶餘額
        /// </summary>
        private void RecalcStorageBalance()
        {
            JMoney.StorageBalanceList = DeepCopy<List<JStorageAmount>>(JMoney.StorageAmountList);
            foreach (JMoneyLog moneyLog in JMoney.MoneyLogList)
            {
                CalcStorageBalance(moneyLog);
            }
            Save(MoneyLogFilePath);
        }

        private void Save(string file_path)
        {
            string json_data = JsonConvert.SerializeObject(JMoney);//存放序列後的文字
            string dir = Path.GetDirectoryName(file_path);
            Directory.CreateDirectory(dir);
            File.WriteAllText(file_path, GlobalVar.IndentJsonString(json_data), Encoding.Unicode);
        }

        public void SetStorageAmount(string storage, int amount)
        {
            int idx = JMoney.StorageAmountList.FindIndex(x => x.Storage.Equals(storage));
            if (idx < 0)
            {
                JMoney.StorageAmountList.Add(new JStorageAmount() { Storage = storage, Amount = amount });
            }
            else
            {
                JMoney.StorageAmountList[idx].Amount = amount;
            }
            RecalcStorageBalance();
        }

        public int GetStorageAmount(string storage)
        {
            int idx = JMoney.StorageAmountList.FindIndex(x => x.Storage.Equals(storage));
            if (idx < 0)
            {
                return 0;
            }
            return JMoney.StorageAmountList[idx].Amount;
        }

        private int GetStorageBalance(string storage)
        {
            int idx = JMoney.StorageBalanceList.FindIndex(x => x.Storage.Equals(storage));
            if (idx < 0)
            {
                return 0;
            }
            return JMoney.StorageBalanceList[idx].Amount;
        }

        /// <summary>
        /// 取得帳本資訊
        /// </summary>
        /// <param name="type">帳目類型,分為"資產"、"收入"、"支出"</param>
        /// <returns></returns>
        public Dictionary<string, int> GetMoneyInfo(string type)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            switch (type)
            {
                case "資產":
                    {
                        foreach (JStorageAmount q in JMoney.StorageBalanceList)
                        {
                            list.Add(q.Storage, q.Amount);
                        }
                    }
                    break;
                case "收入":
                case "支出":
                    {
                        //var query = from log in JMoney.MoneyLogList
                        //            where log.Type.Contains(type)
                        //            group log by log.Item;
                        //foreach (var q in query)
                        //{
                        //    list.Add(q.Key, (from x in JMoney.MoneyLogList where (x.Type.Contains(type) && x.Item.Equals(q.Key)) select x.Amount).Sum());
                        //}

                        var query = from log in JMoney.MoneyLogList
                                    where log.Type.Contains(type)
                                    group log by log.Type;
                        foreach (var q in query)
                        {
                            list.Add(q.Key, (from x in JMoney.MoneyLogList where (x.Type.Contains(type) && x.Type.Equals(q.Key)) select x.Amount).Sum());
                        }
                    }
                    break;
            }
            return list.OrderBy(data => data.Key).ToDictionary(keyvalue => keyvalue.Key, keyvalue => keyvalue.Value);
        }

        public List<JMoneyLog> GetMoneyLogList(int year=0, int month=0, int day=0)
        {
            if (year > 0)
            {
                if (month > 0)
                {
                    if (day > 0)
                    {
                        return JMoney.MoneyLogList.FindAll(x => (x.Date.Year.Equals(year) && x.Date.Month.Equals(month) && x.Date.Day.Equals(day)));
                    }
                    return JMoney.MoneyLogList.FindAll(x => (x.Date.Year.Equals(year) && x.Date.Month.Equals(month)));
                }
                return JMoney.MoneyLogList.FindAll(x => (x.Date.Year.Equals(year)));
            }
            return JMoney.MoneyLogList;
        }

        public void Add(DateTime dt, string type, string item, int amount, string src, string tar, string remark)
        {
            JMoneyLog moneyLog = new JMoneyLog()
            {
                SerialNo = Convert.ToInt32(DateTime.Now.Subtract(new DateTime(2018, 1, 1)).TotalSeconds),
                Date = dt,
                Item = item,
                Amount = amount,
                Type = type,
                Source = src,
                Target = tar,
                Remark = remark
            };
            JMoney.MoneyLogList.Add(moneyLog);
            JMoney.MoneyLogList.Sort(new MoneyLogComparer()); //帳目紀錄排序
            //RecalcStorageBalance();
            CalcStorageBalance(moneyLog);
            Save(MoneyLogFilePath);
        }

        public void Update(JMoneyLog moneyLog)
        {
            int idx = JMoney.MoneyLogList.FindIndex(x => x.SerialNo.Equals(moneyLog.SerialNo));
            if (idx >= 0)
            {
                CalcStorageBalance(JMoney.MoneyLogList[idx], true);     //先將舊的紀錄還原

                //更新紀錄內容
                JMoney.MoneyLogList[idx].Date = moneyLog.Date;
                JMoney.MoneyLogList[idx].Type = moneyLog.Type;
                JMoney.MoneyLogList[idx].Item = moneyLog.Item;
                JMoney.MoneyLogList[idx].Amount = moneyLog.Amount;
                JMoney.MoneyLogList[idx].Source = moneyLog.Source;
                JMoney.MoneyLogList[idx].Target = moneyLog.Target;
                JMoney.MoneyLogList[idx].Remark = moneyLog.Remark;

                CalcStorageBalance(JMoney.MoneyLogList[idx]);     //計算新紀錄的結果
                JMoney.MoneyLogList.Sort(new MoneyLogComparer()); //帳目紀錄排序
                Save(MoneyLogFilePath);
            }
            //RecalcStorageBalance();
        }

        public void Delete(int sn)
        {
            int idx = JMoney.MoneyLogList.FindIndex(x => x.SerialNo.Equals(sn));
            if (idx >= 0)
            {
                CalcStorageBalance(JMoney.MoneyLogList[idx], true);     //將舊的紀錄還原

                //將記錄移除
                JMoney.MoneyLogList.RemoveAt(idx);

                JMoney.MoneyLogList.Sort(new MoneyLogComparer()); //帳目紀錄排序
                Save(MoneyLogFilePath);
            }
            //RecalcStorageBalance();
        }

        /// <summary>
        /// 計算帳戶餘額
        /// </summary>
        /// <param name="moneyLog">消費紀錄</param>
        /// <param name="revert">是否反向(還原消費內容)</param>
        private void CalcStorageBalance(JMoneyLog moneyLog, bool revert=false)
        {
            int amount = moneyLog.Amount;
            if (revert)
            {
                amount = -moneyLog.Amount;
            }

            if (moneyLog.Type.Contains("支出"))
            {
                //Source 帳戶金額要減少
                int idx = JMoney.StorageBalanceList.FindIndex(x => x.Storage.Equals(moneyLog.Source));
                if (idx < 0)
                {
                    //帳戶餘額項目中無此帳戶資料，加入此帳戶餘額
                    JMoney.StorageBalanceList.Add(new JStorageAmount() { Storage = moneyLog.Source, Amount = -amount });
                }
                else
                {
                    //扣除金額
                    JMoney.StorageBalanceList[idx].Amount -= amount;
                }
            }
            else if (moneyLog.Type.Contains("收入"))
            {
                //Target 帳戶金額要增加
                int idx = JMoney.StorageBalanceList.FindIndex(x => x.Storage.Equals(moneyLog.Target));
                if (idx < 0)
                {
                    //帳戶餘額項目中無此帳戶資料，加入此帳戶餘額
                    JMoney.StorageBalanceList.Add(new JStorageAmount() { Storage = moneyLog.Target, Amount = amount });
                }
                else
                {
                    //扣除金額
                    JMoney.StorageBalanceList[idx].Amount += amount;
                }
            }
            else if (moneyLog.Type.Contains("轉帳"))
            {
                //Source 帳戶金額要減少
                int idx = JMoney.StorageBalanceList.FindIndex(x => x.Storage.Equals(moneyLog.Source));
                if (idx < 0)
                {
                    //帳戶餘額項目中無此帳戶資料，加入此帳戶餘額
                    JMoney.StorageBalanceList.Add(new JStorageAmount() { Storage = moneyLog.Source, Amount = -amount });
                }
                else
                {
                    //扣除金額
                    JMoney.StorageBalanceList[idx].Amount -= amount;
                }
                //Target 帳戶金額要增加
                idx = JMoney.StorageBalanceList.FindIndex(x => x.Storage.Equals(moneyLog.Target));
                if (idx < 0)
                {
                    //帳戶餘額項目中無此帳戶資料，加入此帳戶餘額
                    JMoney.StorageBalanceList.Add(new JStorageAmount() { Storage = moneyLog.Target, Amount = amount });
                }
                else
                {
                    //扣除金額
                    JMoney.StorageBalanceList[idx].Amount += amount;
                }
            }
            else
            {
                //...
            }
        }

        /// <summary>
        /// 深複製
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(T RealObject)
        {
            using (Stream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, RealObject);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}
