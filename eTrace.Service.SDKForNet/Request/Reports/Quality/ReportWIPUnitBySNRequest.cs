using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportWIPUnitBySNRequest :
        ServerRequestBase<ReportWIPUnitBySNDatasRequestItem>
        {

        }
        public class DownloadWIPUnitBySNDatasRequest :
       ReportDownloadRequestBase<ReportWIPUnitBySNDatasRequestItem>
        {

        }
        public class ReportWIPUnitBySNDatasRequestItem
    {
            public string IntSN { get; set; }
    }


}