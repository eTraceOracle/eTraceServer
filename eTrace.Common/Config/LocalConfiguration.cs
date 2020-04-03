using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Common
{
    public class LocalConfiguration
    {
        private ILog logger = LogManager.GetLogger(typeof(LocalConfiguration));

        private static LocalConfiguration instance = new LocalConfiguration();
        public static LocalConfiguration Instance
        {
            get { return instance; }
        }

        private LocalConfiguration()
        {
            try
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["LogTime"]))
                {
                    LogTime = Convert.ToBoolean(ConfigurationManager.AppSettings["LogTime"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetDbConnection(string con)
        {
            return ConfigurationManager.AppSettings[con];
        }

        private bool blLogTime = false;
        public bool LogTime
        {
            get { return blLogTime; }
            set { blLogTime = value; }
        }

        public long ReportDownloadMaxRowCount()
        {
            long rt = long.MaxValue;
            string strMaxRowCount = ConfigurationManager.AppSettings["MaxRowCount"];
            if (!string.IsNullOrEmpty(strMaxRowCount))
            {
                if (long.TryParse(strMaxRowCount, out rt))
                {
                    return rt;
                }
            }
            return rt;
        }
        public long ReportCheckDownloadRowCount()
        {
            long rt = long.MaxValue;
            string strMaxRowCount = ConfigurationManager.AppSettings["CheckDwonloadRowCount"];
            if (!string.IsNullOrEmpty(strMaxRowCount))
            {
                if (long.TryParse(strMaxRowCount, out rt))
                {
                    return rt;
                }
            }
            return rt;
        }
        public static  string GetMailHost()
        {
            return  ConfigurationManager.AppSettings["MailHost"];
        }


        public int eTraceCommandTimeout()
        {
            int t1 = int.MaxValue;
            string t2 = ConfigurationManager.AppSettings["eTraceCommandTimeout"];
            if (!string.IsNullOrEmpty(t2))
            {
                if (int.TryParse(t2, out t1))
                {
                    return t1;
                }
            }
            return t1;
        }
    }
}
