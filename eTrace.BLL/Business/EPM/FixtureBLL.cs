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
    public class SMFixtureInspHeaderModuleBLL : eTraceBLLBase<SMFixtureInspHeaderModuleBLL, IFixtureModuleBLL>, IFixtureModuleBLL
    {
        private IFixtureDAL fixtureModuleDAL = null;

        public SMFixtureInspHeaderModuleBLL()
        {
            fixtureModuleDAL=DBManager.Instance.GetFixtureModuleDAL(EmDBType.eTraceConnection);
        }

        public ReportFixtureModel GeFixtures(ReportFixtureQuery query)
        {
            return fixtureModuleDAL.GetFixtures(query);
        }

        public long FixtureDataGetRowCount(ReportFixtureQuery query)
        {
            return fixtureModuleDAL.FixtureDataGetRowCount(query);
        }


        public List<string> GeFixtureStatus()
        {
            return fixtureModuleDAL.GeFixtureStatus();
        }
    }
}
