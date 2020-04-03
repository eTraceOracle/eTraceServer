using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportRawMatPakStatusModel : ModelBase<List<ReportRawMatPakStatusModel.Item>>
    {
        public class Item
        {
            public string OrgCode { get; set; }
            public string SubInv { get; set; }
            public string Locator { get; set; }
            public string CLID { get; set; }
            public string BoxID { get; set; }
            public string PalletID { get; set; }
            public string ItemNo	 { get; set; }
            public decimal QtyBaseUOM { get; set; }
            public string PackedBy { get; set; }
            public DateTime? PackedOn { get; set; }
            public string ChangedBy { get; set; }
            public DateTime? ChangedOn { get; set; }
    }
}

    

    public class ReportRawMatPakStatusModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string SubInv { get; set; }
        public string LocatorFrom { get; set; }
        public string LocatorTo { get; set; }
        public string ItemNo { get; set; }
    }
}
