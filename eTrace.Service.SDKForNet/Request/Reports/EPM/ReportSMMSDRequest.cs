using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportSMMSDRequest : ServerRequestBase<ReportSMMSDRequestItem>
        {

        }

        public class DownLoadSMMSDRequest : ReportDownloadRequestBase<ReportSMMSDRequestItem>
        {

        }


        public class ReportSMMSDRequestItem
        {
            public string MaterialNo { get; set; }
            public string CLID { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public string LastTransaction { get; set; }
        }
        
}

