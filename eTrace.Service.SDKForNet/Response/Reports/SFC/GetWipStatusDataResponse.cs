using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response.Reports
{


    /// <summary>
    /// GetWipStatusDataDetail接口返回数据实体
    /// </summary>
    public class GetWipStatusDataDetailResponse : ServerResponseBase<List<GetWipStatusDataDetailResponseItem>>
    {
    }
     
    /// <summary>
    /// GetWipStatusDataDetail接口返回数据实体
    /// </summary>
    public class GetWipStatusDataSummaryResponse : ServerResponseBase<List<GetWipStatusDataSummaryResponseItem>>
    {
        /// <summary>
        /// 返回总数（不同于行数，指sum(count)）
        /// </summary>
        public long SummaryCount { get; set; }
    }
    /// <summary>
    /// GetWipStatusDataR接口返回数据实体数据项
    /// </summary>
    public class GetWipStatusDataDetailResponseItem : eTrace.Model.V2.Report.WipStatusDetailItem
    {
        public string IsLoaded
        {
            get
            {
                if (string.IsNullOrEmpty(this.LoadedTo) && string.IsNullOrEmpty(this.MBIntSN))
                {
                    return "NO";
                }else
                {
                    return "YES";
                }

            }
        }

    }

    /// <summary>
    /// GetWipStatusDataR接口返回数据实体数据项
    /// </summary>
    public class GetWipStatusDataSummaryResponseItem : eTrace.Model.V2.Report.WipStatusSummaryItem
    {

   
    }
}
