using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportEPMEventResponse : ServerResponseBase<List<ReportEPMEventResponse.Item>>
    {
        public class Item:ReportEPMEventModel.Item
        {

        }
    }

    public class ReportEPMEventPMItemResponse : ServerResponseBase<List<ReportEPMEventPMItemResponse.Item>>
    {
        public class Item: ReportEPMEventItemModel.EventItem
        {

        }
    }

}
