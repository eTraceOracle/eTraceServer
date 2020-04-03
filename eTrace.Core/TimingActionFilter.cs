using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace eTrace.Core
{
    public class TimingActionFilter : ActionFilterAttribute
    {
        private static ILog logger = Common.Log.LogHelper.GetLog("TimingActionFilter");
        //private static ILog sqlLogger = Common.Log.LogHelper.GetSQLLog();
        private const string Key = "__action_duration__";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
           
            //logger.DebugFormat("url:{0},arg--{1}", actionContext.Request.RequestUri,
            //    string.Join(",", (from q in actionContext.ActionArguments
            //                      select string.Format("key:{0},value:{1}{2}", q.Key, SerializationHelper.Serialize(q.Value, EmFormatType.Json), Environment.NewLine)).ToList()));

            if (SkipLogging(actionContext))
            {
                return;
            }
            var stopWatch = new Stopwatch();
            actionContext.Request.Properties[Key] = stopWatch;
            stopWatch.Start();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (!actionExecutedContext.Request.Properties.ContainsKey(Key))
            {
                return;
            } 
            //var aa=  actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;
            var stopWatch = actionExecutedContext.Request.Properties[Key] as Stopwatch;
            if (stopWatch != null)
            {
                stopWatch.Stop();
                var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
                var controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                if (!(controllerName== "Home" && actionName== "Index"))
                {
                    var IPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                    string hostName = string.Empty;
                    try
                    {
                         hostName = System.Net.Dns.GetHostEntry(System.Web.HttpContext.Current.Request.UserHostAddress).HostName;

                    }
                    catch 
                    {
                         
                    }
                    var requestString = string.Join(",", (from q in actionExecutedContext.ActionContext.ActionArguments 
                                 select string.Format("key:{0},value:{1}{2}", q.Key, SerializationHelper.Serialize(q.Value, EmFormatType.Json), Environment.NewLine)).ToList());
                    Common.Log.LogHelper.SQLDebug(new Common.Log.LogHelper.SQLLOGModel()
                    {
                        Action = actionName,
                        Comment =   string.Format("request:{0}" , requestString),
                        Controller = controllerName,
                        HostName = hostName,
                        IP = IPAddress,
                        SentOn = DateTime.Now,
                        Milliseconds = stopWatch.Elapsed.Milliseconds 
                    });
                    //logger.DebugFormat("[Execution of{0}- {1} took {2}]", controllerName, actionName, stopWatch.Elapsed);
                }
              
            }

        }

        private static bool SkipLogging(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<NoLogAttribute>().Any() ||
                    actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<NoLogAttribute>().Any();
        }
    }
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class NoLogAttribute : Attribute
    {

    }
}
