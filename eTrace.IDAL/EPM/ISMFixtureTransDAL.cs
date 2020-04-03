using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ISMFixtureTransDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportSMFixtureTransModel GetSMFixtureTransDatas(ReportSMFixtureTransModelQuery query);
                                                                               
        List<string> GetSMFixtureTransCate();

        List<string> GetSMFixtureTransSubCate();

        List<string> GetSMFixtureTransStatus();

        List<string> GetSMFixtureTransTransactionType();

        long SMFixtureTransDataGetRowCount(ReportSMFixtureTransModelQuery query);
    }
}
