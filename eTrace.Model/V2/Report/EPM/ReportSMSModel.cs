using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
 
    public class ReportSMSSummaryModel : ModelBase<List<ReportSMSSummaryModel.Summary>>
    {
        public class Summary
        { 
            public string Dept { get; set; }
            public string Mon { get; set; }
            public int SMScount { get; set; }
            public decimal SMSPercent { get; set; }
        }
    }

    public class ReportSMSDetailModel : ModelBase<List<ReportSMSDetailModel.Detail>>
    {
        public class Detail
        {
            public int SMScount { get; set; }
            public int SMSID { get; set; }
            public string Dept { get; set; }
            public string Application { get; set; }
            public string Category { get; set; }
            public string Message { get; set; }
            public DateTime ? SentOn { get; set; }
            public string ResultCode { get; set; }
            public string ResultMsg { get; set; }            
        }
    }


    public class ReportSMSModelQuery : ModelQueryBase
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
