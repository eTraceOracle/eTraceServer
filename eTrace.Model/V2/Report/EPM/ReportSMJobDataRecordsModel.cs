using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportSMJobDataRecordsJobModel : ModelBase<List<ReportSMJobDataRecordsJobModel.Job>>
    {
        public class Job
        {
            public string JobID { get; set; }
            public string JobQty { get; set; }
            public DateTime ScheduleDate { get; set; }
            public string DJ { get; set; }
            public string DJQty { get; set; }
            public string Assembly { get; set; }
            public string AssemblyRev { get; set; }
            public string Side { get; set; }
            public string Model { get; set; }
            public string ModelRev { get; set; }
            public string PCB { get; set; }
            public string PCBRev { get; set; }
            public string ProdLine { get; set; }
            public string BaseLine { get; set; }
            public string Status { get; set; }
            public string LeadFree { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime CompletedTime { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }


        }

    }

    public class ReportSMJobDataRecordsEquipmentModel : ModelBase<List<ReportSMJobDataRecordsEquipmentModel.Equipment>>
    {
        public class Equipment
        {
            public string DJ { get; set; }
            public string JobID { get; set; }
            public string JobQty { get; set; }
            public DateTime ScheduleDate { get; set; }
            public string Assembly { get; set; }
            public string AssemblyRev { get; set; }
            public string ProdLine { get; set; }
            public string EquipmentID { get; set; }
            public string SeqNo { get; set; }
            public string Item { get; set; }
            public string ItemType { get; set; }
            public string Category { get; set; }
            public string Value { get; set; }
            public string Unit { get; set; }
        }

    }


    public class ReportSMJobDataRecordsSPGModel : ModelBase<List<ReportSMJobDataRecordsSPGModel.SPG>>
    {
        public class SPG
        {
            public string DJ { get; set; }
            public string JobID { get; set; }
            public string JobQty { get; set; }
            public DateTime ScheduleDate { get; set; }
            public string Assembly { get; set; }
            public string AssemblyRev { get; set; }
            public string ProdLine { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime CompleteTime { get; set; }
            public string Material { get; set; }
            public string MaterialID { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
        }

    }

        public class ReportSMJobDataRecordsQuery : ModelQueryBase
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
