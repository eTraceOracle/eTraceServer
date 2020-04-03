using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    public class ReportEquipmentRequest : ServerRequestBase<ReportEquipmentDatasRequestItem>
    {

    }
    public class DownloadEquipmentDatasRequest : ReportDownloadRequestBase<ReportEquipmentDatasRequestItem>
    {

    }
    public class ReportEquipmentDatasRequestItem
    {
        public string EquipmentID { get; set; }
        public string FixedAssessID { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Status { get; set; }
        public string Model { get; set; }
        public string CurrProdLine { get; set; }
        public string Department { get; set; }
    }


}