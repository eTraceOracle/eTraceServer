using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportSMSolderPasteGlueRequest : ServerRequestBase<ReportSMSolderPasteGlueRequestItem>
        {

        }

        public class DownLoadSMSolderPasteGlueRequest : ReportDownloadRequestBase<ReportSMSolderPasteGlueRequestItem>
        {

        }


        public class ReportSMSolderPasteGlueRequestItem
        {
            public string MaterialNo { get; set; }
            public string CLID { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public string LastTransaction { get; set; }
            public bool Validation { get; set; }
    }
        
}

