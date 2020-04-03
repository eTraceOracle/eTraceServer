using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportWIPUnitByDJRequest :
        ServerRequestBase<ReportWIPUnitByDJDatasRequestItem>
        {

        }
        public class DownloadWIPUnitByDJDatasRequest :
       ReportDownloadRequestBase<ReportWIPUnitByDJDatasRequestItem>
        {

        }
        public class ReportWIPUnitByDJDatasRequestItem
    {
            public string IntSN { get; set; }
    }


}