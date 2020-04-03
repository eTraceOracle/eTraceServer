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
    public class GetListOfRepairDataRequest : ServerRequestBase<ListOfRepairDataRequestItem>
    {
      
    }
    public class DownloadListOfRepairDataRequest : ReportDownloadRequestBase<ListOfRepairDataRequestItem>
    {

    }
    public class ListOfRepairDataRequestItem: eTrace.Model.V2.Report.GetListOfRepairDataQuery.Item
    {
        
    }
}
