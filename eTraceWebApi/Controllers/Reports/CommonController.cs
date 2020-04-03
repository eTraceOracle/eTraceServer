using eTrace.Common.Mail;
using eTrace.Service.SDKForNet;
using eTrace.Service.SDKForNet.Request.Reports.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using eTrace.Common;
using eTrace.Report.BLL.Business;
using System.Drawing;
using System.IO;
using System.Configuration;


namespace eTraceWebApi.Controllers.Reports
{
    public class CommonController : ApiController
    {

        /// <summary>
        /// 发送用户建议
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ServerResponseBase<bool> SendReportFeedbackMail(SendReporFeedbackMailRequest request)
        {
            ServerResponseBase<bool> rt = new ServerResponseBase<bool>();
            var IPAddress = HttpContext.Current.Request.UserHostAddress;
            var hostName = System.Net.Dns.GetHostEntry(HttpContext.Current.Request.UserHostAddress).HostName;
 
            bool? isConvenient = null;
            if (string.Compare(request.Data.IsConvenientToUse, "yes", true) == 0)
            {
                isConvenient = true;
            }
            else if (string.Compare(request.Data.IsConvenientToUse, "NO", true) == 0)
            {
                isConvenient = false ;
            }
            bool? isLike = null;
            if (string.Compare(request.Data.LikeThisVersion, "yes", true) == 0)
            {
                isLike = true;
            }
            else  if (string.Compare(request.Data.LikeThisVersion, "NO", true) == 0)
            {
                isLike = false;
            }
            bool? isPerformance = null;
            if (string.Compare(request.Data.IsBetterThanBefore, "yes", true) == 0)
            {
                isPerformance = true;
            }
            else if (string.Compare(request.Data.IsBetterThanBefore, "NO", true) == 0)
            {
                isPerformance = false;
            }
            rt.Data = ReportFeedBackBLL.Instance.RecordUserFeedback(new eTrace.Model.V2.Report.ReportFeedbackModel()
            {
                BackToOldVersion= request.Data.IsBackToOldVersion,
                Comment = request.Data.CustomAdvice,
                Convenient = isConvenient,
                HostName = hostName,
                IP =IPAddress,
                Like = isLike,
                Performance = isPerformance,
                SentOn =DateTime.Now
            });
            return rt;
            //string module = "eTraceReport";
            //string[] mailTo = new string[] { };
            //using (eTraceOracleERP.eTraceOracleERPSoapClient eTraceOracleERPSoapClient = new eTraceOracleERP.eTraceOracleERPSoapClient())
            //{

            //    mailTo = eTraceOracleERPSoapClient.GetMailList(module)
            //                .Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
            //}
            //ServerResponseBase<bool> rt = new ServerResponseBase<bool>();
            //string mailFrom = "eTrace." + module + "@artesyn.com";
            //string mailSubject = "eTrace Report Feedback";
            //Dictionary<string, string> dicMessage = new Dictionary<string, string>();
            //dicMessage.Add("Item", "Feedback");
            //dicMessage.Add("Do you like this new version?", request.Data.LikeThisVersion);
            //dicMessage.Add("Is it convenient to use?", request.Data.IsConvenientToUse);
            //dicMessage.Add("Is the performance better than previous version?", request.Data.IsBetterThanBefore);
            //dicMessage.Add("IP Address", IPAddress);
            //dicMessage.Add("IP HostName", hostName);
            //dicMessage.Add("Is Back to Old Version", request.Data.IsBackToOldVersion?"Yes":"No");
            //dicMessage.Add("Custom Advice", request.Data.CustomAdvice);

            //string message = @"<body style='font-size:90%;font-family:Calibri'><p>" + "Hi IT Team, " + "</p>" +
            //             "<p>" + "Please see the user feedback" + "</p>" +
            //              "</body>"; 
            // message = message+ MailHelper.ExportDictionaryToHtml(dicMessage);
            //rt.Data = eTrace.Common.Mail.MailHelper.SendMail(mailFrom, mailTo.ToArray(), mailSubject, message);
           
        }

    }
}
