using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportSearchCLIDRequest : ServerRequestBase<ReportSearchCLIDRequestItem>
     {

     }

   public class DownLoadSearchCLIDRequest : ReportDownloadRequestBase<ReportSearchCLIDRequestItem>
     {

     }


    public class ReportSearchCLIDRequestItem
     {
        public string OrgCode { get; set; }
        public string RecDocNo { get; set; }
        public string SubInv { get; set; }
        public string VendorID { get; set; }
        public string Locator { get; set; }
        public string MaterialNo { get; set; }
        public string DateCode { get; set; }
        public string LotNo { get; set; }
        public DateTime RTFrom { get; set; }
        public DateTime RTTo { get; set; }
        public string CLID { get; set; }
        public string ManufacturerPN { get; set; }             // MPN
        public string PODJNo { get; set; }                   // PO/ SA DJ
        public string QMLStatus { get; set; }                  // QMLStatus
        public string StatusCode { get; set; }
        public string ReportType { get; set; }
    }

}

