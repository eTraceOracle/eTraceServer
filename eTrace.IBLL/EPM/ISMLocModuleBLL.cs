using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface ISMLocModuleBLL
    {
        ReportSMLocModel GetSMLocDatas(ReportSMLocModelQuery query);

        //List<string> GetSMLocCate();
        ReportSMLocModel GetSMLocCate();

        List<string> GetSMLocCategory();
        List<string> GetSMLocSubCategory();
        List<string> GetSMLocStore();

        long SMLocDataGetRowCount(ReportSMLocModelQuery query);
    }
}
