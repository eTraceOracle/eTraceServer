using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportEquipmentPMPlanModel : ModelBase<List<ReportEquipmentPMPlanModel.Item>>
    {
        public class Item
        {
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public string Frequency { get; set; }
            public string Tolerance { get; set; }
            public string PMSpecID { get; set; }
            public DateTime ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string Remarks { get; set; }

        }
    }

    public class ReportEquipmentPMPlanModelQuery : ModelQueryBase
    {
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Frequency { get; set; }
    }
}
