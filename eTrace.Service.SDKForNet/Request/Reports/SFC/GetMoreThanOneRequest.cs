using eTrace.Service.SDKForNet.Request.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports.SFC
{
    /// <summary>
    /// GetWipLockData 方法请求参数
    /// </summary>
    public class GetMoreThanOneRequest : ServerRequestBase<MoreThanOneRequestItem>
    {
      
    }
    public class DownloadMoreThanOneRequest : ReportDownloadRequestBase<MoreThanOneRequestItem>
    {

    }
    public class MoreThanOneRequestItem : eTrace.Model.V2.Report.DailyRepairList.RequestItem
    {
        
    }
}
