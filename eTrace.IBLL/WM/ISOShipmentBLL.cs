using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface ISOShipmentBLL

    {
        ReportSOShipmentDetailModel GetSOShipmentDetailData(ReportSOShipmentModelQuery query);

        ReportSOShipmentSummaryModel GetSOShipmentSummaryData(ReportSOShipmentModelQuery query);


        long SOShipmentDataGetRowCount(ReportSOShipmentModelQuery query);
    }
}
