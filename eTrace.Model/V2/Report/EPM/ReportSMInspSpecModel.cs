using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportSMInspSpecModel : ModelBase<List<ReportSMInspSpecModel.Item>>
    {
        public class Item
        {

            public string InspSpecID { get; set; }
            public string Description { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }
            public string InspItem { get; set; }
            public string ItemDesc { get; set; }
            public string InspCondition { get; set; }
            public string Standard { get; set; }
            public string LowerLimit { get; set; }
            public string UpperLimit { get; set; }
            public string Unit { get; set; }

        }
    }

    public class ReportSMInspSpecModelQuery : ModelQueryBase
    {
        public string InspSpecID { get; set; }
    }
}
