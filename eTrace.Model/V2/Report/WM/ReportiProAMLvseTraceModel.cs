using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Model.V2.Report
{
    public class ReportiProAMLvseTraceModel : ModelBase<List<ReportiProAMLvseTraceModel.Item>>
    {
        public class Item
        {
            public string OrgCode { get; set; }
            public string CLID { get; set; }
            public string MaterialNo { get; set; }
            public string MaterialDesc { get; set; }
            public decimal QtyBaseUOM { get; set; }
            public string BaseUOM { get; set; }
            public string CreatedBy { get; set; } 
            public DateTime? ExpDate { get; set; }
            public string RecDocNo { get; set; }
            public string PurOrdNo { get; set; }
            public string DeliveryType { get; set; }
            public string VendorID { get; set; }
            public string VendorName { get; set; }
            public string VendorPN { get; set; }
            public string InvoiceNo { get; set; }
            public string HeaderText { get; set; }
            public string Operator { get; set; }
            public string StockType { get; set; }
            public string QMLStatus { get; set; }
            public string StatusCode { get; set; }
            public string eTraceMFR { get; set; }
            public string eTraceMPN { get; set; }
            public string iProMFR { get; set; }
            public string iProMPN { get; set; }
            public string iProStatus { get; set; }
        }
    }

    

    public class ReportiProAMLvseTraceModelQuery : ModelQueryBase
    {
        public string OrgCode { get; set; }
        public string SubInv { get; set; }
        public string RTDateFrom { get; set; }
        public string RTDateTo { get; set; }
        public string AMLStatus { get; set; }
        public string CLIDStatus { get; set; }        
    }
}
