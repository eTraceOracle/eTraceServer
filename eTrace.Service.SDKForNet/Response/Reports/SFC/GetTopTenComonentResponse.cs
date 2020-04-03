using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
    public class GetTopTenComonentDataResponse : ServerResponseBase<List<GetTopTenComonentDataResponseItem>>
    {

    }
    public class GetTopTenComonentDataResponseItem : eTrace.Model.V2.Report.DailyRepairList.GetTopTenComonentModel.Item
    {
      
    }
}
