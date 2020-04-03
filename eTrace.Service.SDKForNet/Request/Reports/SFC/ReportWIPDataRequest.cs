using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{

    public class ReportWIPTDHeaderRequest :
    ServerRequestBase<ReportWIPTestDataRequestItem>
    {

    }
    public class ReportDownloadWIPTDHeaderRequest :
    ReportDownloadRequestBase<ReportWIPTestDataRequestItem>
    {

    }

    public class ReportWIPTestDataRequest :
        ServerRequestBase<ReportWIPTestDataRequestItem>
    {

    }
    public class ReportDownloadWIPTestDataRequest :
    ReportDownloadRequestBase<ReportWIPTestDataRequestItem>
    {

    }
    /// <summary>
    /// Reuqe
    /// </summary>
    public class ReportDownloadWIPTestDataSummaryRequest :
    ReportDownloadRequestBase<ReportWIPTestDataRequestItem>
    {

    }
    public class ReportWIPestDataSummaryRequest :
    ReportDownloadRequestBase<ReportProductTestDatasRequestItem>
    {

    }

    /// <summary>
    /// query condition ,different for each module.
    /// </summary>
    public class ReportWIPTestDataRequestItem
    {
        public string Model { get; set; }
        public string IntSN { get; set; }
        public string Station { get; set; }
        public bool IsCustomerReport { get; set; }
        public DateTime? ProductTimeStart { get; set; }
        public DateTime? ProductTimeEnd { get; set; }
   
    }

}
