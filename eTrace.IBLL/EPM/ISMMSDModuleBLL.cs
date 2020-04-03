using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface ISMMSDModuleBLL
    {
        ReportSMMSDModel GetSMMSDDatas(ReportSMMSDModelQuery query);

        ReportSMMSDPMItemModel GetSMMSDPMItemDatas(ReportSMMSDModelQuery query);

        List<string> GetSMMSDLastTransaction();

        long SMMSDDataGetRowCount(ReportSMMSDModelQuery query);
    }
}
