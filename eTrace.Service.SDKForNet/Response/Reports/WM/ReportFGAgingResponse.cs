using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportFGAgingDetailResponse : ServerResponseBase<List<ReportFGAgingDetailResponse.Detail>>
    {
        public class Detail : ReportFGAgingDetailModel.Detail

        {

        }

    }

    public class ReportFGAgingSummaryResponse : ServerResponseBase<List<ReportFGAgingSummaryResponse.Summary>>
    {

        public class Summary : ReportFGAgingSummaryModel.Summary

        {

        }

    }


}
