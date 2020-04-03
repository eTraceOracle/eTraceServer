using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportEquipmentFixturePMHeaderModel : ModelBase<List<ReportEquipmentFixturePMHeaderModel.Item>>
    {
        public class Item
        {
            public string EquipmentID { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string Frequency { get; set; }
            public string Seq { get; set; }
            public string Department { get; set; }
            public string ProductionLine { get; set; }
            public string ProductionFloor { get; set; }
            public string Description { get; set; }
            public string Model { get; set; }
            public string Owner { get; set; }
            public string PMSpecID { get; set; }
            public string PMStatus { get; set; }
            public DateTime PMSCheduledDate { get; set; }
            public DateTime PMStartDate { get; set; }
            public DateTime PMCompletionDate { get; set; }
            public string PMUsedTime { get; set; }
            public string PMTechnician { get; set; }
            public string PMResult { get; set; }
            public string Attachment { get; set; }
            public DateTime CreatedOn { get; set; }
            public string CreatedBy { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }
            public string CurrProdLine { get; set; }
            public string CurrLocation { get; set; }
            public string CurrUseCount { get; set; }
            public string PMID { get; set; }

        }

    }

    public class ReportEquipmentFixturePMItemModel : ModelBase<List<ReportEquipmentFixturePMItemModel.PMItems>>
    {
        public class PMItems
        {           
            public string PMItem { get; set; }
            public string ItemDesc { get; set; }
            public string PMItemResult { get; set; }
            public string Operator { get; set; }
            public string InstFileName { get; set; }
            public string AttFileName { get; set; }
            public string PMItemData { get; set; }
            public string Remarks { get; set; }
            public string PMID { get; set; }
        }

    }

    public class ReportEquipmentFixturePMMatModel : ModelBase<List<ReportEquipmentFixturePMMatModel.PMMat>>
    {
        public class PMMat
        {
            public string Material { get; set; }
            public string Qty { get; set; }
            public string UOM { get; set; }
            public string UnitCost { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }
            public string PMID { get; set; }
        }

    }


    public class ReportEquipmentFixturePMDetailModel : ModelBase<List<ReportEquipmentFixturePMDetailModel.DetailItem>>
    {
        public class DetailItem
        {
            public string EquipmentID { get; set; }
            public string PMStatus { get; set; }
            public DateTime PMSCheduledDate { get; set; }
            public DateTime PMStartDate { get; set; }
            public DateTime PMCompletionDate { get; set; }
            public string PMResult { get; set; }
            public string PMItem { get; set; }
            public string ItemDesc { get; set; }
            public string PMItemData { get; set; }
            public string Operator { get; set; }
            public string PMInstruction { get; set; }
            public string Attachment { get; set; }
            public string Remarks { get; set; }
        }

    }

    public class ReportEquipmentFixturePMHeaderQuery : ModelQueryBase
    {
        public string EquipmentID { get; set; }      
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Frequency { get; set; }
        public DateTime ScheduledFrom { get; set; }
        public DateTime ScheduledTo { get; set; }
        public DateTime PMCompletionFrom { get; set; }
        public DateTime PMCompletionTo { get; set; }
        public string Department { get; set; }
        public string PMStatus { get; set; }
        public string ProductionLine { get; set; }
        public string ProductionFloor { get; set; }
        public string PMID { get; set; }
    }
}
