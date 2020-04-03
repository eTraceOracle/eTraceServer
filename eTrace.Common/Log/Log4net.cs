using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Common
{
    public class Log4net
    {
        private static bool isStart = false;

        public static void InitLog4Net()
        {
            if (!isStart)
            {
                isStart = true;
                var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
                XmlConfigurator.ConfigureAndWatch(logCfg);
            }
        }
    }
}
