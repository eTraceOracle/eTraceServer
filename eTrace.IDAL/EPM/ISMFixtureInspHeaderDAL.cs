using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface ISMFixtureInspHeaderDAL
    {

        /// <summary>
        /// 获取Equipment Data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportSMFixtureInspHeaderModel GetSMFixtureInspHeaderDatas(ReportSMFixtureInspHeaderModelQuery query);

        ReportSMFixtureInspItemModel GetSMFixtureInspItemDatas(ReportSMFixtureInspHeaderModelQuery query);

        long SMFixtureInspHeaderDataGetRowCount(ReportSMFixtureInspHeaderModelQuery query);
    }
}
