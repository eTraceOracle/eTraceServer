using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
     public class GetProductErrorLogRequest: ServerRequestBase<ProductErrorLogRequestItem>
    {

    }
    public class DownloadProductErrorLogRequest : ReportDownloadRequestBase<ProductErrorLogRequestItem>
    {

    }

    public class ProductErrorLogRequestItem
    {
        public DateTime ErrorDateFrom { get; set; }
        public DateTime ErrorDateTo { get; set; }
        public string Model { get; set; }
        public string PCBA { get; set; }
        public string DiscreteJob { get; set; }
        public string Module { get; set; } 
    }
}
