using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{                //FeederHeader
    public class ReportSMFeederHeaderModel : ModelBase<List<ReportSMFeederHeaderModel.Header>>
    {
        public class Header
        {
            public string EquipmentID { get; set; }
            public string Description { get; set; }
            public string Model { get; set; }
            public string Manufacturer { get; set; }
            public string ManufacturerSN { get; set; }
            public DateTime MfrDate { get; set; }
            public string CurrProdLine { get; set; }
            public string Status { get; set; }
            public string FailedTimes { get; set; }
            public string Owner { get; set; }
            public DateTime PMScheduledDate { get; set; }
        }
    }
    //PMHeader
    public class ReportSMFeederPMHeaderModel : ModelBase<List<ReportSMFeederPMHeaderModel.PMHeader>>
    {
        public class PMHeader
        {
            public string PMID { get; set; }
            public string EquipmentID { get; set; }
            public string Frequency { get; set; }
            public string PMStatus { get; set; }
            public DateTime PMScheduledDate { get; set; }
            public DateTime PMCompletionDate { get; set; }
            public string PMTechnician { get; set; }
            public string PMResult { get; set; }
            public string CreatedBy { get; set; }
        }

    }
    //PMHeader Item
    public class ReportSMFeederPMHeaderItemModel : ModelBase<List<ReportSMFeederPMHeaderItemModel.PMHeaderItem>>
    {
        public class PMHeaderItem
        {
            public string PMID { get; set; }
            public string PMItem { get; set; }
            public string ItemDesc { get; set; }
            public string PMItemResult { get; set; }
            public string Operator { get; set; }
            public string InstFileName { get; set; }
            public string AttFileName { get; set; }
            public string PMItemData { get; set; }
            public string Remarks { get; set; }
        }

    }
    //PMHeader Mat
    public class ReportSMFeederPMHeaderMatModel : ModelBase<List<ReportSMFeederPMHeaderMatModel.PMHeaderMat>>
    {
        public class PMHeaderMat
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
    //Repair Header
    public class ReportSMFeederRepairHeaderModel : ModelBase<List<ReportSMFeederRepairHeaderModel.RepairHeader>>
    {
        public class RepairHeader
        {
            public string RepID { get; set; }
            public string EquipmentID { get; set; }
            public DateTime FailedTime { get; set; }
            public DateTime FixedTime { get; set; }
            public string Symptom { get; set; }
            public string Cause { get; set; }
            public string CauseType { get; set; }
            public string Action { get; set; }
            public string RepairCenter { get; set; }
            public string Repairman { get; set; }
        }

    }
    //RepairHeader Mat
    public class ReportSMFeederRepairHeaderMatModel : ModelBase<List<ReportSMFeederRepairHeaderMatModel.RepairHeaderMat>>
    {
        public class RepairHeaderMat
        {
            public string RepID { get; set; }
            public string Material { get; set; }
            public string Qty { get; set; }
            public string UOM { get; set; }
            public string UnitCost { get; set; }
            public string Remarks { get; set; }
        }

    }


    public class ReportSMFeederHeaderModelQuery : ModelQueryBase
    {
        public string EquipmentID { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }
        public int FailedTimes { get; set; }
        public string PMItemPMID { get; set; }
        public string PMMatPMID { get; set; }
        public string RepairRepID { get; set; }
    }
}
