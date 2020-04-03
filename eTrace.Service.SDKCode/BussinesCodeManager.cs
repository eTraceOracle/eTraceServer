using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKCode
{
    public class BussinesCodeManager
    {
        private static BussinesCodeManager instance = new BussinesCodeManager();
        public static BussinesCodeManager Instance
        {
            get { return instance; }
        }

        private Dictionary<emLanguageType, Dictionary<emBussinesCodeType, string>> dic = new Dictionary<emLanguageType, Dictionary<emBussinesCodeType, string>>();

        private BussinesCodeManager()
        {
            InitDatas();
        }

        private void InitDatas()
        {
            dic[emLanguageType.CHN] = new Dictionary<emBussinesCodeType, string>();
            dic[emLanguageType.EN] = new Dictionary<emBussinesCodeType, string>();
            dic[emLanguageType.PHI] = new Dictionary<emBussinesCodeType, string>();

            //后面考虑读取数据库

            #region 100000~200000  V1
            #region 100000~110000 V1 Report
            #region 100000~110100 V1 Report WipData

            dic[emLanguageType.CHN][emBussinesCodeType.ReportIntsnEmpty] = "Intsn为空！";
            dic[emLanguageType.EN][emBussinesCodeType.ReportIntsnEmpty] = "Intsn is empty";
            dic[emLanguageType.PHI][emBussinesCodeType.ReportIntsnEmpty] = "";

            #endregion
            #endregion
            #endregion

            #region 200001~400000  V2

            #endregion
        }

        public string GetMessage(emLanguageType lanType, emBussinesCodeType bCode)
        {
            if (dic.ContainsKey(lanType))
            {
                if (dic[lanType].ContainsKey(bCode))
                {
                    return dic[lanType][bCode];
                }
            }
            return string.Empty;
        }
    }
}
