using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IeJITBuildPlanDAL
    {

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReporteJITBuildPlanSearchModel.Item> GeteJITBuildPlanSearchByPage(ReporteJITBuildPlanModelQuery query);


        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReporteJITBuildPlanSearchModel.Item> GeteJITBuildPlanSearchData(ReporteJITBuildPlanModelQuery query);


        /// <summary>
        /// 获取符合查询条件的总数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long eJITBuildPlanSearchDataGetRowCount(ReporteJITBuildPlanModelQuery query);




        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReporteJITBuildPlanDetailModel.Item> GeteJITBuildPlanDetailByPage(ReporteJITBuildPlanModelQuery query);


        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReporteJITBuildPlanDetailModel.Item> GeteJITBuildPlanDetailData(ReporteJITBuildPlanModelQuery query);


        /// <summary>
        /// 获取符合查询条件的总数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long eJITBuildPlanDetailDataGetRowCount(ReporteJITBuildPlanModelQuery query);
    }
}
