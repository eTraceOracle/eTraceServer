using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IListOfRepairDataDAL
    {
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<GetListOfRepairDataModel.Item> GetListOfRepairDataByPage(GetListOfRepairDataQuery query);
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<GetListOfRepairDataModel.Item> GetListOfRepirData(GetListOfRepairDataQuery query);
        /// <summary>
        /// 获取符合查询条件的总数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long GetTotalCount(GetListOfRepairDataQuery.Item query);

        /// <summary>
        /// 这.....
        /// </summary>
        /// <param name="Assembly"></param>
        /// <param name="ItemNO"></param>
        /// <param name="TLA_DJ"></param>
        /// <returns></returns>
        PossibleMaterialInfo GetPossibleMaterialInfo(string Assembly, string ItemNO, string TLA_DJ);

    }
}
