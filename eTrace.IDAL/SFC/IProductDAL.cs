using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IProductDAL
    {

        /// <summary>
        /// TDHeader数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportTDHeaderModel GetTDHeaders(ReportTDHeaderQuery query);
            
        /// <summary>
        /// 获取Product Test Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportProductTestDataModel GetProductTestData(ReportProductTestDataQuery query);
        /// <summary>
        /// 获取Product Test Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportProductTestSummaryDataModel GetProductTestDataSummary(ReportProductTestDataQuery query);

        ReportProductDataModel GetProductDatas(ReportProductDataQuery query);

        long ProductTestDataGetRowCount(ReportProductTestDataQuery query);
        long ProductTestDataSummaryGetRowCount(ReportProductTestDataQuery query);
        long ProductDataGetRowCount(ReportProductDataQuery query);
    }
}
