using eTrace.Model.V2.Report;
using eTrace.Model.V2.Report.DailyRepairList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IRepairBLL
    {
        /// <summary>
        /// 获取数据不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<GetListOfRepairDataModel.Item> GetListOfRepairData(GetListOfRepairDataQuery query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        GetListOfRepairDataModel GetListOfRepairDataByPage(GetListOfRepairDataQuery query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long GetListOfRepairDataTotalCount(GetListOfRepairDataQuery query);

        #region  DailyRepairList
        /// <summary>
        /// 获取数据不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<GetWipInWipOutModel.Item> GetWipInWipOutData(GetWipInWipOutQuery query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        GetWipInWipOutModel GetWipInWipOutDataByPage(GetWipInWipOutQuery query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long GetWipInWipOutDataTotalCount(GetWipInWipOutQuery query);



        /// <summary>
        /// 获取数据不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<GetWipOutModel.Item> GetWipOutData(GetWipOutQuery query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        GetWipOutModel GetWipOutDataByPage(GetWipOutQuery query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long GetWipOutDataTotalCount(GetWipOutQuery query);
        /// <summary>
        /// 获取数据不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<GetTopTenComonentModel.Item> GetTopTenComonentData(GetTopTenComonentQuery query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        GetTopTenComonentModel GetTopTenComonentDataByPage(GetTopTenComonentQuery query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long GetTopTenComonentDataTotalCount(GetTopTenComonentQuery query);
        /// <summary>
        /// 获取数据不分页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<GetMoreThanOneModel.Item> GetMoreThanOneData(GetMoreThanOneQuery query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        GetMoreThanOneModel GetMoreThanOneDataByPage(GetMoreThanOneQuery query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long GetMoreThanOneDataTotalCount(GetMoreThanOneQuery query);

        #endregion


    }


}
