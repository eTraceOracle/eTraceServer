using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model
{
    public class BussinesCodeManager
    {
        private static BussinesCodeManager instance = new BussinesCodeManager();
        public static BussinesCodeManager Instance
        {
            get { return instance; }
        }

        private Dictionary<EmLanguageType, Dictionary<EmBussinesCodeType, string>> dic = new Dictionary<EmLanguageType, Dictionary<EmBussinesCodeType, string>>();

        private BussinesCodeManager()
        {
            InitDatas();
        }

        private void InitDatas()
        {
            dic[EmLanguageType.CHN] = new Dictionary<EmBussinesCodeType, string>();
            dic[EmLanguageType.EN] = new Dictionary<EmBussinesCodeType, string>(); 

            #region 1~100  服务信息

            dic[EmLanguageType.CHN][EmBussinesCodeType.ServerNotFound] = "服务不存在！";
            dic[EmLanguageType.EN][EmBussinesCodeType.ServerNotFound] = "Server is not found!"; 
            #endregion

            //后面考虑读取数据库

            #region 100000~200000  V1
            #region 100000~110000 V1 Report
            #region 100000~110100 V1 Report WipData

            dic[EmLanguageType.CHN][EmBussinesCodeType.ReportIntsnEmpty] = "Intsn为空！";
            dic[EmLanguageType.EN][EmBussinesCodeType.ReportIntsnEmpty] = "Intsn is empty"; 

            dic[EmLanguageType.CHN][EmBussinesCodeType.ReportInternalError] = "Product GetTDHeaderDatasError error！";
            dic[EmLanguageType.EN][EmBussinesCodeType.ReportInternalError] = "Product GetTDHeaderDatasError error"; 
            dic[EmLanguageType.CHN][EmBussinesCodeType.IsOverMaxRow] = "Data rows is max ！";
            dic[EmLanguageType.EN][EmBussinesCodeType.IsOverMaxRow] = "Data rows is max";
            dic[EmLanguageType.CHN][EmBussinesCodeType.ReportProductGetProcessError] = "Resource get process error！";
            dic[EmLanguageType.EN][EmBussinesCodeType.ReportProductGetProcessError] = "Resource get process error"; 
            #endregion
            #endregion
            #endregion

            #region 200001~400000  V2

            #endregion
        }

        public string GetMessage(EmLanguageType lanType, EmBussinesCodeType bCode)
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
