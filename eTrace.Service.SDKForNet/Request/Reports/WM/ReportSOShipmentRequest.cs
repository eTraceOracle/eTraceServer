using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportSOShipmentRequest : ServerRequestBase<ReportSOShipmentRequestItem>
     {

     }

   public class DownLoadSOShipmentDetailRequest : ReportDownloadRequestBase<ReportSOShipmentRequestItem>
     {

     }

    public class DownLoadSOShipmentSummaryRequest : ReportDownloadRequestBase<ReportSOShipmentRequestItem>
    {

    }


    public class ReportSOShipmentRequestItem
     {
        public string OrgCode { get; set; }
        public string DeliveryNo { get; set; }
        public string Item { get; set; }
        public int StorageDay { get; set; }
        public string DateCode { get; set; }
        public string LotNo { get; set; }
        public DateTime IssueDateFrom { get; set; }
        public DateTime IssueDateTo { get; set; }
        public string DestSubInv { get; set; }
        public string ReportType { get; set; }     // Detail / Summary
    }

}

