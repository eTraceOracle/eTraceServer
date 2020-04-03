using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
    public class GetListOfRepairDataResponse : ServerResponseBase<List<GetListOfRepairDataResponseItem>>
    {

    }
    public class GetListOfRepairDataResponseItem: eTrace.Model.V2.Report.GetListOfRepairDataModel.Item
    {
      
    }
}
