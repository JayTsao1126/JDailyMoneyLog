using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDailyMoneyLog
{
    /// <summary>
    /// 更新主畫面的委派
    /// </summary>
    /// <param name="str"></param>
    public delegate void UpdateMoneyInfoDelegate();

    /// <summary>
    /// 定義全域變數類別
    /// </summary>
    public class GlobalVar
    {

        public static JMoneySystem MyMoney = new JMoneySystem();
    }
}
