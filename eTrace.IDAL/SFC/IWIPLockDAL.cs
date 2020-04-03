using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IWIPLockDAL
    {
        /// <summary>
        /// Query By Page
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<GetWipLockDataResultModel.Item> GetWipLoackDataByPage(GetWipLockDataModelQuery query);
        /// <summary>
        /// Query all data by Condition
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<GetWipLockDataResultModel.Item> GetWipLoackData(GetWipLockDataModelQuery query);
        /// <summary>
        /// get Total row count by given query condition
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long GetTotalCount(GetWipLockDataModelQuery.Item query);

    }
}
