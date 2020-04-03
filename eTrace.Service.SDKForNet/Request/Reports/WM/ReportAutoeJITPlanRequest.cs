using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportAutoeJITPlanRequest : ServerRequestBase<ReportAutoeJITPlanRequestItem>
     {

     }

   public class DownLoadAutoeJITPlanRequest : ReportDownloadRequestBase<ReportAutoeJITPlanRequestItem>
     {

     }


    public class ReportAutoeJITPlanRequestItem
     {
        public string OrgCode { get; set; }
        public string Locator { get; set; }
        public DateTime NeedFrom { get; set; }
        public DateTime NeedTo { get; set; }
        public DateTime PlanFrom { get; set; }
        public DateTime PlanTo { get; set; }
        public DateTime CreationFrom { get; set; }
        public DateTime CreationTo { get; set; }

    }

}

