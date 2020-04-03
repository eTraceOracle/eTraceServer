using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportPickOrderRequest : ServerRequestBase<ReportPickOrderRequestItem>
     {

     }

   public class DownLoadPickOrderRequest : ReportDownloadRequestBase<ReportPickOrderRequestItem>
     {

     }


    public class ReportPickOrderRequestItem
     {
        public string OrgCode { get; set; }
        public string Floor { get; set; }
        public string DJNo { get; set; }
        public string PickOrderNo { get; set; }
        public string SourceSubInv { get; set; }
        public string SupplyType { get; set; }
        public DateTime CreationFrom { get; set; }
        public DateTime CreationTo { get; set; }
        public string Status { get; set; }

    }

}

