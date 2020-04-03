using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports.Common
{
    /// <summary>
    /// 发送建议邮件请求Model
    /// </summary>
     public class SendReporFeedbackMailRequest : RequestBase<SendReportFeedbackMailRequestItem>
    {

    
    }
    /// <summary>
    /// 
    /// </summary>
   public class SendReportFeedbackMailRequestItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string LikeThisVersion { get; set; }
        public string IsConvenientToUse { get; set; }
        public string IsBetterThanBefore { get; set; }
        public string IPAdress { get; set; }
        public string HostName { get; set; }
        public bool IsBackToOldVersion { get; set; }

        public string CustomAdvice { get; set; }
    }



}
