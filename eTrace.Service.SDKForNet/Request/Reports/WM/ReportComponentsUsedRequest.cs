using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportComponentsUsedRequest : ServerRequestBase<ReportComponentsUsedRequestItem>
     {

     }

   public class DownLoadComponentsUsedRequest : ReportDownloadRequestBase<ReportComponentsUsedRequestItem>
     {

     }


    public class ReportComponentsUsedRequestItem
     {
        public string OrgCode { get; set; }
        public string DJNo { get; set; }
        public string MaterialNo { get; set; }
        public string CLID { get; set; }
        public string DateCode { get; set; }
        public string LotNo { get; set; }
        public DateTime IssueDateFrom { get; set; }
        public DateTime IssueDateTo { get; set; }
        public string PODJNo { get; set; }                   // PO/ SA DJ
        public string Manufacturer { get; set; }
        public string SubAssembly { get; set; }
        public string ReportType { get; set; }              // Current / Archive / TLADJ
    }

}

