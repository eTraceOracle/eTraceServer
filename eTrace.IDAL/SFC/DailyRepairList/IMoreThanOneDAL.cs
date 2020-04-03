using eTrace.Model.V2.Report;
using eTrace.Model.V2.Report.DailyRepairList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL.DailyRepairList
{
    public interface IMoreThanOneDAL
    {
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<GetMoreThanOneModel.Item> GetDataByPage(GetMoreThanOneQuery query);
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<GetMoreThanOneModel.Item> GetData(GetMoreThanOneQuery query);
        /// <summary>
        /// 获取符合查询条件的总数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long GetTotalCount(RequestItem query);
        

    }
}
