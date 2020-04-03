using eTrace.Model.V2.Report;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportSMJobDataRecordsRequest : ServerRequestBase<ReportSMJobDataRecordsRequestItem>
    {

    }
    public class DownLoadSMJobDataRecordsJobRequest : ReportDownloadRequestBase<ReportSMJobDataRecordsRequestItem>
    {

    }

    public class DownLoadSMJobDataRecordsEquipmentRequest : ReportDownloadRequestBase<ReportSMJobDataRecordsRequestItem>
    {

    }

    public class DownLoadSMJobDataRecordsSPGRequest : ReportDownloadRequestBase<ReportSMJobDataRecordsRequestItem>
    {

    }


    //inherit Query Model
    public class ReportSMJobDataRecordsRequestItem
    {
        public string Job { get; set; }
        public string Assembly { get; set; }
        public string ProdLine { get; set; }
        public string BaseLine { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string JobStatus { get; set; }
        public string DJ { get; set; }
        public string Material { get; set; }
        public string MaterialID { get; set; }
    }

        
}

