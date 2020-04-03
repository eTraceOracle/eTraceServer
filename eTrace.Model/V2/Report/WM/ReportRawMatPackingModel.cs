using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportRawMatPackingModel : ModelBase<List<ReportRawMatPackingModel.Item>>
    {
        public class Item
        {
            public string OrgCode { get; set; }
            public string PalletWeight { get; set; }
            public string PalletID { get; set; }
            public string BoxID { get; set; }
            public string CLID { get; set; }
            public string ItemNo { get; set; }
            public decimal QtyBaseUOM { get; set; }
            public string BaseUOM { get; set; }
            public string SubInv { get; set; }
            public string Locator { get; set; }
            public string DateCode { get; set; }
            public string LotNo { get; set; }
            public string COO { get; set; }
            public string ExpDate { get; set; }
            public string RTLot { get; set; }
            public string Manufacturer { get; set; }
            public string ManufacturerPN { get; set; }
            public DateTime? ChangedOn { get; set; }
            public string ChangedBy { get; set; }
            public string LastTransaction { get; set; }
        }
    }

    

    public class ReportRawMatPackingModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string ItemNo { get; set; }
        public string BoxIDFrom { get; set; }
        public string BoxIDTo { get; set; }
        public string PalletIDFrom { get; set; }
        public string PalletIDTo { get; set; }
        public string CLID { get; set; }
    }
}
