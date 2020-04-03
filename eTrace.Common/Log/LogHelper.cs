using log4net;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Common.Log
{
    
    public static class LogHelper
    {
        //对应log4net.config <logger name="x">
        public static string logdebug = "logdebug";
        public static string loginfo = "loginfo";
        public static string logwarn = "logwarn";
        public static string logerror = "logerror";
        public static string logfatal = "logfatal";
        //将日记对象缓存起来
        public static Dictionary<string, ILog> LogDic = new Dictionary<string, ILog>();
        public static ILog _SQLLogger = null;
        static object _islock = new object();
        public static ILog GetLog(string name)
        {
            try
            {
                if (LogDic == null)
                {
                    LogDic = new Dictionary<string, ILog>();
                }
                lock (_islock)
                {
                    if (!LogDic.ContainsKey(name))
                    {
                        var configRepository = LogManager.GetRepository();
                        var appenders = configRepository.GetAppenders();
                        var configAppender = appenders.First(x=>x is log4net.Appender.RollingFileAppender) as log4net.Appender.RollingFileAppender;
                        //创建一个新的Appender，修改file，其他不变
                        //log4net.Appender.RollingFileAppender newAppender =  new log4net.Appender.RollingFileAppender();
                        log4net.Appender.FileAppender newAppender = new log4net.Appender.FileAppender();
                        //newAppender.CopyModelValueFrom(configAppender);
                        newAppender.AppendToFile = configAppender.AppendToFile;
                        newAppender.File = configAppender.File.Replace("{logger}", name);
                        //newAppender.MaxSizeRollBackups = configAppender.MaxSizeRollBackups;
                        newAppender.Encoding = configAppender.Encoding;
                        newAppender.ErrorHandler = configAppender.ErrorHandler;
                        newAppender.ImmediateFlush = configAppender.ImmediateFlush;
                        newAppender.Layout = configAppender.Layout;
                        newAppender.LockingModel = configAppender.LockingModel;
                        newAppender.Name = string.Format("{0}Appender", name);
                        newAppender.SecurityContext = configAppender.SecurityContext;
                        newAppender.Threshold = configAppender.Threshold;
                        
                        log4net.Repository.ILoggerRepository newRepository = log4net.LogManager.CreateRepository(string.Format("{0}Repository", name));
                        log4net.Config.BasicConfigurator.Configure(newRepository, newAppender);
                        LogDic.Add(name, LogManager.GetLogger(newRepository.Name, name));
                    }
                }
                return LogDic[name];
            }
            catch (Exception ex)
            {
                return LogManager.GetLogger("Default");
            }
        }
        /// <summary>
        /// SQLLog for statistic
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILog GetSQLLog()
        {
            string name = "SQLLoger";
            try
            {
                if (_SQLLogger == null)
                {

                    _SQLLogger = LogManager.GetLogger("ADONetAppender");

                    //var configRepository = LogManager.GetRepository();
                    //var appenders = configRepository.GetAppenders();
                    //var configAppender = appenders.First(x => x is log4net.Appender.AdoNetAppender) as log4net.Appender.AdoNetAppender;
                    //configAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter().DbType = System.Data.DbType.Int32)
                    //configAppender.CommandText = @"Insert into [dbo].[T_ReportFeedback]
                    //                                      ([SentOn],[IP],[HostName],[Comment],[Controller],[Action])
                    //                                values(@SentOn, @IP, @HostName, @Comment, @Controller, @Action)";

                    //configAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter()
                    //{
                    //    DbType = System.Data.DbType.DateTime,
                    //    ParameterName= "@SentOn",
                    //    Layout = new Layout2RawLayoutAdapter(new CustomLayout("%SentOn")),
                    //});
                    //configAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter()
                    //{
                    //    DbType = System.Data.DbType.String ,
                    //    Size=20,
                    //    ParameterName = "@IP",
                    //    Layout = new Layout2RawLayoutAdapter(new CustomLayout("%IP")),
                    //});
                    //configAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter()
                    //{
                    //    DbType = System.Data.DbType.String,
                    //    Size = 30,
                    //    ParameterName = "@HostName",
                    //    Layout = new Layout2RawLayoutAdapter(new CustomLayout("%HostName")),
                    //});
                    //configAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter()
                    //{
                    //    DbType = System.Data.DbType.String,
                    //    Size = 500,
                    //    ParameterName = "@Comment",
                    //    Layout = new Layout2RawLayoutAdapter(new CustomLayout("%Comment")),
                    //});
                    //configAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter()
                    //{
                    //    DbType = System.Data.DbType.String,
                    //    Size = 50,
                    //    ParameterName = "@Controller",
                    //    Layout = new Layout2RawLayoutAdapter(new CustomLayout("%Controller")),
                    //});
                    //configAppender.AddParameter(new log4net.Appender.AdoNetAppenderParameter()
                    //{
                    //    DbType = System.Data.DbType.String,
                    //    Size = 50,
                    //    ParameterName = "@Action",
                    //    Layout = new Layout2RawLayoutAdapter(new CustomLayout("%Action")),
                    //});

                    //log4net.Repository.ILoggerRepository newRepository = log4net.LogManager.CreateRepository(string.Format("{0}Repository", name));
                    //log4net.Config.BasicConfigurator.Configure(newRepository, configAppender);
                    //_SQLLogger = LogManager.GetLogger(newRepository.Name, name);
                }
                
                return _SQLLogger;
            }
            catch (Exception ex)
            {
                return LogManager.GetLogger("Default");
            }
        }
        public static void Debug(object message)
        {
            LogManager.GetLogger(logdebug).Debug(message);
        }

        public static void Debug(object message, Exception ex)
        {
            LogManager.GetLogger(logdebug).Debug(message, ex);
        }

        public static void Error(object message)
        {

            LogManager.GetLogger(logerror).Error(message);
            //LogManager.GetLogger(Log.GetCurrentMethodFullName()).Error(message);
            //LogManager.GetLogger("Test").Error(message);
        }

        public static void SQLDebug(SQLLOGModel logModel)
        {
            //var SQLLoger = LogManager.GetLogger("ADONetAppender") as log4net.Appender.AdoNetAppender;
            //var parameter = new log4net.Appender.AdoNetAppenderParameter();
            //var layout = parameter.Layout;
            //layout.
            //SQLLoger.AddParameter(new log4net.Appender.AdoNetAppenderParameter()
            //{
            //    DbType = System.Data.DbType.Boolean,
                
            //})
            //var test = LogManager.GetLogger("ADONetAppender");
            LogHelper.GetSQLLog().Debug(logModel);
            //LogManager.GetLogger("ADONetAppender").Debug(logModel);
        }
        public static void Error(object message, Exception exception)
        {
            LogManager.GetLogger(logerror).Error(message, exception);
        }
        public static void Fatal(object message)
        {

            LogManager.GetLogger(logfatal).Fatal(message);
            //LogManager.GetLogger(Log.GetCurrentMethodFullName()).Error(message);
            //LogManager.GetLogger("Test").Error(message);
        }

        public static void Fatal(object message, Exception exception)
        {
            LogManager.GetLogger(logfatal).Fatal(message, exception);
        }


        public static void Info(object message)
        {
            LogManager.GetLogger(loginfo).Info(message);
        }

        public static void Info(object message, Exception ex)
        {
            LogManager.GetLogger(loginfo).Info(message, ex);
        }

        public static void Warn(object message)
        {
            LogManager.GetLogger(logwarn).Warn(message);
        }

        public static void Warn(object message, Exception ex)
        {
            LogManager.GetLogger(logwarn).Warn(message, ex);
        }

        //private static string GetCurrentMethodFullName()
        //{
        //    StackFrame frame;
        //    string str;
        //    string str1;
        //    bool flag;
        //    try
        //    {
        //        int num = 2;
        //        StackTrace stackTrace = new StackTrace();
        //        int length = stackTrace.GetFrames().Length;
        //        do
        //        {
        //            int num1 = num;
        //            num = num1 + 1;
        //            frame = stackTrace.GetFrame(num1);
        //            str = frame.GetMethod().DeclaringType.ToString();
        //            flag = (!str.EndsWith("Exception") ? false : num < length);
        //        }
        //        while (flag);
        //        string name = frame.GetMethod().Name;
        //        str1 = string.Concat(str, ".", name);
        //    }
        //    catch
        //    {
        //        str1 = null;
        //    }
        //    return str1;
        //}
        public class SQLLOGModel
        {
            public DateTime SentOn { get; set; }
            public string IP { get; set; } 
            public string HostName { get; set; } 
            //public bool Like { get; set; } 
            //public bool Convenient { get; set; } 
            //public bool Performance { get; set; }
            public string Comment { get; set; }
            //public bool BackToOldVersion { get; set; }
            public string Controller { get; set; }
            public string Action { get; set; }
            public int Milliseconds { get; set; }
        }
    }
}
