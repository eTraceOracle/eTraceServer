using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
    public class GetWipOutDataResponse : ServerResponseBase<List<GetWipOutDataResponseItem>>
    {

    }
    public class GetWipOutDataResponseItem : eTrace.Model.V2.Report.DailyRepairList.GetWipOutModel.Item
    {
      
    }
}
