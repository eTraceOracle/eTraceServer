using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Service.SDKForNet.Request.Reports
{
     public class GetWipStatusDataRequest : ServerRequestBase<GetWipStatusDataRequestItem>
    {

    }
    public class DownloadWipStatusDataRequest : ReportDownloadRequestBase<GetWipStatusDataRequestItem>
    {

    }

    public class GetWipStatusDataRequestItem:eTrace.Model.V2.Report.GetWipStatusQuery.Item
    {
        
    }
}
