using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class WipFlowQuery : ModelQueryBase
    {
        public string Intsn { get; set; }
    }
    public class WipFlowDB : WipFlow
    {
        public string WipId { get; set; }
        public string IntSN { get; set; }
        public string Model { get; set; }
        public string PCBA { get; set; }
        public string DJ { get; set; }
        public string InvOrg { get; set; }
        public string ProdLine { get; set; }
        public string CurrentProcess { get; set; }
        public string Result { get; set; }
        public string AllPassed { get; set; }
        public string MotherBoardSN { get; set; }
        public string JobID { get; set; }
        public string PanelID { get; set; }
        public DateTime ChangedOn { get; set; }
        public string ChangedBy { get; set; }
    }
    public class WipFlow
    {
        public int SeqNo { get; set; }
        public string Process { get; set; }
        public string Status { get; set; }
        public int TestRound { get; set; }
        public int FailedTest { get; set; }
        public string LastResult { get; set; }
        public int MaxTestRound { get; set; }
        public int MaxFailure { get; set; }
    

    }
}
