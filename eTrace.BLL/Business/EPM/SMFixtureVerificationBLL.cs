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
    public class SMFixtureVerificationBLL : eTraceBLLBase<SMFixtureVerificationBLL, ISMFixtureVerificationModuleBLL>, ISMFixtureVerificationModuleBLL
    {
        private ISMFixtureVerificationDAL tdISMFixtureVerificationDAL = null;
        public SMFixtureVerificationBLL()
        {
            tdISMFixtureVerificationDAL = DBManager.Instance.GetSMFixtureVerificationDAL(EmDBType.eTraceConnection);
        }

        public ReportSMFixtureVerificationModel GetSMFixtureVerificationDatas(ReportSMFixtureVerificationModelQuery query)
        {
            return tdISMFixtureVerificationDAL.GetSMFixtureVerificationDatas(query);
        }

        public long SMFixtureVerificationDataGetRowCount(ReportSMFixtureVerificationModelQuery query)
        {
            return tdISMFixtureVerificationDAL.SMFixtureVerificationDataGetRowCount (query);
        }

    }
}
