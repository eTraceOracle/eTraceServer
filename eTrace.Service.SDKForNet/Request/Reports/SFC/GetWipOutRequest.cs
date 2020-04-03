using eTrace.Service.SDKForNet.Request.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports.SFC
{
    /// <summary>
    /// GetWipOut 方法请求参数
    /// </summary>
    public class GetWipOutRequest : ServerRequestBase<WipOutRequestItem>
    {
      
    }
    public class DownloadWipOutRequest : ReportDownloadRequestBase<WipOutRequestItem>
    {

    }
    public class WipOutRequestItem : eTrace.Model.V2.Report.DailyRepairList.RequestItem
    {
        
    }
}
