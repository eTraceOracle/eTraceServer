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
    public class GetWipLockDataRequest : ServerRequestBase<eTrace.Model.V2.Report.GetWipLockDataModelQuery.Item>
    {
      
    }
    public class DownloadWipLockDataRequest: ReportDownloadRequestBase<eTrace.Model.V2.Report.GetWipLockDataModelQuery.Item>
    {

    }
    //public class GetWipLockDataRequestItem
    //{
    //    /// <summary>
    //    /// 开始时间
    //    /// </summary>
    //    public DateTime StartTime { get; set; }
    //    /// <summary>
    //    /// 结束时间
    //    /// </summary>
    //    public DateTime EndTime { get; set; }
    //    /// <summary>
    //    /// 产线
    //    /// </summary>
    //    public string ProdLine { get; set; }
    //    /// <summary>
    //    /// 工序
    //    /// </summary>
    //    public string Process { get; set; }
    //    /// <summary>
    //    /// 产品型号
    //    /// </summary>
    //    public string Model { get; set; }
    //    /// <summary>
    //    /// 板型号
    //    /// </summary>
    //    public string PCBA { get; set; }
    //}
}
