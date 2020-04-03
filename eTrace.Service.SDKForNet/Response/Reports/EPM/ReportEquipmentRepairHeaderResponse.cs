using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportEquipmentRepairHeaderResponse : ServerResponseBase<List<ReportEquipmentRepairHeaderResponse.Item>>
    {
        public class Item: ReportEquipmentRepairHeaderModel.Item
        {
            //public string EquipmentID { get; set; }
            //public string RepairMan { get; set; }
            //public string Department { get; set; }
            //public string ProductionLine { get; set; }
            //public DateTime FailedTime { get; set; }
            //public DateTime FixedTime { get; set; }
            //public string RepairCenter { get; set; }
            //public string Status { get; set; }
            //public string Category { get; set; }
            //public string Symptom { get; set; }
            //public string Cause { get; set; }
            //public string CauseType { get; set; }
            //public string Action { get; set; }
            //public DateTime EndorsedOn { get; set; }
            //public string EndorsedBy { get; set; }
            //public string FileName { get; set; }
            //public string Duration { get; set; }
            //public Decimal DowntimeHour { get; set; }
            //public Decimal DowntimeCostUSD { get; set; }
            //public string Remarks { get; set; }
        }
    }

    public class ReportEquipmentRepairMatResponse : ServerResponseBase<List<ReportEquipmentRepairMatResponse.Mat>>
    {
        public class Mat: ReportEquipmentRepairMatModel.Mat
        {

        }
    }
}
