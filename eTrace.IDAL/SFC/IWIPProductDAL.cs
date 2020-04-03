using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IWIPProductDAL
    {
        ReportWIPProductModel GetWIPUnitFlow(WIPUnitFlowQuery query);


        WIPHeaderModel GetWIPheaderByIntSN(string intSN);
        List<WIPFlowModel> GetWIPFlowListByIntSN(string intSN);
        List<WIPPropertyModel> GetWIPPropertyListByIntSN(string intSN);
        List<WIPTestDataModel> GetWIPTestDataListByIntSN(string intSN);

        ReportWIPTestDataModel GetWIPTestData(ReportWIPTestDataQuery query);
        ReportWIPTDHeaderModel GetWIPTDHeader(ReportWIPTestDataQuery query);
        long ReportWIPTDHeaderGetRowCount(ReportWIPTestDataQuery query);
        long ReportWIPTestDataGetRowCount(ReportWIPTestDataQuery query);

        ReportWIPPropertiesModel GetWIPProperties(ReportWIPTestDataQuery query);
        
    }
}
