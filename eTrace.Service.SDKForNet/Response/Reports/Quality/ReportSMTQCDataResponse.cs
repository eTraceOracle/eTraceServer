using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    //public class ReportSMTQCDataResponse : ServerResponseBase<List<ReportSMTQCDataResponse.Item>>
    public class ReportSMTQCDataResponse : ServerResponseBase<List<ReportSMTQCDataResponse.Item>>
    {
        public class Item: eTrace.Model.V2.Report.ReportSMTQCDataModel.Item
        {

        }
    }
}
