using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportLabelInfoDetailResponse : ServerResponseBase<List<ReportLabelInfoDetailResponse.Detail>>
    {
        public class Detail : ReportLabelInfoDetailModel.Detail

        {

        }

    }

    public class ReportLabelInfoSummaryResponse : ServerResponseBase<List<ReportLabelInfoSummaryResponse.Summary>>
    {

        public class Summary : ReportLabelInfoSummaryModel.Summary

        {

        }

    }

    public class ReportLabelInfoePurgeDTResponse : ServerResponseBase<List<ReportLabelInfoePurgeDTResponse.Detail>>
    {
        public class Detail : ReportLabelInfoePurgeDTModel.Detail

        {

        }

    }

    public class ReportLabelInfoePurgeSMResponse : ServerResponseBase<List<ReportLabelInfoePurgeSMResponse.Summary>>
    {

        public class Summary : ReportLabelInfoePurgeSMModel.Summary

        {

        }

    }
}
