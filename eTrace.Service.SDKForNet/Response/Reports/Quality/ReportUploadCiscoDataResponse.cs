using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    //public class ReportUploadCiscoDataResponse : ServerResponseBase<List<ReportUploadCiscoDataResponse.Item>>
    public class ReportUploadCiscoDataResponse : ServerResponseBase<List<ReportUploadCiscoDataResponse.Item>>
    {
        public class Item: eTrace.Model.V2.Report.ReportUploadCiscoDataModel.Item
        {

        }
    }
}
