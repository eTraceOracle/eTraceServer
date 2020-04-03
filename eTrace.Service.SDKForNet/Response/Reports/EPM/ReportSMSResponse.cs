using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Response
{
    public class ReportSMSDetailResponse : ServerResponseBase<List<ReportSMSDetailResponse.Item>>
    {
        public class Item : ReportSMSDetailModel.Detail

        {

        }

    }

    public class ReportSMSSearchResponse : ServerResponseBase<List<ReportSMSSearchResponse.Item>>
    {

        public class Item : ReportSMSSummaryModel.Summary

        {

        }

    }


}
