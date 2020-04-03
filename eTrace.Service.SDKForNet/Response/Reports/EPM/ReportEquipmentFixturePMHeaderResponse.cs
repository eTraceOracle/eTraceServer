using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportEquipmentFixturePMHeaderResponse : ServerResponseBase<List<ReportEquipmentFixturePMHeaderResponse.Item>>
    {
        public class Item : ReportEquipmentFixturePMHeaderModel.Item
        {
            //public string EquipmentID { get; set; }
            //public string Category { get; set; }
            //public string SubCategory { get; set; }
            //public string EquipmentOrFixtureID { get; set; }
            //public string Frequency { get; set; }
            //public string Seq { get; set; }
            //public DateTime ScheduledFrom { get; set; }
            //public DateTime ScheduledTo { get; set; }
            //public string Department { get; set; }
            //public string ProductionLine { get; set; }
            //public string ProductionFloor { get; set; }
            //public string Description { get; set; }
            //public string Model { get; set; }
            //public string Owner { get; set; }
            //public string PMSpecID { get; set; }
            //public string PMStatus { get; set; }
            //public DateTime PMSCheduledDate { get; set; }
            //public DateTime PMStartDate { get; set; }
            //public DateTime PMCompletionDate { get; set; }
            //public string PMUsedTime { get; set; }
            //public string PMTechnician { get; set; }
            //public string PMResult { get; set; }
            //public string Attachment { get; set; }
            //public DateTime CreatedOn { get; set; }
            //public string CreatedBy { get; set; }
            //public DateTime ChangedOn { get; set; }
            //public string ChangedBy { get; set; }
            //public string Remarks { get; set; }
            //public string CurrProdLine { get; set; }
            //public string CurrLocation { get; set; }
            //public string CurrUseCount { get; set; }
        }

    }
    public class ReportEquipmentFixturePMItemResponse : ServerResponseBase<List<ReportEquipmentFixturePMItemResponse.PMItem>>
    {
        public class PMItem : ReportEquipmentFixturePMItemModel.PMItems
        {

        }
    }

    public class ReportEquipmentFixturePMMatResponse : ServerResponseBase<List<ReportEquipmentFixturePMMatResponse.PMMat>>
    {
        public class PMMat : ReportEquipmentFixturePMMatModel.PMMat
        {

        }
    }
}
