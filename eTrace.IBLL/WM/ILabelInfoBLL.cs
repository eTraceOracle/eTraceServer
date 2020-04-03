using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface ILabelInfoBLL

    {
        ReportLabelInfoDetailModel GetLabelInfoDetailData(ReportLabelInfoModelQuery query);

        ReportLabelInfoSummaryModel GetLabelInfoSummaryData(ReportLabelInfoModelQuery query);

        ReportLabelInfoePurgeDTModel GetLabelInfoePurgeDTData(ReportLabelInfoModelQuery query);

        ReportLabelInfoePurgeSMModel GetLabelInfoePurgeSMData(ReportLabelInfoModelQuery query);


        long LabelInfoDataGetRowCount(ReportLabelInfoModelQuery query);
    }
}
