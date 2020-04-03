using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IFGAgingBLL

    {
        ReportFGAgingDetailModel GetFGAgingDetailData(ReportFGAgingModelQuery query);

        ReportFGAgingSummaryModel GetFGAgingSummaryData(ReportFGAgingModelQuery query);


        long FGAgingDataGetRowCount(ReportFGAgingModelQuery query);
    }
}
