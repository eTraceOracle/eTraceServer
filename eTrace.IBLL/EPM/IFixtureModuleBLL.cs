using eTrace.Model.V2.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTrace.Report.IBLL
{
    public interface IFixtureModuleBLL
    {
        /// <summary>
        /// 获取Fixture数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ReportFixtureModel GeFixtures(ReportFixtureQuery query);

        List<string> GeFixtureStatus();

        long FixtureDataGetRowCount(ReportFixtureQuery query);
    }
}
