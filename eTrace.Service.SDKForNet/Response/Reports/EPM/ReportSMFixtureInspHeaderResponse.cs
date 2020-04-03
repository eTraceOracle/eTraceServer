using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportSMFixtureInspHeaderResponse : ServerResponseBase<List<ReportSMFixtureInspHeaderResponse.Item>>
    {
        public class Item: ReportSMFixtureInspHeaderModel.Item
        {
            //继承Model，避免代码冗余
        }
    }

    public class ReportSMFixtureInspHeaderPMItemResponse : ServerResponseBase<List<ReportSMFixtureInspHeaderPMItemResponse.Item>>
    {
        public class Item: ReportSMFixtureInspItemModel.Item
        {
            //继承Model，避免代码冗余
        }
    }

}
