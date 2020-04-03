using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{
    public class GetWipInWipOutDataResponse : ServerResponseBase<List<GetWipInWipOutDataResponseItem>>
    {

    }
    public class GetWipInWipOutDataResponseItem : eTrace.Model.V2.Report.DailyRepairList.GetWipInWipOutModel.Item
    {
      
    }
}
