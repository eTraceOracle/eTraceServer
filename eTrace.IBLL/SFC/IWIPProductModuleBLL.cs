using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IWIPProductModuleBLL
    {
        ReportWIPProductModel GetWIPUnitFlow(WIPUnitFlowQuery query);
        WipInfo GetWipInfo(string  intSN);

        GetWipLockDataResultModel GetWipLockData(GetWipLockDataModelQuery query);
        GetWipLockDataResultModel GetWipLockDataByPage(GetWipLockDataModelQuery query);
        long GetWipLockDataTotalCount(GetWipLockDataModelQuery query);

        ReportWIPTestDataModel GetWIPTestData(ReportWIPTestDataQuery query);

        ReportWIPTDHeaderModel GetWIPTDHeader(ReportWIPTestDataQuery query);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        WipStatusDetailModel GetWipStatusDetailByPage(GetWipStatusQuery query);
        List<WipStatusDetailItem> GetWipStatusDetail(GetWipStatusQuery query);
        long GetWipStatusDetailTotalCount(GetWipStatusQuery queryItem);

        WipStatusSummaryModel GetWipStatusSummaryByPage(GetWipStatusQuery query);
        List<WipStatusSummaryItem> GetWipStatusSummary(GetWipStatusQuery queryItem);
        long GetWipStatusSummaryTotalCount(GetWipStatusQuery queryItem);
        long ReportWIPTDHeaderGetRowCount(ReportWIPTestDataQuery query);

        long ReportWIPTestDataGetRowCount(ReportWIPTestDataQuery query);
        ReportWIPPropertiesModel GetWIPProperties(ReportWIPTestDataQuery query);

    }
}
