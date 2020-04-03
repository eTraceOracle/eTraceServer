using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportYieldReportByDJResponse : ServerResponseBase<List<ReportYieldReportByDJResponse.Item>>
    {
        public class Item
        {
            public string Model { get; set; }
            public string Station { get; set; }
            public decimal Total { get; set; }
            public decimal Success { get; set; }
            public decimal Failed { get; set; }
            public decimal Yield { get; set; }
            public decimal PPM { get; set; }
            public string ProdOrder { get; set; }
            public decimal POQty { get; set; }
            public string Floor { get; set; }
            public string Description { get; set; }
            public string BusinessUnit { get; set; }
            public string Power { get; set; }
            public string VoltageType { get; set; }
            public string GenerateDate { get; set; }
        }
    }
}
