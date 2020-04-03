using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IProductArchiveModuleBLL
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
        ReportProductTestDataArchiveModel GetProductTestData(ReportProductTestDataArchiveQuery query);
        ReportProductTestSummaryDataArchiveModel GetProductTestDataSummary(ReportProductTestDataArchiveQuery query);

        ReportProductArchiveDataModel GetProductDatasArchive(ReportProductArchiveDataQuery query);
        long ProductTestDataGetRowCount(ReportProductTestDataArchiveQuery query);
        long ProductTestDataSummaryGetRowCount(ReportProductTestDataArchiveQuery query);

        long ProductDataGetRowCount(ReportProductArchiveDataQuery query);


    }
}
