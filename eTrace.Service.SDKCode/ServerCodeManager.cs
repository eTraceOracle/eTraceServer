using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKCode
{
    public class ServerCodeManager
    {
        private static ServerCodeManager instance = new ServerCodeManager();
        public static ServerCodeManager Instance
        {
            get { return instance; }
        }

        private Dictionary<emLanguageType, Dictionary<emServerCodeType, string>> dic = new Dictionary<emLanguageType, Dictionary<emServerCodeType, string>>();

        private ServerCodeManager()
        {
            InitDatas();
        }

        private void InitDatas()
        {
            dic[emLanguageType.CHN] = new Dictionary<emServerCodeType, string>();
            dic[emLanguageType.EN] = new Dictionary<emServerCodeType, string>();
            dic[emLanguageType.PHI] = new Dictionary<emServerCodeType, string>();

            dic[emLanguageType.CHN][emServerCodeType.ServerNotFound] = "服务不存在！";
            dic[emLanguageType.EN][emServerCodeType.ServerNotFound] = "Server is not found!";
            dic[emLanguageType.PHI][emServerCodeType.ServerNotFound] = "";
        }

        public string GetMessage(emLanguageType lanType, emServerCodeType sCode)
        {
            if (dic.ContainsKey(lanType))
            {
                if (dic[lanType].ContainsKey(sCode))
                {
                    return dic[lanType][sCode];
                }
            }
            return string.Empty;
        }
    }
}
