using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace eTrace.Core
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                var controllerName = actionExecutedContext.ActionContext.RequestContext.RouteData.Values["controller"].ToString();
                var IPAddress = HttpContext.Current.Request.UserHostAddress;
                var hostName = System.Net.Dns.GetHostEntry(HttpContext.Current.Request.UserHostAddress).HostName;
                //Common.Log.LogHelper.Error()
                ILog logger = Common.Log.LogHelper.GetLog(controllerName);
                logger.Error(string.Format("IP:{0},hostName:{1}", IPAddress, hostName), actionExecutedContext.Exception);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}