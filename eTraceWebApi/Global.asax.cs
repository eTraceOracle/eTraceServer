using eTrace.Common;
using eTrace.Core;
using log4net;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace eTraceWebApi
{
    /// <summary>
    /// WebApiApplication
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        //private static ILog logger = eTrace.Common.Log.LogHelper.GetLog("WebApiApplication");
        private static ILog _logger = null;
        private static ILog Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = eTrace.Common.Log.LogHelper.GetLog("WebApiApplication");
                }
                return _logger;
            }
        }
        protected void Application_Start()
        {
            try
            {
                Log4net.InitLog4Net(); 
                GlobalConfiguration.Configure(WebApiConfig.Register);
                var formatters = GlobalConfiguration.Configuration.Formatters;
                var jsonFormatter = formatters.JsonFormatter;
                var settings = jsonFormatter.SerializerSettings;
                settings.Formatting = Newtonsoft.Json.Formatting.Indented;
                settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //settings.Converters.Add(new UnixDateTimeConverter());
                Logger.DebugFormat("WebApiApplication Application_Started"); 

            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("WebApiApplication Application_Start error: {0}", ex);
                throw ex;
            }

            DBManagerConfig.Instance.DumpSqlEvent += OnDumpSqlEvent;
        }

        private void OnDumpSqlEvent(object sender, SQLEventArgs e)
        {
            Logger.DebugFormat("SqlDump Info:{0}", e.Sql);
        }
    }
}
