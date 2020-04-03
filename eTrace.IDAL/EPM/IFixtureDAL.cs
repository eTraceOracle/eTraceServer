using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IDAL
{
    public interface IFixtureDAL
    {
        ReportFixtureModel GetFixtures(ReportFixtureQuery query);

        long FixtureDataGetRowCount(ReportFixtureQuery query);

        List<string> GeFixtureStatus();
    }
}
