using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportOnHandCompModel : ModelBase<List<ReportOnHandCompModel.Item>>
    {
        public class Item
        {
            public string Org { get; set; }
            public string SubInv { get; set; }
            public string Locator { get; set; }
            public string LotNo { get; set; }
            public string MATERIALNO { get; set; }
            public string UOM { get; set; }
            public string REVISION { get; set; } 
            public decimal eTraceQTY { get; set; }
            public decimal OraQty { get; set; }
            public decimal Delta { get; set; }            
        }
    }

    

    public class ReportOnHandCompModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string SubInv { get; set; }
        public string Locator { get; set; }
        public string LotNo { get; set; }
        public string ItemNo { get; set; }
        public string DiffFlag { get; set; }        
    }
}
