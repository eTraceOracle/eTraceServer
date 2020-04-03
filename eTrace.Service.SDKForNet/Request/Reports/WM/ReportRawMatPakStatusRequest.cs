using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportRawMatPakStatusRequest : ServerRequestBase<ReportRawMatPakStatusRequestItem>
     {

     }

   public class DownLoadRawMatPakStatusRequest : ReportDownloadRequestBase<ReportRawMatPakStatusRequestItem>
     {

     }


    public class ReportRawMatPakStatusRequestItem
     {
        public string OrgCode { get; set; }
        public string SubInv { get; set; }
        public string LocatorFrom { get; set; }
        public string LocatorTo { get; set; }
        public string ItemNo { get; set; }

    }

}

