using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface ISMSBLL

    {
        ReportSMSSummaryModel GetSMSSummaryData(ReportSMSModelQuery query);
        long SMSSummaryDataGetRowCount(ReportSMSModelQuery query);
        ReportSMSDetailModel GetSMSDetailData(ReportSMSModelQuery query);
        long SMSDetailDataGetRowCount(ReportSMSModelQuery query);

    }
}
