using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportEquipmentRepairHeaderModel : ModelBase<List<ReportEquipmentRepairHeaderModel.Item>>
    {
        public class Item
        {
            public string EquipmentID { get; set; }
            public string RepairMan { get; set; }
            public string Department { get; set; }
            public string ProductionLine { get; set; }
            public DateTime FailedTime { get; set; }
            public DateTime FixedTime { get; set; }
            public string RepairCenter { get; set; }
            public string Status { get; set; }
            public string Category { get; set; }
            public string Symptom { get; set; }
            public string Cause { get; set; }
            public string CauseType { get; set; }
            public string Action { get; set; }
            public DateTime EndorsedOn { get; set; }
            public string EndorsedBy { get; set; }
            public string FileName { get; set; }
            public string Duration { get; set; }
            public Decimal DowntimeHour { get; set; }
            public Decimal DowntimeCostUSD { get; set; }
            public string Remarks { get; set; }
            public string RepID { get; set; }

        }
    }

    public class ReportEquipmentRepairMatModel : ModelBase<List<ReportEquipmentRepairMatModel.Mat>>
    {
        public class Mat
        {
            public string Material { get; set; }
            public string Qty { get; set; }
            public string UOM { get; set; }
            public string UnitCost { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }
        }

    }

    public class ReportEquipmentRepairHeaderModelQuery : ModelQueryBase
    {
        public string EquipmentID { get; set; }
        public string RepairMan { get; set; }
        public string ProductionLine { get; set; }
        public DateTime FailedFrom { get; set; }
        public DateTime FailedTo { get; set; }
        public DateTime FixedFrom { get; set; }
        public DateTime FixedTo { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public string RepID { get; set; }
    }
}
