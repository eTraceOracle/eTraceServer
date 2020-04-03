using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface ISMFixtureTransModuleBLL
    {
        ReportSMFixtureTransModel GetSMFixtureTransDatas(ReportSMFixtureTransModelQuery query);

        //Initialization Repair Status List
        List<string> GetSMFixtureTransCate();
        List<string> GetSMFixtureTransSubCate();
        List<string> GetSMFixtureTransStatus();
        List<string> GetSMFixtureTransTransactionType();
        long SMFixtureTransDataGetRowCount(ReportSMFixtureTransModelQuery query);
    }
}
