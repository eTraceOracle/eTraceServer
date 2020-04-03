using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportSMPIHeaderModel : ModelBase<List<ReportSMPIHeaderModel.Header>>
    {       
        public class Header
        {
            public string PIID { get; set; }
            public string Assembly { get; set; }
            public string AssemblyRev { get; set; }
            public string OtherAssy { get; set; }
            public string Side { get; set; }
            public string ProdLine { get; set; }
            public string BaseLine { get; set; }
            public string PanelType { get; set; }
            public string PanelSize { get; set; }
            public string PanelDrawingNo { get; set; }
            public string PanelDrawingRev { get; set; }
            public string Model { get; set; }
            public string ModelRev { get; set; }
            public string PCB { get; set; }
            public string PCBRev { get; set; }
            public string OtherPCB { get; set; }
            public string ValidTo { get; set; }
            public string Status { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }
            public string ApprovalLevel { get; set; }
            public string ApprovalStatus { get; set; }

        }
    }

    public class ReportSMPIEquipmentsModel : ModelBase<List<ReportSMPIEquipmentsModel.Equipments>>
    {
        public class Equipments
        {
            public string PIID { get; set; }
            public string EquipmentID { get; set; }
            public string SeqNo { get; set; }
            public string ProgramName { get; set; }
            public string Remarks { get; set; }
            public string Item { get; set; }
            public string ItemType { get; set; }
            public string Category { get; set; }
            public string Standard { get; set; }
            public string Unit { get; set; }
        }

    }

    public class ReportSMPIMatsModel : ModelBase<List<ReportSMPIMatsModel.Mats>>
    {
        public class Mats
        {
            public string PIID { get; set; }
            public string ItemNo { get; set; }
            public string MatType { get; set; }
            public string CircuitCode { get; set; }
            public string Image { get; set; }
            public string Remarks { get; set; }

        }
    }

        public class ReportSMPIHistoryModel : ModelBase<List<ReportSMPIHistoryModel.History>>
        {
            public class History
            {
                public string KeyValue { get; set; }
                public string Level { get; set; }
                public string Result { get; set; }
                public string SendBy { get; set; }
                public DateTime SendOn { get; set; }
                public string SendTo { get; set; }
                public string CopyTo { get; set; }
                public string Remarks { get; set; }
            }
        }


        public class ReportSMPIHeaderModelQuery : ModelQueryBase
        {
            public string Assembly { get; set; }
            public string AssemblyRev { get; set; }
            public string ProdLine { get; set; }
            public string BaseLine { get; set; }
            public string ApprovalStatus { get; set; }
            public string EquipmentsPIID { get; set; }
            public string MatsPIID { get; set; }
            public string HistoryPIID { get; set; }

    }
    }
