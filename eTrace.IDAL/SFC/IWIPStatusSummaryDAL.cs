using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IWIPStatusSummaryDAL
    {
      


        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<WipStatusSummaryItem> GetDataByPage(GetWipStatusQuery query);
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<WipStatusSummaryItem> GetData(GetWipStatusQuery query);
        /// <summary>
        /// 获取符合查询条件的总数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long GetTotalCount(GetWipStatusQuery.Item queryItem);
        long GetSummaryCount(GetWipStatusQuery.Item queryItem);

    }
}
