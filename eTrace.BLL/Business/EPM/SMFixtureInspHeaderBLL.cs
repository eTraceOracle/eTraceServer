using eTrace.Common;
using eTrace.Core;
using eTrace.Report.IBLL;
using eTrace.Report.IDAL;
using eTrace.Model;
using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.BLL.Business
{
    public class SMFixtureInspHeaderBLL : eTraceBLLBase<SMFixtureInspHeaderBLL, ISMFixtureInspHeaderModuleBLL>, ISMFixtureInspHeaderModuleBLL
    {
        private ISMFixtureInspHeaderDAL tdISMFixtureInspHeaderDAL = null;
        public SMFixtureInspHeaderBLL()
        {
            tdISMFixtureInspHeaderDAL = DBManager.Instance.GetSMFixtureInspHeaderModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportSMFixtureInspHeaderModel  GetSMFixtureInspHeaderDatas(ReportSMFixtureInspHeaderModelQuery query)
        {
            return tdISMFixtureInspHeaderDAL.GetSMFixtureInspHeaderDatas(query);
        }

        public ReportSMFixtureInspItemModel GetSMFixtureInspItemDatas(ReportSMFixtureInspHeaderModelQuery query)
        {
            return tdISMFixtureInspHeaderDAL.GetSMFixtureInspItemDatas(query);
        }

        public long SMFixtureInspHeaderDataGetRowCount(ReportSMFixtureInspHeaderModelQuery query)
        {
            return tdISMFixtureInspHeaderDAL.SMFixtureInspHeaderDataGetRowCount (query);
        }

    }
}
