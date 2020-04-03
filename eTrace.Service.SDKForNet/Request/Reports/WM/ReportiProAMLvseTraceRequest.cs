using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportiProAMLvseTraceRequest : ServerRequestBase<ReportiProAMLvseTraceRequestItem>
     {

     }

   public class DownLoadiProAMLvseTraceRequest : ReportDownloadRequestBase<ReportiProAMLvseTraceRequestItem>
     {

     }


    public class ReportiProAMLvseTraceRequestItem
     {
        public string OrgCode { get; set; }
        public string SubInv { get; set; }
        public string RTDateFrom { get; set; }
        public string RTDateTo { get; set; }
        public string AMLStatus { get; set; }
        public string CLIDStatus { get; set; }
    }

}

