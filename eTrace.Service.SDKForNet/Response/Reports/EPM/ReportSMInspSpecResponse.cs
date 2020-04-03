using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportSMInspSpecResponse : ServerResponseBase<List<ReportSMInspSpecResponse.Item>>
    {
        public class Item: ReportSMInspSpecModel.Item
        {

        }
    }
}
