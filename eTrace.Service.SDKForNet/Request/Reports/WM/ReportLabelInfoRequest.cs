using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
   public class ReportLabelInfoRequest : ServerRequestBase<ReportLabelInfoRequestItem>
     {

     }

   public class DownLoadLabelInfoDetailRequest : ReportDownloadRequestBase<ReportLabelInfoRequestItem>
     {

     }

    public class DownLoadLabelInfoSummaryRequest : ReportDownloadRequestBase<ReportLabelInfoRequestItem>
    {

    }


    public class DownLoadLabelInfoePurgeRequest : ReportDownloadRequestBase<ReportLabelInfoRequestItem>
    {

        public List<TableHeader> SMTableHeaders = new List<TableHeader>();
        public List<TableHeader> DTTableHeaders = new List<TableHeader>();

    }

    public class ReportLabelInfoRequestItem
     {
        public string OrgCode { get; set; }
        public string RecDocNo { get; set; }
        public string SubInv { get; set; }
        public string VendorID { get; set; }
        public string Locator { get; set; }
        public string MaterialNo { get; set; }
        public string DateCode { get; set; }
        public string LotNo { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturerPN { get; set; }
        public DateTime RTFrom { get; set; }
        public DateTime RTTo { get; set; }
        public string CLID { get; set; }
        public string BoxID { get; set; }
        public string RTLot { get; set; }
        public string StatusCode { get; set; }
        public string LastTransaction { get; set; }
        public string ReportType { get; set; }     // Detail / Summary

    }

}

