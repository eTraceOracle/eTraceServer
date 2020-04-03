using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportSOShipmentDetailResponse : ServerResponseBase<List<ReportSOShipmentDetailResponse.Detail>>
    {
        public class Detail : ReportSOShipmentDetailModel.Detail

        {

        }

    }

    public class ReportSOShipmentSummaryResponse : ServerResponseBase<List<ReportSOShipmentSummaryResponse.Summary>>
    {

        public class Summary : ReportSOShipmentSummaryModel.Summary

        {

        }

    }

 
}
