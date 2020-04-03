using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ISMSDAL
    {

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReportSMSSummaryModel.Summary> GetSMSSummaryByPage(ReportSMSModelQuery query);


        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReportSMSSummaryModel.Summary> GetSMSSummaryData(ReportSMSModelQuery query);


        /// <summary>
        /// 获取符合查询条件的总数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long SMSSummaryDataGetRowCount(ReportSMSModelQuery query);




        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReportSMSDetailModel.Detail> GetSMSDetailByPage(ReportSMSModelQuery query);


        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<ReportSMSDetailModel.Detail> GetSMSDetailData(ReportSMSModelQuery query);


        /// <summary>
        /// 获取符合查询条件的总数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        long SMSDetailDataGetRowCount(ReportSMSModelQuery query);
    }
}
