using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
        public class ReportSMMatTransRequest : ServerRequestBase<ReportSMMatTransRequestItem>
        {

        }

        public class DownLoadSMMatTransRequest : ReportDownloadRequestBase<ReportSMMatTransRequestItem>
        {

        }


        public class ReportSMMatTransRequestItem
        {
        public string Material { get; set; }
        public string TO { get; set; }
        public string FromLocID { get; set; }
        public string ToLocID { get; set; }
        public string MovementType { get; set; }
    }
        
}

