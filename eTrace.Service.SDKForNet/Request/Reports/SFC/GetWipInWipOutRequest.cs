using eTrace.Service.SDKForNet.Request.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
    /// <summary>
    /// GetWipLockData 方法请求参数
    /// </summary>
    public class GetWipInWipOutRequest : ServerRequestBase<WipInWipOutRequestItem>
    {
      
    }
    public class DownloadWipInWipOutRequest : ReportDownloadRequestBase<WipInWipOutRequestItem>
    {

    }
    public class WipInWipOutRequestItem : eTrace.Model.V2.Report.DailyRepairList.RequestItem
    {
        
    }
}
