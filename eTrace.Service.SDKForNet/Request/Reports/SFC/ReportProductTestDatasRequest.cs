using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{

    public class ReportGetProductTestDatasRequest :
        ServerRequestBase<ReportProductTestDatasRequestItem>
    {

    }
    public class ReportDownloadProductTestDatasRequest :
    ReportDownloadRequestBase<ReportProductTestDatasRequestItem>
    {

    }
    /// <summary>
    /// Reuqe
    /// </summary>
    public class ReportDownloadProductTestDatasSummaryRequest :
    ReportDownloadRequestBase<ReportProductTestDatasRequestItem>
    {
        
    }
    public class ReportGetProductTestDatasSummaryRequest :
    ReportDownloadRequestBase<ReportProductTestDatasRequestItem>
    {

    }

    /// <summary>
    /// query condition ,different for each module.
    /// </summary>
    public class ReportProductTestDatasRequestItem
    {
        public string Model { get; set; }
        public string TestId { get; set; }
        public string ProductSN { get; set; }
        public string Station { get; set; }
        public string TestStep { get; set; }
        public string CartonId { get; set; }
        public bool IsCustomerReport { get; set; }
        public string DJ { get; set; } 
        public DateTime? ProductTimeStart { get; set; }
        public DateTime? ProductTimeEnd { get; set; }
        //[JsonIgnore]
        //public DateTime? ProductTimeStart
        //{
        //    get
        //    {
        //        return strProductTimeStart.GetDateTime("yyyy-MM-dd 00:00:00");
        //    }
        //}
        //[JsonProperty("ProductTimeStart")]
        //public string strProductTimeStart { get; set; }
        //[JsonIgnore]
        //public DateTime? ProductTimeEnd
        //{
        //    get
        //    {
        //        return strProductTimeEnd.GetDateTime("yyyy-MM-dd 00:00:00");
        //    }
        //}
        //[JsonProperty("ProductTimeEnd")]
        //public string strProductTimeEnd { get; set; }
    }

 
}
