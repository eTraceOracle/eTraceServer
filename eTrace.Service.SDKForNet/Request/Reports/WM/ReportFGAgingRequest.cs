using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportFGAgingRequest : ServerRequestBase<ReportFGAgingRequestItem>
     {

     }

   public class DownLoadFGAgingDetailRequest : ReportDownloadRequestBase<ReportFGAgingRequestItem>
     {

     }

    public class DownLoadFGAgingSummaryRequest : ReportDownloadRequestBase<ReportFGAgingRequestItem>
    {

    }


    public class ReportFGAgingRequestItem
     {
        public string OrgCode { get; set; }
        public string SubInv { get; set; }
        public string Item { get; set; }
        public int AgingDays { get; set; }
        public DateTime RTDateFrom { get; set; }
        public DateTime RTDateTo { get; set; }
        public string DayOperator { get; set; }
        public string ReportType { get; set; }     // Detail / Summary
    }

}

