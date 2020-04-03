using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportSMSolderPasteGlueModel : ModelBase<List<ReportSMSolderPasteGlueModel.Item>>
    {
        public class Item
        {
            public string Category { get; set; }
            public string CLID { get; set; }
            public string MaterialNo { get; set; }
            public string Qty { get; set; }
            public string UOM { get; set; }
            public string LocID { get; set; }
            public DateTime ExpDate { get; set; }
            public string RemainingLife { get; set; }
            public DateTime WarmUpST { get; set; }
            public DateTime WarmUpET { get; set; }
            public DateTime StirringST { get; set; }
            public DateTime StirringET { get; set; }
            public DateTime FloorLifeST { get; set; }
            public DateTime FloorLifeET { get; set; }
            public DateTime FloorLifeST2 { get; set; }
            public DateTime FloorLifeET2 { get; set; }
            public DateTime OnStencilLifeST { get; set; }
            public DateTime OnStencilLifeET { get; set; }
            public string LastTransaction { get; set; }
            public string RefCLID { get; set; }
            public string RefCLID2 { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }

        }
    }

    public class ReportSMSolderPasteGlueModelQuery : ModelQueryBase
    {
        public string MaterialNo { get; set; }
        public string CLID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string LastTransaction { get; set; }
        public bool Validation { get; set; }
    }
}
