using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IComponentsUsedDAL
    {

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReportComponentsUsedModel.Item> GetComponentsUsedByPage(ReportComponentsUsedModelQuery query);


        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReportComponentsUsedModel.Item> GetComponentsUsedData(ReportComponentsUsedModelQuery query);

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReportComponentsUsedTLAModel.Item> GetComponentsUsedTLAData(ReportComponentsUsedModelQuery query);

        /// <summary>
        /// 获取符合查询条件的总数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long ComponentsUsedDataGetRowCount(ReportComponentsUsedModelQuery query);

    }
}




