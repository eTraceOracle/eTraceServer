using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportRawMatPackingRequest : ServerRequestBase<ReportRawMatPackingRequestItem>
     {

     }

   public class DownLoadRawMatPackingRequest : ReportDownloadRequestBase<ReportRawMatPackingRequestItem>
     {

     }


    public class ReportRawMatPackingRequestItem
     {
        public string OrgCode { get; set; }
        public string ItemNo { get; set; }
        public string BoxIDFrom { get; set; }
        public string BoxIDTo { get; set; }
        public string PalletIDFrom { get; set; }
        public string PalletIDTo { get; set; }
        public string CLID { get; set; }

    }

}

