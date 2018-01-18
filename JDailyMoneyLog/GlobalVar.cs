using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

    public class SystemParamter
    {
        public string MoneyLogFilePath { get; set; }
    }

    /// <summary>
    /// 定義全域變數類別
    /// </summary>
    public class GlobalVar
    {
        public static SystemParamter SYSParm = new SystemParamter();
        public static JMoneySystem MyMoney = new JMoneySystem();

        /// <summary>
        /// 格式化json字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string IndentJsonString(string str)
        {
            string sRet = str;
            JsonSerializer serializer = new JsonSerializer();
            using (TextReader tr = new StringReader(str))
            {
                using (JsonTextReader jtr = new JsonTextReader(tr))
                {
                    object obj = serializer.Deserialize(jtr);
                    if (obj != null)
                    {
                        using (StringWriter textWriter = new StringWriter())
                        {
                            using (JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                            {
                                Formatting = Formatting.Indented,
                                Indentation = 4,
                                IndentChar = ' '
                            })
                            {
                                serializer.Serialize(jsonWriter, obj);
                                sRet = textWriter.ToString();
                            }
                        }
                    }
                }
            }
            return sRet;
        }
    }
}
