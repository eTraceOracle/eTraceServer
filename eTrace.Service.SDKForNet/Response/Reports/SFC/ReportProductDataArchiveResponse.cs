using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
    public class ReportProductDataArchiveResponse : ServerResponseBase<List<ReportProductDataArchiveResponse.Item>>
    {
        public class Item : ReportProductArchiveDataModel.Item 
        {
            public int Seq { get; set; }
        }
    }
}
