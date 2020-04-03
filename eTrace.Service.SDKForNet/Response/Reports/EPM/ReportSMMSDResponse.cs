using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportSMMSDResponse : ServerResponseBase<List<ReportSMMSDResponse.Item>>
    {
        public class Item: ReportSMMSDModel.Item
        {

        }
    }

    public class ReportSMMSDPMItemResponse : ServerResponseBase<List<ReportSMMSDPMItemResponse.Item>>
    {
        public class Item: ReportSMMSDPMItemModel.Item
        {

        }
    }

}
