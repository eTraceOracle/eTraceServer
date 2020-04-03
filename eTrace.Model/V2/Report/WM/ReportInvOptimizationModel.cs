using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportInvOptimizationModel : ModelBase<List<ReportInvOptimizationModel.Item>>
    {
        public class Item
        {
            public string OrgCode { get; set; }
            public string ShortagePN { get; set; }
            public string ShPNDesc { get; set; }
            public double ShPNCost { get; set; }
            public string ShPNBuyer { get; set; }
            public double Shortage { get; set; }
            public string MFR { get; set; }
            public string MPN { get; set; }
            public string AMLStatus { get; set; }
            public string ExcessPN { get; set; }
            public string ExPNDesc { get; set; }
            public double ExPNCost { get; set; }
            public string ExPNBuyer { get; set; }
            public double ExPNMPQ { get; set; }
            public string SubInventory { get; set; }
            public string Locator { get; set; }
            public DateTime ExpDate { get; set; }
            public string RTLot { get; set; }   
            public double OnhandQty { get; set; }
            public double ExcessQty { get; set; }
            public double AllocatedQty { get; set; }
            public double AvlQty { get; set; }
            public double TransferQty { get; set; }
            public double InTransit { get; set; }            
        }
    }
    public class ReportInvOptimizationModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string ShortageItems { get; set; }
        public string DmdCutDate_Shortage { get; set; }
        public string POCutDate_Shortage { get; set; }
        public bool SafetyStock { get; set; }
        public string DmdCutDate_Excess { get; set; }
        public string POCutDate_Excess { get; set; }
    }
}
