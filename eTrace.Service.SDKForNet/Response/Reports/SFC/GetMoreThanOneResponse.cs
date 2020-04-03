using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
    public class GetMoreThanOneDataResponse : ServerResponseBase<List<GetMoreThanOneDataResponseItem>>
    {

    }
    public class GetMoreThanOneDataResponseItem : eTrace.Model.V2.Report.DailyRepairList.GetMoreThanOneModel.Item
    {
      
    }
}
