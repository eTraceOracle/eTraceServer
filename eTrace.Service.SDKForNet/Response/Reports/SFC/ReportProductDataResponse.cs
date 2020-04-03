using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
    public class ReportProductDataResponse : ServerResponseBase<List<ReportProductDataResponse.Item>>
    {
        public class Item: ReportProductDataModel.Item
        {
            public int Seq { get; set; }
        }
    }
}
