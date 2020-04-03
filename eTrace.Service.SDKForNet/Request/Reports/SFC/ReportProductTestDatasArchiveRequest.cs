using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{

    public class ReportGetProductTestDatasArchiveRequest :
        ServerRequestBase<ReportProductTestDatasArchiveRequestItem>
    {

    }
    public class ReportDownloadProductTestDatasArchiveRequest :
    ReportDownloadRequestBase<ReportProductTestDatasArchiveRequestItem>
    {

    }
    /// <summary>
    /// Reuqe
    /// </summary>
    public class ReportDownloadProductTestDatasArchiveSummaryRequest :
    ReportDownloadRequestBase<ReportProductTestDatasArchiveRequestItem>
    {
        
    }
    public class ReportGetProductTestDatasArchiveSummaryRequest :
    ReportDownloadRequestBase<ReportProductTestDatasArchiveRequestItem>
    {

    }

    /// <summary>
    /// query condition ,different for each module.
    /// </summary>
    public class ReportProductTestDatasArchiveRequestItem
    {
        public string Model { get; set; }
        public string TestId { get; set; }
        public string ProductSN { get; set; }
        public string Station { get; set; }
        public string TestStep { get; set; }
        public string IntSN { get; set; }
        public bool IsCustomerReport { get; set; }
        public string DJ { get; set; } 
        public DateTime? ProductTimeStart { get; set; }
        public DateTime? ProductTimeEnd { get; set; } 
    }

 
}
