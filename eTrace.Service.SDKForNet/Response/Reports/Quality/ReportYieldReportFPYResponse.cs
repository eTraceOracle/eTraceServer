using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportYieldReportFPYResponse : ServerResponseBase<List<ReportYieldReportFPYResponse.Item>>
    {
        public class Item
        {
            public string Model { get; set; }
            public string DJ { get; set; }
            public string Description { get; set; }
            public string Floor { get; set; }
            public string BusinessUnit { get; set; }
            public decimal SeqNo { get; set; }
            public string Station { get; set; }
            public decimal FirstTotal { get; set; }
            public decimal FirstSuccess { get; set; }
            public decimal FirstFailed { get; set; }
            public decimal FirstYield { get; set; }
            public decimal FinalTotal { get; set; }
            public decimal FinalSuccess { get; set; }
            public decimal FinalFailed { get; set; }
            public decimal FinalYield { get; set; }
            public decimal FinalWIP { get; set; }
            public string VoltageType { get; set; }
            public string ServerProdDateTime { get; set; }
            public string LocalProdDateTime { get; set; }
            public string Power { get; set; }
        }
    }
}
